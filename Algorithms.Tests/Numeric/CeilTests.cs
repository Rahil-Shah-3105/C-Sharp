using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

public static class CeilTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(0.0, 0)]
    [TestCase(1.1, 2)]
    [TestCase(1.9, 2)]
    [TestCase(1.0, 1)]
    [TestCase(-1.1, -1)]
    [TestCase(-1.9, -1)]
    [TestCase(-1.0, -1)]
    [TestCase(1000000000.1, 1000000001)]
    [TestCase(1, 1)]
    public static void GetsCeilVal<T>(T inputNum, T expected) where T : INumber<T>
    {
		_logger.LogError(">>> Entering GetsCeilVal(inputNum={InputNum},expected={Expected})", inputNum, expected);
        // Act
        var result = Ceil.CeilVal(inputNum);
		_logger.LogError("GetsCeilVal(inputNum={InputNum},expected={Expected}): result → {Result}", inputNum, expected, result);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
		_logger.LogWarning("<<< Exiting GetsCeilVal(inputNum={InputNum},expected={Expected})", inputNum, expected);
    }
}