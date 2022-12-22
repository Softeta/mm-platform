using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Companies.Domain.UnitTests.CompanyAggregate.DataSeed;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;
using System;
using Tests.Shared;
using Xunit;

namespace Companies.Domain.UnitTests.CompanyAggregate
{
    public class ContactPersonTests
    {
        [Theory, AutoMoqData]
        public void Initialize_ShouldCreateNewContactPerson(
           Guid companyId,
           ContactPersonRole role,
           string firstName,
           string lastName,
           Position position,
           CreatedBy createdBy)
        {
            // Arrange
            var contactPersonTestData = new CompanyDataSeed();
            var expectedStage = ContactPersonStage.Approved;

            // Act
            var contactPerson = ContactPerson.CreateByOther(
                companyId,
                contactPersonTestData.PersonEmail,
                role,
                firstName, 
                lastName,
                contactPersonTestData.PersonPhoneCountryCode,
                contactPersonTestData.PersonPhoneNumber,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                null,
                createdBy.Id,
                createdBy.Scope);

            // Assert
            AssertContactPersonData(
                companyId,
                contactPersonTestData.PersonEmail, 
                role, 
                firstName, 
                lastName,
                contactPersonTestData.PersonPhoneCountryCode,
                contactPersonTestData.PersonPhoneNumber, 
                position.Id, 
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                createdBy,
                contactPerson);
            Assert.Equal(expectedStage, contactPerson.Stage);
            Assert.False(contactPerson.Email.IsVerified);
            Assert.Null(contactPerson.Email.VerificationKey);
            Assert.Null(contactPerson.Email.VerificationRequestedAt);
            Assert.Null(contactPerson.Email.VerifiedAt);
        }

