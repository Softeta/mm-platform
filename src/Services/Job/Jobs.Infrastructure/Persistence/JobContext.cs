using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Customization.Context;

namespace Jobs.Infrastructure.Persistence
{
    public sealed class JobContext : BaseContext<JobContext>, IJobContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobAssignedEmployee> JobAssignedEmployees { get; set; }
        public DbSet<JobLanguage> JobLanguages { get; set; }
        public DbSet<JobSeniority> JobSeniorities { get; set; }
        public DbSet<JobSkill> JobSkills { get; set; }
        public DbSet<JobIndustry> JobIndustries { get; set; }
        public DbSet<JobContactPerson> JobContactPersons { get; set; }
        public DbSet<JobInterestedCandidate> JobInterestedCandidates { get; set; }
        public DbSet<JobInterestedLinkedIn> JobInterestedLinkedInCandidates { get; set; }

        public DbSet<JobCandidates> JobCandidates { get; set; }
        public DbSet<JobSelectedCandidate> JobSelectedCandidates { get; set; }
        public DbSet<JobArchivedCandidate> JobArchivedCandidates { get; set; }

        public DbSet<JobCandidatesFilter> JobCandidatesFilters { get; set; }

        public JobContext(DbContextOptions<JobContext> options, IMediator mediator) : base(options, mediator)
        {
            Jobs = Set<Job>();
            JobAssignedEmployees = Set<JobAssignedEmployee>();
            JobLanguages = Set<JobLanguage>();
            JobSeniorities = Set<JobSeniority>();
            JobSkills = Set<JobSkill>();
            JobIndustries = Set<JobIndustry>();
            JobContactPersons = Set<JobContactPerson>();
            JobInterestedCandidates = Set<JobInterestedCandidate>();
            JobInterestedLinkedInCandidates = Set<JobInterestedLinkedIn>();

            JobCandidates = Set<JobCandidates>();
            JobSelectedCandidates = Set<JobSelectedCandidate>();
            JobArchivedCandidates = Set<JobArchivedCandidate>();

            JobCandidatesFilters = Set<JobCandidatesFilter>();
        }
    }
}
