using API.Customization.Pagination;
using FrontOffice.Bff.API.Areas.Jobs.Models;
using FrontOffice.Bff.API.Areas.Jobs.Models.ElasticSearch.Responses;

namespace FrontOffice.Bff.API.Infrastructure.Extensions
{
    public static class JobsPagedResponseExtensions
    {
        public static void CopyResponseDataFrom(
            this PagedResponse<GetRecommendedJobResponse> jobs,
            PagedResponse<JobElasticSearchResponse> jobsElasticSearchResult)
        {
            var existSearchServiceJobs = DoesExistSearchServiceJobs(jobsElasticSearchResult);

            if (jobs.Data is null || !existSearchServiceJobs)
            {
                return;
            }

            jobs.Count = jobsElasticSearchResult.Count;
        }

        public static void OrderByScore(
            this PagedResponse<GetRecommendedJobResponse> jobs,
            PagedResponse<JobElasticSearchResponse> searchServiceJobs)
        {
            var existSearchServiceJobs = DoesExistSearchServiceJobs(searchServiceJobs);

            if (jobs.Data is null || !existSearchServiceJobs)
            {
                return;
            }

            foreach (var job in jobs.Data)
            {
                job.Score = searchServiceJobs
                    .Data?
                    .FirstOrDefault(x => x.Id == job.JobId)?.Score ?? 0;
            }

            jobs.Data = jobs
                .Data
                .OrderByDescending(x => x.Score);
        }

        private static bool DoesExistSearchServiceJobs(PagedResponse<JobElasticSearchResponse> jobs) =>
            jobs.Data is not null && jobs.Data.Any();
    }
}
