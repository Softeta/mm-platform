using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Companies.Domain.Events;
using Companies.Domain.UnitTests.CompanyAggregate.DataSeed;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Shared;
using Xunit;

namespace Companies.Domain.UnitTests.CompanyAggregate
{
    public class CompanyTests
    {
        [Theory, AutoMoqData]
        public void Create_ShouldCreateCompany(
            string name, 
            string registrationNumber,
            string personFirstName,
            string personLastName,
            Position personPosition,
            string addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            IEnumerable<CompanyIndustry> industries,
            List<Guid> fileCacheIds,
            CreatedBy createdBy)
        {
            // Arrange
            var companyTestData = new CompanyDataSeed();
            var company = new Company();
            var expectedLocation = $"{city} ({country})";
            var expectedStatus = CompanyStatus.Approved;

            // Act
            company.Create(
                name,
                registrationNumber,
                null,
                companyTestData.WebsiteUrl,
                companyTestData.LinkedInUrl,
                companyTestData.GlassdoorUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                companyTestData.PersonEmail,
                personFirstName,
                personLastName,
                companyTestData.PersonPhoneCountryCode,
                companyTestData.PersonPhoneNumber,
                personPosition.Id,
                personPosition.Code,
                personPosition.AliasTo?.Id,
                personPosition.AliasTo?.Code,
                null,
                null,
                industries,
                fileCacheIds,
                createdBy.Id,
                createdBy.Scope);

            // Assert
            Assert.NotEqual(Guid.Empty, company.Id);
            Assert.Equal(expectedStatus, company.Status);
            Assert.Equal(name, company.Name);
            Assert.Equal(registrationNumber, company.RegistrationNumber);
            Assert.Equal(companyTestData.WebsiteUrl, company.WebsiteUrl);
            Assert.Equal(companyTestData.LinkedInUrl, company.LinkedInUrl);
            Assert.Equal(companyTestData.GlassdoorUrl, company.GlassdoorUrl);
            Assert.Equal(addressLine, company.Address!.AddressLine);
            Assert.Equal(expectedLocation, company.Address!.Location);
            Assert.Equal(city, company.Address!.City);
            Assert.Equal(country, company.Address!.Country);
            Assert.Equal(postalCode, company.Address!.PostalCode);
            Assert.Equal(longitude, company.Address!.Coordinates?.Longitude);
            Assert.Equal(latitude, company.Address!.Coordinates?.Latitude);
            Assert.Equal(industries.Count(), company.Industries.Count);

            var contactPerson = Assert.Single(company.ContactPersons);
            Assert.Equal(ContactPersonRole.Admin, contactPerson.Role);
        }

        [Theory, AutoMoqData]
        public void Update_ShouldUpdateCompany(
            Company company,
            string addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            IEnumerable<CompanyIndustry> industries)
        {
            // Arrange
            var companyTestData = new CompanyDataSeed();
            var expectedLocation = $"{city} ({country})";

            // Act
            company.Update(
                companyTestData.WebsiteUrl,
                companyTestData.LinkedInUrl,
                companyTestData.GlassdoorUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                industries,
                null,
                false,
                null);

            // Assert
            Assert.Equal(companyTestData.WebsiteUrl, company.WebsiteUrl);
            Assert.Equal(companyTestData.LinkedInUrl, company.LinkedInUrl);
            Assert.Equal(companyTestData.GlassdoorUrl, company.GlassdoorUrl);
            Assert.Equal(addressLine, company.Address!.AddressLine);
            Assert.Equal(expectedLocation, company.Address!.Location);
            Assert.Equal(city, company.Address!.City);
            Assert.Equal(country, company.Address!.Country);
            Assert.Equal(postalCode, company.Address!.PostalCode);
            Assert.Equal(longitude, company.Address!.Coordinates?.Longitude);
            Assert.Equal(latitude, company.Address!.Coordinates?.Latitude);
        }

