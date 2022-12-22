using System.Collections.ObjectModel;
using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;

namespace ElasticSearch.Shared.Clients
{
    public class DocumentsServiceClient : ICandidatesSearchClient, IJobsSearchClient
    {
        private readonly SearchClient _searchClient;

        public DocumentsServiceClient(string url, string  adminKey, string index)
        {
            var endpoint = new Uri(url);
            var key = new AzureKeyCredential(adminKey);

            _searchClient = new SearchClient(endpoint, index, key);
        }

        public async Task SyncDocumentAsync<T>(T document)
        {
            var indexDocumentBatch = IndexDocumentsBatch.MergeOrUpload(new Collection<T> { document });
            await _searchClient.IndexDocumentsAsync(indexDocumentBatch);
        }

        public async Task DeleteDocumentAsync<T>(T document)
        {
            await _searchClient.DeleteDocumentsAsync(new Collection<T> { document });
        }

        public async Task<(List<SearchResult<T>> Documents, long? Count)> SearchDocumentsAsync<T>(string searchText, SearchOptions options)
        {
            var response = await _searchClient.SearchAsync<T>(searchText, options);

            var results = response
                .Value
                .GetResultsAsync();

            var list = new List<SearchResult<T>>();

            await foreach (var res in results)
            {
                list.Add(res);
            }

            return (list, response.Value.TotalCount);
        }
    }
}
