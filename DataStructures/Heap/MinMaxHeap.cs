using Microsoft.Extensions.Logging;
namespace DataStructures.Heap;

/// <summary>
///     This class implements min-max heap.
///     It provides functionality of both min-heap and max-heap with the same time complexity.
///     Therefore it provides constant time retrieval and logarithmic time removal
///     of both the minimum and maximum elements in it.
/// </summary>
/// <typeparam name="T">Generic type.</typeparam>
public class MinMaxHeap<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly List<T> heap;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MinMaxHeap{T}" /> class that contains
    ///     elements copied from a specified enumerable collection and that uses a specified comparer.
    /// </summary>
    /// <param name="collection">The enumerable collection to be copied.</param>
    /// <param name="comparer">The default comparer to use for comparing objects.</param>
    public MinMaxHeap(IEnumerable<T>? collection = null, IComparer<T>? comparer = null)
    {
		_logger.LogDebug(">>> Entering MinMaxHeap(collection={Collection},comparer={Comparer})", collection, comparer);
        Comparer = comparer ?? Comparer<T>.Default;
        collection ??= Enumerable.Empty<T>();
		_logger.LogDebug("MinMaxHeap(collection={Collection},comparer={Comparer}): Comparer → {Comparer1}", collection, comparer, Comparer);

        heap = collection.ToList();
        for (var i = Count / 2 - 1; i >= 0; --i)
        {
            PushDown(i);
        }
		_logger.LogDebug("<<< Exiting MinMaxHeap(collection={Collection},comparer={Comparer})", collection, comparer);
    }

    /// <summary>
    ///     Gets the  <see cref="IComparer{T}" />. object that is used to order the values in the <see cref="MinMaxHeap{T}" />.
    /// </summary>
    public IComparer<T> Comparer { get; }

    /// <summary>
    ///     Gets the number of elements in the <see cref="MinMaxHeap{T}" />.
    /// </summary>
    public int Count => heap.Count;

    /// <summary>
    ///     Adds an element to the heap.
    /// </summary>
    /// <param name="item">The element to add to the heap.</param>
    public void Add(T item)
    {
		_logger.LogDebug(">>> Entering Add(item={Item})", item);
        heap.Add(item);
        PushUp(Count - 1);
		_logger.LogDebug("<<< Exiting Add(item={Item})", item);
    }

    /// <summary>
    ///     Removes the maximum node from the heap and returns its value.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if heap is empty.</exception>
    /// <returns>Value of the removed maximum node.</returns>
    public T ExtractMax()
    {
		_logger.LogDebug(">>> Entering ExtractMax()");
        if (Count == 0)
        {
			_logger.LogWarning("Throwing InvalidOperationException from ExtractMax()");
            throw new InvalidOperationException("Heap is empty");
        }

        var max = GetMax();
		_logger.LogDebug("ExtractMax(): max → {Max}", max);
        RemoveNode(GetMaxNodeIndex());
		_logger.LogDebug("<<< Exiting ExtractMax()");
        return max;
    }

    /// <summary>
    ///     Removes the minimum node from the heap and returns its value.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if heap is empty.</exception>
    /// <returns>Value of the removed minimum node.</returns>
    public T ExtractMin()
    {
		_logger.LogDebug(">>> Entering ExtractMin()");
        if (Count == 0)
        {
			_logger.LogWarning("Throwing InvalidOperationException from ExtractMin()");
            throw new InvalidOperationException("Heap is empty");
        }

        var min = GetMin();
		_logger.LogDebug("ExtractMin(): min → {Min}", min);
        RemoveNode(0);
		_logger.LogDebug("<<< Exiting ExtractMin()");
        return min;
    }

    /// <summary>
    ///     Gets the maximum value in the heap, as defined by the comparer.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if heap is empty.</exception>
    /// <returns>The maximum value in the heap.</returns>
    public T GetMax()
    {
		_logger.LogDebug(">>> Entering GetMax()");
        if (Count == 0)
        {
			_logger.LogWarning("Throwing InvalidOperationException from GetMax()");
		_logger.LogDebug("<<< Exiting GetMax()");
            throw new InvalidOperationException("Heap is empty");
        }

        return heap[GetMaxNodeIndex()];
    }

    /// <summary>
    ///     Gets the minimum value in the heap, as defined by the comparer.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if heap is empty.</exception>
    /// <returns>The minimum value in the heap.</returns>
    public T GetMin()
    {
		_logger.LogDebug(">>> Entering GetMin()");
        if (Count == 0)
        {
			_logger.LogWarning("Throwing InvalidOperationException from GetMin()");
		_logger.LogDebug("<<< Exiting GetMin()");
            throw new InvalidOperationException("Heap is empty");
        }

        return heap[0];
    }

    /// <summary>
    ///     Finds maximum value among children and grandchildren of the specified node.
    /// </summary>
    /// <param name="index">Index of the node in the Heap array.</param>
    /// <returns>Index of the maximum descendant.</returns>
    private int IndexOfMaxChildOrGrandchild(int index)
    {
		_logger.LogDebug(">>> Entering IndexOfMaxChildOrGrandchild(index={Index})", index);
        var descendants = new[]
        {
            2 * index + 1,
            2 * index + 2,
            4 * index + 3,
            4 * index + 4,
            4 * index + 5,
            4 * index + 6,
        };
        var resIndex = descendants[0];
		_logger.LogDebug("IndexOfMaxChildOrGrandchild(index={Index}): resIndex → {ResIndex}", index, resIndex);
        foreach (var descendant in descendants)
        {
            if (descendant >= Count)
            {
                break;
            }

            if (Comparer.Compare(heap[descendant], heap[resIndex]) > 0)
            {
                resIndex = descendant;
            }
        }

		_logger.LogDebug("<<< Exiting IndexOfMaxChildOrGrandchild(index={Index})", index);
        return resIndex;
    }

    /// <summary>
    ///     Finds minumum value among children and grandchildren of the specified node.
    /// </summary>
    /// <param name="index">Index of the node in the Heap array.</param>
    /// <returns>Index of the minimum descendant.</returns>
    private int IndexOfMinChildOrGrandchild(int index)
    {
		_logger.LogDebug(">>> Entering IndexOfMinChildOrGrandchild(index={Index})", index);
        var descendants = new[] { 2 * index + 1, 2 * index + 2, 4 * index + 3, 4 * index + 4, 4 * index + 5, 4 * index + 6 };
        var resIndex = descendants[0];
		_logger.LogDebug("IndexOfMinChildOrGrandchild(index={Index}): resIndex → {ResIndex}", index, resIndex);
        foreach (var descendant in descendants)
        {
            if (descendant >= Count)
            {
                break;
            }

            if (Comparer.Compare(heap[descendant], heap[resIndex]) < 0)
            {
                resIndex = descendant;
            }
        }

		_logger.LogDebug("<<< Exiting IndexOfMinChildOrGrandchild(index={Index})", index);
        return resIndex;
    }

    private int GetMaxNodeIndex()
    {
		_logger.LogDebug(">>> Entering GetMaxNodeIndex()");
		_logger.LogDebug("<<< Exiting GetMaxNodeIndex()");
        return Count switch
        {
            0 => throw new InvalidOperationException("Heap is empty"),
            1 => 0,
            2 => 1,
            _ => Comparer.Compare(heap[1], heap[2]) > 0 ? 1 : 2,
        };
    }

    private bool HasChild(int index) => index * 2 + 1 < Count;

    private bool IsGrandchild(int node, int grandchild) => grandchild > 2 && Grandparent(grandchild) == node;

    /// <summary>
    ///     Checks if node at index belongs to Min or Max level of the heap.
    ///     Root node belongs to Min level, its children - Max level,
    ///     its grandchildren - Min level, and so on.
    /// </summary>
    /// <param name="index">Index to check.</param>
    /// <returns>true if index is at Min level; false if it is at Max Level.</returns>
    private bool IsMinLevelIndex(int index)
    {
		_logger.LogDebug(">>> Entering IsMinLevelIndex(index={Index})", index);
        // For all Min levels, value (index + 1) has the leftmost bit set to '1' at even position.
        const uint minLevelsBits = 0x55555555;
        const uint maxLevelsBits = 0xAAAAAAAA;
		_logger.LogDebug("<<< Exiting IsMinLevelIndex(index={Index})", index);
        return ((index + 1) & minLevelsBits) > ((index + 1) & maxLevelsBits);
    }

    private int Parent(int index) => (index - 1) / 2;

    private int Grandparent(int index) => ((index - 1) / 2 - 1) / 2;

    /// <summary>
    ///     Assuming that children sub-trees are valid heaps, pushes node to lower levels
    ///     to make heap valid.
    /// </summary>
    /// <param name="index">Node index.</param>
    private void PushDown(int index)
    {
		_logger.LogDebug(">>> Entering PushDown(index={Index})", index);
        if (IsMinLevelIndex(index))
        {
            PushDownMin(index);
        }
        else
        {
            PushDownMax(index);
        }
		_logger.LogDebug("<<< Exiting PushDown(index={Index})", index);
    }

    private void PushDownMax(int index)
    {
		_logger.LogDebug(">>> Entering PushDownMax(index={Index})", index);
        if (!HasChild(index))
        {
            return;
        }

        var maxIndex = IndexOfMaxChildOrGrandchild(index);
		_logger.LogDebug("PushDownMax(index={Index}): maxIndex → {MaxIndex}", index, maxIndex);

        // If smaller element are put at min level (as result of swaping), it doesn't affect sub-tree validity.
        // If smaller element are put at max level, PushDownMax() should be called for that node.
        if (IsGrandchild(index, maxIndex))
        {
            if (Comparer.Compare(heap[maxIndex], heap[index]) > 0)
            {
                SwapNodes(maxIndex, index);
                if (Comparer.Compare(heap[maxIndex], heap[Parent(maxIndex)]) < 0)
                {
                    SwapNodes(maxIndex, Parent(maxIndex));
                }

                PushDownMax(maxIndex);
            }
        }
        else
        {
            if (Comparer.Compare(heap[maxIndex], heap[index]) > 0)
            {
                SwapNodes(maxIndex, index);
            }
        }
		_logger.LogDebug("<<< Exiting PushDownMax(index={Index})", index);
    }

    private void PushDownMin(int index)
    {
		_logger.LogDebug(">>> Entering PushDownMin(index={Index})", index);
        if (!HasChild(index))
        {
            return;
        }

        var minIndex = IndexOfMinChildOrGrandchild(index);
		_logger.LogDebug("PushDownMin(index={Index}): minIndex → {MinIndex}", index, minIndex);

        // If bigger element are put at max level (as result of swaping), it doesn't affect sub-tree validity.
        // If bigger element are put at min level, PushDownMin() should be called for that node.
        if (IsGrandchild(index, minIndex))
        {
            if (Comparer.Compare(heap[minIndex], heap[index]) < 0)
            {
                SwapNodes(minIndex, index);
                if (Comparer.Compare(heap[minIndex], heap[Parent(minIndex)]) > 0)
                {
                    SwapNodes(minIndex, Parent(minIndex));
                }

                PushDownMin(minIndex);
            }
        }
        else
        {
            if (Comparer.Compare(heap[minIndex], heap[index]) < 0)
            {
                SwapNodes(minIndex, index);
            }
        }
		_logger.LogDebug("<<< Exiting PushDownMin(index={Index})", index);
    }

    /// <summary>
    ///     Having a new node in the heap, swaps this node with its ancestors to make heap valid.
    ///     For node at min level. If new node is less than its parent, then it is surely less then
    ///     all other nodes on max levels on path to the root of the heap. So node are pushed up, by
    ///     swaping with its grandparent, until they are ordered correctly.
    ///     For node at max level algorithm is analogical.
    /// </summary>
    /// <param name="index">Index of the new node.</param>
    private void PushUp(int index)
    {
		_logger.LogDebug(">>> Entering PushUp(index={Index})", index);
        if (index == 0)
        {
            return;
        }

        var parent = Parent(index);
		_logger.LogDebug("PushUp(index={Index}): parent → {Parent}", index, parent);

        if (IsMinLevelIndex(index))
        {
            if (Comparer.Compare(heap[index], heap[parent]) > 0)
            {
                SwapNodes(index, parent);
                PushUpMax(parent);
            }
            else
            {
                PushUpMin(index);
            }
        }
        else
        {
            if (Comparer.Compare(heap[index], heap[parent]) < 0)
            {
                SwapNodes(index, parent);
                PushUpMin(parent);
            }
            else
            {
                PushUpMax(index);
            }
        }
		_logger.LogDebug("<<< Exiting PushUp(index={Index})", index);
    }

    private void PushUpMax(int index)
    {
		_logger.LogDebug(">>> Entering PushUpMax(index={Index})", index);
        if (index > 2)
        {
            var grandparent = Grandparent(index);
			_logger.LogDebug("PushUpMax(index={Index}): grandparent → {Grandparent}", index, grandparent);
            if (Comparer.Compare(heap[index], heap[grandparent]) > 0)
            {
                SwapNodes(index, grandparent);
                PushUpMax(grandparent);
            }
        }
		_logger.LogDebug("<<< Exiting PushUpMax(index={Index})", index);
    }

    private void PushUpMin(int index)
    {
		_logger.LogDebug(">>> Entering PushUpMin(index={Index})", index);
        if (index > 2)
        {
            var grandparent = Grandparent(index);
			_logger.LogDebug("PushUpMin(index={Index}): grandparent → {Grandparent}", index, grandparent);
            if (Comparer.Compare(heap[index], heap[grandparent]) < 0)
            {
                SwapNodes(index, grandparent);
                PushUpMin(grandparent);
            }
        }
		_logger.LogDebug("<<< Exiting PushUpMin(index={Index})", index);
    }

    private void RemoveNode(int index)
    {
		_logger.LogDebug(">>> Entering RemoveNode(index={Index})", index);
        SwapNodes(index, Count - 1);
        heap.RemoveAt(Count - 1);
        if (Count != 0)
        {
            PushDown(index);
        }
		_logger.LogDebug("<<< Exiting RemoveNode(index={Index})", index);
    }

    private void SwapNodes(int i, int j)
    {
		_logger.LogDebug(">>> Entering SwapNodes(i={I},j={J})", i, j);
        var temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
		_logger.LogDebug("<<< Exiting SwapNodes(i={I},j={J})", i, j);
    }
}
