using Microsoft.Extensions.Logging;
namespace Algorithms.Sorters.External;

public class ExternalMergeSorter<T> : IExternalSorter<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public void Sort(
        ISequentialStorage<T> mainMemory,
        ISequentialStorage<T> temporaryMemory,
        IComparer<T> comparer)
    {
		_logger.LogDebug(">>> Entering Sort(mainMemory={MainMemory},temporaryMemory={TemporaryMemory},comparer={Comparer})", mainMemory, temporaryMemory, comparer);
        var originalSource = mainMemory;
        var source = mainMemory;
        var temp = temporaryMemory;
        var totalLength = mainMemory.Length;
        for (var stripLength = 1L; stripLength < totalLength; stripLength *= 2)
        {
            using var left = source.GetReader();
			_logger.LogDebug("Sort(mainMemory={MainMemory},temporaryMemory={TemporaryMemory},comparer={Comparer}): left → {Left}", mainMemory, temporaryMemory, comparer, left);
            using var right = source.GetReader();
			_logger.LogDebug("Sort(mainMemory={MainMemory},temporaryMemory={TemporaryMemory},comparer={Comparer}): right → {Right}", mainMemory, temporaryMemory, comparer, right);
            using var output = temp.GetWriter();
			_logger.LogDebug("Sort(mainMemory={MainMemory},temporaryMemory={TemporaryMemory},comparer={Comparer}): output → {Output}", mainMemory, temporaryMemory, comparer, output);

            for (var i = 0L; i < stripLength; i++)
            {
                right.Read();
            }

            Merge(left, right, output, stripLength, Math.Min(stripLength, totalLength - stripLength), comparer);
            var step = 2 * stripLength;
            long rightStripStart;
            for (rightStripStart = stripLength + step; rightStripStart < mainMemory.Length; rightStripStart += step)
            {
                for (var i = 0L; i < stripLength; i++)
                {
                    left.Read();
                    right.Read();
                }

                Merge(
                    left,
                    right,
                    output,
                    stripLength,
                    Math.Min(stripLength, totalLength - rightStripStart),
                    comparer);
            }

            for (var i = 0L; i < totalLength + stripLength - rightStripStart; i++)
            {
                output.Write(right.Read());
            }

            (source, temp) = (temp, source);
        }

        if (source == originalSource)
        {
            return;
        }

        using var sorted = source.GetReader();
		_logger.LogDebug("Sort(mainMemory={MainMemory},temporaryMemory={TemporaryMemory},comparer={Comparer}): sorted → {Sorted}", mainMemory, temporaryMemory, comparer, sorted);
        using var dest = originalSource.GetWriter();
		_logger.LogDebug("Sort(mainMemory={MainMemory},temporaryMemory={TemporaryMemory},comparer={Comparer}): dest → {Dest}", mainMemory, temporaryMemory, comparer, dest);
        for (var i = 0; i < totalLength; i++)
        {
            dest.Write(sorted.Read());
        }
		_logger.LogDebug("<<< Exiting Sort(mainMemory={MainMemory},temporaryMemory={TemporaryMemory},comparer={Comparer})", mainMemory, temporaryMemory, comparer);
    }

    private static void Merge(
        ISequentialStorageReader<T> left,
        ISequentialStorageReader<T> right,
        ISequentialStorageWriter<T> output,
        long leftLength,
        long rightLength,
        IComparer<T> comparer)
    {
		_logger.LogDebug(">>> Entering Merge(left={Left},right={Right},output={Output},leftLength={LeftLength},rightLength={RightLength},comparer={Comparer})", left, right, output, leftLength, rightLength, comparer);
        var leftIndex = 0L;
        var rightIndex = 0L;

        var l = left.Read();
		_logger.LogDebug("Merge(left={Left},right={Right},output={Output},leftLength={LeftLength},rightLength={RightLength},comparer={Comparer}): l → {L}", left, right, output, leftLength, rightLength, comparer, l);
        var r = right.Read();
		_logger.LogDebug("Merge(left={Left},right={Right},output={Output},leftLength={LeftLength},rightLength={RightLength},comparer={Comparer}): r → {R}", left, right, output, leftLength, rightLength, comparer, r);
        while (true)
        {
            if (comparer.Compare(l, r) < 0)
            {
                output.Write(l);
                leftIndex++;
                if (leftIndex == leftLength)
                {
                    break;
                }

                l = left.Read();
            }
            else
            {
                output.Write(r);
                rightIndex++;
                if (rightIndex == rightLength)
                {
                    break;
                }

                r = right.Read();
            }
        }

        if (leftIndex < leftLength)
        {
            output.Write(l);
            Copy(left, output, leftLength - leftIndex - 1);
        }

        if (rightIndex < rightLength)
        {
            output.Write(r);
            Copy(right, output, rightLength - rightIndex - 1);
        }
		_logger.LogDebug("<<< Exiting Merge(left={Left},right={Right},output={Output},leftLength={LeftLength},rightLength={RightLength},comparer={Comparer})", left, right, output, leftLength, rightLength, comparer);
    }

    private static void Copy(ISequentialStorageReader<T> from, ISequentialStorageWriter<T> to, long count)
    {
		_logger.LogDebug(">>> Entering Copy(from={From},to={To},count={Count})", from, to, count);
        for (var i = 0; i < count; i++)
        {
            to.Write(from.Read());
        }
		_logger.LogDebug("<<< Exiting Copy(from={From},to={To},count={Count})", from, to, count);
    }
}
