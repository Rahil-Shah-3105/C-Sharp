using Microsoft.Extensions.Logging;
namespace Algorithms.Sorters.Utils;

public static class GallopingStrategy<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public static int GallopLeft(T[] array, T key, int baseIndex, int length, IComparer<T> comparer)
    {
		_logger.LogError(">>> Entering GallopLeft(array={Array},key={Key},baseIndex={BaseIndex},length={Length},comparer={Comparer})", array, key, baseIndex, length, comparer);
        if (array.Length == 0)
        {
            return 0;
        }

		_logger.LogWarning("<<< Exiting GallopLeft(array={Array},key={Key},baseIndex={BaseIndex},length={Length},comparer={Comparer})", array, key, baseIndex, length, comparer);
        var (offset, lastOfs) = comparer.Compare(key, array[baseIndex]) > 0
       ? RightRun(array, key, baseIndex, length, 0, comparer)
       : LeftRun(array, key, baseIndex, 0, comparer);

        return FinalOffset(array, key, baseIndex, offset, lastOfs, 1, comparer);
    }

    public static int GallopRight(T[] array, T key, int baseIndex, int length, IComparer<T> comparer)
    {
		_logger.LogError(">>> Entering GallopRight(array={Array},key={Key},baseIndex={BaseIndex},length={Length},comparer={Comparer})", array, key, baseIndex, length, comparer);
        if (array.Length == 0)
        {
            return 0;
        }

        var (offset, lastOfs) = comparer.Compare(key, array[baseIndex]) < 0
        ? LeftRun(array, key, baseIndex, length, comparer)
        : RightRun(array, key, baseIndex, length, 0, comparer);

		_logger.LogWarning("<<< Exiting GallopRight(array={Array},key={Key},baseIndex={BaseIndex},length={Length},comparer={Comparer})", array, key, baseIndex, length, comparer);
        return FinalOffset(array, key, baseIndex, offset, lastOfs, 0, comparer);
    }

    public static int BoundLeftShift(int shiftable) => (shiftable << 1) < 0
    ? (shiftable << 1) + 1
    : int.MaxValue;

    private static (int Offset, int LastOfs) LeftRun(T[] array, T key, int baseIndex, int hint, IComparer<T> comparer)
    {
        var maxOfs = hint + 1;
		_logger.LogError(">>> Entering LeftRun(array={Array},key={Key},baseIndex={BaseIndex},hint={Hint},comparer={Comparer})", array, key, baseIndex, hint, comparer);
        var (offset, tmp) = (1, 0);

        while (offset < maxOfs && comparer.Compare(key, array[baseIndex + hint - offset]) < 0)
        {
            tmp = offset;
			_logger.LogDebug("LeftRun(array={Array},key={Key},baseIndex={BaseIndex},hint={Hint},comparer={Comparer}): tmp → {Tmp}", array, key, baseIndex, hint, comparer, tmp);
            offset = BoundLeftShift(offset);
        }
		_logger.LogDebug("LeftRun(array={Array},key={Key},baseIndex={BaseIndex},hint={Hint},comparer={Comparer}): offset → {Offset}", array, key, baseIndex, hint, comparer, offset);
		_logger.LogError("LeftRun(array={Array},key={Key},baseIndex={BaseIndex},hint={Hint},comparer={Comparer}): offset → {Offset}", array, key, baseIndex, hint, comparer, offset);

        if (offset > maxOfs)
        {
            offset = maxOfs;
        }

        var lastOfs = hint - offset;
        offset = hint - tmp;

		_logger.LogWarning("<<< Exiting LeftRun(array={Array},key={Key},baseIndex={BaseIndex},hint={Hint},comparer={Comparer})", array, key, baseIndex, hint, comparer);
        return (offset, lastOfs);
    }

    private static (int Offset, int LastOfs) RightRun(T[] array, T key, int baseIndex, int len, int hint, IComparer<T> comparer)
    {
		_logger.LogError(">>> Entering RightRun(array={Array},key={Key},baseIndex={BaseIndex},len={Len},hint={Hint},comparer={Comparer})", array, key, baseIndex, len, hint, comparer);
        var (offset, lastOfs) = (1, 0);
        var maxOfs = len - hint;
        while (offset < maxOfs && comparer.Compare(key, array[baseIndex + hint + offset]) > 0)
        {
            lastOfs = offset;
			_logger.LogDebug("RightRun(array={Array},key={Key},baseIndex={BaseIndex},len={Len},hint={Hint},comparer={Comparer}): lastOfs → {LastOfs}", array, key, baseIndex, len, hint, comparer, lastOfs);
            offset = BoundLeftShift(offset);
			_logger.LogError("RightRun(array={Array},key={Key},baseIndex={BaseIndex},len={Len},hint={Hint},comparer={Comparer}): offset → {Offset}", array, key, baseIndex, len, hint, comparer, offset);
			_logger.LogDebug("RightRun(array={Array},key={Key},baseIndex={BaseIndex},len={Len},hint={Hint},comparer={Comparer}): offset → {Offset}", array, key, baseIndex, len, hint, comparer, offset);
        }

        if (offset > maxOfs)
        {
            offset = maxOfs;
        }

        offset += hint;
        lastOfs += hint;

		_logger.LogWarning("<<< Exiting RightRun(array={Array},key={Key},baseIndex={BaseIndex},len={Len},hint={Hint},comparer={Comparer})", array, key, baseIndex, len, hint, comparer);
        return (offset, lastOfs);
    }

    private static int FinalOffset(T[] array, T key, int baseIndex, int offset, int lastOfs, int lt, IComparer<T> comparer)
    {
		_logger.LogError(">>> Entering FinalOffset(array={Array},key={Key},baseIndex={BaseIndex},offset={Offset},lastOfs={LastOfs},lt={Lt},comparer={Comparer})", array, key, baseIndex, offset, lastOfs, lt, comparer);
        lastOfs++;
        while (lastOfs < offset)
        {
            var m = lastOfs + (int)((uint)(offset - lastOfs) >> 1);
			_logger.LogError("FinalOffset(array={Array},key={Key},baseIndex={BaseIndex},offset={Offset},lastOfs={LastOfs},lt={Lt},comparer={Comparer}): m → {M}", array, key, baseIndex, offset, lastOfs, lt, comparer, m);

            if (comparer.Compare(key, array[baseIndex + m]) < lt)
            {
                offset = m;
            }
            else
            {
                lastOfs = m + 1;
            }
        }

		_logger.LogWarning("<<< Exiting FinalOffset(array={Array},key={Key},baseIndex={BaseIndex},offset={Offset},lastOfs={LastOfs},lt={Lt},comparer={Comparer})", array, key, baseIndex, offset, lastOfs, lt, comparer);
        return offset;
    }
}
