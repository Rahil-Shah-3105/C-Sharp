using Microsoft.Extensions.Logging;
namespace DataStructures.AVLTree;

/// <summary>
///     Generic class to represent nodes in an <see cref="AvlTree{TKey}"/>
///     instance.
/// </summary>
/// <typeparam name="TKey">The type of key for the node.</typeparam>
/// <remarks>
///     Initializes a new instance of the
///     <see cref="AvlTreeNode{TKey}"/> class.
/// </remarks>
/// <param name="key">Key value for node.</param>
internal class AvlTreeNode<TKey>(TKey key)
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Gets or sets key value of node.
    /// </summary>
    public TKey Key { get; set; } = key;

    /// <summary>
    ///     Gets the balance factor of the node.
    /// </summary>
    public int BalanceFactor { get; private set; }

    /// <summary>
    ///     Gets or sets the left child of the node.
    /// </summary>
    public AvlTreeNode<TKey>? Left { get; set; }

    /// <summary>
    ///     Gets or sets the right child of the node.
    /// </summary>
    public AvlTreeNode<TKey>? Right { get; set; }

    /// <summary>
    ///     Gets or sets the height of the node.
    /// </summary>
    private int Height { get; set; }

    /// <summary>
    ///     Update the node's height and balance factor.
    /// </summary>
    public void UpdateBalanceFactor()
    {
		_logger.LogDebug(">>> Entering UpdateBalanceFactor()");
        if (Left is null && Right is null)
        {
            Height = 0;
            BalanceFactor = 0;
        }
        else if (Left is null)
		_logger.LogDebug("UpdateBalanceFactor(): BalanceFactor → {BalanceFactor}", BalanceFactor);
        {
            Height = Right!.Height + 1;
            BalanceFactor = Height;
        }
        else if (Right is null)
        {
            Height = Left!.Height + 1;
            BalanceFactor = -Height;
        }
        else
        {
            Height = Math.Max(Left.Height, Right.Height) + 1;
            BalanceFactor = Right.Height - Left.Height;
        }
		_logger.LogDebug("<<< Exiting UpdateBalanceFactor()");
    }
}
