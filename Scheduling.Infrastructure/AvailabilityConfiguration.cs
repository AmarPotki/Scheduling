using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduling.Domain;
using System;
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

        builder.Property<int?>("LocationId")
            .HasColumnName("LocationId")
            .IsRequired(false);

        builder.HasOne(c => c.Clinician)
            .WithMany()
            .HasForeignKey("ClinicianId")
            .IsRequired(false);

        builder.Property<int?>("ClinicianId")
            .HasColumnName("ClinicianId")
            .IsRequired(false);

        builder.OwnsOne(c => c.TimeRange,
            navigationBuilder =>
            {
                navigationBuilder.Property(rate => rate.Maximum)
                    .HasColumnName("StartTime");

                navigationBuilder.Property(rate => rate.Minimum)
                    .HasColumnName("EndTime");
            });

        builder.OwnsOne(c => c.DateRange,
            navigationBuilder =>
            {
                navigationBuilder.Property(rate => rate.Maximum)
                    .HasColumnName("StartDate");

                navigationBuilder.Property(rate => rate.Minimum)
                    .HasColumnName("EndDate");
            });
        //builder.HasMany(c=>c.DayOfWeeks).
        builder.Property(c => c.DayOfWeeks).HasConversion(
            v => string.Join(',', v.Select(c => c.Value)),
            v => v.Split(",", StringSplitOptions.None).Select(c => DayOfWeek.From(int.Parse(c))).ToHashSet());
    }
}