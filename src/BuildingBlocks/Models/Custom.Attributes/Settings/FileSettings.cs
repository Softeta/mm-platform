namespace Custom.Attributes.Settings
{
    public abstract class FileSettings
    {
        public string[] SupportedTypes { get; set; } = null!;
        public int MaxSizeInKilobytes { get; set; }
    }
}
