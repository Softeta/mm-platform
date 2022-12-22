using Domain.Seedwork.Shared.Entities;

namespace Companies.Domain.Aggregates.CompanyAggregate.Entities
{
    public class CompanyIndustry : IndustryBase
    {
        public Guid CompanyId { get; private set; }

        public CompanyIndustry(Guid industryId, Guid companyId, string code)
        {
            Id = Guid.NewGuid();
            IndustryId = industryId;
            CompanyId = companyId;
            Code = code;
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
