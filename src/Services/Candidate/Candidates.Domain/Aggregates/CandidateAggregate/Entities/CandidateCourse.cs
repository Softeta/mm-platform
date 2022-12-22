using Domain.Seedwork;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;

namespace Candidates.Domain.Aggregates.CandidateAggregate.Entities
{
    public class CandidateCourse : Entity
    {
        public Guid CandidateId { get; private set; }
        public string Name { get; private set; } = null!;
        public string IssuingOrganization { get; private set; } = null!;
        public string? Description { get; private set; }
        public Document? Certificate { get; private set; }

        private CandidateCourse() { }

        public CandidateCourse(
            Guid candidateId,
            string name,
            string issuingOrganization,
            string? description,
            string? certificateUri,
            string? certificateFileName)
        {
            Id = Guid.NewGuid();
            CandidateId = candidateId;
            Name = name;
            IssuingOrganization = issuingOrganization;
            Description = description;
            Certificate = new Document(certificateUri, certificateFileName);
            CreatedAt = DateTimeOffset.UtcNow;

            Validate();
        }

        public void Update(
            string name,
            string issuingOrganization,
            string? description,
            string? certificateUri,
            string? certificateFileName,
            bool isCertificateChanged)
        {
            Name = name;
            IssuingOrganization = issuingOrganization;
            Description = description;
            
            if (isCertificateChanged)
            {
                Certificate = new Document(certificateUri, certificateFileName);
            }
            
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new DomainException($"{nameof(Name)} should be filled in",
                    ErrorCodes.Candidate.Course.NameRequired);
            }

            if (string.IsNullOrWhiteSpace(IssuingOrganization))
            {
                throw new DomainException($"{nameof(IssuingOrganization)} should be filled in",
                    ErrorCodes.Candidate.Course.IssuingOrganizationRequired);
            }
        }
    }
}
