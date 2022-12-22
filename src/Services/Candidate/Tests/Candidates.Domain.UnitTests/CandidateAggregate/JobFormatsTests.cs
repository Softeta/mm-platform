using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects;
using Domain.Seedwork.Enums;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateAggregate
{
    public class JobFormatsTests
    {
        [Fact]
        public void Constructor_ShouldInitialize()
        {
            // Arrange
            var formats = new List<FormatType>
            {
                FormatType.Hybrid,
                FormatType.Onsite
            };

            // Act
            var jobFormats = new JobFormats(formats);

            // Assert
            Assert.True(jobFormats.IsHybrid);
            Assert.True(jobFormats.IsOnSite);
            Assert.False(jobFormats.IsRemote);
        }
    }
}
