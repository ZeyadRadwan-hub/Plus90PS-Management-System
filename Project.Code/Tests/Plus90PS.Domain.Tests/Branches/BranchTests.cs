using Plus90PS.Domain.Branches;

namespace Plus90PS.Domain.Tests.Branches;

public sealed class BranchTests
{
    private static readonly Guid BranchId = Guid.Parse("019985f8-23ac-72ee-9a45-c78648a9c065");

    [Fact]
    public void ValidBranchPreservesSuppliedIdentityAndName()
    {
        var branch = new Branch(BranchId, "Downtown");

        Assert.Equal(BranchId, branch.Id);
        Assert.Equal("Downtown", branch.Name);
    }

    [Fact]
    public void TrimsNameBeforeStoring()
    {
        var branch = new Branch(BranchId, "  Downtown  ");

        Assert.Equal("Downtown", branch.Name);
    }

    [Fact]
    public void RejectsEmptyId()
    {
        Assert.Throws<ArgumentException>(() => new Branch(Guid.Empty, "Downtown"));
    }

    [Fact]
    public void RejectsNullName()
    {
        Assert.Throws<ArgumentNullException>(() => new Branch(BranchId, null!));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void RejectsEmptyOrWhitespaceName(string name)
    {
        Assert.Throws<ArgumentException>(() => new Branch(BranchId, name));
    }

    [Fact]
    public void IdentityAndNameHaveNoPublicSetters()
    {
        Assert.Null(typeof(Branch).GetProperty(nameof(Branch.Id))!.SetMethod);
        Assert.Null(typeof(Branch).GetProperty(nameof(Branch.Name))!.SetMethod);
    }
}
