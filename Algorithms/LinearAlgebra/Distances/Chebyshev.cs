using Microsoft.Extensions.Logging;
namespace Algorithms.LinearAlgebra.Distances;

/// <summary>
/// Implementation of Chebyshev distance.
/// It is the maximum absolute difference between the measures in all dimensions of two points.
/// In other words, it is the maximum distance one has to travel along any coordinate axis to get from one point to another.
///
/// It is commonly used in various fields such as chess, warehouse logistics, and more.
/// </summary>
public static class Chebyshev
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// Calculate Chebyshev distance for two N-Dimensional points.
    /// </summary>
    /// <param name="point1">First N-Dimensional point.</param>
    /// <param name="point2">Second N-Dimensional point.</param>
    /// <returns>Calculated Chebyshev distance.</returns>
    public static double Distance(double[] point1, double[] point2)
    {
		_logger.LogDebug(">>> Entering Distance(point1={Point1},point2={Point2})", point1, point2);
        if (point1.Length != point2.Length)
        {
			_logger.LogWarning("Throwing ArgumentException from Distance(point1={Point1},point2={Point2})", point1, point2);
		_logger.LogDebug("<<< Exiting Distance(point1={Point1},point2={Point2})", point1, point2);
            throw new ArgumentException("Both points should have the same dimensionality");
        }

        // distance = max(|x1-y1|, |x2-y2|, ..., |xn-yn|)
        return point1.Zip(point2, (x1, x2) => Math.Abs(x1 - x2)).Max();
    }
}
