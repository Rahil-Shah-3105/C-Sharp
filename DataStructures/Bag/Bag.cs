using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace DataStructures.Bag;

/// <summary>
/// Implementation of a Bag (or multiset) data structure using a basic linked list.
/// </summary>
/// <remarks>
/// A bag (or multiset, or mset) is a modification of the concept of a set that, unlike a set, allows for multiple instances for each of its elements.
/// The number of instances given for each element is called the multiplicity of that element in the multiset.
/// As a consequence, an infinite number of multisets exist that contain only elements a and b, but vary in the multiplicities of their elements.
/// See https://en.wikipedia.org/wiki/Multiset for more information.
/// </remarks>
/// <typeparam name="T">Generic Type.</typeparam>
public class Bag<T> : IEnumerable<T> where T : notnull
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private BagNode<T>? head;
    private int totalCount;

    /// <summary>
    /// Initializes a new instance of the <see cref="Bag{T}" /> class.
    /// </summary>
    public Bag()
    {
		_logger.LogDebug(">>> Entering Bag()");
        head = null;
        totalCount = 0;
		_logger.LogDebug("Bag(): head → {Head}", head);
		_logger.LogDebug("Bag(): totalCount → {TotalCount}", totalCount);
		_logger.LogDebug("<<< Exiting Bag()");
    }

    /// <summary>
    /// Adds an item to the bag. If the item already exists, increases its multiplicity.
    /// </summary>
    public void Add(T item)
    {
		_logger.LogDebug(">>> Entering Add(item={Item})", item);
        // If the bag is empty, create the first node
        if (head == null)
        {
            head = new BagNode<T>(item);
            totalCount = 1;
			_logger.LogDebug("Add(item={Item}): head → {Head}", item, head);
			_logger.LogDebug("Add(item={Item}): totalCount → {TotalCount}", item, totalCount);
            return;
        }

        // Check if item already exists
        var current = head;
		_logger.LogDebug("Add(item={Item}): current → {Current}", item, current);
        BagNode<T>? previous = null;

        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Item, item))
            {
                current.Multiplicity++;
                totalCount++;
                return;
            }

            previous = current;
			_logger.LogDebug("Add(item={Item}): previous → {Previous}", item, previous);
            current = current.Next;
        }

        previous!.Next = new BagNode<T>(item);
        totalCount++;
		_logger.LogDebug("<<< Exiting Add(item={Item})", item);
    }

    /// <summary>
    /// Clears the bag.
    /// </summary>
    public void Clear()
    {
		_logger.LogDebug(">>> Entering Clear()");
        head = null;
        totalCount = 0;
		_logger.LogDebug("Clear(): head → {Head}", head);
		_logger.LogDebug("Clear(): totalCount → {TotalCount}", totalCount);
		_logger.LogDebug("<<< Exiting Clear()");
    }

    /// <summary>
    /// Gets the number of items in the bag.
    /// </summary>
    public int Count => totalCount;

    /// <summary>
    /// Returns a boolean indicating whether the bag is empty.
    /// </summary>
    public bool IsEmpty() => head == null;

    /// <summary>
    /// Returns an enumerator that iterates through the bag.
    /// </summary>
    public IEnumerator<T> GetEnumerator()
    {
		_logger.LogDebug(">>> Entering GetEnumerator()");
        var current = head;

		_logger.LogDebug("GetEnumerator(): current → {Current}", current);
        while (current != null)
        {
            // Yield the item as many times as its multiplicity, pretending they are separate items
            for (var i = 0; i < current.Multiplicity; i++)
            {
                yield return current.Item;
            }

            current = current.Next;
        }
		_logger.LogDebug("<<< Exiting GetEnumerator()");
    }

    /// <summary>
    /// Returns an enumerator that iterates through the bag.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
