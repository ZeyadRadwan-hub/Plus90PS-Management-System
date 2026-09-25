using Plus90PS.Domain.Employees;

namespace Plus90PS.Domain.Tests.Employees;

public sealed class EmployeeTests
{
    private static readonly Guid EmployeeId = Guid.Parse("019985fc-0000-7000-8000-000000000002");
    private static readonly Guid BusinessId = Guid.Parse("019985fc-0000-7000-8000-000000000001");

    [Theory]
    [InlineData(EmployeeRole.Owner, true)]
    [InlineData(EmployeeRole.Manager, true)]
    [InlineData(EmployeeRole.Cashier, true)]
    [InlineData(EmployeeRole.Owner, false)]
    [InlineData(EmployeeRole.Manager, false)]
    [InlineData(EmployeeRole.Cashier, false)]
    public void PreservesExplicitRoleIdentityAndInitialActiveState(EmployeeRole role, bool isActive)
    {
        var employee = new Employee(EmployeeId, BusinessId, "Zeyad", role, isActive);

        Assert.Equal(EmployeeId, employee.Id);
        Assert.Equal(BusinessId, employee.BusinessId);
        Assert.Equal("Zeyad", employee.Name);
        Assert.Equal(role, employee.Role);
        Assert.Equal(isActive, employee.IsActive);
    }

    [Fact]
    public void TrimsName()
    {
        var employee = new Employee(EmployeeId, BusinessId, "  Zeyad  ", EmployeeRole.Cashier, true);

        Assert.Equal("Zeyad", employee.Name);
    }

    [Fact]
    public void RejectsEmptyEmployeeId()
    {
        Assert.Throws<ArgumentException>(() =>
            new Employee(Guid.Empty, BusinessId, "Zeyad", EmployeeRole.Cashier, true));
    }

    [Fact]
    public void RejectsEmptyBusinessId()
    {
        Assert.Throws<ArgumentException>(() =>
            new Employee(EmployeeId, Guid.Empty, "Zeyad", EmployeeRole.Cashier, true));
    }

    [Fact]
    public void RejectsNullName()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new Employee(EmployeeId, BusinessId, null!, EmployeeRole.Cashier, true));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void RejectsEmptyOrWhitespaceName(string name)
    {
        Assert.Throws<ArgumentException>(() =>
            new Employee(EmployeeId, BusinessId, name, EmployeeRole.Cashier, true));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(99)]
    public void RejectsUnsupportedRole(int role)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Employee(EmployeeId, BusinessId, "Zeyad", (EmployeeRole)role, true));
    }

    [Fact]
    public void ActivateIsIdempotentAndPreservesContext()
    {
        var employee = new Employee(EmployeeId, BusinessId, "Zeyad", EmployeeRole.Manager, false);

        employee.Activate();
        employee.Activate();

        Assert.True(employee.IsActive);
        Assert.Equal(EmployeeId, employee.Id);
        Assert.Equal(BusinessId, employee.BusinessId);
        Assert.Equal("Zeyad", employee.Name);
        Assert.Equal(EmployeeRole.Manager, employee.Role);
    }

    [Fact]
    public void DeactivateIsIdempotentAndPreservesContext()
    {
        var employee = new Employee(EmployeeId, BusinessId, "Zeyad", EmployeeRole.Owner, true);

        employee.Deactivate();
        employee.Deactivate();

        Assert.False(employee.IsActive);
        Assert.Equal(EmployeeId, employee.Id);
        Assert.Equal(BusinessId, employee.BusinessId);
        Assert.Equal("Zeyad", employee.Name);
        Assert.Equal(EmployeeRole.Owner, employee.Role);
    }

    [Fact]
    public void IdentityNameAndRoleHaveNoPublicSettersAndActiveCannotBeSetPublicly()
    {
        foreach (var name in new[]
        {
            nameof(Employee.Id), nameof(Employee.BusinessId), nameof(Employee.Name), nameof(Employee.Role)
        })
        {
            Assert.Null(typeof(Employee).GetProperty(name)!.SetMethod);
        }

        Assert.False(typeof(Employee).GetProperty(nameof(Employee.IsActive))!.SetMethod!.IsPublic);
    }

    [Fact]
    public void EmployeeHasNoBranchIdentityOrUnapprovedMutationOperations()
    {
        Assert.Null(typeof(Employee).GetProperty("BranchId"));
        Assert.Null(typeof(Employee).GetMethod("Delete"));
        Assert.Null(typeof(Employee).GetMethod("ChangeBusiness"));
        Assert.Null(typeof(Employee).GetMethod("ChangeRole"));
        Assert.Null(typeof(Employee).GetMethod("Rename"));
    }

    [Fact]
    public void R1RolesHaveStableExplicitValuesWithoutAdminRole()
    {
        Assert.Equal(1, (int)EmployeeRole.Owner);
        Assert.Equal(2, (int)EmployeeRole.Manager);
        Assert.Equal(3, (int)EmployeeRole.Cashier);
        Assert.Equal(3, Enum.GetValues<EmployeeRole>().Length);
    }
}
