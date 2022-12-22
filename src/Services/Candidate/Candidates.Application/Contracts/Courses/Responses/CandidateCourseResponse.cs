using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Persistence.Customization.FileStorage.Clients.Private;
using Common = Contracts.Candidate.Courses.Responses;

namespace Candidates.Application.Contracts.Courses.Responses
{
    public class CandidateCourseResponse : Common.CandidateCourseResponse
    {
        public static Common.CandidateCourseResponse FromDomain(
            CandidateCourse candidateCourse,
            IPrivateBlobClient privateBlobClient)
        {
            return new Common.CandidateCourseResponse
            {
                Id = candidateCourse.Id,
                Name = candidateCourse.Name,
                IssuingOrganization = candidateCourse.IssuingOrganization,
                Description = candidateCourse.Description,
                Certificate = DocumentResponse.FromDomain(candidateCourse.Certificate, privateBlobClient)
            };
        }
    }
}
