using Contracts.Candidate.Candidates.Responses;
using FrontOffice.Bff.API.Areas.Jobs.Models.Filters;

namespace FrontOffice.Bff.API.Areas.Jobs.Models.ElasticSearch.Requests
{
    public class JobsSearchRequest
    {
        public string? Position { get; set; }
        public string? Location { get; set; }
        public ICollection<string> Skills { get; set; } = new List<string>();
        public ICollection<string> WorkTypes { get; set; } = new List<string>();
        public ICollection<string> WorkingHourTypes { get; set; } = new List<string>();
        public ICollection<string> WorkingFormats { get; set; } = new List<string>();
        public ICollection<string> Industries { get; set; } = new List<string>();
        public ICollection<string> Languages { get; set; } = new List<string>();

        public static JobsSearchRequest FromCandidate(GetCandidateResponse candidate)
        {
            return new JobsSearchRequest
            {
                Position = candidate.CurrentPosition?.Code,
                Skills = candidate.Skills
                    .Select(x => x.Code)
                    .ToList(),
                WorkTypes = candidate.WorkTypes
                    .Select(x => x.ToString())
                    .ToList(),
                WorkingHourTypes = candidate.WorkingHourTypes
                    .Select(x => x.ToString())
                    .ToList(),
                WorkingFormats = candidate.Formats
                    .Select(x => x.ToString())
                    .ToList(),
                Industries = candidate.Industries
                    .Select(x => x.Code)
                    .ToList(),
                Languages = candidate.Languages
                    .Select(x => x.Code)
                    .ToList(),
                Location = candidate.Address?.Location
            };
        }

        public static JobsSearchRequest FromFilter(GetRecommendedJobsRequest request)
        {
            return new JobsSearchRequest
            {
                Position = request.Position,
                Skills = request.Skills,
                WorkTypes = request.WorkTypes,
                WorkingHourTypes = request.WorkingHourTypes,
                WorkingFormats = request.WorkingFormats,
                Industries = request.Industries,
                Languages = request.Languages,
                Location = request.Location
            };
        }
    }
}
