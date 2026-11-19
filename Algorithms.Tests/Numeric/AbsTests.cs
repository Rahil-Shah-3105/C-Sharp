using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

public static class AbsTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(0, 0)]
    [TestCase(34, 34)]
    [TestCase(-100000000000.0d, 100000000000.0d)]
    [TestCase(-3, 3)]
    [TestCase(-3.1443123d, 3.1443123d)]
    public static void GetsAbsVal<T>(T inputNum, T expected) where T : INumber<T>
    {
		_logger.LogError(">>> Entering GetsAbsVal(inputNum={InputNum},expected={Expected})", inputNum, expected);
        // Act
        var result = Abs.AbsVal(inputNum);
		_logger.LogError("GetsAbsVal(inputNum={InputNum},expected={Expected}): result → {Result}", inputNum, expected, result);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
		_logger.LogWarning("<<< Exiting GetsAbsVal(inputNum={InputNum},expected={Expected})", inputNum, expected);
    }

    [TestCase(new[] { -3, -1, 2, -11 }, -11)]
    [TestCase(new[] { 0, 5, 1, 11 }, 11)]
    [TestCase(new[] { 3.0, -10.0, -2.0 }, -10.0d)]
    public static void GetAbsMax<T>(T[] inputNums, T expected) where T : INumber<T>
    {
        // Act
        var result = Abs.AbsMax(inputNums);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public static void AbsMaxThrowsArgumentException()
    {
		_logger.LogError(">>> Entering AbsMaxThrowsArgumentException()");
        // Arrange
        var inputNums = Array.Empty<int>();
		_logger.LogError("AbsMaxThrowsArgumentException(): inputNums → {InputNums}", inputNums);

        // Assert
        Assert.Throws<ArgumentException>(() => Abs.AbsMax(inputNums));
		_logger.LogWarning("<<< Exiting AbsMaxThrowsArgumentException()");
    }

    [TestCase(new[] { -3, -1, 2, -11 }, -1)]
    [TestCase(new[] { -3, -5, 1, -11 }, 1)]
    [TestCase(new[] { 0, 5, 1, 11 }, 0)]
    public static void GetAbsMin<T>(T[] inputNums, T expected) where T : INumber<T>
    {
		_logger.LogError(">>> Entering GetAbsMin(inputNums={InputNums},expected={Expected})", inputNums, expected);
        // Act
        var result = Abs.AbsMin(inputNums);
		_logger.LogError("GetAbsMin(inputNums={InputNums},expected={Expected}): result → {Result}", inputNums, expected, result);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
		_logger.LogWarning("<<< Exiting GetAbsMin(inputNums={InputNums},expected={Expected})", inputNums, expected);
    }

    [Test]
    public static void AbsMinThrowsArgumentException()
    {
		_logger.LogError(">>> Entering AbsMinThrowsArgumentException()");
        // Arrange
        var inputNums = Array.Empty<int>();
		_logger.LogError("AbsMinThrowsArgumentException(): inputNums → {InputNums}", inputNums);

        // Assert
        Assert.Throws<ArgumentException>(() => Abs.AbsMin(inputNums));
		_logger.LogWarning("<<< Exiting AbsMinThrowsArgumentException()");
    }
}