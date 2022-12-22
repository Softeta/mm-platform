using Contracts.Job;
using Contracts.Job.Companies.Responses;
using Contracts.Shared;
using Contracts.Shared.Responses;
using Domain.Seedwork.Enums;
using Jobs.Domain.Aggregates.JobAggregate;
using Common = Contracts.Job.Jobs.Responses;
using Local = Jobs.Application.Contracts;

namespace Jobs.Application.Contracts.Jobs.Responses
{
    public class GetJobResponse : Common.GetJobResponse
    {
        public static Common.GetJobResponse FromDomain(Job job)
        {
            return new Common.GetJobResponse
            {
                Id = job.Id,
                Company = new CompanyResponse
                {
                    Id = job.Company.Id,
                    Name = job.Company.Name,
                    Address = job.Company.Address != null
                        ? new Address
                        {
                            AddressLine = job.Company.Address.AddressLine,
                            City = job.Company.Address.City,
                            Country = job.Company.Address.Country,
                            PostalCode = job.Company.Address.PostalCode,
                            Longitude = job.Company.Address.Coordinates?.Longitude,
                            Latitude = job.Company.Address.Coordinates?.Latitude
                        }
                        : null,
                    Description = job.Company.Description,
                    Logo = !string.IsNullOrEmpty(job.Company.LogoUri) 
                    ? new ImageResponse 
                    { 
                        Uri = job.Company.LogoUri 
                    } 
                    : null,
                    ContactPersons = job.Company.ContactPersons.Select(p => new JobContactPersonResponse
                    {
                        Id = p.PersonId,
                        IsMainContact = p.IsMainContact,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Position = Position.FromDomain(p.Position),
                        Email = p.Email,
                        PhoneNumber = p.PhoneNumber,
                        Picture = !string.IsNullOrEmpty(p.PictureUri)
                        ? new ImageResponse
                        {
                            Uri = p.PictureUri
                        }
                        : null,
                        })
                },
                Owner = job.Owner != null
                ? new Employee
                {
                    Id = job.Owner.Id,
                    FirstName = job.Owner.FirstName,
                    LastName = job.Owner.LastName
                }
                : null,
                Position = Position.FromDomainNotNullable(job.Position),
                YearExperience = Local.YearExperience.FromDomain(job.YearExperience),
                DeadLineDate = job.DeadlineDate,
                Description = job.Description,
                Stage = job.Stage,
                IsPublished = job.IsPublished,
                SharingDate = job.Sharing?.Date,
                StartDate = job.Terms?.Availability?.From,
                EndDate = job.Terms?.Availability?.To,
                Currency = job.Terms?.Currency,
                Freelance = job.Terms?.Freelance != null ? Local.Freelance.FromDomain(job.Terms.Freelance) : null,
                Permanent = job.Terms?.Permanent != null ? Local.Permanent.FromDomain(job.Terms.Permanent) : null,
                WeeklyHours = job.Terms?.PartTimeWorkingHours?.Weekly,
                IsPriority = job.IsPriority,
                WorkingHourTypes = GetWorkingHourTypes(job).ToList(),
                WorkTypes = GetWorkTypes(job),
                IsUrgent = job.Terms?.IsUrgent ?? false,
                AssignedEmployees = job.AssignedEmployees.Select(e => new Employee
                {
                    Id = e.Employee.Id,
                    FirstName = e.Employee.FirstName,
                    LastName = e.Employee.LastName,
                    PictureUri = e.Employee.PictureUri
                }),
                Skills = job.Skills.Select(s => new Skill { Id = s.SkillId, Code = s.Code }),
                Industries = job.Industries.Select(s => new Industry { Id = s.IndustryId, Code = s.Code }),
                Seniorities = job.SeniorityLevels.Select(s => s.Seniority),
                Languages = job.Languages.Select(Language.FromDomain),
                Formats = GetFormats(job),
                IsArchived = job.IsArchived,
                IsActivated = job.IsActivated,
                ParentJobId = job.ParentJobId,
                Location = job.Location,
                InterestedCandidates = job.InterestedCandidates.Select(InterestedCandidate.FromDomain),
                InterestedLinkedIns = job.InterestedLinkedIns.Select(x => x.Url),
                IsSelectionStarted = job.IsSelectionStarted
            };
        }

        private static IEnumerable<WorkingHoursType> GetWorkingHourTypes(Job job)
        {
            var hoursTypes = new List<WorkingHoursType>();

            if (job.Terms?.PartTimeWorkingHours is not null)
            {
                hoursTypes.Add(WorkingHoursType.PartTime);
            }

            if (job.Terms?.FullTimeWorkingHours is not null)
            {
                hoursTypes.Add(WorkingHoursType.FullTime);
            }

            if (job.Terms?.ProjectWorkingHours is not null)
            {
                hoursTypes.Add(WorkingHoursType.ProjectEmployment);
            }
            
            return hoursTypes;
        }

        private static List<FormatType> GetFormats(Job job)
        {
            var formats = new List<FormatType>();

            if (job.Terms?.Formats is null)
            {
                return formats;
            }

            if (job.Terms.Formats.IsRemote)
            {
                formats.Add(FormatType.Remote);
            }
            if (job.Terms.Formats.IsOnSite)
            {
                formats.Add(FormatType.Onsite);
            }
            if (job.Terms.Formats.IsHybrid)
            {
                formats.Add(FormatType.Hybrid);
            }

            return formats;
        }

        private static List<WorkType> GetWorkTypes(Job job)
        {
            var workTypes = new List<WorkType>();

            if (job.Terms?.Freelance?.WorkType != null)
            {
                workTypes.Add(WorkType.Freelance);
            }

            if (job.Terms?.Permanent?.WorkType != null)
            {
                workTypes.Add(WorkType.Permanent);
            }

            return workTypes;
        }
    }
}
