using Domain.Seedwork.Enums;
using System.Text;
using static Domain.Seedwork.Exceptions.ErrorCodes.Filters;

namespace FrontOffice.Bff.API.Builders
{
    public class JobsSearchEndpointBuilder
    {
        private string _functionName = null!;
        private string _functionKey = null!;
        private int? _pageSize;
        private int? _pageNumber;
        private string _candidateId = string.Empty;
        private ICollection<JobStage> _stages = new List<JobStage>();
        private bool? _isPublished;

        public JobsSearchEndpointBuilder WithFunctionName(string name)
        {
            _functionName = name;

            return this;
        }

        public JobsSearchEndpointBuilder WithFunctionKey(string functionKey)
        {
            _functionKey = functionKey;

            return this;
        }

        public JobsSearchEndpointBuilder WithPageSize(int pageSize)
        {
            _pageSize = pageSize;

            return this;
        }

        public JobsSearchEndpointBuilder WithPageNumber(int pageNumber)
        {
            _pageNumber = pageNumber;

            return this;
        }

        public JobsSearchEndpointBuilder WithCandidateId(Guid candidateId)
        {
            _candidateId = candidateId.ToString();

            return this;
        }

        public JobsSearchEndpointBuilder AddStage(JobStage stage)
        {
            _stages.Add(stage);

            return this;
        }

        public JobsSearchEndpointBuilder WithIsPublished(bool? isPublished)
        {
            _isPublished = isPublished;

            return this;
        }

        public string Build()
        {
            var builder = new StringBuilder(_functionName);
            builder.Append($"?code={_functionKey}");

            if (_pageSize.HasValue)
            {
                builder.Append($"&pageSize={_pageSize}");
            }
            if (_pageNumber.HasValue)
            {
                builder.Append($"&pageNumber={_pageNumber}");
            }
            if (_candidateId != string.Empty)
            {
                builder.Append($"&candidateId={_candidateId}");
            }
            if (_isPublished.HasValue)
            {
                builder.Append($"&isPublished={_isPublished.Value}");
            }
            if (_stages.Any())
            {
                builder.Append($"&stages={string.Join("&stages=", _stages)}");
            }

            return builder.ToString();
        }
    }
}
