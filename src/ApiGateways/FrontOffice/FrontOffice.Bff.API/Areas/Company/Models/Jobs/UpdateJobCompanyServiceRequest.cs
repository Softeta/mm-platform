using Common = Contracts.Job;

namespace FrontOffice.Bff.API.Areas.Company.Models.Jobs
{
    public class UpdateJobCompanyServiceRequest : Common.Companies.Requests.UpdateJobCompanyRequest
    {
        public static Common.Companies.Requests.UpdateJobCompanyRequest ToServiceRequest(Common.Jobs.Responses.GetJobResponse job)
        {
            return new Common.Companies.Requests.UpdateJobCompanyRequest
            {
                Address = job.Company.Address,
                Description = job.Company.Description,
                MainContactId = job.Company.ContactPersons.Single(x => x.IsMainContact).Id,
            };
        }

        public static Common.Companies.Requests.UpdateJobCompanyRequest ToServiceRequestStep6(
            Common.Jobs.Responses.GetJobResponse job, 
            Step6RequestBase request)
        {
            var payload = ToServiceRequest(job);
            payload.Address = request.Address;

            return payload;
        }
    }
}