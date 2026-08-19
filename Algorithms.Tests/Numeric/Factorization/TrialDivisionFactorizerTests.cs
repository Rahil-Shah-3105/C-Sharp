using Algorithms.Numeric.Factorization;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric.Factorization;

public static class TrialDivisionFactorizerTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(29)]
    [TestCase(31)]
    public static void PrimeNumberFactorizationFails(int p)
    {
		_logger.LogDebug(">>> Entering PrimeNumberFactorizationFails(p={P})", p);
        // Arrange
        var factorizer = new TrialDivisionFactorizer();
		_logger.LogDebug("PrimeNumberFactorizationFails(p={P}): factorizer → {Factorizer}", p, factorizer);

        // Act
        var success = factorizer.TryFactor(p, out _);
		_logger.LogDebug("PrimeNumberFactorizationFails(p={P}): success → {Success}", p, success);

        // Assert
        Assert.That(success, Is.False);
		_logger.LogDebug("<<< Exiting PrimeNumberFactorizationFails(p={P})", p);
    }

    [TestCase(4, 2)]
    [TestCase(6, 2)]
    [TestCase(8, 2)]
    [TestCase(9, 3)]
    [TestCase(15, 3)]
    [TestCase(35, 5)]
    [TestCase(49, 7)]
    [TestCase(77, 7)]
    public static void PrimeNumberFactorizationSucceeds(int n, int expected)
    {
		_logger.LogDebug(">>> Entering PrimeNumberFactorizationSucceeds(n={N},expected={Expected})", n, expected);
        // Arrange
        var factorizer = new TrialDivisionFactorizer();
		_logger.LogDebug("PrimeNumberFactorizationSucceeds(n={N},expected={Expected}): factorizer → {Factorizer}", n, expected, factorizer);

        // Act
        var success = factorizer.TryFactor(n, out var factor);
		_logger.LogDebug("PrimeNumberFactorizationSucceeds(n={N},expected={Expected}): success → {Success}", n, expected, success);

        // Assert
        Assert.That(success, Is.True);
        Assert.That(factor, Is.EqualTo(expected));
		_logger.LogDebug("<<< Exiting PrimeNumberFactorizationSucceeds(n={N},expected={Expected})", n, expected);
    }
}
