using Domain.Seedwork.Enums;
using EmailService.Send.Commands.Base;
using EmailService.Send.Events.JobCandidates;

namespace EmailService.Send.Commands.JobCandidateReceiver
{
    internal class SendJobCandidateInvitedCommand : SendEmailBaseCommand
    {
        public SendJobCandidateInvitedCommand(
           string filterName,
           JobCandidateChangedPayload payload) : base(
               filterName,
               payload.CandidateId, 
               payload.CandidateId, 
               payload.Email,
               payload.SystemLanguage)
        {
            Payload = payload;
        }

        public JobCandidateChangedPayload Payload { get; set; }
    }
}
