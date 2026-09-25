using Plus90PS.Domain.Pricing;
using Plus90PS.Domain.Sessions;

namespace Plus90PS.Domain.Tests.Pricing;

public sealed class PricingEntryTests
{
    [Theory]
    [InlineData(ConsoleGeneration.PS4, PlayMode.Single, PricingMethod.Hourly)]
    [InlineData(ConsoleGeneration.PS4, PlayMode.Multi, PricingMethod.Hourly)]
    [InlineData(ConsoleGeneration.PS5, PlayMode.Single, PricingMethod.Match)]
    [InlineData(ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Match)]
    public void CapturesSupportedCombination(ConsoleGeneration generation, PlayMode mode, PricingMethod method)
    {
        var entry = new PricingEntry(generation, mode, method, 150.75m);

        Assert.Equal(generation, entry.ConsoleGeneration);
        Assert.Equal(mode, entry.PlayMode);
        Assert.Equal(method, entry.PricingMethod);
        Assert.Equal(150.75m, entry.Price);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void RejectsNonPositivePrice(int price)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new PricingEntry(ConsoleGeneration.PS4, PlayMode.Single, PricingMethod.Hourly, price));
    }

    [Fact]
    public void RejectsUnsupportedEnumValues()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new PricingEntry((ConsoleGeneration)99, PlayMode.Single, PricingMethod.Hourly, 50m));
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new PricingEntry(ConsoleGeneration.PS4, (PlayMode)99, PricingMethod.Hourly, 50m));
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new PricingEntry(ConsoleGeneration.PS4, PlayMode.Single, (PricingMethod)99, 50m));
    }

    [Fact]
    public void HasNoPublicSetters()
    {
        Assert.All(typeof(PricingEntry).GetProperties(), property => Assert.Null(property.SetMethod));
    }
}
