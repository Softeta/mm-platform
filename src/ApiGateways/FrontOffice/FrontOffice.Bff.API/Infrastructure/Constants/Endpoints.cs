namespace FrontOffice.Bff.API.Infrastructure.Constants
{
    internal class Endpoints
    {
        public static class CandidateService
        {
            public const string Candidate = "api/v1/candidates/{0}";
            public const string WorkExperiences = "api/v1/candidates/{0}/work-experiences";
            public const string WorkExperienceById = "api/v1/candidates/{0}/work-experiences/{1}";
            public const string CoreInformationCompleted = "api/v1/candidates/{0}/core-information/completed";
            public const string ApplyToJob = "api/v1/candidates/{0}/applied-jobs/{1}";
            public const string GetPagedAppliedJobs = "api/v1/candidates/{0}/applied-jobs?pageNumber={1}&pageSize={2}";
        }

        public static class CompanyService
        {
            public const string Base = "api/v1/companies";
            public const string Registered = $"{Base}/registered";
            public const string Company = "api/v1/companies/{0}";
        }

        public static class JobService
        {
            public const string Base = "api/v1/jobs";
            public const string Initialization = $"{Base}/initialization";
            public const string Job = "api/v1/jobs/{0}";
            public const string Company = "api/v1/jobs/{0}/company";
        }

        public static class ElasticSearchService
        {
            public const string JobsSearch = "api/suggested-jobs";
        }
    }
}
