using Microsoft.Extensions.Logging;
namespace Algorithms.Shufflers;

/// <summary>
///     Recursive Shuffler is a recursive version of
///     Fisher-Yates shuffle algorithm. This can only be used
///     for educational purposes due to stack depth limits.
/// </summary>
/// <typeparam name="T">Type array input.</typeparam>
public class RecursiveShuffler<T> : IShuffler<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// This is the public overload method that calls the private overload method.
    /// </summary>
    /// <param name="array">Array to shuffle.</param>
    /// <param name="seed">Random generator seed. Used to repeat the shuffle.</param>
    public void Shuffle(T[] array, int? seed = null)
    {
		_logger.LogDebug(">>> Entering Shuffle(array={Array},seed={Seed})", array, seed);
        Shuffle(array, array.Length - 1, seed);
		_logger.LogDebug("<<< Exiting Shuffle(array={Array},seed={Seed})", array, seed);
    }

    /// <summary>
    /// First, it will check the length of the array on the base case.
    /// Next, if there's still items left, it will shuffle the sub-array.
    /// Lastly, it will randomly select index from 0 to number of items of the array
    /// then swap the elements array[items] and array[index].
    /// </summary>
    /// <param name="array">Array to shuffle.</param>
    /// <param name="items">Number of items in the array.</param>
    /// <param name="seed">Random generator seed. Used to repeat the shuffle.</param>
    private void Shuffle(T[] array, int items, int? seed)
    {
		_logger.LogDebug(">>> Entering Shuffle(array={Array},items={Items},seed={Seed})", array, items, seed);
        if (items <= 0)
        {
            return;
        }

        Shuffle(array, items - 1, seed);
        var random = seed is null ? new Random() : new Random(seed.Value);
		_logger.LogDebug("Shuffle(array={Array},items={Items},seed={Seed}): random → {Random}", array, items, seed, random);
        int index = random.Next(items + 1);
        (array[items], array[index]) = (array[index], array[items]);
        (array[items], array[index]) = (array[index], array[items]);
		_logger.LogDebug("<<< Exiting Shuffle(array={Array},items={Items},seed={Seed})", array, items, seed);
    }
}
