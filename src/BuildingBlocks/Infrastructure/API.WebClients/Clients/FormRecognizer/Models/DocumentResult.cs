using System.Text.RegularExpressions;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Domain.Seedwork.Consts;

namespace API.WebClients.Clients.FormRecognizer.Models;

public class DocumentResult
{
    private static readonly Regex DigitRegex = new(RegExpressions.DigitsOnly, RegexOptions.Compiled);

    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? LinkedInUrl { get; set; }
    public string?[] Languages { get; set; } = Array.Empty<string>();
    public string?[] Skills { get; set; } = Array.Empty<string>();
    public string?[] Courses { get; set; } = Array.Empty<string>();
    public string? Location { get; set; }
    public Experience[] Experiences { get; set; } = Array.Empty<Experience>();
    public Education[] Educations { get; set; } = Array.Empty<Education>();

    private DocumentResult()
    {
        
    }

    public static DocumentResult FromLinkedInModel(IReadOnlyDictionary<string, DocumentField> fields)
    {
        var document = new DocumentResult
        {
            FirstName = ParseString(fields, nameof(FirstName)),
            MiddleName = ParseString(fields, nameof(MiddleName)),
            LastName = ParseString(fields, nameof(LastName)),
            PhoneNumber = ParseString(fields, nameof(PhoneNumber)),
            Email = ParseString(fields, nameof(Email)),
            LinkedInUrl = ParseString(fields, nameof(LinkedInUrl)),
            Location = ParseString(fields, nameof(Location))
        };

        if (document.LinkedInUrl is not null)
        {
            document.LinkedInUrl = document.LinkedInUrl.Replace(" ", string.Empty);
        }

        if (fields.TryGetValue(nameof(Languages), out var languagesField))
        {
            document.Languages = languagesField.AsList()
                .Select(x => x.AsDictionary())
                .Select(x => ParseString(x, "Language")).ToArray();
        }

        if (fields.TryGetValue(nameof(Skills), out var skillsField))
        {
            document.Skills = skillsField.AsList()
                .Select(x => x.AsDictionary())
                .Select(x => ParseString(x, "Skill"))
                .ToArray();
        }

        if (fields.TryGetValue(nameof(Courses), out var coursesField))
        {
            document.Courses = coursesField.AsList()
                .Select(x => x.AsDictionary())
                .Select(x => ParseString(x, "Course"))
                .ToArray();
        }

        if (fields.TryGetValue(nameof(Experience), out var experienceField))
        {
            document.Experiences = experienceField.AsList().Select(x => x.AsDictionary()).Select(x => new Experience
            {
                Company = ParseString(x, "Company"),
                Position = ParseString(x, "Position"),
                Description = ParseString(x, "Description"),
                DateFrom = ParseDate(x, "DateFrom"),
                DateTo = ParseDate(x, "DateTo"),
                IsCurrentlyWorking = ParseString(x, "DateTo").ToLower().Equals("present")
            }).ToArray();
        }

        if (fields.TryGetValue(nameof(Education), out var educationField))
        {
            document.Educations = fields[nameof(Education)].AsList().Select(x => x.AsDictionary()).Select(x => new Education
            {
                School = ParseString(x, "School"),
                Degree = ParseString(x, "Degree"),
                FieldOfStudy = ParseString(x, "FieldOfStudy"),
                DateFrom = ParseDate(x, "DateFrom", true),
                DateTo = ParseDate(x, "DateTo", true)
            }).ToArray();
        }

        return document;
    }

    private static string ParseString(IReadOnlyDictionary<string, DocumentField> fields, string inputName)
    {
        fields.TryGetValue(inputName, out var field);
        if (field == null)
        {
            return string.Empty;
        }

        return field.AsString();
    }

    private static DateTime? ParseDate(IReadOnlyDictionary<string, DocumentField> fields, string inputName, bool clear = false)
    {
        fields.TryGetValue(inputName, out var field);
        var dateString = field?.AsString();

        if (string.IsNullOrWhiteSpace(dateString))
        {
            return null;
        }

        if (clear)
        {
            dateString = DigitRegex.Match(dateString).Value;
            if (int.TryParse(dateString, out var value) && value > 0)
            {
                return new DateTime(value, 1, 1);
            }
        }

        if (DateTime.TryParse(dateString, out var date))
        {
            return date;
        }
        return null;
    }
}