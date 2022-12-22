namespace Companies.Domain.UnitTests.CompanyAggregate.DataSeed
{
    public class CompanyDataSeed
    {
        public string? PersonPhoneCountryCode { get; set; }
        public string? PersonPhoneNumber { get; set; }
        public string PersonEmail { get; set; } = null!;
        public string? WebsiteUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? GlassdoorUrl { get; set; }

        public CompanyDataSeed()
        {
            PersonPhoneCountryCode = "+370";
            PersonPhoneNumber = "60000";
            PersonEmail = "valid@email.com";
            WebsiteUrl = "https://websiteurl.com";
            LinkedInUrl = "https://linkedinurl.com/me";
            GlassdoorUrl = "https://glassdoorurl.com/me";
        }
    }
}
