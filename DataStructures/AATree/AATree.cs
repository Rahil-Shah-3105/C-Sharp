using Microsoft.Extensions.Logging;
namespace DataStructures.AATree;

/// <summary>
///     A simple self-balancing binary search tree.
/// </summary>
/// <remarks>
///     AA Trees are a form of self-balancing binary search tree named after their inventor
///     Arne Anderson. AA Trees are designed to be simple to understand and implement.
///     This is accomplished by limiting how nodes can be added to the tree.
///     This simplifies rebalancing operations.
///     More information: https://en.wikipedia.org/wiki/AA_tree .
/// </remarks>
/// <typeparam name="TKey">The type of key for the AA tree.</typeparam>
/// <remarks>
///     Initializes a new instance of the <see cref="AaTree{TKey}" /> class with a custom comparer.
/// </remarks>
/// <param name="customComparer">The custom comparer to use to compare keys.</param>
public class AaTree<TKey>(Comparer<TKey> customComparer)
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     The comparer function to use to compare the keys.
    /// </summary>
    private readonly Comparer<TKey> comparer = customComparer;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AaTree{TKey}" /> class.
    /// </summary>
    public AaTree()
        : this(Comparer<TKey>.Default)
    {
	_logger.LogDebug(">>> Entering AaTree()");
	_logger.LogDebug("<<< Exiting AaTree()");
    }

    /// <summary>
    ///     Gets the root of the tree.
    /// </summary>
    public AaTreeNode<TKey>? Root { get; private set; }

    /// <summary>
    ///     Gets the number of elements in the tree.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    ///     Add a single element to the tree.
    /// </summary>
    /// <param name="key">The element to add to the tree.</param>
    public void Add(TKey key)
    {
		_logger.LogDebug(">>> Entering Add(key={Key})", key);
        Root = Add(key, Root);
        Count++;
		_logger.LogWarning("Add(key={Key}): Root → {Root}", key, Root);
		_logger.LogDebug("<<< Exiting Add(key={Key})", key);
    }

    /// <summary>
    ///     Add multiple elements to the tree.
    /// </summary>
    /// <param name="keys">The elements to add to the tree.</param>
    public void AddRange(IEnumerable<TKey> keys)
    {
		_logger.LogDebug(">>> Entering AddRange(keys={Keys})", keys);
        foreach (var key in keys)
        {
            Root = Add(key, Root);
            Count++;
			_logger.LogWarning("AddRange(keys={Keys}): Root → {Root}", keys, Root);
        }
		_logger.LogDebug("<<< Exiting AddRange(keys={Keys})", keys);
    }

    /// <summary>
    ///     Remove a single element from the tree.
    /// </summary>
    /// <param name="key">Element to remove.</param>
    public void Remove(TKey key)
    {
		_logger.LogDebug(">>> Entering Remove(key={Key})", key);
        if (!Contains(key, Root))
        {
			_logger.LogWarning("Throwing InvalidOperationException from Remove(key={Key})", key);
            throw new InvalidOperationException($"{nameof(key)} is not in the tree");
        }

        Root = Remove(key, Root);
        Count--;
		_logger.LogWarning("Remove(key={Key}): Root → {Root}", key, Root);
		_logger.LogDebug("<<< Exiting Remove(key={Key})", key);
    }

    /// <summary>
    ///     Checks if the specified element is in the tree.
    /// </summary>
    /// <param name="key">The element to look for.</param>
    /// <returns>true if the element is in the tree, false otherwise.</returns>
    public bool Contains(TKey key) => Contains(key, Root);

    /// <summary>
    ///     Gets the largest element in the tree. (ie. the element in the right most node).
    /// </summary>
    /// <returns>The largest element in the tree according to the stored comparer.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the tree is empty.</exception>
    public TKey GetMax()
    {
		_logger.LogDebug(">>> Entering GetMax()");
        if (Root is null)
        {
			_logger.LogWarning("Throwing InvalidOperationException from GetMax()");
            throw new InvalidOperationException("Tree is empty!");
        }

		_logger.LogDebug("<<< Exiting GetMax()");
        return GetMax(Root).Key;
    }

    /// <summary>
    ///     Gets the smallest element in the tree. (ie. the element in the left most node).
    /// </summary>
    /// <returns>The smallest element in the tree according to the stored comparer.</returns>
    /// <throws>InvalidOperationException if the tree is empty.</throws>
    public TKey GetMin()
    {
		_logger.LogDebug(">>> Entering GetMin()");
        if (Root is null)
        {
			_logger.LogWarning("Throwing InvalidOperationException from GetMin()");
            throw new InvalidOperationException("Tree is empty!");
        }

		_logger.LogDebug("<<< Exiting GetMin()");
        return GetMin(Root).Key;
    }

    /// <summary>
    ///     Gets all the elements in the tree in in-order order.
    /// </summary>
    /// <returns>Sequence of elements in in-order order.</returns>
    public IEnumerable<TKey> GetKeysInOrder()
    {
		_logger.LogDebug(">>> Entering GetKeysInOrder()");
        var result = new List<TKey>();
        InOrderWalk(Root);
		_logger.LogDebug("GetKeysInOrder(): result → {Result}", result);
		_logger.LogDebug("<<< Exiting GetKeysInOrder()");
        return result;

        void InOrderWalk(AaTreeNode<TKey>? node)
        {
            if (node is null)
            {
                return;
            }

            InOrderWalk(node.Left);
            result.Add(node.Key);
            InOrderWalk(node.Right);
        }
    }

    /// <summary>
    ///     Gets all the elements in the tree in pre-order order.
    /// </summary>
    /// <returns>Sequence of elements in pre-order order.</returns>
    public IEnumerable<TKey> GetKeysPreOrder()
    {
		_logger.LogDebug(">>> Entering GetKeysPreOrder()");
        var result = new List<TKey>();
        PreOrderWalk(Root);
		_logger.LogDebug("GetKeysPreOrder(): result → {Result}", result);
		_logger.LogDebug("<<< Exiting GetKeysPreOrder()");
        return result;

        void PreOrderWalk(AaTreeNode<TKey>? node)
        {
            if (node is null)
            {
                return;
            }

            result.Add(node.Key);
            PreOrderWalk(node.Left);
            PreOrderWalk(node.Right);
        }
    }

    /// <summary>
    ///     Gets all the elements in the tree in post-order order.
    /// </summary>
    /// <returns>Sequence of elements in post-order order.</returns>
    public IEnumerable<TKey> GetKeysPostOrder()
    {
		_logger.LogDebug(">>> Entering GetKeysPostOrder()");
        var result = new List<TKey>();
        PostOrderWalk(Root);
		_logger.LogDebug("GetKeysPostOrder(): result → {Result}", result);
		_logger.LogDebug("<<< Exiting GetKeysPostOrder()");
        return result;

        void PostOrderWalk(AaTreeNode<TKey>? node)
        {
            if (node is null)
            {
                return;
            }

            PostOrderWalk(node.Left);
            PostOrderWalk(node.Right);
            result.Add(node.Key);
        }
    }

    /// <summary>
    ///     Recursive function to add an element to the tree.
    /// </summary>
    /// <param name="key">The element to add.</param>
    /// <param name="node">The node to search for a empty spot.</param>
    /// <returns>The node with the added element.</returns>
    /// <exception cref="ArgumentException">Thrown if key is already in the tree.</exception>
    private AaTreeNode<TKey> Add(TKey key, AaTreeNode<TKey>? node)
    {
		_logger.LogDebug(">>> Entering Add(key={Key},node={Node})", key, node);
        if (node is null)
        {
            return new AaTreeNode<TKey>(key, 1);
        }

        if (comparer.Compare(key, node.Key) < 0)
        {
            node.Left = Add(key, node.Left);
        }
        else if (comparer.Compare(key, node.Key) > 0)
        {
            node.Right = Add(key, node.Right);
        }
        else
        {
			_logger.LogWarning("Throwing ArgumentException from Add(key={Key},node={Node})", key, node);
            throw new ArgumentException($"""Key "{key}" already in tree!""", nameof(key));
        }

		_logger.LogDebug("<<< Exiting Add(key={Key},node={Node})", key, node);
        return Split(Skew(node))!;
    }

    /// <summary>
    ///     Recursive function to remove an element from the tree.
    /// </summary>
    /// <param name="key">The element to remove.</param>
    /// <param name="node">The node to search from.</param>
    /// <returns>The node with the specified element removed.</returns>
    private AaTreeNode<TKey>? Remove(TKey key, AaTreeNode<TKey>? node)
    {
		_logger.LogDebug(">>> Entering Remove(key={Key},node={Node})", key, node);
        if (node is null)
        {
            return null;
        }

        if (comparer.Compare(key, node.Key) < 0)
        {
            node.Left = Remove(key, node.Left);
        }
        else if (comparer.Compare(key, node.Key) > 0)
        {
            node.Right = Remove(key, node.Right);
        }
        else
        {
            if (node.Left is null && node.Right is null)
            {
                return null;
            }

            if (node.Left is null)
            {
                var successor = GetMin(node.Right!);
				_logger.LogDebug("Remove(key={Key},node={Node}): successor → {Successor}", key, node, successor);
                node.Right = Remove(successor.Key, node.Right);
                node.Key = successor.Key;
				_logger.LogDebug("Remove(key={Key},node={Node}): node.Key → {Arg2}", key, node, node.Key);
            }
            else
            {
                var predecessor = GetMax(node.Left);
				_logger.LogDebug("Remove(key={Key},node={Node}): predecessor → {Predecessor}", key, node, predecessor);
                node.Left = Remove(predecessor.Key, node.Left);
                node.Key = predecessor.Key;
            }
        }

        node = DecreaseLevel(node);
		_logger.LogDebug("Remove(key={Key},node={Node}): node → {Node1}", key, node, node);
        node = Skew(node);
        node!.Right = Skew(node.Right);

        if (node.Right is not null)
        {
            node.Right.Right = Skew(node.Right.Right);
        }

        node = Split(node);
        node!.Right = Split(node.Right);
		_logger.LogDebug("<<< Exiting Remove(key={Key},node={Node})", key, node);
        return node;
    }

    /// <summary>
    ///     Recursive function to check if the element exists in the tree.
    /// </summary>
    /// <param name="key">The element to check for.</param>
    /// <param name="node">The node to search from.</param>
    /// <returns>true if the element exists in the tree, false otherwise.</returns>
    private bool Contains(TKey key, AaTreeNode<TKey>? node) =>
        node is { }
        && comparer.Compare(key, node.Key) is { } v
        && v switch
        {
            { } when v > 0 => Contains(key, node.Right),
            { } when v < 0 => Contains(key, node.Left),
            _ => true,
        };

    /// <summary>
    ///     Recursive to find the maximum/right-most element.
    /// </summary>
    /// <param name="node">The node to traverse from.</param>
    /// <returns>The node with the maximum/right-most element.</returns>
    private AaTreeNode<TKey> GetMax(AaTreeNode<TKey> node)
    {
        while (true)
        {
            if (node.Right is null)
            {
                return node;
            }

            node = node.Right;
        }
    }

    /// <summary>
    ///     Recursive to find the minimum/left-most element.
    /// </summary>
    /// <param name="node">The node to traverse from.</param>
    /// <returns>The node with the minimum/left-most element.</returns>
    private AaTreeNode<TKey> GetMin(AaTreeNode<TKey> node)
    {
        while (true)
        {
            if (node.Left is null)
            {
                return node;
            }

            node = node.Left;
        }
    }

    /// <summary>
    ///     Remove right-horizontal links and replaces them with left-horizontal links.
    ///     Accomplishes this by performing a right rotation.
    /// </summary>
    /// <param name="node">The node to rebalance from.</param>
    /// <returns>The rebalanced node.</returns>
    private AaTreeNode<TKey>? Skew(AaTreeNode<TKey>? node)
    {
		_logger.LogDebug(">>> Entering Skew(node={Node})", node);
        if (node?.Left is null || node.Left.Level != node.Level)
        {
            return node;
        }

        var left = node.Left;
        node.Left = left.Right;
		_logger.LogDebug("Skew(node={Node}): node.Left → {Arg1}", node, node.Left);
		_logger.LogDebug("Skew(node={Node}): left.Right → {Arg1}", node, left.Right);
        left.Right = node;
		_logger.LogDebug("<<< Exiting Skew(node={Node})", node);
        return left;
    }

    /// <summary>
    ///     Reduces the number of right-horizontal links.
    ///     Accomplishes this by performing a left rotation, and incrementing level.
    /// </summary>
    /// <param name="node">The node to rebalance from.</param>
    /// <returns>The rebalanced node.</returns>
    private AaTreeNode<TKey>? Split(AaTreeNode<TKey>? node)
    {
		_logger.LogDebug(">>> Entering Split(node={Node})", node);
        if (node?.Right?.Right is null || node.Level != node.Right.Right.Level)
        {
            return node;
        }

        var right = node.Right;
        node.Right = right.Left;
		_logger.LogDebug("Split(node={Node}): node.Right → {Arg1}", node, node.Right);
		_logger.LogDebug("Split(node={Node}): right.Left → {Arg1}", node, right.Left);
        right.Left = node;
        right.Level++;
		_logger.LogDebug("<<< Exiting Split(node={Node})", node);
        return right;
    }

    /// <summary>
    ///     Decreases the level of node if necessary.
    /// </summary>
    /// <param name="node">The node to decrease level from.</param>
    /// <returns>The node with modified level.</returns>
    private AaTreeNode<TKey> DecreaseLevel(AaTreeNode<TKey> node)
    {
		_logger.LogDebug(">>> Entering DecreaseLevel(node={Node})", node);
        var newLevel = Math.Min(GetLevel(node.Left), GetLevel(node.Right)) + 1;
        if (newLevel >= node.Level)
		_logger.LogDebug("DecreaseLevel(node={Node}): newLevel → {NewLevel}", node, newLevel);
        {
            return node;
        }

        node.Level = newLevel;
		_logger.LogDebug("DecreaseLevel(node={Node}): node.Level → {Arg1}", node, node.Level);
        if (node.Right is { } && newLevel < node.Right.Level)
        {
            node.Right.Level = newLevel;
        }

		_logger.LogDebug("<<< Exiting DecreaseLevel(node={Node})", node);
        return node;

        static int GetLevel(AaTreeNode<TKey>? x) => x?.Level ?? 0;
    }
}
