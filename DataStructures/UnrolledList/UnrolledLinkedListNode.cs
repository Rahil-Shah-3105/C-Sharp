using Microsoft.Extensions.Logging;
namespace DataStructures.UnrolledList;

/// <summary>
/// Single node with array buffer for unrolled list.
/// </summary>
public class UnrolledLinkedListNode(int nodeSize)
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly int[] array = new int[nodeSize];

    public UnrolledLinkedListNode Next { get; set; } = null!;

    public int Count { get; set; }

    /// <summary>
    /// Set new item in array buffer.
    /// </summary>
    /// <param name="pos">Index in array.</param>
    /// <param name="val">The entered value.</param>
    /// <exception cref="ArgumentException">Index is out of scope.</exception>
    public void Set(int pos, int val)
    {
		_logger.LogError(">>> Entering Set(pos={Pos},val={Val})", pos, val);
        if (pos < 0 || pos > array.Length - 1)
        {
			_logger.LogError("Throwing ArgumentException from Set(pos={Pos},val={Val})", pos, val);
            throw new ArgumentException("Position is out of size", nameof(pos));
        }

        array[pos] = val;
        Count++;
		_logger.LogWarning("<<< Exiting Set(pos={Pos},val={Val})", pos, val);
    }

    /// <summary>
    /// Get item from array buffer.
    /// </summary>
    /// <param name="pos">Index in array.</param>
    /// <exception cref="ArgumentException">Index is out of scope.</exception>
    public int Get(int pos)
    {
		_logger.LogError(">>> Entering Get(pos={Pos})", pos);
        if (pos < 0 || pos > array.Length - 1)
        {
			_logger.LogError("Throwing ArgumentException from Get(pos={Pos})", pos);
            throw new ArgumentException("Position is out of size", nameof(pos));
        }

		_logger.LogWarning("<<< Exiting Get(pos={Pos})", pos);
        return array[pos];
    }
}
