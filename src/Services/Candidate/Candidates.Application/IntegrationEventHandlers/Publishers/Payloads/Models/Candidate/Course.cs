using Candidates.Domain.Aggregates.CandidateAggregate.Entities;

namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Candidate
{
    internal class Course
    {
        public string Name { get; set; } = null!;
        public string IssuingOrganization { get; set; } = null!;
        public string? Description { get; set; }
        public Document? Certificate { get; set; }

        public static Course FromDomain(CandidateCourse course)
        {
            return new Course
            {
                Name = course.Name,
                IssuingOrganization = course.IssuingOrganization,
                Description = course.Description,
                Certificate = new Document(course.Certificate?.Uri, course.Certificate?.FileName)
            };
        }
    }
}
