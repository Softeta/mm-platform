using Domain.Seedwork.Enums;

namespace Domain.Seedwork.Extensions
{
    public static class JobStageExtensions
    {
        private static JobStage[] _archived = new[] { JobStage.Successful, JobStage.Lost, JobStage.OnHold };

        public static bool IsArchived(this JobStage stage)
        {
            return _archived.Contains(stage);
        }
    }
}
