namespace Domain.Seedwork.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message, string? code = null, string[]? parameters = null) : base(message, code, parameters)
        {
        }
    }
}