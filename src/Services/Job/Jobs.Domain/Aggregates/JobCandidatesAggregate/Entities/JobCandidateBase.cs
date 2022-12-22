using Domain.Seedwork;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Shared.ValueObjects;

namespace Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities
{
    public abstract class JobCandidateBase : Entity
    {
        public Guid JobId { get; protected set; }
        public Guid CandidateId { get; protected set; }
        public string FirstName { get; protected set; } = null!;
        public string LastName { get; protected set; } = null!;
        public string? Email { get; protected set; }
        public string? PhoneNumber { get; protected set; }
        public string? PictureUri { get; protected set; }
        public Position? Position { get; protected set; }
        public string? Brief { get; protected set; }
        public DateTimeOffset? InvitedAt { get; protected set; }
        public bool HasApplied { get; protected set; }
        public SystemLanguage? SystemLanguage { get; protected set; }

        public virtual void UpdateBrief(string? brief)
        {
            Brief = brief;
        }

        public virtual void Sync(
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            Guid? positionId,
            string? positionCode,
            Guid? positionAliasToId,
            string? positionAliasToCode,
            SystemLanguage? systemLanguage)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            PictureUri = pictureUri;
            Position = Position.Create(
                positionId, 
                positionCode, 
                positionAliasToId, 
                positionAliasToCode);
            SystemLanguage = systemLanguage;
        }

        public void SyncJobPosition(Guid? aliasId, string? aliasCode)
        {
            if (Position != null)
            {
                Position = new Position(Position.Id, Position.Code, aliasId, aliasCode);
            }            
        }
    }
}
