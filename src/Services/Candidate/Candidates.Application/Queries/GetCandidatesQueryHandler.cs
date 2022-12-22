using API.Customization.Extensions;
using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using Candidates.Infrastructure.Persistence;
using Contracts.Candidate;
using Contracts.Candidate.Candidates.Responses;
using Contracts.Candidate.Notes.Responses;
using Contracts.Shared;
using Contracts.Shared.Responses;
using Domain.Seedwork.Consts;
using Domain.Seedwork.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TopologyPoint = NetTopologySuite.Geometries.Point;

namespace Candidates.Application.Queries
{
    public class GetCandidatesQueryHandler : IRequestHandler<GetCandidatesQuery, GetCandidatesResponse>
    {
        private readonly ICandidateContext _context;
        public GetCandidatesQueryHandler(ICandidateContext context)
        {
            _context = context;
        }

        public async Task<GetCandidatesResponse> Handle(GetCandidatesQuery request, CancellationToken cancellationToken)
        {
            var radiusInMeters = request.RadiusInKm.ToMeters();
            TopologyPoint? requestCoordinates = null;

            if (request.Latitude.HasValue && request.Longitude.HasValue && request.RadiusInKm.HasValue)
            {
                requestCoordinates = new TopologyPoint(
                    request.Longitude.Value,
                    request.Latitude.Value)
                { SRID = SpatialReferenceIds.Gps };
            }

            Expression<Func<Candidate, bool>> filterByIds = candidate =>
                request.Ids == null || request.Ids.Contains(candidate.Id);

            Expression<Func<Candidate, bool>> filterByName = candidate =>
                request.Name == null || (candidate.FirstName + " " + candidate.LastName).Contains(request.Name);

            Expression<Func<Candidate, bool>> filterByPositions = candidate =>
                request.Positions == null ||
                (candidate.CurrentPosition != null && request.Positions.Contains(candidate.CurrentPosition!.Id));

            Expression<Func<Candidate, bool>> filterByLocation = candidate =>
                requestCoordinates == null || (
                candidate.Address != null &&
                candidate.Address.Coordinates != null &&
                candidate.Address.Coordinates.Point.Distance(requestCoordinates) <= radiusInMeters);

            Expression<Func<Candidate, bool>> filterByOpenForOpportunities = candidate =>
                request.OpenForOpportunities == null || request.OpenForOpportunities == candidate.OpenForOpportunities;

            Expression<Func<Candidate, bool>> filterByIsFreelance = candidate =>
                request.IsFreelance == null || request.IsFreelance == (candidate.Terms.Freelance != null);

            Expression<Func<Candidate, bool>> filterByIsPermanent = candidate =>
                request.IsPermanent == null || request.IsPermanent == (candidate.Terms.Permanent != null);

            Expression<Func<Candidate, bool>> filterByAvailableFrom = candidate =>
                request.AvailableFrom == null ||
                (candidate.Terms != null && candidate.Terms.Availability != null && request.AvailableFrom >= candidate.Terms.Availability.From);

            Expression<Func<Candidate, bool>> filterByHourlyBudgetTo = candidate =>
                request.HourlyBudgetTo == null ||
                (candidate.Terms != null
                && candidate.Terms.Freelance != null
                && candidate.Terms.Freelance.HourlySalary.HasValue
                && request.HourlyBudgetTo >= candidate.Terms.Freelance.HourlySalary.Value);

            Expression<Func<Candidate, bool>> filterByMounthlyBudgetTo = candidate =>
                request.MonthlyBudgetTo == null 
                || candidate.Terms != null
                && (candidate.Terms.Permanent != null
                && candidate.Terms.Permanent.MonthlySalary.HasValue
                && request.MonthlyBudgetTo >= candidate.Terms.Permanent.MonthlySalary.Value || 
                (candidate.Terms.Freelance != null
                && candidate.Terms.Freelance.MonthlySalary.HasValue
                && request.MonthlyBudgetTo >= candidate.Terms.Freelance.MonthlySalary.Value));

            Expression<Func<Candidate, bool>> filterByCurrency = candidate =>
                request.Currency == null ||
                (candidate.Terms != null
                && candidate.Terms.Currency != null
                && request.Currency == candidate.Terms.Currency);

            Expression<Func<Candidate, bool>> filterByStatuses = candidate =>
                request.Statuses == null || request.Statuses.Contains(candidate.Status);

            Expression<Func<Candidate, bool>> notRejected = candidate =>
                candidate.Status != CandidateStatus.Rejected;

            var candidatesInJob = await GetCandidatesInJobAsync(request.JobId, request.Ids);

            Expression<Func<Candidate, bool>> filterByJobId = candidate =>
                !request.JobId.HasValue || !candidatesInJob.Any(x => x == candidate.Id);

            Expression<Func<Candidate, bool>> filterBySearch = candidate =>
                string.IsNullOrWhiteSpace(request.Search) ||
                candidate.Skills.Select(s => s.Code.ToLower()).Any(c => c.Contains(request.Search.ToLower())) ||
                candidate.Industries.Select(i => i.Code.ToLower()).Any(c => c.Contains(request.Search.ToLower())) ||
                candidate.WorkExperiences.Select(w => w.CompanyName.ToLower()).Any(c => c.Contains(request.Search.ToLower())) ||
                candidate.Educations.Select(e => e.SchoolName.ToLower()).Any(c => c.Contains(request.Search.ToLower())) ||
                candidate.Courses.Select(c => c.Name.ToLower()).Any(c => c.Contains(request.Search.ToLower()));

            var query = _context.Candidates
                .Where(filterByIds)
                .Where(filterByName)
                .Where(filterByPositions)         
                .Where(filterByLocation)
                .Where(filterByOpenForOpportunities)
                .Where(filterByIsFreelance)
                .Where(filterByIsPermanent)
                .Where(filterByAvailableFrom)
                .Where(filterByHourlyBudgetTo)
                .Where(filterByMounthlyBudgetTo)
                .Where(filterByCurrency)
                .Where(filterByStatuses)
                .Where(notRejected)
                .Where(filterByJobId)
                .Where(filterBySearch)
                .OrderByDescending(c => c.CreatedAt);

            var count = await query.CountAsync(cancellationToken);
            
            var candidates = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new GetCandidateBriefResponse
                {
                    Id = x.Id,
                    Picture = x.Picture != null
                        ? new ImageResponse
                        {
                            Uri = x.Picture.ThumbnailUri
                        }
                        : null,
                    FullName = $"{x.FirstName} {x.LastName}",
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email != null ? x.Email.Address : null,
                    Phone = x.Phone != null
                        ? new PhoneFullResponse
                        {
                            CountryCode = x.Phone.CountryCode,
                            Number = x.Phone.Number,
                            PhoneNumber = x.Phone.PhoneNumber,
                        }
                        : null,
                    LinkedInUrl = x.LinkedInUrl,
                    CurrentPosition = x.CurrentPosition != null
                        ? new Position
                        {
                            Id = x.CurrentPosition.Id,
                            Code = x.CurrentPosition.Code
                        }
                        : null,
                    Address = x.Address != null
                        ? new AddressWithLocation
                        {
                            AddressLine = x.Address.AddressLine,
                            Location = x.Address.Location,
                            City = x.Address.City,
                            Country = x.Address.Country,
                            PostalCode = x.Address.PostalCode,
                            Longitude = x.Address.Coordinates != null ? x.Address.Coordinates.Longitude : null,
                            Latitude = x.Address.Coordinates != null ? x.Address.Coordinates.Latitude : null
                        }
                        : null,
                    StartDate = x.Terms.Availability != null ? x.Terms.Availability.From : null,
                    EndDate = x.Terms.Availability != null ? x.Terms.Availability.To : null,
                    Currency = x.Terms.Currency,
                    Freelance = x.Terms.Freelance != null
                        ? new CandidateFreelance
                        {
                            HourlySalary = x.Terms.Freelance.HourlySalary,
                            MonthlySalary = x.Terms.Freelance.MonthlySalary,
                        }
                        : null,
                    Permanent = x.Terms.Permanent != null
                        ? new CandidatePermanent
                        {
                            MonthlySalary = x.Terms.Permanent.MonthlySalary,
                        }
                        : null,
                    OpenForOpportunities = x.OpenForOpportunities,
                    IsShortlisted = x.IsShortListed,
                    Note = x.Note != null ? new NoteResponse
                    {
                        Value = x.Note.Value,
                        EndDate = x.Note.EndDate
                    } : null,
                    CreatedAt = x.CreatedAt,
                    IsHired = x.IsHired,
                    SystemLanguage = x.SystemLanguage
                }).ToListAsync(cancellationToken);

            return new GetCandidatesResponse(count, candidates);
        }

        private async Task<List<Guid>> GetCandidatesInJobAsync(Guid? jobId, IEnumerable<Guid>? candidates)
        {
            if (jobId != null)
            {
                Expression<Func<CandidateJobs, bool>> filterByIds = candidate =>
                    candidates == null || candidates.Contains(candidate.Id);

                return await _context.CandidateJobs
                    .Where(filterByIds)
                    .Where(x => x.SelectedInJobs.Any(x => x.JobId == jobId) || 
                                x.ArchivedInJobs.Any(x => x.JobId == jobId))
                    .Select(x => x.Id)
                    .ToListAsync();
            }

            return new List<Guid>();
        }
    }
}
