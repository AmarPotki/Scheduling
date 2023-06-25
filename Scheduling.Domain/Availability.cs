using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Domain;

namespace Scheduling.Domain
{
    public class Availability : Entity<long>
    {
        private Availability()
        {

        }
        public Availability(HashSet<DayOfWeek> dayOfWeeks,
            DateRange dateRange,
            TimeRange timeRange, string name, Clinician clinician, Location location, IReadOnlyList<Service> services)
        {
            DayOfWeeks = dayOfWeeks;
            DateRange = dateRange;
            TimeRange = timeRange;
            Name = name;
            Clinician = clinician;
            Location = location;
            Services = services;
        }

        public HashSet<DayOfWeek> DayOfWeeks { get; set; }
        public DateRange DateRange { get; set; }
        public TimeRange TimeRange { get; set; }
        public string Name { get; set; }
        public long? ClinicianId { get; set; }
        public Clinician Clinician { get; set; }


        public long? LocationId { get; private set; }

        public Location Location { get; set; }
        public IReadOnlyList<Service> Services { get; set; }

        public void UpdateDateRange(DateRange dateRange)
        {
            DateRange = dateRange;
        }

        public bool IsOccurring(DateOnly targetDate)
        {
          return  DateRange.IsBetween(targetDate) && DayOfWeeks.Contains(targetDate.DayOfWeek);
        }
    }
}
