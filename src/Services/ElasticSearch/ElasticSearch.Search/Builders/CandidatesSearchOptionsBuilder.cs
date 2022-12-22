using ElasticSearch.Search.Constants;

namespace ElasticSearch.Search.Builders
{
    internal class CandidatesSearchOptionsBuilder : SearchOptionsBuilderBase
    {
        public CandidatesSearchOptionsBuilder()
        {
        }

        public CandidatesSearchOptionsBuilder WithPageSize(int pageSize)
        {
            _pageSize = pageSize;

            return this;
        }
        public CandidatesSearchOptionsBuilder WithPageNumber(int pageNumber)
        {
            _pageNumber = pageNumber;

            return this;
        }

        public CandidatesSearchOptionsBuilder AddJobId(string? jobId)
        {
            if (jobId is null) return this;

            AddFilteringAnd();
            _filter.Append($"{CandidatesSearchParams.JobIds}/all(id: id ne '{jobId}')");

            return this;
        }
    }
}
