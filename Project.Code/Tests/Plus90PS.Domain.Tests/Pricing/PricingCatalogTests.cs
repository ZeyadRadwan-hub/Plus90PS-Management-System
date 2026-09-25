using Plus90PS.Domain.Pricing;
using Plus90PS.Domain.Sessions;

namespace Plus90PS.Domain.Tests.Pricing;

public sealed class PricingCatalogTests
{
    [Fact]
    public void SingleEntryCanBeLookedUpAsExistingSnapshot()
    {
        var catalog = PricingCatalog.Create([Entry(ConsoleGeneration.PS4, PlayMode.Single, PricingMethod.Hourly, 35.25m)]);

        Assert.Single(catalog.Entries);
        Assert.True(catalog.TryGetSnapshot(ConsoleGeneration.PS4, PlayMode.Single, PricingMethod.Hourly, out var snapshot));
        var captured = Assert.IsType<SessionPricingSnapshot>(snapshot);
        Assert.Equal(ConsoleGeneration.PS4, captured.ConsoleGeneration);
        Assert.Equal(PlayMode.Single, captured.PlayMode);
        Assert.Equal(PricingMethod.Hourly, captured.PricingMethod);
        Assert.Equal(35.25m, captured.Price);
    }

    [Fact]
    public void AllEightIndependentCombinationsCanHaveDistinctPrices()
    {
        var prices = new (ConsoleGeneration Generation, PlayMode Mode, PricingMethod Method, decimal Expected)[]
        {
            (ConsoleGeneration.PS4, PlayMode.Single, PricingMethod.Hourly, 10m),
            (ConsoleGeneration.PS4, PlayMode.Multi, PricingMethod.Hourly, 20m),
            (ConsoleGeneration.PS5, PlayMode.Single, PricingMethod.Hourly, 30m),
            (ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Hourly, 40m),
            (ConsoleGeneration.PS4, PlayMode.Single, PricingMethod.Match, 50m),
            (ConsoleGeneration.PS4, PlayMode.Multi, PricingMethod.Match, 60m),
            (ConsoleGeneration.PS5, PlayMode.Single, PricingMethod.Match, 70m),
            (ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Match, 80m)
        };
        var catalog = PricingCatalog.Create(prices.Select(price =>
            Entry(price.Generation, price.Mode, price.Method, price.Expected)));

        Assert.Equal(8, catalog.Entries.Count);
        foreach (var price in prices)
        {
            var snapshot = catalog.GetRequiredSnapshot(price.Generation, price.Mode, price.Method);
            Assert.Equal(price.Expected, snapshot.Price);
        }
    }

    [Fact]
    public void PartialCatalogDoesNotInventMissingMatchPrice()
    {
        var catalog = PricingCatalog.Create([
            Entry(ConsoleGeneration.PS4, PlayMode.Single, PricingMethod.Hourly, 20m),
            Entry(ConsoleGeneration.PS4, PlayMode.Multi, PricingMethod.Hourly, 25m),
            Entry(ConsoleGeneration.PS5, PlayMode.Single, PricingMethod.Hourly, 30m)
        ]);

        Assert.Equal(3, catalog.Entries.Count);
        Assert.False(catalog.TryGetSnapshot(ConsoleGeneration.PS5, PlayMode.Single, PricingMethod.Match, out var missing));
        Assert.Null(missing);
    }

    [Fact]
    public void SameConsoleAndModeWithHourlyAndMatchAreNotDuplicates()
    {
        var catalog = PricingCatalog.Create([
            Entry(ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Hourly, 150m),
            Entry(ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Match, 90m)
        ]);

        Assert.Equal(150m, catalog.GetRequiredSnapshot(ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Hourly).Price);
        Assert.Equal(90m, catalog.GetRequiredSnapshot(ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Match).Price);
    }

    [Theory]
    [InlineData(150)]
    [InlineData(170)]
    public void DuplicateCombinationIsRejectedEvenWhenPriceDiffers(int secondPrice)
    {
        Assert.Throws<ArgumentException>(() => PricingCatalog.Create([
            Entry(ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Hourly, 150m),
            Entry(ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Hourly, secondPrice)
        ]));
    }

