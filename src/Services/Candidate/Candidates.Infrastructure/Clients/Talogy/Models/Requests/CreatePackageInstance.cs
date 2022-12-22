namespace Candidates.Infrastructure.Clients.Talogy.Models.Requests
{
    public class CreatePackageInstance
    {
        public string ParticipantId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string PackageTypeId { get; set; } = null!;
        public string LocaleCode { get; set; } = null!;
        public string CompletionUrl { get; set; } = null!;
        public string NotificationUrl { get; set; } = null!;


        public int? AdditionalTimePercent { get; set; } // #1822 TODO: what is it for
        public Dictionary<string, string>? BioData { get; set; } // #1822  TODO: what is it for
        public Dictionary<string, string>? Properties { get; set; } // #1822  TODO: what is it for
    }
}
