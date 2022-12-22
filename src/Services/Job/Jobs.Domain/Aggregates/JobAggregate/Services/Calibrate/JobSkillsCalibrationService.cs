using Jobs.Domain.Aggregates.JobAggregate.Entities;

namespace Jobs.Domain.Aggregates.JobAggregate.Services.Calibrate
{
    public static class JobSkillsCalibrationService
    {
        public static void Calibrate(this List<JobSkill> current, IEnumerable<JobSkill> request, Guid jobId)
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
                    current.Add(new JobSkill(
                        skill.SkillId, 
                        jobId, skill.Code,
                        skill.AliasTo?.Id, 
                        skill.AliasTo?.Code));
                }
            }
        }
    }
}
