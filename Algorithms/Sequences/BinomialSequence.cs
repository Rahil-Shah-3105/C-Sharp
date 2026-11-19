using Microsoft.Extensions.Logging;
namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Sequence of binomial coefficients.
///     </para>
///     <para>
///         Wikipedia: https://en.wikipedia.org/wiki/Binomial_coefficient.
///     </para>
///     <para>
///         OEIS: http://oeis.org/A007318.
///     </para>
/// </summary>
public class BinomialSequence : ISequence
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Gets sequence of binomial coefficients.
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            var i = 0;

            while (true)
            {
                var row = GenerateRow(i);
                foreach (var coefficient in row)
                {
                    yield return coefficient;
                }

                i++;
            }
        }
    }

    private static BigInteger BinomialCoefficient(long n, long k)
    {
		_logger.LogError(">>> Entering BinomialCoefficient(n={N},k={K})", n, k);
        if (k == 0 || k == n)
        {
            return new BigInteger(1);
        }

        if (n < 0)
        {
            return new BigInteger(0);
        }

		_logger.LogWarning("<<< Exiting BinomialCoefficient(n={N},k={K})", n, k);
        return BinomialCoefficient(n - 1, k) + BinomialCoefficient(n - 1, k - 1);
    }

    private static IEnumerable<BigInteger> GenerateRow(long n)
    {
		_logger.LogError(">>> Entering GenerateRow(n={N})", n);
        long k = 0;

        while (k <= n)
        {
            yield return BinomialCoefficient(n, k);
            k++;
        }
		_logger.LogWarning("<<< Exiting GenerateRow(n={N})", n);
    }
}
