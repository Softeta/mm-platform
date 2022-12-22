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
    [Migration("20220520130334_AddPositionToCandidateJobCollections")]
    partial class AddPositionToCandidateJobCollections
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

                    b.Property<bool>("IsShortListed")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("LinkedInUrl")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<bool>("OpenForOpportunities")
                        .HasColumnType("bit");

                    b.Property<string>("PersonalWebsiteUrl")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.HasKey("Id");

                    b.ToTable("Candidates", "candidate");
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateCourse", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CandidateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("IssuingOrganization")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.ToTable("CandidateCourses", "candidate");
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateEducation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CandidateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Degree")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("FieldOfStudy")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTimeOffset>("From")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsStillStudying")
                        .HasColumnType("bit");

                    b.Property<string>("SchoolName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("StudiesDescription")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTimeOffset?>("To")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.ToTable("CandidateEducations", "candidate");
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateIndustry", b =>
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

                    b.ToTable("CandidateIndustries", "candidate");
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

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateShortListedJob", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CandidateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("CandidateShortListedJobs");
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateSkill", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CandidateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CandidateId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("CandidateId1");

                    b.ToTable("CandidateSkills", "candidate");
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateWorkExperience", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CandidateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("From")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsCurrentJob")
                        .HasColumnType("bit");

                    b.Property<string>("JobDescription")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTimeOffset?>("To")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.ToTable("CandidateWorkExperiences", "candidate");
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateWorkExperienceSkill", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CandidateWorkExperienceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("CandidateWorkExperienceId");

                    b.ToTable("CandidateWorkExperienceSkills", "candidate");
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

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateJobsAggregate.CandidateJobs", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsShortlisted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("CandidateJobs", "candidate");
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities.CandidateArchivedInJob", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CandidateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("JobPosition")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Stage")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("CandidateId", "JobId")
                        .IsUnique();

                    b.ToTable("CandidateArchivedInJobs", "candidate");
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities.CandidateSelectedInJob", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CandidateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("JobPosition")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Stage")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("CandidateId", "JobId")
                        .IsUnique();

                    b.ToTable("CandidateSelectedInJobs", "candidate");
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

                    b.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.Image", "Picture", b1 =>
                        {
                            b1.Property<Guid>("CandidateId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("OriginalUri")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.Property<string>("ThumbnailUri")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.HasKey("CandidateId");

                            b1.ToTable("Candidates", "candidate");

                            b1.WithOwner()
                                .HasForeignKey("CandidateId");
                        });

                    b.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.Terms", "Terms", b1 =>
                        {
                            b1.Property<Guid>("CandidateId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Currency")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTimeOffset?>("EndDate")
                                .HasColumnType("datetimeoffset");

                            b1.Property<DateTimeOffset?>("StartDate")
                                .HasColumnType("datetimeoffset");

                            b1.HasKey("CandidateId");

                            b1.ToTable("Candidates", "candidate");

                            b1.WithOwner()
                                .HasForeignKey("CandidateId");

                            b1.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.Freelance", "Freelance", b2 =>
                                {
                                    b2.Property<Guid>("TermsCandidateId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<decimal?>("HourlySalary")
                                        .HasColumnType("Decimal(18,2)");

                                    b2.Property<decimal?>("MonthlySalary")
                                        .HasColumnType("Decimal(18,2)");

                                    b2.Property<string>("WorkType")
                                        .IsRequired()
                                        .HasMaxLength(64)
                                        .HasColumnType("nvarchar(64)");

                                    b2.HasKey("TermsCandidateId");

                                    b2.ToTable("Candidates", "candidate");

                                    b2.WithOwner()
                                        .HasForeignKey("TermsCandidateId");
                                });

                            b1.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.FullTimeWorkingHours", "FulTimeWorkingHours", b2 =>
                                {
                                    b2.Property<Guid>("TermsCandidateId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Type")
                                        .IsRequired()
                                        .HasMaxLength(64)
                                        .HasColumnType("nvarchar(64)");

                                    b2.HasKey("TermsCandidateId");

                                    b2.ToTable("Candidates", "candidate");

                                    b2.WithOwner()
                                        .HasForeignKey("TermsCandidateId");
                                });

                            b1.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.JobFormats", "Formats", b2 =>
                                {
                                    b2.Property<Guid>("TermsCandidateId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<bool>("IsHybrid")
                                        .HasColumnType("bit");

                                    b2.Property<bool>("IsOnSite")
                                        .HasColumnType("bit");

                                    b2.Property<bool>("IsRemote")
                                        .HasColumnType("bit");

                                    b2.HasKey("TermsCandidateId");

                                    b2.ToTable("Candidates", "candidate");

                                    b2.WithOwner()
                                        .HasForeignKey("TermsCandidateId");
                                });

                            b1.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.PartTimeWorkingHours", "PartTimeWorkingHours", b2 =>
                                {
                                    b2.Property<Guid>("TermsCandidateId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Type")
                                        .IsRequired()
                                        .HasMaxLength(64)
                                        .HasColumnType("nvarchar(64)");

                                    b2.Property<int?>("Weekly")
                                        .HasColumnType("int");

                                    b2.HasKey("TermsCandidateId");

                                    b2.ToTable("Candidates", "candidate");

                                    b2.WithOwner()
                                        .HasForeignKey("TermsCandidateId");
                                });

                            b1.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.Permanent", "Permanent", b2 =>
                                {
                                    b2.Property<Guid>("TermsCandidateId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<decimal?>("MonthlySalary")
                                        .HasColumnType("Decimal(18,2)");

                                    b2.Property<string>("WorkType")
                                        .IsRequired()
                                        .HasMaxLength(64)
                                        .HasColumnType("nvarchar(64)");

                                    b2.HasKey("TermsCandidateId");

                                    b2.ToTable("Candidates", "candidate");

                                    b2.WithOwner()
                                        .HasForeignKey("TermsCandidateId");
                                });

                            b1.Navigation("Formats");

                            b1.Navigation("Freelance");

                            b1.Navigation("FulTimeWorkingHours");

                            b1.Navigation("PartTimeWorkingHours");

                            b1.Navigation("Permanent");
                        });

                    b.Navigation("Address");

                    b.Navigation("CommunicationPreference");

                    b.Navigation("Picture");

                    b.Navigation("Terms")
                        .IsRequired();
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateCourse", b =>
                {
                    b.HasOne("Candidates.Domain.Aggregates.CandidateAggregate.Candidate", null)
                        .WithMany("Courses")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.Certificate", "Certificate", b1 =>
                        {
                            b1.Property<Guid>("CandidateCourseId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FileName")
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)");

                            b1.Property<string>("Uri")
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.HasKey("CandidateCourseId");

                            b1.ToTable("CandidateCourses", "candidate");

                            b1.WithOwner()
                                .HasForeignKey("CandidateCourseId");
                        });

                    b.Navigation("Certificate");
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateEducation", b =>
                {
                    b.HasOne("Candidates.Domain.Aggregates.CandidateAggregate.Candidate", null)
                        .WithMany("Educations")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.Certificate", "Certificate", b1 =>
                        {
                            b1.Property<Guid>("CandidateEducationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FileName")
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)");

                            b1.Property<string>("Uri")
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.HasKey("CandidateEducationId");

                            b1.ToTable("CandidateEducations", "candidate");

                            b1.WithOwner()
                                .HasForeignKey("CandidateEducationId");
                        });

                    b.Navigation("Certificate");
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateIndustry", b =>
                {
                    b.HasOne("Candidates.Domain.Aggregates.CandidateAggregate.Candidate", null)
                        .WithMany("Industries")
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

                    b.HasOne("Candidates.Domain.Aggregates.CandidateAggregate.Candidate", null)
                        .WithMany("DesiredSkills")
                        .HasForeignKey("CandidateId1");
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateWorkExperience", b =>
                {
                    b.HasOne("Candidates.Domain.Aggregates.CandidateAggregate.Candidate", null)
                        .WithMany("WorkExperiences")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateWorkExperienceSkill", b =>
                {
                    b.HasOne("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateWorkExperience", null)
                        .WithMany("Skills")
                        .HasForeignKey("CandidateWorkExperienceId")
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

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities.CandidateArchivedInJob", b =>
                {
                    b.HasOne("Candidates.Domain.Aggregates.CandidateJobsAggregate.CandidateJobs", null)
                        .WithMany("ArchivedInJobs")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Candidates.Domain.Aggregates.CandidateJobsAggregate.ValueObjects.Company", "Company", b1 =>
                        {
                            b1.Property<Guid>("CandidateArchivedInJobId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)");

                            b1.HasKey("CandidateArchivedInJobId");

                            b1.ToTable("CandidateArchivedInJobs", "candidate");

                            b1.WithOwner()
                                .HasForeignKey("CandidateArchivedInJobId");
                        });

                    b.Navigation("Company")
                        .IsRequired();
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities.CandidateSelectedInJob", b =>
                {
                    b.HasOne("Candidates.Domain.Aggregates.CandidateJobsAggregate.CandidateJobs", null)
                        .WithMany("SelectedInJobs")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Candidates.Domain.Aggregates.CandidateJobsAggregate.ValueObjects.Company", "Company", b1 =>
                        {
                            b1.Property<Guid>("CandidateSelectedInJobId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)");

                            b1.HasKey("CandidateSelectedInJobId");

                            b1.ToTable("CandidateSelectedInJobs", "candidate");

                            b1.WithOwner()
                                .HasForeignKey("CandidateSelectedInJobId");
                        });

                    b.Navigation("Company")
                        .IsRequired();
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Candidate", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("DesiredSkills");

                    b.Navigation("Educations");

                    b.Navigation("Industries");

                    b.Navigation("Languages");

                    b.Navigation("Skills");

                    b.Navigation("WorkExperiences");

                    b.Navigation("WorkLocations");
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateWorkExperience", b =>
                {
                    b.Navigation("Skills");
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateJobsAggregate.CandidateJobs", b =>
                {
                    b.Navigation("ArchivedInJobs");

                    b.Navigation("SelectedInJobs");
                });
#pragma warning restore 612, 618
        }
    }
}
