using ElasticSearch.Sync.Events.Models.Shared;

namespace ElasticSearch.Sync.Indexes.Helpers
{
    internal static class GetCodeHelper
    {
        public static string GetSkillCode(Skill skill) =>
            string.IsNullOrWhiteSpace(skill.AliasTo?.Code) ? skill.Code : skill.AliasTo.Code;
    }
}
