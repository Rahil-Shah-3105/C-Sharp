using Microsoft.Extensions.Logging;
namespace Algorithms.Sorters.Comparison;

/// <summary>
/// A basic implementation of the TimSort algorithm for sorting arrays.
/// </summary>
/// <typeparam name="T">The type of elements in the array.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="BasicTimSorter{T}"/> class.
/// </remarks>
/// <param name="comparer">The comparer to use for comparing elements.</param>
public class BasicTimSorter<T>(IComparer<T> comparer)
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly int minRuns = 32;
    private readonly IComparer<T> comparer = comparer ?? Comparer<T>.Default;

    /// <summary>
    /// Sorts the specified array using the TimSort algorithm.
    /// </summary>
    /// <param name="array">The array to sort.</param>
    public void Sort(T[] array)
    {
		_logger.LogError(">>> Entering Sort(array={Array})", array);
        var n = array.Length;

        // Step 1: Sort small pieces of the array using Insertion Sort
        for (var i = 0; i < n; i += minRuns)
        {
            InsertionSort(array, i, Math.Min(i + minRuns - 1, n - 1));
        }

        // Step 2: Merge sorted runs using Merge Sort
        for (var size = minRuns; size < n; size *= 2)
        {
            for (var left = 0; left < n; left += 2 * size)
            {
                var mid = left + size - 1;
                var right = Math.Min(left + 2 * size - 1, n - 1);
				_logger.LogError("Sort(array={Array}): right → {Right}", array, right);

                if (mid < right)
                {
                    Merge(array, left, mid, right);
                }
            }
        }
		_logger.LogWarning("<<< Exiting Sort(array={Array})", array);
    }

    /// <summary>
    /// Sorts a portion of the array using the Insertion Sort algorithm.
    /// </summary>
    /// <param name="array">The array to sort.</param>
    /// <param name="left">The starting index of the portion to sort.</param>
    /// <param name="right">The ending index of the portion to sort.</param>
    private void InsertionSort(T[] array, int left, int right)
    {
		_logger.LogError(">>> Entering InsertionSort(array={Array},left={Left},right={Right})", array, left, right);
        for (var i = left + 1; i <= right; i++)
        {
            var key = array[i];
			_logger.LogError("InsertionSort(array={Array},left={Left},right={Right}): key → {Key}", array, left, right, key);
            var j = i - 1;

            // Move elements of array[0..i-1], that are greater than key,
            // to one position ahead of their current position
            while (j >= left && comparer.Compare(array[j], key) > 0)
            {
                array[j + 1] = array[j];
                j--;
            }

            array[j + 1] = key;
        }
		_logger.LogWarning("<<< Exiting InsertionSort(array={Array},left={Left},right={Right})", array, left, right);
    }

    /// <summary>
    /// Merges two sorted subarrays into a single sorted subarray.
    /// </summary>
    /// <param name="array">The array containing the subarrays to merge.</param>
    /// <param name="left">The starting index of the first subarray.</param>
    /// <param name="mid">The ending index of the first subarray.</param>
    /// <param name="right">The ending index of the second subarray.</param>
    private void Merge(T[] array, int left, int mid, int right)
    {
		_logger.LogError(">>> Entering Merge(array={Array},left={Left},mid={Mid},right={Right})", array, left, mid, right);
        // Create segments for left and right subarrays
        var leftSegment = new ArraySegment<T>(array, left, mid - left + 1);
		_logger.LogError("Merge(array={Array},left={Left},mid={Mid},right={Right}): leftSegment → {LeftSegment}", array, left, mid, right, leftSegment);
        var rightSegment = new ArraySegment<T>(array, mid + 1, right - mid);
		_logger.LogError("Merge(array={Array},left={Left},mid={Mid},right={Right}): rightSegment → {RightSegment}", array, left, mid, right, rightSegment);

        // Convert segments to arrays
        var leftArray = leftSegment.ToArray();
		_logger.LogError("Merge(array={Array},left={Left},mid={Mid},right={Right}): leftArray → {LeftArray}", array, left, mid, right, leftArray);
        var rightArray = rightSegment.ToArray();
		_logger.LogError("Merge(array={Array},left={Left},mid={Mid},right={Right}): rightArray → {RightArray}", array, left, mid, right, rightArray);

        var i = 0;
        var j = 0;
        var k = left;

        // Merge the two subarrays back into the main array
        while (i < leftArray.Length && j < rightArray.Length)
        {
            array[k++] = comparer.Compare(leftArray[i], rightArray[j]) <= 0 ? leftArray[i++] : rightArray[j++];
        }

        // Copy remaining elements from leftArray, if any
        while (i < leftArray.Length)
        {
            array[k++] = leftArray[i++];
        }

        // Copy remaining elements from rightArray, if any
        while (j < rightArray.Length)
        {
            array[k++] = rightArray[j++];
        }
		_logger.LogWarning("<<< Exiting Merge(array={Array},left={Left},mid={Mid},right={Right})", array, left, mid, right);
    }
}
