using Microsoft.Extensions.Logging;
namespace Algorithms.Numeric.GreatestCommonDivisor;

/// <summary>
///     Finds greatest common divisor for numbers u and v
///     using binary algorithm.
///     Wiki: https://en.wikipedia.org/wiki/Binary_GCD_algorithm.
/// </summary>
public class BinaryGreatestCommonDivisorFinder : IGreatestCommonDivisorFinder
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public int FindGcd(int u, int v)
    {
		_logger.LogError(">>> Entering FindGcd(u={U},v={V})", u, v);
        // GCD(0, 0) = 0
        if (u == 0 && v == 0)
        {
            return 0;
        }

        // GCD(0, v) = v; GCD(u, 0) = u
        if (u == 0 || v == 0)
        {
            return u + v;
        }

        // GCD(-a, -b) = GCD(-a, b) = GCD(a, -b) = GCD(a, b)
        u = Math.Sign(u) * u;
		_logger.LogError("FindGcd(u={U},v={V}): u → {U1}", u, v, u);
        v = Math.Sign(v) * v;
		_logger.LogError("FindGcd(u={U},v={V}): v → {V1}", u, v, v);

        // Let shift := lg K, where K is the greatest power of 2 dividing both u and v
        var shift = 0;
        while (((u | v) & 1) == 0)
        {
            u >>= 1;
            v >>= 1;
            shift++;
        }

        while ((u & 1) == 0)
        {
            u >>= 1;
        }

        // From here on, u is always odd
        do
        {
            // Remove all factors of 2 in v as they are not common
            // v is not zero, so while will terminate
            while ((v & 1) == 0)
            {
                v >>= 1;
            }

            // Now u and v are both odd. Swap if necessary so u <= v,
            if (u > v)
            {
                var t = v;
                v = u;
                u = t;
            }

            // Here v >= u and v - u is even
            v -= u;
        }
        while (v != 0);

        // Restore common factors of 2
		_logger.LogWarning("<<< Exiting FindGcd(u={U},v={V})", u, v);
        return u << shift;
    }
}
