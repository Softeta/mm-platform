namespace Domain.Seedwork.Exceptions
{
    public class ForbiddenException : BaseException
    {
        public ForbiddenException(string message, string? code = null, string[]? parameters = null) : base(message, code, parameters)
        {
        }
    }
}
