using Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects;

namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Candidate
{
    public class Formats
    {
        public bool IsRemote { get; set; }
        public bool IsOnSite { get; set; }
        public bool IsHybrid { get; set; }

        public static Formats? FromDomain(JobFormats? formats)
        {
            if (formats is null) return null;

            return new Formats
            {
                IsRemote = formats.IsRemote,
                IsOnSite = formats.IsOnSite,
                IsHybrid = formats.IsHybrid
            };
        }
    }
}
