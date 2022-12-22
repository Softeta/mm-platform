namespace Domain.Seedwork.Exceptions
{
    public static class ErrorCodes
    {
        public static class BadRequest 
        {
            public const string CandidateCollectionEmpty = "BadRequest.CandidateCollectionEmpty";
            public const string CandidateMustRegisterBeforeDoingTest = "BadRequest.CandidateMustRegisterBeforeDoingTest";
            public const string OneOrMoreCandidateNotExists = "BadRequest.OneOrMoreCandidateNotExists";
            public const string OneOrMoreAssignedEmployeeNotExist = "BadRequest.OneOrMoreAssignedEmployeeNotExist";
            public const string OwnerNotExists = "BadRequest.OwnerNotExists";
            public const string OwnerIsRequired = "BadRequest.OwnerIsRequired";
            public const string ContactPersonNotExists = "BadRequest.ContactPersonNotExists";
            public const string SuitableAddressDetailsNotFound = "BadRequest.SuitableAddressDetailsNotFound";
            public const string SuitableCityNotFound = "BadRequest.SuitableCityNotFound";
            public const string AddressLineRequired = "BadRequest.AddressLineRequired";
            public const string InvalidPostalCode = "BadRequest.InvalidPostalCode";
            public const string FileExtensionIsNotAllowed = "BadRequest.FileExtensionIsNotAllowed";
            public const string FileMaxSizeExceeded = "BadRequest.MaxSizeExceeded";
            public const string ContactPersonAlreadyExists = "BadRequest.ContactPersonAlreadyExists";
            public const string PostalCodeRequired = "BadRequest.PostalCodeRequired";
            public const string NoEmailInClaims = "BadRequest.NoEmailInClaims";
        }
        public static class NotFound
        {
            public const string CandidateNotFound = "NotFound.CandidateNotFound";
            public const string FileNotFound = "NotFound.FileNotFound";
            public const string CandidateCourseNotFound = "NotFound.CandidateCourseNotFound";
            public const string CandidateEducationNotFound = "NotFound.CandidateEducationNotFound";
            public const string ContactPersonNotFound = "NotFound.ContactPersonNotFound";
            public const string EmailNotFound = "NotFound.EmailNotFound";
            public const string EmailVerificationKeyNotFound = "NotFound.EmailVerificationKeyNotFound";
            public const string EmailLinkNotFound = "NotFound.EmailLinkNotFound";
            public const string TestsPackageNotFound = "NotFound.TestsPackageNotFound";
            public const string CandidateJobsNotFound = "NotFound.CandidateJobsNotFound";
            public const string CandidateSelectedInJobNotFound = "NotFound.CandidateSelectedInJobNotFound";
            public const string TagSystemResultNotFound = "NotFound.TagSystemResultNotFound";
            public const string JobNotFound = "NotFound.JobNotFound";
            public const string JobCandidatesNotFound = "NotFound.JobCandidatesNotFound";
            public const string CompanyNotFound = "NotFound.CompanyNotFound";
            public const string JobContactPersonByEmailNotFound = "NotFound.JobContactPersonByEmailNotFound";
            public const string CandidatesFilterNotFound = "NotFound.CandidatesFilterNotFound";
            public const string ShortlistedCandidateNotFound = "NotFound.ShortlistedCandidateNotFound";
        }

        public static class HttpRequest
        {
            public const string DasnishCvrApiException = "HttpRequest.DasnishCvrApiException";
            public const string HereApiException = "HttpRequest.HereApiException";
            public const string LocalApiException = "HttpRequest.LocalApiException";
            public const string TalogyAuthApiException = "HttpRequest.TalogyAuthApiException";
            public const string TalogyApiException = "HttpRequest.TalogyApiException";
            public const string TagSystemApiException = "HttpRequest.TagSystemApiException";
        }
        public static class Forbidden
        {
            public const string CandidateEditForbidden = "Forbidden.CandidateEditForbidden";
            public const string AccessOtherCompaniesData = "Forbidden.AccessOtherCompaniesData";
            public const string CompanyServiceAccessDenied = "Forbidden.CompanyServiceAccessDenied";
            public const string CandidateServiceAccessDenied = "Forbidden.CandidateServiceAccessDenied";
        }

        public static class Conflict
        {
            public const string LogicalTestAlreadyExists = "Conflict.LogicalTestAlreadyExists";
            public const string PersonalityTestAlreadyExists = "Conflict.PersonalityTestAlreadyExists";
            public const string LogicalTestCanNotCreate = "Conflict.LogicalTestCanNotCreate";
            public const string CandidateAlreadyExists = "Conflict.CandidateAlreadyExists";
            public const string CandidateLinkedInAlreadyExists = "Conflict.CandidateLinkedInAlreadyExists";
            public const string TestPackageNotCompleted = "Conflict.TestPackageNotCompleted";
            public const string TestAlreadyCompleted = "Conflict.TestAlreadyCompleted";
        }

        public static class Shared
        {
            public static class DateRange
            {
                public const string FromRequired = "Shared.DateRange.FromRequired";
                public const string FromGreaterThanTo = "Shared.DateRange.FromGreaterThanTo";
            }
            public static class Email
            {
                public const string AlreadyVerified = "Shared.Email.AlreadyVerified";
                public const string VerificationKeyNotFound = "Shared.Email.VerificationKeyNotFound";
                public const string VerificationKeyNotValid = "Shared.Email.VerificationKeyNotValid";
                public const string VerificationKeyExpired = "Shared.Email.VerificationKeyExpired";
                public const string Invalid = "Shared.Email.Invalid";
            }
            public static class Image
            {
                public const string OriginalPathNotFound = "Shared.Image.OriginalPathNotFound";
                public const string ThumbnailPathNotFound = "Shared.Image.ThumbnailPathNotFound";
            }
            public static class Phone
            {
                public const string CountryCodeMaxLengthExceeded = "Shared.Phone.CountryCodeMaxLengthExceeded";
                public const string NumberMaxLengthExceeded = "Shared.Phone.NumberMaxLengthExceeded";
                public const string CountryCodeNotFound = "Shared.Phone.CountryCodeNotFound";
                public const string NumberNotFound = "Shared.Phone.NumberNotFound";
                public const string NumberTooShort = "Shared.Phone.NumberTooShort";
            }
        }
        public static class Candidate
        {
            public const string AlreadyLinked = "Candidate.AlreadyLinked";
            public const string EmailNotFound = "Candidate.EmailNotFound";
            public const string ModificationForbiddenForNotVerified = "Candidate.ModificationForbiddenForNotVerified";
            public const string CourseNotFound = "Candidate.CourseNotFound";
            public const string EducationNotFound = "Candidate.EducationNotFound";
            public const string EmailOrLinkedInRequired = "Candidate.EmailOrLinkedInRequired";
            public const string FirstNameRequired = "Candidate.FirstNameRequired";
            public const string LastNameRequired = "Candidate.LastNameRequired";
            public const string WorkExperienceNotFound = "Candidate.WorkExperienceNotFound";
            public const string WorkTypeRequired = "Candidate.WorkTypeRequired";
            public const string ApproveNotAllowed = "Candidate.ApproveNotAllowed";
            public const string RejectNotAllowed = "Candidate.RejectNotAllowed";
            public const string ActivityStatusRequired = "Candidate.ActivityStatusRequired";
            public const string LinkedInUrlInvalid = "Candidate.LinkedInUrlInvalid";
            public const string PersonalWebsiteUrlInvalid = "Candidate.PersonalWebsiteUrlInvalid";
            public const string BirthDateInvalid = "Candidate.BirthDateInvalid";
            public const string StartDateInvalid = "Candidate.StartDateInvalid";
            public const string EndDateInvalid = "Candidate.EndDateInvalid";

            public static class Course
            {
                public const string NameRequired = "Candidate.Course.NameRequired";
                public const string IssuingOrganizationRequired = "Candidate.Course.IssuingOrganizationRequired";
            }
            public static class Education
            {
                public const string FieldOfStudyRequired = "Candidate.Education.FieldOfStudyRequired";
                public const string SchoolNameRequired = "Candidate.Education.SchoolNameRequired";
                public const string DegreeRequired = "Candidate.Education.DegreeRequired";
            }
            public static class WorkExperience
            {
                public const string CompanyNameRequired = "Candidate.WorkExperience.CompanyNameRequired";
                public const string PositionRequired = "Candidate.WorkExperience.PositionRequired";
            }
            public static class Jobs
            {
                public const string JobAlreadyAdded = "Candidate.Job.IsAlreadyAdded";
                public const string JobAlreadyArchived = "Candidate.Job.IsAlreadyArchived";
                public const string JobForbbidenToSync = "Candidate.Job.ForbbidenToSync";
                public const string SelectedInJobNotFound = "Candidate.Job.SelectedInJobNotFound";
                public const string ApplyNotAllowedForArchivedJob = "Candidate.Job.ApplyNotAllowedForArchivedJob";
                public const string AlreadyApplied = "Candidate.Job.AlreadyApplied";
            }
        }

        public static class Company
        {
            public const string AdminMandatory = "Company.AdminMandatory";
            public const string OneContactPersonMandatory = "Company.OneContactPersonMandatory";
            public const string ContactPersonNotFound = "Company.ContactPersonNotFound";
            public const string CompanyNotFoundByEmail = "Company.CompanyNotFoundByEmail";
            public const string OtherContactPersonAdminMustExist = "Company.OtherContactPersonAdminMustExist";
            public const string ApproveNotAllowed = "Company.ApproveNotAllowed";
            public const string RejectNotAllowed = "Company.RejectNotAllowed";
            public const string AddContactPersonNotAllowed = "Company.AddContactPersonNotAllowed";

            public static class ContactPerson
            {
                public const string AlreadyLinked = "Company.ContactPerson.AlreadyLinked";
                public const string InvalidCompanyId = "Company.ContactPerson.InvalidCompanyId";
                public const string AlreadyRejected = "Company.ContactPerson.AlreadyRejected";
                public const string ApproveNotAllowed = "Company.ContactPerson.ApproveNotAllowed";
                public const string CanNotVerifyOtherContact = "Company.ContactPerson.CanNotVerifyOtherContact";
            }
        }

        public static class Job
        {
            public const string WorkTypeRequired = "Job.WorkTypeRequired";
            public const string InvalidStage = "Job.InvalidStage";
            public const string InterestedCandidatesMaxCountExceeded = "Job.InterestedCandidatesMaxCountExceeded";
            public const string InterestedLinkedInCandidatesMaxCountExceeded = "Job.InterestedLinkedInCandidatesMaxCountExceeded";
            public const string ApproveNotAllowed = "Job.ApproveNotAllowed";
            public const string RejectNotAllowed = "Job.RejectNotAllowed";
            public const string OwnerRequired = "Job.OwnerRequired";
            public const string StartDateInvalid = "Job.StartDateInvalid";
            public const string DeadlineDateInvalid = "Job.DeadlineDateInvalid";
            public const string SkillRequired = "Job.SkillRequired";
            public const string SeniorityRequired = "Job.SeniorityRequired";
            public const string WorkingHoursRequired = "Job.WorkingHoursRequired";
            public const string StartDateRequired = "Job.StartDateRequired";
            public const string FormatRequired = "Job.FormatRequired";
            public const string LocationRequired = "Job.LocationRequired";
            public const string DescriptionRequired = "Job.DescriptionRequired";

            public static class ContactPerson
            {
                public const string EmailRequired = "Job.ContactPerson.EmailRequired";
            }
            public static class Company
            {
                public const string OneMainContactPersonMandatory = "Job.Company.OneMainContactPersonMandatory";
                public const string InvalidId = "Job.Company.InvalidId";
            }
            public static class Candidate
            {
                public const string NotExistsInArchivedCandidates = "Job.Candidate.NotExistsInArchivedCandidates";
                public const string NotExistsInSelectedCandidates = "Job.Candidate.NotExistsInSelectedCandidates";
                public const string NotExists = "Job.Candidate.NotExists";
                public const string ShortlistedMandatory = "Job.Candidate.ShortlistedMandatory";
                public const string OnlyOneHired = "Job.Candidate.OnlyOneHired";
                public const string JobHasHired = "Job.Candidate.JobHasHired";
                public const string DuplicatedRankNotAllowed = "Job.Candidate.RankingDuplicationNotAllowed";
                public const string EmailNotExists = "Job.Candidate.EmailNotExists";
            }
            public static class ArchivedCandidate
            {
                public const string CandidateIdRequired = "Job.ArchivedCandidate.CandidateIdRequired";
                public const string EmailRequired = "Job.ArchivedCandidate.EmailRequired";
                public const string FirstNameRequired = "Job.ArchivedCandidate.FirstNameRequired";
                public const string LastNameRequired = "Job.ArchivedCandidate.LastNameRequired";
            }
            public static class SelectedCandidate
            {
                public const string CandidateIdRequired = "Job.SelectedCandidate.CandidateIdRequired";
                public const string EmailRequired = "Job.SelectedCandidate.EmailRequired";
                public const string FirstNameRequired = "Job.SelectedCandidate.FirstNameRequired";
                public const string LastNameRequired = "Job.SelectedCandidate.LastNameRequired";
                public const string InvalidRankingValue = "Job.Candidate.InvalidRankingValue";
                public const string RankingUpdateNotAllowed = "Job.Candidate.RankingUpdateNotAllowed";
            }
        }

        public static class Filters
        {
            public static class Candidates
            {
                public const string MaxCandidatesFilterCount = "Filters.Candidates.MaxCandidatesFilterCount";
            }
        }
    }
}
