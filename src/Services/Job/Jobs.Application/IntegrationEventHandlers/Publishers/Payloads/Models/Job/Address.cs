using DomainValueObject = Jobs.Domain.Aggregates.JobAggregate.ValueObjects;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Job
{
    public class Address
    {
        public string AddressLine { get; set; } = null!;
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }

        public static Address? FromAddress(DomainValueObject.Address? address)
        {
            if (address is null) return null;

            return new Address
            {
                AddressLine = address.AddressLine,
                City = address.City,
                Country = address.Country,
                PostalCode = address.PostalCode,
                Longitude = address.Coordinates?.Longitude,
                Latitude = address.Coordinates?.Latitude
            };
        }
    }
}
