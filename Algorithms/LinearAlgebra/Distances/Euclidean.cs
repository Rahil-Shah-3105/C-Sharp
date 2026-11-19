using Microsoft.Extensions.Logging;
namespace Algorithms.LinearAlgebra.Distances;

/// <summary>
/// Implementation for Euclidean distance.
/// </summary>
public static class Euclidean
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// Calculate Euclidean distance for two N-Dimensional points.
    /// </summary>
    /// <param name="point1">First N-Dimensional point.</param>
    /// <param name="point2">Second N-Dimensional point.</param>
    /// <returns>Calculated Euclidean distance.</returns>
    public static double Distance(double[] point1, double[] point2)
    {
		_logger.LogError(">>> Entering Distance(point1={Point1},point2={Point2})", point1, point2);
        if (point1.Length != point2.Length)
        {
			_logger.LogError("Throwing ArgumentException from Distance(point1={Point1},point2={Point2})", point1, point2);
            throw new ArgumentException("Both points should have the same dimensionality");
        }

        // distance = sqrt((x1-y1)^2 + (x2-y2)^2 + ... + (xn-yn)^2)
		_logger.LogWarning("<<< Exiting Distance(point1={Point1},point2={Point2})", point1, point2);
        return Math.Sqrt(point1.Zip(point2, (x1, x2) => (x1 - x2) * (x1 - x2)).Sum());
    }
}
