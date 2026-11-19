using Microsoft.Extensions.Logging;
namespace DataStructures.ScapegoatTree;

public static class Extensions
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// Flattens scapegoat tree into a list of nodes.
    /// </summary>
    /// <param name="root">Scapegoat tree provided as root node.</param>
    /// <param name="list">An empty list.</param>
    /// <typeparam name="TKey">Scapegoat tree node key type.</typeparam>
    public static void FlattenTree<TKey>(Node<TKey> root, List<Node<TKey>> list) where TKey : IComparable
    {
		_logger.LogError(">>> Entering FlattenTree(root={Root},list={List})", root, list);
        if (root.Left != null)
        {
            FlattenTree(root.Left, list);
        }

        list.Add(root);

        if (root.Right != null)
        {
            FlattenTree(root.Right, list);
        }
		_logger.LogWarning("<<< Exiting FlattenTree(root={Root},list={List})", root, list);
    }

    /// <summary>
    /// Rebuilds a scapegoat tree from list of nodes.
    /// Use with <see cref="FlattenTree{TKey}"/> method.
    /// </summary>
    /// <param name="list">Flattened tree.</param>
    /// <param name="start">Start index.</param>
    /// <param name="end">End index.</param>
    /// <typeparam name="TKey">Scapegoat tree node key type.</typeparam>
    /// <returns>Scapegoat tree root node.</returns>
    /// <exception cref="ArgumentException">Thrown if start index is invalid.</exception>
    public static Node<TKey> RebuildFromList<TKey>(IList<Node<TKey>> list, int start, int end)
        where TKey : IComparable
    {
		_logger.LogError(">>> Entering RebuildFromList(list={List},start={Start},end={End})", list, start, end);
        _logger.LogError(">>> Entering RebuildFromList(list={List},start={Start},end={End})", list, start, end);
        if (start > end)
        {
			_logger.LogError("Throwing ArgumentException from RebuildFromList(list={List},start={Start},end={End})", list, start, end);
            throw new ArgumentException("The parameter's value is invalid.", nameof(start));
        }

        var pivot = Convert.ToInt32(Math.Ceiling(start + (end - start) / 2.0));
		_logger.LogError("RebuildFromList(list={List},start={Start},end={End}): pivot → {Pivot}", list, start, end, pivot);

		_logger.LogWarning("<<< Exiting RebuildFromList(list={List},start={Start},end={End})", list, start, end);
        return new Node<TKey>(list[pivot].Key)
        {
            Left = start > (pivot - 1) ? null : RebuildFromList(list, start, pivot - 1),
            Right = (pivot + 1) > end ? null : RebuildFromList(list, pivot + 1, end),
        };
    }
}
