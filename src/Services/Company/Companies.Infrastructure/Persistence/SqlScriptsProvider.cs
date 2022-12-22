using Domain.Seedwork.Exceptions;
using System.Reflection;
using System.Text;

namespace Companies.Infrastructure.Persistence
{
    internal static class SqlScriptsProvider
    {
        public static async Task<string> Get(string migrationId)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var availableResources = assembly.GetManifestResourceNames().ToList();
            var migrationScript = availableResources.FirstOrDefault(x => x.Contains($".{migrationId}_"));
            if (string.IsNullOrEmpty(migrationScript))
            {
                throw new NotFoundException($"Script with migrationId:{migrationId} is not found.");
            }

            Console.WriteLine($"SQL script file will be applied: {migrationScript}");

            await using var resourceStream = assembly.GetManifestResourceStream(migrationScript);
            using var reader = new StreamReader(resourceStream!, Encoding.UTF8);
            return await reader.ReadToEndAsync();
        }
    }
}
