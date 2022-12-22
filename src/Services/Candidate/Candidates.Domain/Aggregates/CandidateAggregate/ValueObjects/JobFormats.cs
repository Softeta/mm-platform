using Domain.Seedwork;
using Domain.Seedwork.Enums;

namespace Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects
{
    public class JobFormats : ValueObject<JobFormats>
    {
        public bool IsRemote { get; init; }
        public bool IsOnSite { get; init; }
        public bool IsHybrid { get; init; }

        private JobFormats() { }

        public JobFormats(ICollection<FormatType> formats)
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
