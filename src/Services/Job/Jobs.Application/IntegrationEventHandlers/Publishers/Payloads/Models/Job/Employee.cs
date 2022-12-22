using DomainValueObject = Jobs.Domain.Aggregates.JobAggregate.ValueObjects;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Job
{
    public class Employee
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? PictureUri { get; set; }

        public static Employee? FromDomain(DomainValueObject.Employee? employee)
        {
            if (employee == null) return null;

            return new Employee
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PictureUri = employee.PictureUri
            };
        }
    }
}
