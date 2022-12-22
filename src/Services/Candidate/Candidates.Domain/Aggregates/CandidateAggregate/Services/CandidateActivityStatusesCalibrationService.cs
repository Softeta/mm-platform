using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Domain.Seedwork.Enums;

namespace Candidates.Domain.Aggregates.CandidateAggregate.Services
{
    public static class CandidateActivityStatusesCalibrationService
    {
        public static void Calibrate(this List<CandidateActivityStatus> current, IEnumerable<ActivityStatus> request, Guid candidateId)
        {
            var requiredActivityStatuses = new HashSet<ActivityStatus>(request);
            var currentActivityStatuses = new HashSet<ActivityStatus>(current.Select(s => s.ActivityStatus));

            var equals = requiredActivityStatuses.SetEquals(currentActivityStatuses);

            if (!equals)
            {
                current.RemoveAll(s => !request.Contains(s.ActivityStatus));

                var desiredActivityStatusesToAdd = request.Where(s => !currentActivityStatuses.Contains(s));

                foreach (var activityStatus in desiredActivityStatusesToAdd)
                {
                    current.Add(new CandidateActivityStatus(candidateId, activityStatus));
                }
            }
        }
    }
}
