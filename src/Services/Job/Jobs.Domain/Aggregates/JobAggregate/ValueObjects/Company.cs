using Domain.Seedwork;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Jobs.Domain.Aggregates.JobAggregate.Entities;

namespace Jobs.Domain.Aggregates.JobAggregate.ValueObjects
{
    public class Company : ValueObject<Company>
    {
        public Guid Id { get; init; }

        public CompanyStatus Status { get; init; }

        public string Name { get; init; } = null!;

        public string? Description { get; init; }

        public string? LogoUri { get; init; }

        public Address? Address { get; init; }

        public IReadOnlyList<JobContactPerson> ContactPersons => _contactPersons;
        private readonly List<JobContactPerson> _contactPersons = new();

        private Company() { }

        public Company(
            Guid companyId,
            CompanyStatus status,
            string name,
            string? addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            string? description,
            string? logoUri,
            IEnumerable<JobContactPerson> contactPersons
        )
        {
            Id = companyId;
            Status = status;
            Name = name;
            Description = description;
            LogoUri = logoUri;

            if (!string.IsNullOrWhiteSpace(addressLine))
            {
                Address = new Address(
                    addressLine,
                    city,
                    country,
                    postalCode,
                    longitude, 
                    latitude);
            }

            _contactPersons.AddRange(contactPersons);

            Validate();
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Id;
            yield return Name;
            yield return Description;
            yield return LogoUri;
        }

        private void Validate()
        {
            if (Id == default)
            {
                throw new DomainException($"Invalid company id: {Id}",
                    ErrorCodes.Job.Company.InvalidId);
            }

            if (ContactPersons.Count(x => x.IsMainContact) != 1)
            {
                throw new DomainException($"Company must have 1 main contact person",
                    ErrorCodes.Job.Company.OneMainContactPersonMandatory);
            }
        }
    }
}
