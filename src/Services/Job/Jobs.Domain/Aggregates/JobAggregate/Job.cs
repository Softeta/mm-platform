using Domain.Seedwork;
using Domain.Seedwork.Consts;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;
using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Jobs.Domain.Aggregates.JobAggregate.Services.Calibrate;
using Jobs.Domain.Aggregates.JobAggregate.ValueObjects;
using Jobs.Domain.Events.JobAggregate;

namespace Jobs.Domain.Aggregates.JobAggregate;

public class Job : AggregateRoot
{
    public Company Company { get; private set; } = null!;

    public Employee? Owner { get; private set; } = null!;

    public Position Position { get; private set; } = null!;

    public YearExperience? YearExperience { get; private set; }

    public DateTimeOffset? DeadlineDate { get; private set; }

    public string? Description { get; private set; }

    public JobStage Stage { get; private set; }

    public Sharing? Sharing { get; private set; }

    public Terms? Terms { get; private set; }

    public bool IsPriority { get; private set; }

    public string? Location { get; private set; }

    public bool IsArchived { get; private set; }

    public bool IsActivated { get; private set; }

    public bool IsPublished { get; private set; }

    public bool IsSelectionStarted { get; private set; }

    public Guid? ParentJobId { get; private set; }

    private readonly List<JobAssignedEmployee> _assignedEmployees = new();
    public IReadOnlyList<JobAssignedEmployee> AssignedEmployees => _assignedEmployees;

    private readonly List<JobSkill> _skills = new();
    public IReadOnlyList<JobSkill> Skills => _skills;

    private readonly List<JobIndustry> _industries = new();
    public IReadOnlyList<JobIndustry> Industries => _industries;

    private readonly List<JobSeniority> _seniorityLevels = new();
    public IReadOnlyList<JobSeniority> SeniorityLevels => _seniorityLevels;

    private readonly List<JobLanguage> _languages = new();
    public IReadOnlyList<JobLanguage> Languages => _languages;

    private readonly List<JobInterestedCandidate> _interestedCandidates = new();
    public IReadOnlyList<JobInterestedCandidate> InterestedCandidates => _interestedCandidates;

    private readonly List<JobInterestedLinkedIn> _interestedLinkedIns = new();
    public IReadOnlyList<JobInterestedLinkedIn> InterestedLinkedIns => _interestedLinkedIns;

    public Job()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void Initialize(
        Company company,
        Guid positionId,
        string positionName,
        Guid? positionAliasToId,
        string? positionAliasToCode,
        DateTimeOffset? startDate,
        DateTimeOffset? endDate,
        ICollection<WorkType> workTypes,
        bool isUrgent)
    {
        Stage = JobStage.Pending;
        Company = company;
        Position = new Position(positionId, positionName, positionAliasToId, positionAliasToCode);

        Terms = new Terms(
            isUrgent,
            startDate,
            endDate,
            null,
            workTypes,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            new List<WorkingHoursType>(),
            new List<FormatType>());

        SetLocation();

        Validate();
        AddEvent(new JobInitializedDomainEvent(this, CreatedAt));
    }

