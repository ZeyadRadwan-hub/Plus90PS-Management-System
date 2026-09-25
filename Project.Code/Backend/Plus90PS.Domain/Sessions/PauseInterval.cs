namespace Plus90PS.Domain.Sessions;

public sealed class PauseInterval
{
    public PauseInterval(DateTimeOffset pausedAt, DateTimeOffset resumedAt)
    {
        if (resumedAt < pausedAt)
        {
            throw new ArgumentOutOfRangeException(nameof(resumedAt), "Resume time cannot precede pause time.");
        }

        PausedAt = pausedAt;
        ResumedAt = resumedAt;
    }

    public DateTimeOffset PausedAt { get; }

    public DateTimeOffset ResumedAt { get; }

    public TimeSpan Duration => ResumedAt - PausedAt;
}
