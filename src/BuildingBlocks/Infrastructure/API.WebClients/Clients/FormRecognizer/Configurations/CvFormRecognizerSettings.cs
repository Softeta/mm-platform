namespace API.WebClients.Clients.FormRecognizer.Configurations;

public class CvFormRecognizerSettings
{
    public string Key { get; set; } = null!;

    public string Url { get; set; } = null!;

    public Guid LinkedInModelId { get; set; }
}