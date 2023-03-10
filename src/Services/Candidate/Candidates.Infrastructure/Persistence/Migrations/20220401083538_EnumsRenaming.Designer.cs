// <auto-generated />
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
    [Migration("20220401083538_EnumsRenaming")]
    partial class EnumsRenaming
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

                    b.Property<DateTimeOffset?>("AvailableFrom")
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

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateWorkType", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CandidateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("WorkType")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.ToTable("CandidateWorkTypes", "candidate");
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Candidate", b =>
                {
                    b.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("CandidateId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .HasMaxLength(64)
                                .HasColumnType("nvarchar(64)");

                            b1.Property<string>("Country")
                                .HasMaxLength(64)
                                .HasColumnType("nvarchar(64)");

                            b1.Property<string>("FullAddress")
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)");

                            b1.HasKey("CandidateId");

                            b1.ToTable("Candidates", "candidate");

                            b1.WithOwner()
                                .HasForeignKey("CandidateId");
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

                    b.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.MinimumSalary", "MinimumSalary", b1 =>
                        {
                            b1.Property<Guid>("CandidateId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)");

                            b1.Property<decimal?>("PerHour")
                                .HasColumnType("Decimal(18,2)");

                            b1.Property<decimal?>("PerMonth")
                                .HasColumnType("Decimal(18,2)");

                            b1.HasKey("CandidateId");

                            b1.ToTable("Candidates", "candidate");

                            b1.WithOwner()
                                .HasForeignKey("CandidateId");
                        });

                    b.Navigation("Address");

                    b.Navigation("CommunicationPreference");

                    b.Navigation("MinimumSalary");
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateJobFormat", b =>
                {
                    b.HasOne("Candidates.Domain.Aggregates.CandidateAggregate.Candidate", null)
                        .WithMany("Formats")
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

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateWorkType", b =>
                {
                    b.HasOne("Candidates.Domain.Aggregates.CandidateAggregate.Candidate", null)
                        .WithMany("WorkTypes")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Candidate", b =>
                {
                    b.Navigation("Formats");

                    b.Navigation("Skills");

                    b.Navigation("WorkTypes");
                });
#pragma warning restore 612, 618
        }
    }
}
