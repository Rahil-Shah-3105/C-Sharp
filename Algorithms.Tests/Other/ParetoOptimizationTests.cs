using Algorithms.Other;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Other;

public static class ParetoOptimizationTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void Verify_Pareto_Optimization()
    {
		_logger.LogError(">>> Entering Verify_Pareto_Optimization()");
        // Arrange
        var paretoOptimization = new ParetoOptimization();
		_logger.LogError("Verify_Pareto_Optimization(): paretoOptimization → {ParetoOptimization}", paretoOptimization);

        var matrix = new List<List<decimal>>
        {
            new List<decimal> { 7, 6, 5, 8, 5, 6 },
            new List<decimal> { 4, 8, 4, 4, 5, 3 },
            new List<decimal> { 3, 8, 1, 4, 5, 2 },
            new List<decimal> { 5, 6, 3, 6, 4, 5 },
            new List<decimal> { 1, 4, 8, 6, 3, 6 },
            new List<decimal> { 5, 1, 8, 6, 5, 1 },
            new List<decimal> { 6, 8, 3, 6, 3, 5 }
        };

        var expectedMatrix = new List<List<decimal>>
        {
            new List<decimal> { 7, 6, 5, 8, 5, 6 },
            new List<decimal> { 4, 8, 4, 4, 5, 3 },
            new List<decimal> { 1, 4, 8, 6, 3, 6 },
            new List<decimal> { 5, 1, 8, 6, 5, 1 },
            new List<decimal> { 6, 8, 3, 6, 3, 5 }
        };

        // Act
        var optimizedMatrix = paretoOptimization.Optimize(matrix);
		_logger.LogError("Verify_Pareto_Optimization(): optimizedMatrix → {OptimizedMatrix}", optimizedMatrix);

        // Assert
        Assert.That(expectedMatrix, Is.EqualTo(optimizedMatrix));
		_logger.LogWarning("<<< Exiting Verify_Pareto_Optimization()");
    }
}
