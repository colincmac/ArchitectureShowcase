namespace ArchitectureShowcase.Core.Domain;

// https://github.com/dotnet-architecture/eShopOnContainers/blob/dev/src/Services/Ordering/Ordering.Domain/SeedWork/Entity.cs
public abstract class Entity
{
    private int? _requestedHashCode;

    public virtual string Id { get; protected set; } = string.Empty;

    public bool IsTransient()
    {
        return Id == string.Empty;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null or not Entity)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (GetType() != obj.GetType())
            return false;

        var item = (Entity)obj;

        return !item.IsTransient() && !IsTransient() && item.Id == Id;
    }

    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode = Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

            return _requestedHashCode.Value;
        }
        else
        {
            return base.GetHashCode();
        }
    }

    public static bool operator ==(Entity left, Entity right)
    {
        return Equals(left, null) ? Equals(right, null) : left.Equals(right);
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }
}