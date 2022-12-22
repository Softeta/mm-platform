using API.WebClients.Clients.HereSearch;
using API.WebClients.Clients.HereSearch.Models;
using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Jobs.Domain.Aggregates.JobAggregate.ValueObjects;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands
{
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Guid>
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILocationProvider _locationProvider;

        public CreateJobCommandHandler(IJobRepository jobRepository, ILocationProvider locationProvider)
        {
            _jobRepository = jobRepository;
            _locationProvider = locationProvider;
        }

        public async Task<Guid> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var job = new Job();

            var addressDetails = await GetAddressDetailsAsync(request.Company.Address?.AddressLine);

            job.Create(
                new Company(
                    request.Company.Id,
                    request.Company.Status,
                    request.Company.Name,
                    request.Company.Address?.AddressLine,
                    addressDetails?.City,
                    addressDetails?.Country,
                    addressDetails?.PostalCode,
                    addressDetails?.Longitude,
                    addressDetails?.Latitude,
                    request.Company.Description,
                    request.Company.LogoUri,
                    request.Company.ContactPersons.Select(p => new JobContactPerson(
                        job.Id,
                        p.Id,
                        p.IsMainContact,
                        p.FirstName,
                        p.LastName,
                        p.PhoneNumber,
                        p.Email,
                        p.Position?.Id,
                        p.Position?.Code,
                        p.Position?.AliasTo?.Id,
                        p.Position?.AliasTo?.Code,
                        p.PictureUri,
                        p.SystemLanguage,
                        p.ExternalId))),
                new Employee(
                    request.Owner.Id,
                    request.Owner.FirstName,
                    request.Owner.LastName,
                    request.Owner.PictureUri),
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

            var jobEntity = _jobRepository.Add(job);
            await _jobRepository.UnitOfWork.SaveEntitiesAsync<Job>(cancellationToken);

            return jobEntity.Id;
        }

        private async Task<AddressDetails?> GetAddressDetailsAsync(string? addressLine)
        {
            if (string.IsNullOrWhiteSpace(addressLine))
            {
                return null;
            }

            return await _locationProvider.GetAddressDetailsAsync(addressLine);
        }
    }
}
