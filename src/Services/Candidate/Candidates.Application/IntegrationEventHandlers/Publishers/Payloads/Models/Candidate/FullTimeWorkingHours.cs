using Domain.Seedwork.Enums;
using ValueObjects = Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects;

namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Candidate
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
