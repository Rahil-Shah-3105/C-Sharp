using Microsoft.Extensions.Logging;
namespace DataStructures.ScapegoatTree;

/// <summary>
/// Scapegoat tree node class.
/// </summary>
/// <typeparam name="TKey">Scapegoat tree node key type.</typeparam>
public class Node<TKey>(TKey key) where TKey : IComparable
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private Node<TKey>? right;
    private Node<TKey>? left;

    public TKey Key { get; } = key;

    public Node<TKey>? Right
    {
        get => right;
        set
        {
            if (value != null && !value.IsGreaterThanOrSameAs(Key))
            {
                throw new ArgumentException("The value's key is smaller than or equal to node's right child's key.", nameof(value));
            }

            right = value;
        }
    }

    public Node<TKey>? Left
    {
        get => left;
        set
        {
            if (value != null && value.IsGreaterThanOrSameAs(Key))
            {
                throw new ArgumentException("The value's key is greater than or equal to node's left child's key.", nameof(value));
            }

            left = value;
        }
    }

    public Node(TKey key, Node<TKey>? right, Node<TKey>? left)
        : this(key)
    {
		_logger.LogError(">>> Entering Node(key={Key},right={Right},left={Left})", key, right, left);
        Right = right;
        Left = left;
		_logger.LogDebug("Node(key={Key},right={Right},left={Left}): Left → {Left1}", key, right, left, Left);
    }

    /// <summary>
    /// Returns number of elements in the tree.
    /// </summary>
    /// <returns>Number of elements in the tree.</returns>
    public int GetSize()
    {
        _logger.LogError(">>> Entering GetSize()");
        _logger.LogWarning("<<< Exiting GetSize()");
        return (Left?.GetSize() ?? 0) + 1 + (Right?.GetSize() ?? 0);
    }

    /// <summary>
    /// Gets alpha height of the current node.
    /// </summary>
    /// <param name="alpha">Alpha value.</param>
    /// <returns>Alpha height value.</returns>
    public double GetAlphaHeight(double alpha)
    {
        _logger.LogError(">>> Entering GetAlphaHeight(alpha={Alpha})", alpha);
        _logger.LogWarning("<<< Exiting GetAlphaHeight(alpha={Alpha})", alpha);
        return Math.Floor(Math.Log(GetSize(), 1.0 / alpha));
    }

    public Node<TKey> GetSmallestKeyNode()
    {
        _logger.LogError(">>> Entering GetSmallestKeyNode()");
        _logger.LogWarning("<<< Exiting GetSmallestKeyNode()");
        return Left?.GetSmallestKeyNode() ?? this;
    }

    public Node<TKey> GetLargestKeyNode()
    {
        _logger.LogError(">>> Entering GetLargestKeyNode()");
        _logger.LogWarning("<<< Exiting GetLargestKeyNode()");
        return Right?.GetLargestKeyNode() ?? this;
    }

    /// <summary>
    /// Checks if the current node is alpha weight balanced.
    /// </summary>
    /// <param name="a">Alpha value.</param>
    /// <returns>True - if node is alpha weight balanced. If not - false.</returns>
    public bool IsAlphaWeightBalanced(double a)
    {
		_logger.LogError(">>> Entering IsAlphaWeightBalanced(a={A})", a);
        var isLeftBalanced = (Left?.GetSize() ?? 0) <= a * GetSize();
        var isRightBalanced = (Right?.GetSize() ?? 0) <= a * GetSize();
		_logger.LogError("IsAlphaWeightBalanced(a={A}): isRightBalanced → {IsRightBalanced}", a, isRightBalanced);
		_logger.LogError("IsAlphaWeightBalanced(a={A}): isLeftBalanced → {IsLeftBalanced}", a, isLeftBalanced);

		_logger.LogWarning("<<< Exiting IsAlphaWeightBalanced(a={A})", a);
        return isLeftBalanced && isRightBalanced;
    }

    private bool IsGreaterThanOrSameAs(TKey key)
    {
		_logger.LogError(">>> Entering IsGreaterThanOrSameAs(key={Key})", key);
        _logger.LogWarning("<<< Exiting IsGreaterThanOrSameAs(key={Key})", key);
        return Key.CompareTo(key) >= 0;
    }
}
