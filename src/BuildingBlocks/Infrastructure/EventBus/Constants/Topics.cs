namespace EventBus.Constants
{
    public static class Topics
    {
        public static class BackOfficeUserChanged
        {
            public const string Name = "backofficeuser_changed";

            public static class Filters
            {
                public const string BackOfficeUserUpdated = "BackOfficeUserUpdated";
                public const string BackOfficeUserDeleted = "BackOfficeUserDeleted";
            }

            public static class Subscribers
            {
                public const string JobService = "JobService";

                public static readonly (string subscriptionName, string[] filterNames) JobServiceFilters = (JobService, new[]
                {
                    Filters.BackOfficeUserUpdated,
                    Filters.BackOfficeUserDeleted
                });
            }
        }

        public static class CandidateChanged
        {
            public const string Name = "candidate_changed";
            public static class Filters
            {
                public const string CandidateCreated = "CandidateCreated";
                public const string CandidateRegistered = "CandidateRegistered";
                public const string CandidateUpdated = "CandidateUpdated";
                public const string CandidateApproved = "CandidateApproved";
                public const string CandidateRejected = "CandidateRejected";
                public const string CandidateEmailVerificationRequested = "CandidateEmailVerificationRequested";
                public const string CandidateSkillsSynced = "CandidateSkillsSynced";
                public const string CandidateJobPositionSynced = "CandidateJobPositionSynced";
            }

            public static class Subscribers
            {
                public const string JobService = "JobService";
                public const string EmailService = "EmailService";
                public const string ElasticSearch = "ElasticSearch";
                public const string CandidateService = "CandidateService";
                public const string EmailServiceSync = "EmailServiceSync";

                public static readonly (string subscriptionName, string[] filterNames) JobServiceFilters = (JobService, new[]
                {
                    Filters.CandidateUpdated
                });

                public static readonly (string subscriptionName, string[] filterNames) EmailServiceFilters = (EmailService, new[]
                {
                    Filters.CandidateEmailVerificationRequested,
                    Filters.CandidateRegistered,
                    Filters.CandidateApproved,
                    Filters.CandidateRejected
                });

                public static readonly (string subscriptionName, string[] filterNames) EmailServiceSyncFilters = (EmailServiceSync, new[]
                {
                    Filters.CandidateCreated,
                    Filters.CandidateUpdated,
                    Filters.CandidateRejected,
                });

                public static readonly (string subscriptionName, string[] filterNames) ElasticSearchFilters = (ElasticSearch, new[]
                {
                    Filters.CandidateCreated,
                    Filters.CandidateUpdated,
                    Filters.CandidateApproved,
                    Filters.CandidateRejected,
                    Filters.CandidateSkillsSynced,
                    Filters.CandidateJobPositionSynced
                });

                public static readonly (string subscriptionName, string[] filterNames) CandidateServiceFilters = (CandidateService, new[]
                {
                    Filters.CandidateRejected
                });
            }
        }

        public static class CandidateJobsChanged
        {
            public const string Name = "candidatejobs_changed";

            public static class Filters
            {
                public const string CandidateJobsArchived = "CandidateJobsArchived";
                public const string CandidateJobsUpdated = "CandidateJobsUpdated";
                public const string CandidateJobsAdded = "CandidateJobsAdded";
                public const string CandidateJobsShortlisted = "CandidateJobsShortlisted";
                public const string CandidateJobsUnshortlisted = "CandidateJobsUnshortlisted";
                public const string CandidateJobsHired = "CandidateJobsHired";
                public const string CandidateJobRejected = "CandidateJobRejected";
                public const string CandidateAppliedToJob = "CandidateAppliedToJob";
            }

            public static class Subscribers
            {
                public const string ElasticSearch = "ElasticSearch";
                public const string JobService = "JobService";
                public const string EmailService = "EmailService";

                public static readonly (string subscriptionName, string[] filterNames) JobServiceFilters = (JobService, new[]
                {
                    Filters.CandidateJobsShortlisted,
                    Filters.CandidateJobsUnshortlisted,
                    Filters.CandidateJobsHired,
                    Filters.CandidateJobRejected,
                    Filters.CandidateAppliedToJob
                });

                public static readonly (string subscriptionName, string[] filterNames) ElasticSearchFilters = (ElasticSearch, new[]
                {
                    Filters.CandidateJobsArchived,
                    Filters.CandidateJobsUpdated,
                    Filters.CandidateJobsAdded,
                    Filters.CandidateJobsHired,
                    Filters.CandidateJobRejected
                });

                public static readonly (string subscriptionName, string[] filterNames) EmailServiceFilters = (EmailService, new[]
                {
                    Filters.CandidateAppliedToJob
                });
            }
        }

        public static class CompanyChanged
        {
            public const string Name = "company_changed";

            public static class Filters
            {
                public const string CompanyCreated = "CompanyCreated";
                public const string CompanyUpdated = "CompanyUpdated";
                public const string CompanyDeleted = "CompanyDeleted";
                public const string CompanyApproved = "CompanyApproved";
                public const string CompanyRejected = "CompanyRejected";
            }

            public static class Subscribers
            {
                public const string JobService = "JobService";
                public const string EmailService = "EmailService";
                public const string CompanyService = "CompanyService";

                public static readonly (string subscriptionName, string[] filterNames) JobServiceFilters = (JobService, new[]
                {
                    Filters.CompanyUpdated,
                    Filters.CompanyDeleted,
                    Filters.CompanyRejected,
                    Filters.CompanyApproved
                });

                public static readonly (string subscriptionName, string[] filterNames) EmailServiceFilters = (EmailService, new[]
                {
                    Filters.CompanyCreated,
                    Filters.CompanyApproved,
                    Filters.CompanyRejected
                });

                public static readonly (string subscriptionName, string[] filterNames) CompanyServiceFilters = (CompanyService, new[]
                {
                    Filters.CompanyRejected
                });
            }
        }

        public static class ContactPersonChanged
        {
            public const string Name = "contactperson_changed";

            public static class Filters
            {
                public const string ContactPersonRegistered = "ContactPersonRegistered";
                public const string ContactPersonLinked = "ContactPersonLinked";
                public const string ContactPersonAdded = "ContactPersonAdded";
                public const string ContactPersonUpdated = "ContactPersonUpdated";
                public const string ContactPersonEmailVerificationRequested = "ContactPersonEmailVerificationRequested";
                public const string ContactPersonRejected = "ContactPersonRejected";
            }

            public static class Subscribers
            {
                public const string EmailService = "EmailService";
                public const string JobService = "JobService";
                public const string CompanyService = "CompanyService";
                public const string EmailServiceSync = "EmailServiceSync";

                public static readonly (string subscriptionName, string[] filterNames) EmailServiceFilters = (EmailService, new[]
                {
                    Filters.ContactPersonRegistered,
                    Filters.ContactPersonAdded,
                    Filters.ContactPersonEmailVerificationRequested
                });

                public static readonly (string subscriptionName, string[] filterNames) JobServiceFilters = (JobService, new[]
                {
                    Filters.ContactPersonUpdated,
                    Filters.ContactPersonLinked
                });

                public static readonly (string subscriptionName, string[] filterNames) CompanyServiceFilters = (CompanyService, new[]
                {
                    Filters.ContactPersonRejected
                });

                public static readonly (string subscriptionName, string[] filterNames) EmailServiceSyncFilters = (EmailServiceSync, new[]
                {
                    Filters.ContactPersonAdded,
                    Filters.ContactPersonRegistered,
                    Filters.ContactPersonUpdated,
                    Filters.ContactPersonRejected,
                    Filters.ContactPersonLinked
                });
            }
        }

        public static class JobChanged
        {
            public const string Name = "job_changed";

            public static class Filters
            {
                public const string JobCreated = "JobCreated";
                public const string JobUpdated = "JobUpdated";
                public const string JobArchived = "JobArchived";
                public const string JobApproved = "JobApproved";
                public const string JobRejected = "JobRejected";
                public const string JobPublished = "JobPublished";
                public const string JobSkillSynced = "JobSkillSynced";
                public const string JobPositionSynced = "JobPositionSynced";
                public const string JobInitialized = "JobInitialized";
            }

            public static class Subscribers
            {
                public const string CompanyService = "CompanyService";
                public const string EmailService = "EmailService";
                public const string ElasticSearch = "ElasticSearch";

                public static readonly (string subscriptionName, string[] filterNames) CompanyServiceFilters = (CompanyService, new[]
                {
                    Filters.JobApproved
                });

                public static readonly (string subscriptionName, string[] filterNames) EmailServiceFilters = (EmailService, new[]
{
                    Filters.JobApproved,
                    Filters.JobRejected,
                    Filters.JobInitialized
                });

                public static readonly (string subscriptionName, string[] filterNames) ElasticSearchFilters = (ElasticSearch, new[]
                {
                    Filters.JobUpdated,
                    Filters.JobArchived,
                    Filters.JobPublished,
                    Filters.JobApproved,
                    Filters.JobSkillSynced,
                    Filters.JobPositionSynced
                });
            }
        }

        public static class JobCandidatesChanged
        {
            public const string Name = "jobcandidates_changed";

            public static class Filters
            {
                public const string JobSelectedCandidatesAdded = "JobSelectedCandidatesAdded";
                public const string JobSelectedCandidatesUpdated = "JobSelectedCandidatesUpdated";
                public const string JobArchivedCandidatesChanged = "JobArchivedCandidatesChanged";
                public const string JobCandidatesInformationUpdated = "JobCandidatesInformationUpdated";
                public const string JobCandidatesHired = "JobCandidatesHired";
                public const string JobCandidatesJobStageUpdated = "JobCandidatesJobStageUpdated";
                public const string JobSelectedCandidateInvited = "JobSelectedCandidateInvited";
                public const string JobCandidatesSharedShortlistViaEmail = "JobCandidatesSharedShortlistViaEmail";
                public const string JobCandidatesShortlisted = "JobCandidatesShortlisted";
            }

            public static class Subscribers
            {
                public const string CandidateService = "CandidateService";
                public const string EmailService = "EmailService";
                public const string ElasticSearch = "ElasticSearch";

                public static readonly (string subscriptionName, string[] filterNames) CandidateServiceFilters = (CandidateService, new[]
                {
                    Filters.JobSelectedCandidatesAdded,
                    Filters.JobSelectedCandidatesUpdated,
                    Filters.JobArchivedCandidatesChanged,
                    Filters.JobCandidatesInformationUpdated,
                    Filters.JobCandidatesHired,
                    Filters.JobCandidatesJobStageUpdated,
                    Filters.JobCandidatesShortlisted
                });

                public static readonly (string subscriptionName, string[] filterNames) EmailServiceFilters = (EmailService, new[]
{
                    Filters.JobSelectedCandidateInvited,
                    Filters.JobCandidatesSharedShortlistViaEmail
                });

                public static readonly (string subscriptionName, string[] filterNames) ElasticSearchFilters = (ElasticSearch, new[]
                {
                    Filters.JobSelectedCandidatesUpdated
                });
            }
        }

        public static class JobShareChanged
        {
            public const string Name = "job_share_changed";

            public static class Filters
            {
                public const string AskedForJobApproval = "AskedForJobApproval";
            }

            public static class Subscribers
            {
                public const string EmailService = "EmailService";

                public static readonly (string subscriptionName, string[] filterNames) EmailServiceFilters = (EmailService, new[]
                {
                    Filters.AskedForJobApproval
                });
            }
        }

        public static class EmailServiceWebHooked
        {
            public const string Name = "email_service_web_hooked";

            public static class Filters
            {
                public const string AskedForJobApprovalWebHook = "AskedForJobApprovalWebHook";
                public const string CandidateVerificationWebHook = "CandidateVerificationWebHook";
                public const string CandidateApprovedWebHook = "CandidateApprovedWebHook";
                public const string CandidateRejectedWebHook = "CandidateRejectedWebHook";
            }

            public static class Subscribers
            {
                public const string JobService = "JobService";
                public const string CandidateService = "CandidateService";

                public static readonly (string subscriptionName, string[] filterNames) JobServiceFilters = (JobService, new[]
                {
                    Filters.AskedForJobApprovalWebHook
                });

                public static readonly (string subscriptionName, string[] filterNames) CandidateServiceFilters = (CandidateService, new[]
{
                    Filters.CandidateVerificationWebHook
                });
            }
        }

        public static class SchedulerJobScheduled
        {
            public const string Name = "schedulerjob_scheduled";

            public static class Filters
            {
                public const string DeleteCandidatesScheduled = "DeleteCandidatesScheduled";
                public const string SyncRegistryCenterCompaniesScheduled = "SyncRegistryCenterCompaniesScheduled";
                public const string RemoveExpiredFileCacheScheduled = "RemoveExpiredFileCacheScheduled";
            }

            public static class Subscribers
            {
                public const string CandidateService = "CandidateService";
                public const string CompanyService = "CompanyService";

                public static readonly (string subscriptionName, string[] filterNames) CandidateServiceFilters = (CandidateService, new[]
                {
                    Filters.DeleteCandidatesScheduled,
                    Filters.RemoveExpiredFileCacheScheduled
                });

                public static readonly (string subscriptionName, string[] filterNames) CompanyServiceFilters = (CompanyService, new[]
                {
                    Filters.SyncRegistryCenterCompaniesScheduled,
                    Filters.RemoveExpiredFileCacheScheduled
                });
            }
        }

        public static class SkillChanged
        {
            public const string Name = "skill_changed";

            public static class Filters
            {
                public const string SkillMerged = "SkillMerged";
                public const string SkillUnmerged = "SkillUnmerged";
            }

            public static class Subscribers
            {
                public const string CandidateService = "CandidateService";
                public const string JobService = "JobService";

                public static readonly (string subscriptionName, string[] filterNames) CandidateServiceFilters = (CandidateService, new[]
                {
                    Filters.SkillMerged,
                    Filters.SkillUnmerged
                });

                public static readonly (string subscriptionName, string[] filterNames) JobServiceFilters = (JobService, new[]
                {
                    Filters.SkillMerged,
                    Filters.SkillUnmerged
                });
            }
        }

        public static class JobPositionChanged
        {
            public const string Name = "jobposition_changed";

            public static class Filters
            {
                public const string JobPositionMerged = "JobPositionMerged";
                public const string JobPositionUnmerged = "JobPositionUnmerged";
            }

            public static class Subscribers
            {
                public const string CandidateService = "CandidateService";
                public const string JobService = "JobService";
                public const string CompanyService = "CompanyService";

                public static readonly (string subscriptionName, string[] filterNames) CandidateServiceFilters = (CandidateService, new[]
                {
                    Filters.JobPositionMerged,
                    Filters.JobPositionUnmerged
                });

                public static readonly (string subscriptionName, string[] filterNames) JobServiceFilters = (JobService, new[]
                {
                    Filters.JobPositionMerged,
                    Filters.JobPositionUnmerged
                });

                public static readonly (string subscriptionName, string[] filterNames) CompanyServiceFilters = (CompanyService, new[]
                {
                    Filters.JobPositionMerged,
                    Filters.JobPositionUnmerged
                });
            }
        }
    }
}
