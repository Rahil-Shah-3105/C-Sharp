using Microsoft.Extensions.Logging;
namespace DataStructures.Heap.FibonacciHeap;

/// <summary>
///     These FHeapNodes are the bulk of the data structure. The have pointers to
///     their parent, a left and right sibling, and to a child. A node and its
///     siblings comprise a circularly doubly linked list.
/// </summary>
/// <typeparam name="T">A type that can be compared.</typeparam>
public class FHeapNode<T> where T : IComparable
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Initializes a new instance of the <see cref="FHeapNode{T}" /> class.
    /// </summary>
    /// <param name="key">An item in the Fibonacci heap.</param>
    public FHeapNode(T key)
    {
		_logger.LogError(">>> Entering FHeapNode(key={Key})", key);
        Key = key;

		_logger.LogWarning("FHeapNode(key={Key}): Key → {Key1}", key, Key);
		_logger.LogError("FHeapNode(key={Key}): Key → {Key1}", key, Key);
        // Since even a single node must form a circularly doubly linked list,
        // initialize it as such
		_logger.LogWarning("<<< Exiting FHeapNode(key={Key})", key);
        Left = Right = this;
		_logger.LogDebug("FHeapNode(key={Key}): Left → {Left}", key, Left);
        Parent = Child = null;
    }

    /// <summary>
    ///     Gets or sets the data of this node.
    /// </summary>
    public T Key { get; set; }

    /// <summary>
    ///     Gets or sets a reference to the parent.
    /// </summary>
    public FHeapNode<T>? Parent { get; set; }

    /// <summary>
    ///     Gets or sets a reference to the left sibling.
    /// </summary>
    public FHeapNode<T> Left { get; set; }

    /// <summary>
    ///     Gets or sets a reference to the right sibling.
    /// </summary>
    public FHeapNode<T> Right { get; set; }

    /// <summary>
    ///     Gets or sets a reference to one of the children, there may be more that
    ///     are siblings the this child, however this structure only maintains a
    ///     reference to one of them.
    /// </summary>
    public FHeapNode<T>? Child { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether this node has been marked,
    ///     used in some operations.
    /// </summary>
    public bool Mark { get; set; }

    /// <summary>
    ///     Gets or sets the number of nodes in the child linked list.
    /// </summary>
    public int Degree { get; set; }

    public void SetSiblings(FHeapNode<T> left, FHeapNode<T> right)
    {
		_logger.LogError(">>> Entering SetSiblings(left={Left},right={Right})", left, right);
        Left = left;
        Right = right;
		_logger.LogDebug("SetSiblings(left={Left},right={Right}): Left → {Left1}", left, right, Left);
    _logger.LogWarning("<<< Exiting SetSiblings(left={Left},right={Right})", left, right);
    }

    /// <summary>
    ///     A helper function to add a node to the right of this one in the current
    ///     circularly doubly linked list.
    /// </summary>
    /// <param name="node">A node to go in the linked list.</param>
    public void AddRight(FHeapNode<T> node)
    {
		_logger.LogError(">>> Entering AddRight(node={Node})", node);
        Right.Left = node;
        node.Right = Right;
		_logger.LogDebug("AddRight(node={Node}): node.Right → {Arg1}", node, node.Right);
		_logger.LogDebug("AddRight(node={Node}): Right.Left → {Arg1}", node, Right.Left);
    _logger.LogWarning("<<< Exiting AddRight(node={Node})", node);
        node.Left = this;
		_logger.LogDebug("AddRight(node={Node}): node.Left → {Arg1}", node, node.Left);
        Right = node;
		_logger.LogDebug("AddRight(node={Node}): Right → {Right}", node, Right);
    }

    /// <summary>
    ///     Similar to AddRight, but adds the node as a sibling to the child node.
    /// </summary>
    /// <param name="node">A node to add to the child list of this node.</param>
    public void AddChild(FHeapNode<T> node)
    {
		_logger.LogError(">>> Entering AddChild(node={Node})", node);
        Degree++;

        if (Child == null)
        {
            Child = node;
			_logger.LogDebug("AddChild(node={Node}): Child → {Child}", node, Child);
            Child.Parent = this;
			_logger.LogDebug("AddChild(node={Node}): Child.Parent → {Arg1}", node, Child.Parent);
            Child.Left = Child.Right = Child;
			_logger.LogDebug("AddChild(node={Node}): Child.Left → {Arg1}", node, Child.Left);

            return;
        }

        Child.AddRight(node);
		_logger.LogWarning("<<< Exiting AddChild(node={Node})", node);
    }

    /// <summary>
    ///     Remove this item from the linked list it's in.
    /// </summary>
    public void Remove()
    {
		_logger.LogError(">>> Entering Remove()");
		_logger.LogDebug("Remove(): Right.Left → {Arg0}", Right.Left);
        Left.Right = Right;
        Right.Left = Left;
		_logger.LogDebug("Remove(): Left.Right → {Arg0}", Left.Right);
    _logger.LogWarning("<<< Exiting Remove()");
    }

    /// <summary>
    ///     Combine the linked list that <c>otherList</c> sits inside, with the
    ///     linked list this is in. Do this by cutting the link between this node,
    ///     and the node to the right of this, and inserting the contents of the
    ///     otherList in between.
    /// </summary>
    /// <param name="otherList">
    ///     A node from another list whose elements we want
    ///     to concatenate to this list.
    /// </param>
    public void ConcatenateRight(FHeapNode<T> otherList)
    {
		_logger.LogError(">>> Entering ConcatenateRight(otherList={OtherList})", otherList);
        Right.Left = otherList.Left;
        otherList.Left.Right = Right;
		_logger.LogDebug("ConcatenateRight(otherList={OtherList}): Right.Left → {Arg1}", otherList, Right.Left);

		_logger.LogWarning("<<< Exiting ConcatenateRight(otherList={OtherList})", otherList);
        Right = otherList;
		_logger.LogDebug("ConcatenateRight(otherList={OtherList}): Right → {Right}", otherList, Right);
        otherList.Left = this;
		_logger.LogDebug("ConcatenateRight(otherList={OtherList}): otherList.Left → {Arg1}", otherList, otherList.Left);
    }
}
