using Domain.Seedwork.Consts;
using Domain.Seedwork.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.Seedwork.Shared.ValueObjects
{
    public class Email : ValueObject<Email>
    {
        public string Address { get; init; } = null!;
        public bool IsVerified { get; init; }
        public Guid? VerificationKey { get; init; }
        public DateTimeOffset? VerificationRequestedAt { get; init; }
        public DateTimeOffset? VerifiedAt { get; init; }

        private Email() { }

        private Email(string email, bool isVerified, Guid? verificationKey, DateTimeOffset? verificationRequestedAt, DateTimeOffset? verifiedAt)
        {
            Address = email;
            IsVerified = isVerified;
            VerificationKey = verificationKey;
            VerificationRequestedAt = verificationRequestedAt;
            VerifiedAt = verifiedAt;

            Validate();
        }

        public static Email? CreateNullable(string? address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                return null;
            }       

            return new Email(address, false, null, null, null);
        }

        public static Email Create(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new DomainException("Email address is rerquired");
            }

            return new Email(address, false, null, null, null);
        }

        public static Email CreateWithVerification(string address)
        {
            return new Email(
                address,
                false,
                Guid.NewGuid(),
                DateTimeOffset.UtcNow,
                null);
        }

        public static Email CreateVerified(string address)
        {
            return new Email(
                address,
                true, 
                null,
                null,
                DateTimeOffset.UtcNow);
        }

        public void ValidateVerification(Guid key, int expirationInMinutes)
        {
            if (!VerificationKey.HasValue)
            {
                throw new DomainException("Verification key not found.", ErrorCodes.Shared.Email.VerificationKeyNotFound);
            }
            if (IsVerified)
            {
                throw new DomainException("Already verified", ErrorCodes.Shared.Email.AlreadyVerified);
            }
            if (VerificationKey != key)
            {
                throw new DomainException("Verification key is not valid.", ErrorCodes.Shared.Email.VerificationKeyNotValid);
            }
            if (!VerificationRequestedAt.HasValue || VerificationRequestedAt.Value.AddMinutes(expirationInMinutes) < DateTimeOffset.UtcNow)
            {
                throw new DomainException("Verification key is expired.", ErrorCodes.Shared.Email.VerificationKeyExpired);
            }
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Address;
            yield return IsVerified;
            yield return VerifiedAt;
        }

        private void Validate()
        {
            var match = Regex.Match(Address, RegExpressions.Email, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                throw new DomainException("Email is invalid", ErrorCodes.Shared.Email.Invalid);
            }
        }
    }
}
