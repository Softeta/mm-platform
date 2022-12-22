using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;
using Jobs.Domain.Aggregates.JobAggregate.Entities;
using System;
using Tests.Shared;
using Xunit;

namespace Jobs.Domain.UnitTests.JobAggregate
{
    public class JobContactPersonTests
    {
        [Theory, AutoMoqData]
        public void Initialize_ShouldCreateNewContactPerson_WhenAllContacts(
            Guid jobId,
            Guid personId,
            bool isMainContact,
            string firstName,
            string lastName,
            string phoneNumber,
            string email,
            Position position,
            string pictureUri,
            SystemLanguage? systemLanguage,
            Guid? externalId)
        {
            // Act
            var contactPerson = new JobContactPerson(
                jobId,
                personId, 
                isMainContact,
                firstName, 
                lastName, 
                phoneNumber,
                email,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                pictureUri,
                systemLanguage,
                externalId);

            // Assert
            AssertContactPersonData(
                jobId, 
                personId, 
                isMainContact,
                firstName, 
                lastName,
                phoneNumber,
                email,
                position,
                systemLanguage,
                contactPerson,
                externalId);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        [InlineAutoMoqData("")]
        [InlineAutoMoqData("   ")]
        public void Initialize_ShouldCreateNewContactPerson_WhenOnlyEmailContactChannel(
            string? phoneNumber,
            Guid jobId,
            Guid personId,
            bool isMainContact,
            string firstName,
            string lastName,
            string email,
            Position position,
            string pictureUri,
            SystemLanguage? systemLanguage,
            Guid? externalId)
        {
            // Act
            var contactPerson = new JobContactPerson(
                jobId,
                personId, 
                isMainContact,
                firstName,
                lastName, 
                phoneNumber,
                email, 
                position.Id, 
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                pictureUri,
                systemLanguage,
                externalId);

            // Assert
            AssertContactPersonData(
                jobId, 
                personId,
                isMainContact,
                firstName,
                lastName,
                phoneNumber, 
                email, 
                position,
                systemLanguage,
                contactPerson,
                externalId);
        }

        [Theory]
        [InlineAutoMoqData(null, null)]
        [InlineAutoMoqData("", "")]
        [InlineAutoMoqData("   ", "    ")]
        public void Initialize_ShouldThrowDomainException_WhenNoEmailContactChannel(
            string phoneNumber,
            string email,
            Guid jobId,
            Guid personId,
            bool isMainContact,
            string firstName,
            string lastName,
            Position position,
            string pictureUri,
            SystemLanguage? systemLanguage,
            Guid? contactExternalId)
        {
            // Act
            var action = () => new JobContactPerson(
                jobId, 
                personId, 
                isMainContact,
                firstName, 
                lastName, 
                phoneNumber,
                email,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                pictureUri,
                systemLanguage,
                contactExternalId);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void SyncIsRegistered_ShouldSyncIsRegistered(
            string? phoneNumber,
            Guid jobId,
            Guid personId,
            bool isMainContact,
            string firstName,
            string lastName,
            string email,
            Position position,
            string pictureUri,
            SystemLanguage? systemLanguage,
            Guid? externalId,
            Guid? externalIdForSync)
        {
            // Arrange
            var contactPerson = new JobContactPerson(
                jobId,
                personId,
                isMainContact,
                firstName,
                lastName,
                phoneNumber,
                email,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                pictureUri,
                systemLanguage,
                externalId);

            // Act
            contactPerson.SyncExternalId(externalIdForSync);

            // Assert
            Assert.Equal(externalIdForSync, contactPerson.ExternalId);
        }

        private static void AssertContactPersonData(
            Guid jobId,
            Guid personId,
            bool isMainContact,
            string firstName,
            string lastName,
            string? phoneNumber,
            string? email, 
            Position position,
            SystemLanguage? systemLanguage,
            JobContactPerson contactPerson,
            Guid? externalId)
        {
            Assert.NotEqual(Guid.Empty, contactPerson.Id);
            Assert.Equal(jobId, contactPerson.JobId);
            Assert.Equal(personId, contactPerson.PersonId);
            Assert.Equal(isMainContact, contactPerson.IsMainContact);
            Assert.Equal(firstName, contactPerson.FirstName);
            Assert.Equal(lastName, contactPerson.LastName);
            Assert.Equal(phoneNumber, contactPerson.PhoneNumber);
            Assert.Equal(email, contactPerson.Email);
            Assert.Equal(position.Id, contactPerson.Position?.Id);
            Assert.Equal(position.Code, contactPerson.Position?.Code);
            Assert.Equal(position.AliasTo?.Id, contactPerson.Position?.AliasTo?.Id);
            Assert.Equal(position.AliasTo?.Code, contactPerson.Position?.AliasTo?.Code);
            Assert.Equal(systemLanguage, contactPerson.SystemLanguage);
            Assert.Equal(externalId, contactPerson.ExternalId);
        }
    }
}
