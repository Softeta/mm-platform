using Domain.Seedwork;

namespace Jobs.Domain.Aggregates.JobAggregate.ValueObjects
{
    public class Employee : ValueObject<Employee>
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string? PictureUri { get; init; }

        public Employee(Guid id, string firstName, string lastName, string? pictureUri)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PictureUri = pictureUri;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Id;
            yield return FirstName;
            yield return LastName;
            yield return PictureUri;
        }
    }
}
