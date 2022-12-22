namespace EmailService.Send.Models.AppSettings
{
    public abstract class EmailWithLinkOptions : TemplateOptions
    {
        public string Url { get; set; } = null!;
    }
}
