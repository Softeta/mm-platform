using Domain.Seedwork.Enums;

namespace EmailService.Send.Helpers
{
    internal static class QueryParamsHelper
    {
        public static string GetLanguage(SystemLanguage? systemLanguage) =>
               $"language={(systemLanguage.HasValue ? systemLanguage.Value.ToString().ToLower() : SystemLanguage.DA.ToString().ToLower())}";
    }
}
