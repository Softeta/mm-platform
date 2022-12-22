using Persistence.Customization.FileStorage.Clients.Private;
using Shared = Contracts.Shared.Responses;
using ValueObjects = Domain.Seedwork.Shared.ValueObjects;

namespace Candidates.Application.Contracts
{
    internal class DocumentResponse : Shared.DocumentResponse
    {
        public static Shared.DocumentResponse? FromDomain(
            ValueObjects.Document? document,
            IPrivateBlobClient? privateBlobClient = null)
        {
            if (document is null) return null;
            
            return new Shared.DocumentResponse
            {
                Uri = GetUri(document.Uri, privateBlobClient),
                Name = document.FileName,
            };
        }

        private static string? GetUri(string? uri, IPrivateBlobClient? privateBlobClient)
        {
            if (string.IsNullOrWhiteSpace(uri)) return null;
            if (privateBlobClient is null) return uri;

            return privateBlobClient.GetSignedUri(uri).AbsoluteUri;
        }
    }
}
