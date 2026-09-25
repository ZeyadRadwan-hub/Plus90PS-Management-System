using Plus90PS.Domain.Businesses;

namespace Plus90PS.Domain.Tests.Businesses;

public sealed class BusinessTests
{
    private static readonly Guid BusinessId = Guid.Parse("019985fc-0000-7000-8000-000000000001");

    [Fact]
    public void ValidBusinessPreservesIdentityAndTrimsName()
    {
        var business = new Business(BusinessId, "  Plus Ninety  ");

        Assert.Equal(BusinessId, business.Id);
        Assert.Equal("Plus Ninety", business.Name);
    }

    [Fact]
    public void RejectsEmptyId()
    {
        Assert.Throws<ArgumentException>(() => new Business(Guid.Empty, "Plus Ninety"));
    }

    [Fact]
    public void RejectsNullName()
    {
        Assert.Throws<ArgumentNullException>(() => new Business(BusinessId, null!));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void RejectsEmptyOrWhitespaceName(string name)
    {
        Assert.Throws<ArgumentException>(() => new Business(BusinessId, name));
    }

    [Fact]
    public void IdentityAndNameCannotBePubliclyReplaced()
    {
        Assert.Null(typeof(Business).GetProperty(nameof(Business.Id))!.SetMethod);
        Assert.Null(typeof(Business).GetProperty(nameof(Business.Name))!.SetMethod);
    }
}
