namespace Framework.Domain;

public class EffectivityOf<T> : ValueObject
{
    public EffectivityOf(T value, DateTime since)
    {
        Value = value;
        Since = since;
    }

    public EffectivityOf(T value)
    {
        Value = value;
        Since = DateTime.Now;
    }

    public T Value { get; private set; }

    public DateTime Since { get; private set; }

    public static explicit operator T(EffectivityOf<T> effectivity)
    {
        return effectivity.Value;
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
        yield return Since;
    }
}
