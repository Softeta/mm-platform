using BackOffice.Bff.API.Models.Job.Requests;
using BackOffice.Bff.API.Models.Job.ServiceRequests.Models;
using BackOffice.Bff.API.Models.Users;
using Contracts.Job.Jobs.Responses;
using Domain.Seedwork.Enums;
using Common = Contracts.Job.Jobs.Requests;

namespace BackOffice.Bff.API.Models.Job.ServiceRequests
{
    public class UpdateJobServiceRequest : Common.UpdateJobRequest
    {
        public static UpdateJobServiceRequest ToServiceRequest(
            UpdateJobRequest request,
            BackOfficeUser owner,
            List<BackOfficeUser> assignedEmployees,
            IEnumerable<InterestedCandidateServiceRequest> interestedCandidates)
        {
            return new UpdateJobServiceRequest
            {
                Owner = Employee.ToServiceRequest(owner),
                Position = request.Position,
                DeadLineDate = request.DeadLineDate,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Currency = request.Currency,
                WeeklyWorkHours = request.WeeklyWorkHours,
                Freelance = request.Freelance,
                Permanent = request.Permanent,
                YearExperience = request.YearExperience,
                IsPriority = request.IsPriority,
                IsUrgent = request.IsUrgent,
                WorkingHourTypes = request.WorkingHourTypes,
                WorkTypes = request.WorkTypes,
                AssignedEmployees = assignedEmployees.Select(Employee.ToServiceRequest),
                Skills = request.Skills,
                Industries = request.Industries,
                Seniorities = request.Seniorities,
                Languages = request.Languages,
                Formats = request.Formats,
                InterestedLinkedIns = request.InterestedLinkedIns,
                InteresedCandidates = interestedCandidates
            };
        }

        public static UpdateJobServiceRequest ToServiceRequest(
            GetJobResponse request,
            List<BackOfficeUser> assignedEmployees)
        {
            return new UpdateJobServiceRequest
            {
                Owner = request.Owner!,
                Position = request.Position,
                DeadLineDate = request.DeadLineDate,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Currency = request.Currency,
                WeeklyWorkHours = request.WeeklyHours,
                Freelance = request.Freelance,
                Permanent = request.Permanent,
                YearExperience = request.YearExperience,
                IsPriority = request.IsPriority,
                IsUrgent = request.IsUrgent,
                WorkingHourTypes = request.WorkingHourTypes,
                WorkTypes = request.WorkTypes,
                AssignedEmployees = assignedEmployees.Select(Employee.ToServiceRequest),
                Skills = request.Skills,
                Industries = request.Industries,
                Seniorities = request.Seniorities,
                Languages = request.Languages,
                Formats = request.Formats,
                InterestedLinkedIns = request.InterestedLinkedIns,
                InteresedCandidates = request.InterestedCandidates
            };
        }
    }
}
