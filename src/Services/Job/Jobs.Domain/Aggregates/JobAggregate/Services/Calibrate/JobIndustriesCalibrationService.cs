using Jobs.Domain.Aggregates.JobAggregate.Entities;

namespace Jobs.Domain.Aggregates.JobAggregate.Services.Calibrate
{
    public static class JobIndustriesCalibrationService
    {
        public static void Calibrate(this List<JobIndustry> current, IEnumerable<JobIndustry> request, Guid jobId)
        {
            var requestIndustryIds = new HashSet<Guid>(request.Select(s => s.IndustryId));
            var currentIndustryIds = new HashSet<Guid>(current.Select(s => s.IndustryId));

            var equals = requestIndustryIds.SetEquals(currentIndustryIds);

            if (!equals)
            {
                current.RemoveAll(s => !requestIndustryIds.Contains(s.IndustryId));

                var industriesToAdd = request.Where(s => !currentIndustryIds.Contains(s.IndustryId));

                foreach (var industry in industriesToAdd)
                {
                    current.Add(new JobIndustry(industry.IndustryId, jobId, industry.Code));
                }
            }
        }
    }
}
