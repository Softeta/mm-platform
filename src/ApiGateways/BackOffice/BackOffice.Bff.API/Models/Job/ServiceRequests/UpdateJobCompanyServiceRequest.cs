using BackOffice.Bff.API.Models.Job.Requests;
using BackOffice.Bff.API.Models.Job.ServiceRequests.Models;
using Contracts.Company.Responses.ContactPersons;
using Contracts.Job.Companies.Requests;

namespace BackOffice.Bff.API.Models.Job.ServiceRequests
{
    public class UpdateJobCompanyServiceRequest : UpdateJobCompanyRequest
    {
        public static UpdateJobCompanyRequest ToServiceRequest(
            JobCompanyRequest request,
            IEnumerable<AddJobContactPersonRequest> contactPersonsToAdd,
            IEnumerable<GetContactPersonBriefResponse> companyContactPersons,
            IEnumerable<Guid> contactPersonsToRemove,
            Guid mainContactId)
        {
            return new UpdateJobCompanyRequest
            {
                Address = request.Address,
                Description = request.Description,
                ContactPersonsToAdd = contactPersonsToAdd
                    .Select(r => JobContactPerson
                        .ToServiceRequest(r, companyContactPersons.FirstOrDefault(c => c.Id == r.Id))),
                ContactPersonsToRemove = contactPersonsToRemove,
                MainContactId = mainContactId
            };
        }
    }
}
