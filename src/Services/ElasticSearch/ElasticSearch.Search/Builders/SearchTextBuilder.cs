using System.Collections.Generic;
using System.Text;
using ElasticSearch.Search.Constants;

namespace ElasticSearch.Search.Builders
{
    public class SearchTextBuilder
    {
        private StringBuilder _filter;

        public SearchTextBuilder()
        {
            _filter = new StringBuilder();
        }

        public SearchTextBuilder AddPosition(string? position)
        {
            if (position is null) return this;

            AddFilteringOr();
            _filter.Append($@"search.ismatchscoring('""{position}""', '{SearchParams.CurrentPosition}')");

            return this;
        }

        public SearchTextBuilder AddSeniorities(ICollection<string> seniorities)
        {
            return AddCollectionToBuilder(seniorities, SearchParams.Seniority);
        }

        public SearchTextBuilder AddSkills(ICollection<string> skills)
        {
            return AddCollectionToBuilder(skills, SearchParams.Skills);
        }

        public SearchTextBuilder AddWorkTypes(ICollection<string> workTypes)
        {
            return AddCollectionToBuilder(workTypes, SearchParams.WorkTypes);
        }

        public SearchTextBuilder AddWorkingHourTypes(ICollection<string> workingHourTypes)
        {
            return AddCollectionToBuilder(workingHourTypes, SearchParams.WorkingHourTypes);
        }

        public SearchTextBuilder AddWorkingFormats(ICollection<string> workingFormats)
        {
            return AddCollectionToBuilder(workingFormats, SearchParams.WorkingFormats);
        }

        public SearchTextBuilder AddIndustries(ICollection<string> industries)
        {
            return AddCollectionToBuilder(industries, SearchParams.Industries);
        }

        public SearchTextBuilder AddLanguages(ICollection<string> languages)
        {
            return AddCollectionToBuilder(languages, SearchParams.Languages);
        }

        public SearchTextBuilder AddLocation(string? location)
        {
            if (location is null) return this;

            AddFilteringOr();
            _filter.Append($@"search.ismatchscoring('""{location}""', '{SearchParams.Location}')");

            return this;
        }

        public string Build()
        {
            return _filter.ToString();
        }

        private SearchTextBuilder AddCollectionToBuilder(ICollection<string> collection, string fieldName)
        {
            if (collection.Count == 0) return this;

            AddFilteringOr();
            _filter.Append($@"search.ismatchscoring('""{string.Join(@""",""", collection)}""', '{fieldName}')");

            return this;
        }

        private void AddFilteringOr()
        {
            if (_filter.Length != 0)
            {
                _filter.Append(" or ");
            }
        }
    }
}
