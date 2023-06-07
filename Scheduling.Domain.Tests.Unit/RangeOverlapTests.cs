using FluentAssertions;
using Scheduling.Domain.Periodic;

namespace Scheduling.Domain.Tests.Unit;

public class RangeOverlapTests
{
    [Theory]
    [InlineData("2013-1-1", "2013-1-15")]
    [InlineData("2013-1-10", "2013-1-25")]
    public void Minimum_should_not_exists_in_range
        (string fromDate, string toDate)
    {
        var sourceRange = new Range<DateOnly>
            (DateOnly.Parse(fromDate), DateOnly.Parse(toDate));

        var targetRange = new Range<DateOnly>
            (new DateOnly(2013, 1, 10), new DateOnly(2013, 1, 20));

        var overlap = targetRange.IsOverlapWith(sourceRange);

        overlap.Should().BeTrue();
    }

    [Theory]
    [InlineData("2013-1-5", "2013-1-20")]
    [InlineData("2013-1-5", "2013-1-15")]
    [InlineData("2013-1-15", "2013-1-18")]
    [InlineData("2013-1-15", "2013-1-20")]
    public void Maximum_should_not_exists_in_range
        (string fromDate, string toDate)
    {
        var sourceRange = new Range<DateOnly>
            (DateOnly.Parse(fromDate), DateOnly.Parse(toDate));

        var targetRange = new Range<DateOnly>
            (new DateOnly(2013, 1, 10), new DateOnly(2013, 1, 20));

        var overlap = targetRange.IsOverlapWith(sourceRange);

        overlap.Should().BeTrue();
    }

    [Fact]
    public void Overlaps_when_range_contains_another_range()
    {
        var sourceRange = new Range<DateOnly>
            (new DateOnly(2013,1,5), new DateOnly(2013, 1, 30));

        var targetRange = new Range<DateOnly>
            (new DateOnly(2013, 1, 10), new DateOnly(2013, 1, 20));

        var overlap = targetRange.IsOverlapWith(sourceRange);

        overlap.Should().BeTrue();
    }

    [Fact]
    public void When_range_are_not_overlaps_eachother()
    {
        var sourceRange = new Range<DateOnly>
            (new DateOnly(2013, 1, 10), new DateOnly(2013, 1, 20));

        var targetRange = new Range<DateOnly>
            (new DateOnly(2013, 1, 21), new DateOnly(2013, 1, 30));

        var overlap = targetRange.IsOverlapWith(sourceRange);

        overlap.Should().BeFalse();
    }
}
