using Utilities.Exceptions;
using Microsoft.Extensions.Logging;

namespace DataStructures.LinkedList.DoublyLinkedList;

/// <summary>
///     Similar to a Singly Linked List but each node contains a refenrence to the previous node in the list.
///     <see cref="System.Collections.Generic.LinkedList{T}" /> is a doubly linked list.
///     Compared to singly linked lists it can be traversed forwards and backwards.
///     Adding a node to a doubly linked list is simpler because ever node contains a reference to the previous node.
/// </summary>
/// <typeparam name="T">Generic type.</typeparam>
public class DoublyLinkedList<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Initializes a new instance of the <see cref="DoublyLinkedList{T}" /> class.
    /// </summary>
    /// <param name="data"> Data of the original head of the list.</param>
    public DoublyLinkedList(T data)
    {
		_logger.LogError(">>> Entering DoublyLinkedList(data={Data})", data);
        Head = new DoublyLinkedListNode<T>(data);
        Tail = Head;
		_logger.LogDebug("DoublyLinkedList(data={Data}): Tail → {Tail}", data, Tail);
		_logger.LogDebug("DoublyLinkedList(data={Data}): Head → {Head}", data, Head);
        Count = 1;
		_logger.LogError("DoublyLinkedList(data={Data}): Count → {Count}", data, Count);
		_logger.LogWarning("<<< Exiting DoublyLinkedList(data={Data})", data);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DoublyLinkedList{T}" /> class from an enumerable.
    /// </summary>
    /// <param name="data"> Enumerable of data to be stored in the list.</param>
    public DoublyLinkedList(IEnumerable<T> data)
    {
        foreach (var d in data)
        {
            Add(d);
        }
    }

    /// <summary>
    ///     Gets the amount of nodes in the list.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    ///     Gets or sets the first node of the list.
    /// </summary>
    private DoublyLinkedListNode<T>? Head { get; set; }

    /// <summary>
    ///     Gets or sets the last node of the list.
    /// </summary>
    private DoublyLinkedListNode<T>? Tail { get; set; }

    /// <summary>
    ///     Replaces the Head of the list with the new value.
    /// </summary>
    /// <param name="data"> Value for the new Head of the list.</param>
    /// <returns>The new Head node.</returns>
    public DoublyLinkedListNode<T> AddHead(T data)
    {
		_logger.LogError(">>> Entering AddHead(data={Data})", data);
        var node = new DoublyLinkedListNode<T>(data);

		_logger.LogError("AddHead(data={Data}): node → {Node}", data, node);
        if (Head is null)
        {
            Head = node;
			_logger.LogError("AddHead(data={Data}): Head → {Head}", data, Head);
			_logger.LogDebug("AddHead(data={Data}): Head → {Head}", data, Head);
            Tail = node;
            Count = 1;
			_logger.LogError("AddHead(data={Data}): Count → {Count}", data, Count);
			_logger.LogDebug("AddHead(data={Data}): Tail → {Tail}", data, Tail);
			_logger.LogDebug("AddHead(data={Data}): Count → {Count}", data, Count);
            return node;
        }

        Head.Previous = node;
		_logger.LogDebug("AddHead(data={Data}): Head.Previous → {Arg1}", data, Head.Previous);
        node.Next = Head;
		_logger.LogDebug("AddHead(data={Data}): node.Next → {Arg1}", data, node.Next);
        Head = node;
        Count++;
		_logger.LogWarning("<<< Exiting AddHead(data={Data})", data);
        return node;
    }

    /// <summary>
    ///     Adds a new value at the end of the list.
    /// </summary>
    /// <param name="data"> New value to be added to the list.</param>
    /// <returns>The new node created based on the new value.</returns>
    public DoublyLinkedListNode<T> Add(T data)
    {
		_logger.LogError(">>> Entering Add(data={Data})", data);
        if (Head is null)
        {
            return AddHead(data);
        }

        var node = new DoublyLinkedListNode<T>(data);
		_logger.LogError("Add(data={Data}): node → {Node}", data, node);
        Tail!.Next = node;
        node.Previous = Tail;
		_logger.LogDebug("Add(data={Data}): node.Previous → {Arg1}", data, node.Previous);
        Tail = node;
		_logger.LogDebug("Add(data={Data}): Tail → {Tail}", data, Tail);
        Count++;
		_logger.LogWarning("<<< Exiting Add(data={Data})", data);
        return node;
    }

    /// <summary>
    ///     Adds a new value after an existing node.
    /// </summary>
    /// <param name="data"> New value to be added to the list.</param>
    /// <param name="existingNode"> An existing node in the list.</param>
    /// <returns>The new node created based on the new value.</returns>
    public DoublyLinkedListNode<T> AddAfter(T data, DoublyLinkedListNode<T> existingNode)
    {
		_logger.LogError(">>> Entering AddAfter(data={Data},existingNode={ExistingNode})", data, existingNode);
        if (existingNode == Tail)
        {
            return Add(data);
        }

        var node = new DoublyLinkedListNode<T>(data);
		_logger.LogError("AddAfter(data={Data},existingNode={ExistingNode}): node → {Node}", data, existingNode, node);
        node.Next = existingNode.Next;
		_logger.LogDebug("AddAfter(data={Data},existingNode={ExistingNode}): node.Next → {Arg2}", data, existingNode, node.Next);
        node.Previous = existingNode;
		_logger.LogDebug("AddAfter(data={Data},existingNode={ExistingNode}): node.Previous → {Arg2}", data, existingNode, node.Previous);
        existingNode.Next = node;
		_logger.LogDebug("AddAfter(data={Data},existingNode={ExistingNode}): existingNode.Next → {Arg2}", data, existingNode, existingNode.Next);

        if (node.Next is not null)
        {
            node.Next.Previous = node;
        }

        Count++;
		_logger.LogWarning("<<< Exiting AddAfter(data={Data},existingNode={ExistingNode})", data, existingNode);
        return node;
    }

    /// <summary>
    ///     Gets an enumerable based on the data in the list.
    /// </summary>
    /// <returns>The data in the list in an IEnumerable. It can used to create a list or an array with LINQ.</returns>
    public IEnumerable<T> GetData()
    {
		_logger.LogError(">>> Entering GetData()");
        var current = Head;
		_logger.LogError("GetData(): current → {Current}", current);
        while (current is not null)
        {
            yield return current.Data;
            current = current.Next;
        }
		_logger.LogWarning("<<< Exiting GetData()");
    }

    /// <summary>
    ///     Gets an enumerable based on the data in the list reversed.
    /// </summary>
    /// <returns>The data in the list in an IEnumerable. It can used to create a list or an array with LINQ.</returns>
    public IEnumerable<T> GetDataReversed()
    {
		_logger.LogError(">>> Entering GetDataReversed()");
        var current = Tail;
		_logger.LogError("GetDataReversed(): current → {Current}", current);
        while (current is not null)
        {
            yield return current.Data;
            current = current.Previous;
        }
		_logger.LogWarning("<<< Exiting GetDataReversed()");
    }

    /// <summary>
    ///     Reverses the list. Because of how doubly linked list are structured this is not a complex action.
    /// </summary>
    public void Reverse()
    {
		_logger.LogError(">>> Entering Reverse()");
        var current = Head;
        DoublyLinkedListNode<T>? temp = null;
		_logger.LogError("Reverse(): current → {Current}", current);

        while (current is not null)
        {
            temp = current.Previous;
            current.Previous = current.Next;
			_logger.LogDebug("Reverse(): current.Previous → {Arg0}", current.Previous);
            current.Next = temp;
			_logger.LogDebug("Reverse(): current.Next → {Arg0}", current.Next);
            current = current.Previous;
        }

        Tail = Head;
		_logger.LogDebug("Reverse(): Tail → {Tail}", Tail);

        // temp can be null on empty list
        if (temp is not null)
        {
            Head = temp.Previous;
			_logger.LogDebug("Reverse(): Head → {Head}", Head);
        }
		_logger.LogWarning("<<< Exiting Reverse()");
    }

    /// <summary>
    ///     Looks for a node in the list that contains the value of the parameter.
    /// </summary>
    /// <param name="data"> Value to be looked for in a node.</param>
    /// <returns>The node in the list the has the paramater as a value or null if not found.</returns>
    public DoublyLinkedListNode<T> Find(T data)
    {
		_logger.LogError(">>> Entering Find(data={Data})", data);
        var current = Head;
		_logger.LogError("Find(data={Data}): current → {Current}", data, current);
        while (current is not null)
        {
            if (current.Data is null && data is null || current.Data is not null && current.Data.Equals(data))
            {
                return current;
            }

            current = current.Next;
        }

		_logger.LogError("Throwing ItemNotFoundException from Find(data={Data})", data);
		_logger.LogWarning("<<< Exiting Find(data={Data})", data);
        throw new ItemNotFoundException();
    }

    /// <summary>
    ///     Looks for a node in the list that contains the value of the parameter.
    /// </summary>
    /// <param name="position"> Position in the list.</param>
    /// <returns>The node in the list the has the paramater as a value or null if not found.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when position is negative or out range of the list.</exception>
    public DoublyLinkedListNode<T> GetAt(int position)
    {
		_logger.LogError(">>> Entering GetAt(position={Position})", position);
        if (position < 0 || position >= Count)
        {
			_logger.LogError("Throwing ArgumentOutOfRangeException from GetAt(position={Position})", position);
            throw new ArgumentOutOfRangeException($"Max count is {Count}");
        }

        var current = Head;
		_logger.LogError("GetAt(position={Position}): current → {Current}", position, current);
        for (var i = 0; i < position; i++)
        {
            current = current!.Next;
        }

		_logger.LogWarning("<<< Exiting GetAt(position={Position})", position);
        return current ?? throw new ArgumentOutOfRangeException($"{nameof(position)} must be an index in the list");
    }

    /// <summary>
    ///     Removes the Head and replaces it with the second node in the list.
    /// </summary>
    public void RemoveHead()
    {
		_logger.LogError(">>> Entering RemoveHead()");
        if (Head is null)
        {
			_logger.LogError("Throwing InvalidOperationException from RemoveHead()");
            throw new InvalidOperationException();
        }

        Head = Head.Next;
		_logger.LogDebug("RemoveHead(): Head → {Head}", Head);
        if (Head is null)
        {
            Tail = null;
			_logger.LogDebug("RemoveHead(): Tail → {Tail}", Tail);
            Count = 0;
			_logger.LogError("RemoveHead(): Count → {Count}", Count);
			_logger.LogDebug("RemoveHead(): Count → {Count}", Count);
            return;
        }

        Head.Previous = null;
		_logger.LogDebug("RemoveHead(): Head.Previous → {Arg0}", Head.Previous);
        Count--;
		_logger.LogWarning("<<< Exiting RemoveHead()");
    }

    /// <summary>
    ///     Removes the last node in the list.
    /// </summary>
    public void Remove()
    {
		_logger.LogError(">>> Entering Remove()");
        if (Tail is null)
        {
			_logger.LogError("Throwing InvalidOperationException from Remove()");
            throw new InvalidOperationException("Cannot prune empty list");
        }

        Tail = Tail.Previous;
		_logger.LogDebug("Remove(): Tail → {Tail}", Tail);
        if (Tail is null)
        {
            Head = null;
			_logger.LogDebug("Remove(): Head → {Head}", Head);
            Count = 0;
			_logger.LogError("Remove(): Count → {Count}", Count);
			_logger.LogDebug("Remove(): Count → {Count}", Count);
            return;
        }

        Tail.Next = null;
		_logger.LogDebug("Remove(): Tail.Next → {Arg0}", Tail.Next);
        Count--;
    }

    /// <summary>
    ///     Removes specific node.
    /// </summary>
    /// <param name="node"> Node to be removed.</param>
    public void RemoveNode(DoublyLinkedListNode<T> node)
    {
		_logger.LogError(">>> Entering RemoveNode(node={Node})", node);
        if (node == Head)
        {
            RemoveHead();
            return;
        }

        if (node == Tail)
        {
            Remove();
            return;
        }

        if (node.Previous is null || node.Next is null)
        {
			_logger.LogError("Throwing ArgumentException from RemoveNode(node={Node})", node);
            throw new ArgumentException(
                $"{nameof(node)} cannot have Previous or Next null if it's an internal node");
        }

        node.Previous.Next = node.Next;
        node.Next.Previous = node.Previous;
        Count--;
		_logger.LogWarning("<<< Exiting RemoveNode(node={Node})", node);
    }

    /// <summary>
    ///     Removes a node that contains the data from the parameter.
    /// </summary>
    /// <param name="data"> Data to be removed form the list.</param>
    public void Remove(T data)
    {
		_logger.LogError(">>> Entering Remove(data={Data})", data);
        var node = Find(data);
		_logger.LogError("Remove(data={Data}): node → {Node}", data, node);
        RemoveNode(node);
    }

    /// <summary>
    ///     Looks for the index of the node with the parameter as data.
    /// </summary>
    /// <param name="data"> Data to look for.</param>
    /// <returns>Returns the index of the node if it is found or -1 if the node is not found.</returns>
    public int IndexOf(T data)
    {
		_logger.LogError(">>> Entering IndexOf(data={Data})", data);
        var current = Head;
        var index = 0;
		_logger.LogError("IndexOf(data={Data}): current → {Current}", data, current);
        while (current is not null)
        {
            if (current.Data is null && data is null || current.Data is not null && current.Data.Equals(data))
            {
                return index;
            }

            index++;
            current = current.Next;
        }

		_logger.LogWarning("<<< Exiting IndexOf(data={Data})", data);
        return -1;
    }

    /// <summary>
    ///     List contains a node that has the parameter as data.
    /// </summary>
    /// <param name="data"> Node to be removed.</param>
    /// <returns>True if the node is found. False if it isn't.</returns>
    public bool Contains(T data)
    {
        _logger.LogError(">>> Entering Contains(data={Data})", data);
        _logger.LogWarning("<<< Exiting Contains(data={Data})", data);
        return IndexOf(data) != -1;
    }
}
