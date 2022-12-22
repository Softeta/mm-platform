using Domain.Seedwork.Exceptions;

namespace Domain.Seedwork.Shared.ValueObjects;

public class Phone : ValueObject<Phone>
{
    private const int MinimumDigitsCount = 4;
    private const int MaxCountryCodeLength = 4;
    private const int MaxDigitsCount = 28;

    public string? CountryCode { get; init; }
    public string? Number { get; init; }
    public string? PhoneNumber { get; init; }

    private Phone() { }

    public Phone(string? countryCode, string? number)
    {
        CountryCode = countryCode;
        Number = number;
        PhoneNumber = $"{CountryCode}{Number}";

        Validate();
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return CountryCode;
        yield return Number;
        yield return PhoneNumber;
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Number) && string.IsNullOrWhiteSpace(CountryCode))
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(Number))
        {
            throw new DomainException($"Missing value for {nameof(Number)}", ErrorCodes.Shared.Phone.NumberNotFound); ;
        }

        if (string.IsNullOrWhiteSpace(CountryCode))
        {
            throw new DomainException($"Missing value for {nameof(CountryCode)}", ErrorCodes.Shared.Phone.CountryCodeNotFound); ;
        }

        if (CountryCode.Length > MaxCountryCodeLength)
        {
            throw new DomainException(
                $"Maximum allowed length for {nameof(CountryCode)} is {MaxCountryCodeLength}",
                ErrorCodes.Shared.Phone.CountryCodeMaxLengthExceeded, 
                new string[] { MaxCountryCodeLength.ToString()});
        }

        if (Number.Length > MaxDigitsCount)
        {
            throw new DomainException(
                $"Maximum allowed length for {nameof(Number)} is {MaxDigitsCount}",
                ErrorCodes.Shared.Phone.NumberMaxLengthExceeded,
                new string[] { MaxDigitsCount.ToString() });
        }


        if (Number.Length < MinimumDigitsCount)
        {
            throw new DomainException($"{nameof(Number)} minimum digits count is {MinimumDigitsCount}",
                ErrorCodes.Shared.Phone.NumberTooShort,
                new string[] { MinimumDigitsCount.ToString() });
        }
    }
}
