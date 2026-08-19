using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

public static class AdditionWithoutArithmeticTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(3, 5, 8)]
    [TestCase(13, 5, 18)]
    [TestCase(-7, 2, -5)]
    [TestCase(0, -7, -7)]
    [TestCase(-321, 0, -321)]
    public static void CalculateAdditionWithoutArithmetic_Test(int first, int second, int expectedResult)
    {
		_logger.LogDebug(">>> Entering CalculateAdditionWithoutArithmetic_Test(first={First},second={Second},expectedResult={ExpectedResult})", first, second, expectedResult);
        // Act
        var result = AdditionWithoutArithmetic.CalculateAdditionWithoutArithmetic(first, second);
		_logger.LogDebug("CalculateAdditionWithoutArithmetic_Test(first={First},second={Second},expectedResult={ExpectedResult}): result → {Result}", first, second, expectedResult, result);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
		_logger.LogDebug("<<< Exiting CalculateAdditionWithoutArithmetic_Test(first={First},second={Second},expectedResult={ExpectedResult})", first, second, expectedResult);
    }
}
