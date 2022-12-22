using Jobs.Domain.Aggregates.JobAggregate.ValueObjects;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Job
{
    public class Formats
    {
        public bool IsRemote { get; set; }
        public bool IsOnSite { get; set; }
        public bool IsHybrid { get; set; }

        public static Formats FromDomain(JobFormats formats)
        {
            return new Formats
            {
                IsRemote = formats.IsRemote,
                IsOnSite = formats.IsOnSite,
                IsHybrid = formats.IsHybrid
            };
        }
    }
}
