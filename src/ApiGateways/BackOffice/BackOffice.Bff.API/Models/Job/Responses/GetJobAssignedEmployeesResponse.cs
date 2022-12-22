using Contracts.Job;

namespace BackOffice.Bff.API.Models.Job.Responses
{
    public class GetJobAssignedEmployeesResponse
    {
        public IEnumerable<Employee> AssignedEmployees { get; set; } = new List<Employee>();
    }
}
