using DataStructures.Graph;
using Microsoft.Extensions.Logging;

namespace Algorithms.Graph;

/// <summary>
/// Floyd Warshall algorithm on directed weighted graph.
/// </summary>
/// <typeparam name="T">generic type of data in graph.</typeparam>
public class FloydWarshall<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// runs the algorithm.
    /// </summary>
    /// <param name="graph">graph upon which to run.</param>
    /// <returns>
    /// a 2D array of shortest paths between any two vertices.
    /// where there is no path between two vertices - double.PositiveInfinity is placed.
    /// </returns>
    public double[,] Run(DirectedWeightedGraph<T> graph)
    {
		_logger.LogError(">>> Entering Run(graph={Graph})", graph);
        var distances = SetupDistances(graph);
        var vertexCount = distances.GetLength(0);
		_logger.LogError("Run(graph={Graph}): distances → {Distances}", graph, distances);
        for (var k = 0; k < vertexCount; k++)
        {
			_logger.LogError("Run(graph={Graph}): vertexCount → {VertexCount}", graph, vertexCount);
            for (var i = 0; i < vertexCount; i++)
            {
                for (var j = 0; j < vertexCount; j++)
                {
                    distances[i, j] = distances[i, j] > distances[i, k] + distances[k, j]
                    ? distances[i, k] + distances[k, j]
                    : distances[i, j];
                }
            }
        }

		_logger.LogWarning("<<< Exiting Run(graph={Graph})", graph);
        return distances;
    }

    /// <summary>
    /// setup adjacency matrix for use by main algorithm run.
    /// </summary>
    /// <param name="graph">graph to dissect adjacency matrix from.</param>
    /// <returns>the adjacency matrix in the format mentioned in Run.</returns>
    private double[,] SetupDistances(DirectedWeightedGraph<T> graph)
    {
		_logger.LogError(">>> Entering SetupDistances(graph={Graph})", graph);
        var distances = new double[graph.Count, graph.Count];
        for (int i = 0; i < distances.GetLength(0); i++)
        {
            for (var j = 0; j < distances.GetLength(0); j++)
            {
                var dist = graph.AdjacentDistance(graph.Vertices[i]!, graph.Vertices[j]!);
				_logger.LogError("SetupDistances(graph={Graph}): dist → {Dist}", graph, dist);
                distances[i, j] = dist != 0 ? dist : double.PositiveInfinity;
            }
        }

        for (var i = 0; i < distances.GetLength(0); i++)
        {
            distances[i, i] = 0;
        }

		_logger.LogWarning("<<< Exiting SetupDistances(graph={Graph})", graph);
        return distances;
    }
}
