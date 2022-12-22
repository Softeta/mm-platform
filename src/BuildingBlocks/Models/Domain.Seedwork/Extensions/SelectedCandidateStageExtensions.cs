using Domain.Seedwork.Enums;

namespace Domain.Seedwork.Extensions
{
    public static class SelectedCandidateStageExtensions
    {
        public static bool IsShortlisted(this SelectedCandidateStage stage)
        {
            return stage is
                SelectedCandidateStage.NoInterview or
                SelectedCandidateStage.FirstInterview or
                SelectedCandidateStage.SecondInterview or
                SelectedCandidateStage.ThirdInterview;
        }
    }
}
