namespace API.WebClients.Constants;

public static class Endpoints
{
    public static class TagSystem
    {
        public const string JobPositionsSimilar = "api/v1/job-positions/similar/?pageSize={0}&search={1}";
        public const string SkillsSimilar = "api/v1/skills/similar/?pageSize={0}&search={1}";
    }

    public static class AdministrationSettingsService
    {
        public const string Languages = "/api/v1/languages?PageSize={0}&Search={1}";
    }
}