using System.Globalization;
using Plus90PS.Domain.Billing;

namespace Plus90PS.Domain.Tests.Billing;

public sealed class MoneyRoundingCalculatorTests
{
    [Theory]
    [InlineData("0.00", "0")]
    [InlineData("100.00", "100")]
    [InlineData("100.01", "100")]
    [InlineData("100.20", "100")]
    [InlineData("100.49", "100")]
    [InlineData("100.50", "101")]
    [InlineData("100.99", "101")]
    [InlineData("100.499", "100")]
    [InlineData("100.500", "101")]
    public void AppliesHalfEgpThresholdBeforeFiveRule(string amountText, string expectedText)
    {
        Assert.Equal(Parse(expectedText), MoneyRoundingCalculator.Calculate(Parse(amountText)));
    }

    [Theory]
    [InlineData("100", "100")]
    [InlineData("101", "101")]
    [InlineData("102", "102")]
    [InlineData("103", "105")]
    [InlineData("104", "105")]
    [InlineData("105", "105")]
    [InlineData("106", "106")]
    [InlineData("107", "107")]
    [InlineData("108", "110")]
    [InlineData("109", "110")]
    [InlineData("110", "110")]
    public void MovesToNextFiveOnlyWhenOneOrTwoEgpAway(string amountText, string expectedText)
    {
        Assert.Equal(Parse(expectedText), MoneyRoundingCalculator.Calculate(Parse(amountText)));
    }

    [Theory]
    [InlineData("103.49", "105")]
    [InlineData("102.50", "105")]
    [InlineData("107.49", "107")]
    [InlineData("107.50", "110")]
    public void CombinesBothStagesInOrder(string amountText, string expectedText)
    {
        Assert.Equal(Parse(expectedText), MoneyRoundingCalculator.Calculate(Parse(amountText)));
    }

    [Fact]
    public void RejectsNegativeAmount()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => MoneyRoundingCalculator.Calculate(-0.01m));
    }

    [Fact]
    public void KeepsLargeDecimalAmountExact()
    {
        Assert.Equal(1234567890123456789012346m,
            MoneyRoundingCalculator.Calculate(1234567890123456789012345.50m));
    }

    private static decimal Parse(string value) => decimal.Parse(value, CultureInfo.InvariantCulture);
}
