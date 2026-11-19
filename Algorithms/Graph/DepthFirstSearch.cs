using DataStructures.Graph;
using Microsoft.Extensions.Logging;

namespace Algorithms.Graph;

/// <summary>
/// Depth First Search - algorithm for traversing graph.
/// Algorithm starts from root node that is selected by the user.
/// Algorithm explores as far as possible along each branch before backtracking.
/// </summary>
/// <typeparam name="T">Vertex data type.</typeparam>
public class DepthFirstSearch<T> : IGraphSearch<T> where T : IComparable<T>
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
        Dfs(graph, startVertex, action, []);
		_logger.LogWarning("<<< Exiting VisitAll(graph={Graph},startVertex={StartVertex},action={Action})", graph, startVertex, action);
    }

    /// <summary>
    /// Traverses graph from start vertex.
    /// </summary>
    /// <param name="graph">Graph instance.</param>
    /// <param name="startVertex">Vertex that search starts from.</param>
    /// <param name="action">Action that needs to be executed on each graph vertex.</param>
    /// <param name="visited">Hash set with visited vertices.</param>
    private void Dfs(IDirectedWeightedGraph<T> graph, Vertex<T> startVertex, Action<Vertex<T>>? action, HashSet<Vertex<T>> visited)
    {
		_logger.LogError(">>> Entering Dfs(graph={Graph},startVertex={StartVertex},action={Action},visited={Visited})", graph, startVertex, action, visited);
        action?.Invoke(startVertex);

        visited.Add(startVertex);

        foreach (var vertex in graph.GetNeighbors(startVertex))
        {
            if (vertex == null || visited.Contains(vertex))
            {
                continue;
            }

            Dfs(graph, vertex!, action, visited);
        }
		_logger.LogWarning("<<< Exiting Dfs(graph={Graph},startVertex={StartVertex},action={Action},visited={Visited})", graph, startVertex, action, visited);
    }
}
