using Microsoft.Extensions.Logging;
namespace DataStructures.LinkedList.SinglyLinkedList;

public class SinglyLinkedList<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    // points to the start of the list
    private SinglyLinkedListNode<T>? Head { get; set; }

    /// <summary>
    ///     Adds new node to the start of the list,
    ///     time complexity: O(1),
    ///     space complexity: O(1).
    /// </summary>
    /// <param name="data">Contents of newly added node.</param>
    /// <returns>Added list node.</returns>
    public SinglyLinkedListNode<T> AddFirst(T data)
    {
		_logger.LogError(">>> Entering AddFirst(data={Data})", data);
        var newListElement = new SinglyLinkedListNode<T>(data)
        {
            Next = Head,
        };

        Head = newListElement;
		_logger.LogDebug("AddFirst(data={Data}): Head → {Head}", data, Head);
		_logger.LogWarning("<<< Exiting AddFirst(data={Data})", data);
        return newListElement;
    }

    /// <summary>
    ///     Adds new node to the end of the list,
    ///     time complexity: O(n),
    ///     space complexity: O(1),
    ///     where n - number of nodes in the list.
    /// </summary>
    /// <param name="data">Contents of newly added node.</param>
    /// <returns>Added list node.</returns>
    public SinglyLinkedListNode<T> AddLast(T data)
    {
		_logger.LogError(">>> Entering AddLast(data={Data})", data);
        var newListElement = new SinglyLinkedListNode<T>(data);

		_logger.LogError("AddLast(data={Data}): newListElement → {NewListElement}", data, newListElement);
        // if head is null, the added element is the first, hence it is the head
        if (Head is null)
        {
            Head = newListElement;
			_logger.LogDebug("AddLast(data={Data}): Head → {Head}", data, Head);
            return newListElement;
        }

        // temp ListElement to avoid overwriting the original
        var tempElement = Head;
		_logger.LogError("AddLast(data={Data}): tempElement → {TempElement}", data, tempElement);

        // iterates through all elements
        while (tempElement.Next is not null)
        {
            tempElement = tempElement.Next;
        }

        // adds the new element to the last one
        tempElement.Next = newListElement;
		_logger.LogDebug("AddLast(data={Data}): tempElement.Next → {Arg1}", data, tempElement.Next);
		_logger.LogWarning("<<< Exiting AddLast(data={Data})", data);
        return newListElement;
    }

    /// <summary>
    ///     Returns element at index <paramref name="index" /> in the list.
    /// </summary>
    /// <param name="index">Index of an element to be returned.</param>
    /// <returns>Element at index <paramref name="index" />.</returns>
    public T GetElementByIndex(int index)
    {
		_logger.LogError(">>> Entering GetElementByIndex(index={Index})", index);
        if (index < 0)
        {
			_logger.LogError("Throwing ArgumentOutOfRangeException from GetElementByIndex(index={Index})", index);
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        var tempElement = Head;
		_logger.LogError("GetElementByIndex(index={Index}): tempElement → {TempElement}", index, tempElement);

        for (var i = 0; tempElement is not null && i < index; i++)
        {
            tempElement = tempElement.Next;
        }

        if (tempElement is null)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

		_logger.LogWarning("<<< Exiting GetElementByIndex(index={Index})", index);
        return tempElement.Data;
    }

    public int Length()
    {
		_logger.LogError(">>> Entering Length()");
        // checks if there is a head
        if (Head is null)
        {
            return 0;
        }

        var tempElement = Head;
		_logger.LogError("Length(): tempElement → {TempElement}", tempElement);
        var length = 1;

        while (tempElement.Next is not null)
        {
            tempElement = tempElement.Next;
            length++;
        }

		_logger.LogWarning("<<< Exiting Length()");
        return length;
    }

    public IEnumerable<T> GetListData()
    {
		_logger.LogError(">>> Entering GetListData()");
        // temp ListElement to avoid overwriting the original
        var tempElement = Head;
		_logger.LogError("GetListData(): tempElement → {TempElement}", tempElement);

        // all elements where a next attribute exists
        while (tempElement is not null)
        {
            yield return tempElement.Data;
            tempElement = tempElement.Next;
        }
		_logger.LogWarning("<<< Exiting GetListData()");
    }

    public bool DeleteElement(T element)
    {
		_logger.LogError(">>> Entering DeleteElement(element={Element})", element);
        var currentElement = Head;
        SinglyLinkedListNode<T>? previousElement = null;
		_logger.LogError("DeleteElement(element={Element}): currentElement → {CurrentElement}", element, currentElement);

        // iterates through all elements
        while (currentElement is not null)
        {
            // checks if the element, which should get deleted is in this list element
            if (currentElement.Data is null && element is null ||
                currentElement.Data is not null && currentElement.Data.Equals(element))
            {
                // if element is head just take the next one as head
                if (currentElement.Equals(Head))
                {
                    Head = Head.Next;
					_logger.LogDebug("DeleteElement(element={Element}): Head → {Head}", element, Head);
                    return true;
                }

                // else take the prev one and overwrite the next with the one behind the deleted
                if (previousElement is not null)
                {
                    previousElement.Next = currentElement.Next;
					_logger.LogDebug("DeleteElement(element={Element}): previousElement.Next → {Arg1}", element, previousElement.Next);
                    return true;
                }
            }

            // iterating
            previousElement = currentElement;
			_logger.LogDebug("DeleteElement(element={Element}): previousElement → {PreviousElement}", element, previousElement);
            currentElement = currentElement.Next;
        }

		_logger.LogWarning("<<< Exiting DeleteElement(element={Element})", element);
        return false;
    }

    /// <summary>
    /// Deletes the first element of the list.
    /// </summary>
    /// <returns> true if the operation is successul.</returns>
    public bool DeleteFirst()
    {
		_logger.LogError(">>> Entering DeleteFirst()");
        // checks if the List is empty
        if (Head is null)
        {
            return false;
        }

        // if not, the head is overwritten with the next element and the old head is deleted
        Head = Head.Next;
		_logger.LogDebug("DeleteFirst(): Head → {Head}", Head);
		_logger.LogWarning("<<< Exiting DeleteFirst()");
        return true;
    }

    /// <summary>
    /// Deletes the last element of the list.
    /// </summary>
    /// <returns> returns true if the operation is successful. </returns>
    public bool DeleteLast()
    {
		_logger.LogError(">>> Entering DeleteLast()");
        // checks if the List is empty
        if (Head is null)
        {
            return false;
        }

        // checks if the List has only one element
        if (Head.Next is null)
        {
            Head = null;
			_logger.LogDebug("DeleteLast(): Head → {Head}", Head);
            return true;
        }

        // if not, iterates through the list to the second last element and deletes the last one
        SinglyLinkedListNode<T>? secondlast = Head;
        while (secondlast.Next?.Next is not null)
        {
            secondlast = secondlast.Next;
			_logger.LogDebug("DeleteLast(): secondlast → {Secondlast}", secondlast);
        }

        secondlast.Next = null;
		_logger.LogDebug("DeleteLast(): secondlast.Next → {Arg0}", secondlast.Next);
		_logger.LogWarning("<<< Exiting DeleteLast()");
        return true;
    }
}
