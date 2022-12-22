using Companies.Domain.Aggregates.CompanyAggregate.Entities;

namespace Companies.Domain.Aggregates.CompanyAggregate.Services
{
    public static class CompanyIndustryCalibrationService
    {
        public static void Calibrate(this List<CompanyIndustry> current, IEnumerable<CompanyIndustry> request, Guid companyId)
        {
            var requestIndustryIds = new HashSet<Guid>(request.Select(s => s.IndustryId));
            var currentIndustryIds = new HashSet<Guid>(current.Select(s => s.IndustryId));

            var equals = requestIndustryIds.SetEquals(currentIndustryIds);

            if (!equals)
            {
                current.RemoveAll(s => !requestIndustryIds.Contains(s.IndustryId));

                var industriesToAdd = request.Where(s => !currentIndustryIds.Contains(s.IndustryId));

                foreach (var industry in industriesToAdd)
                {
                    current.Add(new CompanyIndustry(industry.IndustryId, companyId, industry.Code));
                }
            }
        }
    }
}
