using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DayOfWeek = Scheduling.Domain.DayOfWeek;

namespace Scheduling.Infrastructure;

public class DayOfWeekConfiguration : IEntityTypeConfiguration<DayOfWeek>
{
    public void Configure(EntityTypeBuilder<DayOfWeek> builder)
    {
        builder.ToTable("DayOfWeeks");

        builder
            .Property(p => p.Value)
            .ValueGeneratedNever();

        builder
            .Property(p => p.Name)
            .IsRequired(required: true);

        builder.HasData(DayOfWeek.Monday);
        builder.HasData(DayOfWeek.Tuesday);
        builder.HasData(DayOfWeek.Wednesday);
        builder.HasData(DayOfWeek.Thursday);
        builder.HasData(DayOfWeek.Friday);
        builder.HasData(DayOfWeek.Saturday);
        builder.HasData(DayOfWeek.Sunday);

    }
}