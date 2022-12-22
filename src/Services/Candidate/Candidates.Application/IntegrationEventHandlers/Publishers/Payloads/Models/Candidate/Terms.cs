using ValueObjects = Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects;

namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Candidate
{
    internal class Terms
    {
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string? Currency { get; set; }
        public int? WeeklyWorkHours { get; set; }
        public Formats? Formats { get; set; }
        public PartTimeWorkingHours? PartTimeWorkingHours { get; set; }
        public FullTimeWorkingHours? FullTimeWorkingHours { get; set; }
        public FreelanceContract? Freelance { get; set; }
        public PermanentContract? Permanent { get; set; }

        public static Terms? FromDomain(ValueObjects.Terms terms)
        {
            if (terms is null) return null;

            return new Terms
            {
                StartDate = terms.Availability?.From,
                EndDate = terms.Availability?.To,
                Currency = terms.Currency,
                WeeklyWorkHours = terms.PartTimeWorkingHours?.Weekly,
                Freelance = FreelanceContract.FromDomain(terms.Freelance),
                Permanent = PermanentContract.FromDomain(terms.Permanent),
                Formats = Formats.FromDomain(terms.Formats),
                PartTimeWorkingHours = PartTimeWorkingHours.FromDomain(terms.PartTimeWorkingHours),
                FullTimeWorkingHours = FullTimeWorkingHours.FromDomain(terms.FulTimeWorkingHours)
            };
        }
    }
}
