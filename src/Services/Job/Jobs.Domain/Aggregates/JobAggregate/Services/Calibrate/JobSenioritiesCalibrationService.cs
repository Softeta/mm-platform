using Domain.Seedwork.Enums;
using Jobs.Domain.Aggregates.JobAggregate.Entities;

namespace Jobs.Domain.Aggregates.JobAggregate.Services.Calibrate
{
    public static class JobSenioritiesCalibrationService
    {
        public static bool Calibrate(this List<JobSeniority> current, IEnumerable<JobSeniority> request, Guid jobId)
        {
            var requestSeniorities = new HashSet<SeniorityLevel>(request.Select(s => s.Seniority));
            var currentSeniorities = new HashSet<SeniorityLevel>(current.Select(s => s.Seniority));

            var equals = requestSeniorities.SetEquals(currentSeniorities);

            if (!equals)
            {
                current.RemoveAll(s => !requestSeniorities.Contains(s.Seniority));

                var senioritiesToAdd = requestSeniorities.Where(seniority => !currentSeniorities.Contains(seniority));

                foreach (var seniority in senioritiesToAdd)
                {
                    current.Add(new JobSeniority(jobId, seniority));
                }
                return true;
            }

            return false;
        }
    }
}
