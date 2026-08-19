using Algorithms.ModularArithmetic;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.ModularArithmetic;

public static class ExtendedEuclideanAlgorithmTest
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(240, 46, 2, -9, 47)]
    [TestCase(46, 240, 2, 47, -9)]
    [TestCase(2, 3, 1, -1, 1)]
    [TestCase(1, 1, 1, 0, 1)]
    [TestCase(13, 17, 1, 4, -3)]
    [TestCase(0, 17, 17, 0, 1)]
    [TestCase(17, 0, 17, 1, 0)]
    [TestCase(17, 17, 17, 0, 1)]
    [TestCase(2 * 17, 17, 17, 0, 1)]
    [TestCase(0, 0, 0, 1, 0)]
    [TestCase(2 * 13 * 17, 4 * 9 * 13, 2 * 13, -1, 1)]
    public static void TestCompute(long a, long b, long expectedGCD, long expectedBezoutOfA, long expectedBezoutOfB)
    {
		_logger.LogDebug(">>> Entering TestCompute(a={A},b={B},expectedGCD={ExpectedGCD},expectedBezoutOfA={ExpectedBezoutOfA},expectedBezoutOfB={ExpectedBezoutOfB})", a, b, expectedGCD, expectedBezoutOfA, expectedBezoutOfB);
        // Act
        var eeaResult = ExtendedEuclideanAlgorithm.Compute(a, b);
		_logger.LogDebug("TestCompute(a={A},b={B},expectedGCD={ExpectedGCD},expectedBezoutOfA={ExpectedBezoutOfA},expectedBezoutOfB={ExpectedBezoutOfB}): eeaResult → {EeaResult}", a, b, expectedGCD, expectedBezoutOfA, expectedBezoutOfB, eeaResult);

        // Assert
        Assert.That(eeaResult.Gcd, Is.EqualTo(expectedGCD));
        Assert.That(eeaResult.BezoutA, Is.EqualTo(expectedBezoutOfA));
        Assert.That(eeaResult.BezoutB, Is.EqualTo(expectedBezoutOfB));
		_logger.LogDebug("<<< Exiting TestCompute(a={A},b={B},expectedGCD={ExpectedGCD},expectedBezoutOfA={ExpectedBezoutOfA},expectedBezoutOfB={ExpectedBezoutOfB})", a, b, expectedGCD, expectedBezoutOfA, expectedBezoutOfB);
    }

    [TestCase(240, 46, 2, -9, 47)]
    [TestCase(46, 240, 2, 47, -9)]
    [TestCase(2, 3, 1, -1, 1)]
    [TestCase(1, 1, 1, 0, 1)]
    [TestCase(13, 17, 1, 4, -3)]
    [TestCase(0, 17, 17, 0, 1)]
    [TestCase(17, 0, 17, 1, 0)]
    [TestCase(17, 17, 17, 0, 1)]
    [TestCase(2 * 17, 17, 17, 0, 1)]
    [TestCase(0, 0, 0, 1, 0)]
    [TestCase(2 * 13 * 17, 4 * 9 * 13, 2 * 13, -1, 1)]
    public static void TestCompute_BigInteger(long a, long b, long expectedGCD, long expectedBezoutOfA, long expectedBezoutOfB)
    {
		_logger.LogDebug(">>> Entering TestCompute_BigInteger(a={A},b={B},expectedGCD={ExpectedGCD},expectedBezoutOfA={ExpectedBezoutOfA},expectedBezoutOfB={ExpectedBezoutOfB})", a, b, expectedGCD, expectedBezoutOfA, expectedBezoutOfB);
        // Act
        var eeaResult = ExtendedEuclideanAlgorithm.Compute(new BigInteger(a), new BigInteger(b));
		_logger.LogDebug("TestCompute_BigInteger(a={A},b={B},expectedGCD={ExpectedGCD},expectedBezoutOfA={ExpectedBezoutOfA},expectedBezoutOfB={ExpectedBezoutOfB}): eeaResult → {EeaResult}", a, b, expectedGCD, expectedBezoutOfA, expectedBezoutOfB, eeaResult);

        // Assert
        Assert.That(eeaResult.Gcd, Is.EqualTo(new BigInteger(expectedGCD)));
        Assert.That(eeaResult.BezoutA, Is.EqualTo(new BigInteger(expectedBezoutOfA)));
        Assert.That(eeaResult.BezoutB, Is.EqualTo(new BigInteger(expectedBezoutOfB)));
		_logger.LogDebug("<<< Exiting TestCompute_BigInteger(a={A},b={B},expectedGCD={ExpectedGCD},expectedBezoutOfA={ExpectedBezoutOfA},expectedBezoutOfB={ExpectedBezoutOfB})", a, b, expectedGCD, expectedBezoutOfA, expectedBezoutOfB);
    }
}
