using Contracts.Job;

namespace FrontOffice.Bff.API.Areas.Company.Models.Jobs
{
    public abstract class Step6RequestExtendedBase : Step6RequestBase
    {
        public SalaryBudget? HourlyBudget { get; set; }

        public SalaryBudget? MonthlyBudget { get; set; }
    }
}
