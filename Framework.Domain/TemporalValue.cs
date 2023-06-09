﻿namespace Framework.Domain;

public class TemporalValue<T>
{
    public DateOnly EffectiveSince { get; private set; }
    public T Value { get; private set; }
    public TemporalValue(T value, DateOnly effectiveSince)
    {
        Value = value;
        EffectiveSince = effectiveSince;
    }
    protected bool Equals(TemporalValue<T> other)
    {
        return EffectiveSince.Equals(other.EffectiveSince) && EqualityComparer<T>.Default.Equals(Value, other.Value);
    }
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((TemporalValue<T>)obj);
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(EffectiveSince, Value);
    }
    public override string ToString()
    {
        return Value.ToString();
    }
}