﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Scheduling.Infrastructure;

#nullable disable

namespace Scheduling.Infrastructure.Migrations
{
    [DbContext(typeof(SchedulingDataContext))]
    [Migration("20230719073600_addServiceList")]
    partial class addServiceList
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Scheduling.Domain.Availability", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("ClinicianId")
                        .HasColumnType("bigint")
                        .HasColumnName("ClinicianId");

                    b.Property<string>("DayOfWeeks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExcludedDays")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("LocationId")
                        .HasColumnType("bigint")
                        .HasColumnName("LocationId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Services")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClinicianId");

                    b.HasIndex("LocationId");

                    b.ToTable("ClientAccounts", (string)null);
                });

            modelBuilder.Entity("Scheduling.Domain.Clinician", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Clinician");
                });

            modelBuilder.Entity("Scheduling.Domain.DayOfWeek", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DayOfWeeks", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Monday"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Tuesday"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Wednesday"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Thursday"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Friday"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Saturday"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Sunday"
                        });
                });

            modelBuilder.Entity("Scheduling.Domain.Location", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("Scheduling.Domain.Availability", b =>
                {
                    b.HasOne("Scheduling.Domain.Clinician", "Clinician")
                        .WithMany()
                        .HasForeignKey("ClinicianId");

                    b.HasOne("Scheduling.Domain.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.OwnsOne("Scheduling.Domain.DateRange", "DateRange", b1 =>
                        {
                            b1.Property<long>("AvailabilityId")
                                .HasColumnType("bigint");

                            b1.Property<DateTime>("BeginDate")
                                .HasColumnType("datetime2")
                                .HasColumnName("StartDate");

                            b1.Property<DateTime>("EndDate")
                                .HasColumnType("datetime2")
                                .HasColumnName("EndDate");

                            b1.HasKey("AvailabilityId");

                            b1.ToTable("ClientAccounts");

                            b1.WithOwner()
                                .HasForeignKey("AvailabilityId");
                        });

                    b.OwnsOne("Scheduling.Domain.TimeRange", "TimeRange", b1 =>
                        {
                            b1.Property<long>("AvailabilityId")
                                .HasColumnType("bigint");

                            b1.Property<TimeSpan>("EndTime")
                                .HasColumnType("time")
                                .HasColumnName("EndTime");

                            b1.Property<TimeSpan>("StartTime")
                                .HasColumnType("time")
                                .HasColumnName("StartTime");

                            b1.HasKey("AvailabilityId");

                            b1.ToTable("ClientAccounts");

                            b1.WithOwner()
                                .HasForeignKey("AvailabilityId");
                        });

                    b.Navigation("Clinician");

                    b.Navigation("DateRange")
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("TimeRange")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}