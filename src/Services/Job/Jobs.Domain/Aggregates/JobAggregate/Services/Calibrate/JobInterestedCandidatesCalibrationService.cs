using Jobs.Domain.Aggregates.JobAggregate.Entities;

namespace Jobs.Domain.Aggregates.JobAggregate.Services.Calibrate
{
    public static class JobInterestedCandidatesCalibrationService
    {
        public static void Calibrate(
            this List<JobInterestedCandidate> current,
            IEnumerable<JobInterestedCandidate> request,
            Guid jobId)
        {
            var requestCandidateIds = new HashSet<Guid>(request.Select(s => s.CandidateId));
            var currentCandidateIds = new HashSet<Guid>(current.Select(s => s.CandidateId));

            var equals = requestCandidateIds.SetEquals(currentCandidateIds);

            if (!equals)
            {
                current.RemoveAll(s => !requestCandidateIds.Contains(s.CandidateId));

                var candidatesToAdd = request.Where(s => !currentCandidateIds.Contains(s.CandidateId));

                foreach (var candidate in candidatesToAdd)
                {
                    current.Add(new JobInterestedCandidate(
                        jobId,
                        candidate.CandidateId,
                        candidate.FirstName,
                        candidate.LastName,
                        candidate.Position,
                        candidate.PictureUri));
                }
            }
        }
    }
}
