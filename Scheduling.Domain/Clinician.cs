using ErrorOr;
using Framework.Domain;

namespace Scheduling.Domain;

public class Clinician : ValueObject
{
    private Clinician(long id)
    {
        Id = id;
    }

    public static ErrorOr<Clinician> Create(long id)
    {
        //if (id <= 0)
        //    return Errors.General.Required("ClinicianId");
        return new Clinician(id);
    }
    public long Id { get; private set; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Id;
    }
}