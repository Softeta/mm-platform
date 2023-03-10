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
    [Migration("20220228104226_AddCandidateContext")]
    partial class AddCandidateContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Candidates.Domain.Aggregates.Candidate", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("AvailableFrom")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ContractType")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CurrentPosition")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid?>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("LastName")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

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

            modelBuilder.Entity("Candidates.Domain.Aggregates.Candidate", b =>
                {
                    b.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.CommunicationPreference", "CommunicationPreference", b1 =>
                        {
                            b1.Property<Guid>("CandidateId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("PhoneNumber")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("CandidateId");

                            b1.ToTable("Candidates", "candidate");

                            b1.WithOwner()
                                .HasForeignKey("CandidateId");
                        });

                    b.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.FeeRange", "FeeRange", b1 =>
                        {
                            b1.Property<Guid>("CandidateId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(64)
                                .HasColumnType("nvarchar(64)");

                            b1.HasKey("CandidateId");

                            b1.ToTable("Candidates", "candidate");

                            b1.WithOwner()
                                .HasForeignKey("CandidateId");

                            b1.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.AmountRange", "PerHour", b2 =>
                                {
                                    b2.Property<Guid>("FeeRangeCandidateId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<decimal>("From")
                                        .HasColumnType("Decimal(18,2)");

                                    b2.Property<decimal>("To")
                                        .HasColumnType("Decimal(18,2)");

                                    b2.HasKey("FeeRangeCandidateId");

                                    b2.ToTable("Candidates", "candidate");

                                    b2.WithOwner()
                                        .HasForeignKey("FeeRangeCandidateId");
                                });

                            b1.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.AmountRange", "PerMonth", b2 =>
                                {
                                    b2.Property<Guid>("FeeRangeCandidateId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<decimal>("From")
                                        .HasColumnType("Decimal(18,2)");

                                    b2.Property<decimal>("To")
                                        .HasColumnType("Decimal(18,2)");

                                    b2.HasKey("FeeRangeCandidateId");

                                    b2.ToTable("Candidates", "candidate");

                                    b2.WithOwner()
                                        .HasForeignKey("FeeRangeCandidateId");
                                });

                            b1.Navigation("PerHour");

                            b1.Navigation("PerMonth");
                        });

                    b.OwnsOne("Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects.Location", "Location", b1 =>
                        {
                            b1.Property<Guid>("CandidateId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Country")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("CandidateId");

                            b1.ToTable("Candidates", "candidate");

                            b1.WithOwner()
                                .HasForeignKey("CandidateId");
                        });

                    b.Navigation("CommunicationPreference");

                    b.Navigation("FeeRange");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateJobFormat", b =>
                {
                    b.HasOne("Candidates.Domain.Aggregates.Candidate", null)
                        .WithMany("Formats")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.CandidateAggregate.Entities.CandidateSkill", b =>
                {
                    b.HasOne("Candidates.Domain.Aggregates.Candidate", null)
                        .WithMany("Skills")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Candidates.Domain.Aggregates.Candidate", b =>
                {
                    b.Navigation("Formats");

                    b.Navigation("Skills");
                });
#pragma warning restore 612, 618
        }
    }
}
