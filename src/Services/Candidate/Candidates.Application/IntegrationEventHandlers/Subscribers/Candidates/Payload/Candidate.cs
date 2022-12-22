using Candidates.Application.IntegrationEventHandlers.Subscribers.Candidates.Payload.Models;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Candidates.Payload
{
    public class Candidate
    {
        public Guid Id { get; set; }
        public Guid? ExternalId { get; set; }
        public Document? CurriculumVitae { get; set; }
        public Document? Video { get; set; }
        public Image? Picture { get; set; }
        public IEnumerable<Course> Courses { get; set; } = new List<Course>();
        public IEnumerable<Education> Educations { get; set; } = new List<Education>();
    }
}
