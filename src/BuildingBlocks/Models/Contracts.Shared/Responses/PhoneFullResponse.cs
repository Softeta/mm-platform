using ValueObjects = Domain.Seedwork.Shared.ValueObjects;

namespace Contracts.Shared.Responses
{
    public class PhoneFullResponse
    {
        public string? CountryCode { get; set; }
        public string? Number { get; set; }
        public string? PhoneNumber { get; set; }

        public static PhoneFullResponse? FromDomain(ValueObjects.Phone? phone)
        {
            if (phone is null) return null;

            return new PhoneFullResponse
            {
                CountryCode = phone.CountryCode,
                Number = phone.Number,
                PhoneNumber = phone.PhoneNumber,
            };
        }
    }
}
