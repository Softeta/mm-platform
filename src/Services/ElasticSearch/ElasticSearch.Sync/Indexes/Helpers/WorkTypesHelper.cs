using Domain.Seedwork.Enums;
using ElasticSearch.Sync.Events.Models.Shared;
using System.Collections.Generic;

namespace ElasticSearch.Sync.Indexes.Helpers
{
    internal static class WorkTypesHelper
    {
        public static ICollection<string> Collect(FreelanceContract? freelance, PermanentContract? permanent)
        {
            var workTypes = new List<string>();

            if (freelance != null)
            {
                workTypes.Add(WorkType.Freelance.ToString());
            }

            if (permanent != null)
            {
                workTypes.Add(WorkType.Permanent.ToString());
            }

            return workTypes;
        }
    }
}
