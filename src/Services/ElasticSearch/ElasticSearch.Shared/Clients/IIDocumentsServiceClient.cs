using Azure.Search.Documents;
using Azure.Search.Documents.Models;

namespace ElasticSearch.Shared.Clients
{
    public interface IDocumentsServiceClient
    {
        Task SyncDocumentAsync<T>(T documents);
        Task DeleteDocumentAsync<T>(T documents);
        Task<(List<SearchResult<T>> Documents, long? Count)> SearchDocumentsAsync<T>(string searchText, SearchOptions options);
    }
}
