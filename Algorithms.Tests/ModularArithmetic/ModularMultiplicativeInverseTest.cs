using Algorithms.ModularArithmetic;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.ModularArithmetic;

public static class ModularMultiplicativeInverseTest
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(2, 3, 2)]
    [TestCase(1, 1, 0)]
    [TestCase(13, 17, 4)]
    public static void TestCompute(long a, long n, long expected)
    {
		_logger.LogDebug(">>> Entering TestCompute(a={A},n={N},expected={Expected})", a, n, expected);
        // Act
        var inverse = ModularMultiplicativeInverse.Compute(a, n);
		_logger.LogDebug("TestCompute(a={A},n={N},expected={Expected}): inverse → {Inverse}", a, n, expected, inverse);

        // Assert
        Assert.That(inverse, Is.EqualTo(expected));
		_logger.LogDebug("<<< Exiting TestCompute(a={A},n={N},expected={Expected})", a, n, expected);
    }

    [TestCase(46, 240)]
    [TestCase(0, 17)]
    [TestCase(17, 0)]
    [TestCase(17, 17)]
    [TestCase(0, 0)]
    [TestCase(2 * 13 * 17, 4 * 9 * 13)]
    public static void TestCompute_Irrevertible(long a, long n)
    {
		_logger.LogDebug(">>> Entering TestCompute_Irrevertible(a={A},n={N})", a, n);
        // Act
        void Act() => ModularMultiplicativeInverse.Compute(a, n);

        // Assert
		_logger.LogDebug("<<< Exiting TestCompute_Irrevertible(a={A},n={N})", a, n);
        _ = Assert.Throws<ArithmeticException>(Act);
		_logger.LogDebug("TestCompute_Irrevertible(a={A},n={N}): _ → {_}", a, n, _);
    }

    [TestCase(2, 3, 2)]
    [TestCase(1, 1, 0)]
    [TestCase(13, 17, 4)]
    public static void TestCompute_BigInteger(long a, long n, long expected)
    {
		_logger.LogDebug(">>> Entering TestCompute_BigInteger(a={A},n={N},expected={Expected})", a, n, expected);
        // Act
        var inverse = ModularMultiplicativeInverse.Compute(new BigInteger(a), new BigInteger(n));
		_logger.LogDebug("TestCompute_BigInteger(a={A},n={N},expected={Expected}): inverse → {Inverse}", a, n, expected, inverse);

        // Assert
        Assert.That(inverse, Is.EqualTo(new BigInteger(expected)));
		_logger.LogDebug("<<< Exiting TestCompute_BigInteger(a={A},n={N},expected={Expected})", a, n, expected);
    }

    [TestCase(46, 240)]
    [TestCase(0, 17)]
    [TestCase(17, 0)]
    [TestCase(17, 17)]
    [TestCase(0, 0)]
    [TestCase(2 * 13 * 17, 4 * 9 * 13)]
    public static void TestCompute_BigInteger_Irrevertible(long a, long n)
    {
		_logger.LogDebug(">>> Entering TestCompute_BigInteger_Irrevertible(a={A},n={N})", a, n);
        // Act
        void Act() => ModularMultiplicativeInverse.Compute(new BigInteger(a), new BigInteger(n));

        // Assert
		_logger.LogDebug("<<< Exiting TestCompute_BigInteger_Irrevertible(a={A},n={N})", a, n);
        _ = Assert.Throws<ArithmeticException>(Act);
		_logger.LogDebug("TestCompute_BigInteger_Irrevertible(a={A},n={N}): _ → {_}", a, n, _);
    }
}
