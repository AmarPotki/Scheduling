using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using Framework.Domain;

namespace Scheduling.Domain
{
    public class Availability : AggregateRoot<long>
    {
        private Availability()
        {

        }
        private Availability(string name, HashSet<DayOfWeek> dayOfWeeks,
            DateRange dateRange,
            TimeRange timeRange, Clinician clinician, Location location, ServiceList services)
        {
            DayOfWeeks = dayOfWeeks;
            DateRange = dateRange;
            TimeRange = timeRange;
            Name = name;
            Clinician = clinician;
            Location = location;
            Services = services;
            _excludedDays = new HashSet<DateOnly>();
        }

        public static ErrorOr<Availability> Create(string name, HashSet<DayOfWeek> dayOfWeeks,
            DateRange dateRange,
            TimeRange timeRange, Clinician clinician, Location location, ServiceList services)
        {
            var check = CheckRequirements(name, dateRange, timeRange, clinician, location);
            if (check.IsError)
                return check.Errors;

            return new Availability(name, dayOfWeeks, dateRange, timeRange, clinician, location, services);
        }


        private static ErrorOr<Success> CheckRequirements
        (string name, DateRange dateRange,
            TimeRange timeRange, Clinician clinician, Location location)
        {
            var availabilityErrors = new List<Error>();

            //if (dateRange is null)
            //    availabilityErrors.Add(Errors.General.Required(nameof(DateRange)));

            //if (timeRange is null)
            //    availabilityErrors.Add(Errors.General.Required(nameof(TimeRange)));

            //if (string.IsNullOrWhiteSpace(name))
            //    availabilityErrors.Add(Errors.General.Required(nameof(Name)));

            //if (clinician is null)
            //    availabilityErrors.Add(Errors.General.Required(nameof(Clinician)));

            //if (location is null)
            //    availabilityErrors.Add(Errors.General.Required(nameof(Location)));

            if (availabilityErrors.Any())
                return availabilityErrors;

            return Result.Success;
        }

        public HashSet<DayOfWeek> DayOfWeeks { get; private set; }
        public DateRange DateRange { get; private set; }
        public TimeRange TimeRange { get; private set; }
        public string Name { get; private set; }
        public Clinician Clinician { get; private set; }
        public IReadOnlySet<DateOnly> ExcludedDays => _excludedDays;
        private readonly HashSet<DateOnly> _excludedDays;
        public Location Location { get; private set; }
        public ServiceList Services { get; private set; }

        public void UpdateDateRange(DateRange dateRange)
        {
            DateRange = dateRange;
        }

        public bool IsOccurring(DateOnly targetDate)
        {
            return DateRange.IsBetween(targetDate) &&
                   DayOfWeeks.Contains(targetDate.DayOfWeek) &&
                   !_excludedDays.Contains(targetDate);
        }

        public void ExcludeDay(DateOnly date)
        {
            if (!IsOccurring(date)) throw new Exception("");
            _excludedDays.Add(date);
        }

        public void Archive(DateOnly date)
        {
            if (!DateRange.IsBetween(date)) throw new Exception("");
            var res = DateRange.Create(DateRange.BeginDate, date);
            if (!res.IsError)
                DateRange = res.Value;

        }
    }
}
