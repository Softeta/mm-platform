namespace AdministrationSettings.API.Models.Locations
{
    public class LocationResponse
    {
        public string AddressLine { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string City { get; set; } = null!;

        public string? PostalCode { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
