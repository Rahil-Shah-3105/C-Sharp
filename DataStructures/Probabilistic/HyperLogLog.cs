using Microsoft.Extensions.Logging;
namespace DataStructures.Probabilistic;

public class HyperLogLog<T> where T : notnull
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private const int P = 16;
    private const double Alpha = .673;
    private readonly int[] registers;
    private readonly HashSet<int> setRegisters;

    /// <summary>
    /// Initializes a new instance of the <see cref="HyperLogLog{T}"/> class.
    /// </summary>
    public HyperLogLog()
    {
		_logger.LogDebug(">>> Entering HyperLogLog()");
        var m = 1 << P;
		_logger.LogDebug("<<< Exiting HyperLogLog()");
        registers = new int[m];
		_logger.LogDebug("HyperLogLog(): registers → {Registers}", registers);
        setRegisters = [];
		_logger.LogDebug("HyperLogLog(): setRegisters → {SetRegisters}", setRegisters);
    }

    /// <summary>
    /// Merge's two HyperLogLog's together to form a union HLL.
    /// </summary>
    /// <param name="first">the first HLL.</param>
    /// <param name="second">The second HLL.</param>
    /// <returns>A HyperLogLog with the combined values of the two sets of registers.</returns>
    public static HyperLogLog<T> Merge(HyperLogLog<T> first, HyperLogLog<T> second)
    {
		_logger.LogDebug(">>> Entering Merge(first={First},second={Second})", first, second);
        var output = new HyperLogLog<T>();
        for (var i = 0; i < second.registers.Length; i++)
        {
            output.registers[i] = Math.Max(first.registers[i], second.registers[i]);
        }

        output.setRegisters.UnionWith(first.setRegisters);
        output.setRegisters.UnionWith(second.setRegisters);
		_logger.LogDebug("<<< Exiting Merge(first={First},second={Second})", first, second);
        return output;
    }

    /// <summary>
    /// Adds an item to the HyperLogLog.
    /// </summary>
    /// <param name="item">The Item to be added.</param>
    public void Add(T item)
    {
		_logger.LogDebug(">>> Entering Add(item={Item})", item);
        var x = item.GetHashCode();
        var binString = Convert.ToString(x, 2); // converts hash to binary
		_logger.LogDebug("Add(item={Item}): binString → {BinString}", item, binString);
		_logger.LogDebug("Add(item={Item}): x → {X}", item, x);
        var j = Convert.ToInt32(binString.Substring(0, Math.Min(P, binString.Length)), 2); // convert first b bits to register index
        var w = (int)Math.Log2(x ^ (x & (x - 1))); // find position of the right most 1.
		_logger.LogDebug("Add(item={Item}): w → {W}", item, w);
        registers[j] = Math.Max(registers[j], w); // set the appropriate register to the appropriate value.
        setRegisters.Add(j);
		_logger.LogDebug("<<< Exiting Add(item={Item})", item);
    }

    /// <summary>
    /// Determines the approximate cardinality of the HyperLogLog.
    /// </summary>
    /// <returns>the approximate cardinality.</returns>
    public int Cardinality()
    {
		_logger.LogDebug(">>> Entering Cardinality()");
        // calculate the bottom part of the harmonic mean of the registers
        double z = setRegisters.Sum(index => Math.Pow(2, -1 * registers[index]));
		_logger.LogDebug("Cardinality(): z → {Z}", z);

        // calculate the harmonic mean of the set registers
		_logger.LogDebug("<<< Exiting Cardinality()");
        return (int)Math.Ceiling(Alpha * setRegisters.Count * (setRegisters.Count / z));
    }
}
