using Plus90PS.Domain.Consoles;
using Plus90PS.Domain.Sessions;

namespace Plus90PS.Domain.Tests.Consoles;

public sealed class GameConsoleTests
{
    private static readonly Guid ConsoleId = Guid.Parse("019985fa-5813-7c30-87c2-af081a35e5b6");
    private static readonly Guid BranchId = Guid.Parse("019985fa-6e8e-7e37-bddc-a920ee27ad36");

    [Theory]
    [InlineData(ConsoleGeneration.PS4, true)]
    [InlineData(ConsoleGeneration.PS4, false)]
    [InlineData(ConsoleGeneration.PS5, true)]
    [InlineData(ConsoleGeneration.PS5, false)]
    public void ValidConsolePreservesIdentityConfigurationAndInitialActiveState(
        ConsoleGeneration generation, bool isActive)
    {
        var console = new GameConsole(ConsoleId, BranchId, "Room 1", generation, isActive);

        Assert.Equal(ConsoleId, console.Id);
        Assert.Equal(BranchId, console.BranchId);
        Assert.Equal("Room 1", console.Name);
        Assert.Equal(generation, console.Generation);
        Assert.Equal(isActive, console.IsActive);
    }

    [Fact]
    public void TrimsName()
    {
        var console = new GameConsole(ConsoleId, BranchId, "  Room 1  ", ConsoleGeneration.PS5, true);

        Assert.Equal("Room 1", console.Name);
    }

    [Fact]
    public void RejectsEmptyConsoleId()
    {
        Assert.Throws<ArgumentException>(() =>
            new GameConsole(Guid.Empty, BranchId, "Room 1", ConsoleGeneration.PS4, true));
    }

    [Fact]
    public void RejectsEmptyBranchId()
    {
        Assert.Throws<ArgumentException>(() =>
            new GameConsole(ConsoleId, Guid.Empty, "Room 1", ConsoleGeneration.PS4, true));
    }

    [Fact]
    public void RejectsNullName()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new GameConsole(ConsoleId, BranchId, null!, ConsoleGeneration.PS4, true));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void RejectsEmptyOrWhitespaceName(string name)
    {
        Assert.Throws<ArgumentException>(() =>
            new GameConsole(ConsoleId, BranchId, name, ConsoleGeneration.PS4, true));
    }

    [Fact]
    public void RejectsUnsupportedGeneration()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new GameConsole(ConsoleId, BranchId, "Room 1", (ConsoleGeneration)99, true));
    }

    [Fact]
    public void ActivateIsIdempotentAndDoesNotMutateIdentityOrConfiguration()
    {
        var console = new GameConsole(ConsoleId, BranchId, "Room 1", ConsoleGeneration.PS4, false);

        console.Activate();
        console.Activate();

        Assert.True(console.IsActive);
        Assert.Equal(ConsoleId, console.Id);
        Assert.Equal(BranchId, console.BranchId);
        Assert.Equal("Room 1", console.Name);
        Assert.Equal(ConsoleGeneration.PS4, console.Generation);
    }

    [Fact]
    public void DeactivateIsIdempotentAndDoesNotMutateIdentityOrConfiguration()
    {
        var console = new GameConsole(ConsoleId, BranchId, "Room 1", ConsoleGeneration.PS5, true);

        console.Deactivate();
        console.Deactivate();

        Assert.False(console.IsActive);
        Assert.Equal(ConsoleId, console.Id);
        Assert.Equal(BranchId, console.BranchId);
        Assert.Equal("Room 1", console.Name);
        Assert.Equal(ConsoleGeneration.PS5, console.Generation);
    }

    [Fact]
    public void OnlyIsActiveCanChangeAndHasNoPublicSetter()
    {
        Assert.Null(typeof(GameConsole).GetProperty(nameof(GameConsole.Id))!.SetMethod);
        Assert.Null(typeof(GameConsole).GetProperty(nameof(GameConsole.BranchId))!.SetMethod);
        Assert.Null(typeof(GameConsole).GetProperty(nameof(GameConsole.Name))!.SetMethod);
        Assert.Null(typeof(GameConsole).GetProperty(nameof(GameConsole.Generation))!.SetMethod);
        Assert.False(typeof(GameConsole).GetProperty(nameof(GameConsole.IsActive))!.SetMethod!.IsPublic);
    }
}
