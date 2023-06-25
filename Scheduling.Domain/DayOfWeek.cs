using Framework.Domain;

namespace Scheduling.Domain;

public class DayOfWeek : Enumeration
{
    public DayOfWeek(int id, string name) : base(id, name)
    {

    }
    private DayOfWeek()
    {

    }
    public static DayOfWeek Monday = new DayOfWeek(1, nameof(Monday));
    public static DayOfWeek Tuesday = new DayOfWeek(2, nameof(Tuesday));
    public static DayOfWeek Wednesday = new DayOfWeek(3, nameof(Wednesday));
    public static DayOfWeek Thursday = new DayOfWeek(4, nameof(Thursday));
    public static DayOfWeek Friday = new DayOfWeek(5, nameof(Friday));
    public static DayOfWeek Saturday = new DayOfWeek(6, nameof(Saturday));
    public static DayOfWeek Sunday = new DayOfWeek(7, nameof(Sunday));

    public static IEnumerable<DayOfWeek> List() =>
        new[] { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday };

    public static DayOfWeek From(int id)
    {
        var state = List().SingleOrDefault(s => s.Id == id);

        if (state == null)
        {
            throw new Exception($"Possible values for DayOfWeek: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }

    public static implicit operator DayOfWeek(System.DayOfWeek value)
    {
        return From((int)value+1);
    }
}