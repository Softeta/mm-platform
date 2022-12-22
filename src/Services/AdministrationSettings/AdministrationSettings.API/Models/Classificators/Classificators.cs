using Domain.Seedwork.Enums;

namespace AdministrationSettings.API.Models.Classificators
{
    public class Classificators
    {
        public IEnumerable<WorkType> WorkTypes { get; set; } = new List<WorkType>();
        public IEnumerable<FormatType> FormatTypes { get; set; } = new List<FormatType>();
        public IEnumerable<SeniorityLevel> SeniorityLevels { get; set; } = new List<SeniorityLevel>();
        public IEnumerable<WorkingHoursType> WorkingHourTypes { get; set; } = new List<WorkingHoursType>();
        public IEnumerable<CompanyWorkingHoursType> CompanyWorkingHourTypes { get; set; } = new List<CompanyWorkingHoursType>();
        public IEnumerable<JobStage> JobStages { get; set; } = new List<JobStage>();
        public IEnumerable<SelectedCandidateStage> SelectedCandidateStages { get; set; } = new List<SelectedCandidateStage>();
        public IEnumerable<ArchivedCandidateStage> ArchivedCandidateStages { get; set; } = new List<ArchivedCandidateStage>();
        public IEnumerable<ActivityStatus> ActivityStatuses { get; set; } = new List<ActivityStatus>();
        public IEnumerable<SystemLanguage> SystemLanguages { get; set; } = new List<SystemLanguage>();
    }
}
