using API.Customization.Pagination;
using System.Collections.Generic;
using Xunit;

namespace Infrastructure.APICustomization.UnitTests.Pagination
{
    public class PageResponseTests
    {
        [Theory]
        [InlineData(8, 1, 8, "path", null, null)]
        [InlineData(8, 2, 4, "path", null, @"path?pageNumber=1&pageSize=4")]
        [InlineData(8, 1, 4, "path", @"path?pageNumber=2&pageSize=4", null)]
        [InlineData(8, 2, 3, "path", @"path?pageNumber=3&pageSize=3", @"path?pageNumber=1&pageSize=3")]
        [InlineData(8, 5, 3, "path", null, @"path?pageNumber=3&pageSize=3")]
        [InlineData(7, 5, 3, "path", null, @"path?pageNumber=3&pageSize=3")]
        [InlineData(0, 5, 3, "path", null, null)]
        public void Wrapping_ShouldAddCorrectMetaData_WhenUsed(
            int count,
            int pageNumber,
            int pageSize,
            string path,
            string? nextPageResult,
            string? previousPageResult)
        {
            // Arrange
            var mockData = new List<string>();

            // Act
            var sut = new PagedResponse<string>(count, mockData, pageNumber, pageSize, path, "");

            // Assert
            Assert.Equal(nextPageResult, sut.NextPagePath);
            Assert.Equal(previousPageResult, sut.PreviousPagePath);
        }

        [Theory]
        [InlineData(
            8,
            2,
            3,
            "path",
            @"?workType=freelance&pageNumber=1&pageSize=1",
            @"path?workType=freelance&pageNumber=3&pageSize=3", 
            @"path?workType=freelance&pageNumber=1&pageSize=3")]
        [InlineData(
            8,
            2,
            3,
            "path",
            @"?workType=freelance&someOtherProps=propValue&pageNumber=1&pageSize=1",
            @"path?workType=freelance&someOtherProps=propValue&pageNumber=3&pageSize=3",
            @"path?workType=freelance&someOtherProps=propValue&pageNumber=1&pageSize=3")]
        [InlineData(
            8,
            2,
            3,
            "path",
            @"?pageNumber=1&pageSize=1",
            @"path?pageNumber=3&pageSize=3",
            @"path?pageNumber=1&pageSize=3")]
        [InlineData(
            8,
            2,
            3,
            "path",
            @"?PageNumber=1&PageSize=1",
            @"path?pageNumber=3&pageSize=3",
            @"path?pageNumber=1&pageSize=3")]
        [InlineData(
            8,
            2,
            3,
            "path",
            @"?pagenumber=1&pagesize=1",
            @"path?pageNumber=3&pageSize=3",
            @"path?pageNumber=1&pageSize=3")]
        public void Wrapping_ShouldReturnCorrectPreviousAndNextPaths(
            int count,
            int pageNumber,
            int pageSize,
            string path,
            string queryString,
            string? nextPageResult,
            string? previousPageResult)
        {
            // Arrange
            var mockData = new List<string>();

            // Act
            var sut = new PagedResponse<string>(count, mockData, pageNumber, pageSize, path, queryString);

            // Assert
            Assert.Equal(nextPageResult, sut.NextPagePath);
            Assert.Equal(previousPageResult, sut.PreviousPagePath);
        }
    }
}
