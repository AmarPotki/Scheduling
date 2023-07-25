using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Scheduling.Infrastructure;

public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter() : base(
        dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
        dateTime => DateOnly.FromDateTime(dateTime))
    { }
}


public class DateOnlyListConverter : ValueConverter<IReadOnlySet<DateOnly>, string>
{
    public DateOnlyListConverter() : base(
        dateOnly => JsonConvert.SerializeObject(dateOnly, Formatting.None),
        str => JsonConvert.DeserializeObject<IReadOnlySet<DateOnly>>(str))
    { }
}