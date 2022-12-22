using BackOffice.Bff.API.Models.Job.Requests;
using BackOffice.Bff.API.Models.Job.ServiceRequests.Models;
using Contracts.Job.Companies.Requests;
using Company = Contracts.Company.Responses;

namespace BackOffice.Bff.API.Models.Job.ServiceRequests
{
    public class CreateJobCompanyServiceRequest : CreateJobCompanyRequest
    {
        public static CreateJobCompanyServiceRequest ToServiceRequest(
            JobCompanyRequest request,
            Company.GetCompanyResponse company)
        {
            return new CreateJobCompanyServiceRequest
            {
                Id = company.Id,
                Status = company.Status,
                Name = company.Name!,
                Address = request.Address,
                Description = request.Description,
                LogoUri = company.Logo?.Uri,
                ContactPersons = request.ContactPersons
                    .Select(r => JobContactPerson
                        .ToServiceRequest(r, company.ContactPersons.FirstOrDefault(c => c.Id == r.Id)))
            };
        }
    }
}
