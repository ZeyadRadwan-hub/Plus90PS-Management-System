namespace Plus90PS.Domain.Billing;

public static class MoneyRoundingCalculator
{
    public static decimal Calculate(decimal amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amount);

        var wholeEgp = decimal.Truncate(amount);
        if (amount - wholeEgp >= 0.50m)
        {
            wholeEgp++;
        }

        var increaseToNextFive = 5m - wholeEgp % 5m;
        if (increaseToNextFive is 1m or 2m)
        {
            wholeEgp += increaseToNextFive;
        }

        return wholeEgp;
    }
}
