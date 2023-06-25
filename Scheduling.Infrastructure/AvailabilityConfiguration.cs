﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduling.Domain;
using System;
using Framework.Domain;
using DayOfWeek = Scheduling.Domain.DayOfWeek;

namespace Scheduling.Infrastructure;

public class AvailabilityConfiguration : IEntityTypeConfiguration<Availability>
{
    public void Configure(EntityTypeBuilder<Availability> builder)
    {
        builder.ToTable("ClientAccounts");
        builder.Property<string>(c => c.Name).IsRequired();
        builder.HasOne(c => c.Location)
            .WithMany()
            .HasForeignKey("LocationId")
            .IsRequired(false);

        builder.Property<long?>("LocationId")
            .HasColumnName("LocationId")
            .IsRequired(false);

        builder.HasOne(c => c.Clinician)
            .WithMany()
            .HasForeignKey("ClinicianId")
            .IsRequired(false);

        builder.Property<long?>("ClinicianId")
            .HasColumnName("ClinicianId")
            .IsRequired(false);

        builder.OwnsOne(c => c.TimeRange,
            navigationBuilder =>
            {
                navigationBuilder.Property(r => r.Maximum)
                    .HasColumnName("StartTime").HasConversion<TimeOnlyConverter>();

                navigationBuilder.Property(r => r.Minimum)
                    .HasColumnName("EndTime").HasConversion<TimeOnlyConverter>();

                navigationBuilder.ToTable($"Availabilities", tbuilder =>
                {
                    tbuilder.IsTemporal(t1 =>
                    {
                        t1.UseHistoryTable($"AvailabilityHistory");
                        t1.HasPeriodStart("ValidFrom");
                        t1.HasPeriodEnd("ValidTo");
                    });
                    tbuilder.Property<DateTime>("ValidFrom").HasColumnName("ValidFrom");
                    tbuilder.Property<DateTime>("ValidTo").HasColumnName("ValidTo");
                });
            });

        builder.OwnsOne(c => c.DateRange,
            navigationBuilder =>
            {
                navigationBuilder.Property(r => r.Maximum)
                    .HasColumnName("StartDate").HasConversion<DateOnlyConverter>(); 

                navigationBuilder.Property(r => r.Minimum)
                    .HasColumnName("EndDate").HasConversion<DateOnlyConverter>();

                navigationBuilder.ToTable($"Availabilities", tbuilder =>
                {
                    tbuilder.IsTemporal(
                        t1 =>
                    {
                        t1.UseHistoryTable($"AvailabilityHistory");
                        t1.HasPeriodStart("ValidFrom");
                        t1.HasPeriodEnd("ValidTo");
                    });
                    tbuilder.Property<DateTime>("ValidFrom").HasColumnName("ValidFrom");
                    tbuilder.Property<DateTime>("ValidTo").HasColumnName("ValidTo");
                });
            });
        //builder.HasMany(c=>c.DayOfWeeks).
        builder.Property(c => c.DayOfWeeks).HasConversion(
            v => string.Join(',', v.Select(c => c.Id)),
            v => v.Split(",", StringSplitOptions.None).Select(c => DayOfWeek.From(int.Parse(c))).ToHashSet());
    }
}