    public void Create(
        Company company,
        Employee owner,
        Guid positionId,
        string positionCode,
        Guid? positionAliasToId,
        string? positionAliasToCode,
        DateTimeOffset? deadLineDate,
        string? description,
        DateTimeOffset? startDate,
        DateTimeOffset? endDate,
        int? weeklyWorkHours,
        string? currency,
        int? hoursPerProject,
        decimal? freelanceHourlySalaryFrom,
        decimal? freelanceHourlySalaryTo,
        decimal? freelanceMonthlySalaryFrom,
        decimal? freelanceMonthlySalaryTo,
        decimal? permanentMonthlySalaryFrom,
        decimal? permanentMonthlySalaryTo,
        int? yearExperienceFrom,
        int? yearExperienceTo,
        bool isPriority,
        bool isUrgent,
        ICollection<WorkingHoursType> workingHourTypes,
        ICollection<WorkType> workTypes,
        IEnumerable<JobAssignedEmployee> assignedEmployees,
        IEnumerable<JobSkill> skills,
        IEnumerable<JobIndustry> industries,
        IEnumerable<JobSeniority> seniorities,
        IEnumerable<JobLanguage> languages,
        ICollection<FormatType> formats,
        IEnumerable<JobInterestedCandidate> interestedCandidates,
        IEnumerable<string> interestedLinkedIns)
    {
        Stage = JobStage.Calibration;
        Company = company;
        Owner = owner;
        Position = new Position(positionId, positionCode, positionAliasToId, positionAliasToCode);
        DeadlineDate = deadLineDate;
        Description = description;
        IsPriority = isPriority;
        YearExperience = YearExperience.Create(yearExperienceFrom, yearExperienceTo);

        Terms = new Terms(
            isUrgent,
            startDate,
            endDate,
            currency,
            workTypes,
            hoursPerProject,
            weeklyWorkHours,
            freelanceHourlySalaryFrom,
            freelanceHourlySalaryTo,
            freelanceMonthlySalaryFrom,
            freelanceMonthlySalaryTo,
            permanentMonthlySalaryFrom,
            permanentMonthlySalaryTo,
            workingHourTypes,
            formats);

        SetLocation();

        _assignedEmployees.AddRange(assignedEmployees);
        _skills.AddRange(skills);
        _industries.AddRange(industries);
        _seniorityLevels.AddRange(seniorities);
        _languages.AddRange(languages);
        _interestedCandidates.AddRange(interestedCandidates);
        _interestedLinkedIns.AddRange(interestedLinkedIns.Select(x => new JobInterestedLinkedIn(Id, x)));

        Validate();

        AddEvent(new JobCreatedDomainEvent(this, CreatedAt));
    }

    public void CreateFromJob(Job job)
    {
        Stage = JobStage.Calibration;
        
        Company = new Company(
            job.Company.Id,
            job.Company.Status,
            job.Company.Name,
            job.Company.Address?.AddressLine,
            job.Company.Address?.City,
            job.Company.Address?.Country,
            job.Company.Address?.PostalCode,
            job.Company.Address?.Coordinates?.Longitude,
            job.Company.Address?.Coordinates?.Latitude,
            job.Company.Description,
            job.Company.LogoUri,
            job.Company.ContactPersons.Select(cp => new JobContactPerson(
                Id,
                cp.Id,
                cp.IsMainContact,
                cp.FirstName,
                cp.LastName,
                cp.PhoneNumber,
                cp.Email,
                cp.Position?.Id,
                cp.Position?.Code,
                cp.Position?.AliasTo?.Id,
                cp.Position?.AliasTo?.Code,
                cp.PictureUri,
                cp.SystemLanguage,
                cp.ExternalId
                ))
        );
        
        Owner = job.Owner?.GetCopy();
        Position = job.Position.GetCopy();
        DeadlineDate = job.DeadlineDate;
        Description = job.Description;
        IsPriority = job.IsPriority;
        YearExperience = job.YearExperience?.GetCopy();

        if (job.Terms is not null)
        {
            Terms = new Terms(job.Terms);
        }

        ParentJobId = job.Id;

        SetLocation();

        _assignedEmployees.AddRange(job.AssignedEmployees.Select(ae => new JobAssignedEmployee(
            Id, ae.Employee.Id, ae.Employee.FirstName, ae.Employee.LastName, ae.Employee.PictureUri
        )));
        _skills.AddRange(job.Skills.Select(s => new JobSkill(s.Id, Id, s.Code, s.AliasTo?.Id, s.AliasTo?.Code)));
        _industries.AddRange(job.Industries.Select(i => new JobIndustry(i.Id, Id, i.Code)));
        _seniorityLevels.AddRange(job.SeniorityLevels.Select(sl => new JobSeniority(Id, sl.Seniority)));
        _languages.AddRange(job.Languages.Select(l => new JobLanguage(Id, l.Id, l.Language.Code, l.Language.Name)));
        _interestedCandidates.AddRange(job.InterestedCandidates.Select(
            ic => new JobInterestedCandidate(Id, ic.CandidateId, ic.FirstName, ic.LastName, ic.Position, ic.PictureUri)));
        _interestedLinkedIns.AddRange(job.InterestedLinkedIns.Select(il => new JobInterestedLinkedIn(Id, il.Url)));

        Validate();

        AddEvent(new JobCreatedDomainEvent(this, CreatedAt));
    }

