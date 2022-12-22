namespace BackOffice.Bff.API.Infrastructure.Constants
{
    internal class Endpoints
    {
        public static class JobService
        {
            public const string UpdateJob = "api/v1/jobs/{0}";
            public const string UpdateJobCompany = "api/v1/jobs/{0}/company";
            public const string Jobs = "api/v1/jobs";
            public const string Job = "api/v1/jobs/{0}";
            public const string AddSelectedCandidates = "api/v1/job-candidates/{0}/selected-candidates";
        }

        public static class CompanyService
        {
            public const string Company = "api/v1/companies/{0}";
            public const string GetPagedContactPersons = "api/v1/companies/{0}/contact-persons?pageNumber={1}&pageSize={2}{3}";
        }

        public static class CandidateService
        {
            public const string Candidates = "api/v1/candidates";
        }

        public static class ElasticSearchService
        {
            public const string CandidatesSearch = "api/suggested-candidates";
        }
    }
}
