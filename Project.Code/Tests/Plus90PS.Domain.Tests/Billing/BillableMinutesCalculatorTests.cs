using Plus90PS.Domain.Billing;

namespace Plus90PS.Domain.Tests.Billing;

public sealed class BillableMinutesCalculatorTests
{
    [Theory]
    [InlineData(0, 0, 0, 0, 0)]
    [InlineData(0, 0, 1, 0, 1)]
    [InlineData(0, 0, 59, 0, 1)]
    [InlineData(0, 1, 0, 0, 1)]
    [InlineData(0, 1, 1, 0, 2)]
    [InlineData(2, 27, 0, 0, 150)]
    [InlineData(2, 20, 0, 0, 140)]
    [InlineData(2, 59, 0, 0, 180)]
    [InlineData(2, 26, 0, 0, 146)]
    [InlineData(2, 30, 0, 0, 150)]
    [InlineData(2, 30, 0, 10, 140)]
    [InlineData(2, 32, 0, 5, 150)]
    [InlineData(2, 26, 1, 0, 150)]
    [InlineData(2, 42, 0, 0, 165)]
    [InlineData(2, 57, 0, 0, 180)]
    public void CalculateAppliesFreePauseMinuteCeilingAndQuarterThreshold(
        int elapsedHours,
        int elapsedMinutes,
        int elapsedSeconds,
        int pausedMinutes,
        int expectedMinutes)
    {
        var elapsed = new TimeSpan(elapsedHours, elapsedMinutes, elapsedSeconds);
        var paused = TimeSpan.FromMinutes(pausedMinutes);

        Assert.Equal(expectedMinutes, BillableMinutesCalculator.Calculate(elapsed, paused));
    }

    [Fact]
    public void CalculateRoundsSubsecondActiveTimeUpToOneMinute()
    {
        Assert.Equal(1, BillableMinutesCalculator.Calculate(TimeSpan.FromTicks(1), TimeSpan.Zero));
    }

    [Fact]
    public void CalculateRejectsNegativeElapsed()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            BillableMinutesCalculator.Calculate(TimeSpan.FromTicks(-1), TimeSpan.Zero));
    }

    [Fact]
    public void CalculateRejectsNegativePaused()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            BillableMinutesCalculator.Calculate(TimeSpan.Zero, TimeSpan.FromTicks(-1)));
    }

    [Fact]
    public void CalculateRejectsPausedGreaterThanElapsed()
    {
        Assert.Throws<ArgumentException>(() =>
            BillableMinutesCalculator.Calculate(TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(2)));
    }

    [Fact]
    public void CalculateRejectsResultOutsideIntRange()
    {
        Assert.Throws<OverflowException>(() =>
            BillableMinutesCalculator.Calculate(TimeSpan.MaxValue, TimeSpan.Zero));
    }
}
