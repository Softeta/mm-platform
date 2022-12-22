using System.Text;

namespace BackOffice.Bff.API.Builders
{
    public class CandidatesEndpointBuilder
    {
        private string _endpoint = null!;
        protected int PageNumber { get; private set; }
        protected int PageSize { get; private set; }

        protected ICollection<Guid> Candidates { get; private set; } = null!;

        public CandidatesEndpointBuilder ForEndpoint(string endpoint)
        {
            _endpoint = endpoint;
            return this;
        }

        public CandidatesEndpointBuilder WithAsFirstPage()
        {
            PageNumber = 1;

            return this;
        }

        public CandidatesEndpointBuilder WithPageSize(int pageSize)
        {
            PageSize = pageSize;

            return this;
        }

        public CandidatesEndpointBuilder WithCandidates(ICollection<Guid> candidates)
        {
            Candidates = candidates;
            return this;
        }

        public string Build()
        {
            var queryParams = new StringBuilder();

            queryParams
                .Append(_endpoint)
                .Append('?')
                .Append(nameof(PageNumber)).Append('=').Append(PageNumber)
                .Append('&')
                .Append(nameof(PageSize)).Append('=').Append(PageSize)
                .Append('&');

            AppendFilterCollection(Candidates, nameof(Candidates), queryParams);

            return queryParams.ToString();
        }

        private static void AppendFilterCollection<T>(ICollection<T> collection, string collectionName, StringBuilder queryParams)
        {
            foreach (var item in collection)
            {
                queryParams
                    .Append(collectionName)
                    .Append('=')
                    .Append(item)
                    .Append('&');
            }
        }
    }
}
