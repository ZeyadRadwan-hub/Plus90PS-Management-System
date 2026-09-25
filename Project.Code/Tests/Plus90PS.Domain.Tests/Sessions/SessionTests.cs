using Plus90PS.Domain.Pricing;
using Plus90PS.Domain.Sessions;

namespace Plus90PS.Domain.Tests.Sessions;

public sealed class SessionTests
{
    private static readonly Guid SessionId = Guid.Parse("019985fb-3d31-7bf9-b1b4-748c86f85001");
    private static readonly Guid BranchId = Guid.Parse("019985fb-3d31-7bf9-b1b4-748c86f85002");
    private static readonly Guid ConsoleId = Guid.Parse("019985fb-3d31-7bf9-b1b4-748c86f85003");
    private static readonly Guid EmployeeId = Guid.Parse("019985fb-3d31-7bf9-b1b4-748c86f85004");
    private static readonly DateTimeOffset StartedAt =
        new(2026, 9, 25, 10, 0, 0, TimeSpan.FromHours(2));

    [Theory]
    [InlineData(SessionType.Open)]
    [InlineData(SessionType.Fixed)]
    [InlineData(SessionType.Match)]
    public void StartPreservesSuppliedContextAndBeginsActive(SessionType type)
    {
        var terms = Terms(type);

        var session = Session.Start(SessionId, BranchId, ConsoleId, EmployeeId, terms, StartedAt);

        Assert.Equal(SessionId, session.Id);
        Assert.Equal(BranchId, session.BranchId);
        Assert.Equal(ConsoleId, session.ConsoleId);
        Assert.Equal(EmployeeId, session.OpenedByEmployeeId);
        Assert.Equal(EmployeeId, session.CurrentResponsibleEmployeeId);
        Assert.Same(terms, session.Terms);
        Assert.Equal(StartedAt, session.StartedAt);
        Assert.Equal(StartedAt.Offset, session.StartedAt.Offset);
        Assert.Equal(SessionTimingState.Active, session.State);
        Assert.Null(session.CompletedAt);
        Assert.Empty(session.PauseIntervals);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void StartRejectsEmptyIdentity(int position)
    {
        var ids = new[] { SessionId, BranchId, ConsoleId, EmployeeId };
        ids[position] = Guid.Empty;

        Assert.Throws<ArgumentException>(() =>
            Session.Start(ids[0], ids[1], ids[2], ids[3], Terms(SessionType.Open), StartedAt));
    }

    [Fact]
    public void StartRejectsNullTerms()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Session.Start(SessionId, BranchId, ConsoleId, EmployeeId, null!, StartedAt));
    }

    [Theory]
    [InlineData(SessionType.Open)]
    [InlineData(SessionType.Fixed)]
    public void HourlySessionCanPauseAndResumeMultipleTimes(SessionType type)
    {
        var session = Start(type);

        session.Pause(StartedAt.AddMinutes(20));
        Assert.Equal(SessionTimingState.Paused, session.State);
        session.Resume(StartedAt.AddMinutes(25));
        session.Pause(StartedAt.AddMinutes(40));
        session.Resume(StartedAt.AddMinutes(50));

        Assert.Equal(SessionTimingState.Active, session.State);
        Assert.Equal(2, session.PauseIntervals.Count);
        Assert.Equal(TimeSpan.FromMinutes(5), session.PauseIntervals[0].Duration);
        Assert.Equal(TimeSpan.FromMinutes(10), session.PauseIntervals[1].Duration);
    }

    [Theory]
    [InlineData(SessionType.Open)]
    [InlineData(SessionType.Fixed)]
    [InlineData(SessionType.Match)]
    public void AllSessionTypesRequireExplicitCompletion(SessionType type)
    {
        var session = Start(type);
        var completedAt = StartedAt.AddHours(2);

        Assert.Equal(SessionTimingState.Active, session.State);
        Assert.Null(session.CompletedAt);

        session.Complete(completedAt);

        Assert.Equal(SessionTimingState.Completed, session.State);
        Assert.Equal(completedAt, session.CompletedAt);
    }

    [Fact]
    public void FixedDurationIsOnlyAReferenceAndDoesNotCompleteSession()
    {
        var session = Start(SessionType.Fixed);

        Assert.Equal(TimeSpan.FromHours(1), session.Terms.FixedDuration);
        Assert.Equal(SessionTimingState.Active, session.State);
        Assert.Null(session.CompletedAt);

        session.Complete(StartedAt.AddHours(2));
        Assert.Equal(StartedAt.AddHours(2), session.CompletedAt);
    }

    [Fact]
    public void MatchReferenceDurationDoesNotCompleteOrChangeCapturedPrice()
    {
        var terms = Terms(SessionType.Match);
        var session = Session.Start(SessionId, BranchId, ConsoleId, EmployeeId, terms, StartedAt);

        Assert.Equal(TimeSpan.FromMinutes(25), session.Terms.MatchDuration);
        Assert.Equal(SessionTimingState.Active, session.State);
        Assert.Null(session.CompletedAt);

        session.Complete(StartedAt.AddHours(2));

        Assert.Same(terms.PricingSnapshot, session.Terms.PricingSnapshot);
        Assert.Equal(PricingMethod.Match, session.Terms.PricingSnapshot.PricingMethod);
        Assert.Equal(90m, session.Terms.PricingSnapshot.Price);
    }

    [Fact]
    public void MatchRejectsPauseAndResumeWithoutChangingTiming()
    {
        var session = Start(SessionType.Match);

        Assert.Throws<InvalidOperationException>(() => session.Pause(StartedAt.AddMinutes(10)));
        Assert.Throws<InvalidOperationException>(() => session.Resume(StartedAt.AddMinutes(20)));

        Assert.Equal(SessionTimingState.Active, session.State);
        Assert.Null(session.CompletedAt);
        Assert.Empty(session.PauseIntervals);
        session.Complete(StartedAt.AddMinutes(30));
        Assert.Equal(TimeSpan.FromMinutes(30), session.ElapsedDuration);
    }

    [Fact]
    public void MatchHasNoOperationForAddingAnotherMatch()
    {
        Assert.Null(typeof(Session).GetMethod("AddMatch"));
        Assert.Null(typeof(Session).GetMethod("NextMatch"));
        Assert.Null(typeof(Session).GetProperty("MatchCount"));
    }

    [Fact]
    public void ResumeWhileActiveIsRejectedForHourlySession()
    {
        var session = Start(SessionType.Open);

        Assert.Throws<InvalidOperationException>(() => session.Resume(StartedAt.AddMinutes(1)));
        Assert.Equal(SessionTimingState.Active, session.State);
    }

    [Fact]
    public void SecondPauseIsRejectedWithoutReplacingOriginalPause()
    {
        var session = Start(SessionType.Fixed);
        session.Pause(StartedAt.AddMinutes(20));

        Assert.Throws<InvalidOperationException>(() => session.Pause(StartedAt.AddMinutes(25)));
        session.Resume(StartedAt.AddMinutes(30));

        Assert.Equal(TimeSpan.FromMinutes(10), Assert.Single(session.PauseIntervals).Duration);
    }

    [Fact]
    public void CompletionWhilePausedIsRejectedWithoutAutoResume()
    {
        var session = Start(SessionType.Open);
        session.Pause(StartedAt.AddMinutes(20));

        Assert.Throws<InvalidOperationException>(() => session.Complete(StartedAt.AddMinutes(30)));
        Assert.Equal(SessionTimingState.Paused, session.State);
        Assert.Null(session.CompletedAt);
    }

    [Fact]
    public void CompletedSessionRejectsFurtherTransitions()
    {
        var session = Start(SessionType.Fixed);
        var completedAt = StartedAt.AddMinutes(10);
        session.Complete(completedAt);

        Assert.Throws<InvalidOperationException>(() => session.Pause(StartedAt.AddMinutes(11)));
        Assert.Throws<InvalidOperationException>(() => session.Resume(StartedAt.AddMinutes(11)));
        Assert.Throws<InvalidOperationException>(() => session.Complete(StartedAt.AddMinutes(11)));
        Assert.Equal(completedAt, session.CompletedAt);
    }

    [Fact]
    public void TransitionBeforeStartIsRejected()
    {
        var session = Start(SessionType.Open);

        Assert.Throws<ArgumentOutOfRangeException>(() => session.Pause(StartedAt.AddTicks(-1)));
        Assert.Throws<ArgumentOutOfRangeException>(() => session.Complete(StartedAt.AddTicks(-1)));
        Assert.Equal(SessionTimingState.Active, session.State);
    }

    [Fact]
    public void ResumeBeforePauseIsRejectedWithoutClosingPause()
    {
        var session = Start(SessionType.Open);
        session.Pause(StartedAt.AddMinutes(20));

        Assert.Throws<ArgumentOutOfRangeException>(() => session.Resume(StartedAt.AddMinutes(19)));
        Assert.Equal(SessionTimingState.Paused, session.State);
        Assert.Empty(session.PauseIntervals);

        session.Resume(StartedAt.AddMinutes(25));
        Assert.Equal(TimeSpan.FromMinutes(5), Assert.Single(session.PauseIntervals).Duration);
    }

    [Fact]
    public void CompletionBeforeLastAcceptedTransitionIsRejected()
    {
        var session = Start(SessionType.Open);
        session.Pause(StartedAt.AddMinutes(20));
        session.Resume(StartedAt.AddMinutes(30));

        Assert.Throws<ArgumentOutOfRangeException>(() => session.Complete(StartedAt.AddMinutes(25)));
        Assert.Equal(SessionTimingState.Active, session.State);
        Assert.Null(session.CompletedAt);
    }

    [Fact]
    public void CompletedOpenSessionDelegatesFinalTimingAndBillableMinutes()
    {
        var session = Start(SessionType.Open);
        session.Pause(StartedAt.AddHours(1));
        session.Resume(StartedAt.AddHours(1).AddMinutes(5));
        session.Complete(StartedAt.AddHours(2).AddMinutes(32));

        Assert.Equal(TimeSpan.FromMinutes(152), session.ElapsedDuration);
        Assert.Equal(TimeSpan.FromMinutes(5), session.PausedDuration);
        Assert.Equal(TimeSpan.FromMinutes(147), session.ActiveDuration);
        Assert.Equal(150, session.BillableMinutes);
    }

    [Fact]
    public void MultiplePauseDurationsAreCountedExactlyOnce()
    {
        var session = Start(SessionType.Fixed);
        session.Pause(StartedAt.AddMinutes(20));
        session.Resume(StartedAt.AddMinutes(25));
        session.Pause(StartedAt.AddMinutes(40));
        session.Resume(StartedAt.AddMinutes(50));
        session.Complete(StartedAt.AddHours(1));

        Assert.Equal(TimeSpan.FromMinutes(60), session.ElapsedDuration);
        Assert.Equal(TimeSpan.FromMinutes(15), session.PausedDuration);
        Assert.Equal(TimeSpan.FromMinutes(45), session.ActiveDuration);
        Assert.Equal(45, session.BillableMinutes);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void FinalTimingIsUnavailableBeforeCompletion(bool paused)
    {
        var session = Start(SessionType.Open);
        if (paused)
            session.Pause(StartedAt.AddMinutes(10));

        Assert.Throws<InvalidOperationException>(() => session.ElapsedDuration);
        Assert.Throws<InvalidOperationException>(() => session.PausedDuration);
        Assert.Throws<InvalidOperationException>(() => session.ActiveDuration);
        Assert.Throws<InvalidOperationException>(() => session.BillableMinutes);
    }

    [Fact]
    public void NewCatalogPriceDoesNotChangeStartedSessionTerms()
    {
        var oldCatalog = PricingCatalog.Create([
            new PricingEntry(ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Hourly, 150m)]);
        var oldTerms = new SessionTerms(SessionType.Open,
            oldCatalog.GetRequiredSnapshot(ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Hourly));
        var session = Session.Start(SessionId, BranchId, ConsoleId, EmployeeId, oldTerms, StartedAt);
        var newCatalog = PricingCatalog.Create([
            new PricingEntry(ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Hourly, 170m)]);

        Assert.Same(oldTerms, session.Terms);
        Assert.Equal(150m, session.Terms.PricingSnapshot.Price);
        Assert.Equal(170m,
            newCatalog.GetRequiredSnapshot(ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Hourly).Price);
        Assert.Equal(150m, session.Terms.PricingSnapshot.Price);
    }

    [Fact]
    public void IdentityAndTermsHaveNoPublicSetters()
    {
        foreach (var name in new[]
        {
            nameof(Session.Id), nameof(Session.BranchId), nameof(Session.ConsoleId),
            nameof(Session.OpenedByEmployeeId), nameof(Session.CurrentResponsibleEmployeeId),
            nameof(Session.Terms)
        })
        {
            Assert.Null(typeof(Session).GetProperty(name)!.SetMethod);
        }

        Assert.Null(typeof(Session).GetMethod("TransferResponsibility"));
    }

    [Fact]
    public void TimingCannotBeMutatedOutsideAggregate()
    {
        var session = Start(SessionType.Match);

        Assert.DoesNotContain(typeof(Session).GetProperties(),
            property => property.GetMethod?.IsPublic == true && property.PropertyType == typeof(SessionTiming));
        Assert.DoesNotContain(typeof(Session).GetMethods(),
            method => method.IsPublic && method.ReturnType == typeof(SessionTiming));

        if (session.PauseIntervals is IList<PauseInterval> writable)
        {
            Assert.Throws<NotSupportedException>(() => writable.Add(
                new PauseInterval(StartedAt.AddMinutes(1), StartedAt.AddMinutes(2))));
        }

        Assert.Empty(session.PauseIntervals);
    }

    private static Session Start(SessionType type) =>
        Session.Start(SessionId, BranchId, ConsoleId, EmployeeId, Terms(type), StartedAt);

    private static SessionTerms Terms(SessionType type)
    {
        var method = type == SessionType.Match ? PricingMethod.Match : PricingMethod.Hourly;
        var snapshot = new SessionPricingSnapshot(ConsoleGeneration.PS5, PlayMode.Multi, method, 90m);
        return type switch
        {
            SessionType.Open => new SessionTerms(type, snapshot),
            SessionType.Fixed => new SessionTerms(type, snapshot, fixedDuration: TimeSpan.FromHours(1)),
            SessionType.Match => new SessionTerms(type, snapshot, matchDuration: TimeSpan.FromMinutes(25)),
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
    }
}
