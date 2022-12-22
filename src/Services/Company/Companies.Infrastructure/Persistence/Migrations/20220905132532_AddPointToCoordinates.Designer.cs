﻿// <auto-generated />
using System;
using Companies.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;

#nullable disable

namespace Companies.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(CompanyContext))]
    [Migration("20220905132532_AddPointToCoordinates")]
    partial class AddPointToCoordinates
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Companies.Domain.Aggregates.CompanyAggregate.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("GlassdoorUrl")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("LinkedInUrl")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("RegistrationNumber")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTimeOffset?>("RejectedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("WebsiteUrl")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.HasKey("Id");

                    b.ToTable("Companies", "companies");
                });

            modelBuilder.Entity("Companies.Domain.Aggregates.CompanyAggregate.Entities.CompanyIndustry", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("IndustryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CompanyId", "IndustryId")
                        .IsUnique();

                    b.ToTable("CompanyIndustries", "companies");
                });

            modelBuilder.Entity("Companies.Domain.Aggregates.CompanyAggregate.Entities.ContactPerson", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("LastName")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTimeOffset?>("RejectedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Stage")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("SystemLanguage")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("ContactPersons", "companies");
                });

            modelBuilder.Entity("Companies.Domain.ReadModels.RegistryCenterCompany", b =>
                {
                    b.Property<string>("RegistrationNumber")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Country")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("AddressLine")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("City")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Region")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("ZipCode")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("RegistrationNumber", "Country");

                    b.HasIndex("Name");

                    b.ToTable("RegistryCenterCompanies", "companies");
                });

            modelBuilder.Entity("Companies.Domain.Aggregates.CompanyAggregate.Company", b =>
                {
                    b.OwnsOne("Domain.Seedwork.Shared.ValueObjects.Image", "Logo", b1 =>
                        {
                            b1.Property<Guid>("CompanyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("OriginalUri")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("nvarchar(2000)");

                            b1.Property<string>("ThumbnailUri")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("nvarchar(2000)");

                            b1.HasKey("CompanyId");

                            b1.ToTable("Companies", "companies");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId");
                        });

                    b.OwnsOne("Domain.Seedwork.Shared.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("CompanyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("AddressLine")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)");

                            b1.Property<string>("City")
                                .HasMaxLength(64)
                                .HasColumnType("nvarchar(64)");

                            b1.Property<string>("Country")
                                .HasMaxLength(64)
                                .HasColumnType("nvarchar(64)");

                            b1.Property<string>("Location")
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)");

                            b1.Property<string>("PostalCode")
                                .IsRequired()
                                .HasMaxLength(64)
                                .HasColumnType("nvarchar(64)");

                            b1.HasKey("CompanyId");

                            b1.ToTable("Companies", "companies");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId");

                            b1.OwnsOne("Domain.Seedwork.Shared.ValueObjects.Coordinates", "Coordinates", b2 =>
                                {
                                    b2.Property<Guid>("AddressCompanyId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<double>("Latitude")
                                        .HasColumnType("float");

                                    b2.Property<double>("Longitude")
                                        .HasColumnType("float");

                                    b2.Property<Point>("Point")
                                        .HasColumnType("geography");

                                    b2.HasKey("AddressCompanyId");

                                    b2.ToTable("Companies", "companies");

                                    b2.WithOwner()
                                        .HasForeignKey("AddressCompanyId");
                                });

                            b1.Navigation("Coordinates");
                        });

                    b.Navigation("Address");

                    b.Navigation("Logo");
                });

            modelBuilder.Entity("Companies.Domain.Aggregates.CompanyAggregate.Entities.CompanyIndustry", b =>
                {
                    b.HasOne("Companies.Domain.Aggregates.CompanyAggregate.Company", null)
                        .WithMany("Industries")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Companies.Domain.Aggregates.CompanyAggregate.Entities.ContactPerson", b =>
                {
                    b.HasOne("Companies.Domain.Aggregates.CompanyAggregate.Company", null)
                        .WithMany("ContactPersons")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Seedwork.Shared.ValueObjects.LegalInformationAgreement", "MarketingAcknowledgement", b1 =>
                        {
                            b1.Property<Guid>("ContactPersonId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<bool>("Agreed")
                                .HasColumnType("bit");

                            b1.Property<DateTimeOffset>("ModifiedAt")
                                .HasColumnType("datetimeoffset");

                            b1.HasKey("ContactPersonId");

                            b1.ToTable("ContactPersons", "companies");

                            b1.WithOwner()
                                .HasForeignKey("ContactPersonId");
                        });

                    b.OwnsOne("Domain.Seedwork.Shared.ValueObjects.Image", "Picture", b1 =>
                        {
                            b1.Property<Guid>("ContactPersonId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("OriginalUri")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("nvarchar(2000)");

                            b1.Property<string>("ThumbnailUri")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("nvarchar(2000)");

                            b1.HasKey("ContactPersonId");

                            b1.ToTable("ContactPersons", "companies");

                            b1.WithOwner()
                                .HasForeignKey("ContactPersonId");
                        });

                    b.OwnsOne("Domain.Seedwork.Shared.ValueObjects.LegalInformationAgreement", "TermsAndConditions", b1 =>
                        {
                            b1.Property<Guid>("ContactPersonId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<bool>("Agreed")
                                .HasColumnType("bit");

                            b1.Property<DateTimeOffset>("ModifiedAt")
                                .HasColumnType("datetimeoffset");

                            b1.HasKey("ContactPersonId");

                            b1.ToTable("ContactPersons", "companies");

                            b1.WithOwner()
                                .HasForeignKey("ContactPersonId");
                        });

                    b.OwnsOne("Domain.Seedwork.Shared.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("ContactPersonId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(64)
                                .HasColumnType("nvarchar(64)");

                            b1.Property<bool>("IsVerified")
                                .HasColumnType("bit");

                            b1.Property<Guid?>("VerificationKey")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTimeOffset?>("VerificationRequestedAt")
                                .HasColumnType("datetimeoffset");

                            b1.Property<DateTimeOffset?>("VerifiedAt")
                                .HasColumnType("datetimeoffset");

                            b1.HasKey("ContactPersonId");

                            b1.HasIndex("Address")
                                .IsUnique();

                            b1.ToTable("ContactPersons", "companies");

                            b1.WithOwner()
                                .HasForeignKey("ContactPersonId");
                        });

                    b.OwnsOne("Domain.Seedwork.Shared.ValueObjects.Phone", "Phone", b1 =>
                        {
                            b1.Property<Guid>("ContactPersonId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("CountryCode")
                                .HasMaxLength(4)
                                .HasColumnType("nvarchar(4)");

                            b1.Property<string>("Number")
                                .HasMaxLength(28)
                                .HasColumnType("nvarchar(28)");

                            b1.Property<string>("PhoneNumber")
                                .HasMaxLength(32)
                                .HasColumnType("nvarchar(32)");

                            b1.HasKey("ContactPersonId");

                            b1.ToTable("ContactPersons", "companies");

                            b1.WithOwner()
                                .HasForeignKey("ContactPersonId");
                        });

                    b.OwnsOne("Domain.Seedwork.Shared.ValueObjects.Position", "Position", b1 =>
                        {
                            b1.Property<Guid>("ContactPersonId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("ContactPersonId");

                            b1.ToTable("ContactPersons", "companies");

                            b1.WithOwner()
                                .HasForeignKey("ContactPersonId");

                            b1.OwnsOne("Domain.Seedwork.Shared.ValueObjects.Tag", "AliasTo", b2 =>
                                {
                                    b2.Property<Guid>("PositionContactPersonId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Code")
                                        .IsRequired()
                                        .HasMaxLength(64)
                                        .HasColumnType("nvarchar(64)");

                                    b2.Property<Guid>("Id")
                                        .HasColumnType("uniqueidentifier");

                                    b2.HasKey("PositionContactPersonId");

                                    b2.ToTable("ContactPersons", "companies");

                                    b2.WithOwner()
                                        .HasForeignKey("PositionContactPersonId");
                                });

                            b1.Navigation("AliasTo");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("MarketingAcknowledgement");

                    b.Navigation("Phone");

                    b.Navigation("Picture");

                    b.Navigation("Position");

                    b.Navigation("TermsAndConditions");
                });

            modelBuilder.Entity("Companies.Domain.Aggregates.CompanyAggregate.Company", b =>
                {
                    b.Navigation("ContactPersons");

                    b.Navigation("Industries");
                });
#pragma warning restore 612, 618
        }
    }
}
