using Domain.Seedwork.Shared.ValueObjects;

namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Candidate
{
    internal class LivingAddress
    {
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public string? Location { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }

        public static LivingAddress? FromAddress(Address? address)
        {
            if (address is null) return null;

            return new LivingAddress
            {
                AddressLine = address.AddressLine,
                City = address.City,
                Country = address.Country,
                PostalCode = address.PostalCode,
                Location = address.Location,
                Longitude = address.Coordinates?.Longitude,
                Latitude = address.Coordinates?.Latitude,
            };
        }
    }
}
