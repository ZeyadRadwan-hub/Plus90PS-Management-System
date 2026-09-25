namespace Plus90PS.Domain.Billing;

public static class BillableMinutesCalculator
{
    public static int Calculate(TimeSpan elapsed, TimeSpan paused)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(elapsed, TimeSpan.Zero);
        ArgumentOutOfRangeException.ThrowIfLessThan(paused, TimeSpan.Zero);
        if (paused > elapsed)
        {
            throw new ArgumentException("Paused duration cannot exceed elapsed duration.", nameof(paused));
        }

        var activeTicks = (elapsed - paused).Ticks;
        var wholeMinutes = activeTicks / TimeSpan.TicksPerMinute;
        if (activeTicks % TimeSpan.TicksPerMinute != 0)
        {
            wholeMinutes++;
        }

        var minutesIntoQuarter = wholeMinutes % 15;
        if (minutesIntoQuarter >= 12)
        {
            wholeMinutes += 15 - minutesIntoQuarter;
        }

        return checked((int)wholeMinutes);
    }
}
