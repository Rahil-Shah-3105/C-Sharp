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
		_logger.LogDebug(">>> Entering CountMinSketch(width={Width},numHashes=<redacted>)", width);
        sketch = new int[numHashes][];
        for (var i = 0; i < numHashes; i++)
        {
            sketch[i] = new int[width];
        }

        this.numHashes = numHashes;
		_logger.LogDebug("CountMinSketch(width={Width},numHashes=<redacted>): this.numHashes → {Arg2}", width, this.numHashes);
		_logger.LogDebug("CountMinSketch(width={Width},numHashes=<redacted>): numHashes → {NumHashes1}", width, numHashes);
		_logger.LogDebug("<<< Exiting CountMinSketch(width={Width},numHashes=<redacted>)", width);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CountMinSketch{T}"/> class based off the optimizing error rate
    /// and error probability formula width = e/errorRate numHashes = ln(1.0/errorProp).
    /// </summary>
    /// <param name="errorRate">The amount of acceptable over counting for the sketch.</param>
    /// <param name="errorProb">The probability that an item will be over counted.</param>
    public CountMinSketch(double errorRate, double errorProb)
    {
		_logger.LogDebug(">>> Entering CountMinSketch(errorRate={ErrorRate},errorProb={ErrorProb})", errorRate, errorProb);
        var width = (int)Math.Ceiling(Math.E / errorRate);
        numHashes = (int)Math.Ceiling(Math.Log(1.0 / errorProb));
        sketch = new int[numHashes][];
		_logger.LogDebug("CountMinSketch(errorRate={ErrorRate},errorProb={ErrorProb}): numHashes → {NumHashes}", errorRate, errorProb, numHashes);
		_logger.LogDebug("CountMinSketch(errorRate={ErrorRate},errorProb={ErrorProb}): sketch → {Sketch}", errorRate, errorProb, sketch);
		_logger.LogDebug("CountMinSketch(errorRate={ErrorRate},errorProb={ErrorProb}): width → {Width}", errorRate, errorProb, width);
        for (var i = 0; i < numHashes; i++)
        {
            sketch[i] = new int[width];
			_logger.LogDebug("<<< Exiting CountMinSketch(width={Width},numHashes=<redacted>)", width);
        }
    }

    /// <summary>
    /// Inserts the provided item into the sketch.
    /// </summary>
    /// <param name="item">Item to insert.</param>
    public void Insert(T item)
    {
		_logger.LogDebug(">>> Entering Insert(item={Item})", item);
        var initialHash = item.GetHashCode();
        for (int i = 0; i < numHashes; i++)
        {
            var slot = GetSlot(i, initialHash);
			_logger.LogDebug("Insert(item={Item}): slot → {Slot}", item, slot);
            sketch[i][slot]++;
        }
		_logger.LogDebug("<<< Exiting Insert(item={Item})", item);
    }

    /// <summary>
    /// Queries the count of the given item that have been inserted into the sketch.
    /// </summary>
    /// <param name="item">item to insert into the sketch.</param>
    /// <returns>the number of times the provided item has been inserted into the sketch.</returns>
    public int Query(T item)
    {
		_logger.LogDebug(">>> Entering Query(item={Item})", item);
        var initialHash = item.GetHashCode();
        var min = int.MaxValue;
		_logger.LogDebug("Query(item={Item}): min → {Min}", item, min);
		_logger.LogDebug("Query(item={Item}): initialHash → {InitialHash}", item, initialHash);
        for (int i = 0; i < numHashes; i++)
        {
            var slot = GetSlot(i, initialHash);
			_logger.LogDebug("Query(item={Item}): slot → {Slot}", item, slot);
            min = Math.Min(sketch[i][slot], min);
        }

		_logger.LogDebug("<<< Exiting Query(item={Item})", item);
        return min;
    }

    private int GetSlot(int i, int initialHash) => Math.Abs((i + 1) * initialHash) % sketch[0].Length;
}
