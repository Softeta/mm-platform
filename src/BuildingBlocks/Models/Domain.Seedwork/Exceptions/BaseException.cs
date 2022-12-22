namespace Domain.Seedwork.Exceptions
{
    public class BaseException : Exception
    {
        public string? Code { get; set; }
        public string[] Parameters { get; set; }

        public BaseException(string message, string? code = null, string[]? parameters = null) : base(message)
        {
            Code = code;
            Parameters = parameters ?? new string[] { };
        }
    }
}
