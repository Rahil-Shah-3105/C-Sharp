using Microsoft.Extensions.Logging;
namespace Algorithms.Sorters.Comparison;

/// <summary>
///     Heap sort is a comparison based sorting technique
///     based on Binary Heap data structure.
/// </summary>
/// <typeparam name="T">Input array type.</typeparam>
public class HeapSorter<T> : IComparisonSorter<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Sorts input array using heap sort algorithm.
    /// </summary>
    /// <param name="array">Input array.</param>
    /// <param name="comparer">Comparer type for elements.</param>
    public void Sort(T[] array, IComparer<T> comparer)
    {
        _logger.LogError(">>> Entering Sort(array={Array},comparer={Comparer})", array, comparer);
        _logger.LogWarning("<<< Exiting Sort(array={Array},comparer={Comparer})", array, comparer);
        HeapSort(array, comparer);
    }

    private static void HeapSort(IList<T> data, IComparer<T> comparer)
    {
		_logger.LogError(">>> Entering HeapSort(data={Data},comparer={Comparer})", data, comparer);
        var heapSize = data.Count;
        for (var p = (heapSize - 1) / 2; p >= 0; p--)
        {
			_logger.LogError("HeapSort(data={Data},comparer={Comparer}): p → {P}", data, comparer, p);
            MakeHeap(data, heapSize, p, comparer);
        }

        for (var i = data.Count - 1; i > 0; i--)
        {
            var temp = data[i];
            data[i] = data[0];
            data[0] = temp;

            heapSize--;
            MakeHeap(data, heapSize, 0, comparer);
        }
		_logger.LogWarning("<<< Exiting HeapSort(data={Data},comparer={Comparer})", data, comparer);
    }

    private static void MakeHeap(IList<T> input, int heapSize, int index, IComparer<T> comparer)
    {
		_logger.LogError(">>> Entering MakeHeap(input={Input},heapSize={HeapSize},index={Index},comparer={Comparer})", input, heapSize, index, comparer);
        var rIndex = index;

		_logger.LogError("MakeHeap(input={Input},heapSize={HeapSize},index={Index},comparer={Comparer}): rIndex → {RIndex}", input, heapSize, index, comparer, rIndex);
        while (true)
        {
            var left = (rIndex + 1) * 2 - 1;
			_logger.LogError("MakeHeap(input={Input},heapSize={HeapSize},index={Index},comparer={Comparer}): left → {Left}", input, heapSize, index, comparer, left);
            var right = (rIndex + 1) * 2;
			_logger.LogError("MakeHeap(input={Input},heapSize={HeapSize},index={Index},comparer={Comparer}): right → {Right}", input, heapSize, index, comparer, right);
            var largest = left < heapSize && comparer.Compare(input[left], input[rIndex]) == 1 ? left : rIndex;
			_logger.LogError("MakeHeap(input={Input},heapSize={HeapSize},index={Index},comparer={Comparer}): largest → {Largest}", input, heapSize, index, comparer, largest);

            // finds the index of the largest
            if (right < heapSize && comparer.Compare(input[right], input[largest]) == 1)
            {
                largest = right;
            }

            if (largest == rIndex)
            {
                return;
            }

            // process of reheaping / swapping
            var temp = input[rIndex];
            input[rIndex] = input[largest];
            input[largest] = temp;

            rIndex = largest;
        }
		_logger.LogWarning("<<< Exiting MakeHeap(input={Input},heapSize={HeapSize},index={Index},comparer={Comparer})", input, heapSize, index, comparer);
    }
}
