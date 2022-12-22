namespace BackOffice.Bff.API.Models.Job.Requests
{
    public class AddJobContactPersonRequest
    {
        public Guid Id { get; set; }

        public bool IsMainContact { get; set; }
    }
}