    public void Update(
        Employee? owner,
        Guid positionId,
        string positionCode,
        Guid? positionAliasToId,
        string? positionAliasToCode,
        DateTimeOffset? deadLineDate,
        string? description,
        DateTimeOffset? startDate,
        DateTimeOffset? endDate,
        int? weeklyWorkHours,
        string? currency,
        int? hoursPerProject,
        decimal? freelanceHourlySalaryFrom,
        decimal? freelanceHourlySalaryTo,
        decimal? freelanceMonthlySalaryFrom,
        decimal? freelanceMonthlySalaryTo,
        decimal? permanentMonthlySalaryFrom,
        decimal? permanentMonthlySalaryTo,
        int? yearExperienceFrom,
        int? yearExperienceTo,
        bool isPriority,
        bool isUrgent,
        IEnumerable<WorkingHoursType> workingHourTypes,
        IEnumerable<WorkType> workTypes,
        IEnumerable<JobAssignedEmployee> assignedEmployees,
        IEnumerable<JobSkill> skills,
        IEnumerable<JobIndustry> industries,
        IEnumerable<JobSeniority> seniorities,
        IEnumerable<JobLanguage> languages,
        IEnumerable<FormatType> formats,
        IEnumerable<JobInterestedCandidate> interestedCandidates,
        IEnumerable<string> interestedLinkedIns)
    {
        Owner = owner;
        Position = new Position(positionId, positionCode, positionAliasToId, positionAliasToCode);
        DeadlineDate = deadLineDate;
        Description = description;
        IsPriority = isPriority;
        YearExperience = YearExperience.Create(yearExperienceFrom, yearExperienceTo);

        Terms = new Terms(
            isUrgent,
            startDate,
            endDate,
            currency,
            workTypes,
            hoursPerProject,
            weeklyWorkHours,
            freelanceHourlySalaryFrom,
            freelanceHourlySalaryTo,
            freelanceMonthlySalaryFrom,
            freelanceMonthlySalaryTo,
            permanentMonthlySalaryFrom,
            permanentMonthlySalaryTo,
            workingHourTypes,
            formats);

        _skills.Calibrate(skills, Id);
        _industries.Calibrate(industries, Id);
        _assignedEmployees.Calibrate(assignedEmployees, Id);
        _languages.Calibrate(languages, Id);
        _interestedCandidates.Calibrate(interestedCandidates, Id);
        _interestedLinkedIns.Calibrate(interestedLinkedIns, Id);
        _seniorityLevels.Calibrate(seniorities, Id);

        if (IsPublished)
        {
            ValidatePublished();
        }
        else
        {
            Validate();
        }

        AddEvent(new JobUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void ShareViaEmail(string email)
    {
        Sharing = InitializeSharing();

        var askedForJobApproval = new AskedForJobApprovalDomainEvent
        {
            JobId = Id,
            RouteKey = Sharing.Key,
            ReceiverEmail = email,
            ShareDate = Sharing.Date
        };

        AddEvent(askedForJobApproval);
    }

    public void ShareViaLink()
    {
        Sharing = InitializeSharing();
    }

    public void UpdateCompany(Company company)
    {
        Company = company;
        SetLocation();
    }

    public void SyncCompany(string name, CompanyStatus status, string? logoUri)
    {
        var company = new Company(
                Company.Id,
                status,
                name,
                Company.Address?.AddressLine,
                Company.Address?.City,
                Company.Address?.Country,
                Company.Address?.PostalCode,
                Company.Address?.Coordinates?.Longitude,
                Company.Address?.Coordinates?.Latitude,
                Company.Description,
                logoUri,
                Company.ContactPersons.Select(x => x.Clone()));

        Company = company;
    }

    public void SyncContactPerson(
        Guid personId, 
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
        var contactPerson = Company.ContactPersons.FirstOrDefault(x => x.PersonId == personId);

        if (contactPerson is null) return;

        contactPerson.Sync(
            firstName,
            lastName,
            email,
            phoneNumber,
            pictureUri,
            positionId,
            positionCode,
            positionAliasToId,
            positionAliasToCode,
            systemLanguage);
    }

    public void Publish()
    {
        IsPublished = true;
        ValidatePublished();
        AddEvent(new JobPublishedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void StartSearchAndSelection()
    {
        if (Stage != JobStage.Calibration)
        {
            throw new DomainException($"Invalid job stage. current '{Stage}' of job. Search and selection cannot be activated.",
                ErrorCodes.Job.InvalidStage,
                new string[] { Stage.ToString() });
        }

        Stage = JobStage.CandidateSelection;
        IsSelectionStarted = true;
        AddEvent(new JobSearchAndSelectionStartedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void ShortlistSent()
    {
        Stage = JobStage.ShortListed;
    }

    public void UpdateJobOwner(string firstName, string lastName, string? pictureUri)
    {
        Owner = new Employee(Owner!.Id, firstName, lastName, pictureUri);
    }

    public void UpdateAssignedEmployee(Guid employeeId, string firstName, string lastName, string? pictureUri)
    {
        var assignedEmployee = AssignedEmployees.SingleOrDefault(x => x.Employee.Id == employeeId);

        assignedEmployee?.Update(firstName, lastName, pictureUri);
    }

    public void Hire()
    {
        if (IsArchived || Stage == JobStage.Successful)
        {
            return;
        }

        IsArchived = true;
        Stage = JobStage.Successful;
    }

    public void Archive(JobStage stage)
    {
        var archiveStages = new[] { JobStage.Lost, JobStage.OnHold };

        if (IsArchived || !archiveStages.Contains(stage))
        {
            return;
        }

        var prevStage = Stage;
        IsArchived = true;
        Stage = stage;

        if (prevStage == JobStage.Calibration)
        {
            return;
        }

        AddEvent(new JobArchivedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void Activate()
    {
        var activationAllowedStages = new[] { JobStage.Successful, JobStage.OnHold };
        if (IsActivated)
        {
            return;
        }

        if (!IsArchived || !activationAllowedStages.Contains(Stage))
        {
            throw new DomainException($"Invalid job stage. Job with '{Stage}' stage cannot be activated.",
                ErrorCodes.Job.InvalidStage,
                new [] { Stage.ToString() });
        }

        IsActivated = true;
    }

    public void Approve()
    {
        if (Stage != JobStage.Pending)
        {
            throw new DomainException(
                $"Only pending jobs can be approved. Current job stage: {Stage}",
                ErrorCodes.Job.ApproveNotAllowed);
        }

        Stage = JobStage.Calibration;

        Validate();

        AddEvent(new JobApprovedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void Reject()
    {
        if (Stage != JobStage.Pending)
        {
            throw new DomainException(
                $"Only pending jobs can be rejected. Current job stage: {Stage}",
                ErrorCodes.Job.RejectNotAllowed);
        }

        AddEvent(new JobRejectedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void SyncSkill(Guid id, Guid? aliasId, string? aliasCode)
    {
        var skill = _skills.SingleOrDefault(x => x.SkillId == id);
        if (skill is not null)
        {
            skill.Sync(aliasId, aliasCode);
        }

        AddEvent(new JobSkillSyncedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void SyncJobPositions(Guid id, Guid? aliasId, string? aliasCode)
    {
        if (Position.Id == id)
        {
            Position = new Position(id, Position.Code, aliasId, aliasCode);
        }
        
        var contactPersons = Company.ContactPersons.Where(x => x.Position != null && x.Position.Id == id);
        foreach (var contactPerson in contactPersons)
        {
            contactPerson.SyncPosition(aliasId, aliasCode);
        }

        AddEvent(new JobPositionSyncedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    public void SyncContactPersonExternalId(Guid coontactPersonId, Guid? externalId)
    {
        var contactPersons = Company.ContactPersons.Where(x => x.PersonId == coontactPersonId);

        foreach (var contactPerson in contactPersons)
        {
            contactPerson.SyncExternalId(externalId);
        }
    }

    public void PublishJobUpdatedEvent()
    {
        AddEvent(new JobUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
    }

    private Sharing InitializeSharing()
    {
        if (Sharing is null)
        {
            return new Sharing();
        }
        return new Sharing(Sharing.Key);
    }

    private void Validate()
    {
        if (Stage != JobStage.Pending && Owner is null)
        {
            throw new DomainException($"Owner is required", ErrorCodes.Job.OwnerRequired);
        }
        if (Terms?.Freelance is null && Terms?.Permanent is null)
        {
            throw new DomainException($"At least one work type is required",
                ErrorCodes.Job.WorkTypeRequired);
        }
        if (_interestedCandidates.Count > JobConsts.MaxInterestedCandidateCount)
        {
            throw new DomainException($"Job interested candidates max count exceeded. Max count: {JobConsts.MaxInterestedCandidateCount}",
                ErrorCodes.Job.InterestedCandidatesMaxCountExceeded, new[] { JobConsts.MaxInterestedCandidateCount.ToString() });
        }
        if (_interestedLinkedIns.Count > JobConsts.MaxInterestedCandidateCount)
        {
            throw new DomainException($"Job interested linkedIns max count exceeded. Max count: {JobConsts.MaxInterestedCandidateCount}",
                ErrorCodes.Job.InterestedLinkedInCandidatesMaxCountExceeded, new[] { JobConsts.MaxInterestedCandidateCount.ToString() });
        }
        if (DeadlineDate.HasValue && DeadlineDate.Value.Date <= DateTimeOffset.UtcNow.Date)
        {
            throw new DomainException($"Deadline date cannot be today or earlier", ErrorCodes.Job.DeadlineDateInvalid);
        }
    }

    private void ValidatePublished()
    {
        Validate();

        if (!_skills.Any())
        {
            throw new DomainException($"At least one skill is required", ErrorCodes.Job.SkillRequired);
        }
        if (!_seniorityLevels.Any())
        {
            throw new DomainException($"At least one seniority is required", ErrorCodes.Job.SeniorityRequired);
        }
        if(Terms?.PartTimeWorkingHours is null && Terms?.FullTimeWorkingHours is null && Terms?.PartTimeWorkingHours is null)
        {
            throw new DomainException($"At least one working hours type is required", ErrorCodes.Job.WorkingHoursRequired);
        }
        if (Terms?.Availability?.From is null)
        {
            throw new DomainException($"Start date is required", ErrorCodes.Job.StartDateRequired);
        }
        if (!Terms.Formats.IsRemote && !Terms.Formats.IsOnSite && !Terms.Formats.IsHybrid)
        {
            throw new DomainException($"At least one format is required", ErrorCodes.Job.FormatRequired);
        }
        if (string.IsNullOrWhiteSpace(Location))
        {
            throw new DomainException($"Location is required", ErrorCodes.Job.LocationRequired);
        }
        if (string.IsNullOrWhiteSpace(Description))
        {
            throw new DomainException($"Description is required", ErrorCodes.Job.DescriptionRequired);
        }
    }

    private void SetLocation()
    {
        if (Company.Address is null) return;
        Location = $"{Company.Address.City} ({Company.Address.Country})";
    }
}
