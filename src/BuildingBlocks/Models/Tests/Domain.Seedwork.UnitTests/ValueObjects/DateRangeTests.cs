using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;
using System;
using Tests.Shared;
using Xunit;

namespace Domain.Seedwork.UnitTests.ValueObjects
{
    public class DateRangeTests
    {
        [Theory, AutoMoqData]
        public void Constructor_InitializesDateWithoutTo(DateTimeOffset from)
        {
            // Act
            var dateRange = new DateRange(from, null);

            // Assert
            Assert.Equal(from, dateRange.From);
            Assert.Null(dateRange.To);
        }

        [Theory, AutoMoqData]
        public void Constructor_InitializesDateWithTo(DateTimeOffset from)
        {
            // Arrange
            var to = from.AddDays(1);

            // Act
            var dateRange = new DateRange(from, to);

            // Assert
            Assert.Equal(from, dateRange.From);
            Assert.Equal(to, dateRange.To);
        }
    }
}
