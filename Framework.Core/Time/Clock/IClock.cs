namespace Framework.Domain;

public interface IClock
{
    public DateTime Now();
    public DateOnly DateNow();
}