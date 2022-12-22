using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain.Seedwork.Enums;
using ElasticSearch.Sync.Events.Models.Shared;

namespace ElasticSearch.Sync.Events.Models.CandidateChanged
{
    internal class CandidateChangedPayload
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PictureUri { get; set; }
        public Position? CurrentPosition { get; set; }
        public bool OpenForOpportunities { get; set; }
        public SeniorityLevel? Seniority { get; set; }
        public LivingAddress? Address { get; set; }
        public Terms? Terms { get; set; }
        public bool IsShortlisted { get; set; }
        public CandidateStatus Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public IEnumerable<Industry> Industries { get; set; } = new Collection<Industry>();
        public IEnumerable<Skill> Skills { get; set; } = new Collection<Skill>();
        public IEnumerable<WorkExperience> WorkExperiences { get; set; } = new Collection<WorkExperience>();
        public IEnumerable<Language> Languages { get; set; } = new Collection<Language>();
    }
}
