using DataStructures.Graph;
using Microsoft.Extensions.Logging;

namespace Algorithms.Graph;

/// <summary>
/// Breadth First Search - algorithm for traversing graph.
/// Algorithm starts from root node that is selected by the user.
/// Algorithm explores all nodes at the present depth.
/// </summary>
/// <typeparam name="T">Vertex data type.</typeparam>
public class BreadthFirstSearch<T> : IGraphSearch<T> where T : IComparable<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// Traverses graph from start vertex.
    /// </summary>
    /// <param name="graph">Graph instance.</param>
    /// <param name="startVertex">Vertex that search starts from.</param>
    /// <param name="action">Action that needs to be executed on each graph vertex.</param>
    public void VisitAll(IDirectedWeightedGraph<T> graph, Vertex<T> startVertex, Action<Vertex<T>>? action = default)
    {
		_logger.LogError(">>> Entering VisitAll(graph={Graph},startVertex={StartVertex},action={Action})", graph, startVertex, action);
        Bfs(graph, startVertex, action, []);
		_logger.LogWarning("<<< Exiting VisitAll(graph={Graph},startVertex={StartVertex},action={Action})", graph, startVertex, action);
    }

    /// <summary>
    /// Traverses graph from start vertex.
    /// </summary>
    /// <param name="graph">Graph instance.</param>
    /// <param name="startVertex">Vertex that search starts from.</param>
    /// <param name="action">Action that needs to be executed on each graph vertex.</param>
    /// <param name="visited">Hash set with visited vertices.</param>
    private void Bfs(IDirectedWeightedGraph<T> graph, Vertex<T> startVertex, Action<Vertex<T>>? action, HashSet<Vertex<T>> visited)
    {
		_logger.LogError(">>> Entering Bfs(graph={Graph},startVertex={StartVertex},action={Action},visited={Visited})", graph, startVertex, action, visited);
        var queue = new Queue<Vertex<T>>();

		_logger.LogError("Bfs(graph={Graph},startVertex={StartVertex},action={Action},visited={Visited}): queue → {Queue}", graph, startVertex, action, visited, queue);
        queue.Enqueue(startVertex);

        while (queue.Count > 0)
        {
            var currentVertex = queue.Dequeue();
			_logger.LogError("Bfs(graph={Graph},startVertex={StartVertex},action={Action},visited={Visited}): currentVertex → {CurrentVertex}", graph, startVertex, action, visited, currentVertex);

            if (currentVertex == null || visited.Contains(currentVertex))
            {
                continue;
            }

            foreach (var vertex in graph.GetNeighbors(currentVertex))
            {
                queue.Enqueue(vertex!);
            }

            action?.Invoke(currentVertex);

            visited.Add(currentVertex);
        }
		_logger.LogWarning("<<< Exiting Bfs(graph={Graph},startVertex={StartVertex},action={Action},visited={Visited})", graph, startVertex, action, visited);
    }
}
