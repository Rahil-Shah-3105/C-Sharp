using Microsoft.Extensions.Logging;
namespace DataStructures.AVLTree;

/// <summary>
///     A simple self-balancing binary tree.
/// </summary>
/// <remarks>
///     An AVL tree is a self-balancing binary search tree (BST) named after
///     its inventors: Adelson, Velsky, and Landis. It is the first self-
///     balancing BST invented. The primary property of an AVL tree is that
///     the height of both child subtrees for any node only differ by one.
///     Due to the balanced nature of the tree, its time complexities for
///     insertion, deletion, and search all have a worst-case time
///     complexity of O(log n). Which is an improvement over the worst-case
///     O(n) for a regular BST.
///     See https://en.wikipedia.org/wiki/AVL_tree for more information.
///     Visualizer: https://visualgo.net/en/bst.
/// </remarks>
/// <typeparam name="TKey">Type of key for the tree.</typeparam>
public class AvlTree<TKey>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Gets the number of nodes in the tree.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    ///     Comparer to use when comparing key values.
    /// </summary>
    private readonly Comparer<TKey> comparer;

    /// <summary>
    ///     Reference to the root node.
    /// </summary>
    private AvlTreeNode<TKey>? root;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AvlTree{TKey}"/>
    ///     class.
    /// </summary>
    public AvlTree()
    {
		_logger.LogDebug(">>> Entering AvlTree()");
        comparer = Comparer<TKey>.Default;
		_logger.LogDebug("AvlTree(): comparer → {Comparer}", comparer);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="AvlTree{TKey}"/>
    ///     class using the specified comparer.
    /// </summary>
    /// <param name="customComparer">
    /// Comparer to use when comparing keys.
    /// </param>
    public AvlTree(Comparer<TKey> customComparer)
    {
		_logger.LogDebug(">>> Entering AvlTree(customComparer={CustomComparer})", customComparer);
        comparer = customComparer;
		_logger.LogDebug("AvlTree(customComparer={CustomComparer}): comparer → {Comparer}", customComparer, comparer);
    }

    /// <summary>
    ///     Add a single node to the tree.
    /// </summary>
    /// <param name="key">Key value to add.</param>
    public void Add(TKey key)
    {
		_logger.LogDebug(">>> Entering Add(key={Key})", key);
        if (root is null)
        {
            root = new AvlTreeNode<TKey>(key);
        }
        else
        {
            root = Add(root, key);
        }

        Count++;
		_logger.LogDebug("<<< Exiting Add(key={Key})", key);
    }

    /// <summary>
    ///     Add multiple nodes to the tree.
    /// </summary>
    /// <param name="keys">Key values to add.</param>
    public void AddRange(IEnumerable<TKey> keys)
    {
		_logger.LogDebug(">>> Entering AddRange(keys={Keys})", keys);
        foreach (var key in keys)
        {
            Add(key);
        }
		_logger.LogDebug("<<< Exiting AddRange(keys={Keys})", keys);
    }

    /// <summary>
    ///     Remove a node from the tree.
    /// </summary>
    /// <param name="key">Key value to remove.</param>
    public void Remove(TKey key)
    {
		_logger.LogDebug(">>> Entering Remove(key={Key})", key);
        root = Remove(root, key);
        Count--;
		_logger.LogWarning("Remove(key={Key}): root → {Root}", key, root);
		_logger.LogDebug("<<< Exiting Remove(node={Node},key={Key})", node, key);
    }

    /// <summary>
    ///     Check if given node is in the tree.
    /// </summary>
    /// <param name="key">Key value to search for.</param>
    /// <returns>Whether or not the node is in the tree.</returns>
    public bool Contains(TKey key)
    {
		_logger.LogDebug(">>> Entering Contains(key={Key})", key);
        var node = root;
        while (node is not null)
        {
            var compareResult = comparer.Compare(key, node.Key);
			_logger.LogDebug("Contains(key={Key}): compareResult → {CompareResult}", key, compareResult);
            if (compareResult < 0)
            {
                node = node.Left;
            }
            else if (compareResult > 0)
            {
                node = node.Right;
            }
            else
            {
                return true;
            }
        }

		_logger.LogDebug("<<< Exiting Contains(key={Key})", key);
        return false;
    }

    /// <summary>
    ///     Get the minimum value in the tree.
    /// </summary>
    /// <returns>Minimum value in tree.</returns>
    public TKey GetMin()
    {
		_logger.LogDebug(">>> Entering GetMin()");
        if (root is null)
        {
			_logger.LogWarning("Throwing InvalidOperationException from GetMin()");
		_logger.LogDebug("<<< Exiting GetMin()");
            throw new InvalidOperationException("AVL tree is empty.");
        }

		_logger.LogDebug("<<< Exiting GetMin(node={Node})", node);
        return GetMin(root).Key;
    }

    /// <summary>
    ///     Get the maximum value in the tree.
    /// </summary>
    /// <returns>Maximum value in tree.</returns>
    public TKey GetMax()
    {
		_logger.LogDebug(">>> Entering GetMax()");
        if (root is null)
        {
			_logger.LogWarning("Throwing InvalidOperationException from GetMax()");
		_logger.LogDebug("<<< Exiting GetMax()");
            throw new InvalidOperationException("AVL tree is empty.");
        }

		_logger.LogDebug("<<< Exiting GetMax(node={Node})", node);
        return GetMax(root).Key;
    }

    /// <summary>
    ///     Get keys in order from smallest to largest as defined by the
    ///     comparer.
    /// </summary>
    /// <returns>Keys in tree in order from smallest to largest.</returns>
    public IEnumerable<TKey> GetKeysInOrder()
    {
		_logger.LogDebug(">>> Entering GetKeysInOrder()");
        List<TKey> result = [];
        InOrderWalk(root);
		_logger.LogDebug("GetKeysInOrder(): result → {Result}", result);
		_logger.LogDebug("<<< Exiting GetKeysInOrder()");
        return result;

        void InOrderWalk(AvlTreeNode<TKey>? node)
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
    ///     Get keys in the pre-order order.
    /// </summary>
    /// <returns>Keys in pre-order order.</returns>
    public IEnumerable<TKey> GetKeysPreOrder()
    {
		_logger.LogDebug(">>> Entering GetKeysPreOrder()");
        var result = new List<TKey>();
        PreOrderWalk(root);
		_logger.LogDebug("GetKeysPreOrder(): result → {Result}", result);
		_logger.LogDebug("<<< Exiting GetKeysPreOrder()");
        return result;

        void PreOrderWalk(AvlTreeNode<TKey>? node)
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
    ///     Get keys in the post-order order.
    /// </summary>
    /// <returns>Keys in the post-order order.</returns>
    public IEnumerable<TKey> GetKeysPostOrder()
    {
		_logger.LogDebug(">>> Entering GetKeysPostOrder()");
        var result = new List<TKey>();
        PostOrderWalk(root);
		_logger.LogDebug("GetKeysPostOrder(): result → {Result}", result);
		_logger.LogDebug("<<< Exiting GetKeysPostOrder()");
        return result;

        void PostOrderWalk(AvlTreeNode<TKey>? node)
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
    ///     Helper function to rebalance the tree so that all nodes have a
    ///     balance factor in the range [-1, 1].
    /// </summary>
    /// <param name="node">Node to rebalance.</param>
    /// <returns>New node that has been rebalanced.</returns>
    private static AvlTreeNode<TKey> Rebalance(AvlTreeNode<TKey> node)
    {
		_logger.LogDebug(">>> Entering Rebalance(node={Node})", node);
        if (node.BalanceFactor > 1)
        {
            if (node.Right!.BalanceFactor == -1)
            {
                node.Right = RotateRight(node.Right);
            }

            return RotateLeft(node);
        }

        if (node.BalanceFactor < -1)
        {
            if (node.Left!.BalanceFactor == 1)
            {
                node.Left = RotateLeft(node.Left);
            }

		_logger.LogDebug("<<< Exiting Rebalance(node={Node})", node);
            return RotateRight(node);
        }

        return node;
    }

    /// <summary>
    ///     Perform a left (counter-clockwise) rotation.
    /// </summary>
    /// <param name="node">Node to rotate about.</param>
    /// <returns>New node with rotation applied.</returns>
    private static AvlTreeNode<TKey> RotateLeft(AvlTreeNode<TKey> node)
    {
		_logger.LogDebug(">>> Entering RotateLeft(node={Node})", node);
        var temp1 = node;
        var temp2 = node.Right!.Left;
        node = node.Right;
        node.Left = temp1;
		_logger.LogDebug("RotateLeft(node={Node}): node.Left → {Arg1}", node, node.Left);
        node.Left.Right = temp2;

        node.Left.UpdateBalanceFactor();
        node.UpdateBalanceFactor();

		_logger.LogDebug("<<< Exiting RotateLeft(node={Node})", node);
        return node;
    }

    /// <summary>
    ///     Perform a right (clockwise) rotation.
    /// </summary>
    /// <param name="node">Node to rotate about.</param>
    /// <returns>New node with rotation applied.</returns>
    private static AvlTreeNode<TKey> RotateRight(AvlTreeNode<TKey> node)
    {
		_logger.LogDebug(">>> Entering RotateRight(node={Node})", node);
        var temp1 = node;
        var temp2 = node.Left!.Right;
        node = node.Left;
        node.Right = temp1;
		_logger.LogDebug("RotateRight(node={Node}): node.Right → {Arg1}", node, node.Right);
        node.Right.Left = temp2;

        node.Right.UpdateBalanceFactor();
        node.UpdateBalanceFactor();

		_logger.LogDebug("<<< Exiting RotateRight(node={Node})", node);
        return node;
    }

    /// <summary>
    ///     Helper function to get node instance with minimum key value
    ///     in the specified subtree.
    /// </summary>
    /// <param name="node">Node specifying root of subtree.</param>
    /// <returns>Minimum value in node's subtree.</returns>
    private static AvlTreeNode<TKey> GetMin(AvlTreeNode<TKey> node)
    {
        while (node.Left is not null)
        {
            node = node.Left;
        }

        return node;
    }

    /// <summary>
    ///     Helper function to get node instance with maximum key value
    ///     in the specified subtree.
    /// </summary>
    /// <param name="node">Node specifying root of subtree.</param>
    /// <returns>Maximum value in node's subtree.</returns>
    private static AvlTreeNode<TKey> GetMax(AvlTreeNode<TKey> node)
    {
        while (node.Right is not null)
        {
            node = node.Right;
        }

        return node;
    }

    /// <summary>
    ///     Recursively function to add a node to the tree.
    /// </summary>
    /// <param name="node">Node to check for null leaf.</param>
    /// <param name="key">Key value to add.</param>
    /// <returns>New node with key inserted.</returns>
    private AvlTreeNode<TKey> Add(AvlTreeNode<TKey> node, TKey key)
    {
		_logger.LogDebug(">>> Entering Add(node={Node},key={Key})", node, key);
        // Regular binary search tree insertion
        var compareResult = comparer.Compare(key, node.Key);
		_logger.LogDebug("Add(node={Node},key={Key}): compareResult → {CompareResult}", node, key, compareResult);
        if (compareResult < 0)
        {
            if (node.Left is null)
            {
                var newNode = new AvlTreeNode<TKey>(key);
				_logger.LogDebug("Add(node={Node},key={Key}): newNode → {NewNode}", node, key, newNode);
				_logger.LogDebug("Add(node={Node},key={Key}): node.Left → {Arg2}", node, key, node.Left);
                node.Left = newNode;
            }
            else
            {
                node.Left = Add(node.Left, key);
            }
        }
        else if (compareResult > 0)
        {
            if (node.Right is null)
            {
                var newNode = new AvlTreeNode<TKey>(key);
                node.Right = newNode;
				_logger.LogDebug("Add(node={Node},key={Key}): node.Right → {Arg2}", node, key, node.Right);
            }
            else
            {
                node.Right = Add(node.Right, key);
            }
        }
        else
        {
			_logger.LogWarning("Throwing ArgumentException from Add(node={Node},key={Key})", node, key);
            throw new ArgumentException($"""Key "{key}" already exists in AVL tree.""");
        }

        // Check all of the new node's ancestors for imbalance and perform
        // necessary rotations
        node.UpdateBalanceFactor();

		_logger.LogDebug("<<< Exiting Add(node={Node},key={Key})", node, key);
        return Rebalance(node);
    }

    /// <summary>
    ///     Recursive function to remove node from tree.
    /// </summary>
    /// <param name="node">Node to check for key.</param>
    /// <param name="key">Key value to remove.</param>
    /// <returns>New node with key removed.</returns>
    private AvlTreeNode<TKey>? Remove(AvlTreeNode<TKey>? node, TKey key)
    {
		_logger.LogDebug(">>> Entering Remove(node={Node},key={Key})", node, key);
        if (node == null)
        {
			_logger.LogWarning("Throwing KeyNotFoundException from Remove(node={Node},key={Key})", node, key);
            throw new KeyNotFoundException(
                $"""Key "{key}" is not in the AVL tree.""");
        }

        // Normal binary search tree removal
        var compareResult = comparer.Compare(key, node.Key);
		_logger.LogDebug("Remove(node={Node},key={Key}): compareResult → {CompareResult}", node, key, compareResult);
        if (compareResult < 0)
        {
            node.Left = Remove(node.Left, key);
        }
        else if (compareResult > 0)
        {
            node.Right = Remove(node.Right, key);
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
				_logger.LogDebug("Remove(node={Node},key={Key}): successor → {Successor}", node, key, successor);
                node.Right = Remove(node.Right!, successor.Key);
                node.Key = successor.Key;
				_logger.LogDebug("Remove(node={Node},key={Key}): node.Key → {Arg2}", node, key, node.Key);
            }
            else
            {
                var predecessor = GetMax(node.Left!);
				_logger.LogDebug("Remove(node={Node},key={Key}): predecessor → {Predecessor}", node, key, predecessor);
                node.Left = Remove(node.Left!, predecessor.Key);
                node.Key = predecessor.Key;
            }
        }

        // Check all of the removed node's ancestors for rebalance and
        // perform necessary rotations.
        node.UpdateBalanceFactor();

        return Rebalance(node);
    }
}
