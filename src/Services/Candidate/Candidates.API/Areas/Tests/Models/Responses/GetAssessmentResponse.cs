using Candidates.Domain.Aggregates.CandidateTestsAggregate.ValueObjects;
using Domain.Seedwork.Enums;

namespace Candidates.API.Areas.Tests.Models.Responses
{
    public class GetAssessmentResponse<T>
    {
        public string PackageInstanceId { get; set; } = null!;
        public string Url { get; set; } = null!;
        public AssessmentStatus Status { get; set; }
        public T? Scores { get; set; }

        public static GetAssessmentResponse<LogicalScoresResponse>? FromDomain(LogicalAssessment? assessment)
        {
            if (assessment is null) return null;

            return new GetAssessmentResponse<LogicalScoresResponse>
            {
                PackageInstanceId = assessment.PackageInstanceId,
                Url = assessment.Url,
                Status = assessment.Status,
                Scores = LogicalScoresResponse.FromDomain(assessment.Scores)
            };
        }

        public static GetAssessmentResponse<PersonalityScoresResponse>? FromDomain(PersonalityAssessment? assessment)
        {
            if (assessment is null) return null;

            return new GetAssessmentResponse<PersonalityScoresResponse>
            {
                PackageInstanceId = assessment.PackageInstanceId,
                Url = assessment.Url,
                Status = assessment.Status,
                Scores = PersonalityScoresResponse.FromDomain(assessment.Scores)
            };
        }
    }
}
