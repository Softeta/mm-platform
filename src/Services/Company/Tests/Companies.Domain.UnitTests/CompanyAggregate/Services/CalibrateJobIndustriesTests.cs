using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Companies.Domain.Aggregates.CompanyAggregate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Shared;
using Xunit;

namespace Companies.Domain.UnitTests.CompanyAggregate
{
    public class CalibrateCompanyIndustriesTests
    {
        [Theory, AutoMoqData]
        public void CalibrateCompanyIndustries_ShouldNotChange_WhenRequestedIndustryIdsSameAsCurrent(
            Guid companyId,
            CompanyIndustry industry1,
            CompanyIndustry industry2)
        {
            // Arrange
            var expected = new List<CompanyIndustry>() {
                new CompanyIndustry(industry1.IndustryId, companyId, industry1.Code),
                new CompanyIndustry(industry2.IndustryId, companyId, industry2.Code)
            };

            var expectedCompanyIndustryIds = expected.Select(x => x.IndustryId).ToHashSet();

            var current = new List<CompanyIndustry>() {
                new CompanyIndustry(industry1.IndustryId, companyId, industry1.Code),
                new CompanyIndustry(industry2.IndustryId, companyId, industry2.Code)
            };

            var request = new List<CompanyIndustry>() {
                new CompanyIndustry(industry1.IndustryId, companyId, industry1.Code),
                new CompanyIndustry(industry2.IndustryId, companyId, industry2.Code)
            };

            // Act
            current.Calibrate(request, companyId);

            // Assert
            Assert.Equal(expectedCompanyIndustryIds, current.Select(x => x.IndustryId));
        }

        [Theory, AutoMoqData]
        public void CalibrateJobIndustries_ShouldChange_WhenPartOfCompanyIndustryIdsDifferent(
            Guid companyId,
            CompanyIndustry industry1,
            CompanyIndustry industry2,
            CompanyIndustry industry3)
        {
            // Arrange
            var expected = new List<CompanyIndustry>() {
                new CompanyIndustry(industry2.IndustryId, companyId, industry2.Code),
                new CompanyIndustry(industry3.IndustryId, companyId, industry3.Code)
            };

            var expectedCompanyIndustryIds = expected.Select(x => x.IndustryId).ToHashSet();

            var current = new List<CompanyIndustry>() {
                new CompanyIndustry(industry1.IndustryId, companyId, industry1.Code),
                new CompanyIndustry(industry2.IndustryId, companyId, industry2.Code)
            };

            var request = new List<CompanyIndustry>() {
                new CompanyIndustry(industry2.IndustryId, companyId, industry2.Code),
                new CompanyIndustry(industry3.IndustryId, companyId, industry3.Code)
            };

            // Act
            current.Calibrate(request, companyId);

            // Assert
            Assert.Equal(expectedCompanyIndustryIds, current.Select(x => x.IndustryId));
        }

        [Theory, AutoMoqData]
        public void CalibrateJobIndustries_ShouldAdd_WhenCurrentIndustryIdsEmpty(
            Guid companyId,
            CompanyIndustry industry1,
            CompanyIndustry industry2)
        {
            // Arrange
            var expected = new List<CompanyIndustry>() {
                new CompanyIndustry(industry1.IndustryId, companyId, industry1.Code),
                new CompanyIndustry(industry2.IndustryId, companyId, industry2.Code)
            };

            var expectedCompanyIndustryIds = expected.Select(x => x.IndustryId).ToHashSet();

            var current = new List<CompanyIndustry>();

            var request = new List<CompanyIndustry>() {
                new CompanyIndustry(industry1.IndustryId, companyId, industry1.Code),
                new CompanyIndustry(industry2.IndustryId, companyId, industry2.Code)
            };

            // Act
            current.Calibrate(request, companyId);

            // Assert
            Assert.Equal(expectedCompanyIndustryIds, current.Select(x => x.IndustryId));
        }
    }
}
