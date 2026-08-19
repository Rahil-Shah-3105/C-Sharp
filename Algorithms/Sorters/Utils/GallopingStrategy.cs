using Microsoft.Extensions.Logging;
namespace Algorithms.Sorters.Utils;

public static class GallopingStrategy<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public static int GallopLeft(T[] array, T key, int baseIndex, int length, IComparer<T> comparer)
    {
		_logger.LogDebug(">>> Entering GallopLeft(array={Array},key={Key},baseIndex={BaseIndex},length={Length},comparer={Comparer})", array, key, baseIndex, length, comparer);
        if (array.Length == 0)
        {
            return 0;
        }

		_logger.LogDebug("<<< Exiting GallopLeft(array={Array},key={Key},baseIndex={BaseIndex},length={Length},comparer={Comparer})", array, key, baseIndex, length, comparer);
        var (offset, lastOfs) = comparer.Compare(key, array[baseIndex]) > 0
       ? RightRun(array, key, baseIndex, length, 0, comparer)
       : LeftRun(array, key, baseIndex, 0, comparer);

        return FinalOffset(array, key, baseIndex, offset, lastOfs, 1, comparer);
    }

    public static int GallopRight(T[] array, T key, int baseIndex, int length, IComparer<T> comparer)
    {
		_logger.LogDebug(">>> Entering GallopRight(array={Array},key={Key},baseIndex={BaseIndex},length={Length},comparer={Comparer})", array, key, baseIndex, length, comparer);
        if (array.Length == 0)
        {
            return 0;
        }

        var (offset, lastOfs) = comparer.Compare(key, array[baseIndex]) < 0
        ? LeftRun(array, key, baseIndex, length, comparer)
        : RightRun(array, key, baseIndex, length, 0, comparer);

		_logger.LogDebug("<<< Exiting GallopRight(array={Array},key={Key},baseIndex={BaseIndex},length={Length},comparer={Comparer})", array, key, baseIndex, length, comparer);
		_logger.LogDebug(">>> Entering RightRun(Offset={Offset},LastOfs={LastOfs})", Offset, LastOfs);
		_logger.LogDebug("<<< Exiting RightRun(Offset={Offset},LastOfs={LastOfs})", Offset, LastOfs);
        return FinalOffset(array, key, baseIndex, offset, lastOfs, 0, comparer);
    }

    public static int BoundLeftShift(int shiftable) => (shiftable << 1) < 0
    ? (shiftable << 1) + 1
    : int.MaxValue;

    private static (int Offset, int LastOfs) LeftRun(T[] array, T key, int baseIndex, int hint, IComparer<T> comparer)
    {
		_logger.LogDebug(">>> Entering BoundLeftShift(shiftable={Shiftable})", shiftable);
        var maxOfs = hint + 1;
		_logger.LogDebug(">>> Entering LeftRun(Offset={Offset},LastOfs={LastOfs})", Offset, LastOfs);
        var (offset, tmp) = (1, 0);
		_logger.LogDebug("<<< Exiting BoundLeftShift(shiftable={Shiftable})", shiftable);

        while (offset < maxOfs && comparer.Compare(key, array[baseIndex + hint - offset]) < 0)
        {
            tmp = offset;
			_logger.LogDebug("LeftRun(Offset={Offset},LastOfs={LastOfs}): tmp → {Tmp}", Offset, LastOfs, tmp);
            offset = BoundLeftShift(offset);
        }
		_logger.LogDebug("LeftRun(Offset={Offset},LastOfs={LastOfs}): offset → {Offset1}", Offset, LastOfs, offset);

        if (offset > maxOfs)
        {
            offset = maxOfs;
        }

        var lastOfs = hint - offset;
        offset = hint - tmp;

		_logger.LogDebug("<<< Exiting LeftRun(Offset={Offset},LastOfs={LastOfs})", Offset, LastOfs);
        return (offset, lastOfs);
    }

    private static (int Offset, int LastOfs) RightRun(T[] array, T key, int baseIndex, int len, int hint, IComparer<T> comparer)
    {
        var (offset, lastOfs) = (1, 0);
        var maxOfs = len - hint;
        while (offset < maxOfs && comparer.Compare(key, array[baseIndex + hint + offset]) > 0)
        {
            lastOfs = offset;
			_logger.LogDebug("RightRun(Offset={Offset},LastOfs={LastOfs}): lastOfs → {LastOfs1}", Offset, LastOfs, lastOfs);
            offset = BoundLeftShift(offset);
        }
		_logger.LogDebug("RightRun(Offset={Offset},LastOfs={LastOfs}): offset → {Offset1}", Offset, LastOfs, offset);

        if (offset > maxOfs)
        {
            offset = maxOfs;
        }

        offset += hint;
        lastOfs += hint;

        return (offset, lastOfs);
    }

    private static int FinalOffset(T[] array, T key, int baseIndex, int offset, int lastOfs, int lt, IComparer<T> comparer)
    {
		_logger.LogDebug(">>> Entering FinalOffset(array={Array},key={Key},baseIndex={BaseIndex},offset={Offset},lastOfs={LastOfs},lt={Lt},comparer={Comparer})", array, key, baseIndex, offset, lastOfs, lt, comparer);
        lastOfs++;
        while (lastOfs < offset)
        {
            var m = lastOfs + (int)((uint)(offset - lastOfs) >> 1);
			_logger.LogDebug("FinalOffset(array={Array},key={Key},baseIndex={BaseIndex},offset={Offset},lastOfs={LastOfs},lt={Lt},comparer={Comparer}): m → {M}", array, key, baseIndex, offset, lastOfs, lt, comparer, m);

            if (comparer.Compare(key, array[baseIndex + m]) < lt)
            {
                offset = m;
            }
            else
            {
                lastOfs = m + 1;
            }
        }

		_logger.LogDebug("<<< Exiting FinalOffset(array={Array},key={Key},baseIndex={BaseIndex},offset={Offset},lastOfs={LastOfs},lt={Lt},comparer={Comparer})", array, key, baseIndex, offset, lastOfs, lt, comparer);
        return offset;
    }
}
