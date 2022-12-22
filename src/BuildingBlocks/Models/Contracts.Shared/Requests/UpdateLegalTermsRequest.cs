namespace Contracts.Shared.Requests
{
    public class UpdateLegalTermsRequest
    {
        public bool TermsAgreement { get; set; }
        public bool MarketingAgreement { get; set; }
    }
}
