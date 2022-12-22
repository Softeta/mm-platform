using Candidates.Domain.Aggregates.CandidateAggregate.Entities;

namespace Candidates.Domain.Aggregates.CandidateAggregate.Services
{
    public static class CandidateDesiredSkillsCalibrationService
    {
        public static void Calibrate(this List<CandidateDesiredSkill> current, IEnumerable<CandidateDesiredSkill> request, Guid candidateId)
        {
            var requestDesiredSkillIds = new HashSet<Guid>(request.Select(s => s.SkillId));
            var currentDesiredSkillsIds = new HashSet<Guid>(current.Select(s => s.SkillId));

            var equals = requestDesiredSkillIds.SetEquals(currentDesiredSkillsIds);

            if (!equals)
            {
                current.RemoveAll(s => !requestDesiredSkillIds.Contains(s.SkillId));

                var desiredSkillsToAdd = request.Where(s => !currentDesiredSkillsIds.Contains(s.SkillId));

                foreach (var skill in desiredSkillsToAdd)
                {
                    current.Add(new CandidateDesiredSkill(
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
