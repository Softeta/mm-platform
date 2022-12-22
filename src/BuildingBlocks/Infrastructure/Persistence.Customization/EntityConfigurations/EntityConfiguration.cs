namespace Persistence.Customization.EntityConfigurations
{
    public static class EntityConfiguration
    {
        public const int Currency = 3;
        public const int PhoneNumber = 32;
        public const int PhoneCountryCode = 4;
        public const int PhoneDigits = 28;
        public const int Country = 64;
        public const int Region = 64;
        public const int City = 64;
        public const int PostalCode = 64;
        public const int Address = 256;
        public const int Alias = 64;
        public const int Email = 64;
        public const int Clasificator = 64;
        public const int Title = 256;
        public const int LinkUrl = 2000;
        public const int LongDescription = 4000;
        public const int Note = 250;
        public const string Identifier = "uniqueidentifier";
        public const string MoneySqlType = "Decimal(18, 2)";
        public const string NVarcharMax = "nvarchar(max)";
    }
}
