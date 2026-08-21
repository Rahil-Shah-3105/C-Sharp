using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

public static class FloorTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(0.0, 0)]
    [TestCase(1.1, 1)]
    [TestCase(1.9, 1)]
    [TestCase(1.0, 1)]
    [TestCase(-1.1, -2)]
    [TestCase(-1.9, -2)]
    [TestCase(-1.0, -1)]
    [TestCase(1000000000.1, 1000000000)]
    [TestCase(1, 1)]
    public static void GetsFloorVal<T>(T inputNum, T expected) where T : INumber<T>
    {
		_logger.LogDebug(">>> Entering GetsFloorVal(inputNum={InputNum},expected={Expected})", inputNum, expected);
        // Act
        var result = Floor.FloorVal(inputNum);
		_logger.LogDebug("GetsFloorVal(inputNum={InputNum},expected={Expected}): result → {Result}", inputNum, expected, result);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
		_logger.LogDebug("<<< Exiting GetsFloorVal(inputNum={InputNum},expected={Expected})", inputNum, expected);
    }
}