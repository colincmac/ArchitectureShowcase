namespace ArchitectureShowcase.Domain;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class VersionedEventAttribute : Attribute
{
    public string EventDomain { get; }
    public string EventName { get; }
    public int Version { get; }
    public string VersionPrefix { get; }

    public string FullVersionedName => $"{EventDomain}.{EventName}.{VersionPrefix}{Version}";

    public VersionedEventAttribute(string eventDomain, string eventName, int version = 1, string versionPrefix = "v")
    {
        if (string.IsNullOrEmpty(eventDomain))
            throw new ArgumentNullException(nameof(eventDomain));
        if (string.IsNullOrEmpty(eventName))
            throw new ArgumentNullException(nameof(eventName));
        if (version < 1)
            throw new ArgumentOutOfRangeException(nameof(version), "Version must be >= 1");
        EventDomain = eventDomain;
        EventName = eventName;
        Version = version;
        VersionPrefix = versionPrefix;
    }

}
