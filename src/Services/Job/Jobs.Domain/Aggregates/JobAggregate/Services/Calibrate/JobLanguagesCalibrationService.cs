using Jobs.Domain.Aggregates.JobAggregate.Entities;

namespace Jobs.Domain.Aggregates.JobAggregate.Services.Calibrate
{
    public static class JobLanguagesCalibrationService
    {
        public static void Calibrate(this List<JobLanguage> current, IEnumerable<JobLanguage> request, Guid jobId)
        {
            var requestLanguageIds = new HashSet<Guid>(request.Select(l => l.Language.Id));
            var currentLanguageIds = new HashSet<Guid>(current.Select(l => l.Language.Id));

            var equals = requestLanguageIds.SetEquals(currentLanguageIds);

            if (!equals)
            {
                current.RemoveAll(l => !requestLanguageIds.Contains(l.Language.Id));

                var jobLanguagesToAdd = request.Where(l => !currentLanguageIds.Contains(l.Language.Id));

                foreach (var jobLanguage in jobLanguagesToAdd)
                {
                    current.Add(new JobLanguage(
                        jobId,
                        jobLanguage.Language.Id,
                        jobLanguage.Language.Code,
                        jobLanguage.Language.Name)
                    );
                }
            }
        }
    }
}
