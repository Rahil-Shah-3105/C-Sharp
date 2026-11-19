using DataStructures.Graph;
using Microsoft.Extensions.Logging;

namespace Algorithms.Graph.Dijkstra;

public static class DijkstraAlgorithm
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// Implementation of the Dijkstra shortest path algorithm for cyclic graphs.
    /// https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm.
    /// </summary>
    /// <param name="graph">Graph instance.</param>
    /// <param name="startVertex">Starting vertex instance.</param>
    /// <typeparam name="T">Generic Parameter.</typeparam>
    /// <returns>List of distances from current vertex to all other vertices.</returns>
    /// <exception cref="InvalidOperationException">Exception thrown in case when graph is null or start
    /// vertex does not belong to graph instance.</exception>
    public static DistanceModel<T>[] GenerateShortestPath<T>(DirectedWeightedGraph<T> graph, Vertex<T> startVertex)
    {
		_logger.LogError(">>> Entering GenerateShortestPath(graph={Graph},startVertex={StartVertex})", graph, startVertex);
        ValidateGraphAndStartVertex(graph, startVertex);

        var visitedVertices = new List<Vertex<T>>();
		_logger.LogError("GenerateShortestPath(graph={Graph},startVertex={StartVertex}): visitedVertices → {VisitedVertices}", graph, startVertex, visitedVertices);

        var distanceArray = InitializeDistanceArray(graph, startVertex);
		_logger.LogError("GenerateShortestPath(graph={Graph},startVertex={StartVertex}): distanceArray → {DistanceArray}", graph, startVertex, distanceArray);

        var distanceRecord = new PriorityQueue<DistanceModel<T>, double>();
		_logger.LogError("GenerateShortestPath(graph={Graph},startVertex={StartVertex}): distanceRecord → {DistanceRecord}", graph, startVertex, distanceRecord);

        distanceRecord.Enqueue(distanceArray[0], distanceArray[0].Distance);

        while (visitedVertices.Count != distanceArray.Length && distanceRecord.Count != 0)
        {
            while (visitedVertices.Contains(distanceRecord.Peek().Vertex!))
            {
                distanceRecord.Dequeue();
            }

            var minDistance = distanceRecord.Dequeue();
			_logger.LogError("GenerateShortestPath(graph={Graph},startVertex={StartVertex}): minDistance → {MinDistance}", graph, startVertex, minDistance);

            var currentPath = minDistance.Distance;

            visitedVertices.Add(minDistance.Vertex!);

            var neighborVertices = graph
                .GetNeighbors(minDistance.Vertex!)
                .Where(x => x != null && !visitedVertices.Contains(x))
                .ToList();

            foreach (var vertex in neighborVertices)
            {
                var adjacentDistance = graph.AdjacentDistance(minDistance.Vertex!, vertex!);
				_logger.LogError("GenerateShortestPath(graph={Graph},startVertex={StartVertex}): adjacentDistance → {AdjacentDistance}", graph, startVertex, adjacentDistance);

                var distance = distanceArray[vertex!.Index];

                var fullDistance = currentPath + adjacentDistance;

                if (distance.Distance > fullDistance)
                {
                    distance.Distance = fullDistance;
					_logger.LogDebug("GenerateShortestPath(graph={Graph},startVertex={StartVertex}): distance.Distance → {Arg2}", graph, startVertex, distance.Distance);
                    distance.PreviousVertex = minDistance.Vertex;
					_logger.LogDebug("GenerateShortestPath(graph={Graph},startVertex={StartVertex}): distance.PreviousVertex → {Arg2}", graph, startVertex, distance.PreviousVertex);
                    distanceRecord.Enqueue(distance, fullDistance);
                }
            }
        }

		_logger.LogWarning("<<< Exiting GenerateShortestPath(graph={Graph},startVertex={StartVertex})", graph, startVertex);
        return distanceArray;
    }

    private static DistanceModel<T>[] InitializeDistanceArray<T>(
        IDirectedWeightedGraph<T> graph,
        Vertex<T> startVertex)
    {
		_logger.LogError(">>> Entering InitializeDistanceArray(graph={Graph},startVertex={StartVertex})", graph, startVertex);
        var distArray = new DistanceModel<T>[graph.Count];

        distArray[startVertex.Index] = new DistanceModel<T>(startVertex, startVertex, 0);

        foreach (var vertex in graph.Vertices.Where(x => x != null && !x.Equals(startVertex)))
        {
            distArray[vertex!.Index] = new DistanceModel<T>(vertex, null, double.MaxValue);
        }

		_logger.LogWarning("<<< Exiting InitializeDistanceArray(graph={Graph},startVertex={StartVertex})", graph, startVertex);
        return distArray;
    }

    private static void ValidateGraphAndStartVertex<T>(DirectedWeightedGraph<T> graph, Vertex<T> startVertex)
    {
		_logger.LogError(">>> Entering ValidateGraphAndStartVertex(graph={Graph},startVertex={StartVertex})", graph, startVertex);
        if (graph is null)
        {
			_logger.LogError("Throwing ArgumentNullException from ValidateGraphAndStartVertex(graph={Graph},startVertex={StartVertex})", graph, startVertex);
            throw new ArgumentNullException(nameof(graph));
        }

        if (startVertex.Graph != null && !startVertex.Graph.Equals(graph))
        {
            throw new ArgumentNullException(nameof(graph));
        }
		_logger.LogWarning("<<< Exiting ValidateGraphAndStartVertex(graph={Graph},startVertex={StartVertex})", graph, startVertex);
    }
}
