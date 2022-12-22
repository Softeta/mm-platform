using Domain.Seedwork;

namespace Candidates.Domain.Aggregates.CandidateTestsAggregate.ValueObjects
{
    public class Raport : ValueObject<Raport>
    {
        public string InstanceId { get; init; } = null!; // General PAPI3NSL-88dbb186-cc89-4e7c-88e4-b32f66945df4-PAPI~MarcherMarkholt-PAPI3NSL-General-Feedbac
        public string Url { get; init; } = null!;

        private Raport() { }

        public Raport(string instanceId, string url)
        {
            InstanceId = instanceId;
            Url = url;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return InstanceId;
            yield return Url;
        }
    }
}
