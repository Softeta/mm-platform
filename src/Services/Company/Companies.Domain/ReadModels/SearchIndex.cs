namespace Companies.Domain.ReadModels
{
    public class SearchIndex
    {
        public Guid Id { get; set; }
        public string CountryCode { get; set; } = null!;
        public string RegistrationNumber { get; set; } = null!;
        public string Index { get; set; } = null!;
    }
}
