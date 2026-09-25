namespace Plus90PS.Domain.Branches;

public sealed class Branch
{
    public Branch(Guid id, string name)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Branch ID must not be empty.", nameof(id));
        ArgumentNullException.ThrowIfNull(name);

        var trimmedName = name.Trim();
        if (trimmedName.Length == 0)
            throw new ArgumentException("Branch name must not be empty or whitespace.", nameof(name));

        Id = id;
        Name = trimmedName;
    }

    public Guid Id { get; }

    public string Name { get; }
}
