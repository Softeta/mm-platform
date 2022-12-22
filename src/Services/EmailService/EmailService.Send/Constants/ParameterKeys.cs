namespace EmailService.Send.Constants
{
    internal class ParameterKeys
    {
        internal class AskedForApprovalParameterKeys
        {
            public const string Key = "Key";
        }

        internal class EmailVerificationParameterKeys
        {
            public const string Url = "Url";
        }

        internal class CandidateWelcomeParameterKeys
        {
            public const string Url = "Url";
            public const string CandidateFirstName = "CandidateFirstname";
        }

        internal class EmailToContactPersonParameterKeys
        {
            public const string Url = "Url";
            public const string ContactFirstName = "ContactFirstName";
            public const string CompanyName = "CompanyName";
        }

        internal class JobCandidateInvitedParameterKeys
        {
            public const string Url = "Url";
            public const string JobPosition = "JobPosition";
            public const string CompanyName = "CompanyName";
            public const string CandidateFirstName = "CandidateFirstname";
        }

        internal class JobApprovedParameterKeys
        {
            public const string JobPosition = "JobPosition";
            public const string CompanyName = "CompanyName";
            public const string ContactFirstName = "ContactFirstName";
        }

        internal class JobRejectedParameterKeys
        {
            public const string JobPosition = "JobPosition";
            public const string ContactFirstName = "ContactFirstName";
        }

        internal class JobCandidatesShortlistActivatedParameterKeys
        {
            public const string Url = "Url";
            public const string JobPosition = "JobPosition";
            public const string ContactFirstName = "ContactFirstName";
        }

        internal class CandidateAppliedToJobParameterKeys
        {
            public const string CandidateFirstName = "CandidateFirstname";
            public const string JobPosition = "JobPosition";
            public const string CompanyName = "CompanyName";
        }

        internal class JobSubmittedParameterKeys
        {
            public const string ContactFirstName = "ContactFirstName";
            public const string JobPosition = "JobPosition";
        }
    }
}
