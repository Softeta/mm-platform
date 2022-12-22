using Domain.Seedwork.Enums;
using System.Collections.Generic;
using System;
using ElasticSearch.Sync.Events.Models.Shared;

namespace ElasticSearch.Sync.Events.Models.JobChanged;

internal class JobChangedPayload
{
    public Guid JobId { get; set; }
    public Position Position { get; set; } = null!;
    public JobStage Stage { get; set; }
    public IEnumerable<Skill> Skills { get; set; } = new List<Skill>();
    public IEnumerable<Industry> Industries { get; set; } = new List<Industry>();
    public IEnumerable<Seniority> Seniorities { get; set; } = new List<Seniority>();
    public IEnumerable<Language> Languages { get; set; } = new List<Language>();
    public string? Location { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public Terms? Terms { get; set; }
    public bool IsPublished { get; set; }
}