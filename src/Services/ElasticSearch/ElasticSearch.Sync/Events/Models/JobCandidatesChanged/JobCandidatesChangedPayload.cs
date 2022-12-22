using Domain.Seedwork.Enums;
using System;
using System.Collections.Generic;

namespace ElasticSearch.Sync.Events.Models.JobCandidatesChanged;

internal class JobCandidatesChangedPayload
{
    public Guid JobId { get; set; }
    public JobStage Stage { get; set; }
    public IEnumerable<SelectedCandidate>? SelectedCandidates { get; set; }

}