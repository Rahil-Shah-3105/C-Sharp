using Microsoft.Extensions.Logging;
namespace DataStructures.SegmentTrees;

/// <summary>
///     This is an extension of a segment tree, which allows the update of a single element.
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="SegmentTreeUpdate" /> class.
///     Runtime complexity: O(n) where n equals the array-length.
/// </remarks>
/// <param name="arr">Array on which the queries should be made.</param>
public class SegmentTreeUpdate(int[] arr) : SegmentTree(arr)
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Updates a single element of the input array.
    ///     Changes the leaf first and updates its parents afterwards.
    ///     Runtime complexity: O(logN) where N equals the initial array-length.
    /// </summary>
    /// <param name="node">Index of the node that should be updated.</param>
    /// <param name="value">New Value of the element.</param>
    public void Update(int node, int value)
    {
		_logger.LogDebug(">>> Entering Update(node={Node},value={Value})", node, value);
        Tree[node + Tree.Length / 2] = value;
        Propagate(Parent(node + Tree.Length / 2));
		_logger.LogDebug("<<< Exiting Update(node={Node},value={Value})", node, value);
    }

    /// <summary>
    ///     Recalculates the value of node by its children.
    ///     Calls its parent to do the same.
    /// </summary>
    /// <param name="node">Index of current node.</param>
    private void Propagate(int node)
    {
		_logger.LogDebug(">>> Entering Propagate(node={Node})", node);
        if (node == 0)
        {
            // passed root
            return;
        }

        Tree[node] = Tree[Left(node)] + Tree[Right(node)];
        Propagate(Parent(node));
		_logger.LogDebug("<<< Exiting Propagate(node={Node})", node);
    }
}
