namespace BackOffice.Bff.API.Infrastructure
{
    public class WeavyJwtSettings
    {
        public int ExpiresInMinutes { get; set; }
        public string Client { get; set; } = null!;
        public string Key { get; set; } = null!;
    }
}


