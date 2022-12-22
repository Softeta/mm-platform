using Domain.Seedwork.Enums;

namespace Contracts.Candidate
{
    public class PartTimeWorkingHours
    {
        public WorkingHoursType Type { get; set; }
        public int? Weekly { get; set; }
    }
}
