using Domain.Seedwork.Enums;
using System.Collections.Generic;
using System.Linq;

namespace ElasticSearch.Sync.Commands;

internal class BaseJobDocumentHandler
{
    private static readonly IEnumerable<JobStage> _syncStages = new List<JobStage>()
    {
        JobStage.Calibration,
        JobStage.CandidateSelection,
        JobStage.ShortListed,
        JobStage.Successful,
        JobStage.OnHold,
        JobStage.Lost
    };

    protected static bool IsValid(JobStage jobStage)
    {
        return _syncStages.Contains(jobStage);
    }
}