using Candidates.Domain.Aggregates.CandidateAggregate.Entities;

namespace Candidates.Domain.Aggregates.CandidateAggregate.Services
{
    public static class CandidateSkillsCalibrationService
    {
        public static void Calibrate(this List<CandidateSkill> current, IEnumerable<CandidateSkill> request, Guid candidateId)
        {
            var requestSkillIds = new HashSet<Guid>(request.Select(s => s.SkillId));
            var currentSkillsIds = new HashSet<Guid>(current.Select(s => s.SkillId));

            var equals = requestSkillIds.SetEquals(currentSkillsIds);

            if (!equals)
            {
                current.RemoveAll(s => !requestSkillIds.Contains(s.SkillId));

                var skillsToAdd = request.Where(s => !currentSkillsIds.Contains(s.SkillId));

                foreach (var skill in skillsToAdd)
                {
                    current.Add(new CandidateSkill(
                        skill.SkillId, 
                        candidateId,
                        skill.Code,
                        skill.AliasTo?.Id,
                        skill.AliasTo?.Code));
                }
            }
        }
    }
}
