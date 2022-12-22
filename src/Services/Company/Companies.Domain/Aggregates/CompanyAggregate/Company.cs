using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Companies.Domain.Aggregates.CompanyAggregate.Services;
using Companies.Domain.Events;
using Domain.Seedwork;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;

namespace Companies.Domain.Aggregates.CompanyAggregate;

public class Company : AggregateRoot
{
    public CompanyStatus Status { get; private set; }

    public string? Name { get; private set; }

    public string? RegistrationNumber { get; private set; }

    public Image? Logo { get; private set; }

    public Address? Address { get; private set; }

    public string? WebsiteUrl { get; private set; }

    public string? LinkedInUrl { get; private set; }

    public string? GlassdoorUrl { get; private set; }

    public DateTimeOffset? RejectedAt { get; private set; }

    public IReadOnlyList<ContactPerson> ContactPersons => _contactPersons;
    private readonly List<ContactPerson> _contactPersons = new();

    private readonly List<CompanyIndustry> _industries = new();
    public IReadOnlyList<CompanyIndustry> Industries => _industries;

    public Company()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    public void RegisterMyself(
        string email,
        Guid externalId,
        SystemLanguage? systemLanguage,
        bool acceptTermsAndConditions,
        bool acceptMarketingAcknowledgement)
    {
        var contactPerson = ContactPerson.CreateMyself(Id);

        Status = CompanyStatus.Registered;
        contactPerson.RegisterMyself(
            email,
            externalId,
            systemLanguage,
            acceptTermsAndConditions,
            acceptMarketingAcknowledgement);

        _contactPersons.Add(contactPerson);

        AddEvent(new ContactPersonRegisteredDomainEvent(this, contactPerson, contactPerson.CreatedAt));
    }

    public void LinkContactPerson(
        string email,
        Guid externalId,
        SystemLanguage? systemLanguage,
        bool acceptTermsAndConditions,
        bool acceptMarketingAcknowledgement)
    {
        var contactPerson = _contactPersons.Single(x => x.Email.Address == email);

        contactPerson.Link(
            externalId,
            systemLanguage,
            acceptTermsAndConditions,
            acceptMarketingAcknowledgement);

        AddEvent(new ContactPersonLinkedDomainEvent(this, contactPerson, DateTimeOffset.UtcNow));
    }

    public void Create(
        string name,
        string registrationNumber,
        Dictionary<ImageType, string?>? logoPaths,
        string? websiteUrl,
        string? linkedInUrl,
        string? glassdoorUrl,
        string addressLine,
        string? city,
        string? country,
        string? postalCode,
        double? longitude,
        double? latitude,
        string personEmail,
        string personFirstName,
        string personLastName,
        string? personPhoneCountryCode,
        string? personPhoneNumber,
        Guid? personPositionId,
        string? personPositionCode,
        Guid? personPositionAliasToId,
        string? personPositionAliasToCode,
        Dictionary<ImageType, string?>? personProfilePicturePaths,
        Guid? personProfilePictureCacheId,
        IEnumerable<CompanyIndustry> industries,
        List<Guid> fileCacheIds,
        Guid createdById,
        Scope createdByScope)
    {
        Status = CompanyStatus.Approved;
        Name = name;
        RegistrationNumber = registrationNumber;
        WebsiteUrl = websiteUrl;
        LinkedInUrl = linkedInUrl;
        GlassdoorUrl = glassdoorUrl;

        Address = new Address(
            addressLine,
            city,
            country,
            postalCode,
            longitude,
            latitude);

        if (logoPaths != null)
        {
            Logo = new Image(logoPaths);
        }

        AddContactPerson(
            personEmail,
            ContactPersonRole.Admin,
            personFirstName,
            personLastName,
            personPhoneCountryCode,
            personPhoneNumber,
            personPositionId,
            personPositionCode,
            personPositionAliasToId,
            personPositionAliasToCode,
            personProfilePicturePaths,
            personProfilePictureCacheId,
            createdById,
            createdByScope);

        _industries.AddRange(industries);

        Validate();

        if (fileCacheIds.Count > 0)
        {
            AddEvent(new CompanyFilesAddedDomainEvent(fileCacheIds, CreatedAt));
        }
        AddEvent(new CompanyCreatedDomainEvent(this, CreatedAt));
    }

