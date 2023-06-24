using Framework.Domain;

namespace Scheduling.Domain;

public class Clinician : Entity<long>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public EmailAddress Email { get; private set; }

}