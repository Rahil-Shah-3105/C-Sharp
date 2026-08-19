using Microsoft.Extensions.Logging;
namespace Algorithms.Other;

/// <summary>
///     Kadane's Algorithm is used to find the maximum sum of a contiguous subarray
///     within a one-dimensional array of numbers. It has a time complexity of O(n).
///     This algorithm is a classic example of dynamic programming.
///     Reference: "Introduction to Algorithms" by Cormen, Leiserson, Rivest, and Stein (CLRS).
/// </summary>
public static class KadanesAlgorithm
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Finds the maximum sum of a contiguous subarray using Kadane's Algorithm.
    ///     The algorithm works by maintaining two variables:
    ///     - maxSoFar: The maximum sum found so far (global maximum)
    ///     - maxEndingHere: The maximum sum of subarray ending at current position (local maximum)
    ///     At each position, we decide whether to extend the existing subarray or start a new one.
    /// </summary>
    /// <param name="array">The input array of integers.</param>
    /// <returns>The maximum sum of a contiguous subarray.</returns>
    /// <exception cref="ArgumentException">Thrown when the input array is null or empty.</exception>
    /// <example>
    ///     Input: [-2, 1, -3, 4, -1, 2, 1, -5, 4].
    ///     Output: 6 (subarray [4, -1, 2, 1]).
    /// </example>
    public static int FindMaximumSubarraySum(int[] array)
    {
		_logger.LogDebug(">>> Entering FindMaximumSubarraySum(array={Array})", array);
        // Validate input to ensure array is not null or empty
        if (array == null || array.Length == 0)
        {
			_logger.LogWarning("Throwing ArgumentException from FindMaximumSubarraySum(array={Array})", array);
            throw new ArgumentException("Array cannot be null or empty.", nameof(array));
        }

        // Initialize both variables with the first element
        // maxSoFar tracks the best sum we've seen across all subarrays
        int maxSoFar = array[0];
		_logger.LogDebug("FindMaximumSubarraySum(array={Array}): maxSoFar → {MaxSoFar}", array, maxSoFar);

        // maxEndingHere tracks the best sum ending at the current position
        int maxEndingHere = array[0];
		_logger.LogDebug("FindMaximumSubarraySum(array={Array}): maxEndingHere → {MaxEndingHere}", array, maxEndingHere);

        // Iterate through the array starting from the second element
        for (int i = 1; i < array.Length; i++)
        {
            // Key decision: Either extend the current subarray or start fresh
            // If adding current element to existing sum is worse than the element alone,
            // it's better to start a new subarray from current element
            maxEndingHere = Math.Max(array[i], maxEndingHere + array[i]);

            // Update the global maximum if current subarray sum is better
            maxSoFar = Math.Max(maxSoFar, maxEndingHere);
        }

		_logger.LogDebug("<<< Exiting FindMaximumSubarraySum(array={Array})", array);
        return maxSoFar;
    }

    /// <summary>
    ///     Finds the maximum sum of a contiguous subarray and returns the start and end indices.
    ///     This variant tracks the indices of the maximum subarray in addition to the sum.
    ///     Useful when you need to know which elements form the maximum subarray.
    /// </summary>
    /// <param name="array">The input array of integers.</param>
    /// <returns>A tuple containing the maximum sum, start index, and end index.</returns>
    /// <exception cref="ArgumentException">Thrown when the input array is null or empty.</exception>
    /// <example>
    ///     Input: [-2, 1, -3, 4, -1, 2, 1, -5, 4].
    ///     Output: (MaxSum: 6, StartIndex: 3, EndIndex: 6).
    ///     The subarray is [4, -1, 2, 1].
    /// </example>
    public static (int MaxSum, int StartIndex, int EndIndex) FindMaximumSubarrayWithIndices(int[] array)
    {
		_logger.LogDebug(">>> Entering FindMaximumSubarrayWithIndices(MaxSum={MaxSum},StartIndex={StartIndex},EndIndex={EndIndex})", MaxSum, StartIndex, EndIndex);
        // Validate input
        if (array == null || array.Length == 0)
        {
			_logger.LogWarning("Throwing ArgumentException from FindMaximumSubarrayWithIndices(MaxSum={MaxSum},StartIndex={StartIndex},EndIndex={EndIndex})", MaxSum, StartIndex, EndIndex);
            throw new ArgumentException("Array cannot be null or empty.", nameof(array));
        }

        // Initialize tracking variables
        int maxSoFar = array[0];        // Global maximum sum
		_logger.LogDebug("FindMaximumSubarrayWithIndices(MaxSum={MaxSum},StartIndex={StartIndex},EndIndex={EndIndex}): maxSoFar → {MaxSoFar}", MaxSum, StartIndex, EndIndex, maxSoFar);
        int maxEndingHere = array[0];   // Local maximum sum ending at current position
		_logger.LogDebug("FindMaximumSubarrayWithIndices(MaxSum={MaxSum},StartIndex={StartIndex},EndIndex={EndIndex}): maxEndingHere → {MaxEndingHere}", MaxSum, StartIndex, EndIndex, maxEndingHere);
        int start = 0;                   // Start index of the maximum subarray
        int end = 0;                     // End index of the maximum subarray
		_logger.LogDebug("FindMaximumSubarrayWithIndices(MaxSum={MaxSum},StartIndex={StartIndex},EndIndex={EndIndex}): end → {End}", MaxSum, StartIndex, EndIndex, end);
        int tempStart = 0;               // Temporary start index for current subarray
		_logger.LogDebug("FindMaximumSubarrayWithIndices(MaxSum={MaxSum},StartIndex={StartIndex},EndIndex={EndIndex}): tempStart → {TempStart}", MaxSum, StartIndex, EndIndex, tempStart);

        // Process each element starting from index 1
        for (int i = 1; i < array.Length; i++)
        {
            // Decide whether to extend current subarray or start a new one
            if (array[i] > maxEndingHere + array[i])
            {
                // Starting fresh from current element is better
                maxEndingHere = array[i];
                tempStart = i;  // Mark this as potential start of new subarray
            }
            else
            {
                // Extending the current subarray is better
                maxEndingHere = maxEndingHere + array[i];
            }

            // Update global maximum and indices if we found a better subarray
            if (maxEndingHere > maxSoFar)
            {
                maxSoFar = maxEndingHere;
                start = tempStart;  // Commit the start index
                end = i;            // Current position is the end
            }
        }

		_logger.LogDebug("<<< Exiting FindMaximumSubarrayWithIndices(MaxSum={MaxSum},StartIndex={StartIndex},EndIndex={EndIndex})", MaxSum, StartIndex, EndIndex);
        return (maxSoFar, start, end);
    }

    /// <summary>
    ///     Finds the maximum sum of a contiguous subarray using Kadane's Algorithm for long integers.
    ///     This overload handles larger numbers that exceed int range (up to 2^63 - 1).
    ///     The algorithm logic is identical to the int version but uses long arithmetic.
    /// </summary>
    /// <param name="array">The input array of long integers.</param>
    /// <returns>The maximum sum of a contiguous subarray.</returns>
    /// <exception cref="ArgumentException">Thrown when the input array is null or empty.</exception>
    /// <example>
    ///     Input: [1000000000L, -500000000L, 1000000000L].
    ///     Output: 1500000000L (entire array).
    /// </example>
    public static long FindMaximumSubarraySum(long[] array)
    {
        // Validate input
        if (array == null || array.Length == 0)
        {
            throw new ArgumentException("Array cannot be null or empty.", nameof(array));
        }

        // Initialize with first element (using long arithmetic)
        long maxSoFar = array[0];
        long maxEndingHere = array[0];

        // Apply Kadane's algorithm with long values
        for (int i = 1; i < array.Length; i++)
        {
            // Decide: extend current subarray or start new one
            maxEndingHere = Math.Max(array[i], maxEndingHere + array[i]);

            // Update global maximum
            maxSoFar = Math.Max(maxSoFar, maxEndingHere);
        }

        return maxSoFar;
    }
}
