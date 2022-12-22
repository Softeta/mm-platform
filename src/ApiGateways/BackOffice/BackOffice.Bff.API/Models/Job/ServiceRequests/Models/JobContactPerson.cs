using BackOffice.Bff.API.Models.Job.Requests;
using Contracts.Shared;
using Domain.Seedwork.Exceptions;
using Common = Contracts.Job.Companies.Requests;
using ContactPersons = Contracts.Company.Responses.ContactPersons;

namespace BackOffice.Bff.API.Models.Job.ServiceRequests.Models
{
    public class JobContactPerson : Common.JobContactPersonRequest
    {
        public static Common.JobContactPersonRequest ToServiceRequest(
            AddJobContactPersonRequest request,
            ContactPersons.GetContactPersonBase? contactPerson)
        {
            if (contactPerson is null)
            {
                throw new BadRequestException($"Contact person does not exist. Id: {request.Id}",
                    ErrorCodes.BadRequest.ContactPersonNotExists);
            }

            return new Common.JobContactPersonRequest
            {
                Id = request.Id,
                FirstName = contactPerson.FirstName!,
                LastName = contactPerson.LastName!,
                IsMainContact = request.IsMainContact,
                Position = Position.From(
                    contactPerson.Position?.Id,
                    contactPerson.Position?.Code,
                    contactPerson.Position?.AliasTo?.Id,
                    contactPerson.Position?.AliasTo?.Code),
                PhoneNumber = contactPerson.Phone?.PhoneNumber,
                Email = contactPerson.Email,
                PictureUri = contactPerson.Picture?.Uri,
                SystemLanguage = contactPerson.SystemLanguage,
                ExternalId = contactPerson.ExternalId
            };
        }
    }
}
