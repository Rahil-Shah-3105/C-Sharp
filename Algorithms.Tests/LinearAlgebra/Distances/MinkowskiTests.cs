using Algorithms.LinearAlgebra.Distances;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.LinearAlgebra.Distances;

public class MinkowskiTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(new[] { 2.0, 3.0 }, new[] { -1.0, 5.0 }, 1, 5.0)] // Simulate Manhattan condition
    [TestCase(new[] { 7.0, 4.0, 3.0 }, new[] { 17.0, 6.0, 2.0 }, 2, 10.247)] // Simulate Euclidean condition
    [TestCase(new[] { 1.0, 2.0, 3.0, 4.0 }, new[] { 1.75, 2.25, -3.0, 0.5 }, 20, 6.0)] // Simulate Chebyshev condition
    [TestCase(new[] { 1.0, 1.0, 9.0 }, new[] { 2.0, 2.0, -5.2 }, 3, 14.2)]
    [TestCase(new[] { 1.0, 2.0, 3.0 }, new[] { 1.0, 2.0, 3.0 }, 5, 0.0)]
    public void DistanceTest(double[] point1, double[] point2, int order, double expectedDistance)
    {
		_logger.LogDebug(">>> Entering DistanceTest(point1={Point1},point2={Point2},order={Order},expectedDistance={ExpectedDistance})", point1, point2, order, expectedDistance);
        Minkowski.Distance(point1, point2, order).Should().BeApproximately(expectedDistance, 0.01);
		_logger.LogDebug("<<< Exiting DistanceTest(point1={Point1},point2={Point2},order={Order},expectedDistance={ExpectedDistance})", point1, point2, order, expectedDistance);
    }

    [TestCase(new[] { 2.0, 3.0 }, new[] { -1.0 }, 2)]
    [TestCase(new[] { 1.0 }, new[] { 1.0, 2.0, 3.0 }, 1)]
    [TestCase(new[] { 1.0, 1.0 }, new[] { 2.0, 2.0 }, 0)]
    public void DistanceThrowsArgumentExceptionOnInvalidInput(double[] point1, double[] point2, int order)
    {
		_logger.LogDebug(">>> Entering DistanceThrowsArgumentExceptionOnInvalidInput(point1={Point1},point2={Point2},order={Order})", point1, point2, order);
        Action action = () => Minkowski.Distance(point1, point2, order);
        action.Should().Throw<ArgumentException>();
		_logger.LogDebug("DistanceThrowsArgumentExceptionOnInvalidInput(point1={Point1},point2={Point2},order={Order}): action → {Action}", point1, point2, order, action);
		_logger.LogDebug("<<< Exiting DistanceThrowsArgumentExceptionOnInvalidInput(point1={Point1},point2={Point2},order={Order})", point1, point2, order);
    }
}