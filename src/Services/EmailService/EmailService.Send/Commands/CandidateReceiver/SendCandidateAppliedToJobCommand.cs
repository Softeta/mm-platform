using Domain.Seedwork.Enums;
using EmailService.Send.Commands.Base;
using EmailService.Send.Events.CandidateJobs.Models;
using System;

namespace EmailService.Send.Commands.CandidateReceiver
{
    internal class SendCandidateAppliedToJobCommand : SendEmailBaseCommand
    {
        public SendCandidateAppliedToJobCommand(
           string filterName,
           CandidateAppliedInJob appliedToJob,
           string? firstName,
           string? emailAddress,
           SystemLanguage? systemLanguage) : base(
               filterName, 
               appliedToJob.CandidateId, 
               appliedToJob.JobId,
               emailAddress, systemLanguage)
        {
            AppliedToJob = appliedToJob;
            FirstName = firstName;
        }

        public CandidateAppliedInJob AppliedToJob { get; set; }
        public string? FirstName { get; set; }
    }
}
