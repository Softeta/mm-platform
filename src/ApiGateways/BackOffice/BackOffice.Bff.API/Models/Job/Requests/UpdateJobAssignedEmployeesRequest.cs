namespace BackOffice.Bff.API.Models.Job.Requests
{
    public class UpdateJobAssignedEmployeesRequest
    {
        public ICollection<Guid> AssignedEmployees { get; set; } = new List<Guid>();
    }
}
