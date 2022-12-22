using API.Customization.Pagination;
using BackOffice.Bff.API.Models.Candidate.Response;
using BackOffice.Bff.API.Models.ElasticSearch.Responses;

namespace BackOffice.Bff.API.Infrastructure.Extensions
{
    public static class CandidatesPagedResponseExtensions
    {
        public static void CopyResponseDataFrom(
            this PagedResponse<GetCandidateResponse> candidates,
            PagedResponse<CandidateElasticSearchResponse> searchServiceCandidates)
        {
            var existSearchServiceCandidates = DoesExistSearchServiceCandidates(searchServiceCandidates);

            if (candidates.Data is null || !existSearchServiceCandidates)
            {
                return;
            }

            candidates.Count = searchServiceCandidates.Count;
        }

        public static void OrderByScore(
            this PagedResponse<GetCandidateResponse> candidates,
            PagedResponse<CandidateElasticSearchResponse> searchServiceCandidates)
        {
            var existSearchServiceCandidates = DoesExistSearchServiceCandidates(searchServiceCandidates);

            if (candidates.Data is null || !existSearchServiceCandidates)
            {
                return;
            }

            foreach (var candidate in candidates.Data)
            {
                candidate.Score = searchServiceCandidates
                    .Data?
                    .FirstOrDefault(x => x.Id == candidate.Id)?.Score ?? 0;
            }

            candidates.Data = candidates
                .Data
                .OrderByDescending(x => x.Score);
        }

        private static bool DoesExistSearchServiceCandidates(PagedResponse<CandidateElasticSearchResponse> candidates) =>
            candidates.Data is not null && candidates.Data.Any();
    }
}
