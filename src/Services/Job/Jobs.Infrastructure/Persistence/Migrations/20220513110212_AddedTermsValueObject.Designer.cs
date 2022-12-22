﻿// <auto-generated />
using System;
using Jobs.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Jobs.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(JobContext))]
    [Migration("20220513110212_AddedTermsValueObject")]
    partial class AddedTermsValueObject
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Jobs.Domain.Aggregates.JobAggregate.Entities.JobArchivedCandidate", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CandidateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("PictureUri")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Stage")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("ArchivedCandidate", "jobs");
                });

            modelBuilder.Entity("Jobs.Domain.Aggregates.JobAggregate.Entities.JobAssignedEmployee", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("AssignedEmployees", "jobs");
                });

            modelBuilder.Entity("Jobs.Domain.Aggregates.JobAggregate.Entities.JobLanguage", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("JobLanguages", "jobs");
                });

            modelBuilder.Entity("Jobs.Domain.Aggregates.JobAggregate.Entities.JobRegion", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("JobRegions", "jobs");
                });

            modelBuilder.Entity("Jobs.Domain.Aggregates.JobAggregate.Entities.JobSelectedCandidate", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CandidateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("PictureUri")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTimeOffset?>("ShortListSendDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Stage")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("SelectedCandidate", "jobs");
                });

            modelBuilder.Entity("Jobs.Domain.Aggregates.JobAggregate.Entities.JobSeniority", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Seniority")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("JobSeniorities", "jobs");
                });

            modelBuilder.Entity("Jobs.Domain.Aggregates.JobAggregate.Entities.JobSkill", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("JobSkills", "jobs");
                });

            modelBuilder.Entity("Jobs.Domain.Aggregates.JobAggregate.Job", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeadlineDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("Industry")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTimeOffset?>("ShortListSendDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Stage")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Jobs", "jobs");
                });

            modelBuilder.Entity("Jobs.Domain.Aggregates.JobAggregate.Entities.JobArchivedCandidate", b =>
                {
                    b.HasOne("Jobs.Domain.Aggregates.JobAggregate.Job", null)
                        .WithMany("ArchivedCandidates")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Jobs.Domain.Aggregates.JobAggregate.Entities.JobAssignedEmployee", b =>
                {
                    b.HasOne("Jobs.Domain.Aggregates.JobAggregate.Job", null)
                        .WithMany("AssignedEmployees")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Jobs.Domain.Aggregates.JobAggregate.ValueObjects.Employee", "Employee", b1 =>
                        {
                            b1.Property<Guid>("JobAssignedEmployeeId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(64)
                                .HasColumnType("nvarchar(64)");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(64)
                                .HasColumnType("nvarchar(64)");

                            b1.Property<string>("PictureUri")
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.HasKey("JobAssignedEmployeeId");

                            b1.ToTable("AssignedEmployees", "jobs");

                            b1.WithOwner()
                                .HasForeignKey("JobAssignedEmployeeId");
                        });

                    b.Navigation("Employee")
                        .IsRequired();
                });

            modelBuilder.Entity("Jobs.Domain.Aggregates.JobAggregate.Entities.JobLanguage", b =>
                {
                    b.HasOne("Jobs.Domain.Aggregates.JobAggregate.Job", null)
                        .WithMany("Languages")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Jobs.Domain.Aggregates.JobAggregate.Entities.JobRegion", b =>
                {
                    b.HasOne("Jobs.Domain.Aggregates.JobAggregate.Job", null)
                        .WithMany("Regions")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Jobs.Domain.Aggregates.JobAggregate.Entities.JobSelectedCandidate", b =>
                {
                    b.HasOne("Jobs.Domain.Aggregates.JobAggregate.Job", null)
                        .WithMany("SelectedCandidates")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Jobs.Domain.Aggregates.JobAggregate.Entities.JobSeniority", b =>
                {
                    b.HasOne("Jobs.Domain.Aggregates.JobAggregate.Job", null)
                        .WithMany("SeniorityLevels")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Jobs.Domain.Aggregates.JobAggregate.Entities.JobSkill", b =>
                {
                    b.HasOne("Jobs.Domain.Aggregates.JobAggregate.Job", null)
                        .WithMany("Skills")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Jobs.Domain.Aggregates.JobAggregate.Job", b =>
                {
                    b.OwnsOne("Jobs.Domain.Aggregates.JobAggregate.ValueObjects.Employee", "Owner", b1 =>
                        {
                            b1.Property<Guid>("JobId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(64)
                                .HasColumnType("nvarchar(64)");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(64)
                                .HasColumnType("nvarchar(64)");

                            b1.Property<string>("PictureUri")
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.HasKey("JobId");

                            b1.ToTable("Jobs", "jobs");

                            b1.WithOwner()
                                .HasForeignKey("JobId");
                        });

                    b.OwnsOne("Jobs.Domain.Aggregates.JobAggregate.ValueObjects.Company", "Company", b1 =>
                        {
                            b1.Property<Guid>("JobId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Description")
                                .HasMaxLength(4000)
                                .HasColumnType("nvarchar(4000)");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("LogoUri")
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(64)
                                .HasColumnType("nvarchar(64)");

                            b1.HasKey("JobId");

                            b1.ToTable("Jobs", "jobs");

                            b1.WithOwner()
                                .HasForeignKey("JobId");

                            b1.OwnsOne("Jobs.Domain.Aggregates.JobAggregate.ValueObjects.Address", "Address", b2 =>
                                {
                                    b2.Property<Guid>("CompanyJobId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("AddressLine")
                                        .IsRequired()
                                        .HasMaxLength(256)
                                        .HasColumnType("nvarchar(256)");

                                    b2.HasKey("CompanyJobId");

                                    b2.ToTable("Jobs", "jobs");

                                    b2.WithOwner()
                                        .HasForeignKey("CompanyJobId");

                                    b2.OwnsOne("Jobs.Domain.Aggregates.JobAggregate.ValueObjects.Coordinates", "Coordinates", b3 =>
                                        {
                                            b3.Property<Guid>("AddressCompanyJobId")
                                                .HasColumnType("uniqueidentifier");

                                            b3.Property<double>("Latitude")
                                                .HasColumnType("float");

                                            b3.Property<double>("Longitude")
                                                .HasColumnType("float");

                                            b3.HasKey("AddressCompanyJobId");

                                            b3.ToTable("Jobs", "jobs");

                                            b3.WithOwner()
                                                .HasForeignKey("AddressCompanyJobId");
                                        });

                                    b2.Navigation("Coordinates");
                                });

                            b1.OwnsMany("Jobs.Domain.Aggregates.JobAggregate.Entities.JobContactPerson", "ContactPersons", b2 =>
                                {
                                    b2.Property<Guid>("Id")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<DateTimeOffset>("CreatedAt")
                                        .HasColumnType("datetimeoffset");

                                    b2.Property<string>("Email")
                                        .IsRequired()
                                        .HasMaxLength(64)
                                        .HasColumnType("nvarchar(64)");

                                    b2.Property<string>("FirstName")
                                        .IsRequired()
                                        .HasMaxLength(64)
                                        .HasColumnType("nvarchar(64)");

                                    b2.Property<bool>("IsMainContact")
                                        .HasColumnType("bit");

                                    b2.Property<Guid>("JobId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("LastName")
                                        .IsRequired()
                                        .HasMaxLength(64)
                                        .HasColumnType("nvarchar(64)");

                                    b2.Property<Guid>("PersonId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("PhoneNumber")
                                        .HasMaxLength(32)
                                        .HasColumnType("nvarchar(32)");

                                    b2.Property<string>("PictureUri")
                                        .HasMaxLength(200)
                                        .HasColumnType("nvarchar(200)");

                                    b2.HasKey("Id");

                                    b2.HasIndex("JobId");

                                    b2.ToTable("JobContactPersons", "jobs");

                                    b2.WithOwner()
                                        .HasForeignKey("JobId");
                                });

                            b1.Navigation("Address");

                            b1.Navigation("ContactPersons");
                        });

                    b.OwnsOne("Jobs.Domain.Aggregates.JobAggregate.ValueObjects.Sharing", "Sharing", b1 =>
                        {
                            b1.Property<Guid>("JobId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTimeOffset>("Date")
                                .HasColumnType("datetimeoffset");

                            b1.Property<Guid>("Key")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("JobId");

                            b1.ToTable("Jobs", "jobs");

                            b1.WithOwner()
                                .HasForeignKey("JobId");
                        });

                    b.OwnsOne("Jobs.Domain.Aggregates.JobAggregate.ValueObjects.Terms", "Terms", b1 =>
                        {
                            b1.Property<Guid>("JobId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Currency")
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)");

                            b1.Property<DateTimeOffset?>("EndDate")
                                .HasColumnType("datetimeoffset");

                            b1.Property<DateTimeOffset?>("StartDate")
                                .HasColumnType("datetimeoffset");

                            b1.HasKey("JobId");

                            b1.ToTable("Jobs", "jobs");

                            b1.WithOwner()
                                .HasForeignKey("JobId");

                            b1.OwnsOne("Jobs.Domain.Aggregates.JobAggregate.ValueObjects.Freelance", "Freelance", b2 =>
                                {
                                    b2.Property<Guid>("TermsJobId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int?>("HoursPerProject")
                                        .HasColumnType("int");

                                    b2.Property<string>("WorkType")
                                        .IsRequired()
                                        .HasMaxLength(64)
                                        .HasColumnType("nvarchar(64)");

                                    b2.HasKey("TermsJobId");

                                    b2.ToTable("Jobs", "jobs");

                                    b2.WithOwner()
                                        .HasForeignKey("TermsJobId");

                                    b2.OwnsOne("Jobs.Domain.Aggregates.JobAggregate.ValueObjects.SalaryBudget", "HourlyBudget", b3 =>
                                        {
                                            b3.Property<Guid>("FreelanceTermsJobId")
                                                .HasColumnType("uniqueidentifier");

                                            b3.Property<decimal?>("From")
                                                .HasColumnType("Decimal(18,2)");

                                            b3.Property<decimal?>("To")
                                                .HasColumnType("Decimal(18,2)");

                                            b3.HasKey("FreelanceTermsJobId");

                                            b3.ToTable("Jobs", "jobs");

                                            b3.WithOwner()
                                                .HasForeignKey("FreelanceTermsJobId");
                                        });

                                    b2.OwnsOne("Jobs.Domain.Aggregates.JobAggregate.ValueObjects.SalaryBudget", "MonthlyBudget", b3 =>
                                        {
                                            b3.Property<Guid>("FreelanceTermsJobId")
                                                .HasColumnType("uniqueidentifier");

                                            b3.Property<decimal?>("From")
                                                .HasColumnType("Decimal(18,2)");

                                            b3.Property<decimal?>("To")
                                                .HasColumnType("Decimal(18,2)");

                                            b3.HasKey("FreelanceTermsJobId");

                                            b3.ToTable("Jobs", "jobs");

                                            b3.WithOwner()
                                                .HasForeignKey("FreelanceTermsJobId");
                                        });

                                    b2.Navigation("HourlyBudget");

                                    b2.Navigation("MonthlyBudget");
                                });

                            b1.OwnsOne("Jobs.Domain.Aggregates.JobAggregate.ValueObjects.FullTimeWorkingHours", "FullTimeWorkingHours", b2 =>
                                {
                                    b2.Property<Guid>("TermsJobId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Type")
                                        .IsRequired()
                                        .HasMaxLength(64)
                                        .HasColumnType("nvarchar(64)");

                                    b2.HasKey("TermsJobId");

                                    b2.ToTable("Jobs", "jobs");

                                    b2.WithOwner()
                                        .HasForeignKey("TermsJobId");
                                });

                            b1.OwnsOne("Jobs.Domain.Aggregates.JobAggregate.ValueObjects.JobFormats", "Formats", b2 =>
                                {
                                    b2.Property<Guid>("TermsJobId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<bool>("IsHybrid")
                                        .HasColumnType("bit");

                                    b2.Property<bool>("IsOnSite")
                                        .HasColumnType("bit");

                                    b2.Property<bool>("IsRemote")
                                        .HasColumnType("bit");

                                    b2.HasKey("TermsJobId");

                                    b2.ToTable("Jobs", "jobs");

                                    b2.WithOwner()
                                        .HasForeignKey("TermsJobId");
                                });

                            b1.OwnsOne("Jobs.Domain.Aggregates.JobAggregate.ValueObjects.PartTimeWorkingHours", "PartTimeWorkingHours", b2 =>
                                {
                                    b2.Property<Guid>("TermsJobId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Type")
                                        .IsRequired()
                                        .HasMaxLength(64)
                                        .HasColumnType("nvarchar(64)");

                                    b2.Property<int?>("Weekly")
                                        .HasColumnType("int");

                                    b2.HasKey("TermsJobId");

                                    b2.ToTable("Jobs", "jobs");

                                    b2.WithOwner()
                                        .HasForeignKey("TermsJobId");
                                });

                            b1.OwnsOne("Jobs.Domain.Aggregates.JobAggregate.ValueObjects.Permanent", "Permanent", b2 =>
                                {
                                    b2.Property<Guid>("TermsJobId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("WorkType")
                                        .IsRequired()
                                        .HasMaxLength(64)
                                        .HasColumnType("nvarchar(64)");

                                    b2.HasKey("TermsJobId");

                                    b2.ToTable("Jobs", "jobs");

                                    b2.WithOwner()
                                        .HasForeignKey("TermsJobId");

                                    b2.OwnsOne("Jobs.Domain.Aggregates.JobAggregate.ValueObjects.SalaryBudget", "MonthlyBudget", b3 =>
                                        {
                                            b3.Property<Guid>("PermanentTermsJobId")
                                                .HasColumnType("uniqueidentifier");

                                            b3.Property<decimal?>("From")
                                                .HasColumnType("Decimal(18,2)");

                                            b3.Property<decimal?>("To")
                                                .HasColumnType("Decimal(18,2)");

                                            b3.HasKey("PermanentTermsJobId");

                                            b3.ToTable("Jobs", "jobs");

                                            b3.WithOwner()
                                                .HasForeignKey("PermanentTermsJobId");
                                        });

                                    b2.Navigation("MonthlyBudget");
                                });

                            b1.Navigation("Formats");

                            b1.Navigation("Freelance");

                            b1.Navigation("FullTimeWorkingHours");

                            b1.Navigation("PartTimeWorkingHours");

                            b1.Navigation("Permanent");
                        });

                    b.OwnsOne("Jobs.Domain.Aggregates.JobAggregate.ValueObjects.YearExperience", "YearExperience", b1 =>
                        {
                            b1.Property<Guid>("JobId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int?>("From")
                                .HasColumnType("int");

                            b1.Property<int?>("To")
                                .HasColumnType("int");

                            b1.HasKey("JobId");

                            b1.ToTable("Jobs", "jobs");

                            b1.WithOwner()
                                .HasForeignKey("JobId");
                        });

                    b.Navigation("Company")
                        .IsRequired();

                    b.Navigation("Owner")
                        .IsRequired();

                    b.Navigation("Sharing");

                    b.Navigation("Terms")
                        .IsRequired();

                    b.Navigation("YearExperience");
                });

            modelBuilder.Entity("Jobs.Domain.Aggregates.JobAggregate.Job", b =>
                {
                    b.Navigation("ArchivedCandidates");

                    b.Navigation("AssignedEmployees");

                    b.Navigation("Languages");

                    b.Navigation("Regions");

                    b.Navigation("SelectedCandidates");

                    b.Navigation("SeniorityLevels");

                    b.Navigation("Skills");
                });
#pragma warning restore 612, 618
        }
    }
}
