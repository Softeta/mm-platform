using DomainEntity = Jobs.Domain.Aggregates.JobAggregate.Entities;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Job
{
    public class AssignedEmployee
    {
        public Employee? Employee { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public static AssignedEmployee FromDomain(DomainEntity.JobAssignedEmployee jobAssignedEmployee)
        {
            return new AssignedEmployee
            {
                Employee = Employee.FromDomain(jobAssignedEmployee.Employee),
                CreatedAt = jobAssignedEmployee.CreatedAt
            };
        }
    }
}
