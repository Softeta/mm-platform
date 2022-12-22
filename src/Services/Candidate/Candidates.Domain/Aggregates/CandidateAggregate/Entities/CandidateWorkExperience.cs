using Candidates.Domain.Aggregates.CandidateAggregate.Services;
using Domain.Seedwork;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;

namespace Candidates.Domain.Aggregates.CandidateAggregate.Entities
{
    public class CandidateWorkExperience : Entity
    {
        public Guid CandidateId { get; private set; }
        public WorkExperienceType Type { get; private set; }
        public string CompanyName { get; private set; } = null!;
        public Position Position { get; private set; } = null!;
        public DateRange Period { get; private set; } = null!;
        public string? JobDescription { get; private set; }
        public bool IsCurrentJob { get; private set; }

        private readonly List<CandidateWorkExperienceSkill> _skills = new();
        public IReadOnlyList<CandidateWorkExperienceSkill> Skills => _skills;

        public CandidateWorkExperience() 
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public void Create(
            Guid candidateId,
            WorkExperienceType type,
            string companyName,
            Guid positionId,
            string positionCode,
            Guid? positionAliasToId,
            string? positionAliasToCode,
            DateTimeOffset from,
            DateTimeOffset? to,
            string? jobDescription,
            bool isCurrentJob,
            IEnumerable<CandidateWorkExperienceSkill> skills)
        {
            CandidateId = candidateId;
            Type = type;
            CompanyName = companyName;
            Position = new Position(
                positionId,
                positionCode,
                positionAliasToId,
                positionAliasToCode);
            Period = new DateRange(from, to);
            JobDescription = jobDescription;
            IsCurrentJob = isCurrentJob;
            _skills.AddRange(skills);

            Validate();
        }

        public void Update(
            WorkExperienceType type,
            string companyName,
            Guid positionId,
            string positionCode,
            Guid? positionAliasToId,
            string? positionAliasToCode,
            DateTimeOffset from,
            DateTimeOffset? to,
            string? jobDescription,
            bool isCurrentJob,
            IEnumerable<CandidateWorkExperienceSkill> skills)
        {
            Type = type;
            CompanyName = companyName;
            Position = new Position(
                positionId, 
                positionCode, 
                positionAliasToId, 
                positionAliasToCode);
            Period = new DateRange(from, to);
            JobDescription = jobDescription;
            IsCurrentJob = isCurrentJob;
            _skills.Calibrate(skills, Id);

            Validate();
        }

        public void SyncSkill(Guid id, Guid? aliasId, string? aliasCode)
        {
            var skill = _skills.SingleOrDefault(x => x.SkillId == id);
            if (skill is not null)
            {
                skill.Sync(aliasId, aliasCode);
            }
        }

        public void SyncJobPosition(Guid? aliasId, string? aliasCode)
        {
            Position = new Position(Position.Id, Position.Code, aliasId, aliasCode);
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(CompanyName))
            {
                throw new DomainException($"{nameof(CompanyName)} should be filled in",
                    ErrorCodes.Candidate.WorkExperience.CompanyNameRequired);
            }

            if (string.IsNullOrWhiteSpace(Position.Code))
            {
                throw new DomainException($"{nameof(Position)} should be filled in",
                    ErrorCodes.Candidate.WorkExperience.PositionRequired);
            }
        }
    }
}
