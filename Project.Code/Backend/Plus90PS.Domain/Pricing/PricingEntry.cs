using Plus90PS.Domain.Sessions;

namespace Plus90PS.Domain.Pricing;

public sealed record PricingEntry
{
    public PricingEntry(
        ConsoleGeneration consoleGeneration,
        PlayMode playMode,
        PricingMethod pricingMethod,
        decimal price)
    {
        if (!Enum.IsDefined(consoleGeneration))
            throw new ArgumentOutOfRangeException(nameof(consoleGeneration));
        if (!Enum.IsDefined(playMode))
            throw new ArgumentOutOfRangeException(nameof(playMode));
        if (!Enum.IsDefined(pricingMethod))
            throw new ArgumentOutOfRangeException(nameof(pricingMethod));
        if (price <= 0m)
            throw new ArgumentOutOfRangeException(nameof(price), "Configured price must be positive.");

        ConsoleGeneration = consoleGeneration;
        PlayMode = playMode;
        PricingMethod = pricingMethod;
        Price = price;
    }

    public ConsoleGeneration ConsoleGeneration { get; }

    public PlayMode PlayMode { get; }

    public PricingMethod PricingMethod { get; }

    public decimal Price { get; }
}
