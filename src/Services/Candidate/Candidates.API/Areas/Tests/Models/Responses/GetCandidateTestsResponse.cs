using Candidates.Domain.Aggregates.CandidateTestsAggregate;
using Persistence.Customization.FileStorage.Clients.Private;

namespace Candidates.API.Areas.Tests.Models.Responses
{
    public class GetCandidateTestsResponse
    {
        public GetAssessmentResponse<LogicalScoresResponse>? LogicalAssessment { get; set; }
        public GetAssessmentResponse<PersonalityScoresResponse>? PersonalityAssessment { get; set; }
        public RaportResponse? PapiDynamicWheel { get; set; }
        public RaportResponse? PapiGeneralFeedback { get; set; }
        public RaportResponse? LgiGeneralFeedback { get; set; }

        public static GetCandidateTestsResponse? FromDomain(CandidateTests? candidateTests, IPrivateBlobClient privateBlobClient)
        {
            if (candidateTests is null) return null;

            return new GetCandidateTestsResponse
            {
                LogicalAssessment = GetAssessmentResponse<LogicalScoresResponse>.FromDomain(candidateTests.LogicalAssessment),
                PersonalityAssessment = GetAssessmentResponse<PersonalityScoresResponse>.FromDomain(candidateTests.PersonalityAssessment),
                PapiDynamicWheel = RaportResponse.FromDomain(candidateTests.PapiDynamicWheel, privateBlobClient),
                PapiGeneralFeedback = RaportResponse.FromDomain(candidateTests.PapiGeneralFeedback, privateBlobClient),
                LgiGeneralFeedback = RaportResponse.FromDomain(candidateTests.LgiGeneralFeedback, privateBlobClient)
            };
        }
    }
}
