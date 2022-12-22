using Candidates.Domain.Aggregates.CandidateTestsAggregate.ValueObjects;
using Persistence.Customization.FileStorage.Clients.Private;

namespace Candidates.API.Areas.Tests.Models.Responses
{
    public class RaportResponse
    {
        public string InstanceId { get; set; } = null!;
        public string Url { get; set; } = null!;

        public static RaportResponse? FromDomain(Raport? raport, IPrivateBlobClient privateBlobClient)
        {
            if (raport is null) return null;

            return new RaportResponse
            {
                InstanceId = raport.InstanceId,
                Url = privateBlobClient.GetSignedUri(raport.Url).AbsoluteUri
            };
        }
    }
}
