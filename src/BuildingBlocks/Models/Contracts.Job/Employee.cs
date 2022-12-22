namespace Contracts.Job
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? PictureUri { get; set; }
    }
}
