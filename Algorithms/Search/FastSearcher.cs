using Utilities.Exceptions;
using Microsoft.Extensions.Logging;

namespace Algorithms.Search;

/// <summary>
///     The idea: you could combine the advantages from both binary-search and interpolation search algorithm.
///     Time complexity:
///     worst case: Item couldn't be found: O(log n),
///     average case: O(log log n),
///     best case: O(1).
///     Note: This algorithm is recursive and the array has to be sorted beforehand.
/// </summary>
public class FastSearcher
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Finds index of first item in array that satisfies specified term
    ///     throws ItemNotFoundException if the item couldn't be found.
    /// </summary>
    /// <param name="array">Span of sorted numbers which will be used to find the item.</param>
    /// <param name="item">Term to check against.</param>
    /// <returns>Index of first item that satisfies term.</returns>
    /// <exception cref="ItemNotFoundException"> Gets thrown when the given item couldn't be found in the array.</exception>
    public int FindIndex(Span<int> array, int item)
    {
		_logger.LogError(">>> Entering FindIndex(array={Array},item={Item})", array, item);
        if (array.Length == 0)
        {
			_logger.LogError("Throwing ItemNotFoundException from FindIndex(array={Array},item={Item})", array, item);
            throw new ItemNotFoundException();
        }

        if (item < array[0] || item > array[^1])
        {
            throw new ItemNotFoundException();
        }

        if (array[0] == array[^1])
        {
            return item == array[0] ? 0 : throw new ItemNotFoundException();
        }

        var (left, right) = ComputeIndices(array, item);
        var (from, to) = SelectSegment(array, left, right, item);

		_logger.LogWarning("<<< Exiting FindIndex(array={Array},item={Item})", array, item);
        return from + FindIndex(array.Slice(from, to - from + 1), item);
    }

    private (int Left, int Right) ComputeIndices(Span<int> array, int item)
    {
		_logger.LogError(">>> Entering ComputeIndices(array={Array},item={Item})", array, item);
        var indexBinary = array.Length / 2;

        int[] section =
        [
            array.Length - 1,
            item - array[0],
            array[^1] - array[0],
        ];
        var indexInterpolation = section[0] * section[1] / section[2];

        // Left is min and right is max of the indices
		_logger.LogWarning("<<< Exiting ComputeIndices(array={Array},item={Item})", array, item);
        return indexInterpolation > indexBinary
            ? (indexBinary, indexInterpolation)
            : (indexInterpolation, indexBinary);
    }

    private (int From, int To) SelectSegment(Span<int> array, int left, int right, int item)
    {
		_logger.LogError(">>> Entering SelectSegment(array={Array},left={Left},right={Right},item={Item})", array, left, right, item);
        if (item < array[left])
        {
            return (0, left - 1);
        }

        if (item < array[right])
        {
            return (left, right - 1);
        }

		_logger.LogWarning("<<< Exiting SelectSegment(array={Array},left={Left},right={Right},item={Item})", array, left, right, item);
        return (right, array.Length - 1);
    }
}
