using Domain.Seedwork;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;

namespace Jobs.Domain.Aggregates.JobAggregate.Entities
{
    public class JobContactPerson : Entity
    {
        public Guid JobId { get; private set; }
        public Guid PersonId { get; private set; }
        public bool IsMainContact { get; private set; }
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public Position? Position { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string Email { get; private set; } = null!;
        public string? PictureUri { get; private set; }
        public SystemLanguage? SystemLanguage { get; private set; }
        public Guid? ExternalId { get; private set; }

        private JobContactPerson () { }

        public JobContactPerson(
            Guid jobId,
            Guid personId,
            bool isMainContact,
            string firstName,
            string lastName,
            string? phoneNumber,
            string email,
            Guid? positionId,
            string? positionCode,
            Guid? positionAliasToId,
            string? positionAliasToCode,
            string? pictureUri,
            SystemLanguage? systemLanguage,
            Guid? externalId)
        {
            Id = Guid.NewGuid();
            JobId = jobId;
            PersonId = personId;
            IsMainContact = isMainContact;
            FirstName = firstName;
            LastName = lastName;
            Position = Position.Create(
                positionId,
                positionCode,
                positionAliasToId,
                positionAliasToCode);
            PhoneNumber = phoneNumber;
            Email = email;
            PictureUri = pictureUri;
            SystemLanguage = systemLanguage;
            ExternalId = externalId;
            CreatedAt = DateTimeOffset.UtcNow;

            Validate();
        }

        public void Sync(
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            Guid? positionId,
            string? positionCode,
            Guid? positionAliasToId,
            string? positionAliasToCode,
            SystemLanguage? systemLanguage)
        {
            FirstName = firstName;
            LastName = lastName;
            Position = Position.Create(
                positionId, 
                positionCode,
                positionAliasToId,
                positionAliasToCode);
            PhoneNumber = phoneNumber;
            Email = email;
            PictureUri = pictureUri;
            SystemLanguage = systemLanguage;
        }

        public void SyncPosition(Guid? aliasToId, string? aliasToCode)
        {
            if (Position != null)
            {
                Position = new Position(Position.Id, Position.Code, aliasToId, aliasToCode);
            }   
        }

        public void SyncExternalId(Guid? externalId)
        {
            ExternalId = externalId;
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                throw new DomainException($"{nameof(Email)} is mandatory",
                    ErrorCodes.Job.ContactPerson.EmailRequired);
            }
        }

        public JobContactPerson Clone()
        {
            return (JobContactPerson)MemberwiseClone();
        }
    }
}
