using Plus90PS.Domain.Sessions;

namespace Plus90PS.Domain.Tests.Sessions;

public sealed class SessionTermsTests
{
    [Theory]
    [InlineData(ConsoleGeneration.PS4, PlayMode.Single)]
    [InlineData(ConsoleGeneration.PS5, PlayMode.Multi)]
    public void OpenCapturesHourlyTermsWithoutPlannedDuration(ConsoleGeneration generation, PlayMode mode)
    {
        var pricing = new SessionPricingSnapshot(generation, mode, PricingMethod.Hourly, 35m);

        var terms = new SessionTerms(SessionType.Open, pricing);

        Assert.Equal(SessionType.Open, terms.SessionType);
        Assert.Same(pricing, terms.PricingSnapshot);
        Assert.Equal(generation, terms.ConsoleGeneration);
        Assert.Equal(mode, terms.PlayMode);
        Assert.Null(terms.FixedDuration);
        Assert.Null(terms.MatchDuration);
    }

    [Theory]
    [InlineData(ConsoleGeneration.PS4, PlayMode.Single, 60)]
    [InlineData(ConsoleGeneration.PS5, PlayMode.Single, 90)]
    public void FixedCapturesPositivePlannedDuration(ConsoleGeneration generation, PlayMode mode, int minutes)
    {
        var pricing = new SessionPricingSnapshot(generation, mode, PricingMethod.Hourly, 45m);

        var terms = new SessionTerms(SessionType.Fixed, pricing, fixedDuration: TimeSpan.FromMinutes(minutes));

        Assert.Equal(SessionType.Fixed, terms.SessionType);
        Assert.Equal(TimeSpan.FromMinutes(minutes), terms.FixedDuration);
        Assert.Null(terms.MatchDuration);
        Assert.Equal(PricingMethod.Hourly, terms.PricingSnapshot.PricingMethod);
    }

    [Theory]
    [InlineData(ConsoleGeneration.PS4, PlayMode.Multi)]
    [InlineData(ConsoleGeneration.PS5, PlayMode.Multi)]
    public void MatchCapturesFixedPriceAndPositiveReferenceDuration(ConsoleGeneration generation, PlayMode mode)
    {
        var pricing = new SessionPricingSnapshot(generation, mode, PricingMethod.Match, 80m);

        var terms = new SessionTerms(SessionType.Match, pricing, matchDuration: TimeSpan.FromMinutes(25));

        Assert.Equal(SessionType.Match, terms.SessionType);
        Assert.Equal(80m, terms.PricingSnapshot.Price);
        Assert.Equal(TimeSpan.FromMinutes(25), terms.MatchDuration);
        Assert.Null(terms.FixedDuration);
    }

    [Theory]
    [InlineData(SessionType.Open, PricingMethod.Match)]
    [InlineData(SessionType.Fixed, PricingMethod.Match)]
    [InlineData(SessionType.Match, PricingMethod.Hourly)]
    public void RejectsWrongPricingMethod(SessionType type, PricingMethod method)
    {
        var pricing = Pricing(method);
        var fixedDuration = type == SessionType.Fixed ? TimeSpan.FromHours(1) : (TimeSpan?)null;
        var matchDuration = type == SessionType.Match ? TimeSpan.FromMinutes(25) : (TimeSpan?)null;

        Assert.Throws<ArgumentException>(() =>
            new SessionTerms(type, pricing, fixedDuration, matchDuration));
    }

    [Theory]
    [InlineData(60, null)]
    [InlineData(null, 25)]
    public void OpenRejectsEitherDuration(int? fixedMinutes, int? matchMinutes)
    {
        Assert.Throws<ArgumentException>(() =>
            new SessionTerms(SessionType.Open, Pricing(PricingMethod.Hourly),
                Minutes(fixedMinutes), Minutes(matchMinutes)));
    }

    [Fact]
    public void FixedRequiresPlannedDuration()
    {
        Assert.Throws<ArgumentException>(() =>
            new SessionTerms(SessionType.Fixed, Pricing(PricingMethod.Hourly)));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void FixedRejectsNonPositivePlannedDuration(int minutes)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new SessionTerms(SessionType.Fixed, Pricing(PricingMethod.Hourly),
                fixedDuration: TimeSpan.FromMinutes(minutes)));
    }

    [Fact]
    public void FixedRejectsMatchDuration()
    {
        Assert.Throws<ArgumentException>(() =>
            new SessionTerms(SessionType.Fixed, Pricing(PricingMethod.Hourly),
                fixedDuration: TimeSpan.FromHours(1), matchDuration: TimeSpan.FromMinutes(25)));
    }

    [Fact]
    public void MatchRequiresReferenceDuration()
    {
        Assert.Throws<ArgumentException>(() =>
            new SessionTerms(SessionType.Match, Pricing(PricingMethod.Match)));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void MatchRejectsNonPositiveReferenceDuration(int minutes)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new SessionTerms(SessionType.Match, Pricing(PricingMethod.Match),
                matchDuration: TimeSpan.FromMinutes(minutes)));
    }

    [Fact]
    public void MatchRejectsFixedDuration()
    {
        Assert.Throws<ArgumentException>(() =>
            new SessionTerms(SessionType.Match, Pricing(PricingMethod.Match),
                fixedDuration: TimeSpan.FromHours(1), matchDuration: TimeSpan.FromMinutes(25)));
    }

    [Fact]
    public void RejectsNullPricingAndUnsupportedSessionType()
    {
        Assert.Throws<ArgumentNullException>(() => new SessionTerms(SessionType.Open, null!));
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new SessionTerms((SessionType)99, Pricing(PricingMethod.Hourly)));
    }

    [Fact]
    public void CapturedTermsKeepTheirOriginalPriceWhenNewPricingIsCreated()
    {
        var currentPricing = Pricing(PricingMethod.Hourly);
        var terms = new SessionTerms(SessionType.Open, currentPricing);

        currentPricing = new SessionPricingSnapshot(ConsoleGeneration.PS4, PlayMode.Single,
            PricingMethod.Hourly, 99m);

        Assert.Equal(50m, terms.PricingSnapshot.Price);
        Assert.Equal(99m, currentPricing.Price);
    }

    [Fact]
    public void SnapshotAndTermsHaveNoPublicSetters()
    {
        Assert.All(typeof(SessionPricingSnapshot).GetProperties(),
            property => Assert.Null(property.SetMethod));
        Assert.All(typeof(SessionTerms).GetProperties(),
            property => Assert.Null(property.SetMethod));
    }

    private static SessionPricingSnapshot Pricing(PricingMethod method) =>
        new(ConsoleGeneration.PS4, PlayMode.Single, method, 50m);

    private static TimeSpan? Minutes(int? value) =>
        value.HasValue ? TimeSpan.FromMinutes(value.Value) : null;
}
