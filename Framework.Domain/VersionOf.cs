namespace Framework.Domain;

public class VersionOf : ValueObject
{
    protected VersionOf()
    {
    }

    public VersionOf(int version)
    {
        Version = version;
    }

    public int Version { get; private set; }


    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Version;
    }
}
