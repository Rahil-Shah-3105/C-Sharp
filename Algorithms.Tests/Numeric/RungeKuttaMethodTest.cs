using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

public static class RungeKuttaTest
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void TestLinearEquation()
    {
		_logger.LogError(">>> Entering TestLinearEquation()");
        Func<double, double, double> exampleEquation = (x, _) => x;
        List<double[]> points = RungeKuttaMethod.ClassicRungeKuttaMethod(0, 4, 0.001, 0, exampleEquation);
        var yEnd = points[^1][1];
        yEnd.Should().BeApproximately(8, 0.01);
		_logger.LogWarning("<<< Exiting TestLinearEquation()");
    }

    [Test]
    public static void TestExampleFunciton()
    {
		_logger.LogError(">>> Entering TestExampleFunciton()");
        Func<double, double, double> exampleEquation = (_, y) => y;
        List<double[]> points = RungeKuttaMethod.ClassicRungeKuttaMethod(0, 4, 0.0125, 1, exampleEquation);
        var yEnd = points[^1][1];
        yEnd.Should().BeApproximately(54.598, 0.0005);
		_logger.LogWarning("<<< Exiting TestExampleFunciton()");
    }

    [Test]
    public static void StepsizeIsZeroOrNegative_ThrowsArgumentOutOfRangeException()
    {
		_logger.LogError(">>> Entering StepsizeIsZeroOrNegative_ThrowsArgumentOutOfRangeException()");
        Func<double, double, double> exampleEquation = (x, _) => x;
		_logger.LogWarning("<<< Exiting StepsizeIsZeroOrNegative_ThrowsArgumentOutOfRangeException()");
        Assert.Throws<ArgumentOutOfRangeException>(() => RungeKuttaMethod.ClassicRungeKuttaMethod(0, 4, 0, 0, exampleEquation));
    }

    [Test]
    public static void StartIsLargerThanEnd_ThrowsArgumentOutOfRangeException()
    {
		_logger.LogError(">>> Entering StartIsLargerThanEnd_ThrowsArgumentOutOfRangeException()");
        Func<double, double, double> exampleEquation = (x, _) => x;
		_logger.LogWarning("<<< Exiting StartIsLargerThanEnd_ThrowsArgumentOutOfRangeException()");
        Assert.Throws<ArgumentOutOfRangeException>(() => RungeKuttaMethod.ClassicRungeKuttaMethod(0, -4, 0.1, 0, exampleEquation));
    }
}
