using Plus90PS.Domain.Branches;

namespace Plus90PS.Domain.Employees;

public static class EmployeeBranchScope
{
    public static bool IsWithinBusinessScope(Employee employee, Branch branch)
    {
        ArgumentNullException.ThrowIfNull(employee);
        ArgumentNullException.ThrowIfNull(branch);

        return employee.IsActive && employee.BusinessId == branch.BusinessId;
    }
}
