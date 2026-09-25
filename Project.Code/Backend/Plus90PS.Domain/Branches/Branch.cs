namespace Plus90PS.Domain.Branches;

public sealed class Branch
{
    public Branch(Guid id, Guid businessId, string name)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Branch ID must not be empty.", nameof(id));
        if (businessId == Guid.Empty)
            throw new ArgumentException("Business ID must not be empty.", nameof(businessId));
        ArgumentNullException.ThrowIfNull(name);

        var trimmedName = name.Trim();
        if (trimmedName.Length == 0)
            throw new ArgumentException("Branch name must not be empty or whitespace.", nameof(name));

        Id = id;
        BusinessId = businessId;
        Name = trimmedName;
    }

    public Guid Id { get; }

    public Guid BusinessId { get; }

    public string Name { get; }
}
