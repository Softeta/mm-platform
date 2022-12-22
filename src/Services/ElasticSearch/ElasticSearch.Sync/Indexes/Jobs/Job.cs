using ElasticSearch.Sync.Events.Models.JobChanged;
using ElasticSearch.Sync.Indexes.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElasticSearch.Sync.Indexes.Jobs
{
    internal class Job
    {
        public string Id { get; set; } = null!;
        public string? Position { get; set; }
        public ICollection<string> Skills { get; set; } = new List<string>();
        public ICollection<string> Seniorities { get; set; } = new List<string>();
        public ICollection<string> WorkTypes { get; set; } = new List<string>();
        public ICollection<string> WorkingFormats { get; set; } = new List<string>();
        public ICollection<string> Industries { get; set; } = new List<string>();
        public ICollection<string> Languages { get; set; } = new List<string>();
        public string? Location { get; set; }
        public string Stage { get; set; } = null!;
        public bool? IsPublished { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public static Job FromEvent(JobChangedPayload payload)
        {
            return new Job
            {
                Id = payload.JobId.ToString(),
                Position = payload.Position?.AliasTo?.Code ?? payload.Position?.Code,
                Skills = payload.Skills
                    .Select(x => GetCodeHelper.GetSkillCode(x))
                    .ToList(),
                WorkTypes = WorkTypesHelper.Collect(payload.Terms?.Freelance, payload.Terms?.Permanent),
                Seniorities = payload.Seniorities
                    .Select(x => x.SeniorityLevel.ToString()!)
                    .ToList(),
                Languages = payload.Languages
                    .Select(x => x.Code)
                    .ToList(),
                Location = payload.Location,
                WorkingFormats = WorkFormatsHelper.Collect(payload.Terms?.Formats),
                Industries = payload.Industries
                    .Select(x => x.Code)
                    .ToList(),
                Stage = payload.Stage.ToString(),
                IsPublished = payload.IsPublished,
                CreatedAt = payload.CreatedAt
            };
        }
    }
}
