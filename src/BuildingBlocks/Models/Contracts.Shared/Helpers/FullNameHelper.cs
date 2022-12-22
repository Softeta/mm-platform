namespace Contracts.Shared.Helpers;

public static class FullNameHelper
{
    public static string? GetFullName(string? firstName, string? lastName)
    {
        if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
        {
            return $"{firstName} {lastName}";
        }
        if (!string.IsNullOrWhiteSpace(firstName))
        {
            return firstName;
        }
        if (!string.IsNullOrWhiteSpace(lastName))
        {
            return lastName;
        }
        return null;
    }
}