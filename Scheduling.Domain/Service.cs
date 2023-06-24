using Framework.Domain;

namespace Scheduling.Domain;

public class Service : Entity<long>
{
    public Service(string name)
    {
        Name = name;
    }

    public string Name { get;private set; }
    public IReadOnlyCollection<Availability> Availabilities { get; set; }
}