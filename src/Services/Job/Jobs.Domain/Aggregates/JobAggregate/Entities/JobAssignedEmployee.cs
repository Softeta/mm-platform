using Domain.Seedwork;
using Jobs.Domain.Aggregates.JobAggregate.ValueObjects;

namespace Jobs.Domain.Aggregates.JobAggregate.Entities
{
    public class JobAssignedEmployee : Entity
    {
        public Guid JobId { get; private set; }
        public Employee Employee { get; private set; } = null!;

        private JobAssignedEmployee() { }

        public JobAssignedEmployee(
            Guid jobId, 
            Guid employeeId, 
            string employeeFirstName,
            string employeeLastName,
            string? employeePictureUri)
        {
            Id = Guid.NewGuid();
            JobId = jobId;
            Employee = new Employee(
                employeeId,
                employeeFirstName,
                employeeLastName,
                employeePictureUri);
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public void Update(string firstName, string lastName, string? pictureUri)
        {
            Employee = new Employee(Employee.Id, firstName, lastName, pictureUri);
        }
    }
}
