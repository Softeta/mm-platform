namespace Domain.Seedwork.Shared.ValueObjects
{
    public class Document : ValueObject<Document>
    {
        public string? Uri { get; init; }
        public string? FileName { get; init; }

        public Document() { }

        public Document(string? uri, string? fileName)
        {
            Uri = uri;
            FileName = fileName;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Uri;
            yield return FileName;
        }
    }
}
