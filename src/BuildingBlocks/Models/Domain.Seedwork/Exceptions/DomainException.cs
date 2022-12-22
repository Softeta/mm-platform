namespace Domain.Seedwork.Exceptions
{
    public class DomainException : BaseException
    {
        public DomainException(string message, string? code = null, string[]? parameters = null) : base(message, code, parameters)
        {
        }
    }
}