        [Theory]
        [InlineAutoMoqData(default)]
        public void Initialize_ShouldThrowDomainException_WhenDefaultCompanyId(
            Guid companyId,
            ContactPersonRole role,
            string firstName,
            string lastName,
            Position position,
            CreatedBy createdBy)
        {
            // Act
            var contactPersonTestData = new CompanyDataSeed();
            var action = () => ContactPerson.CreateByOther(
                companyId,
                contactPersonTestData.PersonEmail,
                role,
                firstName,
                lastName,
                contactPersonTestData.PersonPhoneCountryCode,
                contactPersonTestData.PersonPhoneNumber, 
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                null,
                createdBy.Id,
                createdBy.Scope);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void RegisterMyself_ShouldSetValues(
           Guid companyId,
           ContactPersonRole role,
           string firstName,
           string lastName,
           Position position,
           Guid externalId,
           SystemLanguage? systemLanguage,
           bool acceptTermsAndConditions,
           bool acceptMarketingAcknowledgement,
           CreatedBy createdBy)
        {
            // Arrange
            var contactPersonTestData = new CompanyDataSeed();
            var contactPerson = ContactPerson.CreateByOther(
                companyId,
                contactPersonTestData.PersonEmail,
                role,
                firstName,
                lastName,
                contactPersonTestData.PersonPhoneCountryCode,
                contactPersonTestData.PersonPhoneNumber,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                null,
                createdBy.Id,
                createdBy.Scope);

            var expectedStage = ContactPersonStage.Registered;

            // Act
            contactPerson.RegisterMyself(
                contactPersonTestData.PersonEmail,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            // Assert
            AssertContactPersonData(
                companyId,
                contactPersonTestData.PersonEmail,
                role,
                firstName, 
                lastName,
                contactPersonTestData.PersonPhoneCountryCode,
                contactPersonTestData.PersonPhoneNumber,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                createdBy,
                contactPerson);
            Assert.Equal(expectedStage, contactPerson.Stage);
            Assert.False(contactPerson.Email.IsVerified);
            Assert.NotNull(contactPerson.Email.VerificationKey);
            Assert.NotNull(contactPerson.Email.VerificationRequestedAt);
            Assert.Null(contactPerson.Email.VerifiedAt);
        }

        [Theory, AutoMoqData]
        public void RegisterMyself_ShouldThrowDomainException_WhenPersonIsAlreadyLinked(
          Guid companyId,
          ContactPersonRole role,
          string firstName,
          string lastName,
          Position position,
          Guid externalId,
          SystemLanguage? systemLanguage,
          bool acceptTermsAndConditions,
          bool acceptMarketingAcknowledgement,
          CreatedBy createdBy)
        {
            // Arrange
            var contactPersonTestData = new CompanyDataSeed();
            var contactPerson = ContactPerson.CreateByOther(
                companyId,
                contactPersonTestData.PersonEmail,
                role,
                firstName,
                lastName,
                contactPersonTestData.PersonPhoneCountryCode,
                contactPersonTestData.PersonPhoneNumber,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                null,
                createdBy.Id,
                createdBy.Scope);

            contactPerson.RegisterMyself(
                contactPersonTestData.PersonEmail,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            // Act
            Action action = () => contactPerson.RegisterMyself(
                contactPersonTestData.PersonEmail,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void Update_ShouldSetValues(
           Guid companyId,
           ContactPersonRole role,
           string firstName,
           string lastName,
           Position position,
           ContactPersonRole newRole,
           string newFirstName,
           string newLastName,
           Position newPosition,
           CreatedBy createdBy)
        {
            // Arrange
            var contactPersonTestData = new CompanyDataSeed();
            var contactPerson = ContactPerson.CreateByOther(
                companyId,
                contactPersonTestData.PersonEmail,
                role,
                firstName,
                lastName,
                contactPersonTestData.PersonPhoneCountryCode,
                contactPersonTestData.PersonPhoneNumber,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                null,
                createdBy.Id,
                createdBy.Scope);

            var newPhoneCountryCode = "+370";
            var newPhoneNumber = "60001";

            // Act
            contactPerson.Update(
                newRole,
                newFirstName,
                newLastName,
                newPhoneCountryCode,
                newPhoneNumber,
                newPosition.Id,
                newPosition.Code,
                newPosition.AliasTo?.Id,
                newPosition.AliasTo?.Code,
                null,
                false);

            // Assert
            AssertContactPersonData(
                companyId,
                contactPersonTestData.PersonEmail,
                newRole,
                newFirstName,
                newLastName,
                newPhoneCountryCode,
                newPhoneNumber,
                newPosition.Id,
                newPosition.Code,
                newPosition.AliasTo?.Id,
                newPosition.AliasTo?.Code,
                createdBy,
                contactPerson);
        }

        [Theory, AutoMoqData]
        public void SetPending_ShouldSetValues(
           Guid companyId,
           ContactPersonRole role,
           string firstName,
           string lastName,
           Position position,
           string newFirstName,
           string newLastName,
           Position newPosition,
           CreatedBy createdBy)
        {
            // Arrange
            var contactPersonTestData = new CompanyDataSeed();
            var contactPerson = ContactPerson.CreateByOther(
                companyId,
                contactPersonTestData.PersonEmail,
                role,
                firstName,
                lastName,
                contactPersonTestData.PersonPhoneCountryCode,
                contactPersonTestData.PersonPhoneNumber,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                null,
                createdBy.Id,
                createdBy.Scope);

            var newPhoneCountryCode = "+370";
            var newPhoneNumber = "60001";

            var expectedStage = ContactPersonStage.Pending;

            // Act
            contactPerson.SetPending(
                newFirstName,
                newLastName,
                newPhoneCountryCode,
                newPhoneNumber,
                newPosition.Id,
                newPosition.Code,
                newPosition.AliasTo?.Id,
                newPosition.AliasTo?.Code);

            // Assert
            Assert.Equal(expectedStage, contactPerson.Stage);
            AssertContactPersonData(
                companyId,
                contactPersonTestData.PersonEmail,
                role,
                newFirstName,
                newLastName,
                newPhoneCountryCode,
                newPhoneNumber,
                newPosition.Id,
                newPosition.Code,
                newPosition.AliasTo?.Id,
                newPosition.AliasTo?.Code,
                createdBy,
                contactPerson);
        }

        [Theory, AutoMoqData]
        public void Link_ShouldThrowDomainException_WhenPersonIsAlreadyLinked(
          Guid companyId,
          ContactPersonRole role,
          string firstName,
          string lastName,
          Position position,
          Guid externalId,
          SystemLanguage? systemLanguage,
          bool acceptTermsAndConditions,
          bool acceptMarketingAcknowledgement,
         CreatedBy createdBy)
        {
            // Arrange
            var contactPersonTestData = new CompanyDataSeed();
            var contactPerson = ContactPerson.CreateByOther(
                companyId,
                contactPersonTestData.PersonEmail,
                role,
                firstName,
                lastName,
                contactPersonTestData.PersonPhoneCountryCode,
                contactPersonTestData.PersonPhoneNumber,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                null,
                createdBy.Id,
                createdBy.Scope);

            contactPerson.Link(
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            // Act
            Action action = () => contactPerson.Link(
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void Link_ShouldSetValues(
           Guid companyId,
           ContactPersonRole role,
           string firstName,
           string lastName,
           Position position,
           Guid externalId,
           SystemLanguage? systemLanguage,
           bool acceptTermsAndConditions,
           bool acceptMarketingAcknowledgement,
           CreatedBy createdBy)
        {
            // Arrange
            var contactPersonTestData = new CompanyDataSeed();
            var contactPerson = ContactPerson.CreateByOther(
                companyId,
                contactPersonTestData.PersonEmail,
                role,
                firstName,
                lastName,
                contactPersonTestData.PersonPhoneCountryCode,
                contactPersonTestData.PersonPhoneNumber,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                null,
                createdBy.Id,
                createdBy.Scope);

            var expectedStage = ContactPersonStage.Approved;

            // Act
            contactPerson.Link(
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            // Assert
            AssertContactPersonData(
                companyId,
                contactPersonTestData.PersonEmail,
                role,
                firstName,
                lastName,
                contactPersonTestData.PersonPhoneCountryCode,
                contactPersonTestData.PersonPhoneNumber,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                createdBy,
                contactPerson);
            Assert.Equal(expectedStage, contactPerson.Stage);
            Assert.False(contactPerson.Email.IsVerified);
            Assert.NotNull(contactPerson.Email.VerificationKey);
            Assert.NotNull(contactPerson.Email.VerificationRequestedAt);
            Assert.Null(contactPerson.Email.VerifiedAt);
        }

        [Theory, AutoMoqData]
        public void Verify_ShouldVerifyContactPerson(
            Guid companyId,
            Guid externalId,
            SystemLanguage? systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement)
        {
            // Arrange
            var contactPersonTestData = new CompanyDataSeed();
            int verificationExpiresInMinutes = 1440;
            var contactPerson = ContactPerson.CreateMyself(companyId);
            contactPerson.RegisterMyself(
                contactPersonTestData.PersonEmail,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            var verificationKey = contactPerson.Email!.VerificationKey!.Value;

            // Act
            contactPerson.VerifyEmail(verificationKey, verificationExpiresInMinutes);

            // Assert
            Assert.True(contactPerson.Email.IsVerified);
            Assert.NotNull(contactPerson.Email.VerifiedAt);
        }

        [Theory, AutoMoqData]
        public void Verify_ShouldThrowException_IfContactPersonIsAlreadyActivated(
            Guid companyId,
            Guid externalId,
            SystemLanguage? systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement)
        {
            // Arrange
            var contactPersonTestData = new CompanyDataSeed();
            int verificationExpiresInMinutes = 1440;
            var contactPerson = ContactPerson.CreateMyself(companyId);
            contactPerson.RegisterMyself(
                contactPersonTestData.PersonEmail,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            var verificationKey = contactPerson.Email!.VerificationKey!.Value;
            contactPerson.VerifyEmail(verificationKey, verificationExpiresInMinutes);

            // Act
            var action = () => contactPerson.VerifyEmail(verificationKey, verificationExpiresInMinutes);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void Verify_ShouldThrowException_IfEmailVerificationExpired(
            Guid companyId,
            Guid externalId,
            SystemLanguage? systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement)
        {
            // Arrange
            var contactPersonTestData = new CompanyDataSeed();
            int verificationExpiresInMinutes = 0;
            var contactPerson = ContactPerson.CreateMyself(companyId);
            contactPerson.RegisterMyself(
                contactPersonTestData.PersonEmail,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            var verificationKey = contactPerson.Email!.VerificationKey!.Value;

            // Act
            var action = () => contactPerson.VerifyEmail(verificationKey, verificationExpiresInMinutes);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void RequestEmailVerification_ShouldRegenerateVerificationEmailDetails(
            Guid companyId,
            Guid externalId,
            SystemLanguage systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement)
        {
            // Arrange
            var contactPersonTestData = new CompanyDataSeed();
            var contactPerson = ContactPerson.CreateMyself(companyId);
            contactPerson.RegisterMyself(
                contactPersonTestData.PersonEmail,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            var oldVerificationKey = contactPerson.Email!.VerificationKey!.Value;
            var oldVerificationRequestAt = contactPerson.Email!.VerificationRequestedAt!.Value;

            // Act
            contactPerson.RequestEmailVerification();

            // Assert
            Assert.NotEqual(oldVerificationKey, contactPerson.Email!.VerificationKey!.Value);
            Assert.True(oldVerificationRequestAt < contactPerson.Email!.VerificationRequestedAt!.Value);
        }

        [Theory, AutoMoqData]
        public void Reject_ShouldReject(
            Guid companyId,
            ContactPersonRole role,
            string firstName,
            string lastName,
            Position position,
            CreatedBy createdBy)
        {
            // Arrange
            var expectedContactPersonStage = ContactPersonStage.Rejected;
            var contactPersonTestData = new CompanyDataSeed();

            var contactPerson = ContactPerson.CreateByOther(
                companyId,
                contactPersonTestData.PersonEmail, 
                role, 
                firstName,
                lastName,
                null,
                null,
                position.Id, 
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                null,
                createdBy.Id,
                createdBy.Scope);

            // Act
            contactPerson.Reject();

            // Assert
            Assert.Equal(expectedContactPersonStage, contactPerson.Stage);
            Assert.NotNull(contactPerson.RejectedAt);
        }

        [Theory, AutoMoqData]
        public void Reject_ShouldThrowException_WhenAlreadyRejected(
            Guid companyId,
            ContactPersonRole role,
            string firstName,
            string lastName,
            Position position,
            CreatedBy createdBy)
        {
            // Arrange
            var contactPersonTestData = new CompanyDataSeed();
            var contactPerson = ContactPerson.CreateByOther(
                companyId,
                contactPersonTestData.PersonEmail,
                role,
                firstName,
                lastName,
                null,
                null,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                null,
                createdBy.Id,
                createdBy.Scope);

            contactPerson.Reject();

            // Act
            var action = () => contactPerson.Reject();

            // Assert
            Assert.Throws<DomainException>(action);
        }

        private static void AssertContactPersonData(
            Guid companyId,
            string? email,
            ContactPersonRole role,
            string firstName,
            string lastName,
            string? phoneCountryCode,
            string? phoneNumber,
            Guid? positionId,
            string? positionCode,
            Guid? positionAliasToId,
            string? positionAliasToCode,
            CreatedBy? createdBy,
            ContactPerson contactPerson)
        {
            Assert.NotEqual(Guid.Empty, contactPerson.Id);
            Assert.Equal(email, contactPerson.Email?.Address);
            Assert.Equal(companyId, contactPerson.CompanyId);
            Assert.Equal(role, contactPerson.Role);
            Assert.Equal(firstName, contactPerson.FirstName);
            Assert.Equal(lastName, contactPerson.LastName);
            Assert.Equal(phoneCountryCode, contactPerson.Phone?.CountryCode);
            Assert.Equal(phoneNumber, contactPerson.Phone?.Number);
            Assert.Equal(positionId, contactPerson.Position?.Id);
            Assert.Equal(positionCode, contactPerson.Position?.Code);
            Assert.Equal(positionAliasToId, contactPerson.Position?.AliasTo?.Id);
            Assert.Equal(positionAliasToCode, contactPerson.Position?.AliasTo?.Code);
            Assert.Equal(createdBy?.Id, contactPerson.CreatedBy?.Id);
            Assert.Equal(createdBy?.Scope, contactPerson.CreatedBy?.Scope);
        }

        [Theory, AutoMoqData]
        public void UpdateLegalTerms_ShouldUpdate(bool termsAgreement, bool marketingAgreement)
        {
            // Arrange
            var contactPerson = ContactPerson.CreateMyself(Guid.NewGuid());

            // Act
            contactPerson.UpdateLegalTerms(termsAgreement, marketingAgreement);

            // Assert
            Assert.NotNull(contactPerson.TermsAndConditions);
            Assert.NotNull(contactPerson.MarketingAcknowledgement);
            Assert.Equal(termsAgreement, contactPerson.TermsAndConditions!.Agreed);
            Assert.Equal(marketingAgreement, contactPerson.MarketingAcknowledgement!.Agreed);
        }

        [Theory, AutoMoqData]
        public void SyncJobPosition_ShouldSync(
            Guid newAliasId,
            string newAliasCode,
            Guid companyId,
            ContactPersonRole role,
            string firstName,
            string lastName,
            Position position,
            CreatedBy createdBy)
        {
            // Arrange 
            var contactPersonTestData = new CompanyDataSeed();

            var contactPerson = ContactPerson.CreateByOther(
                companyId,
                contactPersonTestData.PersonEmail,
                role,
                firstName,
                lastName,
                null,
                null,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                null,
                createdBy.Id,
                createdBy.Scope);

            // Act
            contactPerson.SyncJobPosition(newAliasId, newAliasCode);

            // Assert
            Assert.Equal(position.Id, contactPerson.Position!.Id);
            Assert.Equal(position.Code, contactPerson.Position!.Code);
            Assert.Equal(newAliasId, contactPerson.Position!.AliasTo?.Id);
            Assert.Equal(newAliasCode, contactPerson.Position!.AliasTo?.Code);
        }

        [Theory, AutoMoqData]
        public void UpdateSettings_ShouldUpdate(
            SystemLanguage systemLanguage,
            bool marketingAcknowledgement,
            ContactPersonRole role,
            string firstName,
            string lastName,
            Position position,
            CreatedBy createdBy)
        {
            // Arrange 
            var contactPersonTestData = new CompanyDataSeed();

            var contactPerson = ContactPerson.CreateByOther(
                Guid.NewGuid(),
                contactPersonTestData.PersonEmail,
                role,
                firstName,
                lastName,
                null,
                null,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                null,
                createdBy.Id,
                createdBy.Scope);

            // Act
            contactPerson.UpdateSettings(systemLanguage, marketingAcknowledgement);

            // Assert
            Assert.Equal(systemLanguage, contactPerson.SystemLanguage);
            Assert.Equal(marketingAcknowledgement, contactPerson.MarketingAcknowledgement?.Agreed);
        }
    }
}
