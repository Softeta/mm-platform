using Domain.Seedwork.Enums;

namespace FrontOffice.Bff.API.Areas.Company.Models.Jobs
{
    public class Step6FreelanceRequest : Step6RequestExtendedBase
    {
        public ICollection<CompanyWorkingHoursType> CompanyWorkingHourTypes { get; set; } = new List<CompanyWorkingHoursType>();

        public int? HoursPerProject { get; set; }
    }
}
