using Domain.Seedwork.Enums;
using Jobs.Domain.Aggregates.JobAggregate.Entities;

namespace Jobs.Domain.Aggregates.JobAggregate.Services.Calibrate
{
    public static class JobWorkingHoursCalibrationService
    {
        public static void Calibrate(this List<JobWorkingHours> current, IEnumerable<JobWorkingHours> request, Guid jobId)
        {
            var requestWorkingHourTypes = new HashSet<WorkingHoursType>(request.Select(w => w.WorkingHoursType));
            var currentWorkingHourTypes = new HashSet<WorkingHoursType>(current.Select(w => w.WorkingHoursType));

            var equals = requestWorkingHourTypes.Equals(currentWorkingHourTypes);

            if (!equals)
            {
                current.RemoveAll(w => !requestWorkingHourTypes.Contains(w.WorkingHoursType));

                var workingHourTypesToAdd = requestWorkingHourTypes.Where(workingHoursType => !currentWorkingHourTypes.Contains(workingHoursType));

                foreach (var workingHourType in workingHourTypesToAdd)
                {
                    current.Add(new JobWorkingHours(jobId, workingHourType));
                }
            }
        }
    }
}
