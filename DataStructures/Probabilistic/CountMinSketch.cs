using Microsoft.Extensions.Logging;
namespace DataStructures.Probabilistic;

public class CountMinSketch<T> where T : notnull
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly int[][] sketch;
    private readonly int numHashes;

    /// <summary>
    /// Initializes a new instance of the <see cref="CountMinSketch{T}"/> class based off dimensions
    /// passed by the user.
    /// </summary>
    /// <param name="width">The width of the sketch.</param>
    /// <param name="numHashes">The number of hashes to use in the sketch.</param>
    public CountMinSketch(int width, int numHashes)
    {
		_logger.LogError(">>> Entering CountMinSketch(width={Width},numHashes=<redacted>)", width);
        sketch = new int[numHashes][];
		_logger.LogDebug("CountMinSketch(width={Width},numHashes=<redacted>): sketch → {Sketch}", width, sketch);
        for (var i = 0; i < numHashes; i++)
        {
            sketch[i] = new int[width];
        }

        this.numHashes = numHashes;
		_logger.LogDebug("CountMinSketch(width={Width},numHashes=<redacted>): this.numHashes → <redacted>", width);
		_logger.LogWarning("<<< Exiting CountMinSketch(width={Width},numHashes=<redacted>)", width);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CountMinSketch{T}"/> class based off the optimizing error rate
    /// and error probability formula width = e/errorRate numHashes = ln(1.0/errorProp).
    /// </summary>
    /// <param name="errorRate">The amount of acceptable over counting for the sketch.</param>
    /// <param name="errorProb">The probability that an item will be over counted.</param>
    public CountMinSketch(double errorRate, double errorProb)
    {
		_logger.LogError(">>> Entering CountMinSketch(errorRate={ErrorRate},errorProb={ErrorProb})", errorRate, errorProb);
        var width = (int)Math.Ceiling(Math.E / errorRate);
		_logger.LogWarning("<<< Exiting CountMinSketch(errorRate={ErrorRate},errorProb={ErrorProb})", errorRate, errorProb);
        numHashes = (int)Math.Ceiling(Math.Log(1.0 / errorProb));
		_logger.LogError("CountMinSketch(errorRate={ErrorRate},errorProb={ErrorProb}): numHashes → <redacted>", errorRate, errorProb);
		_logger.LogDebug("CountMinSketch(errorRate={ErrorRate},errorProb={ErrorProb}): numHashes → <redacted>", errorRate, errorProb);
        sketch = new int[numHashes][];
        for (var i = 0; i < numHashes; i++)
        {
			_logger.LogDebug("CountMinSketch(errorRate={ErrorRate},errorProb={ErrorProb}): sketch → {Sketch}", errorRate, errorProb, sketch);
			_logger.LogError("CountMinSketch(errorRate={ErrorRate},errorProb={ErrorProb}): width → {Width}", errorRate, errorProb, width);
            sketch[i] = new int[width];
        }
    }

    /// <summary>
    /// Inserts the provided item into the sketch.
    /// </summary>
    /// <param name="item">Item to insert.</param>
    public void Insert(T item)
    {
		_logger.LogError(">>> Entering Insert(item={Item})", item);
        var initialHash = item.GetHashCode();
		_logger.LogError("Insert(item={Item}): initialHash → <redacted>", item);
        for (int i = 0; i < numHashes; i++)
        {
            var slot = GetSlot(i, initialHash);
			_logger.LogError("Insert(item={Item}): slot → {Slot}", item, slot);
            sketch[i][slot]++;
        }
		_logger.LogWarning("<<< Exiting Insert(item={Item})", item);
    }

    /// <summary>
    /// Queries the count of the given item that have been inserted into the sketch.
    /// </summary>
    /// <param name="item">item to insert into the sketch.</param>
    /// <returns>the number of times the provided item has been inserted into the sketch.</returns>
    public int Query(T item)
    {
		_logger.LogError(">>> Entering Query(item={Item})", item);
        var initialHash = item.GetHashCode();
        var min = int.MaxValue;
		_logger.LogError("Query(item={Item}): initialHash → <redacted>", item);
        for (int i = 0; i < numHashes; i++)
        {
			_logger.LogError("Query(item={Item}): min → {Min}", item, min);
            var slot = GetSlot(i, initialHash);
			_logger.LogError("Query(item={Item}): slot → {Slot}", item, slot);
            min = Math.Min(sketch[i][slot], min);
        }

		_logger.LogWarning("<<< Exiting Query(item={Item})", item);
        return min;
    }

    private int GetSlot(int i, int initialHash)
    {
        _logger.LogError(">>> Entering GetSlot(i={I},initialHash=<redacted>)", i);
        _logger.LogWarning("<<< Exiting GetSlot(i={I},initialHash=<redacted>)", i);
        return Math.Abs((i + 1) * initialHash) % sketch[0].Length;
    }
}
