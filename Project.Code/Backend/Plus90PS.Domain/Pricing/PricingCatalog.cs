using Plus90PS.Domain.Sessions;

namespace Plus90PS.Domain.Pricing;

public sealed class PricingCatalog
{
    private readonly Dictionary<(ConsoleGeneration, PlayMode, PricingMethod), PricingEntry> _byCombination;

    private PricingCatalog(
        Dictionary<(ConsoleGeneration, PlayMode, PricingMethod), PricingEntry> byCombination,
        IReadOnlyCollection<PricingEntry> entries)
    {
        _byCombination = byCombination;
        Entries = entries;
    }

    public IReadOnlyCollection<PricingEntry> Entries { get; }

    public static PricingCatalog Create(IEnumerable<PricingEntry> entries)
    {
        ArgumentNullException.ThrowIfNull(entries);

        var byCombination = new Dictionary<(ConsoleGeneration, PlayMode, PricingMethod), PricingEntry>();
        var copiedEntries = new List<PricingEntry>();

        foreach (var entry in entries)
        {
            if (entry is null)
                throw new ArgumentException("Pricing entries cannot contain null.", nameof(entries));

            var key = (entry.ConsoleGeneration, entry.PlayMode, entry.PricingMethod);
            if (!byCombination.TryAdd(key, entry))
                throw new ArgumentException("Duplicate pricing combination.", nameof(entries));

            copiedEntries.Add(entry);
        }

        return new PricingCatalog(byCombination, Array.AsReadOnly(copiedEntries.ToArray()));
    }

    public bool TryGetSnapshot(
        ConsoleGeneration consoleGeneration,
        PlayMode playMode,
        PricingMethod pricingMethod,
        out SessionPricingSnapshot? snapshot)
    {
        ValidateDimensions(consoleGeneration, playMode, pricingMethod);

        if (_byCombination.TryGetValue((consoleGeneration, playMode, pricingMethod), out var entry))
        {
            snapshot = new SessionPricingSnapshot(consoleGeneration, playMode, pricingMethod, entry.Price);
            return true;
        }

        snapshot = null;
        return false;
    }

    public SessionPricingSnapshot GetRequiredSnapshot(
        ConsoleGeneration consoleGeneration,
        PlayMode playMode,
        PricingMethod pricingMethod)
    {
        if (TryGetSnapshot(consoleGeneration, playMode, pricingMethod, out var snapshot))
            return snapshot!;

        throw new KeyNotFoundException(
            $"No price is configured for {consoleGeneration} + {playMode} + {pricingMethod}.");
    }

    private static void ValidateDimensions(
        ConsoleGeneration consoleGeneration,
        PlayMode playMode,
        PricingMethod pricingMethod)
    {
        if (!Enum.IsDefined(consoleGeneration))
            throw new ArgumentOutOfRangeException(nameof(consoleGeneration));
        if (!Enum.IsDefined(playMode))
            throw new ArgumentOutOfRangeException(nameof(playMode));
        if (!Enum.IsDefined(pricingMethod))
            throw new ArgumentOutOfRangeException(nameof(pricingMethod));
    }
}
