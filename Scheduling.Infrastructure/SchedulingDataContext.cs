using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using Scheduling.Domain;

namespace Scheduling.Infrastructure
{
    public class SchedulingDataContext:DbContext
    {
        public SchedulingDataContext(DbContextOptions<SchedulingDataContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Availability>().ToTable(
                "Availabilities",
                b => b.IsTemporal(
                    b =>
                    {
                        b.HasPeriodStart("ValidFrom");
                        b.HasPeriodEnd("ValidTo");
                        b.UseHistoryTable("AvailabilityHistory");
                    }))
                .HasMany(e => e.Services)
                .WithMany(e => e.Availabilities)
                .UsingEntity("Availabilities_Services");
           
        }
    }
}