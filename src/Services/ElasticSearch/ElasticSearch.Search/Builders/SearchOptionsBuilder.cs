using Azure.Search.Documents;

namespace ElasticSearch.Search.Builders
{
    internal class SearchOptionsBuilder
    {
        private int _pageSize;
        private int _pageNumber;
        private string? _filter;
        private string? _orderBy;

        public SearchOptionsBuilder WithPageSize(int pageSize)
        {
            _pageSize = pageSize;

            return this;
        }

        public SearchOptionsBuilder WithPageNumber(int pageNumber)
        {
            _pageNumber = pageNumber;

            return this;
        }

        public SearchOptionsBuilder WithFilter(string filter)
        {
            _filter = filter;

            return this;
        }

        public SearchOptionsBuilder WithOrdering(string? orderByField)
        {
            _orderBy = orderByField;

            return this;
        }

        public SearchOptions Build()
        {
            var options = new SearchOptions
            {
                Filter = _filter,
                Size = _pageSize,
                Skip = (_pageNumber - 1) * _pageSize,
                IncludeTotalCount = true
            };

            if (!string.IsNullOrEmpty(_orderBy))
            {
                options.OrderBy.Add(_orderBy);
            }

            return options;
        }
    }
}
