using Newtonsoft.Json;

namespace API.WebClients.Clients.HereSearch.Models
{
    internal class HereSearchGeoCodeItemsResponse
    {
        public ICollection<HereSearchGeoCode> Items { get; set; } = new List<HereSearchGeoCode>();
    }

    internal class HereSearchGeoCode
    {
        [JsonProperty("title")]
        public string Title { get; set; } = null!;

        [JsonProperty("address")]
        public AddressDetails Address { get; set; } = null!;

        [JsonProperty("position")]
        public PositionDetails Position { get; set; } = null!;

        [JsonProperty("scoring")]
        public ScoringDetails Scoring { get; set; } = null!;

        internal class AddressDetails
        {
            [JsonProperty("label")]
            public string Label { get; set; } = null!;

            [JsonProperty("countryCode")]
            public string CountryCode { get; set; } = null!;

            [JsonProperty("countryName")]
            public string CountryName { get; set; } = null!;

            [JsonProperty("city")]
            public string? City { get; set; }

            [JsonProperty("postalCode")]
            public string? PostalCode { get; set; }
        }

        internal class PositionDetails
        {
            [JsonProperty("lat")]
            public double Latitude { get; set; }

            [JsonProperty("lng")]
            public double Longitude { get; set; }
        }

        internal class ScoringDetails
        {
            [JsonProperty("queryScore")]
            public double QueryScore { get; set; }
        }
    }
}
