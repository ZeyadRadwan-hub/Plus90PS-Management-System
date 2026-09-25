using Plus90PS.Domain.Branches;

namespace Plus90PS.Domain.Tests.Branches;

public sealed class BranchTests
{
    private static readonly Guid BranchId = Guid.Parse("019985f8-23ac-72ee-9a45-c78648a9c065");
    private static readonly Guid BusinessId = Guid.Parse("019985f8-23ac-72ee-9a45-c78648a9c066");

    [Fact]
    public void ValidBranchPreservesSuppliedIdentityAndName()
    {
        var branch = new Branch(BranchId, BusinessId, "Downtown");

        Assert.Equal(BranchId, branch.Id);
        Assert.Equal(BusinessId, branch.BusinessId);
        Assert.Equal("Downtown", branch.Name);
    }

    [Fact]
    public void TrimsNameBeforeStoring()
    {
        var branch = new Branch(BranchId, BusinessId, "  Downtown  ");

        Assert.Equal("Downtown", branch.Name);
    }

    [Fact]
    public void RejectsEmptyId()
    {
        Assert.Throws<ArgumentException>(() => new Branch(Guid.Empty, BusinessId, "Downtown"));
    }

    [Fact]
    public void RejectsEmptyBusinessId()
    {
        Assert.Throws<ArgumentException>(() => new Branch(BranchId, Guid.Empty, "Downtown"));
    }

    [Fact]
    public void RejectsNullName()
    {
        Assert.Throws<ArgumentNullException>(() => new Branch(BranchId, BusinessId, null!));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void RejectsEmptyOrWhitespaceName(string name)
    {
        Assert.Throws<ArgumentException>(() => new Branch(BranchId, BusinessId, name));
    }

    [Fact]
    public void IdentityAndNameHaveNoPublicSetters()
    {
        Assert.Null(typeof(Branch).GetProperty(nameof(Branch.Id))!.SetMethod);
        Assert.Null(typeof(Branch).GetProperty(nameof(Branch.BusinessId))!.SetMethod);
        Assert.Null(typeof(Branch).GetProperty(nameof(Branch.Name))!.SetMethod);
    }
}
