using Microsoft.Extensions.Logging;
namespace Algorithms.Numeric.GreatestCommonDivisor;

/// <summary>
///     Euclidean algorithm for finding the greatest common divisor.
/// </summary>
public class EuclideanGreatestCommonDivisorFinder : IGreatestCommonDivisorFinder
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Finds greatest common divisor for numbers a and b
    ///     using euclidean algorithm.
    /// </summary>
    /// <param name="a">First number.</param>
    /// <param name="b">Second number.</param>
    /// <returns>Greatest common divisor.</returns>
    public int FindGcd(int a, int b)
    {
		_logger.LogError(">>> Entering FindGcd(a={A},b={B})", a, b);
        if (a == 0 && b == 0)
        {
            return int.MaxValue;
        }

        if (a == 0 || b == 0)
        {
            return a + b;
        }

        var aa = a;
		_logger.LogError("FindGcd(a={A},b={B}): aa → {Aa}", a, b, aa);
        var bb = b;
		_logger.LogError("FindGcd(a={A},b={B}): bb → {Bb}", a, b, bb);
        var cc = aa % bb;
		_logger.LogError("FindGcd(a={A},b={B}): cc → {Cc}", a, b, cc);

        while (cc != 0)
        {
            aa = bb;
            bb = cc;
            cc = aa % bb;
        }

		_logger.LogWarning("<<< Exiting FindGcd(a={A},b={B})", a, b);
        return bb;
    }
}
