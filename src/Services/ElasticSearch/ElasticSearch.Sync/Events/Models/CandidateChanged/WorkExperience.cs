using System.Collections.Generic;
using ElasticSearch.Sync.Events.Models.Shared;

namespace ElasticSearch.Sync.Events.Models.CandidateChanged
{
    internal class WorkExperience
    {
        public IEnumerable<Skill>? Skills { get; set; }
        public Position? Position { get; set; }
    }
}
