using Azure.Search.Documents;
using System.Text;

namespace ElasticSearch.Search.Builders
{
    public abstract class SearchOptionsBuilderBase
    {
        protected StringBuilder _filter;
        protected int _pageSize;
        protected int _pageNumber;

        protected SearchOptionsBuilderBase()
        {
            _filter = new StringBuilder();
        }

        protected void AddFilteringAnd()
        {
            if (_filter.Length != 0)
            {
                _filter.Append(" and ");
            }
        }

        public SearchOptions Build()
        {
            return new SearchOptionsBuilder()
                .WithPageSize(_pageSize)
                .WithPageNumber(_pageNumber)
                .WithFilter(_filter.ToString())
                .Build();
        }
    }
}
