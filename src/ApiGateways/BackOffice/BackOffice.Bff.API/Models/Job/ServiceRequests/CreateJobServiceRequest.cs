using BackOffice.Bff.API.Models.Job.Requests;
using BackOffice.Bff.API.Models.Job.ServiceRequests.Models;
using BackOffice.Bff.API.Models.Users;
using Common = Contracts.Job.Jobs.Requests;
using Company = Contracts.Company.Responses;

namespace BackOffice.Bff.API.Models.Job.ServiceRequests
{
    public class CreateJobServiceRequest : Common.CreateJobRequest
    {
        public static Common.CreateJobRequest ToServiceRequest(
            CreateJobRequest request,
            BackOfficeUser owner,
            List<BackOfficeUser> assignedEmployees,
            Company.GetCompanyResponse company,
            IEnumerable<InterestedCandidateServiceRequest> interestedCandidates)
        {
            return new Common.CreateJobRequest
            {
                Company = CreateJobCompanyServiceRequest.ToServiceRequest(request.Company, company),
                Owner = Employee.ToServiceRequest(owner),
                Position = request.Position,
                DeadLineDate = request.DeadLineDate,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Currency = request.Currency,
                Freelance = request.Freelance,
                Permanent = request.Permanent,
                IsPriority = request.IsPriority,
                IsUrgent = request.IsUrgent,
                WorkTypes = request.WorkTypes,
                AssignedEmployees = assignedEmployees.Select(Employee.ToServiceRequest),
                Skills = request.Skills,
                Industries = request.Industries,
                Seniorities = request.Seniorities,
                Languages = request.Languages,
                Formats = request.Formats,
                WeeklyWorkHours = request.WeeklyWorkHours,
                WorkingHourTypes = request.WorkingHourTypes,
                InterestedLinkedIns = request.InterestedLinkedIns,
                InteresedCandidates = interestedCandidates
            };
        }
    }
}
