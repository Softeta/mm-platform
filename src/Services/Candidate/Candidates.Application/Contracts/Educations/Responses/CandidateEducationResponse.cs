using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Persistence.Customization.FileStorage.Clients.Private;
using Common = Contracts.Candidate.Educations.Responses;

namespace Candidates.Application.Contracts.Educations.Responses
{
    public class CandidateEducationResponse : Common.CandidateEducationResponse
    {
        public static Common.CandidateEducationResponse FromDomain(
            CandidateEducation candidateEducation, 
            IPrivateBlobClient privateBlobClient)
        {
            return new Common.CandidateEducationResponse
            {
                Id = candidateEducation.Id,
                SchoolName = candidateEducation.SchoolName,
                Degree = candidateEducation.Degree,
                FieldOfStudy = candidateEducation.FieldOfStudy,
                From = candidateEducation.Period.From,
                To = candidateEducation.Period.To,
                StudiesDescription = candidateEducation.StudiesDescription,
                IsStillStudying = candidateEducation.IsStillStudying,
                Certificate = DocumentResponse.FromDomain(candidateEducation.Certificate, privateBlobClient)
            };
        }
    }
}
