using Microsoft.Extensions.Logging;
namespace Algorithms.LinearAlgebra.Distances;

/// <summary>
/// Implementation fo Manhattan distance.
/// It is the sum of the lengths of the projections of the line segment between the points onto the coordinate axes.
/// In other words, it is the sum of absolute difference between the measures in all dimensions of two points.
///
/// Its commonly used in regression analysis.
/// </summary>
public static class Manhattan
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// Calculate Manhattan distance for two N-Dimensional points.
    /// </summary>
    /// <param name="point1">First N-Dimensional point.</param>
    /// <param name="point2">Second N-Dimensional point.</param>
    /// <returns>Calculated Manhattan distance.</returns>
    public static double Distance(double[] point1, double[] point2)
    {
		_logger.LogError(">>> Entering Distance(point1={Point1},point2={Point2})", point1, point2);
        if (point1.Length != point2.Length)
        {
			_logger.LogError("Throwing ArgumentException from Distance(point1={Point1},point2={Point2})", point1, point2);
            throw new ArgumentException("Both points should have the same dimensionality");
        }

        // distance = |x1-y1| + |x2-y2| + ... + |xn-yn|
		_logger.LogWarning("<<< Exiting Distance(point1={Point1},point2={Point2})", point1, point2);
        return point1.Zip(point2, (x1, x2) => Math.Abs(x1 - x2)).Sum();
    }
}
