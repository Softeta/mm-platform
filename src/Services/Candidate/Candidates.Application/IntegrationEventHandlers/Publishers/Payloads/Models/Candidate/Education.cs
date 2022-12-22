using Candidates.Domain.Aggregates.CandidateAggregate.Entities;

namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Candidate
{
    internal class Education
    {
        public string SchoolName { get; private set; } = null!;
        public string Degree { get; set; } = null!;
        public string FieldOfStudy { get; private set; } = null!;
        public DateRange Period { get; private set; } = null!;
        public string? StudiesDescription { get; private set; }
        public bool IsStillStudying { get; private set; }
        public Document? Certificate { get; private set; }

        public static Education FromDomain(CandidateEducation education)
        {
            return new Education
            {
                SchoolName = education.SchoolName,
                Degree = education.Degree,
                FieldOfStudy = education.FieldOfStudy,
                Period = new DateRange(education.Period.From, education.Period.To),
                StudiesDescription = education.StudiesDescription,
                IsStillStudying = education.IsStillStudying,
                Certificate = new Document(education.Certificate?.Uri, education.Certificate?.FileName)
            };
        }
    }
}
