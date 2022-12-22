using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Candidates.Domain.Aggregates.CandidateAggregate.Services;
using Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects;
using Candidates.Domain.Events.CandidateAggregate;
using Domain.Seedwork;
using Domain.Seedwork.Attributes;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;

namespace Candidates.Domain.Aggregates.CandidateAggregate;

public class Candidate : AggregateRoot
{
    public Guid? ExternalId { get; private set; }

    [MigrationField]
    public long? CandidateOldPlatformId { get; init; }
    public Email? Email { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public CandidateStatus Status { get; private set; }
    public Image? Picture { get; private set; }
    public Position? CurrentPosition { get; private set; }
    public DateTimeOffset? BirthDate { get; private set; }
    public bool OpenForOpportunities { get; private set; }
    public string? LinkedInUrl { get; private set; }
    public string? PersonalWebsiteUrl { get; private set; }
    public Address? Address { get; private set; }
    public Terms Terms { get; private set; } = null!;
    public Phone? Phone { get; private set; }
    public bool IsShortListed { get; private set; }
    public bool IsHired { get; private set; }
    public int? YearsOfExperience { get; private set; }
    public string? Bio { get; private set; }
    public Document? CurriculumVitae { get; private set; }
    public Document? Video { get; private set; }
    public Note? Note { get; private set; }
    public DateTimeOffset? RejectedAt { get; private set; }
    public SystemLanguage? SystemLanguage { get; private set; }
    public LegalInformationAgreement? TermsAndConditions { get; private set; }
    public LegalInformationAgreement? MarketingAcknowledgement { get; private set; }

    private readonly List<CandidateActivityStatus> _activityStatuses = new();
    public IReadOnlyList<CandidateActivityStatus> ActivityStatuses => _activityStatuses;

    private readonly List<CandidateIndustry> _industries = new();
    public IReadOnlyList<CandidateIndustry> Industries => _industries;

    private readonly List<CandidateSkill> _skills = new();
    public IReadOnlyList<CandidateSkill> Skills => _skills;

    private readonly List<CandidateDesiredSkill> _desiredSkills = new();
    public IReadOnlyList<CandidateDesiredSkill> DesiredSkills => _desiredSkills;

    private readonly List<CandidateLanguage> _languages = new();
    public IReadOnlyList<CandidateLanguage> Languages => _languages;

    private readonly List<CandidateCourse> _courses = new();
    public IReadOnlyList<CandidateCourse> Courses => _courses;

    private readonly List<CandidateEducation> _educations = new();
    public IReadOnlyList<CandidateEducation> Educations => _educations;

    private readonly List<CandidateWorkExperience> _workExperiences = new();
    public IReadOnlyList<CandidateWorkExperience> WorkExperiences => _workExperiences;

    private readonly List<CandidateHobby> _hobbies = new();
    public IReadOnlyList<CandidateHobby> Hobbies => _hobbies;

    public Candidate()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void RegisterMyself(
        string email,
        Guid externalId,
        SystemLanguage? systemLanguage,
        bool acceptTermsAndConditions,
        bool acceptMarketingAcknowledgement)
    {
        ValidateIfCandidateLinked();

        Email = Email.CreateWithVerification(email);
        ExternalId = externalId;
        Status = CandidateStatus.Registered;
        SystemLanguage = systemLanguage;
        TermsAndConditions = new LegalInformationAgreement(acceptTermsAndConditions, DateTimeOffset.UtcNow);
        MarketingAcknowledgement = new LegalInformationAgreement(acceptMarketingAcknowledgement, DateTimeOffset.UtcNow);

        AddEvent(new CandidateRegisteredDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void LinkCandidate(
        Guid externalId,
        SystemLanguage? systemLanguage,
        bool acceptTermsAndConditions,
        bool acceptMarketingAcknowledgement)
    {
        ValidateIfCandidateLinked();
        ValidateIfEmailExist();

        Email = Email.CreateWithVerification(Email!.Address);
        ExternalId = externalId;
        SystemLanguage = systemLanguage;
        TermsAndConditions = new LegalInformationAgreement(acceptTermsAndConditions, DateTimeOffset.UtcNow);
        MarketingAcknowledgement = new LegalInformationAgreement(acceptMarketingAcknowledgement, DateTimeOffset.UtcNow);

        AddEvent(new CandidateRegisteredDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void Initialize(
        string? email,
        string firstName,
        string lastName,
        Dictionary<ImageType, string?>? picturePaths,
        Guid? currentPositionId,
        string? currentPositionCode,
        Guid? currentPositionAliasId,
        string? currentPositionAliasCode,
        DateTimeOffset? birthDate,
        bool openForOpportunities,
        string? linkedInUrl,
        string? personalWebsiteUrl,
        string? addressLine,
        string? city,
        string? country,
        string? postalCode,
        double? longitude,
        double? latitude,
        DateTimeOffset? startDate,
        DateTimeOffset? endDate,
        int? weeklyWorkHours,
        string? currency,
        decimal? freelanceHourlySalary,
        decimal? freelanceMonthlySalary,
        decimal? permanentMonthlySalary,
        ICollection<WorkingHoursType>? workingHourTypes,
        string? phoneCountryCode,
        string? phoneNumber,
        int? yearsOfExperience,
        string? bio,
        string? curriculumVitaeUri,
        string? curriculumVitaeFileName,
        string? videoUri,
        string? videoFileName,
        IEnumerable<CandidateIndustry> industries,
        IEnumerable<CandidateSkill> skills,
        IEnumerable<CandidateDesiredSkill> desiredSkills,
        IEnumerable<CandidateLanguage> languages,
        IEnumerable<CandidateCourse> courses,
        IEnumerable<CandidateEducation> educations,
        IEnumerable<CandidateWorkExperience> workExperiences,
        IEnumerable<CandidateHobby> hobbies,
        ICollection<FormatType> formats,
        ICollection<WorkType> workTypes,
        List<Guid> fileCacheIds)
    {
        Email = Email.CreateNullable(email);
        Status = CandidateStatus.Approved;
        FirstName = firstName;
        LastName = lastName;
        if (picturePaths != null)
        {
            Picture = new Image(picturePaths);
        }
        if (currentPositionId.HasValue && !string.IsNullOrWhiteSpace(currentPositionCode))
        {
            CurrentPosition = new Position(
                currentPositionId.Value,
                currentPositionCode,
                currentPositionAliasId,
                currentPositionAliasCode);
        }
        BirthDate = birthDate;
        OpenForOpportunities = openForOpportunities;
        LinkedInUrl = linkedInUrl;
        PersonalWebsiteUrl = personalWebsiteUrl;
        YearsOfExperience = yearsOfExperience;
        Bio = bio;
        CurriculumVitae = new Document(curriculumVitaeUri, curriculumVitaeFileName);
        Video = new Document(videoUri, videoFileName);

        if (!string.IsNullOrWhiteSpace(addressLine))
        {
            Address = new Address(
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude);
        }

        Terms = new Terms(
            workTypes,
            freelanceHourlySalary,
            freelanceMonthlySalary,
            permanentMonthlySalary,
            startDate,
            endDate,
            currency,
            workingHourTypes,
            weeklyWorkHours,
            formats);

        Phone = new Phone(phoneCountryCode, phoneNumber);
        _industries.AddRange(industries);
        _skills.AddRange(skills);
        _desiredSkills.AddRange(desiredSkills);
        _languages.AddRange(languages);
        _courses.AddRange(courses);
        _educations.AddRange(educations);
        _workExperiences.AddRange(workExperiences);
        _hobbies.AddRange(hobbies);

        ValidateInitialization();

        if (fileCacheIds.Count > 0)
        {
            AddEvent(new CandidateFilesAddedDomainEvent(fileCacheIds, CreatedAt));
        }
        AddEvent(new CandidateCreatedDomainEvent(this, CreatedAt));
    }

    public void Update(
        string? email,
        string? firstName,
        string? lastName,
        Guid? currentPositionId,
        string? currentPositionCode,
        Guid? currentPositionAliasToId,
        string? currentPositionAliasToCode,
        DateTimeOffset? birthDate,
        bool openForOpportunities,
        string? linkedInUrl,
        string? personalWebsiteUrl,
        string? addressLine,
        string? city,
        string? country,
        string? postalCode,
        double? longitude,
        double? latitude,
        DateTimeOffset? startDate,
        DateTimeOffset? endDate,
        int? weeklyWorkHours,
        string? currency,
        decimal? freelanceHourlySalary,
        decimal? freelanceMonthlySalary,
        decimal? permanentMonthlySalary,
        ICollection<WorkingHoursType>? workingHourTypes,
        string? phoneCountryCode,
        string? phoneNumber,
        int? yearsOfExperience,
        string? bio,
        string? curriculumVitaeUri,
        string? curriculumVitaeFileName,
        bool isCurriculumVitaeChanged,
        string? videoUri,
        string? videoFileName,
        bool isVideoChanged,
        Dictionary<ImageType, string?>? picturePaths,
        bool isPictureChanged,
        IEnumerable<ActivityStatus> activityStatuses,
        IEnumerable<CandidateIndustry> industries,
        IEnumerable<CandidateSkill> skills,
        IEnumerable<CandidateDesiredSkill> desiredSkills,
        IEnumerable<CandidateLanguage> languages,
        IEnumerable<CandidateHobby> hobbies,
        ICollection<FormatType> formats,
        ICollection<WorkType> workTypes,
        List<Guid> fileCacheIds)
    {
        ValidateIfAllowedToModify();
        if (email != Email?.Address)
        {
            Email = Email.CreateNullable(email);
        }
       
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        OpenForOpportunities = openForOpportunities;
        LinkedInUrl = linkedInUrl;
        PersonalWebsiteUrl = personalWebsiteUrl;
        YearsOfExperience = yearsOfExperience;

        if (currentPositionId.HasValue && !string.IsNullOrWhiteSpace(currentPositionCode))
        {
            CurrentPosition = new Position(
                currentPositionId.Value,
                currentPositionCode,
                currentPositionAliasToId,
                currentPositionAliasToCode);
        }
        else
        {
            CurrentPosition = null;
        }

        if (!string.IsNullOrWhiteSpace(addressLine))
        {
            Address = new Address(
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude);
        }

        Terms = new Terms(
            workTypes,
            freelanceHourlySalary,
            freelanceMonthlySalary,
            permanentMonthlySalary,
            startDate,
            endDate,
            currency,
            workingHourTypes,
            weeklyWorkHours,
            formats);

        Phone = new Phone(phoneCountryCode, phoneNumber);
        Bio = bio;
        if (isCurriculumVitaeChanged)
        {
            CurriculumVitae = new Document(curriculumVitaeUri, curriculumVitaeFileName);
        }
        if (isVideoChanged)
        {
            Video = new Document(videoUri, videoFileName);
        }
        if (isPictureChanged)
        {
            Picture = picturePaths != null ? new Image(picturePaths) : null;
        }

        _activityStatuses.Calibrate(activityStatuses, Id);
        _industries.Calibrate(industries, Id);
        _skills.Calibrate(skills, Id);
        _desiredSkills.Calibrate(desiredSkills, Id);
        _languages.Calibrate(languages, Id);
        _hobbies.Calibrate(hobbies, Id);

        ValidateUpdate();

        if (fileCacheIds.Count > 0)
        {
            AddEvent(new CandidateFilesAddedDomainEvent(fileCacheIds, DateTimeOffset.UtcNow));
        }
        AddEvent(new CandidateUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void UpdateOpenForOpportunities(bool openForOpportunities)
    {
        OpenForOpportunities = openForOpportunities;
    }

    public void AddCourse(
        string name,
        string issuingOrganization, 
        string? description,
        string? certificateUri,
        string? certificateFileName,
        Guid? certificateCacheId)
    {
        ValidateIfAllowedToModify();

        var course = new CandidateCourse(
            Id,
            name,
            issuingOrganization,
            description,
            certificateUri,
            certificateFileName);

        _courses.Add(course);

        if (certificateCacheId.HasValue)
        {
            AddEvent(new CandidateFilesAddedDomainEvent(
                new List<Guid>() { certificateCacheId.Value }, 
                DateTimeOffset.UtcNow));
        }

        AddEvent(new CandidateUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void UpdateCourse(
        Guid courseId,
        string name,
        string issuingOrganization,
        string? description,
        string? certificateUri,
        string? certificateFileName,
        bool isCertificateChanged,
        Guid? certificateCacheId)
    {
        ValidateIfAllowedToModify();

        var course = GetCourse(courseId);
        course.Update(
            name,
            issuingOrganization,
            description, 
            certificateUri,
            certificateFileName,
            isCertificateChanged);

        if (isCertificateChanged && certificateCacheId.HasValue)
        {
            AddEvent(new CandidateFilesAddedDomainEvent(
                new List<Guid>() { certificateCacheId.Value },
                DateTimeOffset.UtcNow));
        }

        AddEvent(new CandidateUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void DeleteCourse(Guid courseId)
    {
        ValidateIfAllowedToModify();

        var course = GetCourse(courseId);
        _courses.Remove(course);

        AddEvent(new CandidateUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void AddEducation(
        string schoolName,
        string degree,
        string fieldOfStudy,
        DateTimeOffset from,
        DateTimeOffset? to,
        string? studiesDescription,
        bool isStillStudying,
        string? certificateUri,
        string? certificateFileName,
        Guid? certificateCacheId)
    {
        ValidateIfAllowedToModify();

        var education = new CandidateEducation(
            Id,
            schoolName,
            degree,
            fieldOfStudy,
            from,
            to,
            studiesDescription,
            isStillStudying,
            certificateUri,
            certificateFileName);

        _educations.Add(education);

        if (certificateCacheId.HasValue)
        {
            AddEvent(new CandidateFilesAddedDomainEvent(
                new List<Guid>() { certificateCacheId.Value },
                DateTimeOffset.UtcNow));
        }

        AddEvent(new CandidateUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void UpdateEducation(
        Guid educationId,
        string schoolName,
        string degree,
        string fieldOfStudy,
        DateTimeOffset from,
        DateTimeOffset? to,
        string? studiesDescription,
        bool isStillStudying,
        string? certificateUri,
        string? certificateFileName,
        bool isCertificateChanged,
        Guid? certificateCacheId)
    {
        ValidateIfAllowedToModify();

        var education = GetEducation(educationId);
        education.Update(
            schoolName,
            degree,
            fieldOfStudy,
            from,
            to,
            studiesDescription, 
            isStillStudying,
            certificateUri,
            certificateFileName,
            isCertificateChanged);

        if (isCertificateChanged && certificateCacheId.HasValue)
        {
            AddEvent(new CandidateFilesAddedDomainEvent(
                new List<Guid>() { certificateCacheId.Value },
                DateTimeOffset.UtcNow));
        }

        AddEvent(new CandidateUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void DeleteEducation(Guid educationId)
    {
        ValidateIfAllowedToModify();

        var education = GetEducation(educationId);
        _educations.Remove(education);

        AddEvent(new CandidateUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void AddWorkExperience(
        WorkExperienceType type,
        string companyName,
        Guid positionId,
        string positionCode,
        Guid? positionAliasToId,
        string? positionAliasToCode,
        DateTimeOffset from,
        DateTimeOffset? to,
        string? jobDescription,
        bool isCurrentJob,
        IEnumerable<CandidateWorkExperienceSkill> skills)
    {
        ValidateIfAllowedToModify();

        var workExperience = new CandidateWorkExperience();
        workExperience.Create(
            Id,
            type,
            companyName,
            positionId,
            positionCode,
            positionAliasToId,
            positionAliasToCode,
            from,
            to,
            jobDescription, 
            isCurrentJob,
            skills);

        _workExperiences.Add(workExperience);

        AddEvent(new CandidateUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void UpdateWorkExperience(
        Guid workExperienceId,
        WorkExperienceType type,
        string companyName,
        Guid positionId,
        string positionCode,
        Guid? positionsAliasToId,
        string? positionsAliasToCode,
        DateTimeOffset from,
        DateTimeOffset? to,
        string? jobDescription,
        bool isCurrentJob,
        IEnumerable<CandidateWorkExperienceSkill> skills)
    {
        ValidateIfAllowedToModify();

        var workExperience = GetWorkExperience(workExperienceId);
        workExperience.Update(
            type, 
            companyName, 
            positionId, 
            positionCode,
            positionsAliasToId,
            positionsAliasToCode,
            from,
            to,
            jobDescription, 
            isCurrentJob, 
            skills);

        AddEvent(new CandidateUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void DeleteWorkExperience(Guid workExperienceId)
    {
        ValidateIfAllowedToModify();

        var workExperience = GetWorkExperience(workExperienceId);
        _workExperiences.Remove(workExperience);

        AddEvent(new CandidateUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void ToggleIsShortlisted(bool isShortlisted)
    {
        IsShortListed = isShortlisted;
    }

    public void ToggleIsHired(bool isHired)
    {
        IsHired = isHired;
    }

    public void PublishCandidateUpdatedEvent()
    {
        AddEvent(new CandidateUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void VerifyEmail(Guid key, int expirationInMinutes)
    {
        ValidateIfEmailExist();
        Email!.ValidateVerification(key, expirationInMinutes);
        Email = Email.CreateVerified(Email.Address);
    }

    public void RequestEmailVerification()
    {
        ValidateIfEmailExist();
        Email = Email.CreateWithVerification(Email!.Address);
        AddEvent(new CandidateEmailVerificationRequestedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void UpdateNote(string? note, DateTimeOffset? endDate)
    {
        Note = Note.Create(note, endDate);
    }

    public void Approve()
    {
        if (Status != CandidateStatus.Pending)
        {
            throw new DomainException(
                $"Only pending candidate can be approved. Current candidate status: {Status}", 
                ErrorCodes.Candidate.ApproveNotAllowed);
        }

        Status = CandidateStatus.Approved;
        AddEvent(new CandidateApprovedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void Reject()
    {
        if (Status != CandidateStatus.Pending)
        {
            throw new DomainException(
                $"Only pending candidate can be rejected. Current candidate status: {Status}",
                ErrorCodes.Candidate.RejectNotAllowed);
        }

        Status = CandidateStatus.Rejected;
        RejectedAt = DateTimeOffset.UtcNow;

        AddEvent(new CandidateRejectedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void CompleteCoreInformation()
    {
        ValidateIfEmailExist();
        ValidateName();
        ValidateWorkType();
        ValidateActivityStatus();
        ValidateWorkExperience();

        if (Status == CandidateStatus.Registered)
        {
            Status = CandidateStatus.Pending;
        }
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

    public void SyncSkills(Guid id, Guid? aliasId, string? aliasCode)
    {
        var skill = _skills.SingleOrDefault(x => x.SkillId == id);
        if (skill is not null)
        {
            skill.Sync(aliasId, aliasCode);
        }

        var desiredSkill = _desiredSkills.SingleOrDefault(x => x.SkillId == id);
        if (desiredSkill is not null)
        {
            desiredSkill.Sync(aliasId, aliasCode);
        }

        var workExpierences = _workExperiences.Where(x => x.Skills.Any(s => s.SkillId == id));
        foreach (var workExperience in workExpierences)
        {
            workExperience.SyncSkill(id, aliasId, aliasCode);
        }

        AddEvent(new CandidateSkillsSyncedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void SyncJobPositions(Guid id, Guid? aliasId, string? aliasCode)
    {
        if (CurrentPosition != null && CurrentPosition.Id == id)
        {
            CurrentPosition = new Position(id, CurrentPosition.Code, aliasId, aliasCode);
        }

        var workExperiences = _workExperiences.Where(x => x.Position.Id == id);
        foreach (var workExperience in workExperiences)
        {
            workExperience.SyncJobPosition(aliasId, aliasCode);
        }

        AddEvent(new CandidateJobPositionSyncedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    private CandidateCourse GetCourse(Guid id)
    {
        var course = _courses.Where(x => x.Id == id).SingleOrDefault();

        if (course is null)
        {
            throw new DomainException($"Course was not fond, course id: {id}",
                ErrorCodes.Candidate.CourseNotFound);
        }

        return course;
    }

    private CandidateEducation GetEducation(Guid id)
    {
        var education = _educations.Where(x => x.Id == id).SingleOrDefault();

        if (education is null)
        {
            throw new DomainException($"Education was not fond, education id: {id}",
                ErrorCodes.Candidate.EducationNotFound);
        }

        return education;
    }

    private CandidateWorkExperience GetWorkExperience(Guid id)
    {
        var workExperience = _workExperiences.Where(x => x.Id == id).SingleOrDefault();

        if (workExperience is null)
        {
            throw new DomainException($"Work experience was not fond, work experiece id: {id}",
                ErrorCodes.Candidate.WorkExperienceNotFound);
        }

        return workExperience;
    }

    private void ValidateInitialization()
    {
        ValidateIdentification();
        ValidateName();
        ValidateWorkType();
        ValidateBirthDate();
    }

    private void ValidateUpdate()
    {
        ValidateIdentification();
        ValidateBirthDate();
    }

    private void ValidateName()
    {
        if (string.IsNullOrWhiteSpace(FirstName))
        {
            throw new DomainException("First name is required", ErrorCodes.Candidate.FirstNameRequired);
        }

        if (string.IsNullOrWhiteSpace(LastName))
        {
            throw new DomainException("Last name name is required", ErrorCodes.Candidate.LastNameRequired);
        }
    }

    private void ValidateWorkType()
    {
        if (Terms.Freelance is null && Terms.Permanent is null)
        {
            throw new DomainException("Work type is required", ErrorCodes.Candidate.WorkTypeRequired);
        }
    }

    private void ValidateIdentification()
    {
        if (string.IsNullOrWhiteSpace(Email?.Address) &&
            string.IsNullOrWhiteSpace(LinkedInUrl))
        {
            throw new DomainException("Email address or linkedIn is requred", ErrorCodes.Candidate.EmailOrLinkedInRequired);
        }
    }

    private void ValidateIfAllowedToModify()
    {
        var isForbiddenToModify = Status == CandidateStatus.Registered &&
                                (Email is not null && !Email.IsVerified);

        if (isForbiddenToModify)
        {
            throw new DomainException(
                "Modification is forbidden for not verified candidate.",
                ErrorCodes.Candidate.ModificationForbiddenForNotVerified);
        }
    }

    private void ValidateIfCandidateLinked()
    {
        if (ExternalId.HasValue && ExternalId != Guid.Empty)
        {
            throw new DomainException("Candidate is already linked", ErrorCodes.Candidate.AlreadyLinked);
        }
    }

    private void ValidateIfEmailExist()
    {
        if (Email is null)
        {
            throw new DomainException("Email of candidate not found.", ErrorCodes.Candidate.EmailNotFound);
        }
    }

    private void ValidateActivityStatus()
    {
        if (ActivityStatuses.Count == 0)
        {
            throw new DomainException("Activity status is required", ErrorCodes.Candidate.ActivityStatusRequired);
        }
    }

    private void ValidateWorkExperience()
    {
        var isInValidWorkExperience =
            (ActivityStatuses.Any(x => x.ActivityStatus == ActivityStatus.Freelancer) ||
            ActivityStatuses.Any(x => x.ActivityStatus == ActivityStatus.Permanent))
            && WorkExperiences.Count == 0;

        if (isInValidWorkExperience)
        {
            throw new DomainException("Work experience is required when activity statuses have freelancer or permanet",
                ErrorCodes.Candidate.WorkExperienceNotFound);
        }
    }

    private void ValidateBirthDate()
    {
        if (BirthDate.HasValue &&
            BirthDate.Value.Date >= DateTimeOffset.Now.Date)
        {
            throw new DomainException("Birth date is invalid", ErrorCodes.Candidate.BirthDateInvalid);
        }
    }
}
