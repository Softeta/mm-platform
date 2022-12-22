using System.ComponentModel.DataAnnotations;

namespace FrontOffice.Bff.API.Areas.Company.Models.Jobs
{
    public class Step4Request
    {
        [MaxLength(4000)]
        public string? Description { get; set; }
    }
}
