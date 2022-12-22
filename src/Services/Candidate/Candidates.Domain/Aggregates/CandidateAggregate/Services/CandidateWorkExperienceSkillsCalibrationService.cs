using Candidates.Domain.Aggregates.CandidateAggregate.Entities;

namespace Candidates.Domain.Aggregates.CandidateAggregate.Services
{
    public static class CandidateWorkExperienceSkillsCalibrationService
    {
        public static void Calibrate(this List<CandidateWorkExperienceSkill> current, IEnumerable<CandidateWorkExperienceSkill> request, Guid workExperienceId)
        {
            var requestWorkExperienceSkillIds = new HashSet<Guid>(request.Select(s => s.SkillId));
            var currentWorkExperienceSkillsIds = new HashSet<Guid>(current.Select(s => s.SkillId));

            var equals = requestWorkExperienceSkillIds.SetEquals(currentWorkExperienceSkillsIds);

            if (!equals)
            {
                current.RemoveAll(s => !requestWorkExperienceSkillIds.Contains(s.SkillId));

                var workExperienceSkillsToAdd = request.Where(s => !currentWorkExperienceSkillsIds.Contains(s.SkillId));

                foreach (var skill in workExperienceSkillsToAdd)
                {
                    current.Add(new CandidateWorkExperienceSkill(
                        skill.SkillId,
                        workExperienceId,
                        skill.Code, 
                        skill.AliasTo?.Id,
                        skill.AliasTo?.Code));
                }
            }
        }
    }
}
