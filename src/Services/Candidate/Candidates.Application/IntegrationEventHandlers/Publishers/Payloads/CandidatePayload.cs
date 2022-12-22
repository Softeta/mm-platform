using Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Candidate;
using Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Shared;
using Candidates.Domain.Aggregates.CandidateAggregate;
using Domain.Seedwork.Enums;

namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads
{
    internal class CandidatePayload
    {
        public Guid Id { get; set; }
        public Guid? ExternalId { get; set; }
        public Email? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PictureUri { get; set; }
        public Position? CurrentPosition { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
        public bool OpenForOpportunities { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? PersonalWebsiteUrl { get; set; }
        public LivingAddress? Address { get; set; }
        public Terms? Terms { get; set; }
        public Phone? Phone { get; set; }
        public CandidateStatus Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public SystemLanguage? SystemLanguage { get; set; }
        public string? Bio { get; set; }
        public Document? CurriculumVitae { get; set; }
        public Document? Video { get; set; }
        public Image? Picture { get; set; }
        public IEnumerable<Industry> Industries { get; set; } = new List<Industry>();
        public IEnumerable<DesiredSkill> DesiredSkills { get; set; } = new List<DesiredSkill>();
        public IEnumerable<Skill> Skills { get; set; } = new List<Skill>();
        public IEnumerable<Language> Languages { get; set; } = new List<Language>();
        public IEnumerable<WorkLocation> WorkLocations { get; set; } = new List<WorkLocation>();
        public IEnumerable<Course> Courses { get; set; } = new List<Course>();
        public IEnumerable<Education> Educations { get; set; } = new List<Education>();
        public IEnumerable<Hobby> Hobbies { get; set; } = new List<Hobby>();
        public IEnumerable<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();
        public bool MarketingAcknowledgement { get; set; }

        public static CandidatePayload FromDomain(Candidate candidate)
        {
            return new CandidatePayload
            {
                Id = candidate.Id,
                ExternalId = candidate.ExternalId,
                Email = Email.FromDomain(candidate.Email),
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                PictureUri = candidate.Picture?.ThumbnailUri,
                CurrentPosition = candidate.CurrentPosition != null ? Position.FromDomain(candidate.CurrentPosition) : null,
                BirthDate = candidate.BirthDate,
                OpenForOpportunities = candidate.OpenForOpportunities,
                LinkedInUrl = candidate.LinkedInUrl,
                PersonalWebsiteUrl = candidate.PersonalWebsiteUrl,
                Terms = Terms.FromDomain(candidate.Terms),
                Address = LivingAddress.FromAddress(candidate.Address),
                Phone = new Phone(candidate.Phone?.PhoneNumber),
                Status = candidate.Status,
                CreatedAt = candidate.CreatedAt,
                SystemLanguage = candidate.SystemLanguage,
                Bio = candidate.Bio,
                CurriculumVitae = new Document(candidate.CurriculumVitae?.Uri, candidate.CurriculumVitae?.FileName),
                Video = new Document(candidate.Video?.Uri, candidate.Video?.FileName),
                Picture = candidate.Picture != null ? new Image(candidate.Picture.OriginalUri, candidate.Picture.ThumbnailUri) : null,
                Industries = candidate.Industries.Select(x => new Industry(x.Code, x.CreatedAt)),
                Skills = candidate.Skills.Select(Skill.FromDomain),
                DesiredSkills = candidate.Skills.Select(x => new DesiredSkill(x.Code, x.CreatedAt)),
                Languages = candidate.Languages.Select(x => new Language(x.Language.Id, x.Language.Code, x.Language.Name, x.CreatedAt)),
                Educations = candidate.Educations.Select(Education.FromDomain),
                Courses = candidate.Courses.Select(Course.FromDomain),
                Hobbies = candidate.Hobbies.Select(x => new Hobby(x.Code, x.CreatedAt)),
                WorkExperiences = candidate.WorkExperiences.Select(WorkExperience.FromDomain),
                MarketingAcknowledgement = candidate.MarketingAcknowledgement?.Agreed ?? false
            };
        }
    }
}
