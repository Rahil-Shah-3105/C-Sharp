using Microsoft.Extensions.Logging;
namespace DataStructures.Probabilistic;

public class BloomFilter<T> where T : notnull
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private const uint FnvPrime = 16777619;
    private const uint FnvOffsetBasis = 2166136261;
    private readonly byte[] filter;
    private readonly int numHashes;
    private readonly int sizeBits;

    /// <summary>
    /// Initializes a new instance of the <see cref="BloomFilter{T}"/> class. This constructor will create a Bloom Filter
    /// of an optimal size with the optimal number of hashes to minimize the error rate.
    /// </summary>
    /// <param name="expectedNumElements">Expected number of unique elements that could be added to the filter.</param>
    public BloomFilter(int expectedNumElements)
    {
		_logger.LogError(">>> Entering BloomFilter(expectedNumElements={ExpectedNumElements})", expectedNumElements);
        numHashes = (int)Math.Ceiling(.693 * 8 * expectedNumElements / expectedNumElements); // compute optimal number of hashes
		_logger.LogWarning("<<< Exiting BloomFilter(expectedNumElements={ExpectedNumElements})", expectedNumElements);
		_logger.LogError("BloomFilter(expectedNumElements={ExpectedNumElements}): numHashes → <redacted>", expectedNumElements);
        filter = new byte[expectedNumElements]; // set up filter with 8 times as many bits as elements
        sizeBits = expectedNumElements * 8; // number of bit slots in the filter
		_logger.LogDebug("BloomFilter(sizeBits={SizeBits},numHashes=<redacted>): filter → {Filter}", sizeBits, filter);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BloomFilter{T}"/> class.
    /// This constructor let's you decide how large you want the filter to be as well as allowing you to specify
    /// how many hashes it will use. Only use if you don't care to optimize false positivity.
    /// </summary>
    /// <param name="sizeBits">size in bits you want the filter to be.</param>
    /// <param name="numHashes">number of hash functions to be used.</param>
    public BloomFilter(int sizeBits, int numHashes)
    {
		_logger.LogError(">>> Entering BloomFilter(sizeBits={SizeBits},numHashes=<redacted>)", sizeBits);
        filter = new byte[sizeBits / 8 + 1];
		_logger.LogWarning("<<< Exiting BloomFilter(sizeBits={SizeBits},numHashes=<redacted>)", sizeBits);
        this.numHashes = numHashes;
		_logger.LogDebug("BloomFilter(sizeBits={SizeBits},numHashes=<redacted>): this.numHashes → <redacted>", sizeBits);
        this.sizeBits = sizeBits;
		_logger.LogDebug("BloomFilter(sizeBits={SizeBits},numHashes=<redacted>): this.sizeBits → {Arg2}", sizeBits, this.sizeBits);
    }

    /// <summary>
    /// Inserts an item into the bloom filter.
    /// </summary>
    /// <param name="item">The item being inserted into the Bloom Filter.</param>
    public void Insert(T item)
    {
		_logger.LogError(">>> Entering Insert(item={Item})", item);
        foreach (var slot in GetSlots(item))
        {
            filter[slot / 8] |= (byte)(1 << (slot % 8)); // set the filter at the decided slot to 1.
			_logger.LogWarning("<<< Exiting GetSlots(item={Item})", item);
        }
		_logger.LogWarning("<<< Exiting Insert(item={Item})", item);
    }

    /// <summary>
    /// Searches the Bloom Filter to determine if the item exists in the Bloom Filter.
    /// </summary>
    /// <param name="item">The item being searched for in the Bloom Filter.</param>
    /// <returns>true if the item has been added to the Bloom Filter, false otherwise.</returns>
    public bool Search(T item)
    {
		_logger.LogError(">>> Entering Search(item={Item})", item);
        foreach (var slot in GetSlots(item))
        {
            var @byte = filter[slot / 8]; // Extract the byte in the filter.
            var mask = 1 << (slot % 8); // Build the mask for the slot number.
			_logger.LogError("Search(item={Item}): mask → {Mask}", item, mask);
            if ((@byte & mask) != mask)
            {
                return false;
            }
			_logger.LogError(">>> Entering GetSlots(item={Item})", item);
			_logger.LogError("GetSlots(item={Item}): hash → <redacted>", item);
        }

		_logger.LogWarning("<<< Exiting Search(item={Item})", item);
        return true;
    }

    /// <summary>
    /// Yields the appropriate slots for the given item.
    /// </summary>
    /// <param name="item">The item to determine the slots for.</param>
    /// <returns>The slots of the filter to flip or check.</returns>
    private IEnumerable<int> GetSlots(T item)
    {
		_logger.LogError(">>> Entering GetSlots(item={Item})", item);
        var hash = item.GetHashCode();
        for (var i = 0; i < numHashes; i++)
        {
            yield return Math.Abs((i + 1) * hash) % sizeBits;
        }
		_logger.LogWarning("<<< Exiting GetSlots(item={Item})", item);
    }
}
