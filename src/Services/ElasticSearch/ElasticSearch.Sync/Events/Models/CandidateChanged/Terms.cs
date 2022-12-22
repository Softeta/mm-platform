using ElasticSearch.Sync.Events.Models.Shared;
using System;

namespace ElasticSearch.Sync.Events.Models.CandidateChanged
{
    internal class Terms
    {
        public DateTimeOffset? StartDate { get; set; }
        public string? Currency { get; set; }
        public FreelanceContract? Freelance { get; set; }
        public PermanentContract? Permanent { get; set; }
        public Formats? Formats { get; set; }
        public PartTimeWorkingHours? PartTimeWorkingHours { get; set; }
        public FullTimeWorkingHours? FullTimeWorkingHours { get; set; }
    }
}
