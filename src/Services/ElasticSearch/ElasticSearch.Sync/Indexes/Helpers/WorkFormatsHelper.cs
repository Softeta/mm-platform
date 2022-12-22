using ElasticSearch.Sync.Events.Models.Shared;
using System.Collections.Generic;

namespace ElasticSearch.Sync.Indexes.Helpers
{
    internal static class WorkFormatsHelper
    {
        public static ICollection<string> Collect(Formats? formats)
        {
            var workFormats = new List<string>();
            if (formats is null)
            {
                return workFormats;
            }

            if (formats.IsRemote)
            {
                workFormats.Add(nameof(formats.IsRemote));
            }
            if (formats.IsHybrid)
            {
                workFormats.Add(nameof(formats.IsHybrid));
            }
            if (formats.IsOnSite)
            {
                workFormats.Add(nameof(formats.IsOnSite));
            }

            return workFormats;
        }
    }
}
