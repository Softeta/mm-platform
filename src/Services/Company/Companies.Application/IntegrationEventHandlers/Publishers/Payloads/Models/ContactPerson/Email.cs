using ValueObjects = Domain.Seedwork.Shared.ValueObjects;

namespace Companies.Application.IntegrationEventHandlers.Publishers.Payloads.Models.ContactPerson
{
    internal class Email
    {
        public string Address { get; set; } = null!;
        public bool IsVerified { get; set; }
        public Guid? VerificationKey { get; set; }
        public DateTimeOffset? VerificationRequestedAt { get; set; }
        public DateTimeOffset? VerifiedAt { get; set; }

        public static Email FromDomain(ValueObjects.Email email)
        {
            return new Email
            {
                Address = email.Address,
                IsVerified = email.IsVerified,
                VerificationKey = email.VerificationKey,
                VerificationRequestedAt = email.VerificationRequestedAt,
                VerifiedAt = email.VerifiedAt
            };
        }
    }
}
