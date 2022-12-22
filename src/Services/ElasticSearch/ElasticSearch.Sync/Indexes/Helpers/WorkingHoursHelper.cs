using ElasticSearch.Sync.Events.Models.Shared;
using System.Collections.Generic;

namespace ElasticSearch.Sync.Indexes.Helpers
{
    internal static class WorkingHoursHelper
    {
        public static ICollection<string> Collect(
            FullTimeWorkingHours? fullTimeWorkingHours,
            PartTimeWorkingHours? partTimeWorkingHours)
        {
            var workingHours = new List<string>();

            if (fullTimeWorkingHours is not null)
            {
                workingHours.Add(fullTimeWorkingHours.Type.ToString());
            }
            if (partTimeWorkingHours is not null)
            {
                workingHours.Add(partTimeWorkingHours.Type.ToString());
            }

            return workingHours;
        }
    }
}
