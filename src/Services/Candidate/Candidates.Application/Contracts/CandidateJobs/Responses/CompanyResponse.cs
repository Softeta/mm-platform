using Candidates.Domain.Aggregates.CandidateJobsAggregate.ValueObjects;
using Common = Contracts.Candidate.CandidateJobs.Responses;

namespace Candidates.Application.Contracts.CandidateJobs.Responses
{
    public class CompanyResponse : Common.CompanyResponse
    {
        public static CompanyResponse FromDomain(Company company)
        {
            return new CompanyResponse
            {
                Name = company.Name,
                LogoUri = company.LogoUri
            };
        }
    }
}
