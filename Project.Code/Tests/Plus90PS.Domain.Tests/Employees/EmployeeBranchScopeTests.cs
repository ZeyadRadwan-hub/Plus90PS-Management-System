using Plus90PS.Domain.Branches;
using Plus90PS.Domain.Employees;
using Plus90PS.Domain.Sessions;

namespace Plus90PS.Domain.Tests.Employees;

public sealed class EmployeeBranchScopeTests
{
    private static readonly Guid EmployeeId = Guid.Parse("019985fc-0000-7000-8000-000000000002");
    private static readonly Guid BusinessX = Guid.Parse("019985fc-0000-7000-8000-000000000001");
    private static readonly Guid BusinessY = Guid.Parse("019985fc-0000-7000-8000-000000000003");
    private static readonly Guid BranchA = Guid.Parse("019985fc-0000-7000-8000-000000000004");
    private static readonly Guid BranchB = Guid.Parse("019985fc-0000-7000-8000-000000000005");
    private static readonly Guid BranchC = Guid.Parse("019985fc-0000-7000-8000-000000000006");

    [Theory]
    [InlineData(EmployeeRole.Owner)]
    [InlineData(EmployeeRole.Manager)]
    [InlineData(EmployeeRole.Cashier)]
    public void ActiveEmployeeIsWithinBothSameBusinessBranchesButNotAnotherBusiness(EmployeeRole role)
    {
        var employee = new Employee(EmployeeId, BusinessX, "Zeyad", role, true);
        var firstBranch = new Branch(BranchA, BusinessX, "A");
        var secondBranch = new Branch(BranchB, BusinessX, "B");
        var foreignBranch = new Branch(BranchC, BusinessY, "C");

        Assert.True(EmployeeBranchScope.IsWithinBusinessScope(employee, firstBranch));
        Assert.True(EmployeeBranchScope.IsWithinBusinessScope(employee, secondBranch));
        Assert.False(EmployeeBranchScope.IsWithinBusinessScope(employee, foreignBranch));
    }

    [Theory]
    [InlineData(EmployeeRole.Owner)]
    [InlineData(EmployeeRole.Manager)]
    [InlineData(EmployeeRole.Cashier)]
    public void DeactivationRemovesScopeAcrossEveryBranchInTheBusiness(EmployeeRole role)
    {
        var employee = new Employee(EmployeeId, BusinessX, "Zeyad", role, true);
        var firstBranch = new Branch(BranchA, BusinessX, "A");
        var secondBranch = new Branch(BranchB, BusinessX, "B");

        employee.Deactivate();

        Assert.False(EmployeeBranchScope.IsWithinBusinessScope(employee, firstBranch));
        Assert.False(EmployeeBranchScope.IsWithinBusinessScope(employee, secondBranch));
    }

    [Fact]
    public void InitiallyInactiveEmployeeIsOutsideBusinessScopeUntilActivated()
    {
        var employee = new Employee(EmployeeId, BusinessX, "Zeyad", EmployeeRole.Cashier, false);
        var branch = new Branch(BranchA, BusinessX, "A");

        Assert.False(EmployeeBranchScope.IsWithinBusinessScope(employee, branch));
        employee.Activate();
        Assert.True(EmployeeBranchScope.IsWithinBusinessScope(employee, branch));
    }

    [Fact]
    public void DeactivationDoesNotEraseHistoricalSessionOpenerIdentity()
    {
        var employee = new Employee(EmployeeId, BusinessX, "Zeyad", EmployeeRole.Cashier, true);
        var sessionId = Guid.Parse("019985fc-0000-7000-8000-000000000007");
        var consoleId = Guid.Parse("019985fc-0000-7000-8000-000000000008");
        var terms = new SessionTerms(SessionType.Open,
            new SessionPricingSnapshot(ConsoleGeneration.PS5, PlayMode.Single, PricingMethod.Hourly, 100m));
        var startedAt = new DateTimeOffset(2026, 9, 25, 10, 0, 0, TimeSpan.Zero);
        var session = Session.Start(sessionId, BranchA, consoleId, employee.Id, terms, startedAt);

        employee.Deactivate();

        Assert.Equal(EmployeeId, session.OpenedByEmployeeId);
        Assert.Equal(EmployeeId, session.CurrentResponsibleEmployeeId);
        Assert.False(EmployeeBranchScope.IsWithinBusinessScope(employee, new Branch(BranchA, BusinessX, "A")));
    }

    [Fact]
    public void RejectsNullArguments()
    {
        var employee = new Employee(EmployeeId, BusinessX, "Zeyad", EmployeeRole.Cashier, true);
        var branch = new Branch(BranchA, BusinessX, "A");

        Assert.Throws<ArgumentNullException>(() => EmployeeBranchScope.IsWithinBusinessScope(null!, branch));
        Assert.Throws<ArgumentNullException>(() => EmployeeBranchScope.IsWithinBusinessScope(employee, null!));
    }
}
