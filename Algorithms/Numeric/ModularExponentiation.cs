using Microsoft.Extensions.Logging;
namespace Algorithms.Numeric;

/// <summary>
///     Modular exponentiation is a type of exponentiation performed over a modulus
///     Modular exponentiation c is: c = b^e mod m where b is base, e is exponent, m is modulus
///     (Wiki: https://en.wikipedia.org/wiki/Modular_exponentiation).
/// </summary>
public class ModularExponentiation
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Performs Modular Exponentiation on b, e, m.
    /// </summary>
    /// <param name="b">Base.</param>
    /// <param name="e">Exponent.</param>
    /// <param name="m">Modulus.</param>
    /// <returns>Modular Exponential.</returns>
    public int ModularPow(int b, int e, int m)
    {
		_logger.LogError(">>> Entering ModularPow(b={B},e={E},m={M})", b, e, m);
        // initialize result in variable res
        int res = 1;
		_logger.LogError("ModularPow(b={B},e={E},m={M}): res → {Res}", b, e, m, res);
        if (m == 1)
        {
            // 1 divides every number
            return 0;
        }

        if (m <= 0)
        {
            // exponential not defined in this case
			_logger.LogError("Throwing ArgumentException from ModularPow(b={B},e={E},m={M})", b, e, m);
            throw new ArgumentException(string.Format("{0} is not a positive integer", m));
        }

        for (int i = 0; i < e; i++)
        {
            res = (res * b) % m;
        }

		_logger.LogWarning("<<< Exiting ModularPow(b={B},e={E},m={M})", b, e, m);
        return res;
    }
}
