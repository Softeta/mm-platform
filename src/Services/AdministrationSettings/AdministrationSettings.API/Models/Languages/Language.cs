namespace AdministrationSettings.API.Models.Languages
{
    public class Language
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
