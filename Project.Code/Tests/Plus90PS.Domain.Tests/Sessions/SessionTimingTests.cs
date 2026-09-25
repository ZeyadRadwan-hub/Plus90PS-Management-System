using Plus90PS.Domain.Sessions;

namespace Plus90PS.Domain.Tests.Sessions;

public sealed class SessionTimingTests
{
    private static readonly DateTimeOffset StartedAt =
        new(2026, 9, 25, 10, 0, 0, TimeSpan.FromHours(2));

    [Fact]
    public void StartPreservesTimestampAndBeginsActive()
    {
        var timing = SessionTiming.Start(StartedAt);

        Assert.Equal(SessionTimingState.Active, timing.State);
        Assert.Equal(StartedAt, timing.StartedAt);
        Assert.Equal(StartedAt.Offset, timing.StartedAt.Offset);
        Assert.Null(timing.CompletedAt);
        Assert.Empty(timing.PauseIntervals);
    }

    [Fact]
    public void PauseAndResumeCreateOneCompletedInterval()
    {
        var timing = SessionTiming.Start(StartedAt);
        var pausedAt = StartedAt.AddMinutes(20);
        var resumedAt = pausedAt.AddMinutes(5);

        timing.Pause(pausedAt);

        Assert.Equal(SessionTimingState.Paused, timing.State);
        Assert.Empty(timing.PauseIntervals);

        timing.Resume(resumedAt);

        Assert.Equal(SessionTimingState.Active, timing.State);
        var interval = Assert.Single(timing.PauseIntervals);
        Assert.Equal(pausedAt, interval.PausedAt);
        Assert.Equal(resumedAt, interval.ResumedAt);
        Assert.Equal(TimeSpan.FromMinutes(5), interval.Duration);
    }

    [Fact]
    public void CompletePreservesTimestampAndCalculatesWithExistingBillingRule()
    {
        var timing = SessionTiming.Start(StartedAt);
        timing.Pause(StartedAt.AddHours(1));
        timing.Resume(StartedAt.AddHours(1).AddMinutes(5));
        var completedAt = StartedAt.AddHours(2).AddMinutes(32);

        timing.Complete(completedAt);

        Assert.Equal(SessionTimingState.Completed, timing.State);
        Assert.Equal(completedAt, timing.CompletedAt);
        Assert.Equal(TimeSpan.FromMinutes(152), timing.ElapsedDuration);
        Assert.Equal(TimeSpan.FromMinutes(5), timing.PausedDuration);
        Assert.Equal(TimeSpan.FromMinutes(147), timing.ActiveDuration);
        Assert.Equal(150, timing.BillableMinutes);
    }

    [Fact]
    public void MultiplePausesAreSummedOnce()
    {
        var timing = SessionTiming.Start(StartedAt);
        timing.Pause(StartedAt.AddMinutes(20));
        timing.Resume(StartedAt.AddMinutes(25));
        timing.Pause(StartedAt.AddMinutes(40));
        timing.Resume(StartedAt.AddMinutes(50));
        timing.Complete(StartedAt.AddHours(1));

        Assert.Equal(2, timing.PauseIntervals.Count);
        Assert.Equal(TimeSpan.FromMinutes(60), timing.ElapsedDuration);
        Assert.Equal(TimeSpan.FromMinutes(15), timing.PausedDuration);
        Assert.Equal(TimeSpan.FromMinutes(45), timing.ActiveDuration);
        Assert.Equal(45, timing.BillableMinutes);
    }

    [Fact]
    public void ZeroDurationCompletionIsAllowed()
    {
        var timing = SessionTiming.Start(StartedAt);

        timing.Complete(StartedAt);

        Assert.Equal(TimeSpan.Zero, timing.ElapsedDuration);
        Assert.Equal(TimeSpan.Zero, timing.PausedDuration);
        Assert.Equal(TimeSpan.Zero, timing.ActiveDuration);
        Assert.Equal(0, timing.BillableMinutes);
    }

    [Fact]
    public void FinalResultsAreUnavailableBeforeCompletion()
    {
        var timing = SessionTiming.Start(StartedAt);

        Assert.Throws<InvalidOperationException>(() => timing.ElapsedDuration);
        Assert.Throws<InvalidOperationException>(() => timing.PausedDuration);
        Assert.Throws<InvalidOperationException>(() => timing.ActiveDuration);
        Assert.Throws<InvalidOperationException>(() => timing.BillableMinutes);

        timing.Pause(StartedAt.AddMinutes(20));

        Assert.Throws<InvalidOperationException>(() => timing.ElapsedDuration);
        Assert.Throws<InvalidOperationException>(() => timing.PausedDuration);
        Assert.Throws<InvalidOperationException>(() => timing.ActiveDuration);
        Assert.Throws<InvalidOperationException>(() => timing.BillableMinutes);
    }

    [Fact]
    public void ResumeWhileActiveIsRejected()
    {
        var timing = SessionTiming.Start(StartedAt);

        Assert.Throws<InvalidOperationException>(() => timing.Resume(StartedAt.AddMinutes(1)));
        Assert.Equal(SessionTimingState.Active, timing.State);
        Assert.Empty(timing.PauseIntervals);
    }

