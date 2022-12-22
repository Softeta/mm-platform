using API.WebClients.Clients.FormRecognizer.Models;

namespace API.WebClients.Clients.FormRecognizer;

public interface IFormRecognizerApiClient
{
    Task<DocumentResult?> AnalyzeCvAsync(Stream file, CvDocumentSource source, CancellationToken cancellationToken);
}