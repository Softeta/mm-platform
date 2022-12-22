using Contracts.Shared;
using Domain.Seedwork.Enums;

namespace FrontOffice.Bff.API.Areas.Company.Models.Jobs
{
    public class Step5Request
    {
        public ICollection<Skill> Skills { get; set; } = new List<Skill>();

        public ICollection<Industry> Industries { get; set; } = new List<Industry>();

        public ICollection<Language> Languages { get; set; } = new List<Language>();

        public ICollection<SeniorityLevel> Seniorities { get; set; } = new List<SeniorityLevel>();
    }
}
