using ErrorOr;
using Framework.Domain;

namespace Scheduling.Domain;


public class Location : ValueObject
{
    private Location(long id)
    {
        Id = id;
    }

    public static ErrorOr<Location> Create(long id)
    {
        //    if (id <= 0)
        //        return Errors.General.Required("LocationId");
        return new Location(id);
    }
    public long Id { get; private set; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Id;
    }
}