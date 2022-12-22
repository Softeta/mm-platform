﻿// <auto-generated />
using System;
using Companies.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Companies.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(CompanyContext))]
    [Migration("20220705120800_AddRegistrationFieldOnContactPerson")]
    partial class AddRegistrationFieldOnContactPerson
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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Companies", "companies");
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

            modelBuilder.Entity("Companies.Domain.Aggregates.CompanyAggregate.Company", b =>
                {
                    b.OwnsOne("Companies.Domain.Aggregates.CompanyAggregate.ValueObjects.Image", "Logo", b1 =>
                        {
                            b1.Property<Guid>("CompanyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("OriginalUri")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.Property<string>("ThumbnailUri")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.HasKey("CompanyId");

                            b1.ToTable("Companies", "companies");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId");
                        });

                    b.OwnsOne("Companies.Domain.Aggregates.CompanyAggregate.ValueObjects.Address", "Address", b1 =>
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
                                .HasMaxLength(64)
                                .HasColumnType("nvarchar(64)");

                            b1.HasKey("CompanyId");

                            b1.ToTable("Companies", "companies");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId");

                            b1.OwnsOne("Companies.Domain.Aggregates.CompanyAggregate.ValueObjects.Coordinates", "Coordinates", b2 =>
                                {
                                    b2.Property<Guid>("AddressCompanyId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<double>("Latitude")
                                        .HasColumnType("float");

                                    b2.Property<double>("Longitude")
                                        .HasColumnType("float");

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

            modelBuilder.Entity("Companies.Domain.Aggregates.CompanyAggregate.Entities.ContactPerson", b =>
                {
                    b.HasOne("Companies.Domain.Aggregates.CompanyAggregate.Company", null)
                        .WithMany("ContactPersons")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Companies.Domain.Aggregates.CompanyAggregate.ValueObjects.LegalInformationAgreement", "MarketingAcknowledgement", b1 =>
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

                    b.OwnsOne("Companies.Domain.Aggregates.CompanyAggregate.ValueObjects.Image", "Picture", b1 =>
                        {
                            b1.Property<Guid>("ContactPersonId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("OriginalUri")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.Property<string>("ThumbnailUri")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.HasKey("ContactPersonId");

                            b1.ToTable("ContactPersons", "companies");

                            b1.WithOwner()
                                .HasForeignKey("ContactPersonId");
                        });

                    b.OwnsOne("Companies.Domain.Aggregates.CompanyAggregate.ValueObjects.LegalInformationAgreement", "TermsAndConditions", b1 =>
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

                    b.OwnsOne("Companies.Domain.Aggregates.CompanyAggregate.ValueObjects.Email", "Email", b1 =>
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

                    b.OwnsOne("Companies.Domain.Aggregates.CompanyAggregate.ValueObjects.Phone", "Phone", b1 =>
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

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("MarketingAcknowledgement");

                    b.Navigation("Phone");

                    b.Navigation("Picture");

                    b.Navigation("TermsAndConditions");
                });

            modelBuilder.Entity("Companies.Domain.Aggregates.CompanyAggregate.Company", b =>
                {
                    b.Navigation("ContactPersons");
                });
#pragma warning restore 612, 618
        }
    }
}