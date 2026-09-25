namespace Plus90PS.Domain.Employees;

public sealed class Employee
{
    public Employee(Guid id, Guid businessId, string name, EmployeeRole role, bool isActive)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Employee ID must not be empty.", nameof(id));
        if (businessId == Guid.Empty)
            throw new ArgumentException("Business ID must not be empty.", nameof(businessId));
        ArgumentNullException.ThrowIfNull(name);

        var trimmedName = name.Trim();
        if (trimmedName.Length == 0)
            throw new ArgumentException("Employee name must not be empty or whitespace.", nameof(name));
        if (!Enum.IsDefined(role))
            throw new ArgumentOutOfRangeException(nameof(role));

        Id = id;
        BusinessId = businessId;
        Name = trimmedName;
        Role = role;
        IsActive = isActive;
    }

    public Guid Id { get; }

    public Guid BusinessId { get; }

    public string Name { get; }

    public EmployeeRole Role { get; }

    public bool IsActive { get; private set; }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;
}