    [Fact]
    public void PauseWhilePausedIsRejectedWithoutReplacingOpenPause()
    {
        var timing = SessionTiming.Start(StartedAt);
        timing.Pause(StartedAt.AddMinutes(20));

        Assert.Throws<InvalidOperationException>(() => timing.Pause(StartedAt.AddMinutes(25)));
        timing.Resume(StartedAt.AddMinutes(30));

        Assert.Equal(TimeSpan.FromMinutes(10), Assert.Single(timing.PauseIntervals).Duration);
    }

    [Fact]
    public void CompleteWhilePausedIsRejected()
    {
        var timing = SessionTiming.Start(StartedAt);
        timing.Pause(StartedAt.AddMinutes(20));

        Assert.Throws<InvalidOperationException>(() => timing.Complete(StartedAt.AddMinutes(30)));
        Assert.Equal(SessionTimingState.Paused, timing.State);
        Assert.Null(timing.CompletedAt);
    }

    [Fact]
    public void PauseAfterCompletionIsRejected()
    {
        var timing = SessionTiming.Start(StartedAt);
        timing.Complete(StartedAt.AddMinutes(10));

        Assert.Throws<InvalidOperationException>(() => timing.Pause(StartedAt.AddMinutes(11)));
        Assert.Equal(SessionTimingState.Completed, timing.State);
        Assert.Empty(timing.PauseIntervals);
    }

    [Fact]
    public void ResumeAfterCompletionIsRejected()
    {
        var timing = SessionTiming.Start(StartedAt);
        timing.Complete(StartedAt.AddMinutes(10));

        Assert.Throws<InvalidOperationException>(() => timing.Resume(StartedAt.AddMinutes(11)));
        Assert.Equal(SessionTimingState.Completed, timing.State);
    }

    [Fact]
    public void CompleteAfterCompletionIsRejectedWithoutReplacingTimestamp()
    {
        var timing = SessionTiming.Start(StartedAt);
        var firstCompletion = StartedAt.AddMinutes(10);
        timing.Complete(firstCompletion);

        Assert.Throws<InvalidOperationException>(() => timing.Complete(StartedAt.AddMinutes(20)));
        Assert.Equal(firstCompletion, timing.CompletedAt);
    }

    [Fact]
    public void PauseBeforeStartIsRejected()
    {
        var timing = SessionTiming.Start(StartedAt);

        Assert.Throws<ArgumentOutOfRangeException>(() => timing.Pause(StartedAt.AddTicks(-1)));
        Assert.Equal(SessionTimingState.Active, timing.State);
    }

    [Fact]
    public void ResumeBeforePauseIsRejectedWithoutClosingPause()
    {
        var timing = SessionTiming.Start(StartedAt);
        timing.Pause(StartedAt.AddMinutes(20));

        Assert.Throws<ArgumentOutOfRangeException>(() => timing.Resume(StartedAt.AddMinutes(19)));
        Assert.Equal(SessionTimingState.Paused, timing.State);
        Assert.Empty(timing.PauseIntervals);

        timing.Resume(StartedAt.AddMinutes(25));
        Assert.Equal(TimeSpan.FromMinutes(5), Assert.Single(timing.PauseIntervals).Duration);
    }

    [Fact]
    public void CompleteBeforeLastTransitionIsRejected()
    {
        var timing = SessionTiming.Start(StartedAt);
        timing.Pause(StartedAt.AddMinutes(20));
        timing.Resume(StartedAt.AddMinutes(30));

        Assert.Throws<ArgumentOutOfRangeException>(() => timing.Complete(StartedAt.AddMinutes(25)));
        Assert.Equal(SessionTimingState.Active, timing.State);
        Assert.Null(timing.CompletedAt);
    }

    [Fact]
    public void CompleteBeforeStartIsRejected()
    {
        var timing = SessionTiming.Start(StartedAt);

        Assert.Throws<ArgumentOutOfRangeException>(() => timing.Complete(StartedAt.AddTicks(-1)));
        Assert.Equal(SessionTimingState.Active, timing.State);
    }

    [Fact]
    public void EqualPauseAndResumeTimestampsMakeZeroPause()
    {
        var timing = SessionTiming.Start(StartedAt);
        var transitionAt = StartedAt.AddMinutes(20);
        timing.Pause(transitionAt);
        timing.Resume(transitionAt);
        timing.Complete(transitionAt);

        Assert.Equal(TimeSpan.Zero, Assert.Single(timing.PauseIntervals).Duration);
        Assert.Equal(TimeSpan.Zero, timing.PausedDuration);
        Assert.Equal(TimeSpan.FromMinutes(20), timing.ActiveDuration);
    }

    [Fact]
    public void PauseIntervalRejectsReversedTimestamps()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new PauseInterval(StartedAt.AddMinutes(10), StartedAt.AddMinutes(9)));
    }

    [Fact]
    public void ExposedPauseIntervalsCannotBeMutated()
    {
        var timing = SessionTiming.Start(StartedAt);
        timing.Pause(StartedAt.AddMinutes(5));
        timing.Resume(StartedAt.AddMinutes(10));

        if (timing.PauseIntervals is IList<PauseInterval> writable)
        {
            Assert.Throws<NotSupportedException>(() => writable.Add(
                new PauseInterval(StartedAt.AddMinutes(11), StartedAt.AddMinutes(12))));
        }

        Assert.Single(timing.PauseIntervals);
    }
}
