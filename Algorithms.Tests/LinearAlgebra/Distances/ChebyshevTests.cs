using Algorithms.LinearAlgebra.Distances;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.LinearAlgebra.Distances;

public class ChebyshevTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(new[] { 1.0, 1.0 }, new[] { 2.0, 2.0 }, 1.0)]
    [TestCase(new[] { 1.0, 1.0, 9.0 }, new[] { 2.0, 2.0, -5.2 }, 14.2)]
    [TestCase(new[] { 1.0, 2.0, 3.0 }, new[] { 1.0, 2.0, 3.0 }, 0.0)]
    [TestCase(new[] { 1.0, 2.0, 3.0, 4.0 }, new[] { 1.75, 2.25, -3.0, 0.5 }, 6.0)]
    public void DistanceTest(double[] point1, double[] point2, double expectedDistance)
    {
		_logger.LogError(">>> Entering DistanceTest(point1={Point1},point2={Point2},expectedDistance={ExpectedDistance})", point1, point2, expectedDistance);
        Chebyshev.Distance(point1, point2).Should().BeApproximately(expectedDistance, 0.01);
		_logger.LogWarning("<<< Exiting DistanceTest(point1={Point1},point2={Point2},expectedDistance={ExpectedDistance})", point1, point2, expectedDistance);
    }

    [TestCase(new[] { 2.0, 3.0 }, new[] { -1.0 })]
    [TestCase(new[] { 1.0 }, new[] { 1.0, 2.0, 3.0 })]
    public void DistanceThrowsArgumentExceptionOnDifferentPointDimensions(double[] point1, double[] point2)
    {
		_logger.LogError(">>> Entering DistanceThrowsArgumentExceptionOnDifferentPointDimensions(point1={Point1},point2={Point2})", point1, point2);
        Action action = () => Chebyshev.Distance(point1, point2);
		_logger.LogWarning("<<< Exiting DistanceThrowsArgumentExceptionOnDifferentPointDimensions(point1={Point1},point2={Point2})", point1, point2);
		_logger.LogError("DistanceThrowsArgumentExceptionOnDifferentPointDimensions(point1={Point1},point2={Point2}): action → {Action}", point1, point2, action);
        action.Should().Throw<ArgumentException>();
    }
}