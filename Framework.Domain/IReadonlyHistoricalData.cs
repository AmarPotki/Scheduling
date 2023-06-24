namespace Framework.Domain;

public interface IReadonlyHistoricalData<T>
{
    public T? GetValue();
    T? EffectiveValueAt(DateOnly effectiveSince);
}