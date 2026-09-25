namespace Plus90PS.Domain.Sessions;

public sealed class SessionTerms
{
    public SessionTerms(
        SessionType sessionType,
        SessionPricingSnapshot pricingSnapshot,
        TimeSpan? fixedDuration = null,
        TimeSpan? matchDuration = null)
    {
        ArgumentNullException.ThrowIfNull(pricingSnapshot);
        if (!Enum.IsDefined(sessionType))
            throw new ArgumentOutOfRangeException(nameof(sessionType));

        switch (sessionType)
        {
            case SessionType.Open:
                RequireMethod(pricingSnapshot, PricingMethod.Hourly);
                if (fixedDuration.HasValue)
                    throw new ArgumentException("Open sessions have no fixed duration.", nameof(fixedDuration));
                if (matchDuration.HasValue)
                    throw new ArgumentException("Open sessions have no match duration.", nameof(matchDuration));
                break;

            case SessionType.Fixed:
                RequireMethod(pricingSnapshot, PricingMethod.Hourly);
                if (!fixedDuration.HasValue)
                    throw new ArgumentException("Fixed sessions require a planned duration.", nameof(fixedDuration));
                if (fixedDuration.Value <= TimeSpan.Zero)
                    throw new ArgumentOutOfRangeException(nameof(fixedDuration));
                if (matchDuration.HasValue)
                    throw new ArgumentException("Fixed sessions have no match duration.", nameof(matchDuration));
                break;

            case SessionType.Match:
                RequireMethod(pricingSnapshot, PricingMethod.Match);
                if (!matchDuration.HasValue)
                    throw new ArgumentException("Match sessions require a reference duration.", nameof(matchDuration));
                if (matchDuration.Value <= TimeSpan.Zero)
                    throw new ArgumentOutOfRangeException(nameof(matchDuration));
                if (fixedDuration.HasValue)
                    throw new ArgumentException("Match sessions have no fixed duration.", nameof(fixedDuration));
                break;
        }

        SessionType = sessionType;
        PricingSnapshot = pricingSnapshot;
        FixedDuration = fixedDuration;
        MatchDuration = matchDuration;
    }

    public SessionType SessionType { get; }

    public ConsoleGeneration ConsoleGeneration => PricingSnapshot.ConsoleGeneration;

    public PlayMode PlayMode => PricingSnapshot.PlayMode;

    public SessionPricingSnapshot PricingSnapshot { get; }

    public TimeSpan? FixedDuration { get; }

    public TimeSpan? MatchDuration { get; }

    private static void RequireMethod(SessionPricingSnapshot pricingSnapshot, PricingMethod expected)
    {
        if (pricingSnapshot.PricingMethod != expected)
            throw new ArgumentException($"This session type requires {expected} pricing.", nameof(pricingSnapshot));
    }
}
