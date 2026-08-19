using Microsoft.Extensions.Logging;
namespace DataStructures.DisjointSet;

/// <summary>
/// Implementation of Disjoint Set with Union By Rank and Path Compression heuristics.
/// </summary>
/// <typeparam name="T"> generic type for implementation.</typeparam>
public class DisjointSet<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// make a new set and return its representative.
    /// </summary>
    /// <param name="x">element to add in to the DS.</param>
    /// <returns>representative of x.</returns>
    public Node<T> MakeSet(T x) => new(x);

    /// <summary>
    /// find the representative of a certain node.
    /// </summary>
    /// <param name="node">node to find representative.</param>
    /// <returns>representative of x.</returns>
    public Node<T> FindSet(Node<T> node)
    {
		_logger.LogDebug(">>> Entering FindSet(node={Node})", node);
        if (node != node.Parent)
        {
            node.Parent = FindSet(node.Parent);
        }

		_logger.LogDebug("<<< Exiting FindSet(node={Node})", node);
        return node.Parent;
    }

    /// <summary>
    /// merge two sets.
    /// </summary>
    /// <param name="x">first set member.</param>
    /// <param name="y">second set member.</param>
    public void UnionSet(Node<T> x, Node<T> y)
    {
		_logger.LogDebug(">>> Entering UnionSet(x={X},y={Y})", x, y);
        Node<T> nx = FindSet(x);
        Node<T> ny = FindSet(y);
		_logger.LogDebug("UnionSet(x={X},y={Y}): ny → {Ny}", x, y, ny);
        if (nx == ny)
		_logger.LogDebug("UnionSet(x={X},y={Y}): nx → {Nx}", x, y, nx);
        {
            return;
        }

        if (nx.Rank > ny.Rank)
        {
            ny.Parent = nx;
			_logger.LogDebug("UnionSet(x={X},y={Y}): ny.Parent → {Arg2}", x, y, ny.Parent);
        }
        else if (ny.Rank > nx.Rank)
        {
            nx.Parent = ny;
			_logger.LogDebug("UnionSet(x={X},y={Y}): nx.Parent → {Arg2}", x, y, nx.Parent);
        }
        else
        {
            nx.Parent = ny;
            ny.Rank++;
        }
		_logger.LogDebug("<<< Exiting UnionSet(x={X},y={Y})", x, y);
    }
}
