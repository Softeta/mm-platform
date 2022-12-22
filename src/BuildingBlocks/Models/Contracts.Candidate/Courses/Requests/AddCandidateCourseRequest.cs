using Contracts.Shared.Requests;

namespace Contracts.Candidate.Courses.Requests
{
    public class AddCandidateCourseRequest : CandidateCourseRequestBase
    {
        public AddFileCacheRequest? Certificate { get; set; }
    }
}
