using Domain.Seedwork.Enums;
using ValueObjects = Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects;

namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Candidate
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
