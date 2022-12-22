using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace API.Customization.Extensions
{
    public static class StartupExtensions
    {
        public static void AddControllerOptions(this IMvcBuilder builder)
        {
            builder
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
        }
    }
}
