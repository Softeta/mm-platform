using Contracts.Shared.Requests;

namespace Contracts.Candidate.Courses.Requests
{
    public class UpdateCandidateCourseRequest : CandidateCourseRequestBase
    {
        public UpdateFileCacheRequest Certificate { get; set; } = null!;

    }
}
