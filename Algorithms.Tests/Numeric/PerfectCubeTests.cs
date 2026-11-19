using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

public static class PerfectCubeTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(-27, ExpectedResult = true)]
    [TestCase(27, ExpectedResult = true)]
    [TestCase(4, ExpectedResult = false)]
    [TestCase(64, ExpectedResult = true)]
    [TestCase(0, ExpectedResult = true)]
    [TestCase(1, ExpectedResult = true)]
    [TestCase(8, ExpectedResult = true)]
    [TestCase(9, ExpectedResult = false)]
    public static bool IsPerfectCube_ResultIsCorrect(int number)
    {
		_logger.LogError(">>> Entering IsPerfectCube_ResultIsCorrect(number={Number})", number);
        // Act
        var result = PerfectCubeChecker.IsPerfectCube(number);
		_logger.LogError("IsPerfectCube_ResultIsCorrect(number={Number}): result → {Result}", number, result);

        // Assert
		_logger.LogWarning("<<< Exiting IsPerfectCube_ResultIsCorrect(number={Number})", number);
        return result;
    }

    [TestCase(-27, ExpectedResult = true)]
    [TestCase(27, ExpectedResult = true)]
    [TestCase(4, ExpectedResult = false)]
    [TestCase(64, ExpectedResult = true)]
    [TestCase(0, ExpectedResult = true)]
    [TestCase(1, ExpectedResult = true)]
    [TestCase(8, ExpectedResult = true)]
    [TestCase(9, ExpectedResult = false)]
    public static bool IsPerfectCubeBinarySearch_ResultIsCorrect(int number)
    {
		_logger.LogError(">>> Entering IsPerfectCubeBinarySearch_ResultIsCorrect(number={Number})", number);
        // Act
        var result = PerfectCubeChecker.IsPerfectCubeBinarySearch(number);
		_logger.LogError("IsPerfectCubeBinarySearch_ResultIsCorrect(number={Number}): result → {Result}", number, result);

        // Assert
		_logger.LogWarning("<<< Exiting IsPerfectCubeBinarySearch_ResultIsCorrect(number={Number})", number);
        return result;
    }
}
