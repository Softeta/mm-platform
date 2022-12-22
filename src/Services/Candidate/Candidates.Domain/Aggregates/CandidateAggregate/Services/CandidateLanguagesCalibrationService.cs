using Candidates.Domain.Aggregates.CandidateAggregate.Entities;

namespace Candidates.Domain.Aggregates.CandidateAggregate.Services
{
    public static class CandidateLanguagesCalibrationService
    {
        public static void Calibrate(this List<CandidateLanguage> current, IEnumerable<CandidateLanguage> request, Guid candidateId)
        {
            var requestLanguageIds = new HashSet<Guid>(request.Select(l => l.Language.Id));
            var currentLanguageIds = new HashSet<Guid>(current.Select(l => l.Language.Id));

            var equals = requestLanguageIds.SetEquals(currentLanguageIds);

            if (!equals)
            {
                current.RemoveAll(l => !requestLanguageIds.Contains(l.Language.Id));

                var languagesToAdd = request.Where(l => !currentLanguageIds.Contains(l.Language.Id));

                foreach (var language in languagesToAdd)
                {
                    current.Add(new CandidateLanguage(
                        candidateId,
                        language.Language.Id,
                        language.Language.Code,
                        language.Language.Name)
                    );
                }
            }
        }
    }
}
