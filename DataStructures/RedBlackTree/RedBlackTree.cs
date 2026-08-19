using Microsoft.Extensions.Logging;
namespace DataStructures.RedBlackTree;

/// <summary>
///     A self-balancing bindary tree.
/// </summary>
/// <remarks>
///     A red-black tree is a self-balancing binary search tree (BST) that
///     stores a color with each node. A node's color can either be red or
///     black. Several properties are maintained to ensure the tree remains
///     balanced.
///     <list type="number">
///         <item>
///             <term>A red node does not have a red child.</term>
///         </item>
///         <item>
///             <term>All null nodes are considered black.</term>
///         </item>
///         <item>
///             <term>
///                 Every path from a node to its descendant leaf nodes
///             has the same number of black nodes.
///             </term>
///         </item>
///         <item>
///             <term>(Optional) The root is always black.</term>
///         </item>
///     </list>
///     Red-black trees are generally slightly more unbalanced than an
///     AVL tree, but insertion and deletion is generally faster.
///     See https://en.wikipedia.org/wiki/Red%E2%80%93black_tree for more information.
/// </remarks>
/// <typeparam name="TKey">Type of key for the tree.</typeparam>
public class RedBlackTree<TKey>
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
    private RedBlackTreeNode<TKey>? root;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RedBlackTree{TKey}"/> class.
    /// </summary>
    public RedBlackTree()
    {
		_logger.LogDebug(">>> Entering RedBlackTree()");
        comparer = Comparer<TKey>.Default;
		_logger.LogDebug("RedBlackTree(): comparer → {Comparer}", comparer);
		_logger.LogDebug("<<< Exiting RedBlackTree()");
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="RedBlackTree{TKey}"/> class
    ///     using the specified comparer.
    /// </summary>
    /// <param name="customComparer">Comparer to use when comparing keys.</param>
    public RedBlackTree(Comparer<TKey> customComparer)
    {
		_logger.LogDebug(">>> Entering RedBlackTree(customComparer={CustomComparer})", customComparer);
        comparer = customComparer;
		_logger.LogDebug("RedBlackTree(customComparer={CustomComparer}): comparer → {Comparer}", customComparer, comparer);
		_logger.LogDebug("<<< Exiting RedBlackTree(customComparer={CustomComparer})", customComparer);
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
            // Case 3
            // New node is root
            root = new RedBlackTreeNode<TKey>(key, null)
            {
			_logger.LogWarning("Add(key={Key}): root → {Root}", key, root);
                Color = NodeColor.Black,
            };
            Count++;
            return;
        }

        // Regular binary tree insertion
        var node = Add(root, key);
		_logger.LogDebug("Add(key={Key}): node → {Node}", key, node);

        // Get which side child was added to
        var childDir = comparer.Compare(node.Key, node.Parent!.Key);
		_logger.LogDebug("Add(key={Key}): childDir → {ChildDir}", key, childDir);

        // Set node to be new node's parent for easier handling
        node = node.Parent;

        // Return tree to valid state
        int addCase;
        do
        {
            addCase = GetAddCase(node);

			_logger.LogDebug("Add(key={Key}): addCase → {AddCase}", key, addCase);
            switch (addCase)
            {
                case 1:
                    break;
                case 2:
                    var oldParent = node.Parent;
                    node = AddCase2(node);

                    if (node is not null)
                    {
                        childDir = comparer.Compare(oldParent!.Key, oldParent.Parent!.Key);
                    }

                    break;
                case 4:
                    node.Color = NodeColor.Black;
					_logger.LogDebug("Add(key={Key}): node.Color → {Arg1}", key, node.Color);
                    break;
                case 56:
                    AddCase56(node, comparer.Compare(node.Key, node.Parent!.Key), childDir);
                    break;
                default:
					_logger.LogWarning("Throwing InvalidOperationException from Add(key={Key})", key);
                    throw new InvalidOperationException("It should not be possible to get here!");
            }
        }
        while (addCase == 2 && node is not null);

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
        // Search for node
        var node = Remove(root, key);
		_logger.LogDebug("Remove(key={Key}): node → {Node}", key, node);

        // Simple cases
        node = RemoveSimpleCases(node);

        // Exit if deleted node was not non-root black leaf
        if (node is null)
        {
            return;
        }

        // Delete node
        DeleteLeaf(node.Parent!, comparer.Compare(node.Key, node.Parent!.Key));

        // Recolor tree
        do
        {
            node = RemoveRecolor(node);
        }
        while (node is not null && node.Parent is not null);    // Case 2: Reached root

        Count--;
		_logger.LogDebug("<<< Exiting Remove(key={Key})", key);
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
            throw new InvalidOperationException("Tree is empty!");
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
            throw new InvalidOperationException("Tree is empty!");
        }

		_logger.LogDebug("<<< Exiting GetMax(node={Node})", node);
        return GetMax(root).Key;
    }

    /// <summary>
    ///     Get keys in order from smallest to largest as defined by the comparer.
    /// </summary>
    /// <returns>Keys in tree in order from smallest to largest.</returns>
    public IEnumerable<TKey> GetKeysInOrder()
    {
		_logger.LogDebug(">>> Entering GetKeysInOrder()");
        var result = new List<TKey>();
        InOrderWalk(root);
		_logger.LogDebug("GetKeysInOrder(): result → {Result}", result);
		_logger.LogDebug("<<< Exiting GetKeysInOrder()");
        return result;

        void InOrderWalk(RedBlackTreeNode<TKey>? node)
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

        void PreOrderWalk(RedBlackTreeNode<TKey>? node)
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

        void PostOrderWalk(RedBlackTreeNode<TKey>? node)
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
    ///     Perform binary tree insertion.
    /// </summary>
    /// <param name="node">Root of subtree to search from.</param>
    /// <param name="key">Key value to insert.</param>
    /// <returns>Node that was added.</returns>
    private RedBlackTreeNode<TKey> Add(RedBlackTreeNode<TKey> node, TKey key)
    {
		_logger.LogDebug(">>> Entering Add(node={Node},key={Key})", node, key);
        int compareResult;
        RedBlackTreeNode<TKey> newNode;
        while (true)
        {
            compareResult = comparer.Compare(key, node!.Key);
            if (compareResult < 0)
            {
                if (node.Left is null)
                {
                    newNode = new RedBlackTreeNode<TKey>(key, node);
                    node.Left = newNode;
					_logger.LogDebug("Add(node={Node},key={Key}): newNode → {NewNode}", node, key, newNode);
					_logger.LogDebug("Add(node={Node},key={Key}): node.Left → {Arg2}", node, key, node.Left);
                    break;
                }
                else
                {
                    node = node.Left;
					_logger.LogDebug("Add(node={Node},key={Key}): node → {Node1}", node, key, node);
                }
            }
            else if (compareResult > 0)
            {
                if (node.Right is null)
                {
                    newNode = new RedBlackTreeNode<TKey>(key, node);
                    node.Right = newNode;
					_logger.LogDebug("Add(node={Node},key={Key}): node.Right → {Arg2}", node, key, node.Right);
                    break;
                }
                else
                {
                    node = node.Right;
                }
            }
            else
            {
				_logger.LogWarning("Throwing ArgumentException from Add(node={Node},key={Key})", node, key);
                throw new ArgumentException($"""Key "{key}" already exists in tree!""");
            }
        }

        return newNode;
    }

    /// <summary>
    ///     Perform case 2 of insertion by pushing blackness down from parent.
    /// </summary>
    /// <param name="node">Parent of inserted node.</param>
    /// <returns>Grandparent of inserted node.</returns>
    private RedBlackTreeNode<TKey>? AddCase2(RedBlackTreeNode<TKey> node)
    {
		_logger.LogDebug(">>> Entering AddCase2(node={Node})", node);
        var grandparent = node.Parent;
        var parentDir = comparer.Compare(node.Key, node.Parent!.Key);
		_logger.LogDebug("AddCase2(node={Node}): parentDir → {ParentDir}", node, parentDir);
        var uncle = parentDir < 0 ? grandparent!.Right : grandparent!.Left;

        node.Color = NodeColor.Black;
		_logger.LogDebug("AddCase2(node={Node}): node.Color → {Arg1}", node, node.Color);
        uncle!.Color = NodeColor.Black;
        grandparent.Color = NodeColor.Red;
		_logger.LogDebug("AddCase2(node={Node}): grandparent.Color → {Arg1}", node, grandparent.Color);

        // Keep root black
        if (node.Parent.Parent is null)
        {
            node.Parent.Color = NodeColor.Black;
        }

        // Set current node as parent to move up tree
		_logger.LogDebug("<<< Exiting AddCase2(node={Node})", node);
        return node.Parent.Parent;
    }

    /// <summary>
    ///     Perform rotations needed for cases 5 and 6 of insertion.
    /// </summary>
    /// <param name="node">Parent of node just inserted.</param>
    /// <param name="parentDir">The side node is on of its parent.</param>
    /// <param name="childDir">The side the child node is on.</param>
    private void AddCase56(RedBlackTreeNode<TKey> node, int parentDir, int childDir)
    {
		_logger.LogDebug(">>> Entering AddCase56(node={Node},parentDir={ParentDir},childDir={ChildDir})", node, parentDir, childDir);
        if (parentDir < 0)
        {
            // Case 5
            if (childDir > 0)
            {
                node = RotateLeft(node);
				_logger.LogDebug("AddCase56(node={Node},parentDir={ParentDir},childDir={ChildDir}): node → {Node1}", node, parentDir, childDir, node);
            }

            // Case 6
            node = RotateRight(node.Parent!);
            node.Color = NodeColor.Black;
			_logger.LogDebug("AddCase56(node={Node},parentDir={ParentDir},childDir={ChildDir}): node.Color → {Arg3}", node, parentDir, childDir, node.Color);
            node.Right!.Color = NodeColor.Red;
        }
        else
        {
            // Case 5
            if (childDir < 0)
            {
                node = RotateRight(node);
            }

            // Case 6
            node = RotateLeft(node.Parent!);
            node.Color = NodeColor.Black;
            node.Left!.Color = NodeColor.Red;
        }
		_logger.LogDebug("<<< Exiting AddCase56(node={Node},parentDir={ParentDir},childDir={ChildDir})", node, parentDir, childDir);
    }

    /// <summary>
    ///     Determine which add case applies to inserted node.
    /// </summary>
    /// <param name="node">Parent of inserted node.</param>
    /// <returns>Case number needed to get tree in valid state. Cases 5 and 6 are represented by 56.</returns>
    private int GetAddCase(RedBlackTreeNode<TKey> node)
    {
		_logger.LogDebug(">>> Entering GetAddCase(node={Node})", node);
        if (node.Color == NodeColor.Black)
        {
            return 1;
        }
        else if (node.Parent is null)
        {
            return 4;
        }
        else
        {
            // Remaining insert cases need uncle
            var grandparent = node.Parent;
            var parentDir = comparer.Compare(node.Key, node.Parent.Key);
			_logger.LogDebug("GetAddCase(node={Node}): parentDir → {ParentDir}", node, parentDir);
            var uncle = parentDir < 0 ? grandparent.Right : grandparent.Left;

            // Case 5 & 6
            if (uncle is null || uncle.Color == NodeColor.Black)
            {
			_logger.LogDebug("<<< Exiting GetAddCase(node={Node})", node);
                return 56;
            }

            return 2;
        }
    }

    /// <summary>
    ///     Search for the node to be deleted.
    /// </summary>
    /// <param name="node">Node to start search from.</param>
    /// <param name="key">Key to search for.</param>
    /// <returns>Node to be deleted.</returns>
    private RedBlackTreeNode<TKey> Remove(RedBlackTreeNode<TKey>? node, TKey key)
    {
		_logger.LogDebug(">>> Entering Remove(node={Node},key={Key})", node, key);
        if (node is null)
        {
			_logger.LogWarning("Throwing InvalidOperationException from Remove(node={Node},key={Key})", node, key);
            throw new InvalidOperationException("Tree is empty!");
        }
        else if (!Contains(key))
        {
			_logger.LogWarning("Throwing KeyNotFoundException from Remove(node={Node},key={Key})", node, key);
            throw new KeyNotFoundException($"Key {key} is not in the tree!");
        }
        else
        {
            // Find node
            int dir;
            while (true)
            {
                dir = comparer.Compare(key, node!.Key);
                if (dir < 0)
                {
                    node = node.Left;
					_logger.LogDebug("Remove(node={Node},key={Key}): node → {Node1}", node, key, node);
                }
                else if (dir > 0)
                {
                    node = node.Right;
                }
                else
                {
                    break;
                }
            }

            return node;
        }
    }

    /// <summary>
    ///     Get the tree back into a valid state after removing non-root black leaf.
    /// </summary>
    /// <param name="node">Non-root black leaf being removed.</param>
    private RedBlackTreeNode<TKey>? RemoveRecolor(RedBlackTreeNode<TKey> node)
    {
		_logger.LogDebug(">>> Entering RemoveRecolor(node={Node})", node);
        var removeCase = GetRemoveCase(node);

		_logger.LogDebug("RemoveRecolor(node={Node}): removeCase → {RemoveCase}", node, removeCase);
        var dir = comparer.Compare(node.Key, node.Parent!.Key);
		_logger.LogDebug("RemoveRecolor(node={Node}): dir → {Dir}", node, dir);

        // Determine current node's sibling and nephews
        var sibling = dir < 0 ? node.Parent.Right : node.Parent.Left;
        var closeNewphew = dir < 0 ? sibling!.Left : sibling!.Right;
        var distantNephew = dir < 0 ? sibling!.Right : sibling!.Left;

        switch (removeCase)
        {
            case 1:
                sibling.Color = NodeColor.Red;
				_logger.LogDebug("RemoveRecolor(node={Node}): sibling.Color → {Arg1}", node, sibling.Color);
                return node.Parent;
            case 3:
                RemoveCase3(node, closeNewphew, dir);
                break;
            case 4:
                RemoveCase4(sibling);
                break;
            case 5:
                RemoveCase5(node, sibling, dir);
                break;
            case 6:
                RemoveCase6(node, distantNephew!, dir);
                break;
            default:
				_logger.LogWarning("Throwing InvalidOperationException from RemoveRecolor(node={Node})", node);
		_logger.LogDebug("<<< Exiting RemoveRecolor(node={Node})", node);
                throw new InvalidOperationException("It should not be possible to get here!");
        }

        return null;
    }

    /// <summary>
    ///     Simple removal cases where black height doesn't change.
    /// </summary>
    /// <param name="node">Node to remove.</param>
    /// <returns>Non-root black leaf node or null. Null indicates that removal was performed.</returns>
    private RedBlackTreeNode<TKey>? RemoveSimpleCases(RedBlackTreeNode<TKey> node)
    {
		_logger.LogDebug(">>> Entering RemoveSimpleCases(node={Node})", node);
        // Node to delete is root and has no children
        if (node.Parent is null && node.Left is null && node.Right is null)
        {
            root = null;
			_logger.LogWarning("RemoveSimpleCases(node={Node}): root → {Root}", node, root);
            Count--;
            return null;
        }

        // Node has two children. Swap pointers
        if (node.Left is not null && node.Right is not null)
        {
            var successor = GetMin(node.Right);
			_logger.LogDebug("RemoveSimpleCases(node={Node}): successor → {Successor}", node, successor);
			_logger.LogDebug("RemoveSimpleCases(node={Node}): node.Key → {Arg1}", node, node.Key);
            node.Key = successor.Key;
            node = successor;
        }

        // At this point node should have at most one child
        if (node.Color == NodeColor.Red)
        {
            // Node is red so it must have no children since it doesn't have two children
            DeleteLeaf(node.Parent!, comparer.Compare(node.Key, node.Parent!.Key));

            Count--;
            return null;
        }
        else
        {
            // Node is black and may or may not be node
			_logger.LogDebug("<<< Exiting RemoveSimpleCases(node={Node})", node);
            return RemoveBlackNode(node);
        }
    }

    /// <summary>
    ///     Node to delete is black. If it is a leaf then we need to recolor, otherwise remove it.
    /// </summary>
    /// <param name="node">Black node to examine.</param>
    /// <returns>Node to start recoloring from. Null if deletion occurred.</returns>
    private RedBlackTreeNode<TKey>? RemoveBlackNode(RedBlackTreeNode<TKey> node)
    {
		_logger.LogDebug(">>> Entering RemoveBlackNode(node={Node})", node);
        // Node is black and has at most one child. If it has a child it must be red.
        var child = node.Left ?? node.Right;

        // Continue to recoloring if node is leaf
        if (child is null)
        {
            return node;
        }

        // Recolor child
        child.Color = NodeColor.Black;
		_logger.LogDebug("RemoveBlackNode(node={Node}): child.Color → {Arg1}", node, child.Color);
		_logger.LogDebug("RemoveBlackNode(node={Node}): child.Parent → {Arg1}", node, child.Parent);
        child.Parent = node.Parent;

        var childDir = node.Parent is null ? 0 : comparer.Compare(node.Key, node.Parent.Key);
		_logger.LogDebug("RemoveBlackNode(node={Node}): childDir → {ChildDir}", node, childDir);

        // Replace node with child
        Transplant(node.Parent, child, childDir);

        Count--;
		_logger.LogDebug("<<< Exiting RemoveBlackNode(node={Node})", node);
        return null;
    }

    /// <summary>
    ///     Perform case 3 of removal.
    /// </summary>
    /// <param name="node">Node that was removed.</param>
    /// <param name="closeNephew">Close nephew of removed node.</param>
    /// <param name="childDir">Side of parent the removed node was.</param>
    private void RemoveCase3(RedBlackTreeNode<TKey> node, RedBlackTreeNode<TKey>? closeNephew, int childDir)
    {
		_logger.LogDebug(">>> Entering RemoveCase3(node={Node},closeNephew={CloseNephew},childDir={ChildDir})", node, closeNephew, childDir);
        // Rotate and recolor
        var sibling = childDir < 0 ? RotateLeft(node.Parent!) : RotateRight(node.Parent!);
		_logger.LogDebug("RemoveCase3(node={Node},closeNephew={CloseNephew},childDir={ChildDir}): sibling → {Sibling}", node, closeNephew, childDir, sibling);
		_logger.LogDebug("RemoveCase3(node={Node},closeNephew={CloseNephew},childDir={ChildDir}): sibling.Color → {Arg3}", node, closeNephew, childDir, sibling.Color);
        sibling.Color = NodeColor.Black;
        if (childDir < 0)
        {
            sibling.Left!.Color = NodeColor.Red;
        }
        else
        {
            sibling.Right!.Color = NodeColor.Red;
        }

        // Get new distant newphew
        sibling = closeNephew!;
        var distantNephew = childDir < 0 ? sibling.Right : sibling.Left;

        // Parent is red, sibling is black
        if (distantNephew is not null && distantNephew.Color == NodeColor.Red)
        {
            RemoveCase6(node, distantNephew, childDir);
            return;
        }

        // Get new close nephew
        closeNephew = childDir < 0 ? sibling!.Left : sibling!.Right;

        // Sibling is black, distant nephew is black
        if (closeNephew is not null && closeNephew.Color == NodeColor.Red)
        {
            RemoveCase5(node, sibling!, childDir);
            return;
        }

        // Final recoloring
        RemoveCase4(sibling!);
		_logger.LogDebug("<<< Exiting RemoveCase3(node={Node},closeNephew={CloseNephew},childDir={ChildDir})", node, closeNephew, childDir);
    }

    /// <summary>
    ///     Perform case 4 of removal.
    /// </summary>
    /// <param name="sibling">Sibling of removed node.</param>
    private void RemoveCase4(RedBlackTreeNode<TKey> sibling)
    {
		_logger.LogDebug(">>> Entering RemoveCase4(sibling={Sibling})", sibling);
        sibling.Color = NodeColor.Red;
        sibling.Parent!.Color = NodeColor.Black;
		_logger.LogDebug("RemoveCase4(sibling={Sibling}): sibling.Color → {Arg1}", sibling, sibling.Color);
		_logger.LogDebug("<<< Exiting RemoveCase4(sibling={Sibling})", sibling);
    }

    /// <summary>
    ///     Perform case 5 of removal.
    /// </summary>
    /// <param name="node">Node that was removed.</param>
    /// <param name="sibling">Sibling of removed node.</param>
    /// <param name="childDir">Side of parent removed node was on.</param>
    private void RemoveCase5(RedBlackTreeNode<TKey> node, RedBlackTreeNode<TKey> sibling, int childDir)
    {
		_logger.LogDebug(">>> Entering RemoveCase5(node={Node},sibling={Sibling},childDir={ChildDir})", node, sibling, childDir);
        sibling = childDir < 0 ? RotateRight(sibling) : RotateLeft(sibling);
        var distantNephew = childDir < 0 ? sibling.Right! : sibling.Left!;
		_logger.LogDebug("RemoveCase5(node={Node},sibling={Sibling},childDir={ChildDir}): sibling → {Sibling1}", node, sibling, childDir, sibling);

        sibling.Color = NodeColor.Black;
		_logger.LogDebug("RemoveCase5(node={Node},sibling={Sibling},childDir={ChildDir}): sibling.Color → {Arg3}", node, sibling, childDir, sibling.Color);
		_logger.LogDebug("RemoveCase5(node={Node},sibling={Sibling},childDir={ChildDir}): distantNephew.Color → {Arg3}", node, sibling, childDir, distantNephew.Color);
        distantNephew.Color = NodeColor.Red;

        RemoveCase6(node, distantNephew, childDir);
		_logger.LogDebug("<<< Exiting RemoveCase5(node={Node},sibling={Sibling},childDir={ChildDir})", node, sibling, childDir);
    }

    /// <summary>
    ///     Perform case 6 of removal.
    /// </summary>
    /// <param name="node">Node that was removed.</param>
    /// <param name="distantNephew">Distant nephew of removed node.</param>
    /// <param name="childDir">Side of parent removed node was on.</param>
    private void RemoveCase6(RedBlackTreeNode<TKey> node, RedBlackTreeNode<TKey> distantNephew, int childDir)
    {
		_logger.LogDebug(">>> Entering RemoveCase6(node={Node},distantNephew={DistantNephew},childDir={ChildDir})", node, distantNephew, childDir);
        var oldParent = node.Parent!;
        node = childDir < 0 ? RotateLeft(oldParent) : RotateRight(oldParent);
		_logger.LogDebug("RemoveCase6(node={Node},distantNephew={DistantNephew},childDir={ChildDir}): node → {Node1}", node, distantNephew, childDir, node);
		_logger.LogDebug("RemoveCase6(node={Node},distantNephew={DistantNephew},childDir={ChildDir}): node.Color → {Arg3}", node, distantNephew, childDir, node.Color);
		_logger.LogDebug("RemoveCase6(node={Node},distantNephew={DistantNephew},childDir={ChildDir}): oldParent.Color → {Arg3}", node, distantNephew, childDir, oldParent.Color);
		_logger.LogDebug("RemoveCase6(node={Node},distantNephew={DistantNephew},childDir={ChildDir}): distantNephew.Color → {Arg3}", node, distantNephew, childDir, distantNephew.Color);
        node.Color = oldParent.Color;
        oldParent.Color = NodeColor.Black;
        distantNephew.Color = NodeColor.Black;
		_logger.LogDebug("<<< Exiting RemoveCase6(node={Node},distantNephew={DistantNephew},childDir={ChildDir})", node, distantNephew, childDir);
    }

    /// <summary>
    ///     Determine which removal case is required.
    /// </summary>
    /// <param name="node">Node being removed.</param>
    /// <returns>Which removal case should be performed.</returns>
    private int GetRemoveCase(RedBlackTreeNode<TKey> node)
    {
		_logger.LogDebug(">>> Entering GetRemoveCase(node={Node})", node);
        var dir = comparer.Compare(node.Key, node.Parent!.Key);

		_logger.LogDebug("GetRemoveCase(node={Node}): dir → {Dir}", node, dir);
        // Determine current node's sibling and nephews
        var sibling = dir < 0 ? node.Parent.Right : node.Parent.Left;
        var closeNewphew = dir < 0 ? sibling!.Left : sibling!.Right;
        var distantNephew = dir < 0 ? sibling!.Right : sibling!.Left;

        if (sibling.Color == NodeColor.Red)
        {
            return 3;
        }
        else if (distantNephew is not null && distantNephew.Color == NodeColor.Red)
        {
            return 6;
        }
        else if (closeNewphew is not null && closeNewphew.Color == NodeColor.Red)
        {
            return 5;
        }
        else if (node.Parent.Color == NodeColor.Red)
        {
            return 4;
        }
        else
        {
			_logger.LogDebug("<<< Exiting GetRemoveCase(node={Node})", node);
            return 1;
        }
		_logger.LogDebug(">>> Entering RotateLeft(node={Node})", node);
		_logger.LogDebug("RotateLeft(node={Node}): node.Parent → {Arg1}", node, node.Parent);
		_logger.LogDebug("RotateLeft(node={Node}): node.Left → {Arg1}", node, node.Left);
		_logger.LogWarning("RotateLeft(node={Node}): root → {Root}", node, root);
		_logger.LogDebug("<<< Exiting RotateLeft(node={Node})", node);
    }

    /// <summary>
    ///     Set child of node or delete leaf.
    /// </summary>
    /// <param name="node">Node to set child of. Set to null for root.</param>
    /// <param name="child">Node to set as child.</param>
    /// <param name="dir">Which side of node to place child.</param>
    private void Transplant(RedBlackTreeNode<TKey>? node, RedBlackTreeNode<TKey>? child, int dir)
    {
		_logger.LogDebug(">>> Entering Transplant(node={Node},child={Child},dir={Dir})", node, child, dir);
        if (node is null)
        {
            root = child;
			_logger.LogWarning("Transplant(node={Node},child={Child},dir={Dir}): root → {Root}", node, child, dir, root);
        }
        else if (child is null)
        {
            DeleteLeaf(node, dir);
        }
        else if (dir < 0)
        {
            node.Left = child;
			_logger.LogDebug("Transplant(node={Node},child={Child},dir={Dir}): node.Left → {Arg3}", node, child, dir, node.Left);
        }
        else
        {
            node.Right = child;
			_logger.LogDebug("Transplant(node={Node},child={Child},dir={Dir}): node.Right → {Arg3}", node, child, dir, node.Right);
        }
		_logger.LogDebug("<<< Exiting Transplant(node={Node},child={Child},dir={Dir})", node, child, dir);
    }

    /// <summary>
    ///     Delete leaf node.
    /// </summary>
    /// <param name="node">Parent of leaf node to delete.</param>
    /// <param name="dir">Side of parent leaf is on.</param>
    private void DeleteLeaf(RedBlackTreeNode<TKey> node, int dir)
    {
		_logger.LogDebug(">>> Entering DeleteLeaf(node={Node},dir={Dir})", node, dir);
        if (dir < 0)
        {
            node.Left = null;
			_logger.LogDebug("DeleteLeaf(node={Node},dir={Dir}): node.Left → {Arg2}", node, dir, node.Left);
        }
        else
        {
            node.Right = null;
			_logger.LogDebug("DeleteLeaf(node={Node},dir={Dir}): node.Right → {Arg2}", node, dir, node.Right);
        }
		_logger.LogDebug("<<< Exiting DeleteLeaf(node={Node},dir={Dir})", node, dir);
    }

    /// <summary>
    ///     Perform a left (counter-clockwise) rotation.
    /// </summary>
    /// <param name="node">Node to rotate about.</param>
    /// <returns>New node with rotation applied.</returns>
    private RedBlackTreeNode<TKey> RotateLeft(RedBlackTreeNode<TKey> node)
    {
        var temp1 = node;
        var temp2 = node!.Right!.Left;

        node = node.Right;
        node.Parent = temp1.Parent;
        if (node.Parent is not null)
        {
            var nodeDir = comparer.Compare(node.Key, node.Parent.Key);
			_logger.LogDebug("RotateLeft(node={Node}): nodeDir → {NodeDir}", node, nodeDir);
            if (nodeDir < 0)
            {
                node.Parent.Left = node;
            }
            else
            {
                node.Parent.Right = node;
            }
        }

        node.Left = temp1;
        node.Left.Parent = node;

        node.Left.Right = temp2;
        if (temp2 is not null)
        {
            node.Left.Right!.Parent = temp1;
        }

        if (node.Parent is null)
        {
            root = node;
        }

        return node;
    }

    /// <summary>
    ///     Perform a right (clockwise) rotation.
    /// </summary>
    /// <param name="node">Node to rotate about.</param>
    /// <returns>New node with rotation applied.</returns>
    private RedBlackTreeNode<TKey> RotateRight(RedBlackTreeNode<TKey> node)
    {
		_logger.LogDebug(">>> Entering RotateRight(node={Node})", node);
        var temp1 = node;
        var temp2 = node!.Left!.Right;

        node = node.Left;
        node.Parent = temp1.Parent;
		_logger.LogDebug("RotateRight(node={Node}): node.Parent → {Arg1}", node, node.Parent);
        if (node.Parent is not null)
        {
            var nodeDir = comparer.Compare(node.Key, node.Parent.Key);
			_logger.LogDebug("RotateRight(node={Node}): nodeDir → {NodeDir}", node, nodeDir);
            if (nodeDir < 0)
            {
                node.Parent.Left = node;
            }
            else
            {
                node.Parent.Right = node;
            }
        }

        node.Right = temp1;
		_logger.LogDebug("RotateRight(node={Node}): node.Right → {Arg1}", node, node.Right);
        node.Right.Parent = node;

        node.Right.Left = temp2;
        if (temp2 is not null)
        {
            node.Right.Left!.Parent = temp1;
        }

        if (node.Parent is null)
        {
            root = node;
			_logger.LogWarning("RotateRight(node={Node}): root → {Root}", node, root);
        }

		_logger.LogDebug("<<< Exiting RotateRight(node={Node})", node);
        return node;
    }

    /// <summary>
    ///     Helper function to get node instance with minimum key value
    ///     in the specified subtree.
    /// </summary>
    /// <param name="node">Node specifying root of subtree.</param>
    /// <returns>Minimum value in node's subtree.</returns>
    private RedBlackTreeNode<TKey> GetMin(RedBlackTreeNode<TKey> node)
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
    /// <param name="node">Node specifyng root of subtree.</param>
    /// <returns>Maximum value in node's subtree.</returns>
    private RedBlackTreeNode<TKey> GetMax(RedBlackTreeNode<TKey> node)
    {
        while (node.Right is not null)
        {
            node = node.Right;
        }

        return node;
    }
}
