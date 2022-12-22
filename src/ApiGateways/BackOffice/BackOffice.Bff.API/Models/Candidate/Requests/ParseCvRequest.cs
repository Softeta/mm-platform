using API.WebClients.Clients.FormRecognizer.Models;

namespace BackOffice.Bff.API.Models.Candidate.Requests;

public class ParseCvRequest
{
    public string? FileUri { get; set; }

    public Guid? FileCacheId { get; set; }

    public CvDocumentSource Source { get; set; }

}