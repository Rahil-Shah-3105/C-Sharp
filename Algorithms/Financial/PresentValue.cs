using Microsoft.Extensions.Logging;
namespace Algorithms.Financial;

/// <summary>
/// PresentValue is the value of an expected income stream determined as of the date of valuation.
/// </summary>
public static class PresentValue
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public static double Calculate(double discountRate, List<double> cashFlows)
    {
		_logger.LogDebug(">>> Entering Calculate(discountRate={DiscountRate},cashFlows={CashFlows})", discountRate, cashFlows);
        if (discountRate < 0)
        {
			_logger.LogWarning("Throwing ArgumentException from Calculate(discountRate={DiscountRate},cashFlows={CashFlows})", discountRate, cashFlows);
            throw new ArgumentException("Discount rate cannot be negative");
        }

        if (cashFlows.Count == 0)
        {
            throw new ArgumentException("Cash flows list cannot be empty");
        }

        double presentValue = cashFlows.Select((t, i) => t / Math.Pow(1 + discountRate, i)).Sum();
		_logger.LogDebug("Calculate(discountRate={DiscountRate},cashFlows={CashFlows}): presentValue → {PresentValue}", discountRate, cashFlows, presentValue);

		_logger.LogDebug("<<< Exiting Calculate(discountRate={DiscountRate},cashFlows={CashFlows})", discountRate, cashFlows);
        return Math.Round(presentValue, 2);
    }
}
