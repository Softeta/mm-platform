namespace Contracts.Shared.Requests
{
    public class PhoneRequest
    {
        public string? CountryCode { get; set; }
        public string? Number { get; set; }

        public static PhoneRequest From(string? countryCode, string? number)
        {
            return new PhoneRequest
            {
                CountryCode = countryCode,
                Number = number
            };
        }
    }
}
