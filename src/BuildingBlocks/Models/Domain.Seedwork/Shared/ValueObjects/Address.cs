namespace Domain.Seedwork.Shared.ValueObjects;

public class Address : ValueObject<Address>
{
    public string AddressLine { get; init; } = null!;
    public string? City { get; init; }
    public string? Country { get; init; }
    public string? PostalCode { get; init; }
    public Coordinates? Coordinates { get; init; }
    public string? Location { get; private set; }

    private Address() { }

    public Address(
            string addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude)
    {
        AddressLine = addressLine;
        City = city;
        Country = country;
        PostalCode = postalCode;

        SetLocation();

        if (longitude.HasValue && latitude.HasValue)
        {
            Coordinates = new Coordinates(longitude.Value, latitude.Value);
        }
    }
    private void SetLocation()
    {
        if (string.IsNullOrWhiteSpace(City)) return;
        if (string.IsNullOrWhiteSpace(Country)) return;

        Location = $"{City} ({Country})";
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return AddressLine;
        yield return City;
        yield return Country;
        yield return PostalCode;
        yield return Coordinates;
    }
}