        [Theory, AutoMoqData]
        public void AddContactPerson_ShouldAddContactPerson(
            Guid externalId,
            SystemLanguage? systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement,
            string name,
            string registrationNumber,
            string personFirstName,
            string personLastName,
            Position personPosition,
            string addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            IEnumerable<CompanyIndustry> industries)
        {
            // Arrange 
            var company = new Company();
            var companyTestData = new CompanyDataSeed();
            var person = GetContactPerson(company.Id);

            company.RegisterMyself(
                person.Email.Address,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            company.Register(
                name,
                registrationNumber,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                person.Email.Address,
                personFirstName,
                personLastName,
                companyTestData.PersonPhoneCountryCode,
                companyTestData.PersonPhoneNumber,
                personPosition.Id,
                personPosition.Code,
                personPosition.AliasTo?.Id,
                personPosition.AliasTo?.Code,
                industries);

            var expectedContactPersonCount = company.ContactPersons.Count + 1;

            company.Approve();

            // Act
            AddContactPerson(company, person);

            // Assert
            Assert.Equal(expectedContactPersonCount, company.ContactPersons.Count);
            var @event = company.DomainEvents.Last();
            Assert.IsType<ContactPersonAddedDomainEvent>(@event);
        }

        [Theory, AutoMoqData]
        public void RegisterMyself_ShouldCreateContactPerson(
            Company company,
            Guid externalId,
            SystemLanguage? systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement
            )
        {
            // Arrange
            var person = GetContactPerson(company.Id);
            var expectedContactPersonCount = company.ContactPersons.Count + 1;
            var expectedStage = ContactPersonStage.Registered;
            var expectedCompanyStatus = CompanyStatus.Registered;

            // Act
            company.RegisterMyself(
                person.Email.Address,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            // Assert
            Assert.Equal(expectedCompanyStatus, company.Status);
            Assert.Equal(expectedContactPersonCount, company.ContactPersons.Count);

            var @vent = Assert.Single(company.DomainEvents);
            var contactPersonRegisteredEvent = @vent as ContactPersonRegisteredDomainEvent;

            Assert.NotNull(contactPersonRegisteredEvent);
            Assert.Equal(externalId, contactPersonRegisteredEvent!.ContactPerson.ExternalId);
            Assert.Equal(expectedStage, contactPersonRegisteredEvent!.ContactPerson.Stage);
        }

        [Theory, AutoMoqData]
        public void Register_ShouldSetValues(
            Company company,
            Guid externalId,
            SystemLanguage? systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement,
            string name,
            string registrationNumber,
            string personFirstName,
            string personLastName,
            Position personPosition,
            string addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            IEnumerable<CompanyIndustry> industries)
        {
            // Arrange
            var companyTestData = new CompanyDataSeed();
            var person = GetContactPerson(company.Id);
            var expectedContactPersonCount = company.ContactPersons.Count + 1;
            var expectedStage = ContactPersonStage.Pending;
            var expectedCompanyStatus = CompanyStatus.Pending;

            company.RegisterMyself(
                person.Email.Address,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            var expectedLocation = $"{city} ({country})";

            // Act
            company.Register(
                name,
                registrationNumber,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                person.Email.Address,
                personFirstName,
                personLastName,
                companyTestData.PersonPhoneCountryCode,
                companyTestData.PersonPhoneNumber,
                personPosition.Id,
                personPosition.Code,
                personPosition.AliasTo?.Id,
                personPosition.AliasTo?.Code,
                industries);

            // Assert
            Assert.Equal(expectedCompanyStatus, company.Status);
            Assert.Equal(name, company.Name);
            Assert.Equal(registrationNumber, company.RegistrationNumber);
            Assert.Equal(addressLine, company.Address!.AddressLine);
            Assert.Equal(expectedLocation, company.Address!.Location);
            Assert.Equal(city, company.Address!.City);
            Assert.Equal(country, company.Address!.Country);
            Assert.Equal(postalCode, company.Address!.PostalCode);
            Assert.Equal(longitude, company.Address!.Coordinates?.Longitude);
            Assert.Equal(latitude, company.Address!.Coordinates?.Latitude);
            Assert.Equal(industries.Count(), company.Industries.Count);

            var contactPerson = Assert.Single(company.ContactPersons);
            Assert.Equal(ContactPersonRole.Admin, contactPerson.Role);

            Assert.Equal(expectedCompanyStatus, company.Status);
            Assert.Equal(expectedContactPersonCount, company.ContactPersons.Count);

            var @vent = Assert.Single(company.DomainEvents);
            var contactPersonRegisteredEvent = @vent as ContactPersonRegisteredDomainEvent;

            Assert.NotNull(contactPersonRegisteredEvent);
            Assert.Equal(externalId, contactPersonRegisteredEvent!.ContactPerson.ExternalId);
            Assert.Equal(expectedStage, contactPersonRegisteredEvent!.ContactPerson.Stage);
        }

        [Theory, AutoMoqData]
        public void LinkContactPerson_ShouldCreateContactPerson(
            string name,
            string registrationNumber,
            string personFirstName,
            string personLastName,
            Position personPosition,
            string addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            IEnumerable<CompanyIndustry> industries,
            List<Guid> fileCacheIds,
            Guid externalId,
            SystemLanguage? systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement,
            CreatedBy createdBy)
        {
            // Arrange
            var companyTestData = new CompanyDataSeed();
            var company = new Company();

            company.Create(
                name,
                registrationNumber,
                null,
                companyTestData.WebsiteUrl,
                companyTestData.LinkedInUrl,
                companyTestData.GlassdoorUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                companyTestData.PersonEmail,
                personFirstName,
                personLastName,
                companyTestData.PersonPhoneCountryCode,
                companyTestData.PersonPhoneNumber,
                personPosition.Id,
                personPosition.Code,
                personPosition.AliasTo?.Id,
                personPosition.AliasTo?.Code,
                null,
                null,
                industries,
                fileCacheIds,
                createdBy.Id,
                createdBy.Scope);

            var person = GetContactPerson(company.Id);
            company.AddContactPerson(
                person.Email.Address,
                person.Role,
                person.FirstName!,
                person.LastName!,
                person.Phone?.CountryCode,
                person.Phone?.Number,
                person.Position?.Id,
                person.Position?.Code,
                person.Position?.AliasTo?.Id,
                person.Position?.AliasTo?.Code,
                null,
                null,
                createdBy.Id,
                createdBy.Scope);

            foreach (var @event in company.DomainEvents)
            {
                @event.MarkAsPublished();
            }
           
            var expectedContactPersonCount = company.ContactPersons.Count;
            var expectedStage = ContactPersonStage.Approved;

            // Act
            company.LinkContactPerson(
                person.Email.Address,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            // Assert
            Assert.Equal(expectedContactPersonCount, company.ContactPersons.Count);

            var @vent = Assert.Single(company.DomainEvents.Where(x => !x.Published));
            var contactPersonRegisteredEvent = @vent as ContactPersonLinkedDomainEvent;

            Assert.NotNull(contactPersonRegisteredEvent);
            Assert.Equal(externalId, contactPersonRegisteredEvent!.ContactPerson.ExternalId);
            Assert.Equal(expectedStage, contactPersonRegisteredEvent!.ContactPerson.Stage);
        }

        [Theory, AutoMoqData]
        public void UpdateContactPerson_ShouldThrowException_WhenContactPersonNotExist(ContactPerson contact)
        {
            // Arrange
            var company = new Company();

            // Act
            var action = () => company.UpdateContactPerson(
                Guid.NewGuid(),
                contact.Role,
                contact.FirstName!,
                contact.LastName!,
                contact.Phone?.CountryCode,
                contact.Phone?.Number,
                contact.Position?.Id,
                contact.Position?.Code,
                contact.Position?.AliasTo?.Id,
                contact.Position?.AliasTo?.Code,
                null,
                false,
                null);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void UpdateContactPerson_ShouldPublishDomainEvent(
            string name,
            string registrationNumber,
            string personFirstName,
            string personLastName,
            Position personPosition,
            string addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            IEnumerable<CompanyIndustry> industries,
            ContactPerson contact,
            CreatedBy createdBy)
        {
            // Arrange
            var expectedEventsCount = 3;
            var companyTestData = new CompanyDataSeed();
            var company = new Company();

            company.Create(
                name,
                registrationNumber,
                null,
                companyTestData.WebsiteUrl,
                companyTestData.LinkedInUrl,
                companyTestData.GlassdoorUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                companyTestData.PersonEmail,
                personFirstName,
                personLastName,
                companyTestData.PersonPhoneCountryCode,
                companyTestData.PersonPhoneNumber,
                personPosition.Id,
                personPosition.Code,
                personPosition.AliasTo?.Id,
                personPosition.AliasTo?.Code,
                null,
                null,
                industries,
                new List<Guid>(),
                createdBy.Id,
                createdBy.Scope);

            var contactPerson = company.ContactPersons.First();

            // Act
            company.UpdateContactPerson(
                contactPerson.Id,
                ContactPersonRole.Admin,
                contact.FirstName!,
                contact.LastName!,
                contact.Phone?.CountryCode,
                contact.Phone?.Number,
                contact.Position?.Id,
                contact.Position?.Code,
                contact.Position?.AliasTo?.Id,
                contact.Position?.AliasTo?.Code,
                null,
                false,
                null);

            // Assert
            Assert.Equal(expectedEventsCount, company.DomainEvents.Count);
        }

        [Theory, AutoMoqData]
        public void UpdateContactPerson_ShouldThrowException_WhenRoleChangedFromAdminToUser(
            string name,
            string registrationNumber,
            string personFirstName,
            string personLastName,
            Position personPosition,
            string addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            IEnumerable<CompanyIndustry> industries,
            ContactPerson contact,
            CreatedBy createdBy)
        {
            // Arrange
            var companyTestData = new CompanyDataSeed();
            var company = new Company();

            company.Create(
                name,
                registrationNumber,
                null,
                companyTestData.WebsiteUrl,
                companyTestData.LinkedInUrl,
                companyTestData.GlassdoorUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                companyTestData.PersonEmail,
                personFirstName,
                personLastName,
                companyTestData.PersonPhoneCountryCode,
                companyTestData.PersonPhoneNumber,
                personPosition.Id,
                personPosition.Code,
                personPosition.AliasTo?.Id,
                personPosition.AliasTo?.Code,
                null,
                null,
                industries,
                new List<Guid>(),
                createdBy.Id,
                createdBy.Scope);

            var contactPerson = company.ContactPersons.First();

            // Act
            var action = () => company.UpdateContactPerson(
                contactPerson.Id,
                ContactPersonRole.User,
                contact.FirstName!,
                contact.LastName!,
                contact.Phone?.CountryCode,
                contact.Phone?.Number,
                contact.Position?.Id,
                contact.Position?.Code,
                contact.Position?.AliasTo?.Id,
                contact.Position?.AliasTo?.Code,
                null,
                false,
                null);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void RequestEmailVerification_ShouldPublishDomaiEvent(
            Guid externalId,
            SystemLanguage systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement)
        {
            // Arrange
            var companyTestData = new CompanyDataSeed();

            var company = new Company();
            company.RegisterMyself(
                companyTestData.PersonEmail,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            // Act
            company.RequestEmailVerification(externalId);

            // Assert
            Assert.Single(company.DomainEvents.Where(x => x.GetType() == typeof(ContactPersonEmailVerificationRequestedDomainEvent)));
        }

        [Theory, AutoMoqData]
        public void Approve_ShouldApproveCompany(
            Guid externalId,
            SystemLanguage? systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement,
            string name,
            string registrationNumber,
            string personFirstName,
            string personLastName,
            Position personPosition,
            string addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            IEnumerable<CompanyIndustry> industries)
        {
            // Arrange 
            var companyTestData = new CompanyDataSeed();
            var company = new Company();
            var person = GetContactPerson(company.Id);
            var expectedCompanyStatus = CompanyStatus.Approved;
            var expectedContactPersonStatus = ContactPersonStage.Approved;

            company.RegisterMyself(
                person.Email.Address,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            company.Register(
                name,
                registrationNumber,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                person.Email.Address,
                personFirstName,
                personLastName,
                companyTestData.PersonPhoneCountryCode,
                companyTestData.PersonPhoneNumber,
                personPosition.Id,
                personPosition.Code,
                personPosition.AliasTo?.Id,
                personPosition.AliasTo?.Code,
                industries);

            // Act
            company.Approve();

            // Assert
            Assert.Equal(expectedCompanyStatus, company.Status);
            Assert.Equal(expectedContactPersonStatus, company.ContactPersons.Single().Stage);
            var @event = company.DomainEvents.Last();
            Assert.IsType<CompanyApprovedDomainEvent>(@event);
        }

        [Theory, AutoMoqData]
        public void Approve_ShouldThrowException_WhenCompanyAlreadyApproved(
            string name,
            string registrationNumber,
            string personFirstName,
            string personLastName,
            Position personPosition,
            string addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            IEnumerable<CompanyIndustry> industries,
            List<Guid> fileCacheIds,
            CreatedBy createdBy)
        {
            // Arrange
            var companyTestData = new CompanyDataSeed();
            var company = new Company();

            company.Create(
                name,
                registrationNumber,
                null,
                companyTestData.WebsiteUrl,
                companyTestData.LinkedInUrl,
                companyTestData.GlassdoorUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                companyTestData.PersonEmail,
                personFirstName,
                personLastName,
                companyTestData.PersonPhoneCountryCode,
                companyTestData.PersonPhoneNumber,
                personPosition.Id,
                personPosition.Code,
                personPosition.AliasTo?.Id,
                personPosition.AliasTo?.Code,
                null,
                null,
                industries,
                fileCacheIds,
                createdBy.Id,
                createdBy.Scope);

            // Act
            var action = () => company.Approve();

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void DeleteContactPerson_ShouldRemoveContactPersonFromList(
            string name,
            string registrationNumber,
            string personFirstName,
            string personLastName,
            Position personPosition,
            string addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            IEnumerable<CompanyIndustry> industries,
            CreatedBy createdBy)
        {
            // Arrange
            var companyTestData = new CompanyDataSeed();
            var company = new Company();

            company.Create(
                name,
                registrationNumber,
                null,
                companyTestData.WebsiteUrl,
                companyTestData.LinkedInUrl,
                companyTestData.GlassdoorUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                companyTestData.PersonEmail,
                personFirstName,
                personLastName,
                companyTestData.PersonPhoneCountryCode,
                companyTestData.PersonPhoneNumber,
                personPosition.Id,
                personPosition.Code,
                personPosition.AliasTo?.Id,
                personPosition.AliasTo?.Code,
                null,
                null,
                industries,
                new List<Guid>(),
                createdBy.Id,
                createdBy.Scope);

            var person = GetContactPerson(company.Id);
          
            AddContactPerson(company, person);
            AddContactPerson(company, person);
            AddContactPerson(company, person);

            var random = new Random();
            var contactPersonToDelete = company.ContactPersons[random.Next(company.ContactPersons.Count)];

            // Act
            company.DeleteContactPerson(contactPersonToDelete.Id);

            // Assert
            Assert.Null(company.ContactPersons.FirstOrDefault(x => x.Id == contactPersonToDelete.Id));
        }

        [Theory, AutoMoqData]
        public void Reject_ShouldRejectCompany(
            Guid externalId,
            SystemLanguage? systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement,
            string name,
            string registrationNumber,
            string personFirstName,
            string personLastName,
            Position personPosition,
            string addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            IEnumerable<CompanyIndustry> industries)
        {
            // Arrange 
            var companyTestData = new CompanyDataSeed();
            var company = new Company();
            var person = GetContactPerson(company.Id);
            var expectedCompanyStatus = CompanyStatus.Rejected;
            var expectedContactPersonStatus = ContactPersonStage.Rejected;

            company.RegisterMyself(
                person.Email.Address,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            company.Register(
                name,
                registrationNumber,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                person.Email.Address,
                personFirstName,
                personLastName,
                companyTestData.PersonPhoneCountryCode,
                companyTestData.PersonPhoneNumber,
                personPosition.Id,
                personPosition.Code,
                personPosition.AliasTo?.Id,
                personPosition.AliasTo?.Code,
                industries);

            // Act
            company.Reject();

            // Assert
            Assert.Equal(expectedCompanyStatus, company.Status);
            Assert.Equal(expectedContactPersonStatus, company.ContactPersons.Single().Stage);
            var @event = company.DomainEvents.Last();
            Assert.IsType<CompanyRejectedDomainEvent>(@event);
        }

        [Theory, AutoMoqData]
        public void Reject_ShouldThrowException_WhenCompanyAlreadyApproved(
            string name,
            string registrationNumber,
            string personFirstName,
            string personLastName,
            Position personPosition,
            string addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            IEnumerable<CompanyIndustry> industries,
            List<Guid> fileCacheIds,
            CreatedBy createdBy)
        {
            // Arrange
            var companyTestData = new CompanyDataSeed();
            var company = new Company();

            company.Create(
                name,
                registrationNumber,
                null,
                companyTestData.WebsiteUrl,
                companyTestData.LinkedInUrl,
                companyTestData.GlassdoorUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                companyTestData.PersonEmail,
                personFirstName,
                personLastName,
                companyTestData.PersonPhoneCountryCode,
                companyTestData.PersonPhoneNumber,
                personPosition.Id,
                personPosition.Code,
                personPosition.AliasTo?.Id,
                personPosition.AliasTo?.Code,
                null,
                null,
                industries,
                fileCacheIds,
                createdBy.Id,
                createdBy.Scope);

            // Act
            var action = () => company.Reject();

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void AddContactPerson_ShouldThrowException_WhenStatusPending(
            Guid externalId,
            SystemLanguage? systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement,
            string name,
            string registrationNumber,
            string personFirstName,
            string personLastName,
            Position personPosition,
            string addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            IEnumerable<CompanyIndustry> industries)
        {
            // Arrange 
            var companyTestData = new CompanyDataSeed();
            var company = new Company();
            var person = GetContactPerson(company.Id);

            company.RegisterMyself(
                person.Email.Address,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            company.Register(
                name,
                registrationNumber,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                person.Email.Address,
                personFirstName,
                personLastName,
                companyTestData.PersonPhoneCountryCode,
                companyTestData.PersonPhoneNumber,
                personPosition.Id,
                personPosition.Code,
                personPosition.AliasTo?.Id,
                personPosition.AliasTo?.Code,
                industries);

            // Act 
            var action = () => AddContactPerson(company, person);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        private static ContactPerson GetContactPerson(Guid companyId)
        {
            return ContactPerson.CreateByOther(
                companyId,
                "email@email.com",
                ContactPersonRole.Admin,
                "firstName",
                "lastName",
                "+370",
                "6000000",
                Guid.NewGuid(),
                "Developer",
                null,
                null,
                null,
                Guid.NewGuid(),
                Scope.BackOffice);
        }

        private void AddContactPerson(Company company, ContactPerson person)
        {
            company.AddContactPerson(
                person.Email.Address,
                person.Role,
                person.FirstName!,
                person.LastName!,
                person.Phone?.CountryCode,
                person.Phone?.Number,
                person.Position?.Id,
                person.Position?.Code,
                person.Position?.AliasTo?.Id,
                person.Position?.AliasTo?.Code,
                null,
                null,
                Guid.NewGuid(),
                Scope.BackOffice);
        }
    }
}
