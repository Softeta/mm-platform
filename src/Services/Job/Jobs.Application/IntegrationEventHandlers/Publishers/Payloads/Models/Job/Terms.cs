using ValueObjects = Jobs.Domain.Aggregates.JobAggregate.ValueObjects;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Job
{
    public class Terms
    {
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string? Currency { get; set; }
        public Formats Formats { get; set; } = null!;
        public PartTimeWorkingHours? PartTimeWorkingHours { get; set; }
        public FullTimeWorkingHours? FullTimeWorkingHours { get; set; }
        public Freelance? Freelance { get; set; }
        public Permanent? Permanent { get; set; }

        public static Terms FromDomain(ValueObjects.Terms terms)
        {
            return new Terms
            {
                StartDate = terms.Availability?.From,
                EndDate = terms.Availability?.To,
                Currency = terms.Currency,
                PartTimeWorkingHours = PartTimeWorkingHours.FromDomain(terms.PartTimeWorkingHours),
                FullTimeWorkingHours = FullTimeWorkingHours.FromDomain(terms.FullTimeWorkingHours),
                Freelance = Freelance.FromDomain(terms.Freelance),
                Permanent = Permanent.FromDomain(terms.Permanent),
                Formats = Formats.FromDomain(terms.Formats)
            };
        }
    }
}
