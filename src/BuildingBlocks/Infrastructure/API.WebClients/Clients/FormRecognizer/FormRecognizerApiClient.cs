using API.WebClients.Clients.FormRecognizer.Configurations;
using API.WebClients.Clients.FormRecognizer.Models;
using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Microsoft.Extensions.Options;

namespace API.WebClients.Clients.FormRecognizer;

public class FormRecognizerApiClient : IFormRecognizerApiClient
{
    private readonly DocumentAnalysisClient _documentAnalysisClient;
    private readonly Guid _linkedInModelId;

    public FormRecognizerApiClient(IOptions<CvFormRecognizerSettings> formRecognizerOptions)
    {
        var cvFormRecognizerSettings = formRecognizerOptions.Value;

        var credential = new AzureKeyCredential(cvFormRecognizerSettings.Key);
        _documentAnalysisClient = new DocumentAnalysisClient(new Uri(cvFormRecognizerSettings.Url), credential);

        _linkedInModelId = cvFormRecognizerSettings.LinkedInModelId;
    }

    public async Task<DocumentResult?> AnalyzeCvAsync(Stream file, CvDocumentSource source, CancellationToken cancellationToken)
    {
        switch (source)
        {
            case CvDocumentSource.LinkedIn:
                return await AnalyzeLinkedInCvAsync(file, cancellationToken);
        }

        return null;
    }

    private async Task<DocumentResult> AnalyzeLinkedInCvAsync(Stream file, CancellationToken cancellationToken)
    {
        var operation = await _documentAnalysisClient.AnalyzeDocumentAsync(
            WaitUntil.Completed,
            _linkedInModelId.ToString(), 
            file, 
            cancellationToken: cancellationToken);

        await operation.WaitForCompletionAsync(cancellationToken);

        var documentFields = operation.Value.Documents.First().Fields;

        return DocumentResult.FromLinkedInModel(documentFields);
    }
}