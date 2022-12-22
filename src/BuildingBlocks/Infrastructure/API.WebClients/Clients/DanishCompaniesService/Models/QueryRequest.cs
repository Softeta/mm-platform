using Newtonsoft.Json;

namespace API.WebClients.Clients.DanishCompaniesService.Models
{
    public class QueryRequest
    {
        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("search_after", NullValueHandling = NullValueHandling.Ignore)]
        public string[]? SearchAfter { get; set; }

        [JsonProperty("sort")]
        public Sort Sort { get; set; } = null!;

        [JsonProperty("query")]
        public Query Query { get; set; } = null!;

        [JsonProperty("_source")]
        public string[] Source { get; set; } = null!;
    }

    public class Sort
    {
        [JsonProperty("Vrvirksomhed.cvrNummer")]
        public string Field { get; set; } = null!;
    }

    public class Query
    {
        [JsonProperty("bool")]
        public MustBool Bool { get; set; } = null!;
    }

    public class MustBool
    {
        [JsonProperty("must")]
        public Must[] Must { get; set; } = null!;
    }

    public class Must
    {
        [JsonProperty("bool")]
        public ShouldBool Bool { get; set; } = null!;
    }

    public class ShouldBool
    {
        [JsonProperty("should")]
        public Should[] Should { get; set; } = null!;
    }

    public class Should
    {
        [JsonProperty("match")]
        public virtual Match Match { get; set; } = null!;
    }

    public class PhraseMatchPrefixShould : Should
    {
        [JsonProperty("match_phrase_prefix")]
        public override Match Match { get; set; } = null!;
    }

    public abstract class Match
    {
        public virtual dynamic Value { get; set; } = null!;
    }

    public class CompanyTypeMatch : Match
    {
        [JsonProperty(Fields.CompanyType)]
        public override dynamic Value { get; set; } = null!;
    }

    public class CountryCodeMatch : Match
    {
        [JsonProperty(Fields.CountryCode)]
        public override dynamic Value { get; set; } = null!;
    }

    public class CvrNumberMatch : Match
    {
        [JsonProperty(Fields.RegistrationNumber)]
        public override dynamic Value { get; set; } = null!;
    }

    public class NamePhrasePrefixMatch : Match
    {
        [JsonProperty(Fields.LatestName)]
        public override dynamic Value { get; set; } = null!;
    }

    public class MatchQuery
    {
        [JsonProperty("query")]
        public string Query { get; set; } = null!;
    }

    public static class Fields
    {
        public const string CompanyType = "Vrvirksomhed.virksomhedsform.kortBeskrivelse";
        public const string LatestName = "Vrvirksomhed.virksomhedMetadata.nyesteNavn.navn";
        public const string RegistrationNumber = "Vrvirksomhed.cvrNummer";
        public const string Region = "Vrvirksomhed.virksomhedMetadata.nyesteBeliggenhedsadresse.kommune.kommuneNavn";
        public const string Street = "Vrvirksomhed.virksomhedMetadata.nyesteBeliggenhedsadresse.vejnavn";
        public const string HouseNumber = "Vrvirksomhed.virksomhedMetadata.nyesteBeliggenhedsadresse.husnummerFra";
        public const string ZipCode = "Vrvirksomhed.virksomhedMetadata.nyesteBeliggenhedsadresse.postnummer";
        public const string CountryCode = "Vrvirksomhed.virksomhedMetadata.nyesteBeliggenhedsadresse.landekode";
        public const string City = "Vrvirksomhed.virksomhedMetadata.nyesteBeliggenhedsadresse.bynavn";
        public const string DoorNumber = "Vrvirksomhed.virksomhedMetadata.nyesteBeliggenhedsadresse.sidedoer";
    }

    public static class CompanyTypes
    {
        public const string IVS = "IVS";
        public const string ApS = "ApS";
        public const string AS = @"A/S";
    }

    public static class CountryCodes
    {
        public const string DK = "DK";
    }

    public static class SortType
    {
        public const string Asc = "asc";
    }
}
