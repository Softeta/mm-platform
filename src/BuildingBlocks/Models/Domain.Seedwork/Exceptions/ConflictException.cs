namespace Domain.Seedwork.Exceptions
{
    public class ConflictException : BaseException
    {
        public ConflictException(string message, string? code = null, string[]? parameters = null) : base(message, code, parameters)
        {
        }
    }
}