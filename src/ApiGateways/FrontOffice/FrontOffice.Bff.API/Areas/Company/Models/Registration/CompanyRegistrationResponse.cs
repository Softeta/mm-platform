using Contracts.Company.Responses;
using Contracts.Job.Jobs.Responses;

namespace FrontOffice.Bff.API.Areas.Company.Models.Registration
{
    public class CompanyRegistrationResponse
    {
        public GetCompanyResponse Company { get; set; } = null!;
        public GetJobResponse? Job { get; set; }
        public string? ErrorCode { get; set; }

        public static CompanyRegistrationResponse FromResponses(
            GetCompanyResponse company, 
            GetJobResponse? job,
            string? errorCode)
        {
            return new CompanyRegistrationResponse
            {
                Company = company,
                Job = job,
                ErrorCode = errorCode
            };
        }
    }
}
