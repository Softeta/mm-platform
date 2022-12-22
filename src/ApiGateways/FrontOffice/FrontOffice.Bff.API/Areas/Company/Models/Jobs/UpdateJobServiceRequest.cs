using Domain.Seedwork.Enums;
using FrontOffice.Bff.API.Constants;
using FrontOffice.Bff.API.Infrastructure;
using Common = Contracts.Job.Jobs;

namespace FrontOffice.Bff.API.Areas.Company.Models.Jobs
{
    public class UpdateJobServiceRequest : Common.Requests.UpdateJobRequest
    {
        public static Common.Requests.UpdateJobRequest ToServiceRequest(Common.Responses.GetJobResponse job)
        {
            return new Common.Requests.UpdateJobRequest
            {
                Owner = job.Owner,
                Position = job.Position,
                DeadLineDate = job.DeadLineDate,
                Description = job.Description,
                StartDate = job.StartDate,
                EndDate = job.EndDate,
                Currency = job.Currency,
                WeeklyWorkHours = job.WeeklyHours,
                Freelance = job.Freelance,
                Permanent = job.Permanent,
                YearExperience = job.YearExperience,
                IsPriority = job.IsPriority,
                IsUrgent = job.IsUrgent,
                WorkingHourTypes = job.WorkingHourTypes,
                WorkTypes = job.WorkTypes,
                AssignedEmployees = job.AssignedEmployees,
                Skills = job.Skills,
                Industries = job.Industries,
                Seniorities = job.Seniorities,
                Languages = job.Languages,
                Formats = job.Formats,
                InteresedCandidates = job.InterestedCandidates,
                InterestedLinkedIns = job.InterestedLinkedIns
            };
        }

        public static Common.Requests.UpdateJobRequest ToServiceRequestStep4(
            Common.Responses.GetJobResponse job, 
            Step4Request request)
        {
            var payload = ToServiceRequest(job);
            payload.Description = request.Description;

            return payload;
        }

        public static Common.Requests.UpdateJobRequest ToServiceRequestStep5(
            Common.Responses.GetJobResponse job,
            Step5Request request)
        {
            var payload = ToServiceRequest(job);
            payload.Skills = request.Skills;
            payload.Industries = request.Industries;
            payload.Languages = request.Languages;
            payload.Seniorities = request.Seniorities;

            return payload;
        }

        public static Common.Requests.UpdateJobRequest ToServiceRequestStep6Freelance(
           Common.Responses.GetJobResponse job,
           Step6FreelanceRequest request,
           CountrySettings countrySettings)
        {
            var payload = ToServiceRequest(job);

            if (payload.Freelance is null)
            {
                payload.Freelance = new Contracts.Job.Freelance();
            }

            payload.WorkingHourTypes = new List<WorkingHoursType>();
            payload.Freelance.MonthlyBudget = request.HourlyBudget;
            payload.Freelance.MonthlyBudget = request.MonthlyBudget;
            payload.Currency = request.Currency;
            payload.Formats = request.Formats;

            if (request.CompanyWorkingHourTypes.Any(x => x == CompanyWorkingHoursType.ProjectEmployment))
            {
                payload.Freelance.HoursPerProject = request.HoursPerProject;
                payload.WorkingHourTypes.Add(WorkingHoursType.ProjectEmployment);
            }

            if (request.CompanyWorkingHourTypes.Any(x => x == CompanyWorkingHoursType.Continuous))
            {
                payload.WeeklyWorkHours = request.WeeklyWorkHours;

                var countryWeeklyFullTimeHours = CountryFulltimeHours(countrySettings, job.Company.Address?.Country);

                if (countryWeeklyFullTimeHours <= request.WeeklyWorkHours)
                {
                    payload.WorkingHourTypes.Add(WorkingHoursType.FullTime);
                }
                else
                {
                    payload.WorkingHourTypes.Add(WorkingHoursType.PartTime);
                }
            }

            return payload;
        }

