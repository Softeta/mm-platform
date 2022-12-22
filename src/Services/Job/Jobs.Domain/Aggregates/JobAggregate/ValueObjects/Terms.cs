using Domain.Seedwork;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Shared.ValueObjects;

namespace Jobs.Domain.Aggregates.JobAggregate.ValueObjects
{
    public class Terms : ValueObject<Terms>
    {
        public DateRange? Availability { get; init; }
        public bool IsUrgent { get; init; }
        public string? Currency { get; init; }
        public Freelance? Freelance { get; init; }
        public Permanent? Permanent { get; init; }
        public PartTimeWorkingHours? PartTimeWorkingHours { get; init; }
        public FullTimeWorkingHours? FullTimeWorkingHours { get; init; }
        public ProjectWorkingHours? ProjectWorkingHours { get; init; }
        public JobFormats Formats { get; init; } = null!;

        private Terms() { }

        public Terms(
            bool? isUrgent,
            DateTimeOffset? startDate,
            DateTimeOffset? endDate,
            string? currency,
            IEnumerable<WorkType> workTypes,
            int? hoursPerProject,
            int? weeklyHours,
            decimal? freelanceHourlySalaryFrom,
            decimal? freelanceHourlySalaryTo,
            decimal? freelanceMonthlySalaryFrom,
            decimal? freelanceMonthlySalaryTo,
            decimal? permanentMonthlySalaryFrom,
            decimal? permanentMonthlySalaryTo,
            IEnumerable<WorkingHoursType> workingHourTypes,
            IEnumerable<FormatType> jobFormats)
        {
            IsUrgent = isUrgent ?? false;
            Availability = DateRange.FromDates(startDate, endDate);

            if (workTypes.Any(x => x == WorkType.Freelance))
            {
                Freelance = new Freelance(
                    hoursPerProject,
                    freelanceHourlySalaryFrom,
                    freelanceHourlySalaryTo,
                    freelanceMonthlySalaryFrom,
                    freelanceMonthlySalaryTo);
            }

            if (workTypes.Any(x => x == WorkType.Permanent))
            {
                Permanent = new Permanent(
                    permanentMonthlySalaryFrom,
                    permanentMonthlySalaryTo);
            }

            if (workingHourTypes.Any(x => x == WorkingHoursType.FullTime))
            {
                FullTimeWorkingHours = new FullTimeWorkingHours();
            }

            if (workingHourTypes.Any(x => x == WorkingHoursType.PartTime))
            {
                PartTimeWorkingHours = new PartTimeWorkingHours(weeklyHours);
            }

            if (workingHourTypes.Any(x => x == WorkingHoursType.ProjectEmployment))
            {
                ProjectWorkingHours = new ProjectWorkingHours();
            }

            Currency = currency;
            Formats = new JobFormats(jobFormats);
        }

        public Terms(Terms? terms)
        {
            if (terms == null)
            {
                return;
            }

            IsUrgent = terms.IsUrgent;
            Freelance = new Freelance(terms.Freelance);
            Permanent = new Permanent(terms.Permanent);

            if (terms.Availability is not null)
            {
                Availability = new DateRange(terms.Availability.From, terms.Availability.To);
            }

            if (terms.FullTimeWorkingHours is not null)
            {
                FullTimeWorkingHours = new FullTimeWorkingHours();
            }

            if (terms.PartTimeWorkingHours is not null)
            {
                PartTimeWorkingHours = new PartTimeWorkingHours(terms.PartTimeWorkingHours.Weekly);
            }
            
            if (terms.ProjectWorkingHours is not null)
            {
                ProjectWorkingHours = new ProjectWorkingHours();
            }

            Currency= terms.Currency;
            Formats = terms.Formats.GetCopy();
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Availability;
            yield return Currency;
        }
    }
}
