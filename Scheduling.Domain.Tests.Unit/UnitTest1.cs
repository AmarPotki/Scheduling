using FluentAssertions;
using Scheduling.Domain.Periodic;

namespace Scheduling.Domain.Tests.Unit
{
    public class UnitTest1
    {
        [Fact]
        public void Calculate_slots_between_dates()
        {
            var rangeOfPlan = new DateRange
                (new DateOnly(2023, 1, 1), new DateOnly(2023, 12, 31));

            var timeOfPlane = new TimeRage
                (new TimeOnly(2, 0), new TimeOnly(5, 0));

            var days = new List<DayOfWeek>
            {
                DayOfWeek.Wednesday,
                DayOfWeek.Friday,
            };

            var plan = new PeriodicPlan
                (rangeOfPlan, timeOfPlane, days);

            var expectedDates = new List<Slot>
            {
                new Slot(new DateOnly(2023,5,3),new TimeRage(new TimeOnly(2, 0), new TimeOnly(5, 0))),
                new Slot(new DateOnly(2023,5,5),new TimeRage(new TimeOnly(2, 0), new TimeOnly(5, 0))),
                new Slot(new DateOnly(2023,5,10),new TimeRage(new TimeOnly(2, 0), new TimeOnly(5, 0))),
                new Slot(new DateOnly(2023,5,12),new TimeRage(new TimeOnly(2, 0), new TimeOnly(5, 0))),
            };

            var actualSlots = plan.Calculate
                (new DateRange(new DateOnly(2023, 5, 1), new DateOnly(2023, 5, 15)));

            expectedDates.Should().BeEquivalentTo(actualSlots);
        }
    }
}