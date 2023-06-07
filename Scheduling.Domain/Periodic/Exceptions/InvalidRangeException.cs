namespace Scheduling.Domain.Periodic.Exceptions;

public class InvalidRangeException : Exception
{
    public InvalidRangeException() : base
        ("Minimum value should be smaller or equal to maximum value")
    {

    }
}