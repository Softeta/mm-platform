using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;
using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Jobs.Domain.Aggregates.JobAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using Tests.Shared;
using Xunit;

namespace Jobs.Domain.UnitTests.JobAggregate
{
    public class JobCompanyTests
    {
        [Theory, AutoMoqData]
        public void CreateJobCompany_ShouldCreate(
           Guid companyId,
           CompanyStatus status,
           string name,
           string addressLine,
           string? city,
           string? country,
           string? postalCode,
           double? longitude,
           double? latitude,
           string logoUri,
           string description,
           Guid jobId,
           Guid personId,
           string firstName,
           string lastName,
           string phoneNumber,
           string email,
           Position position,
           string pictureUri,
           SystemLanguage? systemLanguage,
           Guid externalId)
        {
            // Assert 
            var contactPersons = new List<JobContactPerson>();
            contactPersons.Add(new JobContactPerson(
                jobId,
                personId, 
                true,
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
                externalId));
            contactPersons.Add(new JobContactPerson(
                jobId, 
                personId,
                false, 
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
                externalId));

            // Act
            var company = new Company(
                companyId,
                status,
                name, 
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude, 
                description, 
                logoUri, 
                contactPersons);

            // Assert 
            Assert.Equal(companyId, company.Id);
            Assert.Equal(name, company.Name);

            if (addressLine != null)
            {
                Assert.Equal(addressLine, company.Address?.AddressLine);
                Assert.Equal(city, company.Address?.City);
                Assert.Equal(country, company.Address?.Country);
                Assert.Equal(postalCode, company.Address?.PostalCode);
                Assert.Equal(longitude, company.Address?.Coordinates?.Longitude);
                Assert.Equal(latitude, company.Address?.Coordinates?.Latitude);
            } 
            else
            {
                Assert.Null(company.Address);
            }
            
            Assert.Equal(company.ContactPersons.Count, company.ContactPersons.Count);
        }

        [Theory]
        [InlineAutoMoqData(default)]
        public void CreateJobCompany_ShouldThrowDomainException_WhenCompanyIdDefault(
           Guid companyId,
           CompanyStatus status,
           string name,
           string addressLine,
           string? city,
           string? country,
           string? postalCode,
           double? longitude,
           double? latitude,
           string logoUri,
           string description,
           IEnumerable<JobContactPerson> contactPersons)
        {
            // Act
            var action = () => new Company(
                companyId,
                status,
                name,
                addressLine,
                city,
                country,
                postalCode,
                longitude, 
                latitude, 
                description, 
                logoUri,
                contactPersons);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void CreateJobCompany_ShouldThrowDomainException_WhenMoreThanOneMainContactPerson(
           Guid companyId,
           CompanyStatus status,
           string name,
           string addressLine,
           string? city,
           string? country,
           string? postalCode,
           double? longitude,
           double? latitude,
           string logoUri,
           string description,
           Guid jobId, 
           Guid personId,
           string firstName,
           string lastName,
           string phoneNumber,
           string email,
           Position position,
           string pictureUri,
           SystemLanguage? systemLanguage,
           Guid? externalId)
        {
            // Assert 
            var contactPersons = new List<JobContactPerson>();
            contactPersons.Add(new JobContactPerson(
                jobId,
                personId,
                true, 
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
                externalId));
            contactPersons.Add(new JobContactPerson(
                jobId,
                personId,
                true, 
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
                externalId));
            // Act

            var action = () => new Company(
                companyId,
                status,
                name,
                addressLine,
                city,
                country,
                postalCode,
                longitude, 
                latitude, 
                description,
                logoUri, 
                contactPersons);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void CreateJobCompany_ShouldThrowDomainException_WhenNoMainContactPerson(
           Guid companyId,
           CompanyStatus status,
           string name,
           string addressLine,
           string? city,
           string? country,
           string? postalCode,
           double? longitude,
           double? latitude,
           string logoUri,
           string description,
           Guid jobId,
           Guid personId,
           string firstName,
           string lastName,
           string phoneNumber,
           string email,
           Position position,
           string pictureUri,
           SystemLanguage? systemLanguage,
           Guid? externalId)
        {
            // Assert 
            var contactPersons = new List<JobContactPerson>();
            contactPersons.Add(new JobContactPerson(
                jobId,
                personId, 
                false,
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
                externalId));
            // Act

            var action = () => new Company(
                companyId,
                status,
                name,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                description,
                logoUri,
                contactPersons);

            // Assert
            Assert.Throws<DomainException>(action);
        }
    }
}
