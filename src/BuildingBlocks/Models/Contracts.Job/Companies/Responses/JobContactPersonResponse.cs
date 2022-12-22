using Contracts.Shared;
using Contracts.Shared.Responses;

namespace Contracts.Job.Companies.Responses
{
    public class JobContactPersonResponse
    {
        public Guid Id { get; set; }

        public bool IsMainContact { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public Position? Position { get; set; }

        public string? PhoneNumber { get; set; }

        public string Email { get; set; } = null!;

        public ImageResponse? Picture { get; set; }
    }
}
