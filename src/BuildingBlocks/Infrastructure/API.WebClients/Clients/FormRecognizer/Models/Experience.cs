namespace API.WebClients.Clients.FormRecognizer.Models;

public class Experience
{
    public string? Company { get; set; }
    public string? Position { get; set; }
    public string? Description { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public bool IsCurrentlyWorking { get; set; }
}