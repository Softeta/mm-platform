using Contracts.Shared;
using Domain.Seedwork.Enums;
using System.ComponentModel.DataAnnotations;

namespace FrontOffice.Bff.API.Areas.Company.Models.Jobs
{
    public abstract class Step6RequestBase
    {
        public int? WeeklyWorkHours { get; set; }

        [StringLength(3)]
        public string? Currency { get; set; }

        public ICollection<FormatType> Formats { get; set; } = new List<FormatType>();

        public Address? Address { get; set; }

        public bool ShouldUpdateAddress()
        {
            return Formats.Any(x => x == FormatType.Onsite || x == FormatType.Hybrid);
        }
    }
}
