using System.Threading.Tasks;
using EmailService.Sync.Events.Candidate;
using sib_api_v3_sdk.Model;
using Task = System.Threading.Tasks.Task;

namespace EmailService.Sync.SendInBlue
{
    public interface ICandidateContactsClient
    {
        Task CreateContactAsync(CandidatePayload model);
        Task UpdateContactAsync(CandidatePayload model);
        Task DeleteContactAsync(string email);
        Task<GetExtendedContactDetails?> GetContactAsync(string email);
    }
}
