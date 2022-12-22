using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities;
using Candidates.Domain.Aggregates.CandidateTestsAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Customization.Context;

namespace Candidates.Infrastructure.Persistence
{
    public sealed class CandidateContext : BaseContext<CandidateContext>, ICandidateContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateSkill> CandidateSkills { get; set; }
        public DbSet<CandidateDesiredSkill> CandidateDesiredSkills { get; set; }
        public DbSet<CandidateCourse> CandidateCourses { get; set; }
        public DbSet<CandidateEducation> CandidateEducations { get; set; }
        public DbSet<CandidateWorkExperienceSkill> CandidateWorkExperienceSkills { get; set; }
        public DbSet<CandidateWorkExperience> CandidateWorkExperiences { get; set; }
        public DbSet<CandidateShortListedJob> CandidateShortListedJobs { get; set; }
        public DbSet<CandidateActivityStatus> CandidateActivityStatuses { get; set; }

        public DbSet<CandidateJobs> CandidateJobs { get; set; }
        public DbSet<CandidateSelectedInJob> CandidateSelectedInJobs { get; set; }
        public DbSet<CandidateArchivedInJob> CandidateArchivedInJobs { get; set; }
        public DbSet<CandidateAppliedInJob> CandidateAppliedInJobs { get; set; }

        public DbSet<CandidateTests> CandidateTests { get; set; }

        public CandidateContext(DbContextOptions<CandidateContext> options, IMediator mediator) :
            base(options, mediator)
        {
            Candidates = Set<Candidate>();
            CandidateSkills = Set<CandidateSkill>();
            CandidateDesiredSkills = Set<CandidateDesiredSkill>();
            CandidateCourses = Set<CandidateCourse>();
            CandidateEducations = Set<CandidateEducation>();
            CandidateWorkExperienceSkills = Set<CandidateWorkExperienceSkill>();
            CandidateWorkExperiences = Set<CandidateWorkExperience>();
            CandidateShortListedJobs = Set<CandidateShortListedJob>();
            CandidateActivityStatuses = Set<CandidateActivityStatus>();

            CandidateJobs = Set<CandidateJobs>();
            CandidateSelectedInJobs = Set<CandidateSelectedInJob>();
            CandidateArchivedInJobs = Set<CandidateArchivedInJob>();
            CandidateAppliedInJobs = Set<CandidateAppliedInJob>();

            CandidateTests = Set<CandidateTests>();
        }
    }
}
