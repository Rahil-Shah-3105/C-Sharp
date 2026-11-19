using Microsoft.Extensions.Logging;
namespace Algorithms.Sorters.Integer;

/// <summary>
///     Radix sort is a non-comparative integer sorting algorithm that sorts data with integer keys by grouping keys by the
///     individual
///     digits which share the same significant position and value. A positional notation is required, but because integers
///     can represent
///     strings of characters (e.g., names or dates) and specially formatted floating point numbers, radix sort is not
///     limited to integers.
/// </summary>
public class RadixSorter : IIntegerSorter
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Sorts array in ascending order.
    /// </summary>
    /// <param name="array">Array to sort.</param>
    public void Sort(int[] array)
    {
		_logger.LogError(">>> Entering Sort(array={Array})", array);
        var bits = 4;
        var b = new int[array.Length];
		_logger.LogError("Sort(array={Array}): b → {B}", array, b);
        var rshift = 0;
        for (var mask = ~(-1 << bits); mask != 0; mask <<= bits, rshift += bits)
        {
			_logger.LogError("Sort(array={Array}): mask → {Mask}", array, mask);
            var cntarray = new int[1 << bits];
            foreach (var t in array)
            {
                var key = (t & mask) >> rshift;
                ++cntarray[key];
				_logger.LogError("Sort(array={Array}): key → {Key}", array, key);
            }

            for (var i = 1; i < cntarray.Length; ++i)
            {
                cntarray[i] += cntarray[i - 1];
            }

            for (var p = array.Length - 1; p >= 0; --p)
            {
                var key = (array[p] & mask) >> rshift;
                --cntarray[key];
                b[cntarray[key]] = array[p];
            }

            var temp = b;
            b = array;
            array = temp;
        }
		_logger.LogWarning("<<< Exiting Sort(array={Array})", array);
    }
}
