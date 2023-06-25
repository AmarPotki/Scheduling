using System.Reflection;
using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using Scheduling.Domain;

namespace Scheduling.Infrastructure
{
    public class SchedulingDataContext : DbContext
    {
        public SchedulingDataContext(DbContextOptions<SchedulingDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly
                (Assembly.GetExecutingAssembly(), x => x.Namespace == typeof(AvailabilityConfiguration).Namespace);
            modelBuilder.Entity<Availability>().ToTable(
                "Availabilities",
                b =>
                {
                    b.IsTemporal(
                        t =>
                        {
                            t.HasPeriodStart("ValidFrom");
                            t.HasPeriodEnd("ValidTo");
                            t.UseHistoryTable("AvailabilityHistory");
                        });
                    b.Property("ValidFrom").HasColumnName("ValidFrom");
                    b.Property("ValidTo").HasColumnName("ValidTo");
                })
                .HasMany(e => e.Services)
                .WithMany(e => e.Availabilities)
                .UsingEntity("Availabilities_Services");

        }
    }
}