        public static Common.Requests.UpdateJobRequest ToServiceRequestStep6Permanent(
           Common.Responses.GetJobResponse job,
           Step6PermanentRequest request,
           CountrySettings countrySettings)
        {
            var payload = ToServiceRequest(job);

            if (payload.Permanent is null)
            {
                payload.Permanent = new Contracts.Job.Permanent();
            }

            payload.WorkingHourTypes = new List<WorkingHoursType>();
            payload.Permanent.MonthlyBudget = request.HourlyBudget;
            payload.Permanent.MonthlyBudget = request.MonthlyBudget;
            payload.Currency = request.Currency;
            payload.Formats = request.Formats;

            var countryWeeklyFullTimeHours = CountryFulltimeHours(countrySettings, job.Company.Address?.Country);

            if (countryWeeklyFullTimeHours <= request.WeeklyWorkHours)
            {
                payload.WorkingHourTypes.Add(WorkingHoursType.FullTime);
            }
            else
            {
                payload.WorkingHourTypes.Add(WorkingHoursType.PartTime);
            }

            return payload;
        }

        public static Common.Requests.UpdateJobRequest ToServiceRequestStep6(
           Common.Responses.GetJobResponse job,
           Step6Request request,
           CountrySettings countrySettings)
        {
            var payload = ToServiceRequest(job);

            payload.WorkingHourTypes = new List<WorkingHoursType>();
            payload.Currency = request.Currency;
            payload.Formats = request.Formats;
            payload.WeeklyWorkHours = request.WeeklyWorkHours;

            SetFreelancePart(payload, job, request, countrySettings);
            SetPermanentPart(payload, job, request, countrySettings);

            payload.WorkingHourTypes = payload.WorkingHourTypes
                .Distinct()
                .ToList();

            return payload;
        }

        private static decimal CountryFulltimeHours(CountrySettings countrySettings, string? country)
        {
            if (string.IsNullOrEmpty(country))
            {
                return CountryDefaults.FulltimeWorkingHours;
            }

            if (countrySettings.WeeklyFullTimeHours.TryGetValue(country, out var hours))
            {
                return hours;
            }

            return CountryDefaults.FulltimeWorkingHours;
        }

        private static void SetFreelancePart(
            Common.Requests.UpdateJobRequest payload,
            Common.Responses.GetJobResponse job,
            Step6Request request,
            CountrySettings countrySettings)
        {
            if (payload.Freelance is null)
            {
                payload.Freelance = new Contracts.Job.Freelance();
            }

            payload.Freelance.HourlyBudget = request.Freelance.HourlyBudget;
            payload.Freelance.MonthlyBudget = request.Freelance.MonthlyBudget;

            if (request.Freelance.CompanyWorkingHourTypes.Any(x => x == CompanyWorkingHoursType.ProjectEmployment))
            {
                payload.Freelance.HoursPerProject = request.Freelance.HoursPerProject;
                payload.WorkingHourTypes.Add(WorkingHoursType.ProjectEmployment);
            }

            if (request.Freelance.CompanyWorkingHourTypes.Any(x => x == CompanyWorkingHoursType.Continuous))
            {
                var countryWeeklyFullTimeHours = CountryFulltimeHours(countrySettings, job.Company.Address?.Country);

                if (countryWeeklyFullTimeHours <= request.WeeklyWorkHours)
                {
                    payload.WorkingHourTypes.Add(WorkingHoursType.FullTime);
                }
                else
                {
                    payload.WorkingHourTypes.Add(WorkingHoursType.PartTime);
                }
            }
        }

        private static void SetPermanentPart(
            Common.Requests.UpdateJobRequest payload,
            Common.Responses.GetJobResponse job,
            Step6Request request,
            CountrySettings countrySettings)
        {
            if (payload.Permanent is null)
            {
                payload.Permanent = new Contracts.Job.Permanent();
            }

            payload.Permanent.MonthlyBudget = request.Permanent.MonthlyBudget;

            var countryWeeklyFullTimeHours = CountryFulltimeHours(countrySettings, job.Company.Address?.Country);

            if (countryWeeklyFullTimeHours <= request.WeeklyWorkHours)
            {
                payload.WorkingHourTypes.Add(WorkingHoursType.FullTime);
            }
            else
            {
                payload.WorkingHourTypes.Add(WorkingHoursType.PartTime);
            } 
        }
    }
}