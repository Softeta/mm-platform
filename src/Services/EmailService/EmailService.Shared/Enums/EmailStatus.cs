namespace EmailService.Shared.Enums
{
    public enum EmailStatus
    {
        sent = 1,
        delivered = 2,
        soft_bounce = 3,
        hard_bounce = 4,
        unique_opened = 5,
        invalid_email = 6
    }
}
