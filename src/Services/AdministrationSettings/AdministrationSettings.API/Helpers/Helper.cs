using Newtonsoft.Json;
using FileIO = System.IO.File;

namespace AdministrationSettings.API.Helpers
{
    public static class Helper
    {
        public static async Task<List<T>> GetDataFromFileAsync<T>(string path) where T : class
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), path);
            var dataJson = await FileIO.ReadAllTextAsync(fullPath);
            return JsonConvert.DeserializeObject<List<T>>(dataJson);
        }
    }
}
