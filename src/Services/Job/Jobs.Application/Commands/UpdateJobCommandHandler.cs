using API.Customization.Authorization.Constants;
using Azure.Core;
using Domain.Seedwork.Exceptions;
using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Jobs.Domain.Aggregates.JobAggregate.ValueObjects;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.OpenApi.Writers;

namespace Jobs.Application.Commands
{
    public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, Job>
    {
        private readonly IJobRepository _jobRepository;

        public UpdateJobCommandHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<Job> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            ValidateOwner(request);

            var job = await _jobRepository.GetAsync(request.JobId);

            job.Update(
                BuildOwner(request),
                request.Position.Id,
                request.Position.Code,
                request.Position.AliasTo?.Id,
                request.Position.AliasTo?.Code,
                request.DeadLineDate,
                request.Description,
                request.StartDate,
                request.EndDate,
                request.WeeklyWorkHours,
                request.Currency,
                request.Freelance?.HoursPerProject,
                request.Freelance?.HourlyBudget?.From,
                request.Freelance?.HourlyBudget?.To,
                request.Freelance?.MonthlyBudget?.From,
                request.Freelance?.MonthlyBudget?.To,
                request.Permanent?.MonthlyBudget?.From,
                request.Permanent?.MonthlyBudget?.To,
                request.YearExperience?.From,
                request.YearExperience?.To,
                request.IsPriority,
                request.IsUrgent,
                request.WorkingHourTypes,
                request.WorkTypes,
                request.AssignedEmployees.Select(e => new JobAssignedEmployee(
                    job.Id,
                    e.Id,
                    e.FirstName,
                    e.LastName,
                    e.PictureUri)),
                request.Skills.Select(s => new JobSkill(s.Id, job.Id, s.Code, s.AliasTo?.Id, s.AliasTo?.Code)),
                request.Industries.Select(s => new JobIndustry(s.Id, job.Id, s.Code)),
                request.Seniorities.Select(s => new JobSeniority(job.Id, s)),
                request.Languages.Select(l => new JobLanguage(job.Id, l.Id, l.Code, l.Name)),
                request.Formats,
                request.InterestedCandidates.Select(
                    i => new JobInterestedCandidate(job.Id, i.Id, i.FirstName, i.LastName, i.Position, i.Picture?.Uri)),
                request.InterestedLinkedIns);

            _jobRepository.Update(job);
            await _jobRepository.UnitOfWork.SaveEntitiesAsync<Job>(cancellationToken);

            return job;
        }

        private static void ValidateOwner(UpdateJobCommand request)
        {
            if (request.Scope == CustomScopes.BackOffice.User && request.Owner is null)
            {
                throw new BadRequestException("Owner is required", ErrorCodes.BadRequest.OwnerIsRequired);
            }
        }

        private static Employee? BuildOwner(UpdateJobCommand request)
        {
            if (request.Owner is null)
            {
                return null;
            }

            return new Employee(
                    request.Owner.Id,
                    request.Owner.FirstName,
                    request.Owner.LastName,
                    request.Owner.PictureUri);
        }
    }
}
