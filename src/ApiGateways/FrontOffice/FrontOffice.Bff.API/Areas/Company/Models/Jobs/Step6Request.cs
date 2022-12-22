using Contracts.Job;
using Domain.Seedwork.Enums;

namespace FrontOffice.Bff.API.Areas.Company.Models.Jobs
{
    public class Step6Request : Step6RequestBase
    {
        public PermanentSection Permanent { get; set; } = null!;

        public FreelanceSection Freelance { get; set; } = null!;
    }

    public class FreelanceSection
    {
        public ICollection<CompanyWorkingHoursType> CompanyWorkingHourTypes { get; set; } = new List<CompanyWorkingHoursType>();

        public int? HoursPerProject { get; set; }

        public SalaryBudget? HourlyBudget { get; set; }

        public SalaryBudget? MonthlyBudget { get; set; }
    }

    public class PermanentSection
    {
        public SalaryBudget? MonthlyBudget { get; set; }
    }
}
