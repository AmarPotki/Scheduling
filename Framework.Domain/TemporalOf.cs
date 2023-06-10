namespace Framework.Domain;

public class TemporalOf<T>
{
    public TemporalOf(T initialValue)
    {
        Set(initialValue);
    }

    private readonly List<EffectivityOf<T>> _records = new();

    public T Value => Get();

    public T this[DateTime at] => GetAt(at);

    public void Set(T item)
    {
        _records.Add(new EffectivityOf<T>(item));
    }

    public static explicit operator TemporalOf<T>(T value) =>
        new(value);

    public static explicit operator T(TemporalOf<T> temporal) =>
        temporal.Value;

    public T Get() => 
        _records.Last().Value;

    public T GetAt(DateTime targetDate)
    {
        var target = _records.LastOrDefault(c => c.Since < targetDate);

        if (target == null)
            throw new ArgumentOutOfRangeException(nameof(targetDate));

        return target.Value;
    }
}

public static class TemporalExtentions
{
    public static TemporalOf<T> ToTemporal<T>(this T value)
    {
        return new TemporalOf<T>(value);
    }
}
