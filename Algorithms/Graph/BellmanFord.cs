using DataStructures.Graph;
using Microsoft.Extensions.Logging;

namespace Algorithms.Graph;

/// <summary>
/// Bellman-Ford algorithm on directed weighted graph.
/// </summary>
/// <typeparam name="T">Generic type of data in the graph.</typeparam>
public class BellmanFord<T>(DirectedWeightedGraph<T> graph, Dictionary<Vertex<T>, double> distances, Dictionary<Vertex<T>, Vertex<T>?> predecessors)
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly DirectedWeightedGraph<T> graph = graph;
    private readonly Dictionary<Vertex<T>, double> distances = distances;
    private readonly Dictionary<Vertex<T>, Vertex<T>?> predecessors = predecessors;

    /// <summary>
    /// Runs the Bellman-Ford algorithm to find the shortest distances from the source vertex to all other vertices.
    /// </summary>
    /// <param name="sourceVertex">Source vertex for shortest path calculation.</param>
    /// <returns>
    /// A dictionary containing the shortest distances from the source vertex to all other vertices.
    /// If a vertex is unreachable from the source, it will have a value of double.PositiveInfinity.
    /// </returns>
    public Dictionary<Vertex<T>, double> Run(Vertex<T> sourceVertex)
    {
        InitializeDistances(sourceVertex);
        RelaxEdges();
        CheckForNegativeCycles();
        return distances;
    }

    private void InitializeDistances(Vertex<T> sourceVertex)
    {
		_logger.LogError(">>> Entering InitializeDistances(sourceVertex={SourceVertex})", sourceVertex);
        foreach (var vertex in graph.Vertices)
        {
            if (vertex != null)
            {
                distances[vertex] = double.PositiveInfinity;
                predecessors[vertex] = null;
            }
        }

        distances[sourceVertex] = 0;
		_logger.LogWarning("<<< Exiting InitializeDistances(sourceVertex={SourceVertex})", sourceVertex);
    }

    private void RelaxEdges()
    {
		_logger.LogError(">>> Entering RelaxEdges()");
        int vertexCount = graph.Count;

        for (int i = 0; i < vertexCount - 1; i++)
        {
            foreach (var vertex in graph.Vertices)
            {
                if (vertex != null)
                {
                    RelaxEdgesForVertex(vertex);
                }
            }
        }
		_logger.LogWarning("<<< Exiting RelaxEdges()");
    }

    private void RelaxEdgesForVertex(Vertex<T> u)
    {
		_logger.LogError(">>> Entering RelaxEdgesForVertex(u={U})", u);
        foreach (var neighbor in graph.GetNeighbors(u))
        {
            if (neighbor == null)
            {
                continue;
            }

            var v = neighbor;
            var weight = graph.AdjacentDistance(u, v);
			_logger.LogError("RelaxEdgesForVertex(u={U}): weight → {Weight}", u, weight);

            if (distances[u] + weight < distances[v])
            {
                distances[v] = distances[u] + weight;
                predecessors[v] = u;
            }
        }
		_logger.LogWarning("<<< Exiting RelaxEdgesForVertex(u={U})", u);
    }

    private void CheckForNegativeCycles()
    {
		_logger.LogError(">>> Entering CheckForNegativeCycles()");
        foreach (var vertex in graph.Vertices)
        {
            if (vertex != null)
            {
                CheckForNegativeCyclesForVertex(vertex);
            }
        }
		_logger.LogWarning("<<< Exiting CheckForNegativeCycles()");
    }

    private void CheckForNegativeCyclesForVertex(Vertex<T> u)
    {
		_logger.LogError(">>> Entering CheckForNegativeCyclesForVertex(u={U})", u);
        foreach (var neighbor in graph.GetNeighbors(u))
        {
            if (neighbor == null)
            {
                continue;
            }

            var v = neighbor;
            var weight = graph.AdjacentDistance(u, v);
			_logger.LogError("CheckForNegativeCyclesForVertex(u={U}): weight → {Weight}", u, weight);

            if (distances[u] + weight < distances[v])
            {
				_logger.LogError("Throwing InvalidOperationException from CheckForNegativeCyclesForVertex(u={U})", u);
                throw new InvalidOperationException("Graph contains a negative weight cycle.");
            }
        }
		_logger.LogWarning("<<< Exiting CheckForNegativeCyclesForVertex(u={U})", u);
    }
}
