using ElasticSearch.Search.Builders;

namespace Search.UnitTests.Builders
{
    public class SearchTextBuilderTests
    {
        [Fact]
        public void AddLocation_ShouldAddLocationWithValue()
        {
            // Arrange
            var expected = @"search.ismatchscoring('""Testing place""', 'Location')";

            // Act
            var actual = new SearchTextBuilder()
                .AddLocation("Testing place")
                .Build()
                .ToString();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddPosition_ShouldAddPositionWithValue()
        {
            // Arrange
            var expected = @"search.ismatchscoring('""Testing position""', 'CurrentPosition')";

            // Act
            var actual = new SearchTextBuilder()
                .AddPosition("Testing position")
                .Build()
                .ToString();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddSeniorities_ShouldAddSenioritiesWithValue()
        {
            // Arrange
            var values = new List<string> 
            {
                "Senior",
                "Mid"
            };
            var expected = @"search.ismatchscoring('""Senior"",""Mid""', 'Seniority')";

            // Act
            var actual = new SearchTextBuilder()
                .AddSeniorities(values)
                .Build()
                .ToString();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddSkills_ShouldAddSkillsWithValue()
        {
            // Arrange
            var values = new List<string>
            {
                ".NET",
                "C#",
                "SQL"
            };
            var expected = @"search.ismatchscoring('"".NET"",""C#"",""SQL""', 'Skills')";

            // Act
            var actual = new SearchTextBuilder()
                .AddSkills(values)
                .Build()
                .ToString();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddWorkTypes_ShouldAddWorkTypesWithValue()
        {
            // Arrange
            var values = new List<string>
            {
                "Freelance",
                "Permanent"
            };
            var expected = @"search.ismatchscoring('""Freelance"",""Permanent""', 'WorkTypes')";

            // Act
            var actual = new SearchTextBuilder()
                .AddWorkTypes(values)
                .Build()
                .ToString();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddWorkingHourTypes_ShouldAddWorkingHourTypesWithValue()
        {
            // Arrange
            var values = new List<string>
            {
                "Full-time",
                "Project"
            };
            var expected = @"search.ismatchscoring('""Full-time"",""Project""', 'WorkingHourTypes')";

            // Act
            var actual = new SearchTextBuilder()
                .AddWorkingHourTypes(values)
                .Build()
                .ToString();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddWorkingFormats_ShouldAddWorkingFormatsWithValue()
        {
            // Arrange
            var values = new List<string>
            {
                "Onsite",
                "Remote"
            };
            var expected = @"search.ismatchscoring('""Onsite"",""Remote""', 'WorkingFormats')";

            // Act
            var actual = new SearchTextBuilder()
                .AddWorkingFormats(values)
                .Build()
                .ToString();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddIndustries_ShouldAddIndustriesWithValue()
        {
            // Arrange
            var values = new List<string>
            {
                "Development",
                "Consultancy"
            };
            var expected = @"search.ismatchscoring('""Development"",""Consultancy""', 'Industries')";

            // Act
            var actual = new SearchTextBuilder()
                .AddIndustries(values)
                .Build()
                .ToString();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddLanguages_ShouldAddLanguagesWithValue()
        {
            // Arrange
            var values = new List<string>
            {
                "English",
                "Lithuanian"
            };
            var expected = @"search.ismatchscoring('""English"",""Lithuanian""', 'Languages')";

            // Act
            var actual = new SearchTextBuilder()
                .AddLanguages(values)
                .Build()
                .ToString();

            // Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void Build_ShouldBuildQueryString()
        {
            // Arrange
            var skills = new List<string>
            {
                ".NET",
                "C#",
                "SQL"
            };
            var languages = new List<string>
            {
                "English",
                "Lithuanian"
            };
            var expected = @"search.ismatchscoring('""English"",""Lithuanian""', 'Languages') or search.ismatchscoring('""Testing position""', 'CurrentPosition') or search.ismatchscoring('"".NET"",""C#"",""SQL""', 'Skills')";

            // Act
            var actual = new SearchTextBuilder()
                .AddLanguages(languages)
                .AddPosition("Testing position")
                .AddSkills(skills)
                .Build()
                .ToString();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
