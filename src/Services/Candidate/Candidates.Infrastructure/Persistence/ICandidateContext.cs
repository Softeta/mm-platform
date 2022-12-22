using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities;
using Candidates.Domain.Aggregates.CandidateTestsAggregate;
using Microsoft.EntityFrameworkCore;

namespace Candidates.Infrastructure.Persistence
{
    public interface ICandidateContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateSkill> CandidateSkills { get; set; }
        public DbSet<CandidateDesiredSkill> CandidateDesiredSkills { get; set; }
        public DbSet<CandidateCourse> CandidateCourses { get; set; }
        public DbSet<CandidateEducation> CandidateEducations { get; set; }
        public DbSet<CandidateWorkExperienceSkill> CandidateWorkExperienceSkills { get; set; }
        public DbSet<CandidateWorkExperience> CandidateWorkExperiences { get; set; }
        public DbSet<CandidateActivityStatus> CandidateActivityStatuses { get; set; }

        public DbSet<CandidateJobs> CandidateJobs { get; set; }
        public DbSet<CandidateSelectedInJob> CandidateSelectedInJobs { get; set; }
        public DbSet<CandidateArchivedInJob> CandidateArchivedInJobs { get; set; }
        public DbSet<CandidateAppliedInJob> CandidateAppliedInJobs { get; set; }

        public DbSet<CandidateTests> CandidateTests { get; set; }
    }
}
