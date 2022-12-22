using Candidates.Domain.Aggregates.CandidateAggregate.Entities;

namespace Candidates.Domain.Aggregates.CandidateAggregate.Services
{
    public static class CandidateIndustriesCalibrationService
    {
        public static void Calibrate(this List<CandidateIndustry> current, IEnumerable<CandidateIndustry> request, Guid candidateId)
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
                    current.Add(new CandidateIndustry(industry.IndustryId, candidateId, industry.Code));
                }
            }
        }
    }
}
