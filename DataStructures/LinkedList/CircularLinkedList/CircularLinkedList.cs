using Microsoft.Extensions.Logging;
namespace DataStructures.LinkedList.CircularLinkedList
{
    /// <summary>
    /// CircularLinkedList.
    /// @author Mohit Singh. <a href="https://github.com/mohit-gogitter">mohit-gogitter</a>
    /// </summary>
    /// <typeparam name="T">The generic type parameter.</typeparam>
    public class CircularLinkedList<T>
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        /// <summary>
        /// Points to the last node in the Circular Linked List.
        /// </summary>
        private CircularLinkedListNode<T>? tail;

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularLinkedList{T}"/> class.
        /// </summary>
        public CircularLinkedList()
        {
			_logger.LogDebug(">>> Entering CircularLinkedList()");
            tail = null;
			_logger.LogDebug("CircularLinkedList(): tail → {Tail}", tail);
			_logger.LogDebug("<<< Exiting CircularLinkedList()");
        }

        /// <summary>
        /// Gets the head node (tail.Next) of the Circular Linked List.
        /// </summary>
        public CircularLinkedListNode<T>? GetHead()
        {
			_logger.LogDebug(">>> Entering GetHead()");
			_logger.LogDebug("<<< Exiting GetHead()");
            return tail?.Next;
        }

        /// <summary>
        /// Determines whether the Circular Linked List is empty.
        /// </summary>
        /// <returns>True if the list is empty; otherwise, false.</returns>
        public bool IsEmpty()
        {
			_logger.LogDebug(">>> Entering IsEmpty()");
			_logger.LogDebug("<<< Exiting IsEmpty()");
            return tail == null;
        }

        /// <summary>
        /// Inserts a new node at the beginning of the Circular Linked List.
        /// </summary>
        /// <param name="data">The data to insert into the new node.</param>
        public void InsertAtBeginning(T data)
        {
			_logger.LogDebug(">>> Entering InsertAtBeginning(data={Data})", data);
            var newNode = new CircularLinkedListNode<T>(data);
            if (IsEmpty())
            {
                tail = newNode;
				_logger.LogDebug("InsertAtBeginning(data={Data}): tail → {Tail}", data, tail);
				_logger.LogDebug("InsertAtBeginning(data={Data}): tail.Next → {Arg1}", data, tail.Next);
                tail.Next = tail;
            }
            else
            {
                newNode.Next = tail!.Next;
				_logger.LogDebug("InsertAtBeginning(data={Data}): newNode.Next → {Arg1}", data, newNode.Next);
                tail.Next = newNode;
            }
			_logger.LogDebug("<<< Exiting InsertAtBeginning(data={Data})", data);
        }

        /// <summary>
        /// Inserts a new node at the end of the Circular Linked List.
        /// </summary>
        /// <param name="data">The data to insert into the new node.</param>
        public void InsertAtEnd(T data)
        {
			_logger.LogDebug(">>> Entering InsertAtEnd(data={Data})", data);
            var newNode = new CircularLinkedListNode<T>(data);
            if (IsEmpty())
            {
                tail = newNode;
                tail.Next = tail;
				_logger.LogDebug("InsertAtEnd(data={Data}): tail → {Tail}", data, tail);
				_logger.LogDebug("InsertAtEnd(data={Data}): tail.Next → {Arg1}", data, tail.Next);
            }
            else
            {
                newNode.Next = tail!.Next;
				_logger.LogDebug("InsertAtEnd(data={Data}): newNode.Next → {Arg1}", data, newNode.Next);
                tail.Next = newNode;
                tail = newNode;
            }
			_logger.LogDebug("<<< Exiting InsertAtEnd(data={Data})", data);
        }

        /// <summary>
        /// Inserts a new node after a specific value in the list.
        /// </summary>
        /// <param name="value">The value to insert the node after.</param>
        /// <param name="data">The data to insert into the new node.</param>
        public void InsertAfter(T value, T data)
        {
			_logger.LogDebug(">>> Entering InsertAfter(value={Value},data={Data})", value, data);
            if (IsEmpty())
            {
				_logger.LogWarning("Throwing InvalidOperationException from InsertAfter(value={Value},data={Data})", value, data);
                throw new InvalidOperationException("List is empty.");
            }

            var current = tail!.Next;
			_logger.LogDebug("InsertAfter(value={Value},data={Data}): current → {Current}", value, data, current);
            do
            {
                if (current!.Data!.Equals(value))
                {
                    var newNode = new CircularLinkedListNode<T>(data);
					_logger.LogDebug("InsertAfter(value={Value},data={Data}): newNode → {NewNode}", value, data, newNode);
					_logger.LogDebug("InsertAfter(value={Value},data={Data}): newNode.Next → {Arg2}", value, data, newNode.Next);
					_logger.LogDebug("InsertAfter(value={Value},data={Data}): current.Next → {Arg2}", value, data, current.Next);
                    newNode.Next = current.Next;
                    current.Next = newNode;

                    return;
                }

                current = current.Next;
            }
            while (current != tail.Next);
			_logger.LogDebug("<<< Exiting InsertAfter(value={Value},data={Data})", value, data);
        }

        /// <summary>
        /// Deletes a node with a specific value from the list.
        /// </summary>
        /// <param name="value">The value of the node to delete.</param>
        public void DeleteNode(T value)
        {
			_logger.LogDebug(">>> Entering DeleteNode(value={Value})", value);
            if (IsEmpty())
            {
				_logger.LogWarning("Throwing InvalidOperationException from DeleteNode(value={Value})", value);
                throw new InvalidOperationException("List is empty.");
            }

            var current = tail!.Next;
			_logger.LogDebug("DeleteNode(value={Value}): current → {Current}", value, current);
            var previous = tail;
			_logger.LogDebug("DeleteNode(value={Value}): previous → {Previous}", value, previous);

            do
            {
                if (current!.Data!.Equals(value))
                {
                    if (current == tail && current.Next == tail)
                    {
                        tail = null;
                    }
                    else if (current == tail)
                    {
                        previous!.Next = tail.Next;
                        tail = previous;
                    }
                    else if (current == tail.Next)
                    {
                        tail.Next = current.Next;
                    }
                    else
                    {
                        previous!.Next = current.Next;
                    }

                    return;
                }

                previous = current;
                current = current.Next;
            }
            while (current != tail!.Next);
			_logger.LogDebug("<<< Exiting DeleteNode(value={Value})", value);
        }
    }
}
