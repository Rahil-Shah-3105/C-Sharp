using Microsoft.Extensions.Logging;
namespace Algorithms.LinearAlgebra.Distances;

/// <summary>
/// Implementation of Minkowski distance.
/// It is the sum of the lengths of the projections of the line segment between the points onto the
/// coordinate axes, raised to the power of the order and then taking the p-th root.
/// For the case of order = 1, the Minkowski distance degenerates to the Manhattan distance,
/// for order = 2, the usual Euclidean distance is obtained and for order = infinity, the Chebyshev distance is obtained.
/// </summary>
public static class Minkowski
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// Calculate Minkowski distance for two N-Dimensional points.
    /// </summary>
    /// <param name="point1">First N-Dimensional point.</param>
    /// <param name="point2">Second N-Dimensional point.</param>
    /// <param name="order">Order of the Minkowski distance.</param>
    /// <returns>Calculated Minkowski distance.</returns>
    public static double Distance(double[] point1, double[] point2, int order)
    {
		_logger.LogDebug(">>> Entering Distance(point1={Point1},point2={Point2},order={Order})", point1, point2, order);
        if (order < 1)
        {
			_logger.LogWarning("Throwing ArgumentException from Distance(point1={Point1},point2={Point2},order={Order})", point1, point2, order);
            throw new ArgumentException("The order must be greater than or equal to 1.");
        }

        if (point1.Length != point2.Length)
        {
		_logger.LogDebug("<<< Exiting Distance(point1={Point1},point2={Point2},order={Order})", point1, point2, order);
            throw new ArgumentException("Both points should have the same dimensionality");
        }

        // distance = (|x1-y1|^p + |x2-y2|^p + ... + |xn-yn|^p)^(1/p)
        return Math.Pow(point1.Zip(point2, (x1, x2) => Math.Pow(Math.Abs(x1 - x2), order)).Sum(), 1.0 / order);
    }
}
