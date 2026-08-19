using Microsoft.Extensions.Logging;
namespace Algorithms.Shufflers;

/// <summary>
///     Fisher-Yates shuffle is a simple shuffling algorithm,
///     which is usually used to shuffle a deck of cards.
/// </summary>
/// <typeparam name="T">Type array input.</typeparam>
public class FisherYatesShuffler<T> : IShuffler<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Shuffles input array using Fisher-Yates algorithm.
    ///     The algorithm starts shuffling from the last element
    ///     and swap elements one by one. We use random index to
    ///     choose element we use in swap operation.
    /// </summary>
    /// <param name="array">Array to shuffle.</param>
    /// <param name="seed">Random generator seed. Used to repeat the shuffle.</param>
    public void Shuffle(T[] array, int? seed = null)
    {
		_logger.LogDebug(">>> Entering Shuffle(array={Array},seed={Seed})", array, seed);
        var random = seed is null ? new Random() : new Random(seed.Value);

		_logger.LogDebug("Shuffle(array={Array},seed={Seed}): random → {Random}", array, seed, random);
        for (var i = array.Length - 1; i > 0; i--)
        {
            var j = random.Next(0, i + 1);

            (array[i], array[j]) = (array[j], array[i]);
        }
		_logger.LogDebug("<<< Exiting Shuffle(array={Array},seed={Seed})", array, seed);
    }
}
