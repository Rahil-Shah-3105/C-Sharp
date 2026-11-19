using Algorithms.LinearAlgebra.Distances;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.LinearAlgebra.Distances;

public class ManhattanTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// Test the result given by Manhattan distance function.
    /// </summary>
    /// <param name="point1">Origin point.</param>
    /// <param name="point2">Target point.</param>
    /// <param name="expectedDistance">Expected result.</param>
    [TestCase(new[] { 1.5 }, new[] { -1.0 }, 2.5)]
    [TestCase(new[] { 2.0, 3.0 }, new[] { -1.0, 5.0 }, 5)]
    [TestCase(new[] { 1.0, 2.0, 3.0 }, new[] { 1.0, 2.0, 3.0 }, 0)]
    [TestCase(new[] { 1.0, 2.0, 3.0, 4.0 }, new[] { 1.75, 2.25, -3.0, 0.5 }, 10.5)]
    public void DistanceTest(double[] point1, double[] point2, double expectedDistance)
    {
		_logger.LogError(">>> Entering DistanceTest(point1={Point1},point2={Point2},expectedDistance={ExpectedDistance})", point1, point2, expectedDistance);
        Manhattan.Distance(point1, point2).Should().BeApproximately(expectedDistance, 0.01);
		_logger.LogWarning("<<< Exiting DistanceTest(point1={Point1},point2={Point2},expectedDistance={ExpectedDistance})", point1, point2, expectedDistance);
    }

    /// <summary>
    /// Test that it throws ArgumentException if two different dimension arrays are given.
    /// </summary>
    /// <param name="point1">First point of N dimensions.</param>
    /// <param name="point2">Second point of M dimensions, M != N.</param>
    [TestCase(new[] { 2.0, 3.0 }, new[] { -1.0 })]
    [TestCase(new[] { 1.0 }, new[] { 1.0, 2.0, 3.0 })]
    public void DistanceThrowsArgumentExceptionOnDifferentPointDimensions(double[] point1, double[] point2)
    {
		_logger.LogError(">>> Entering DistanceThrowsArgumentExceptionOnDifferentPointDimensions(point1={Point1},point2={Point2})", point1, point2);
        Action action = () => Manhattan.Distance(point1, point2);
		_logger.LogWarning("<<< Exiting DistanceThrowsArgumentExceptionOnDifferentPointDimensions(point1={Point1},point2={Point2})", point1, point2);
		_logger.LogError("DistanceThrowsArgumentExceptionOnDifferentPointDimensions(point1={Point1},point2={Point2}): action → {Action}", point1, point2, action);
        action.Should().Throw<ArgumentException>();
    }
}
