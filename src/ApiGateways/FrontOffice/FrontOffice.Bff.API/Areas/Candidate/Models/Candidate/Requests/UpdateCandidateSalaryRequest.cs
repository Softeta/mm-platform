namespace FrontOffice.Bff.API.Areas.Candidate.Models.Candidate.Requests
{
    public class UpdateCandidateSalaryRequest
    {
        public string? Currency { get; set; }

        public decimal? FreelanceHourlySalary { get; set; }

        public decimal? FreelanceMonthlySalary { get; set; }

        public decimal? FullTimeMonthlySalary { get; set; }

    }
}
