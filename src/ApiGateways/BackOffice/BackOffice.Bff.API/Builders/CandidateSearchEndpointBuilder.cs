using Domain.Seedwork.Enums;
using System.Text;

namespace BackOffice.Bff.API.Builders
{
    public class CandidateSearchEndpointBuilder
    {
        private string _functionName = null!;
        private string _functionKey = null!;
        private string? _queryString;
        private string _jobId = string.Empty;
        private string _status = string.Empty;

        public CandidateSearchEndpointBuilder WithFunctionName(string name)
        {
            _functionName = name;

            return this;
        }

        public CandidateSearchEndpointBuilder WithFunctionKey(string functionKey)
        {
            _functionKey = functionKey;

            return this;
        }

        public CandidateSearchEndpointBuilder WithQueryStrings(string? queryString)
        {
            _queryString = queryString;

            return this;
        }

        public CandidateSearchEndpointBuilder WithJobId(Guid? jobId)
        {
            if (!jobId.HasValue) return this;

            _jobId = jobId.Value.ToString();

            return this;
        }

        public CandidateSearchEndpointBuilder WithStatus(string? status)
        {
            if (status is null)
            {
                _status = CandidateStatus.Approved.ToString();
            }

            return this;
        }

        public string Build()
        {
            var builder = new StringBuilder(_functionName);

            if (!string.IsNullOrWhiteSpace(_queryString))
            {
                builder
                    .Append(_queryString)
                    .Append($"&code={_functionKey}");
            }
            else
            {
                builder.Append($"?code={_functionKey}");
            }

            if (_jobId != string.Empty)
            {
                builder.Append($"&jobId={_jobId}");
            }

            if (_status != string.Empty)
            {
                builder.Append($"&status={_status}");
            }

            return builder.ToString();
        }
    }
}
