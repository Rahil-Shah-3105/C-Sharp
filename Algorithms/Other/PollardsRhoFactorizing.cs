using Algorithms.Numeric.GreatestCommonDivisor;
using Microsoft.Extensions.Logging;

namespace Algorithms.Other;

/// <summary>Implementation of the Pollard's rho algorithm.
/// Algorithm for integer factorization.
/// Wiki: https://en.wikipedia.org/wiki/Pollard's_rho_algorithm.
/// </summary>
public static class PollardsRhoFactorizing
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public static int Calculate(int number)
    {
		_logger.LogDebug(">>> Entering Calculate(number={Number})", number);
        var x = 2;
        var y = 2;
		_logger.LogDebug("Calculate(number={Number}): y → {Y}", number, y);
        var d = 1;
		_logger.LogDebug("Calculate(number={Number}): d → {D}", number, d);
		_logger.LogDebug("Calculate(number={Number}): x → {X}", number, x);
        var p = number;
        var i = 0;
        var gcd = new BinaryGreatestCommonDivisorFinder();
		_logger.LogDebug("Calculate(number={Number}): gcd → {Gcd}", number, gcd);

        while (d == 1)
        {
            x = Fun_g(x, p);
            y = Fun_g(Fun_g(y, p), p);
            d = gcd.FindGcd(Math.Abs(x - y), p);
            i++;
        }

		_logger.LogDebug("<<< Exiting Calculate(number={Number})", number);
        return d;
    }

    private static int Fun_g(int x, int p)
    {
		_logger.LogDebug(">>> Entering Fun_g(x={X},p={P})", x, p);
        return (x * x + 1) % p;
    }
}
