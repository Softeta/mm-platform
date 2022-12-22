using Contracts.Shared.Requests.Base;

namespace Contracts.Shared.Requests
{
    public class UpdateFileCacheRequest : FileCacheRequestBase
    {
        public bool HasChanged { get; set; }
    }
}
