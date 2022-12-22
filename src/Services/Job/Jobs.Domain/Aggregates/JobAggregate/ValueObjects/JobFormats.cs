using Domain.Seedwork;
using Domain.Seedwork.Enums;

namespace Jobs.Domain.Aggregates.JobAggregate.ValueObjects
{
    public class JobFormats : ValueObject<JobFormats>
    {
        public bool IsRemote { get; init; }
        public bool IsOnSite { get; init; }
        public bool IsHybrid { get; init; }

        private JobFormats() { }

        public JobFormats(IEnumerable<FormatType> formats)
        {
            IsRemote = formats.Any(x => x == FormatType.Remote);
            IsOnSite = formats.Any(x => x == FormatType.Onsite);
            IsHybrid = formats.Any(x => x == FormatType.Hybrid);
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return IsRemote;
            yield return IsOnSite;
            yield return IsHybrid;
        }
    }
}
