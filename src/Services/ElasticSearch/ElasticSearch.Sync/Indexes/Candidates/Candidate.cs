using Domain.Seedwork.Enums;
using ElasticSearch.Sync.Events.Models.CandidateChanged;
using ElasticSearch.Sync.Events.Models.Shared;
using ElasticSearch.Sync.Indexes.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace ElasticSearch.Sync.Indexes.Candidates
{
    internal class Candidate
    {
        public string Id { get; set; } = null!;
        public string? CurrentPosition { get; set; }
        public ICollection<string> Skills { get; set; } = new List<string>();
        public string? Seniority { get; set; }
        public ICollection<string> WorkTypes { get; set; } = new List<string>();
        public ICollection<string> WorkingHourTypes { get; set; } = new List<string>();
        public ICollection<string> WorkingFormats { get; set; } = new List<string>();
        public ICollection<string> Industries { get; set; } = new List<string>();
        public ICollection<string> Languages { get; set; } = new List<string>();
        public string? Location { get; set; }

        public static Candidate FromEvent(CandidateChangedPayload payload)
        {
            return new Candidate
            {
                Id = payload.Id.ToString(),
                CurrentPosition = payload.CurrentPosition?.AliasTo?.Code ?? payload.CurrentPosition?.Code,
                Skills = CollectUniqueSkills(payload),
                Seniority = payload.Seniority?.ToString(),
                WorkTypes = WorkTypesHelper.Collect(payload.Terms?.Freelance, payload.Terms?.Permanent),
                WorkingHourTypes = WorkingHoursHelper.Collect(payload.Terms?.FullTimeWorkingHours, payload.Terms?.PartTimeWorkingHours),
                WorkingFormats = WorkFormatsHelper.Collect(payload.Terms?.Formats),
                Industries = payload.Industries
                    .Select(x => x.Code)
                    .ToList(),
                Languages = payload.Languages
                    .Select(x => x.Code)
                    .ToList(),
                Location = payload.Address?.Location
            };
        }

        private static ICollection<string> CollectUniqueSkills(CandidateChangedPayload payload)
        {
            var workExperienceSkills = payload.WorkExperiences
                .Where(w => w.Skills != null)
                .SelectMany(w => w.Skills!.Select(s => GetCodeHelper.GetSkillCode(s)))
                .ToList();

            return payload.Skills
                .Select(s => GetCodeHelper.GetSkillCode(s))
                .Concat(workExperienceSkills)
                .Distinct()
                .ToList();
        }
    }
}