    public void Register(
        string name,
        string registrationNumber,
        string addressLine,
        string? city,
        string? country,
        string? postalCode,
        double? longitude,
        double? latitude,
        string personEmail,
        string personFirstName,
        string personLastName,
        string? personPhoneCountryCode,
        string? personPhoneNumber,
        Guid? personPositionId,
        string? personPositionCode,
        Guid? personPositionAliasToId,
        string? personPositionAliasToCode,
        IEnumerable<CompanyIndustry> industries)
    {
        Status = CompanyStatus.Pending;
        Name = name;
        RegistrationNumber = registrationNumber;

        Address = new Address(
            addressLine,
            city,
            country,
            postalCode,
            longitude,
            latitude);

        _industries.AddRange(industries);

        var contactPerson = GetContactPerson(personEmail);

        contactPerson.SetPending(
            personFirstName,
            personLastName,
            personPhoneCountryCode,
            personPhoneNumber,
            personPositionId,
            personPositionCode,
            personPositionAliasToId,
            personPositionAliasToCode);
    }

    public void Update(
        string? websiteUrl,
        string? linkedInUrl,
        string? glassdoorUrl,
        string addressLine,
        string? city,
        string? country,
        string? postalCode,
        double? longitude,
        double? latitude,
        IEnumerable<CompanyIndustry> industries,
        Dictionary<ImageType, string?>? logoPaths,
        bool isLogoChanged,
        Guid? logoCacheId)
    {
        WebsiteUrl = websiteUrl;
        LinkedInUrl = linkedInUrl;
        GlassdoorUrl = glassdoorUrl;

        Address = new Address(
            addressLine,
            city,
            country,
            postalCode,
            longitude,
            latitude);

        if (isLogoChanged)
        {
            Logo = logoPaths != null ? new Image(logoPaths) : null;
        }

        _industries.Calibrate(industries, Id);

        if (isLogoChanged && logoCacheId.HasValue)
        {
            AddEvent(new CompanyFilesAddedDomainEvent(
                new List<Guid>() { logoCacheId.Value },
                DateTimeOffset.UtcNow));
        }
        AddEvent(new CompanyUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public ContactPerson AddContactPerson(
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
        Guid? pictureCacheId,
        Guid createdById,
        Scope createdByScope)
    {
        if (Status != CompanyStatus.Approved)
        {
            throw new DomainException("Adding contact person is not allowed for not approved company",
                ErrorCodes.Company.AddContactPersonNotAllowed);
        }

        var newContactPerson = ContactPerson.CreateByOther(
            Id,
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

        _contactPersons.Add(newContactPerson);

        if (pictureCacheId.HasValue)
        {
            AddEvent(new CompanyFilesAddedDomainEvent(
                new List<Guid>() { pictureCacheId.Value },
                newContactPerson.CreatedAt));
        }

        AddEvent(new ContactPersonAddedDomainEvent(this, newContactPerson, newContactPerson.CreatedAt));

        return newContactPerson;
    }

    public ContactPerson UpdateContactPerson(
        Guid id,
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
        bool isPictureChanged,
        Guid? pictureCacheId)
    {
        var contactPerson = GetContactPerson(id);

        contactPerson.Update(
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
            isPictureChanged);

        if (isPictureChanged && pictureCacheId.HasValue)
        {
            AddEvent(new CompanyFilesAddedDomainEvent(
                new List<Guid>() { pictureCacheId.Value },
                 DateTimeOffset.UtcNow));
        }

        ValidateHasAdmin();

        AddEvent(new ContactPersonUpdatedDomainEvent(this, contactPerson, DateTimeOffset.UtcNow));

        return contactPerson;
    }

    public ContactPerson UpdateContactPersonLegalTerms(Guid contactId, bool termsAgreement, bool marketingAgreement)
    {
        var contactPerson = GetContactPerson(contactId);

        contactPerson.UpdateLegalTerms(termsAgreement, marketingAgreement);

        AddEvent(new ContactPersonUpdatedDomainEvent(this, contactPerson, DateTimeOffset.UtcNow));

        return contactPerson;
    }

    public ContactPerson UpdateContactPersonSettings(Guid contactId, SystemLanguage systemLanguage, bool marketingAcknowledgement)
    {
        var contactPerson = GetContactPerson(contactId);

        contactPerson.UpdateSettings(systemLanguage, marketingAcknowledgement);

        AddEvent(new ContactPersonUpdatedDomainEvent(this, contactPerson, DateTimeOffset.UtcNow));

        return contactPerson;
    }

    public ContactPerson VerifyContactPersonEmail(Guid contactId, Guid key, int expirationInMinutes)
    {
        var contactPerson = GetContactPerson(contactId);

        contactPerson.VerifyEmail(key, expirationInMinutes);

        return contactPerson;
    }

    public ContactPerson RequestEmailVerification(Guid externalId)
    {
        var contactPerson = GetContactPersonByExternlId(externalId);
        contactPerson.RequestEmailVerification();

        AddEvent(new ContactPersonEmailVerificationRequestedDomainEvent(this, contactPerson, DateTimeOffset.UtcNow));

        return contactPerson;
    }

    public void RejectContactPerson(Guid contactId)
    {
        var contactPerson = GetContactPerson(contactId);
        ValidateIfOtherContactPersonAdminExist(contactId);

        contactPerson.Reject();

        AddEvent(new ContactPersonRejectedDomainEvent(this, contactPerson, DateTimeOffset.UtcNow));
    }
    
    public void DeleteContactPerson(Guid contactId)
    {
        var contactPerson = GetContactPerson(contactId);
        _contactPersons.Remove(contactPerson);
    }

    public void Approve()
    {
        if (Status != CompanyStatus.Pending)
        {
            throw new DomainException(
                $"Only pending company can be approved. Current company status: {Status}",
                ErrorCodes.Company.ApproveNotAllowed);
        }

        Validate();
        var contactPersonToApprove = _contactPersons.Single();
        contactPersonToApprove.Approve();

        Status = CompanyStatus.Approved;

        AddEvent(new CompanyApprovedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void Reject()
    {
        if (Status != CompanyStatus.Pending)
        {
            throw new DomainException(
                $"Only pending company can be rejected. Current company status: {Status}",
                ErrorCodes.Company.RejectNotAllowed);
        }

        Validate();
        var contactPerson = _contactPersons.Single();
        contactPerson.Reject();

        Status = CompanyStatus.Rejected;
        RejectedAt = DateTimeOffset.UtcNow;

        AddEvent(new CompanyRejectedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public ContactPerson GetContactPerson(Guid id)
    {
        var contactPerson = _contactPersons.SingleOrDefault(x => x.Id == id);

        if (contactPerson is null)
        {
            throw new DomainException($"Contact person was not fond, contact id: {id}", 
                ErrorCodes.Company.ContactPersonNotFound);
        }

        return contactPerson;
    }

    public void SyncJobPositions(Guid id, Guid? aliasId, string? aliasCode)
    {
        var contactPersons = _contactPersons.Where(x => x.Position != null && x.Position.Id == id);

        foreach (var contactPerson in contactPersons)
        {
            contactPerson.SyncJobPosition(aliasId, aliasCode);
        }
    }

    private ContactPerson GetContactPerson(string email)
    {
        var contactPerson = _contactPersons.SingleOrDefault(x => x.Email.Address == email);

        if (contactPerson is null)
        {
            throw new DomainException($"Contact person was not fond, contact email: {email}",
                ErrorCodes.Company.ContactPersonNotFound);
        }

        return contactPerson;
    }

    private ContactPerson GetContactPersonByExternlId(Guid externalId)
    {
        var contactPerson = _contactPersons.SingleOrDefault(x => x.ExternalId == externalId);

        if (contactPerson is null)
        {
            throw new DomainException($"Contact person was not fond, contact externalId: {externalId}", 
                ErrorCodes.Company.ContactPersonNotFound);
        }

        return contactPerson;
    }

    private void Validate()
    {
        if (_contactPersons.Count != 1)
        {
            throw new DomainException("One contact person must exit", 
                ErrorCodes.Company.OneContactPersonMandatory);
        }

        ValidateHasAdmin();
    }

    private void ValidateHasAdmin()
    {
        if (_contactPersons.All(x => x.Role != ContactPersonRole.Admin))
        {
            throw new DomainException("Admin must exist",
                ErrorCodes.Company.AdminMandatory);
        }
    }

    private void ValidateIfOtherContactPersonAdminExist(Guid contactId)
    {
        var admin = _contactPersons
            .Where(x => x.Id != contactId)
            .Where(x => x.Role == ContactPersonRole.Admin)
            .Where(x => x.Stage != ContactPersonStage.Rejected)
            .FirstOrDefault();

        if (admin is null)
        {
            throw new DomainException("Other admin of contact person must exist", 
                ErrorCodes.Company.OtherContactPersonAdminMustExist);
        }
    }
}
