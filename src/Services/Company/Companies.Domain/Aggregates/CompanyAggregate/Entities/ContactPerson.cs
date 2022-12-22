using Domain.Seedwork;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;

namespace Companies.Domain.Aggregates.CompanyAggregate.Entities
{
    public class ContactPerson : Entity
    {
        public Guid CompanyId { get; init; }
        public Guid? ExternalId { get; private set; }
        public Email Email { get; private set; } = null!;
        public ContactPersonStage Stage { get; private set; }
        public ContactPersonRole Role { get; private set; }
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public Position? Position { get; private set; }
        public Phone? Phone { get; private set; }
        public Image? Picture { get; private set; }
        public SystemLanguage? SystemLanguage { get; private set; }
        public LegalInformationAgreement? TermsAndConditions { get; private set; }
        public LegalInformationAgreement? MarketingAcknowledgement { get; private set; }
        public DateTimeOffset? RejectedAt { get; private set; }
        public CreatedBy? CreatedBy { get; private set; }

        private ContactPerson() { }

        private ContactPerson(Guid companyId)
        {
            Id = Guid.NewGuid();
            CompanyId = companyId;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        private ContactPerson(
            Guid companyId,
            string email,
            ContactPersonRole role,
            string firstName, 
            string lastName,
            string? phoneCountryCode,
            string? phoneNumber,
            Guid? positionId,
            string? positionCode,
            Guid? positionAliasToId,
            string? positionAliasToCode,
            Dictionary<ImageType, string?>? picturePaths,
            Guid createdById,
            Scope createdByScope)
        {
            Id = Guid.NewGuid();
            CompanyId = companyId;
            Stage = ContactPersonStage.Approved;
            Role = role;
            FirstName = firstName;
            LastName = lastName;
            Position = Position.Create(positionId, positionCode, positionAliasToId, positionAliasToCode);
            Phone = new Phone(phoneCountryCode, phoneNumber);
            Email = Email.Create(email);
            CreatedBy = new CreatedBy(createdById, createdByScope);
            CreatedAt = DateTimeOffset.UtcNow;

            if (picturePaths != null)
            {
                Picture = new Image(picturePaths);
            } 

            Validate();
        }

        public static ContactPerson CreateMyself(Guid companyId) => 
            new ContactPerson(companyId);

        public static ContactPerson CreateByOther(
            Guid companyId,
            string email,
            ContactPersonRole role,
            string firstName,
            string lastName,
            string? phoneCountryCode,
            string? phoneNumber,
            Guid? positionId,
            string? positionCode,
            Guid? positionAliasToId,
            string? positionAliasToCode,
            Dictionary<ImageType, string?>? picturePaths,
            Guid createdById,
            Scope createdByScope)
        {
            return new ContactPerson(
                companyId,
                email,
                role,
                firstName,
                lastName,
                phoneCountryCode,
                phoneNumber,
                positionId,
                positionCode,
                positionAliasToId,
                positionAliasToCode,
                picturePaths,
                createdById,
                createdByScope);
        }

        public void RegisterMyself(
            string email,
            Guid externalId,
            SystemLanguage? systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement)
        {
            ValidateIfCandidateLinked();

            Stage = ContactPersonStage.Registered;
            Role = ContactPersonRole.Admin;
            Email = Email.CreateWithVerification(email);
            ExternalId = externalId;
            SystemLanguage = systemLanguage;
            TermsAndConditions = new LegalInformationAgreement(acceptTermsAndConditions, DateTimeOffset.UtcNow);
            MarketingAcknowledgement = new LegalInformationAgreement(acceptMarketingAcknowledgement, DateTimeOffset.UtcNow);
        }

        public void Link(
            Guid externalId,
            SystemLanguage? systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement)
        {
            ValidateIfCandidateLinked();

            Email = Email.CreateWithVerification(Email!.Address);
            ExternalId = externalId;
            SystemLanguage = systemLanguage;
            TermsAndConditions = new LegalInformationAgreement(acceptTermsAndConditions, DateTimeOffset.UtcNow);
            MarketingAcknowledgement = new LegalInformationAgreement(acceptMarketingAcknowledgement, DateTimeOffset.UtcNow);
        }

        public void Update(
            ContactPersonRole role,
            string firstName,
            string lastName,
            string? phoneCountryCode,
            string? phoneNumber,
            Guid? positionId,
            string? positionCode,
            Guid? positionAliasToId,
            string? positionAliasToCode,
            Dictionary<ImageType, string?>? picturePaths,
            bool isPictureChanged)
        {
            Role = role;
            FirstName = firstName;
            LastName = lastName;
            Position = Position.Create(positionId, positionCode, positionAliasToId, positionAliasToCode);
            Phone = new Phone(phoneCountryCode, phoneNumber);
            
            if (isPictureChanged)
            {
                Picture = picturePaths != null
                ? new Image(picturePaths)
                : null;
            }
        }

        public void VerifyEmail(Guid key, int expirationInMinutes)
        {
            if (Email is null)
            {
                throw new DomainException("Email not found", ErrorCodes.NotFound.EmailNotFound);
            }

            Email.ValidateVerification(key, expirationInMinutes);
            Email = Email.CreateVerified(Email.Address);
        }

        public void RequestEmailVerification()
        {
            Email = Email.CreateWithVerification(Email!.Address);
        }

        public void SetPending(
            string firstName,
            string lastName,
            string? phoneCountryCode,
            string? phoneNumber,
            Guid? positionId,
            string? positionCode,
            Guid? positionAliasToId,
            string? positionAliasToCode)
        {
            Stage = ContactPersonStage.Pending;

            Update(
                Role,
                firstName,
                lastName,
                phoneCountryCode,
                phoneNumber,
                positionId,
                positionCode,
                positionAliasToId,
                positionAliasToCode,
                null,
                false);
        }

        public void Reject()
        {
            if (Stage == ContactPersonStage.Rejected)
            {
                throw new DomainException("Candidate is already rejected", ErrorCodes.Company.ContactPerson.AlreadyRejected);
            }

            Stage = ContactPersonStage.Rejected;
            RejectedAt = DateTimeOffset.UtcNow;
        }
        
        public void Approve()
        {
            if (Stage != ContactPersonStage.Pending)
            {
                throw new DomainException(
                    "Only pending contact person can be approved. Current contact person status: {Status}",
                ErrorCodes.Company.ContactPerson.ApproveNotAllowed);
            }
            Stage = ContactPersonStage.Approved;
        }

        public void UpdateLegalTerms(bool termsAgreement, bool marketingAgreement)
        {
            TermsAndConditions = new LegalInformationAgreement(termsAgreement, DateTimeOffset.UtcNow);
            MarketingAcknowledgement = new LegalInformationAgreement(marketingAgreement, DateTimeOffset.UtcNow);
        }

        public void UpdateSettings(SystemLanguage systemLanguage, bool marketingAcknowledgement)
        {
            SystemLanguage = systemLanguage;
            MarketingAcknowledgement = new LegalInformationAgreement(marketingAcknowledgement, DateTimeOffset.UtcNow);
        }

        public void SyncJobPosition(Guid? aliasId, string? aliasCode)
        {
            if (Position != null)
            {
                Position = new Position(Position.Id, Position.Code, aliasId, aliasCode);
            }
        }

        private void Validate()
        {
            if (CompanyId == Guid.Empty)
            {
                throw new DomainException($"Invalid {nameof(CompanyId)}: {CompanyId}",
                    ErrorCodes.Company.ContactPerson.InvalidCompanyId);
            }
        }

        private void ValidateIfCandidateLinked()
        {
            if (ExternalId.HasValue && ExternalId != Guid.Empty)
            {
                throw new DomainException("Contact person is already linked", 
                    ErrorCodes.Company.ContactPerson.AlreadyLinked);
            }
        }
    }
}
