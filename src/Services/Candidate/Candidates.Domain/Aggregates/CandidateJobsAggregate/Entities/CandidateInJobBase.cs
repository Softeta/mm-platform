using Domain.Seedwork.Attributes;
using Domain.Seedwork.Shared.ValueObjects;

namespace Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities
{
    public class CandidateInJobBase : JobInformationBase
    {
        public string? CoverLetter { get; protected set; }

        public Document? MotivationVideo { get; protected set; }

        [ProjectionField]
        public DateTimeOffset? InvitedAt { get; protected set; }

        [ProjectionField]
        public bool HasApplied { get; protected set; }

        public virtual void SyncCandidateJobAttachments(Document? motivationVideo, string? coverLetter)
        {
            CoverLetter = coverLetter;
            MotivationVideo = motivationVideo;
        }

        public virtual void SyncCandidateJobInformation(DateTimeOffset? invitedAt, bool hasApplied)
        {
            InvitedAt = invitedAt;
            HasApplied = hasApplied;
        }

        public void RemoveMotivationVideo()
        {
            MotivationVideo = null;
        }
    }
}
