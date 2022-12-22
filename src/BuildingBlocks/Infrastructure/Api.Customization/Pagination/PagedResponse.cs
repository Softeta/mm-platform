using Microsoft.AspNetCore.WebUtilities;
using System.Text.RegularExpressions;

namespace API.Customization.Pagination
{
    public class PagedResponse<T>
    {
        public static string PageNumberPattern = @"PageNumber=\d+&?";
        public static string PageSizePattern = @"PageSize=\d+&?";

        public long Count { get; set; }
        public IEnumerable<T>? Data { get; set; }
        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }
        public string? NextPagePath { get; set; }
        public string? PreviousPagePath { get; set; }

        public PagedResponse()
        {
        }

        public PagedResponse(long count, IEnumerable<T> data, int pageNumber, int pageSize, string path, string queryString)
        {
            Count = count;
            Data = data;
            CurrentPage = pageNumber;
            PageSize = pageSize;
            NextPagePath = count > pageSize * pageNumber ? GetPageUri(pageNumber + 1, pageSize, path, queryString) : null;
            PreviousPagePath = GetPreviousPagePath(count, pageNumber, pageSize, path, queryString);
        }

        private static string? GetPreviousPagePath(long count, int pageNumber, int pageSize, string path, string queryString)
        {
            if (pageNumber < 2 || count == 0)
            {
                return null;
            }

            var previousPage = pageNumber - 1;
            var lastPage = (int)Math.Ceiling((decimal)count / pageSize);

            if (count <= pageSize * pageNumber - pageSize)
            {
                previousPage = lastPage;
            }

            return GetPageUri(previousPage, pageSize, path, queryString);
        }

        private static string GetPageUri(int pageNumber, int pageSize, string route, string queryString)
        {
            queryString = ProccessQueryString(queryString);
            var modifiedUri = QueryHelpers.AddQueryString($"{route}{queryString}", "pageNumber", pageNumber.ToString());

            return QueryHelpers.AddQueryString(modifiedUri, "pageSize", pageSize.ToString());
        }

        private static string ProccessQueryString(string queryString)
        {
            var pageNumberRegex = new Regex(PageNumberPattern, RegexOptions.IgnoreCase);
            var pageSizeRegex = new Regex(PageSizePattern, RegexOptions.IgnoreCase);
            queryString = pageNumberRegex.Replace(queryString, string.Empty);
            queryString = pageSizeRegex.Replace(queryString, string.Empty);

            if (queryString.EndsWith('&'))
            {
                queryString = queryString.Remove(queryString.Length - 1);
            }
            if (queryString == "?")
            {
                queryString = string.Empty;
            }

            return queryString;
        }
    }
}
