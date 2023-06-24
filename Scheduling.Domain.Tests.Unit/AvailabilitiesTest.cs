using FluentAssertions;
using Framework.Domain;

namespace Scheduling.Domain.Tests.Unit;

public class AvailabilitiesTest
{
    [Fact]
    public void Calculate_slots_between_dates()
    {
        var rangeOfPlan = new DateRange
            (new DateOnly(2023, 1, 1), new DateOnly(2023, 12, 31));

        var timeOfPlane = new TimeRange
            (new TimeOnly(8, 0), new TimeOnly(13, 0));

        var days = new HashSet<DayOfWeek>
        {
            DayOfWeek.Wednesday,
            DayOfWeek.Friday,
        };

        //var availabilityVersion = new AvailabilityVersion(days, rangeOfPlan, timeOfPlane, "my Availability", new Clinician(),
        //    new Location(), new List<Service>());

        //var availability = new Availability(availabilityVersion, SystemClock.Instance);


    }
}