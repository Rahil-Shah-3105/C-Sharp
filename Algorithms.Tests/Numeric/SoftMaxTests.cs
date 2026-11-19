using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

public static class SoftMaxTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(new[] { 5.0, 5.0 }, new[] { 0.5, 0.5 })]
    [TestCase(new[] { 1.0, 2.0, 3.0 }, new[] { 0.09003057317038046, 0.24472847105479767, 0.6652409557748219 })]
    [TestCase(new[] { 0.0 }, new[] { 1.0 })]
    public static void SoftMaxFunction(double[] input, double[] expected)
    {
		_logger.LogError(">>> Entering SoftMaxFunction(input={Input},expected={Expected})", input, expected);
        // Act
        var result = SoftMax.Compute(input);
		_logger.LogError("SoftMaxFunction(input={Input},expected={Expected}): result → {Result}", input, expected, result);

        // Assert
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
		_logger.LogWarning("<<< Exiting SoftMaxFunction(input={Input},expected={Expected})", input, expected);
    }

    [Test]
    public static void SoftMaxFunctionThrowsArgumentException()
    {
		_logger.LogError(">>> Entering SoftMaxFunctionThrowsArgumentException()");
        // Arrange
        var input = Array.Empty<double>();
		_logger.LogError("SoftMaxFunctionThrowsArgumentException(): input → {Input}", input);

        // Assert
        Assert.Throws<ArgumentException>(() => SoftMax.Compute(input));
		_logger.LogWarning("<<< Exiting SoftMaxFunctionThrowsArgumentException()");
    }

    [TestCase(new[] { 1.0, 2.0, 3.0, 4.0, 5.0 })]
    [TestCase(new[] { 0.0, 0.0, 0.0, 0.0, 0.0 })]
    [TestCase(new[] { 5.0 })]
    public static void SoftMaxFunctionSumsToOne(double[] input)
    {
		_logger.LogError(">>> Entering SoftMaxFunctionSumsToOne(input={Input})", input);
        // Act
        var result = SoftMax.Compute(input);
		_logger.LogError("SoftMaxFunctionSumsToOne(input={Input}): result → {Result}", input, result);

        var sum = 0.0;
        foreach (var value in result)
        {
            sum += value;
        }

        // Assert
        Assert.That(sum, Is.EqualTo(1.0).Within(1e-9));
		_logger.LogWarning("<<< Exiting SoftMaxFunctionSumsToOne(input={Input})", input);
    }
}