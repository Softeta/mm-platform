using FrontOffice.Bff.API.Areas.Company.Models.Shared;

namespace FrontOffice.Bff.API.Areas.Company.Models.Jobs
{
    public class Step1Request : InitializeJobRequest
    {
        public Guid CompanyId { get; set; }
    }
}
