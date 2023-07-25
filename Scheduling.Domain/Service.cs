using ErrorOr;
using Framework.Domain;

namespace Scheduling.Domain;

public class Service : ValueObject
{
    private Service(long id)
    {
        Id = id;
    }

    public static ErrorOr<Service> Create(long id)
    {
        //    if (id <= 0)
        //        return Errors.General.Required("LocationId");
        return new Service(id);
    }
    public long Id { get; set; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Id;
    }
}