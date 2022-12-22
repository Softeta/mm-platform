using Domain.Seedwork.Shared.Entities;

namespace Contracts.Shared
{
    public class Industry
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;

        public static Industry FromDomain(IndustryBase candidateIndustry)
        {
            return new Industry
            {
                Id = candidateIndustry.IndustryId,
                Code = candidateIndustry.Code,
            };
        }
    }
}
