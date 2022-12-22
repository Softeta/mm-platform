namespace Companies.Domain.ReadModels
{
    public class RegistryCenterCompany
    {
        public string RegistrationNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
        public string AddressLine { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string? Region { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public ICollection<SearchIndex> SearchIndexes { get; set; } = new List<SearchIndex>();

        public void BuildIndexes()
        {
            var words = Name.Split(' ', StringSplitOptions.TrimEntries);

            if (words is null || words.Length == 0) return;

            SearchIndexes = words.Select(word => new SearchIndex
            {
                Id = Guid.NewGuid(),
                RegistrationNumber = RegistrationNumber,
                CountryCode = CountryCode,
                Index = word
            }).ToList();
        }
    }
}
