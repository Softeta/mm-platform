using ValueObjects = Domain.Seedwork.Shared.ValueObjects;

namespace Contracts.Shared
{
    public class AddressWithLocation : Address
    {
        public string? Location { get; set; }

        public new static AddressWithLocation? FromDomain(ValueObjects.Address? address)
        {
            if (address is null) return null;

            return new AddressWithLocation
            {
                AddressLine = address.AddressLine,
                Location = address.Location,
                City = address.City,
                Country = address.Country,
                PostalCode = address.PostalCode,
                Longitude = address.Coordinates?.Longitude,
                Latitude = address.Coordinates?.Latitude,
            };
        }
    }
}
