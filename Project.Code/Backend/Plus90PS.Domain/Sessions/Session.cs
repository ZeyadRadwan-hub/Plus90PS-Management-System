namespace Plus90PS.Domain.Sessions;

public sealed class Session
{
    private readonly SessionTiming _timing;

    private Session(
        Guid id,
        Guid branchId,
        Guid consoleId,
        Guid openedByEmployeeId,
        SessionTerms terms,
        SessionTiming timing)
    {
        Id = id;
        BranchId = branchId;
        ConsoleId = consoleId;
        OpenedByEmployeeId = openedByEmployeeId;
        CurrentResponsibleEmployeeId = openedByEmployeeId;
        Terms = terms;
        _timing = timing;
    }

    public Guid Id { get; }

    public Guid BranchId { get; }

    public Guid ConsoleId { get; }

    public Guid OpenedByEmployeeId { get; }

    public Guid CurrentResponsibleEmployeeId { get; }

    public SessionTerms Terms { get; }

    public DateTimeOffset StartedAt => _timing.StartedAt;

    public DateTimeOffset? CompletedAt => _timing.CompletedAt;

    public SessionTimingState State => _timing.State;

    public IReadOnlyList<PauseInterval> PauseIntervals => _timing.PauseIntervals;

    public TimeSpan ElapsedDuration => _timing.ElapsedDuration;

    public TimeSpan PausedDuration => _timing.PausedDuration;

    public TimeSpan ActiveDuration => _timing.ActiveDuration;

    public int BillableMinutes => _timing.BillableMinutes;

    public static Session Start(
        Guid id,
        Guid branchId,
        Guid consoleId,
        Guid openedByEmployeeId,
        SessionTerms terms,
        DateTimeOffset startedAt)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Session ID must not be empty.", nameof(id));
        if (branchId == Guid.Empty)
            throw new ArgumentException("Branch ID must not be empty.", nameof(branchId));
        if (consoleId == Guid.Empty)
            throw new ArgumentException("Console ID must not be empty.", nameof(consoleId));
        if (openedByEmployeeId == Guid.Empty)
            throw new ArgumentException("Opening employee ID must not be empty.", nameof(openedByEmployeeId));
        ArgumentNullException.ThrowIfNull(terms);

        return new Session(id, branchId, consoleId, openedByEmployeeId, terms, SessionTiming.Start(startedAt));
    }

    public void Pause(DateTimeOffset pausedAt)
    {
        EnsurePauseEligible();
        _timing.Pause(pausedAt);
    }

    public void Resume(DateTimeOffset resumedAt)
    {
        EnsurePauseEligible();
        _timing.Resume(resumedAt);
    }

    public void Complete(DateTimeOffset completedAt) => _timing.Complete(completedAt);

    private void EnsurePauseEligible()
    {
        if (Terms.SessionType == SessionType.Match)
            throw new InvalidOperationException("Match sessions cannot be paused or resumed.");
    }
}
