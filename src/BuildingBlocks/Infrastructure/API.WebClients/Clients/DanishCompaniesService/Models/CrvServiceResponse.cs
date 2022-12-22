using Newtonsoft.Json;

namespace API.WebClients.Clients.DanishCompaniesService.Models
{
    public class CrvServiceResponse
    {
        [JsonProperty("hits")]
        public Data Hits { get; set; } = null!;
    }

    public class Data
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("hits")]
        public Hit[] Hits { get; set; } = null!;
    }

    public class Hit
    {
        [JsonProperty("_source")]
        public Source Source { get; set; } = null!;

        [JsonProperty("sort")]
        public string[] Sort { get; set; } = null!;
    }

    public class Source
    {
        [JsonProperty("Vrvirksomhed")]
        public Company Company { get; set; } = null!;
    }

    public class Company
    {
        [JsonProperty("cvrNummer")]
        public int RegistrationNumber { get; set; }

        [JsonProperty("virksomhedMetadata")]
        public CompanyMetadata CompanyMetadata { get; set; } = null!;
    }

    public class CompanyMetadata
    {
        [JsonProperty("nyesteNavn")]
        public CompanyName? CompanyName { get; set; }

        [JsonProperty("nyesteBeliggenhedsadresse")]
        public Address? Address { get; set; }
    }

    public class CompanyName
    {
        [JsonProperty("navn")]
        public string Name { get; set; } = null!;
    }

    public class Address
    {
        [JsonProperty("landekode")]
        public string CountryCode { get; set; } = null!;


        [JsonProperty("kommune")]
        public Region Region { get; set; } = null!;


        [JsonProperty("bynavn")]
        public string City { get; set; } = null!;


        [JsonProperty("vejnavn")]
        public string StreetName { get; set; } = null!;


        [JsonProperty("husnummerFra")]
        public string HouseNumber { get; set; } = null!;


        [JsonProperty("sidedoer")]
        public string DoorNumber { get; set; } = null!;


        [JsonProperty("postnummer")]
        public string ZipCode { get; set; } = null!;
    }

    public class Region
    {
        [JsonProperty("kommuneNavn")]
        public string Name { get; set; } = null!;
    }
}
