using Domain.Seedwork.Enums;
using EmailService.Sync.Events.Candidate;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using EmailService.Sync.Events.ContactPerson;
using Sib = sib_api_v3_sdk;
using Task = System.Threading.Tasks.Task;
using System.Collections.Generic;
using EmailService.Sync.Events.Models;

namespace EmailService.Sync.SendInBlue
{
    public class SendInBlueClient : ICandidateContactsClient, IContactPersonContactsClient
    {
        private const string ApiKeyName = "api-key";
        private readonly Sib.Client.Configuration _configurations;
      
        public SendInBlueClient(string apiKey)
        {
            _configurations = new Sib.Client.Configuration();
            _configurations.ApiKey.Add(ApiKeyName, apiKey);
        }

        public async Task CreateContactAsync(CandidatePayload model)
        {
            var api = new ContactsApi(_configurations);
            await api.CreateContactAsync(new CreateContact
            {
                Email = model.Email.Address,
                Attributes = BuildContactsAttributes(model)
            });
        }

        public async Task UpdateContactAsync(CandidatePayload model)
        {
            var api = new ContactsApi(_configurations);
            await api.UpdateContactAsync(model.Email.Address, new UpdateContact
            {
                Attributes = BuildContactsAttributes(model)
            });
        }

        public async Task CreateContactAsync(ContactPersonPayload model)
        {
            var api = new ContactsApi(_configurations);

            await api.CreateContactAsync(new CreateContact
            {
                Email = model.Email.Address,
                Attributes = BuildContactsAttributes(model)
            });
        }

        public async Task UpdateContactAsync(ContactPersonPayload model)
        {
            var api = new ContactsApi(_configurations);

            await api.UpdateContactAsync(model.Email.Address, new UpdateContact
            {
                Attributes = BuildContactsAttributes(model)
            });
        }

        public async Task DeleteContactAsync(string email)
        {
            var api = new ContactsApi(_configurations);
            await api.DeleteContactAsync(email);
        }

        public async Task<GetExtendedContactDetails?> GetContactAsync(string email)
        {
            var api = new ContactsApi(_configurations);
            try
            {
                var contactDetails = await api.GetContactInfoAsync(email);
                return contactDetails;
            }
            catch
            {
                return null;
            }
        }

        private static object BuildContactsAttributes(CandidatePayload payload)
        {
            return new
            {
                PHONE = payload.Phone?.PhoneNumber,
                FIRSTNAME = payload.FirstName,
                LASTNAME = payload.LastName,
                CURRENTPOSITION = payload.CurrentPosition?.Code,
                OPENFOROPPORTUNITIES = payload.OpenForOpportunitiesValue,
                WORKTYPE = string.Join(",", payload.WorkTypes),
                WORKFORMAT = string.Join(",", BuildFormats(payload.Terms?.Formats) ?? Array.Empty<FormatType>()),
                STARTDATE = payload.Terms?.StartDate?.ToString("yyyy-MM-dd"),
                ENDDATE = payload.Terms?.EndDate?.ToString("yyyy-MM-dd"),
                SYSTEMLANGUAGE = payload.SystemLanguageValue,
                MARKETINGPERMISSIONS = payload.MarketingAcknowledgementValue,
                CURRENTCOMPANY = payload.WorkExperiences.FirstOrDefault(x => x.IsCurrentJob)?.CompanyName,
                ISAPPROVED = payload.StatusValue
            };
        }

        private static IEnumerable<FormatType> BuildFormats(Formats? formats)
        {
            if (formats?.IsHybrid ?? false)
            {
                yield return FormatType.Hybrid;
            }

            if (formats?.IsRemote ?? false)
            {
                yield return FormatType.Remote;
            }

            if (formats?.IsOnSite ?? false)
            {
                yield return FormatType.Onsite;
            }
        }

        private static object BuildContactsAttributes(ContactPersonPayload payload)
        {
            return new
            {
                PHONE = payload.Phone?.PhoneNumber,
                FIRSTNAME = payload.FirstName,
                LASTNAME = payload.LastName,
                ROLE = payload.RoleValue,
                MARKETINGPERMISSIONS = payload.MarketingAcknowledgementValue,
                COMPANY_NAME = payload.CompanyName
            };
        }
    }
}
