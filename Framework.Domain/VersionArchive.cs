namespace Framework.Domain;

public class VersionArchive<T> where T : VersionOf
{
    private readonly List<T> _versions = new();
    public IReadOnlyCollection<T> Versions => _versions;

    public VersionArchive()
    {
    }

    public VersionArchive(IEnumerable<T> values)
    {
        _versions = values.ToList();
    }

    public void Set(T item)
    {
        _versions.Add(item);
    }

    public static VersionArchive<T> Create(List<T> values)
    {
        var versions = new VersionArchive<T>(values);
        return versions;
    }

    private VersionArchive(List<T> values)
    {
        _versions = values;
    }

    public T this[int version] => Get(version);

    public T Get(int version)
    {
        var target = Versions.LastOrDefault(c => c.Version == version);

        if (target == null)
            throw new ArgumentOutOfRangeException(nameof(version));

        return target;
    }
}
