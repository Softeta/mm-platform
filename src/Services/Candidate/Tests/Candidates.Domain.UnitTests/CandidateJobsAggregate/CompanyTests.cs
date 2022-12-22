using Candidates.Domain.Aggregates.CandidateJobsAggregate.ValueObjects;
using System;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateJobsAggregate
{
    public class CompanyTests
    {
        [Theory, AutoMoqData]
        public void Constructor_ShouldInitialize(Guid id, string name, string? logoUri)
        {
            // Arrange

            // Act
            var company = new Company(id, name, logoUri);

            // Assert
            Assert.Equal(id, company.Id);
            Assert.Equal(name, company.Name);
            Assert.Equal(logoUri, company.LogoUri);
        }
    }
}
