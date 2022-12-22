using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Web;

namespace API.Customization.Extensions
{
    public static class ObjectExtensions
    {
        public static bool TryParseJson<T>(this string jsonString, out T result)
        {
            try
            {
                result = JsonConvert.DeserializeObject<T>(jsonString);
                if (result is null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                result = default!;
                return false;
            }
        }

        public static T QueryStringToObject<T>(this string queryParams)
        {
            var dict = HttpUtility.ParseQueryString(queryParams);
            string json = JsonConvert.SerializeObject(dict.Cast<string>().ToDictionary(k => k, v => GetValues(dict[v])));
            return JsonConvert.DeserializeObject<T>(json);
        }

        private static object? GetValues(string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            if (value.Contains(","))
            {
                return value.Split(",");
            }

            return value;
        }

        public static async Task<T> ParseRequestObjectAsync<T>(this HttpRequest request)
        {
            using var streamReader = new StreamReader(request.Body);
            var requestBody = await streamReader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<T>(requestBody);
        }
    }
}
