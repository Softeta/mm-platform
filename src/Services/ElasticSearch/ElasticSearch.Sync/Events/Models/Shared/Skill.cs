namespace ElasticSearch.Sync.Events.Models.Shared
{
    internal class Skill
    {
        public string Code { get; set; } = null!;
        public Alias? AliasTo { get; set; }
    }
}
