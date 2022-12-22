using Contracts.Company.Responses;
using Contracts.Job.Companies.Requests;
using Contracts.Job.Jobs.Requests;
using Contracts.Shared;
using Domain.Seedwork.Enums;
using System.ComponentModel.DataAnnotations;

namespace FrontOffice.Bff.API.Areas.Company.Models.Shared
{
    public class InitializeJobRequest
    {
        [Required]
        public Position Position { get; set; } = null!;

        public DateTimeOffset? StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public bool IsUrgent { get; set; }

        public ICollection<WorkType> WorkTypes { get; set; } = new List<WorkType>();

        public InitializePendingJobRequest ToPendingJobServiceRequest(GetCompanyResponse company, string UserEmail)
        {
            var contactPerson = company.ContactPersons.Single(x => x.Email == UserEmail);

            return new InitializePendingJobRequest
            {
                Company = new CreateJobCompanyRequest
                {
                    Id = company.Id,
                    Status = company.Status,
                    Name = company.Name!,
                    LogoUri = company.Logo?.Uri,
                    Address = company.Address,
                    ContactPersons = new List<JobContactPersonRequest>
                    {
                        new JobContactPersonRequest
                        {
                            Id = contactPerson.Id,
                            Email = contactPerson.Email,
                            FirstName = contactPerson.FirstName!,
                            LastName = contactPerson.LastName!,
                            PictureUri = contactPerson.Picture?.Uri,
                            IsMainContact = true,
                            Position = contactPerson.Position,
                            PhoneNumber = contactPerson.Phone?.PhoneNumber,
                            SystemLanguage = contactPerson.SystemLanguage,
                            ExternalId = contactPerson.ExternalId
                        }
                    }
                },
                Position = Position,
                StartDate = StartDate,
                EndDate = EndDate,
                IsUrgent = IsUrgent,
                WorkTypes = WorkTypes
            };
        }
    }
}
