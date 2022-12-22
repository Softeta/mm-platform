namespace Domain.Seedwork.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message, string? code = null, string[]? parameters = null) : base(message, code, parameters)
        {
        }
    }
}
