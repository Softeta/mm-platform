using System;

namespace Jobs.Domain.UnitTests.JobAggregate.DataSeed
{
    internal class JobDataSeed
    {
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public DateTimeOffset? DeadlineDate { get; set; }

        public JobDataSeed()
        {
            StartDate = DateTimeOffset.Parse("2045-01-01");
            EndDate = DateTimeOffset.Parse("2045-01-02");
            DeadlineDate = DateTime.Parse("2045-01-01");
        }

        public void SetInvalidDeadlineDate(int minusDays)
        {
            DeadlineDate = DateTimeOffset.UtcNow.Date.AddDays(-minusDays);
        }

        public void SetInvalidStartDate(int minusDays)
        {
            StartDate = DateTimeOffset.UtcNow.Date.AddDays(-minusDays);
        }
    }
}
