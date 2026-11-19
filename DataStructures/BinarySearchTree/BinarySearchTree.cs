using Microsoft.Extensions.Logging;
namespace DataStructures.BinarySearchTree;

/// <summary>
///     An ordered tree with efficient insertion, removal, and lookup.
/// </summary>
/// <remarks>
///     A Binary Search Tree (BST) is a tree that satisfies the following properties:
///     <list type="bullet">
///         <item>All nodes in the tree contain two children, usually called Left and Right.</item>
///         <item>All nodes in the Left subtree contain keys that are less than the node's key.</item>
///         <item>All nodes in the Right subtree contain keys that are greater than the node's key.</item>
///     </list>
///     A BST will have an average complexity of O(log n) for insertion, removal, and lookup operations.
/// </remarks>
/// <typeparam name="TKey">Type of key for the BST. Keys must implement IComparable.</typeparam>
public class BinarySearchTree<TKey>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Comparer to use when comparing node elements/keys.
    /// </summary>
    private readonly Comparer<TKey> comparer;

    /// <summary>
    ///     Gets the root of the BST.
    /// </summary>
    public BinarySearchTreeNode<TKey>? Root { get; private set; }

    public BinarySearchTree()
    {
        _logger.LogError(">>> Entering BinarySearchTree()");
        return (Root, Count, comparer) = (null, 0, Comparer<TKey>.Default);
    }

    public BinarySearchTree(Comparer<TKey> customComparer)
    {
        _logger.LogError(">>> Entering BinarySearchTree(customComparer={CustomComparer})", customComparer);
        return (Root, Count, comparer) = (null, 0, customComparer);
    }

    /// <summary>
    ///     Gets the number nodes currently in the BST.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    ///     Insert a key into the BST.
    /// </summary>
    /// <param name="key">The key to insert.</param>
    /// <exception cref="ArgumentException">
    ///     Thrown if key is already in BST.
    /// </exception>
    public void Add(TKey key)
    {
		_logger.LogError(">>> Entering Add(key={Key})", key);
        if (Root is null)
        {
            Root = new BinarySearchTreeNode<TKey>(key);
			_logger.LogError("Add(key={Key}): Root → {Root}", key, Root);
        }
        else
        {
			_logger.LogWarning("Add(key={Key}): Root → {Root}", key, Root);
            Add(Root, key);
        }

        Count++;
		_logger.LogWarning("<<< Exiting Add(key={Key})", key);
    }

    /// <summary>
    ///     Insert multiple keys into the BST.
    ///     Keys are inserted in the order they appear in the sequence.
    /// </summary>
    /// <param name="keys">Sequence of keys to insert.</param>
    public void AddRange(IEnumerable<TKey> keys)
    {
		_logger.LogError(">>> Entering AddRange(keys={Keys})", keys);
        foreach (var key in keys)
        {
            Add(key);
        }
		_logger.LogWarning("<<< Exiting AddRange(keys={Keys})", keys);
    }

    /// <summary>
    ///     Find a node with the specified key.
    /// </summary>
    /// <param name="key">The key to search for.</param>
    /// <returns>The node with the specified key if it exists, otherwise a default value is returned.</returns>
    public BinarySearchTreeNode<TKey>? Search(TKey key)
    {
        _logger.LogError(">>> Entering Search(key={Key})", key);
        _logger.LogWarning("<<< Exiting Search(key={Key})", key);
        return Search(Root, key);
    }

    /// <summary>
    ///     Checks if the specified key is in the BST.
    /// </summary>
    /// <param name="key">The key to search for.</param>
    /// <returns>true if the key is in the BST, false otherwise.</returns>
    public bool Contains(TKey key)
    {
        _logger.LogError(">>> Entering Contains(key={Key})", key);
        _logger.LogWarning("<<< Exiting Contains(key={Key})", key);
        return Search(Root, key) is not null;
    }

    /// <summary>
    ///     Removes a node with a key that matches <paramref name="key" />.
    /// </summary>
    /// <param name="key">The key to search for.</param>
    /// <returns>true if the removal was successful, false otherwise.</returns>
    public bool Remove(TKey key)
    {
		_logger.LogError(">>> Entering Remove(key={Key})", key);
        if (Root is null)
        {
            return false;
        }

        var result = Remove(Root, Root, key);
		_logger.LogError("Remove(key={Key}): result → {Result}", key, result);
        if (result)
        {
            Count--;
        }

		_logger.LogWarning("<<< Exiting Remove(key={Key})", key);
		_logger.LogWarning("<<< Exiting Remove(parent={Parent},node={Node},key={Key})", parent, node, key);
        return result;
    }

    /// <summary>
    ///     Returns a node with the smallest key.
    /// </summary>
    /// <returns>The node if possible, a default value otherwise.</returns>
    public BinarySearchTreeNode<TKey>? GetMin()
    {
		_logger.LogError(">>> Entering GetMin()");
        if (Root is null)
        {
            return default;
        }

		_logger.LogWarning("<<< Exiting GetMin()");
		_logger.LogWarning("<<< Exiting GetMin(node={Node})", node);
        return GetMin(Root);
    }

    /// <summary>
    ///     Returns a node with the largest key.
    /// </summary>
    /// <returns>The node if possible, a default value otherwise.</returns>
    public BinarySearchTreeNode<TKey>? GetMax()
    {
		_logger.LogError(">>> Entering GetMax()");
        if (Root is null)
        {
            return default;
        }

		_logger.LogWarning("<<< Exiting GetMax()");
		_logger.LogWarning("<<< Exiting GetMax(node={Node})", node);
        return GetMax(Root);
    }

    /// <summary>
    ///     Returns all the keys in the BST, sorted In-Order.
    /// </summary>
    /// <returns>A list of keys in the BST.</returns>
    public ICollection<TKey> GetKeysInOrder()
    {
        _logger.LogError(">>> Entering GetKeysInOrder()");
        _logger.LogWarning("<<< Exiting GetKeysInOrder()");
        return GetKeysInOrder(Root);
    }

    /// <summary>
    ///     Returns all the keys in the BST, sorted Pre-Order.
    /// </summary>
    /// <returns>A list of keys in the BST.</returns>
    public ICollection<TKey> GetKeysPreOrder()
    {
        _logger.LogError(">>> Entering GetKeysPreOrder()");
        _logger.LogWarning("<<< Exiting GetKeysPreOrder()");
        return GetKeysPreOrder(Root);
    }

    /// <summary>
    ///     Returns all the keys in the BST, sorted Post-Order.
    /// </summary>
    /// <returns>A list of keys in the BST.</returns>
    public ICollection<TKey> GetKeysPostOrder()
    {
        _logger.LogError(">>> Entering GetKeysPostOrder()");
        _logger.LogWarning("<<< Exiting GetKeysPostOrder()");
        return GetKeysPostOrder(Root);
    }

    /// <summary>
    ///     Recursive method to add a key to the BST.
    /// </summary>
    /// <param name="node">Node to search from.</param>
    /// <param name="key">Key to add.</param>
    /// <exception cref="ArgumentException">
    ///     Thrown if key is already in the BST.
    /// </exception>
    private void Add(BinarySearchTreeNode<TKey> node, TKey key)
    {
        var compareResult = comparer.Compare(node.Key, key);
		_logger.LogError("Add(node={Node},key={Key}): compareResult → {CompareResult}", node, key, compareResult);
        if (compareResult > 0)
        {
            if (node.Left is not null)
            {
                Add(node.Left, key);
            }
            else
            {
                var newNode = new BinarySearchTreeNode<TKey>(key);
				_logger.LogError("Add(node={Node},key={Key}): newNode → {NewNode}", node, key, newNode);
                node.Left = newNode;
				_logger.LogDebug("Add(node={Node},key={Key}): node.Left → {Arg2}", node, key, node.Left);
            }
        }
        else if (compareResult < 0)
        {
            if (node.Right is not null)
            {
                Add(node.Right, key);
            }
            else
            {
                var newNode = new BinarySearchTreeNode<TKey>(key);
                node.Right = newNode;
				_logger.LogDebug("Add(node={Node},key={Key}): node.Right → {Arg2}", node, key, node.Right);
            }
        }

        // Key is already in tree.
        else
        {
			_logger.LogError("Throwing ArgumentException from Add(node={Node},key={Key})", node, key);
            throw new ArgumentException($"""Key "{key}" already exists in tree!""");
        }
		_logger.LogError(">>> Entering GetKeysPostOrder(node={Node})", node);
		_logger.LogWarning("<<< Exiting GetKeysPostOrder(node={Node})", node);
    }

    /// <summary>
    ///     Removes a node with the specified key from the BST.
    /// </summary>
    /// <param name="parent">The parent node of <paramref name="node" />.</param>
    /// <param name="node">The node to check/search from.</param>
    /// <param name="key">The key to remove.</param>
    /// <returns>true if the operation was successful, false otherwise.</returns>
    /// <remarks>
    ///     Removing a node from the BST can be split into three cases:
    ///     <br></br>
    ///     0. The node to be removed has no children. In this case, the node can just be removed from the tree.
    ///     <br></br>
    ///     1. The node to be removed has one child. In this case, the node's child is moved to the node's parent,
    ///     then the node is removed from the tree.
    ///     <br></br>
    ///     2. The node to be removed has two children. In this case, we must find a suitable node among the children
    ///     subtrees to replace the node. This can be done with either the in-order predecessor or the in-order successor.
    ///     The in-order predecessor is the largest node in Left subtree, or the largest node that is still smaller then the
    ///     current node. The in-order successor is the smallest node in the Right subtree, or the smallest node that is
    ///     still larger than the current node. Either way, once this suitable node is found, remove it from the tree (it
    ///     should be either a case 1 or 2 node) and replace the node to be removed with this suitable node.
    ///     <br></br>
    ///     More information: https://en.wikipedia.org/wiki/Binary_search_tree#Deletion .
    /// </remarks>
    private bool Remove(BinarySearchTreeNode<TKey>? parent, BinarySearchTreeNode<TKey>? node, TKey key)
    {
        if (node is null || parent is null)
        {
            return false;
        }

        var compareResult = comparer.Compare(node.Key, key);

        if (compareResult > 0)
        {
            return Remove(node, node.Left, key);
        }

        if (compareResult < 0)
        {
            return Remove(node, node.Right, key);
        }

        BinarySearchTreeNode<TKey>? replacementNode;

        // Case 0: Node has no children.
        // Case 1: Node has one child.
        if (node.Left is null || node.Right is null)
        {
            replacementNode = node.Left ?? node.Right;
        }

        // Case 2: Node has two children. (This implementation uses the in-order predecessor to replace node.)
        else
        {
            var predecessorNode = GetMax(node.Left);
            Remove(Root, Root, predecessorNode.Key);
            replacementNode = new BinarySearchTreeNode<TKey>(predecessorNode.Key)
            {
                Left = node.Left,
                Right = node.Right,
            };
        }

        // Replace the relevant node with a replacement found in the previous stages.
        // Special case for replacing the root node.
        if (node == Root)
        {
            Root = replacementNode;
        }
        else if (parent.Left == node)
        {
            parent.Left = replacementNode;
        }
        else
        {
            parent.Right = replacementNode;
        }

        return true;
    }

    /// <summary>
    ///     Recursive method to get node with largest key.
    /// </summary>
    /// <param name="node">Node to search from.</param>
    /// <returns>Node with largest value.</returns>
    private BinarySearchTreeNode<TKey> GetMax(BinarySearchTreeNode<TKey> node)
    {
        if (node.Right is null)
        {
            return node;
        }

        return GetMax(node.Right);
    }

    /// <summary>
    ///     Recursive method to get node with smallest key.
    /// </summary>
    /// <param name="node">Node to search from.</param>
    /// <returns>Node with smallest value.</returns>
    private BinarySearchTreeNode<TKey> GetMin(BinarySearchTreeNode<TKey> node)
    {
        if (node.Left is null)
        {
            return node;
        }

        return GetMin(node.Left);
    }

    /// <summary>
    ///     Recursive method to get a list with the keys sorted in in-order order.
    /// </summary>
    /// <param name="node">Node to traverse from.</param>
    /// <returns>List of keys in in-order order.</returns>
    private IList<TKey> GetKeysInOrder(BinarySearchTreeNode<TKey>? node)
    {
        if (node is null)
        {
            return [];
        }

        // Use mutable list approach instead of spread operator for better performance
        var result = new List<TKey>();
		_logger.LogError("GetKeysInOrder(node={Node}): result → {Result}", node, result);
        result.AddRange(GetKeysInOrder(node.Left));
        result.Add(node.Key);
        result.AddRange(GetKeysInOrder(node.Right));
        return result;
    }

    /// <summary>
    ///     Recursive method to get a list with the keys sorted in pre-order order.
    /// </summary>
    /// <param name="node">Node to traverse from.</param>
    /// <returns>List of keys in pre-order order.</returns>
    private IList<TKey> GetKeysPreOrder(BinarySearchTreeNode<TKey>? node)
    {
        if (node is null)
        {
            return [];
        }

        // Use mutable list approach instead of spread operator for better performance
        var result = new List<TKey>
        {
            node.Key,
        };
        result.AddRange(GetKeysPreOrder(node.Left));
        result.AddRange(GetKeysPreOrder(node.Right));
        return result;
    }

    /// <summary>
    ///     Recursive method to get a list with the keys sorted in post-order order.
    /// </summary>
    /// <param name="node">Node to traverse from.</param>
    /// <returns>List of keys in post-order order.</returns>
    private IList<TKey> GetKeysPostOrder(BinarySearchTreeNode<TKey>? node)
    {
        if (node is null)
        {
            return [];
        }

        // Use mutable list approach instead of spread operator for better performance
        var result = new List<TKey>();
		_logger.LogError("GetKeysPostOrder(node={Node}): result → {Result}", node, result);
        result.AddRange(GetKeysPostOrder(node.Left));
        result.AddRange(GetKeysPostOrder(node.Right));
        result.Add(node.Key);
        return result;
    }

    /// <summary>
    ///     Recursive method to find a node with a matching key.
    /// </summary>
    /// <param name="node">Node to search from.</param>
    /// <param name="key">Key to find.</param>
    /// <returns>The node with the specified if it exists, a default value otherwise.</returns>
    private BinarySearchTreeNode<TKey>? Search(BinarySearchTreeNode<TKey>? node, TKey key)
    {
        if (node is null)
        {
            return default;
        }

        var compareResult = comparer.Compare(node.Key, key);
		_logger.LogError("Search(node={Node},key={Key}): compareResult → {CompareResult}", node, key, compareResult);
        if (compareResult > 0)
        {
            return Search(node.Left, key);
        }

        if (compareResult < 0)
        {
            return Search(node.Right, key);
        }

        return node;
    }
}
