using Contracts.Shared;
using Contracts.Shared.Responses;
using Domain.Seedwork.Enums;

namespace Contracts.Company.Responses.ContactPersons
{
    public abstract class GetContactPersonBase
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Position? Position { get; set; }
        public string Email { get; set; } = null!;
        public PhoneFullResponse? Phone { get; set; }
        public ImageResponse? Picture { get; set; }
        public SystemLanguage? SystemLanguage { get; set; }
        public Guid? ExternalId { get; set; }
    }
}
