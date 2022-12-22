using EmailService.Sync.Events.ContactPerson;
using sib_api_v3_sdk.Model;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace EmailService.Sync.SendInBlue;

public interface IContactPersonContactsClient
{
    Task CreateContactAsync(ContactPersonPayload model);
    Task UpdateContactAsync(ContactPersonPayload model);
    Task DeleteContactAsync(string email);
    Task<GetExtendedContactDetails?> GetContactAsync(string email);
}