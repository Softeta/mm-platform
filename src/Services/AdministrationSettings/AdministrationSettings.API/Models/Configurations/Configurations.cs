using Microsoft.AspNetCore.StaticFiles;

namespace AdministrationSettings.API.Models.Configurations
{
    public class Configurations
    {
        public FileSettings ImageSettings { get; set; } = null!;
        public FileSettings VideoSettings { get; set; } = null!;
        public FileSettings DocumentSettings { get; set; } = null!;
    }

    public class FileSettings
    {
        public string[] SupportedTypes { get; set; } = null!;
        public int MaxSizeInKilobytes { get; set; }
        public List<string> SupportedExtensions => GetSupportedExtensions(SupportedTypes);

        public static List<string> GetSupportedExtensions(string[] supportedTypes)
        {
            var supportedExtensions = new List<string>();
            var extensionMappings = new FileExtensionContentTypeProvider().Mappings;

            foreach (var supportedType in supportedTypes)
            {
                var result = extensionMappings
                    .Where(x => x.Value == supportedType)
                    .Select(x => x.Key).ToList();

                supportedExtensions.AddRange(result);
            }

            return supportedExtensions;
        }
    }
}
