using Microsoft.Extensions.Logging;
namespace DataStructures.ScapegoatTree;

/// <summary>
/// A scapegoat implementation class.
/// See https://en.wikipedia.org/wiki/Scapegoat_tree for more information about scapegoat tree.
/// </summary>
/// <typeparam name="TKey">The scapegoat tree key type.</typeparam>
public class ScapegoatTree<TKey> where TKey : IComparable
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// Gets the α (alpha) value of the tree.
    /// </summary>
    public double Alpha { get; private set; }

    /// <summary>
    /// Gets the root node of the tree.
    /// </summary>
    public Node<TKey>? Root { get; private set; }

    /// <summary>
    /// Gets the number of nodes in the tree.
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    /// Gets the maximal value of the tree Size since the last time the tree was completely rebuilt.
    /// </summary>
    public int MaxSize { get; private set; }

    /// <summary>
    /// Gets an event handler which will fire when tree is being balanced.
    /// </summary>
    public event EventHandler? TreeIsUnbalanced;

    public ScapegoatTree()
        : this(alpha: 0.5, size: 0)
    {
	_logger.LogError(">>> Entering ScapegoatTree()");
    _logger.LogWarning("<<< Exiting ScapegoatTree()");
    }

    public ScapegoatTree(double alpha)
        : this(alpha, size: 0)
    {
	_logger.LogError(">>> Entering ScapegoatTree(alpha={Alpha})", alpha);
    _logger.LogWarning("<<< Exiting ScapegoatTree(alpha={Alpha})", alpha);
    }

    public ScapegoatTree(Node<TKey> node, double alpha)
        : this(alpha, size: node.GetSize())
    {
		_logger.LogError(">>> Entering ScapegoatTree(node={Node},alpha={Alpha})", node, alpha);
        Root = node;
		_logger.LogWarning("ScapegoatTree(node={Node},alpha={Alpha}): Root → {Root}", node, alpha, Root);
    _logger.LogWarning("<<< Exiting ScapegoatTree(node={Node},alpha={Alpha})", node, alpha);
    }

    public ScapegoatTree(TKey key, double alpha = 0.5)
        : this(alpha, size: 1)
    {
        Root = new Node<TKey>(key);
		_logger.LogError("ScapegoatTree(key={Key},alpha={Alpha}): Root → {Root}", key, alpha, Root);
    }

    private ScapegoatTree(double alpha, int size)
    {
		_logger.LogWarning("ScapegoatTree(key={Key},alpha={Alpha}): Root → {Root}", key, alpha, Root);
        CheckAlpha(alpha);

        Alpha = alpha;
		_logger.LogDebug("ScapegoatTree(alpha={Alpha},size={Size}): Alpha → {Alpha1}", alpha, size, Alpha);

        Size = size;
		_logger.LogDebug("ScapegoatTree(alpha={Alpha},size={Size}): Size → {Size1}", alpha, size, Size);
        MaxSize = size;
		_logger.LogDebug("ScapegoatTree(alpha={Alpha},size={Size}): MaxSize → {MaxSize}", alpha, size, MaxSize);
    }

    /// <summary>
    /// Checks if current instance of the scapegoat tree is alpha weight balanced.
    /// </summary>
    /// <returns>True - if tree is alpha weight balanced. Otherwise, false.</returns>
    public bool IsAlphaWeightBalanced()
    {
		_logger.LogError(">>> Entering IsAlphaWeightBalanced()");
        _logger.LogWarning("<<< Exiting IsAlphaWeightBalanced()");
        return Root?.IsAlphaWeightBalanced(Alpha) ?? true;
    }

    /// <summary>
    /// Check if any node in the tree has specified key value.
    /// </summary>
    /// <param name="key">Key value.</param>
    /// <returns>Returns true if node exists, false if not.</returns>
    public bool Contains(TKey key)
    {
		_logger.LogError(">>> Entering Contains(key={Key})", key);
        _logger.LogWarning("<<< Exiting Contains(key={Key})", key);
        return Search(key) != null;
    }

    /// <summary>
    /// Searches current instance of the scapegoat tree for specified key.
    /// </summary>
    /// <param name="key">Key value.</param>
    /// <returns>Node with the specified key or null.</returns>
    public Node<TKey>? Search(TKey key)
    {
		_logger.LogError(">>> Entering Search(key={Key})", key);
        if (Root == null)
        {
            return null;
        }

        var current = Root;
		_logger.LogError("Search(key={Key}): current → {Current}", key, current);

        while (true)
        {
            var result = current.Key.CompareTo(key);
			_logger.LogError("Search(key={Key}): result → {Result}", key, result);

            switch (result)
            {
                case 0:
                    return current;
                case > 0 when current.Left != null:
                    current = current.Left;
                    break;
                case < 0 when current.Right != null:
                    current = current.Right;
                    break;
                default:
					_logger.LogWarning("<<< Exiting Search(key={Key})", key);
                    return null;
            }
        }
    }

    /// <summary>
    /// Inserts a new key into current instance of the scapegoat tree. Rebuilds tree if it's unbalanced.
    /// </summary>
    /// <param name="key">Key value.</param>
    /// <returns>True - if insertion is successful, false - if the key is already present in the tree.</returns>
    public bool Insert(TKey key)
    {
		_logger.LogError(">>> Entering Insert(key={Key})", key);
        var node = new Node<TKey>(key);

		_logger.LogError("Insert(key={Key}): node → {Node}", key, node);
        if (Root == null)
        {
            Root = node;
			_logger.LogWarning("Insert(key={Key}): Root → {Root}", key, Root);

            UpdateSizes();

            return true;
        }

        var path = new Stack<Node<TKey>>();
		_logger.LogError("Insert(key={Key}): path → {Path}", key, path);

        var current = Root;
		_logger.LogError("Insert(key={Key}): current → {Current}", key, current);

        var found = false;

        while (!found)
        {
            path.Push(current);

            var result = current.Key.CompareTo(node.Key);
			_logger.LogError("Insert(key={Key}): result → {Result}", key, result);

            switch (result)
            {
                case < 0 when current.Right != null:
                    current = current.Right;
                    continue;
                case < 0:
                    current.Right = node;
					_logger.LogDebug("Insert(key={Key}): current.Right → {Arg1}", key, current.Right);
                    found = true;
					_logger.LogError("Insert(key={Key}): found → {Found}", key, found);
                    break;
                case > 0 when current.Left != null:
                    current = current.Left;
                    continue;
                case > 0:
                    current.Left = node;
					_logger.LogDebug("Insert(key={Key}): current.Left → {Arg1}", key, current.Left);
                    found = true;
                    break;
                default:
                    return false;
            }
        }

        UpdateSizes();

        if (path.Count > Root.GetAlphaHeight(Alpha))
        {
            TreeIsUnbalanced?.Invoke(this, EventArgs.Empty);

            BalanceFromPath(path);

            MaxSize = Math.Max(MaxSize, Size);
			_logger.LogError("Insert(key={Key}): MaxSize → {MaxSize}", key, MaxSize);
			_logger.LogDebug("Insert(key={Key}): MaxSize → {MaxSize}", key, MaxSize);
        }

		_logger.LogWarning("<<< Exiting Insert(key={Key})", key);
        return true;
    }

    /// <summary>
    /// Removes the specified key from the current instance of the scapegoat tree. Rebuilds tree if it's unbalanced.
    /// </summary>
    /// <param name="key">Key value.</param>
    /// <returns>True - if key was successfully removed, false - if the key wasn't found in the tree.</returns>
    public bool Delete(TKey key)
    {
		_logger.LogError(">>> Entering Delete(key={Key})", key);
        if (Root == null)
        {
            return false;
        }

        if (Remove(Root, Root, key))
        {
            Size--;

            if (Root != null && Size < Alpha * MaxSize)
            {
                TreeIsUnbalanced?.Invoke(this, EventArgs.Empty);

                var list = new List<Node<TKey>>();
				_logger.LogError("Delete(key={Key}): list → {List}", key, list);

                Extensions.FlattenTree(Root, list);

                Root = Extensions.RebuildFromList(list, 0, list.Count - 1);
				_logger.LogError("Delete(key={Key}): Root → {Root}", key, Root);
				_logger.LogWarning("Delete(key={Key}): Root → {Root}", key, Root);

                MaxSize = Size;
				_logger.LogDebug("Delete(key={Key}): MaxSize → {MaxSize}", key, MaxSize);
            }

            return true;
        }

		_logger.LogWarning("<<< Exiting Delete(key={Key})", key);
        return false;
    }

    /// <summary>
    /// Clears the tree.
    /// </summary>
    public void Clear()
    {
		_logger.LogError(">>> Entering Clear()");
        Size = 0;
		_logger.LogDebug("Clear(): Size → {Size}", Size);
        MaxSize = 0;
		_logger.LogDebug("Clear(): MaxSize → {MaxSize}", MaxSize);
        Root = null;
		_logger.LogWarning("Clear(): Root → {Root}", Root);
		_logger.LogWarning("<<< Exiting Clear()");
    }

    /// <summary>
    /// Changes <see cref="Alpha"/> value to adjust balancing.
    /// </summary>
    /// <param name="value">New alpha value.</param>
    public void Tune(double value)
    {
		_logger.LogError(">>> Entering Tune(value={Value})", value);
        CheckAlpha(value);
		_logger.LogWarning("<<< Exiting Tune(value={Value})", value);
        Alpha = value;
		_logger.LogDebug("Tune(value={Value}): Alpha → {Alpha}", value, Alpha);
    }

    /// <summary>
    /// Searches for a scapegoat node in provided stack.
    /// </summary>
    /// <param name="path">Stack instance with nodes, starting with root node.</param>
    /// <returns>Scapegoat node with its parent node. Parent can be null if scapegoat node is root node.</returns>
    /// <exception cref="ArgumentException">Thrown if path stack is empty.</exception>
    /// <exception cref="InvalidOperationException">Thrown if scapegoat wasn't found.</exception>
    public (Node<TKey>? Parent, Node<TKey> Scapegoat) FindScapegoatInPath(Stack<Node<TKey>> path)
    {
		_logger.LogError(">>> Entering FindScapegoatInPath(path={Path})", path);
        if (path.Count == 0)
        {
			_logger.LogError("Throwing ArgumentException from FindScapegoatInPath(path={Path})", path);
            throw new ArgumentException("The path collection should not be empty.", nameof(path));
        }

        var depth = 1;

        while (path.TryPop(out var next))
        {
            if (depth > next.GetAlphaHeight(Alpha))
            {
                return path.TryPop(out var parent) ? (parent, next) : (null, next);
            }

            depth++;
        }

		_logger.LogError("Throwing InvalidOperationException from FindScapegoatInPath(path={Path})", path);
		_logger.LogWarning("<<< Exiting FindScapegoatInPath(path={Path})", path);
        throw new InvalidOperationException("Scapegoat node wasn't found. The tree should be unbalanced.");
    }

    private static void CheckAlpha(double alpha)
    {
		_logger.LogError(">>> Entering CheckAlpha(alpha={Alpha})", alpha);
        if (alpha is < 0.5 or > 1.0)
        {
			_logger.LogError("Throwing ArgumentException from CheckAlpha(alpha={Alpha})", alpha);
            throw new ArgumentException("The alpha parameter's value should be in 0.5..1.0 range.", nameof(alpha));
        }
		_logger.LogWarning("<<< Exiting CheckAlpha(alpha={Alpha})", alpha);
    }

    private bool Remove(Node<TKey>? parent, Node<TKey>? node, TKey key)
    {
		_logger.LogError(">>> Entering Remove(parent={Parent},node={Node},key={Key})", parent, node, key);
        if (node is null || parent is null)
        {
            return false;
        }

        var compareResult = node.Key.CompareTo(key);
		_logger.LogError("Remove(parent={Parent},node={Node},key={Key}): compareResult → {CompareResult}", parent, node, key, compareResult);

        if (compareResult > 0)
        {
            return Remove(node, node.Left, key);
        }

        if (compareResult < 0)
        {
            return Remove(node, node.Right, key);
        }

        Node<TKey>? replacementNode;

        // Case 0: Node has no children.
        // Case 1: Node has one child.
        if (node.Left is null || node.Right is null)
        {
            replacementNode = node.Left ?? node.Right;
			_logger.LogDebug("Remove(parent={Parent},node={Node},key={Key}): replacementNode → {ReplacementNode}", parent, node, key, replacementNode);
        }

        // Case 2: Node has two children. (This implementation uses the in-order predecessor to replace node.)
        else
        {
            var predecessorNode = node.Left.GetLargestKeyNode();
			_logger.LogError("Remove(parent={Parent},node={Node},key={Key}): predecessorNode → {PredecessorNode}", parent, node, key, predecessorNode);
            Remove(Root, Root, predecessorNode.Key);
            replacementNode = new Node<TKey>(predecessorNode.Key)
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
			_logger.LogWarning("Remove(parent={Parent},node={Node},key={Key}): Root → {Root}", parent, node, key, Root);
        }
        else if (parent.Left == node)
        {
            parent.Left = replacementNode;
			_logger.LogDebug("Remove(parent={Parent},node={Node},key={Key}): parent.Left → {Arg3}", parent, node, key, parent.Left);
        }
        else
        {
            parent.Right = replacementNode;
			_logger.LogDebug("Remove(parent={Parent},node={Node},key={Key}): parent.Right → {Arg3}", parent, node, key, parent.Right);
        }

		_logger.LogWarning("<<< Exiting Remove(parent={Parent},node={Node},key={Key})", parent, node, key);
        return true;
    }

    private void BalanceFromPath(Stack<Node<TKey>> path)
    {
		_logger.LogError(">>> Entering BalanceFromPath(path={Path})", path);
        var (parent, scapegoat) = FindScapegoatInPath(path);

        var list = new List<Node<TKey>>();
		_logger.LogError("BalanceFromPath(path={Path}): list → {List}", path, list);

        Extensions.FlattenTree(scapegoat, list);

        var tree = Extensions.RebuildFromList(list, 0, list.Count - 1);
		_logger.LogError("BalanceFromPath(path={Path}): tree → {Tree}", path, tree);

        if (parent == null)
        {
            Root = tree;
			_logger.LogWarning("BalanceFromPath(path={Path}): Root → {Root}", path, Root);
        }
        else
        {
            var result = parent.Key.CompareTo(tree.Key);
			_logger.LogError("BalanceFromPath(path={Path}): result → {Result}", path, result);

            if (result < 0)
            {
                parent.Right = tree;
				_logger.LogDebug("BalanceFromPath(path={Path}): parent.Right → {Arg1}", path, parent.Right);
            }
            else
            {
                parent.Left = tree;
				_logger.LogDebug("BalanceFromPath(path={Path}): parent.Left → {Arg1}", path, parent.Left);
            }
        }
		_logger.LogWarning("<<< Exiting BalanceFromPath(path={Path})", path);
    }

    private void UpdateSizes()
    {
		_logger.LogError(">>> Entering UpdateSizes()");
        Size += 1;
        MaxSize = Math.Max(Size, MaxSize);
		_logger.LogError("UpdateSizes(): MaxSize → {MaxSize}", MaxSize);
		_logger.LogWarning("<<< Exiting UpdateSizes()");
    }
}
