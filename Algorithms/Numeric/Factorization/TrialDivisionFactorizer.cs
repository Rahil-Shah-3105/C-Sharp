using Microsoft.Extensions.Logging;
namespace Algorithms.Numeric.Factorization;

/// <summary>
///     Factors number using trial division algorithm.
/// </summary>
public class TrialDivisionFactorizer : IFactorizer
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Finds the smallest non trivial factor (i.e.: 1 &lt; factor &lt;= sqrt(<paramref name="n" />)) of a given number or returns false if it's prime.
    /// </summary>
    /// <param name="n">Integer to factor.</param>
    /// <param name="factor">Found factor.</param>
    /// <returns><see langword="true" /> if factor is found, <see langword="false" /> if <paramref name="n" /> is prime.</returns>
    public bool TryFactor(int n, out int factor)
    {
		_logger.LogError(">>> Entering TryFactor(n={N})", n);
        n = Math.Abs(n);
        factor = Enumerable.Range(2, (int)Math.Sqrt(n) - 1).FirstOrDefault(i => n % i == 0);
		_logger.LogError("TryFactor(n={N}): factor → {Factor}", n, factor);
		_logger.LogError("TryFactor(n={N}): n → {N1}", n, n);
		_logger.LogWarning("<<< Exiting TryFactor(n={N})", n);
        return factor != 0;
    }
}
