using Microsoft.Extensions.Logging;
namespace Algorithms.Numeric;

/// <summary>
/// A Krishnamurthy number is a number whose sum of the factorial of digits
/// is equal to the number itself.
///
/// For example, 145 is a Krishnamurthy number since: 1! + 4! + 5! = 1 + 24 + 120 = 145.
/// </summary>
public static class KrishnamurthyNumberChecker
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// Check if a number is Krishnamurthy number or not.
    /// </summary>
    /// <param name="n">The number to check.</param>
    /// <returns>True if the number is Krishnamurthy, false otherwise.</returns>
    public static bool IsKMurthyNumber(int n)
    {
		_logger.LogError(">>> Entering IsKMurthyNumber(n={N})", n);
        int sumOfFactorials = 0;
        int tmp = n;

        if (n <= 0)
        {
            return false;
        }

        while (n != 0)
        {
            int factorial = (int)Factorial.Calculate(n % 10);
			_logger.LogError("IsKMurthyNumber(n={N}): factorial → {Factorial}", n, factorial);
            sumOfFactorials += factorial;
            n = n / 10;
        }

		_logger.LogWarning("<<< Exiting IsKMurthyNumber(n={N})", n);
        return tmp == sumOfFactorials;
    }
}