    [Theory]
    [InlineData(ConsoleGeneration.PS4, PlayMode.Multi, PricingMethod.Hourly)]
    [InlineData(ConsoleGeneration.PS5, PlayMode.Single, PricingMethod.Hourly)]
    [InlineData(ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Match)]
    public void LookupDoesNotSubstituteAnotherDimension(ConsoleGeneration generation, PlayMode mode, PricingMethod method)
    {
        var catalog = PricingCatalog.Create([
            Entry(ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Hourly, 150m)
        ]);

        Assert.False(catalog.TryGetSnapshot(generation, mode, method, out var snapshot));
        Assert.Null(snapshot);
        Assert.Throws<KeyNotFoundException>(() => catalog.GetRequiredSnapshot(generation, mode, method));
    }

    [Theory]
    [InlineData(99, 0, 0)]
    [InlineData(0, 99, 0)]
    [InlineData(0, 0, 99)]
    public void InvalidLookupDimensionIsRejectedRatherThanTreatedAsMissing(int generation, int mode, int method)
    {
        var catalog = PricingCatalog.Create([]);

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            catalog.TryGetSnapshot((ConsoleGeneration)generation, (PlayMode)mode, (PricingMethod)method, out _));
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            catalog.GetRequiredSnapshot((ConsoleGeneration)generation, (PlayMode)mode, (PricingMethod)method));
    }

    [Fact]
    public void ExistingSnapshotKeepsOldPriceWhenNewCatalogHasNewPrice()
    {
        var oldCatalog = PricingCatalog.Create([Entry(ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Hourly, 150m)]);
        var oldSnapshot = oldCatalog.GetRequiredSnapshot(ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Hourly);
        var newCatalog = PricingCatalog.Create([Entry(ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Hourly, 170m)]);

        Assert.Equal(150m, oldSnapshot.Price);
        Assert.Equal(170m, newCatalog.GetRequiredSnapshot(ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Hourly).Price);
        Assert.Equal(150m, oldSnapshot.Price);
    }

    [Fact]
    public void ChangingOriginalInputListDoesNotChangeCatalog()
    {
        var entries = new List<PricingEntry> { Entry(ConsoleGeneration.PS4, PlayMode.Single, PricingMethod.Hourly, 25m) };
        var catalog = PricingCatalog.Create(entries);
        entries.Clear();
        entries.Add(Entry(ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Match, 100m));

        Assert.Single(catalog.Entries);
        Assert.Equal(25m, catalog.GetRequiredSnapshot(ConsoleGeneration.PS4, PlayMode.Single, PricingMethod.Hourly).Price);
        Assert.False(catalog.TryGetSnapshot(ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Match, out _));
    }

    [Fact]
    public void ExposedEntriesCannotMutateCatalog()
    {
        var catalog = PricingCatalog.Create([Entry(ConsoleGeneration.PS4, PlayMode.Single, PricingMethod.Hourly, 25m)]);

        var writable = Assert.IsAssignableFrom<IList<PricingEntry>>(catalog.Entries);
        Assert.Throws<NotSupportedException>(() => writable[0] =
            Entry(ConsoleGeneration.PS4, PlayMode.Single, PricingMethod.Hourly, 99m));
        Assert.Throws<NotSupportedException>(() => writable.Add(
            Entry(ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Match, 100m)));

        Assert.Equal(25m, catalog.GetRequiredSnapshot(ConsoleGeneration.PS4, PlayMode.Single, PricingMethod.Hourly).Price);
        Assert.Single(catalog.Entries);
    }

    [Fact]
    public void NullInputAndNullEntryAreRejected()
    {
        Assert.Throws<ArgumentNullException>(() => PricingCatalog.Create(null!));
        Assert.Throws<ArgumentException>(() => PricingCatalog.Create([null!]));
    }

    private static PricingEntry Entry(ConsoleGeneration generation, PlayMode mode, PricingMethod method, decimal price) =>
        new(generation, mode, method, price);
}
