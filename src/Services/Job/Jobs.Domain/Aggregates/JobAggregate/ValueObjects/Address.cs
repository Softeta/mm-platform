using Domain.Seedwork;
using Domain.Seedwork.Shared.ValueObjects;

namespace Jobs.Domain.Aggregates.JobAggregate.ValueObjects
{
    public class Address : ValueObject<Address>
    {
        public string AddressLine { get; init; } = null!;
        public string? City { get; init; }
        public string? Country { get; init; }
        public string? PostalCode { get; init; }
        public Coordinates? Coordinates { get; init; }

        private Address() { }

        public Address(
            string addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude)
        {
            AddressLine = addressLine;
            City = city;
            Country = country;
            PostalCode = postalCode;

            if (longitude.HasValue && latitude.HasValue)
            {
                Coordinates = new Coordinates(longitude.Value, latitude.Value);
            }
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return AddressLine;
            yield return City;
            yield return Country;
            yield return PostalCode;
            yield return Coordinates;
        }
    }
}
