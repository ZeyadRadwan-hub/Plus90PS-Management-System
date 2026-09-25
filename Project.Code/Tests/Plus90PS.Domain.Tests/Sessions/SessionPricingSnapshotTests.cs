using Plus90PS.Domain.Sessions;

namespace Plus90PS.Domain.Tests.Sessions;

public sealed class SessionPricingSnapshotTests
{
    [Theory]
    [InlineData(ConsoleGeneration.PS4, PlayMode.Single, PricingMethod.Hourly, "25.75")]
    [InlineData(ConsoleGeneration.PS5, PlayMode.Multi, PricingMethod.Match, "90.50")]
    public void CapturesValidPricingContext(
        ConsoleGeneration generation, PlayMode mode, PricingMethod method, string priceText)
    {
        var price = decimal.Parse(priceText, System.Globalization.CultureInfo.InvariantCulture);

        var snapshot = new SessionPricingSnapshot(generation, mode, method, price);

        Assert.Equal(generation, snapshot.ConsoleGeneration);
        Assert.Equal(mode, snapshot.PlayMode);
        Assert.Equal(method, snapshot.PricingMethod);
        Assert.Equal(price, snapshot.Price);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-0.01")]
    public void RejectsNonPositivePrice(string priceText)
    {
        var price = decimal.Parse(priceText, System.Globalization.CultureInfo.InvariantCulture);

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new SessionPricingSnapshot(ConsoleGeneration.PS4, PlayMode.Single, PricingMethod.Hourly, price));
    }

    [Fact]
    public void RejectsUnsupportedEnumValues()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new SessionPricingSnapshot((ConsoleGeneration)99, PlayMode.Single, PricingMethod.Hourly, 25m));
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new SessionPricingSnapshot(ConsoleGeneration.PS4, (PlayMode)99, PricingMethod.Hourly, 25m));
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new SessionPricingSnapshot(ConsoleGeneration.PS4, PlayMode.Single, (PricingMethod)99, 25m));
    }
}
