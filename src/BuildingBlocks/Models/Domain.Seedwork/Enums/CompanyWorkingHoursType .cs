using System.ComponentModel;

namespace Domain.Seedwork.Enums
{
    public enum CompanyWorkingHoursType
    {
        [Description("Describes Full-time or Part-time from WorkingHourTypes")]
        Continuous = 1,
        [Description("Describes ProjectEmployment from WorkingHourTypes")]
        ProjectEmployment = 2
    }
}
