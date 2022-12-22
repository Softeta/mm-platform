using API.Customization.Authorization.Constants;
using API.Customization.Extensions;
using Contracts.Job;
using Contracts.Job.Companies.Responses;
using Contracts.Job.Jobs.Responses;
using Domain.Seedwork.Consts;
using Domain.Seedwork.Exceptions;
using Jobs.Application.Queries.Jobs.Extensions;
using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TopologyPoint = NetTopologySuite.Geometries.Point;

namespace Jobs.Application.Queries.Jobs
{
    public class GetJobsQueryHandler : IRequestHandler<GetJobsQuery, GetJobsResponse>
    {
        private readonly IJobContext _context;

        public GetJobsQueryHandler(IJobContext context)
        {
            _context = context;
        }

        public async Task<GetJobsResponse> Handle(GetJobsQuery request, CancellationToken cancellationToken)
        {
            ValidateAccess(request);

            var radiusInMeters = request.RadiusInKm.ToMeters();
            TopologyPoint? requestCoordinates = null;

            if (request.Latitude.HasValue && request.Longitude.HasValue && request.RadiusInKm.HasValue)
            {
                requestCoordinates = new TopologyPoint(
                    request.Longitude.Value, 
                    request.Latitude.Value) { SRID = SpatialReferenceIds.Gps };
            }

            Expression<Func<Job, bool>> filterByLocation = job =>
                requestCoordinates == null || (
                job.Company.Address != null &&
                job.Company.Address.Coordinates != null &&
                job.Company.Address.Coordinates.Point.Distance(requestCoordinates) <= radiusInMeters);

            Expression<Func<Job, bool>> filterByUserId = job =>
                !request.UserId.HasValue || 
                job.AssignedEmployees.Any(e => e.Employee.Id == request.UserId) ||
                (job.Owner != null && job.Owner.Id == request.UserId);

            Expression<Func<Job, bool>> filterByAssignedEmployees = job =>
                request.AssignedEmployees == null || job.AssignedEmployees.Any(e => request.AssignedEmployees.Contains(e.Employee.Id));

            Expression<Func<Job, bool>> filterByCompanies = job =>
                request.Companies == null || request.Companies.Any(c => job.Company.Id == c);

            Expression<Func<Job, bool>> filterByPositions = job =>
                request.Positions == null || request.Positions.Contains(job.Position.Id);

            Expression<Func<Job, bool>> filterByDeadLine = job =>
                request.DeadLineDate == null || job.DeadlineDate < request.DeadLineDate;

            Expression<Func<Job, bool>> filterByJobStages = job =>
                request.JobStages == null || request.JobStages.Contains(job.Stage);

            Expression<Func<Job, bool>> filterByWorkType = job =>
                request.WorkTypes == null ||
                (job.Terms != null && job.Terms.Permanent != null && request.WorkTypes.Contains(job.Terms.Permanent.WorkType)) ||
                (job.Terms != null && job.Terms.Freelance != null && request.WorkTypes.Contains(job.Terms.Freelance.WorkType));

            Expression<Func<Job, bool>> filterByOwner = job =>
                request.Owner == null || (job.Owner != null && request.Owner == job.Owner.Id);

            Expression<Func<Job, bool>> filterByCreationDate = job =>
                request.CreatedAt == null || job.CreatedAt >= request.CreatedAt;

            Expression<Func<Job, bool>> filterBySearch = job =>
                string.IsNullOrWhiteSpace(request.Search) ||
                job.Skills.Select(s => s.Code.ToLower()).Any(c => c.Contains(request.Search.ToLower())) ||
                job.Industries.Select(i => i.Code.ToLower()).Any(c => c.Contains(request.Search.ToLower()));

            Expression<Func<Job, bool>> filterByCandidateScope = job =>
                request.Scope != CustomScopes.FrontOffice.Candidate || job.IsPublished;

            Expression<Func<Job, bool>> filterByExcludedJobIds = job =>
                request.ExcludedJobIds == null || !request.ExcludedJobIds.Contains(job.Id);

            Expression<Func<Job, bool>> filterByJobIds = job => 
                request.JobIds == null || request.JobIds.Contains(job.Id);

            var query = _context.Jobs
                .Include(x => x.AssignedEmployees)
                .AsSplitQuery()
                .Where(filterByUserId)
                .Where(filterByAssignedEmployees)
                .Where(filterByCompanies)
                .Where(filterByPositions)
                .Where(filterByDeadLine)
                .Where(filterByWorkType)
                .Where(filterByJobStages)
                .Where(filterByOwner)
                .Where(filterByLocation)
                .Where(filterByCreationDate)
                .Where(filterBySearch)
                .Where(filterByCandidateScope)
                .Where(filterByExcludedJobIds)
                .Where(filterByJobIds);

            query = query.OrderJobsData(request.OrderBy);

            var count = await query.CountAsync(cancellationToken);

            var jobs = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new GetJobBriefResponse
                {
                    JobId = x.Id,
                    CompanyName = x.Company.Name,
                    CompanyLogoUri = x.Company.LogoUri,
                    Position = x.Position.Code,
                    Freelance = x.Terms != null && x.Terms.Freelance != null ? x.Terms.Freelance.WorkType : null,
                    Permanent = x.Terms != null && x.Terms.Permanent != null ? x.Terms.Permanent.WorkType : null,
                    JobStage = x.Stage,
                    DeadlineDate = x.DeadlineDate,
                    AssignedTo = x.AssignedEmployees.Select(x => new Employee
                    {
                        Id = x.Employee.Id,
                        FirstName = x.Employee.FirstName,
                        LastName = x.Employee.LastName,
                        PictureUri = x.Employee.PictureUri
                    }),
                    MainContact = x.Company.ContactPersons
                    .Where(c => c.IsMainContact)
                    .Select(c => new JobContactPersonResponse
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Email = c.Email,
                        IsMainContact = c.IsMainContact,

                    }).Take(1).FirstOrDefault(),
                    Owner = x.Owner != null
                    ? new Employee
                    {
                        Id = x.Owner.Id,
                        FirstName = x.Owner.FirstName,
                        LastName = x.Owner.LastName,
                    }
                    : null,
                    IsPriority = x.IsPriority,
                    CreatedAt = x.CreatedAt,
                    IsArchived = x.IsArchived
                }).ToListAsync(cancellationToken);
            
            return new GetJobsResponse(count, jobs);
        }

        private static void ValidateAccess(GetJobsQuery request)
        {
            var isInvalid = request.Scope == CustomScopes.FrontOffice.Company 
                && ((request.Companies is not null && request.Companies.All(c => c == request.CompanyId) == false) 
                    || (request.Companies is null));

            if (isInvalid)
            {
                throw new ForbiddenException("Forbidden to access other companies data from the FrontOffice scope.",
                    ErrorCodes.Forbidden.AccessOtherCompaniesData);
            }
        }
    }
}