using Plus90PS.Domain.Sessions;

namespace Plus90PS.Domain.Consoles;

public sealed class GameConsole
{
    public GameConsole(Guid id, Guid branchId, string name, ConsoleGeneration generation, bool isActive)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Console ID must not be empty.", nameof(id));
        if (branchId == Guid.Empty)
            throw new ArgumentException("Branch ID must not be empty.", nameof(branchId));
        ArgumentNullException.ThrowIfNull(name);

        var trimmedName = name.Trim();
        if (trimmedName.Length == 0)
            throw new ArgumentException("Console name must not be empty or whitespace.", nameof(name));
        if (!Enum.IsDefined(generation))
            throw new ArgumentOutOfRangeException(nameof(generation));

        Id = id;
        BranchId = branchId;
        Name = trimmedName;
        Generation = generation;
        IsActive = isActive;
    }

    public Guid Id { get; }

    public Guid BranchId { get; }

    public string Name { get; }

    public ConsoleGeneration Generation { get; }

    public bool IsActive { get; private set; }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;
}
