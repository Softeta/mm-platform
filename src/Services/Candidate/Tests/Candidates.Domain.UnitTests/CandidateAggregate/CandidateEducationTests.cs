using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using System;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateAggregate
{
    public class CandidateEducationTests
    {
        [Theory, AutoMoqData]
        public void Constructor_ShouldInitCandidateEducation(
            Guid candidateId,
            string schoolName,
            string degree,
            string fieldOfStudy,
            DateTimeOffset from,
            string studiesDescription,
            bool isStillStudying,
            string certificateUri,
            string certificateFileName)
        {
            // Arrange
            var to = from.AddDays(1);

            // Act
            var education = new CandidateEducation(candidateId,
                schoolName,
                degree,
                fieldOfStudy,
                from,
                to,
                studiesDescription,
                isStillStudying,
                certificateUri,
                certificateFileName);

            // Assert
            Assert.Equal(schoolName, education.SchoolName);
            Assert.Equal(degree, education.Degree);
            Assert.Equal(fieldOfStudy, education.FieldOfStudy);
            Assert.Equal(from, education.Period.From);
            Assert.Equal(to, education.Period.To);
            Assert.Equal(studiesDescription, education.StudiesDescription);
            Assert.Equal(isStillStudying, education.IsStillStudying);
            Assert.Equal(certificateUri, education.Certificate?.Uri);
            Assert.Equal(certificateFileName, education.Certificate?.FileName);
        }

        [Theory]
        [InlineAutoMoqData("")]
        [InlineAutoMoqData(" ")]
        public void Constructor_ShouldThrowException_WhenSchoolNameEmpty(
            string schoolName,
            string degree,
            string fieldOfStudy)
        {
            // Act
            Action action = () => new CandidateEducation(
                Guid.NewGuid(),
                schoolName,
                degree,
                fieldOfStudy,
                DateTimeOffset.Now,
                null,
                null,
                false,
                null,
                null);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory]
        [InlineAutoMoqData("")]
        [InlineAutoMoqData(" ")]
        public void Constructor_ShouldThrowException_WhenFieldOfStudyEmpty(
            string fieldOfStudy,
            string schoolName,
            string degree)
        {
            // Act
            Action action = () => new CandidateEducation(
                Guid.NewGuid(),
                schoolName,
                degree,
                fieldOfStudy,
                DateTimeOffset.Now,
                null,
                null,
                false,
                null,
                null);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory]
        [InlineAutoMoqData("")]
        [InlineAutoMoqData(" ")]
        public void Constructor_ShouldThrowException_WhenDegreeEmpty(
            string degree,
            string fieldOfStudy,
            string schoolName)
        {
            // Act
            Action action = () => new CandidateEducation(
                Guid.NewGuid(),
                schoolName,
                degree,
                fieldOfStudy,
                DateTimeOffset.Now,
                null,
                null,
                false,
                null,
                null);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void Update_ShouldUpdateCandidateEducation(
            Guid candidateId,
            string schoolName,
            string degree,
            string fieldOfStudy,
            DateTimeOffset from,
            string studiesDescription,
            bool isStillStudying,
            string certificateUri,
            string certificateFileName,
            bool isCertificateChanged)
        {
            // Arrange
            var to = from.AddDays(1);
            var education = new CandidateEducation(candidateId,
                schoolName,
                degree,
                fieldOfStudy,
                from,
                to,
                studiesDescription,
                isStillStudying,
                certificateUri,
                certificateFileName);

            // Act
            education.Update(
                schoolName,
                degree,
                fieldOfStudy,
                from,
                to,
                studiesDescription,
                isStillStudying,
                certificateUri,
                certificateFileName,
                isCertificateChanged);

            // Assert
            Assert.Equal(schoolName, education.SchoolName);
            Assert.Equal(degree, education.Degree);
            Assert.Equal(fieldOfStudy, education.FieldOfStudy);
            Assert.Equal(from, education.Period.From);
            Assert.Equal(to, education.Period.To);
            Assert.Equal(studiesDescription, education.StudiesDescription);
            Assert.Equal(isStillStudying, education.IsStillStudying);
        }

        [Theory]
        [InlineAutoMoqData(true)]
        public void Update_ShouldUpdateCertificate_WhenIsCertificateChangedTrue(
            bool isCertificateChanged,
            Guid candidateId,
            string schoolName,
            string degree,
            string fieldOfStudy,
            DateTimeOffset from,
            string studiesDescription,
            bool isStillStudying,
            string certificateUri,
            string certificateFileName)
        {
            // Arrange
            var education = new CandidateEducation(candidateId,
                schoolName,
                degree,
                fieldOfStudy,
                from,
                null,
                studiesDescription,
                isStillStudying,
                certificateUri,
                certificateFileName);

            // Act
            education.Update(
                schoolName,
                degree,
                fieldOfStudy,
                from,
                null,
                studiesDescription,
                isStillStudying,
                certificateUri,
                certificateFileName,
                isCertificateChanged);

            // Assert
            Assert.Equal(certificateUri, education.Certificate?.Uri);
            Assert.Equal(certificateFileName, education.Certificate?.FileName);
        }

        [Theory]
        [InlineAutoMoqData(false)]
        public void Update_ShouldNotUpdateCertificate_WhenIsCertificateChangedFalse(
            bool isCertificateChanged,
            Guid candidateId,
            string schoolName,
            string degree,
            string fieldOfStudy,
            DateTimeOffset from,
            string studiesDescription,
            bool isStillStudying,
            string certificateUri,
            string certificateFileName)
        {
            // Arrange
            var education = new CandidateEducation(candidateId,
                schoolName,
                degree,
                fieldOfStudy,
                from,
                null,
                studiesDescription,
                isStillStudying,
                certificateUri,
                certificateFileName);
            var expectedcertificateUri = education.Certificate?.Uri;
            var expectedCertificateFileName = education.Certificate?.FileName;

            // Act
            education.Update(
                schoolName,
                degree,
                fieldOfStudy,
                from,
                null,
                studiesDescription,
                isStillStudying,
                certificateUri,
                certificateFileName,
                isCertificateChanged);

            // Assert
            Assert.Equal(expectedcertificateUri, education.Certificate?.Uri);
            Assert.Equal(expectedCertificateFileName, education.Certificate?.FileName);
        }
    }
}
