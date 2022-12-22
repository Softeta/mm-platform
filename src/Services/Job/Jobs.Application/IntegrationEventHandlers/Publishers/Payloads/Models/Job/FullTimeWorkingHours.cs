using Domain.Seedwork.Enums;
using ValueObjects = Jobs.Domain.Aggregates.JobAggregate.ValueObjects;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Job
{
    public class FullTimeWorkingHours
    {
        public WorkingHoursType Type { get; set; }

        public static FullTimeWorkingHours? FromDomain(ValueObjects.FullTimeWorkingHours? workingHours)
        {
            if (workingHours is null) return null;

            return new FullTimeWorkingHours
            {
                Type = workingHours.Type
            };
        }
    }
}
