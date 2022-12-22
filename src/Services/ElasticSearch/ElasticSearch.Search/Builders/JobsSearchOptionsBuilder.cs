using ElasticSearch.Search.Constants;
using System.Collections.Generic;
using System.Linq;

namespace ElasticSearch.Search.Builders
{
    public class JobsSearchOptionsBuilder : SearchOptionsBuilderBase
    {
        public JobsSearchOptionsBuilder()
        {
        }

        public JobsSearchOptionsBuilder WithPageSize(int pageSize)
        {
            _pageSize = pageSize;

            return this;
        }

        public JobsSearchOptionsBuilder WithPageNumber(int pageNumber)
        {
            _pageNumber = pageNumber;

            return this;
        }

        public JobsSearchOptionsBuilder WithCandidateId(string? candidateId)
        {
            if (candidateId is null) return this;

            AddFilteringAnd();
            _filter.Append($"{JobsSearchParams.AppliedCandidateIds}/all(id: id ne '{candidateId}')");

            return this;
        }

        public JobsSearchOptionsBuilder WithStages(ICollection<string>? stages)
        {
            if (!stages?.Any() ?? false) return this;

            _filter.Append($"and ({JobsSearchParams.Stage} eq '{string.Join($"' or {JobsSearchParams.Stage} eq '", stages!)}')");

            return this;
        }

        public JobsSearchOptionsBuilder WithIsPublished(bool? isPublished)
        {
            if (!isPublished.HasValue) return this;

            AddFilteringAnd();
            _filter.Append($"{JobsSearchParams.IsPublished} eq {isPublished.Value.ToString().ToLower()}");

            return this;
        }
    }
}
