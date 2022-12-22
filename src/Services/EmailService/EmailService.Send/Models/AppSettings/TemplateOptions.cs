namespace EmailService.Send.Models.AppSettings
{
    public abstract class TemplateOptions
    {
        public TemplateId TemplateId { get; set; } = null!;
    }

    public class TemplateId
    {
        public long EN { get; set; }
        public long DA { get; set; }
        public long SV { get; set; }
    }
}
