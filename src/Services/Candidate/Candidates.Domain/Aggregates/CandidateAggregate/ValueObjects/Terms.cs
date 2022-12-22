using Domain.Seedwork;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;

namespace Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects
{
    public class Terms : ValueObject<Terms>
    {
        public DateRange? Availability { get; init; }
        public string? Currency { get; init; }
        public Permanent? Permanent { get; init; }
        public Freelance? Freelance { get; init; }
        public PartTimeWorkingHours? PartTimeWorkingHours { get; init; }
        public FullTimeWorkingHours? FulTimeWorkingHours { get; init; }
        public ProjectWorkingHours? ProjectWorkingHours { get; init; }
        public JobFormats? Formats { get; init; }

        private Terms() { }

        public Terms(
            ICollection<WorkType> workTypes,
            decimal? freelanceHourlySalary,
            decimal? freelanceMonthlySalary,
            decimal? permanentMonthlySalary,
            DateTimeOffset? startDate,
            DateTimeOffset? endDate,
            string? currency,
            ICollection<WorkingHoursType>? workingHourTypes,
            int? workHours,
            ICollection<FormatType> jobFormats)
        {
            if (workTypes.Any(x => x == WorkType.Freelance))
            {
                Freelance = new Freelance(freelanceHourlySalary, freelanceMonthlySalary);
            }

            if (workTypes.Any(x => x == WorkType.Permanent))
            {
                Permanent = new Permanent(permanentMonthlySalary);
            }

            if (workingHourTypes?.Any(x => x == WorkingHoursType.FullTime) ?? false)
            {
                FulTimeWorkingHours = new FullTimeWorkingHours();
            }

            if (workingHourTypes?.Any(x => x == WorkingHoursType.PartTime) ?? false)
            {
                PartTimeWorkingHours = new PartTimeWorkingHours(workHours);
            }

            if (workingHourTypes?.Any(x => x == WorkingHoursType.ProjectEmployment) ?? false)
            {
                ProjectWorkingHours = new ProjectWorkingHours();
            }

            Formats = new JobFormats(jobFormats);
            Availability = DateRange.FromDates(startDate, endDate);
            Currency = currency;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Availability;
            yield return Currency;
        }
    }
}
