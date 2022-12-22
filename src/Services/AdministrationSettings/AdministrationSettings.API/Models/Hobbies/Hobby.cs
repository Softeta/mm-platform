using Microsoft.AspNetCore.Mvc;

namespace AdministrationSettings.API.Models.Hobbies
{
    public class Hobby
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
    }
}
