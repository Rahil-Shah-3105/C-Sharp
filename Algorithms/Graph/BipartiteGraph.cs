using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Algorithms.Graph;

/// <summary>
/// Checks if a graph is bipartite (2-colorable).
/// A bipartite graph can be divided into two independent sets where no two vertices
/// within the same set are adjacent.
/// </summary>
public static class BipartiteGraph
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// Checks if a graph is bipartite using BFS-based coloring.
    /// </summary>
    /// <typeparam name="T">Type of vertex.</typeparam>
    /// <param name="vertices">All vertices in the graph.</param>
    /// <param name="getNeighbors">Function to get neighbors of a vertex.</param>
    /// <returns>True if graph is bipartite, false otherwise.</returns>
    public static bool IsBipartite<T>(
        IEnumerable<T> vertices,
        Func<T, IEnumerable<T>> getNeighbors) where T : notnull
    {
		_logger.LogError(">>> Entering IsBipartite(vertices={Vertices},getNeighbors={GetNeighbors})", vertices, getNeighbors);
        if (vertices == null)
        {
			_logger.LogError("Throwing ArgumentNullException from IsBipartite(vertices={Vertices},getNeighbors={GetNeighbors})", vertices, getNeighbors);
            throw new ArgumentNullException(nameof(vertices));
        }

        if (getNeighbors == null)
        {
            throw new ArgumentNullException(nameof(getNeighbors));
        }

        var vertexList = vertices.ToList();
		_logger.LogError("IsBipartite(vertices={Vertices},getNeighbors={GetNeighbors}): vertexList → {VertexList}", vertices, getNeighbors, vertexList);
        if (vertexList.Count == 0)
        {
            return true; // Empty graph is bipartite
        }

        var colors = new Dictionary<T, int>();
		_logger.LogError("IsBipartite(vertices={Vertices},getNeighbors={GetNeighbors}): colors → {Colors}", vertices, getNeighbors, colors);

        // Check each connected component
        foreach (var start in vertexList)
        {
            if (colors.ContainsKey(start))
            {
                continue; // Already colored
            }

            if (!BfsColor(start, colors, getNeighbors))
            {
                return false;
            }
        }

		_logger.LogWarning("<<< Exiting IsBipartite(vertices={Vertices},getNeighbors={GetNeighbors})", vertices, getNeighbors);
        return true;
    }

    /// <summary>
    /// Gets the two partitions of a bipartite graph.
    /// </summary>
    /// <typeparam name="T">Type of vertex.</typeparam>
    /// <param name="vertices">All vertices in the graph.</param>
    /// <param name="getNeighbors">Function to get neighbors of a vertex.</param>
    /// <returns>Tuple of two sets representing the partitions, or null if not bipartite.</returns>
    public static (HashSet<T> SetA, HashSet<T> SetB)? GetPartitions<T>(
        IEnumerable<T> vertices,
        Func<T, IEnumerable<T>> getNeighbors) where T : notnull
    {
		_logger.LogError(">>> Entering static()");
        if (vertices == null)
        {
			_logger.LogError("Throwing ArgumentNullException from static()");
            throw new ArgumentNullException(nameof(vertices));
        }

        if (getNeighbors == null)
        {
            throw new ArgumentNullException(nameof(getNeighbors));
        }

        var vertexList = vertices.ToList();
		_logger.LogError("static(): vertexList → {VertexList}", vertexList);
        if (vertexList.Count == 0)
        {
            return (new HashSet<T>(), new HashSet<T>());
        }

        var colors = new Dictionary<T, int>();
		_logger.LogError("static(): colors → {Colors}", colors);

        // Color all components
        foreach (var start in vertexList)
        {
            if (colors.ContainsKey(start))
            {
                continue;
            }

            if (!BfsColor(start, colors, getNeighbors))
            {
                return null; // Not bipartite
            }
        }

        // Split into two sets based on color
        var setA = new HashSet<T>();
		_logger.LogError("static(): setA → {SetA}", setA);
        var setB = new HashSet<T>();
		_logger.LogError("static(): setB → {SetB}", setB);

        foreach (var vertex in vertexList)
        {
            if (colors[vertex] == 0)
            {
                setA.Add(vertex);
            }
            else
            {
                setB.Add(vertex);
            }
        }

		_logger.LogWarning("<<< Exiting static()");
        return (setA, setB);
    }

    /// <summary>
    /// Checks if a graph is bipartite using DFS-based coloring.
    /// </summary>
    /// <typeparam name="T">Type of vertex.</typeparam>
    /// <param name="vertices">All vertices in the graph.</param>
    /// <param name="getNeighbors">Function to get neighbors of a vertex.</param>
    /// <returns>True if graph is bipartite, false otherwise.</returns>
    public static bool IsBipartiteDfs<T>(
        IEnumerable<T> vertices,
        Func<T, IEnumerable<T>> getNeighbors) where T : notnull
    {
		_logger.LogError(">>> Entering IsBipartiteDfs(vertices={Vertices},getNeighbors={GetNeighbors})", vertices, getNeighbors);
        if (vertices == null)
        {
			_logger.LogError("Throwing ArgumentNullException from IsBipartiteDfs(vertices={Vertices},getNeighbors={GetNeighbors})", vertices, getNeighbors);
            throw new ArgumentNullException(nameof(vertices));
        }

        if (getNeighbors == null)
        {
            throw new ArgumentNullException(nameof(getNeighbors));
        }

        var vertexList = vertices.ToList();
		_logger.LogError("IsBipartiteDfs(vertices={Vertices},getNeighbors={GetNeighbors}): vertexList → {VertexList}", vertices, getNeighbors, vertexList);
        if (vertexList.Count == 0)
        {
            return true;
        }

        var colors = new Dictionary<T, int>();
		_logger.LogError("IsBipartiteDfs(vertices={Vertices},getNeighbors={GetNeighbors}): colors → {Colors}", vertices, getNeighbors, colors);

        foreach (var start in vertexList)
        {
            if (colors.ContainsKey(start))
            {
                continue;
            }

            if (!DfsColor(start, 0, colors, getNeighbors))
            {
                return false;
            }
        }

		_logger.LogWarning("<<< Exiting IsBipartiteDfs(vertices={Vertices},getNeighbors={GetNeighbors})", vertices, getNeighbors);
        return true;
    }

    private static bool BfsColor<T>(
        T start,
        Dictionary<T, int> colors,
        Func<T, IEnumerable<T>> getNeighbors) where T : notnull
    {
		_logger.LogError(">>> Entering BfsColor(start={Start},colors={Colors},getNeighbors={GetNeighbors})", start, colors, getNeighbors);
        _logger.LogError(">>> Entering BfsColor(start={Start},colors={Colors},getNeighbors={GetNeighbors})", start, colors, getNeighbors);
        var queue = new Queue<T>();
		_logger.LogError("BfsColor(start={Start},colors={Colors},getNeighbors={GetNeighbors}): queue → {Queue}", start, colors, getNeighbors, queue);
        queue.Enqueue(start);
        colors[start] = 0;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
			_logger.LogError("BfsColor(start={Start},colors={Colors},getNeighbors={GetNeighbors}): current → {Current}", start, colors, getNeighbors, current);
            var currentColor = colors[current];
            var nextColor = 1 - currentColor;

            foreach (var neighbor in getNeighbors(current))
            {
                if (!colors.ContainsKey(neighbor))
                {
                    colors[neighbor] = nextColor;
                    queue.Enqueue(neighbor);
                }
                else if (colors[neighbor] == currentColor)
                {
                    return false; // Same color as current - not bipartite
                }
                else
                {
                    // Different color - valid
                }
            }
        }

		_logger.LogWarning("<<< Exiting BfsColor(start={Start},colors={Colors},getNeighbors={GetNeighbors})", start, colors, getNeighbors);
        return true;
    }

    private static bool DfsColor<T>(
        T vertex,
        int color,
        Dictionary<T, int> colors,
        Func<T, IEnumerable<T>> getNeighbors) where T : notnull
    {
		_logger.LogError(">>> Entering DfsColor(vertex={Vertex},color={Color},colors={Colors},getNeighbors={GetNeighbors})", vertex, color, colors, getNeighbors);
        colors[vertex] = color;
        var nextColor = 1 - color;

        foreach (var neighbor in getNeighbors(vertex))
        {
            if (!colors.ContainsKey(neighbor))
            {
                if (!DfsColor(neighbor, nextColor, colors, getNeighbors))
                {
                    return false;
                }
            }
            else if (colors[neighbor] == color)
            {
                return false; // Same color - not bipartite
            }
            else
            {
                // Different color - valid
            }
        }

		_logger.LogWarning("<<< Exiting DfsColor(vertex={Vertex},color={Color},colors={Colors},getNeighbors={GetNeighbors})", vertex, color, colors, getNeighbors);
        return true;
    }
}
