namespace API.Customization.Exceptions
{
    public static class ExceptionExtensions
    {
        public static void AppendStackTrace(this ExceptionResponse exceptionResponse, string? stackTrace, string? source, bool isStackTraceOn)
        {
            if (!isStackTraceOn) return;
            if (exceptionResponse.StackTrace is null)
            {
                exceptionResponse.StackTrace = $"Source {source}:\n{stackTrace}";
            }
            else
            {
                exceptionResponse.StackTrace = $"{exceptionResponse.StackTrace}\n Source {source}:\n{stackTrace}";
            }
        }
    }
}