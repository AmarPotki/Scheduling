using Scheduling.Domain;
using DayOfWeek = Scheduling.Domain.DayOfWeek;

namespace Scheduling.Api
{
    public class AvailabilityCommand
    {
        public HashSet<DayOfWeek> DayOfWeeks { get; set; }
        public DateOnly NewStartDate { get; set; }
        public DateOnly NewEndDate { get; set; }
        public TimeRange TimeRange { get; set; }
        public string Name { get; set; }
        public long? ClinicianId { get; set; }
        public IReadOnlySet<DateOnly> ExcludedDays => _excludedDays;
        private readonly HashSet<DateOnly> _excludedDays;

        public long? LocationId { get; private set; }

        public IReadOnlyList<Service> Services { get; set; }
    }
}
