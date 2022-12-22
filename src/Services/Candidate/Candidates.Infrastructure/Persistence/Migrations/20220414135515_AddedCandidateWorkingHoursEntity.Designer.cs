﻿// <auto-generated />
using System;
using Candidates.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Candidates.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(CandidateContext))]
    [Migration("20220414135515_AddedCandidateWorkingHoursEntity")]
    partial class AddedCandidateWorkingHoursEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Candidate", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("BirthDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CurrentPosition")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("DesiredGoals")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("DesiredPosition")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<Guid?>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Hobbies")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("LastName")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("LinkedInUrl")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<bool>("OpenForOpportunities")
                        .HasColumnType("bit");

                    b.Property<string>("PictureUri")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Candidates", "candidate");
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateJobFormat", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CandidateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("FormatType")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.ToTable("CandidateJobFormats", "candidate");
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateLanguage", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CandidateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.ToTable("CandidateLanguages", "candidate");
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateSkill", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CandidateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.ToTable("CandidateSkills", "candidate");
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateWorkLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CandidateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("FullAddress")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.ToTable("CandidateWorkLocations", "candidate");
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Candidate", b =>
                {
                    b.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("CandidateId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("AddressLine")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)");

                            b1.HasKey("CandidateId");

                            b1.ToTable("Candidates", "candidate");

                            b1.WithOwner()
                                .HasForeignKey("CandidateId");

                            b1.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.Coordinates", "Coordinates", b2 =>
                                {
                                    b2.Property<Guid>("AddressCandidateId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<double>("Latitude")
                                        .HasColumnType("float");

                                    b2.Property<double>("Longitude")
                                        .HasColumnType("float");

                                    b2.HasKey("AddressCandidateId");

                                    b2.ToTable("Candidates", "candidate");

                                    b2.WithOwner()
                                        .HasForeignKey("AddressCandidateId");
                                });

                            b1.Navigation("Coordinates");
                        });

                    b.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.CommunicationPreference", "CommunicationPreference", b1 =>
                        {
                            b1.Property<Guid>("CandidateId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("PhoneNumber")
                                .HasMaxLength(32)
                                .HasColumnType("nvarchar(32)");

                            b1.HasKey("CandidateId");

                            b1.ToTable("Candidates", "candidate");

                            b1.WithOwner()
                                .HasForeignKey("CandidateId");
                        });

                    b.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.Freelance", "Freelance", b1 =>
                        {
                            b1.Property<Guid>("CandidateId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTimeOffset?>("EndDate")
                                .HasColumnType("datetimeoffset");

                            b1.Property<DateTimeOffset?>("StartDate")
                                .HasColumnType("datetimeoffset");

                            b1.Property<int?>("WorkHours")
                                .HasColumnType("int");

                            b1.Property<int>("WorkType")
                                .HasMaxLength(64)
                                .HasColumnType("int");

                            b1.HasKey("CandidateId");

                            b1.ToTable("Candidates", "candidate");

                            b1.WithOwner()
                                .HasForeignKey("CandidateId");

                            b1.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.MinimumSalary", "MinimumSalary", b2 =>
                                {
                                    b2.Property<Guid>("FreelanceCandidateId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Currency")
                                        .IsRequired()
                                        .HasMaxLength(3)
                                        .HasColumnType("nvarchar(3)");

                                    b2.Property<decimal?>("PerHour")
                                        .HasColumnType("Decimal(18,2)");

                                    b2.Property<decimal?>("PerMonth")
                                        .HasColumnType("Decimal(18,2)");

                                    b2.HasKey("FreelanceCandidateId");

                                    b2.ToTable("Candidates", "candidate");

                                    b2.WithOwner()
                                        .HasForeignKey("FreelanceCandidateId");
                                });

                            b1.Navigation("MinimumSalary");
                        });

                    b.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.Permanent", "Permanent", b1 =>
                        {
                            b1.Property<Guid>("CandidateId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTimeOffset?>("StartDate")
                                .HasColumnType("datetimeoffset");

                            b1.Property<int?>("WorkHours")
                                .HasColumnType("int");

                            b1.Property<int>("WorkType")
                                .HasMaxLength(64)
                                .HasColumnType("int");

                            b1.HasKey("CandidateId");

                            b1.ToTable("Candidates", "candidate");

                            b1.WithOwner()
                                .HasForeignKey("CandidateId");

                            b1.OwnsMany("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateWorkingHours", "WorkingHourTypes", b2 =>
                                {
                                    b2.Property<Guid>("Id")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("CandidateId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<DateTimeOffset>("CreatedAt")
                                        .HasColumnType("datetimeoffset");

                                    b2.Property<string>("WorkingHoursType")
                                        .IsRequired()
                                        .HasMaxLength(64)
                                        .HasColumnType("nvarchar(64)");

                                    b2.HasKey("Id");

                                    b2.HasIndex("CandidateId");

                                    b2.ToTable("CandidateWorkingHours", "candidate");

                                    b2.WithOwner()
                                        .HasForeignKey("CandidateId");
                                });

                            b1.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.MinimumSalary", "MinimumSalary", b2 =>
                                {
                                    b2.Property<Guid>("PermanentCandidateId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Currency")
                                        .IsRequired()
                                        .HasMaxLength(3)
                                        .HasColumnType("nvarchar(3)");

                                    b2.Property<decimal?>("PerHour")
                                        .HasColumnType("Decimal(18,2)");

                                    b2.Property<decimal?>("PerMonth")
                                        .HasColumnType("Decimal(18,2)");

                                    b2.HasKey("PermanentCandidateId");

                                    b2.ToTable("Candidates", "candidate");

                                    b2.WithOwner()
                                        .HasForeignKey("PermanentCandidateId");
                                });

                            b1.Navigation("MinimumSalary");

                            b1.Navigation("WorkingHourTypes");
                        });

                    b.Navigation("Address");

                    b.Navigation("CommunicationPreference");

                    b.Navigation("Freelance");

                    b.Navigation("Permanent");
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateJobFormat", b =>
                {
                    b.HasOne("Candidates.Domain.Aggregates.CandidateAggregate.Candidate", null)
                        .WithMany("Formats")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateLanguage", b =>
                {
                    b.HasOne("Candidates.Domain.Aggregates.CandidateAggregate.Candidate", null)
                        .WithMany("Languages")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateSkill", b =>
                {
                    b.HasOne("Candidates.Domain.Aggregates.CandidateAggregate.Candidate", null)
                        .WithMany("Skills")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateWorkLocation", b =>
                {
                    b.HasOne("Candidates.Domain.Aggregates.CandidateAggregate.Candidate", null)
                        .WithMany("WorkLocations")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Candidate", b =>
                {
                    b.Navigation("Formats");

                    b.Navigation("Languages");

                    b.Navigation("Skills");

                    b.Navigation("WorkLocations");
                });
#pragma warning restore 612, 618
        }
    }
}
