using Domain.Seedwork.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Email = EmailService.Sync.Events.Models.Email;
using Language = Domain.Seedwork.Enums.SystemLanguage;
using Phone = EmailService.Sync.Events.Models.Phone;
using Position = EmailService.Sync.Events.Models.Position;

namespace EmailService.Sync.Events.Candidate;

public class CandidatePayload
{
    private readonly IReadOnlyDictionary<bool, int> _booleanValues = new ReadOnlyDictionary<bool, int>(
        new Dictionary<bool, int>()
        {
            { true, 1 },
            { false, 2 }
        });

    private readonly IReadOnlyDictionary<Language, int> _languageValues = new ReadOnlyDictionary<Language, int>(
        new Dictionary<Language, int>()
        {
            { Language.DA, 1 },
            { Language.SV, 2 },
            { Language.EN, 3 }
        });

    public Email Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public Position? CurrentPosition { get; set; }
    public bool OpenForOpportunities { get; set; }
    public Terms? Terms { get; set; }
    public Phone? Phone { get; set; }
    public CandidateStatus Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public Language? SystemLanguage { get; set; }
    public IEnumerable<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();
    public bool MarketingAcknowledgement { get; set; }

    public int OpenForOpportunitiesValue => _booleanValues[OpenForOpportunities];

    public int MarketingAcknowledgementValue => _booleanValues[MarketingAcknowledgement];

    public int StatusValue => _booleanValues[Status == CandidateStatus.Approved];

    public int SystemLanguageValue => _languageValues[SystemLanguage ?? Language.EN];
    
    public IEnumerable<string> WorkTypes
    {
        get
        {
            var list = new List<string>();
            if (Terms?.Freelance is not null)
            {
                list.Add("Freelance");
            }

            if (Terms?.Permanent is not null)
            {
                list.Add("Permanent");
            }

            return list;
        }
    }
}