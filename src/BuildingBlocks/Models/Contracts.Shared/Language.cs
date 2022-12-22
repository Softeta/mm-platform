using Domain.Seedwork.Shared.Entities;

namespace Contracts.Shared
{
    public class Language
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public static Language FromDomain(LanguageBase language)
        {
            return new Language
            {
                Id = language.Language.Id,
                Code = language.Language.Code,
                Name = language.Language.Name,
            };
        }
    }
}