using Domain.Seedwork;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;

namespace Candidates.Domain.Aggregates.CandidateAggregate.Entities
{
    public class CandidateEducation : Entity
    {
        public Guid CandidateId { get; private set; }
        public string SchoolName { get; private set; } = null!;
        public string Degree { get; set; } = null!;
        public string FieldOfStudy { get; private set; } = null!;
        public DateRange Period { get; private set; } = null!;
        public string? StudiesDescription { get; private set; }
        public bool IsStillStudying { get; private set; }
        public Document? Certificate { get; private set; }

        private CandidateEducation() { }

        public CandidateEducation(
            Guid candidateId,
            string schoolName,
            string degree,
            string fieldOfStudy,
            DateTimeOffset from,
            DateTimeOffset? to,
            string? studiesDescription,
            bool isStillStudying,
            string? certificateUri,
            string? certificateFileName)
        {
            Id = Guid.NewGuid();
            CandidateId = candidateId;
            SchoolName = schoolName;
            Degree = degree;
            FieldOfStudy = fieldOfStudy;
            Period = new DateRange(from, to);
            StudiesDescription = studiesDescription;
            IsStillStudying = isStillStudying;
            Certificate = new Document(certificateUri, certificateFileName);
            CreatedAt = DateTimeOffset.UtcNow;

            Validate();
        }

        public void Update(
            string schoolName,
            string degree,
            string fieldOfStudy,
            DateTimeOffset from,
            DateTimeOffset? to,
            string? studiesDescription,
            bool isStillStudying,
            string? certificateUri,
            string? certificateFileName,
            bool isCertificateChanged)
        {
            SchoolName = schoolName;
            Degree = degree;
            FieldOfStudy = fieldOfStudy;
            Period = new DateRange(from, to);
            StudiesDescription = studiesDescription;
            IsStillStudying = isStillStudying;
            
            if (isCertificateChanged)
            {
                Certificate = new Document(certificateUri, certificateFileName);
            }
           
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(SchoolName))
            {
                throw new DomainException($"{nameof(SchoolName)} should be filled in",
                    ErrorCodes.Candidate.Education.SchoolNameRequired);
            }

            if (string.IsNullOrWhiteSpace(FieldOfStudy))
            {
                throw new DomainException($"{nameof(FieldOfStudy)} should be filled in",
                    ErrorCodes.Candidate.Education.FieldOfStudyRequired);
            }

            if (string.IsNullOrWhiteSpace(Degree))
            {
                throw new DomainException($"{nameof(Degree)} should be filled in",
                    ErrorCodes.Candidate.Education.DegreeRequired);
            }
        }
    }
}
