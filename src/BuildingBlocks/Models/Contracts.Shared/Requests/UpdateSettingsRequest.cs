using Domain.Seedwork.Enums;

namespace Contracts.Shared.Requests
{
    public class UpdateSettingsRequest
    {
        public SystemLanguage SystemLanguage { get; set; }

        public bool MarketingAcknowledgement { get; set; }
    }
}
