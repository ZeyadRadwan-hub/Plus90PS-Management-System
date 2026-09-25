namespace Plus90PS.Domain.Businesses;

public sealed class Business
{
    public Business(Guid id, string name)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Business ID must not be empty.", nameof(id));
        ArgumentNullException.ThrowIfNull(name);

        var trimmedName = name.Trim();
        if (trimmedName.Length == 0)
            throw new ArgumentException("Business name must not be empty or whitespace.", nameof(name));

        Id = id;
        Name = trimmedName;
    }

    public Guid Id { get; }

    public string Name { get; }
}
