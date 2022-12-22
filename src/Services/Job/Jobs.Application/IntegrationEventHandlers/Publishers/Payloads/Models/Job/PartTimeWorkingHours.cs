using Domain.Seedwork.Enums;
using ValueObjects = Jobs.Domain.Aggregates.JobAggregate.ValueObjects;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Job
{
    public class PartTimeWorkingHours
    {
        public WorkingHoursType Type { get; set; }
        public int? Weekly { get; set; }

        public static PartTimeWorkingHours? FromDomain(ValueObjects.PartTimeWorkingHours? workingHours)
        {
            if (workingHours is null) return null;

            return new PartTimeWorkingHours
            {
                Type = workingHours.Type,
                Weekly = workingHours.Weekly
            };
        }
    }
}
