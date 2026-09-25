using Plus90PS.Domain.Billing;

namespace Plus90PS.Domain.Sessions;

public sealed class SessionTiming
{
    private readonly List<PauseInterval> _pauseIntervals = [];
    private DateTimeOffset? _currentPauseStartedAt;
    private DateTimeOffset _lastAcceptedAt;

    private SessionTiming(DateTimeOffset startedAt)
    {
        StartedAt = startedAt;
        _lastAcceptedAt = startedAt;
        State = SessionTimingState.Active;
    }

    public DateTimeOffset StartedAt { get; }

    public DateTimeOffset? CompletedAt { get; private set; }

    public SessionTimingState State { get; private set; }

    public IReadOnlyList<PauseInterval> PauseIntervals => _pauseIntervals.AsReadOnly();

    public TimeSpan ElapsedDuration => GetCompletedAt() - StartedAt;

    public TimeSpan PausedDuration
    {
        get
        {
            GetCompletedAt();
            var total = TimeSpan.Zero;
            foreach (var interval in _pauseIntervals)
            {
                total += interval.Duration;
            }

            return total;
        }
    }

    public TimeSpan ActiveDuration => ElapsedDuration - PausedDuration;

    public int BillableMinutes => BillableMinutesCalculator.Calculate(ElapsedDuration, PausedDuration);

    public static SessionTiming Start(DateTimeOffset startedAt) => new(startedAt);

    public void Pause(DateTimeOffset pausedAt)
    {
        EnsureState(SessionTimingState.Active);
        EnsureChronological(pausedAt, nameof(pausedAt));

        _currentPauseStartedAt = pausedAt;
        _lastAcceptedAt = pausedAt;
        State = SessionTimingState.Paused;
    }

    public void Resume(DateTimeOffset resumedAt)
    {
        EnsureState(SessionTimingState.Paused);
        EnsureChronological(resumedAt, nameof(resumedAt));

        _pauseIntervals.Add(new PauseInterval(_currentPauseStartedAt!.Value, resumedAt));
        _currentPauseStartedAt = null;
        _lastAcceptedAt = resumedAt;
        State = SessionTimingState.Active;
    }

    public void Complete(DateTimeOffset completedAt)
    {
        EnsureState(SessionTimingState.Active);
        EnsureChronological(completedAt, nameof(completedAt));

        CompletedAt = completedAt;
        _lastAcceptedAt = completedAt;
        State = SessionTimingState.Completed;
    }

    private void EnsureState(SessionTimingState required)
    {
        if (State != required)
        {
            throw new InvalidOperationException($"Timing must be {required} for this transition.");
        }
    }

    private void EnsureChronological(DateTimeOffset timestamp, string parameterName)
    {
        if (timestamp < _lastAcceptedAt)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Transition time cannot precede the last accepted time.");
        }
    }

    private DateTimeOffset GetCompletedAt() =>
        CompletedAt ?? throw new InvalidOperationException("Final timing results are unavailable before completion.");
}
