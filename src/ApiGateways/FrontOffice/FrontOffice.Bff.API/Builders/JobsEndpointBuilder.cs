using System.Text;

namespace FrontOffice.Bff.API.Builders
{
    public class JobsEndpointBuilder
    {
        private string _endpoint = null!;
        private int _pageNumber;
        private int _pageSize;
        private ICollection<Guid> _jobs = new List<Guid>();

        public JobsEndpointBuilder ForEndpoint(string endpoint)
        {
            _endpoint = endpoint;
            return this;
        }

        public JobsEndpointBuilder WithAsFirstPage()
        {
            _pageNumber = 1;

            return this;
        }

        public JobsEndpointBuilder WithPageSize(int pageSize)
        {
            _pageSize = pageSize;

            return this;
        }

        public JobsEndpointBuilder WithJobs(ICollection<Guid> jobs)
        {
            _jobs = jobs;

            return this;
        }

        public string Build()
        {
            var queryParams = new StringBuilder();

            queryParams
                .Append(_endpoint)
                .Append('?')
                .Append("PageNumber").Append('=').Append(_pageNumber)
                .Append('&')
                .Append("PageSize").Append('=').Append(_pageSize)
                .Append('&');

            AppendFilterCollection(_jobs, "JobIds", queryParams);

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
