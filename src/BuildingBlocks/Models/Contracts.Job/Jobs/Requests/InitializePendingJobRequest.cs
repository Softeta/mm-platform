using Contracts.Job.Companies.Requests;
using Contracts.Shared;
using Domain.Seedwork.Enums;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Job.Jobs.Requests
{
    public class InitializePendingJobRequest
    {
        [Required]
        public CreateJobCompanyRequest Company { get; set; } = null!;

        [Required]
        public Position Position { get; set; } = null!;

        public DateTimeOffset? StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public bool IsUrgent { get; set; }

        public ICollection<WorkType> WorkTypes { get; set; } = new List<WorkType>();
    }
}
