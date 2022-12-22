using System;

namespace Candidates.Domain.UnitTests.CandidateAggregate.DataSeed
{
    public class CandidateDataSeed
    {
        public string? Email { get; set; }
        public string? PhoneCountryCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? PersonalWebsiteUrl { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }

        public CandidateDataSeed()
        {
            Email = "valid@email.com";
            PhoneCountryCode = "+11";
            PhoneNumber = "45856";
            LinkedInUrl = "https://linkedin.com/me";
            PersonalWebsiteUrl = "https://mypersonalwebsite.com";
            BirthDate = DateTimeOffset.Parse("2000-01-01");
            StartDate = DateTimeOffset.Parse("2045-01-01");
            EndDate = DateTimeOffset.Parse("2045-01-02");
        }

        public void SetInvalidBirthdate(int addDays)
        {
            BirthDate = DateTimeOffset.UtcNow.Date.AddDays(addDays);
        }

        public void SetInvalidStartDate(int minusDays)
        {
            StartDate = DateTimeOffset.UtcNow.Date.AddDays(-minusDays);
        }

        public void RemoveStartDate()
        {
            StartDate = null;
        }
    }
}
