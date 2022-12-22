using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Infrastructure.Persistence
{
    public interface IJobContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobAssignedEmployee> JobAssignedEmployees { get; set; }
        public DbSet<JobLanguage> JobLanguages { get; set; }
        public DbSet<JobSeniority> JobSeniorities { get; set; }
        public DbSet<JobSkill> JobSkills { get; set; }
        public DbSet<JobIndustry> JobIndustries { get; set; }
        public DbSet<JobContactPerson> JobContactPersons { get; set; }

        public DbSet<JobCandidates> JobCandidates { get; set; }
        public DbSet<JobSelectedCandidate> JobSelectedCandidates { get; set; }
        public DbSet<JobArchivedCandidate> JobArchivedCandidates { get; set; }
    }
}
