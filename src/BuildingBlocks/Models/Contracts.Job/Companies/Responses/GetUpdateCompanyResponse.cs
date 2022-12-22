namespace Contracts.Job.Companies.Responses
{
    public class GetUpdateCompanyResponse
    {
        public Guid JobId { get; set; }
        public CompanyResponse Company { get; set; } = null!;
    }
}
