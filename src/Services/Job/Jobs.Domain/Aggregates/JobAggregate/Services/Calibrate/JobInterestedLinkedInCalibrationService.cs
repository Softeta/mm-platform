using Jobs.Domain.Aggregates.JobAggregate.Entities;

namespace Jobs.Domain.Aggregates.JobAggregate.Services.Calibrate
{
    public static class JobInterestedLinkedInCalibrationService
    {
        public static void Calibrate(
           this List<JobInterestedLinkedIn> current,
           IEnumerable<string> request,
           Guid jobId)
        {
            var requestLinkedIns = new HashSet<string>(request);
            var currentLinkedIns = new HashSet<string>(current.Select(s => s.Url));

            var equals = requestLinkedIns.SetEquals(currentLinkedIns);

            if (!equals)
            {
                current.RemoveAll(s => !requestLinkedIns.Contains(s.Url));

                var linkedInsToAdd = request.Where(s => !currentLinkedIns.Contains(s));

                foreach (var linkedInToAdd in linkedInsToAdd)
                {
                    current.Add(new JobInterestedLinkedIn(jobId, linkedInToAdd));
                }
            }
        }
    }
}
