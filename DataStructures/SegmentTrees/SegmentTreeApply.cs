using Microsoft.Extensions.Logging;
namespace DataStructures.SegmentTrees;

/// <summary>
///     This is an extension of a segment tree, which allows applying distributive operations to a subarray
///     (in this case multiplication).
/// </summary>
public class SegmentTreeApply : SegmentTree
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Initializes a new instance of the <see cref="SegmentTreeApply" /> class.
    ///     Runtime complexity: O(n) where n equals the array-length.
    /// </summary>
    /// <param name="arr">Array on which the operations should be made.</param>
    public SegmentTreeApply(int[] arr)
        : base(arr)
    {
		_logger.LogDebug(">>> Entering SegmentTreeApply(arr={Arr})", arr);
        // Initilizes and fills "operand" array with neutral element (in this case 1, because value * 1 = value)
        Operand = new int[Tree.Length];
		_logger.LogDebug("SegmentTreeApply(arr={Arr}): Operand → {Operand}", arr, Operand);
        Array.Fill(Operand, 1);
		_logger.LogDebug("<<< Exiting SegmentTreeApply(arr={Arr})", arr);
    }

    /// <summary>
    ///     Gets an array that stores for each node an operand,
    ///     which must be applied to all direct and indirect child nodes of this node
    ///     (but not to the node itself).
    /// </summary>
    public int[] Operand { get; }

    /// <summary>
    ///     Applies a distributive operation to a subarray defined by <c>l</c> and <c>r</c>
    ///     (in this case multiplication by <c>value</c>).
    ///     Runtime complexity: O(logN) where N equals the initial array-length.
    /// </summary>
    /// <param name="l">Left border of the subarray.</param>
    /// <param name="r">Right border of the subarray.</param>
    /// <param name="value">Value with which each element of the interval is calculated.</param>
    public void Apply(int l, int r, int value)
    {
		_logger.LogDebug(">>> Entering Apply(l={L},r={R},value={Value})", l, r, value);
        // The Application start at node with 1
        // Node with index 1 includes the whole input subarray
        Apply(++l, ++r, value, 1, Tree.Length / 2, 1);
		_logger.LogDebug("<<< Exiting Apply(l={L},r={R},value={Value})", l, r, value);
    }

    /// <summary>
    ///     Edits a query.
    /// </summary>
    /// <param name="l">Left border of the query.</param>
    /// <param name="r">Right border of the query.</param>
    /// <param name="a">Left end of the subarray enclosed by <c>i</c>.</param>
    /// <param name="b">Right end of the subarray enclosed by <c>i</c>.</param>
    /// <param name="i">Current node.</param>
    /// <returns>Sum of a subarray between <c>l</c> and <c>r</c> (including <c>l</c> and <c>r</c>).</returns>
    protected override int Query(int l, int r, int a, int b, int i)
    {
		_logger.LogDebug(">>> Entering Query(l={L},r={R},a={A},b={B},i={I})", l, r, a, b, i);
        if (l <= a && b <= r)
        {
            return Tree[i];
        }

        if (r < a || b < l)
        {
            return 0;
        }

        var m = (a + b) / 2;
		_logger.LogDebug("Query(l={L},r={R},a={A},b={B},i={I}): m → {M}", l, r, a, b, i, m);

        // Application of the saved operand to the direct and indrect child nodes
		_logger.LogDebug("<<< Exiting Query(l={L},r={R},a={A},b={B},i={I})", l, r, a, b, i);
        return Operand[i] * (Query(l, r, a, m, Left(i)) + Query(l, r, m + 1, b, Right(i)));
    }

    /// <summary>
    ///     Applies the operation.
    /// </summary>
    /// <param name="l">Left border of the Application.</param>
    /// <param name="r">Right border of the Application.</param>
    /// <param name="value">Multiplier by which the subarray is to be multiplied.</param>
    /// <param name="a">Left end of the subarray enclosed by <c>i</c>.</param>
    /// <param name="b">Right end of the subarray enclosed by <c>i</c>.</param>
    /// <param name="i">Current node.</param>
    private void Apply(int l, int r, int value, int a, int b, int i)
    {
		_logger.LogDebug(">>> Entering Apply(l={L},r={R},value={Value},a={A},b={B},i={I})", l, r, value, a, b, i);
        // If a and b are in the (by l and r) specified subarray
        if (l <= a && b <= r)
        {
            // Applies the operation to the current node and saves it for the direct and indirect child nodes
            Operand[i] = value * Operand[i];
            Tree[i] = value * Tree[i];
            return;
        }

        // If a or b are out of the by l and r specified subarray stop application at this node
        if (r < a || b < l)
        {
            return;
        }

        // Calculates index m of the node that cuts the current subarray in half
        var m = (a + b) / 2;
		_logger.LogDebug("Apply(l={L},r={R},value={Value},a={A},b={B},i={I}): m → {M}", l, r, value, a, b, i, m);

        // Applies the operation to both halfes
        Apply(l, r, value, a, m, Left(i));
        Apply(l, r, value, m + 1, b, Right(i));

        // Recalculates the value of this node by its (possibly new) children.
        Tree[i] = Operand[i] * (Tree[Left(i)] + Tree[Right(i)]);
		_logger.LogDebug("<<< Exiting Apply(l={L},r={R},value={Value},a={A},b={B},i={I})", l, r, value, a, b, i);
    }
}
