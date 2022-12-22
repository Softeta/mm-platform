using Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects;
using System;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateAggregate
{
    public class NoteTests
    {
        [Theory, AutoMoqData]
        public void Create_ShouldInitializeNote(string value, DateTimeOffset? endDate)
        {
            // Act
            var note = Note.Create(value, endDate);

            // Arrange
            Assert.Equal(value, note?.Value);
            Assert.Equal(endDate, note?.EndDate);
        }

        [Theory, AutoMoqData]
        public void Create_ShouldReturnNull_WhenNoValue(DateTimeOffset? endDate)
        {
            // Act
            var note = Note.Create(null, endDate);

            // Arrange
            Assert.Null(note?.Value);
        }
    }
}
