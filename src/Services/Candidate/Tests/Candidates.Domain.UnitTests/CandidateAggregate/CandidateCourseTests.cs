using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Domain.Seedwork.Exceptions;
using System;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateAggregate
{
    public class CandidateCourseTests
    {
        [Theory, AutoMoqData]
        public void Constructor_ShouldInitCandidateCourse(
            Guid candidateId,
            string name,
            string issuingOrganization,
            string courseDescription,
            string certificateUri,
            string certificateFileName)
        {
            // Arrange

            // Act
            var course = new CandidateCourse(
                candidateId,
                name,
                issuingOrganization,
                courseDescription,
                certificateUri,
                certificateFileName);

            // Assert
            Assert.Equal(name, course.Name);
            Assert.Equal(issuingOrganization, course.IssuingOrganization);
            Assert.Equal(courseDescription, course.Description);
            Assert.Equal(certificateUri, course.Certificate?.Uri);
            Assert.Equal(certificateFileName, course.Certificate?.FileName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_ShouldThrowException_WhenNameEmpty(string name)
        {
            // Arrange

            // Act
            Action action = () => new CandidateCourse(
                Guid.NewGuid(),
                name,
                "organization",
                null,
                null,
                null);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_ShouldThrowException_WheIssuingOrganizationEmpty(string issuingOrganization)
        {
            // Arrange

            // Act
            Action action = () => new CandidateCourse(
                Guid.NewGuid(),
                "course",
                issuingOrganization,
                null,
                null,
                null);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void Update_ShouldUpdateCandidateCourse(
            CandidateCourse course,
            string name,
            string issuingOrganization,
            string courseDescription,
            string certificateUri,
            string certificateFileName,
            bool isCertificateChanged)
        {
            // Arrange

            // Act
            course.Update(
                name,
                issuingOrganization,
                courseDescription,
                certificateUri,
                certificateFileName,
                isCertificateChanged);

            // Assert
            Assert.Equal(name, course.Name);
            Assert.Equal(issuingOrganization, course.IssuingOrganization);
            Assert.Equal(courseDescription, course.Description);
        }

        [Theory]
        [InlineAutoMoqData(true)]
        public void Update_ShouldUpdateCertificate_WhenIsCertificateChangedTrue(
            bool isCertificateChanged,
            CandidateCourse course,
            string name,
            string issuingOrganization,
            string courseDescription,
            string certificateUri,
            string certificateFileName)
        {
            // Arrange

            // Act
            course.Update(
                name,
                issuingOrganization,
                courseDescription,
                certificateUri,
                certificateFileName,
                isCertificateChanged);

            // Assert
            Assert.Equal(certificateUri, course.Certificate?.Uri);
            Assert.Equal(certificateFileName, course.Certificate?.FileName);
        }

        [Theory]
        [InlineAutoMoqData(false)]
        public void Update_ShouldNotUpdateCertificate_WhenIsCertificateChangedFalse(
            bool isCertificateChanged,
            CandidateCourse course,
            string name,
            string issuingOrganization,
            string courseDescription,
            string certificateUri,
            string certificateFileName)
        {
            // Arrange
            var expectedcertificateUri = course.Certificate?.Uri;
            var expectedCertificateFileName = course.Certificate?.FileName;

            // Act
            course.Update(
                name,
                issuingOrganization,
                courseDescription,
                certificateUri,
                certificateFileName,
                isCertificateChanged);

            // Assert
            Assert.Equal(expectedcertificateUri, course.Certificate?.Uri);
            Assert.Equal(expectedCertificateFileName, course.Certificate?.FileName);
        }
    }
}
