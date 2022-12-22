using System;
using System.Collections.Generic;
using Domain.Seedwork.Enums;
using ElasticSearch.Sync.Events.Models.Shared;

namespace ElasticSearch.Sync.Events.Models.JobChanged
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
