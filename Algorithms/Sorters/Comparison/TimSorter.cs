using Algorithms.Sorters.Utils;
using Microsoft.Extensions.Logging;

namespace Algorithms.Sorters.Comparison;

/// <summary>
///     Timsort is a hybrid stable sorting algorithm, derived from merge sort and insertion sort, designed to perform well on many kinds of real-world data.
///     It was originally implemented by Tim Peters in 2002 for use in the Python programming language.
///
///     This class is based on a Java interpretation of Tim Peter's original work.
///     Java class is viewable here:
///     https://web.archive.org/web/20190119032242/http://cr.openjdk.java.net:80/~martin/webrevs/openjdk7/timsort/raw_files/new/src/share/classes/java/util/TimSort.java
///
///     Tim Peters's list sort for Python, is described in detail here:
///     http://svn.python.org/projects/python/trunk/Objects/listsort.txt
///
///     Tim's C code may be found here: http://svn.python.org/projects/python/trunk/Objects/listobject.c
///
///     The underlying techniques are described in this paper (and may have even earlier origins):
///     "Optimistic Sorting and Information Theoretic Complexity"
///     Peter McIlroy
///     SODA (Fourth Annual ACM-SIAM Symposium on Discrete Algorithms),
///     pp 467-474, Austin, Texas, 25-27 January 1993.
/// </summary>
/// <typeparam name="T">Type of array element.</typeparam>
public class TimSorter<T> : IComparisonSorter<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly int minMerge;
    private readonly int initMinGallop;

    private readonly int[] runBase;
    private readonly int[] runLengths;

    private int minGallop;
    private int stackSize;

    private IComparer<T> comparer = default!;

    /// <summary>
    /// Private class for handling gallop merges, allows for tracking array indexes and wins.
    /// </summary>
    /// <typeparam name="Tc">Type of array element.</typeparam>
    private class TimChunk<Tc>
    {
    	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        public Tc[] Array { get; set; } = default!;

        public int Index { get; set; }

        public int Remaining { get; set; }

        public int Wins { get; set; }
    }

    public TimSorter(TimSorterSettings settings, IComparer<T> comparer)
    {
		_logger.LogError(">>> Entering TimSorter(settings={Settings},comparer={Comparer})", settings, comparer);
        initMinGallop = minGallop;

		_logger.LogDebug("TimSorter(settings={Settings},comparer={Comparer}): initMinGallop → {InitMinGallop}", settings, comparer, initMinGallop);
        // Using the worst case stack size from the C implementation.
        // Based on the findings in the original listsort.txt:
        // ... the stack can never grow larger than about log_base_phi(N) entries, where phi = (1 + sqrt(5)) / 2 ~= 1.618.
        // Thus a small # of stack slots suffice for very large arrays ...
        runBase = new int[85];
		_logger.LogDebug("TimSorter(settings={Settings},comparer={Comparer}): runBase → {RunBase}", settings, comparer, runBase);
        runLengths = new int[85];
		_logger.LogDebug("TimSorter(settings={Settings},comparer={Comparer}): runLengths → {RunLengths}", settings, comparer, runLengths);

        stackSize = 0;
		_logger.LogDebug("TimSorter(settings={Settings},comparer={Comparer}): stackSize → {StackSize}", settings, comparer, stackSize);

        minGallop = settings.MinGallop;
		_logger.LogDebug("TimSorter(settings={Settings},comparer={Comparer}): minGallop → {MinGallop}", settings, comparer, minGallop);
        minMerge = settings.MinMerge;
		_logger.LogDebug("TimSorter(settings={Settings},comparer={Comparer}): minMerge → {MinMerge}", settings, comparer, minMerge);

		_logger.LogWarning("<<< Exiting TimSorter(settings={Settings},comparer={Comparer})", settings, comparer);
        this.comparer = comparer ?? Comparer<T>.Default;
		_logger.LogDebug("TimSorter(settings={Settings},comparer={Comparer}): this.comparer → {Arg2}", settings, comparer, this.comparer);
    }

    /// <summary>
    ///     Sorts array using specified comparer
    ///     worst case performance: O(n log(n)),
    ///     best case performance:  O(n),
    ///     See <a href="https://en.wikipedia.org/wiki/Timsort">here</a> for more info.
    /// </summary>
    /// <param name="array">Array to sort.</param>
    /// <param name="comparer">Compares elements.</param>
    public void Sort(T[] array, IComparer<T> comparer)
    {
		_logger.LogError(">>> Entering Sort(array={Array},comparer={Comparer})", array, comparer);
        ArgumentNullException.ThrowIfNull(array);

        var start = 0;
        var remaining = array.Length;

        if (remaining < minMerge)
        {
            if (remaining < 2)
            {
                // Arrays of size 0 or 1 are always sorted.
                return;
            }

            // Don't need to merge, just binary sort
            BinarySort(array, start, remaining, start);
            return;
        }

        var minRun = MinRunLength(remaining, minMerge);
		_logger.LogError("Sort(array={Array},comparer={Comparer}): minRun → {MinRun}", array, comparer, minRun);

        do
        {
            // Identify next run
            var runLen = CountRunAndMakeAscending(array, start);
			_logger.LogError("Sort(array={Array},comparer={Comparer}): runLen → {RunLen}", array, comparer, runLen);

            // If the run is too short extend to Min(MIN_RUN, remaining)
            if (runLen < minRun)
            {
                var force = Math.Min(minRun, remaining);
				_logger.LogError("Sort(array={Array},comparer={Comparer}): force → {Force}", array, comparer, force);
                BinarySort(array, start, start + force, start + runLen);
                runLen = force;
            }

            runBase[stackSize] = start;
            runLengths[stackSize] = runLen;
            stackSize++;

            MergeCollapse(array);

            start += runLen;
            remaining -= runLen;
        }
        while (remaining != 0);

        MergeForceCollapse(array);
		_logger.LogWarning("<<< Exiting Sort(array={Array},comparer={Comparer})", array, comparer);
    }

    /// <summary>
    /// Returns the minimum acceptable run length for an array of the specified
    /// length.Natural runs shorter than this will be extended.
    ///
    /// Computation is:
    ///   If total less than minRun, return n (it's too small to bother with fancy stuff).
    ///   Else if total is an exact power of 2, return minRun/2.
    ///   Else return an int k, where <![CDATA[minRun/2 <= k <= minRun]]>, such that total/k
    ///     is close to, but strictly less than, an exact power of 2.
    /// </summary>
    /// <param name="total">Total length remaining to sort.</param>
    /// <returns>Minimum run length to be merged.</returns>
    private static int MinRunLength(int total, int minRun)
    {
		_logger.LogError(">>> Entering MinRunLength(total={Total},minRun={MinRun})", total, minRun);
        var r = 0;
        while (total >= minRun)
        {
            r |= total & 1;
            total >>= 1;
        }

		_logger.LogWarning("<<< Exiting MinRunLength(total={Total},minRun={MinRun})", total, minRun);
        return total + r;
    }

    /// <summary>
    /// Reverse the specified range of the specified array.
    /// </summary>
    /// <param name="array">the array in which a range is to be reversed.</param>
    /// <param name="start">the index of the first element in the range to be reversed.</param>
    /// <param name="end">the index after the last element in the range to be reversed.</param>
    private static void ReverseRange(T[] array, int start, int end)
    {
		_logger.LogError(">>> Entering ReverseRange(array={Array},start={Start},end={End})", array, start, end);
        _logger.LogError(">>> Entering ReverseRange(array={Array},start={Start},end={End})", array, start, end);
        end--;
        while (start < end)
        {
            var t = array[start];
            array[start++] = array[end];
            array[end--] = t;
        }
		_logger.LogWarning("<<< Exiting ReverseRange(array={Array},start={Start},end={End})", array, start, end);
    }

    /// <summary>
    /// Check the chunks before getting in to a merge to make sure there's something to actually do.
    /// </summary>
    /// <param name="left">TimChunk of the left hand side.</param>
    /// <param name="right">TimChunk of the right hand side.</param>
    /// <param name="dest">The current target point for the remaining values.</param>
    /// <returns>If a merge is required.</returns>
    private static bool NeedsMerge(TimChunk<T> left, TimChunk<T> right, ref int dest)
    {
		_logger.LogError(">>> Entering NeedsMerge(left={Left},right={Right},dest={Dest})", left, right, dest);
        right.Array[dest++] = right.Array[right.Index++];
        if (--right.Remaining == 0)
        {
            Array.Copy(left.Array, left.Index, right.Array, dest, left.Remaining);
            return false;
        }

        if (left.Remaining == 1)
        {
            Array.Copy(right.Array, right.Index, right.Array, dest, right.Remaining);
            right.Array[dest + right.Remaining] = left.Array[left.Index];
            return false;
        }

		_logger.LogWarning("<<< Exiting NeedsMerge(left={Left},right={Right},dest={Dest})", left, right, dest);
        return true;
    }

    /// <summary>
    /// Moves over the last parts of the chunks.
    /// </summary>
    /// <param name="left">TimChunk of the left hand side.</param>
    /// <param name="right">TimChunk of the right hand side.</param>
    /// <param name="dest">The current target point for the remaining values.</param>
    private static void FinalizeMerge(TimChunk<T> left, TimChunk<T> right, int dest)
    {
		_logger.LogError(">>> Entering FinalizeMerge(left={Left},right={Right},dest={Dest})", left, right, dest);
        if (left.Remaining == 1)
        {
            Array.Copy(right.Array, right.Index, right.Array, dest, right.Remaining);
            right.Array[dest + right.Remaining] = left.Array[left.Index];
        }
        else if (left.Remaining == 0)
        {
			_logger.LogError("Throwing ArgumentException from FinalizeMerge(left={Left},right={Right},dest={Dest})", left, right, dest);
            throw new ArgumentException("Comparison method violates its general contract!");
        }
        else
        {
            Array.Copy(left.Array, left.Index, right.Array, dest, left.Remaining);
        }
		_logger.LogWarning("<<< Exiting FinalizeMerge(left={Left},right={Right},dest={Dest})", left, right, dest);
    }

    /// <summary>
    /// Returns the length of the run beginning at the specified position in
    /// the specified array and reverses the run if it is descending (ensuring
    /// that the run will always be ascending when the method returns).
    ///
    /// A run is the longest ascending sequence with:
    ///
    ///    <![CDATA[a[lo] <= a[lo + 1] <= a[lo + 2] <= ...]]>
    ///
    /// or the longest descending sequence with:
    ///
    ///    <![CDATA[a[lo] >  a[lo + 1] >  a[lo + 2] >  ...]]>
    ///
    /// For its intended use in a stable mergesort, the strictness of the
    /// definition of "descending" is needed so that the call can safely
    /// reverse a descending sequence without violating stability.
    /// </summary>
    /// <param name="array">the array in which a run is to be counted and possibly reversed.</param>
    /// <param name="start">index of the first element in the run.</param>
    /// <returns>the length of the run beginning at the specified position in the specified array.</returns>
    private int CountRunAndMakeAscending(T[] array, int start)
    {
		_logger.LogError(">>> Entering CountRunAndMakeAscending(array={Array},start={Start})", array, start);
        _logger.LogError(">>> Entering CountRunAndMakeAscending(array={Array},start={Start})", array, start);
        var runHi = start + 1;
        if (runHi == array.Length)
        {
            return 1;
        }

        // Find end of run, and reverse range if descending
        if (comparer.Compare(array[runHi++], array[start]) < 0)
        { // Descending
            while (runHi < array.Length && comparer.Compare(array[runHi], array[runHi - 1]) < 0)
            {
                runHi++;
            }

            ReverseRange(array, start, runHi);
        }
        else
        { // Ascending
            while (runHi < array.Length && comparer.Compare(array[runHi], array[runHi - 1]) >= 0)
            {
                runHi++;
            }
        }

        return runHi - start;
    }

    /// <summary>
    /// Sorts the specified portion of the specified array using a binary
    /// insertion sort. It requires O(n log n) compares, but O(n^2) data movement.
    /// </summary>
    /// <param name="array">Array to sort.</param>
    /// <param name="start">The index of the first element in the range to be sorted.</param>
    /// <param name="end">The index after the last element in the range to be sorted.</param>
    /// <param name="first">The index of the first element in the range that is not already known to be sorted, must be between start and end.</param>
    private void BinarySort(T[] array, int start, int end, int first)
    {
		_logger.LogError(">>> Entering BinarySort(array={Array},start={Start},end={End},first={First})", array, start, end, first);
        _logger.LogError(">>> Entering BinarySort(array={Array},start={Start},end={End},first={First})", array, start, end, first);
        if (first >= end || first <= start)
        {
            first = start + 1;
        }

        for (; first < end; first++)
        {
            var target = array[first];
            var targetInsertLocation = BinarySearch(array, start, first - 1, target);
			_logger.LogError("BinarySort(array={Array},start={Start},end={End},first={First}): targetInsertLocation → {TargetInsertLocation}", array, start, end, first, targetInsertLocation);
            Array.Copy(array, targetInsertLocation, array, targetInsertLocation + 1, first - targetInsertLocation);

            array[targetInsertLocation] = target;
        }
		_logger.LogWarning("<<< Exiting BinarySort(array={Array},start={Start},end={End},first={First})", array, start, end, first);
    }

    private int BinarySearch(T[] array, int left, int right, T target)
    {
		_logger.LogError(">>> Entering BinarySearch(array={Array},left={Left},right={Right},target={Target})", array, left, right, target);
        while (left < right)
        {
            var mid = (left + right) >> 1;
			_logger.LogError("BinarySearch(array={Array},left={Left},right={Right},target={Target}): mid → {Mid}", array, left, right, target, mid);
            if (comparer.Compare(target, array[mid]) < 0)
            {
                right = mid;
            }
            else
            {
                left = mid + 1;
            }
        }

		_logger.LogWarning("<<< Exiting BinarySearch(array={Array},left={Left},right={Right},target={Target})", array, left, right, target);
        return comparer.Compare(target, array[left]) < 0
            ? left
            : left + 1;
    }

    private void MergeCollapse(T[] array)
    {
		_logger.LogError(">>> Entering MergeCollapse(array={Array})", array);
        while (stackSize > 1)
        {
            var n = stackSize - 2;
            if (n > 0 && runLengths[n - 1] <= runLengths[n] + runLengths[n + 1])
            {
                if (runLengths[n - 1] < runLengths[n + 1])
                {
                    n--;
                }

                MergeAt(array, n);
            }
            else if (runLengths[n] <= runLengths[n + 1])
            {
                MergeAt(array, n);
            }
            else
            {
                break;
            }
        }
		_logger.LogWarning("<<< Exiting MergeCollapse(array={Array})", array);
    }

    private void MergeForceCollapse(T[] array)
    {
		_logger.LogError(">>> Entering MergeForceCollapse(array={Array})", array);
        while (stackSize > 1)
        {
            var n = stackSize - 2;
            if (n > 0 && runLengths[n - 1] < runLengths[n + 1])
            {
                n--;
            }

            MergeAt(array, n);
        }
		_logger.LogWarning("<<< Exiting MergeForceCollapse(array={Array})", array);
    }

    private void MergeAt(T[] array, int index)
    {
		_logger.LogError(">>> Entering MergeAt(array={Array},index={Index})", array, index);
        var baseA = runBase[index];
        var lenA = runLengths[index];
        var baseB = runBase[index + 1];
        var lenB = runLengths[index + 1];
		_logger.LogError("MergeAt(array={Array},index={Index}): lenB → {LenB}", array, index, lenB);

        runLengths[index] = lenA + lenB;

        if (index == stackSize - 3)
        {
            runBase[index + 1] = runBase[index + 2];
            runLengths[index + 1] = runLengths[index + 2];
        }

        stackSize--;

        var k = GallopingStrategy<T>.GallopRight(array, array[baseB], baseA, lenA, comparer);

        baseA += k;
        lenA -= k;

        if (lenA <= 0)
        {
            return;
        }

        lenB = GallopingStrategy<T>.GallopLeft(array, array[baseA + lenA - 1], baseB, lenB, comparer);

        if (lenB <= 0)
        {
            return;
        }

        Merge(array, baseA, lenA, baseB, lenB);
		_logger.LogWarning("<<< Exiting MergeAt(array={Array},index={Index})", array, index);
    }

    private void Merge(T[] array, int baseA, int lenA, int baseB, int lenB)
    {
        var endA = baseA + lenA;
        var dest = baseA;

        TimChunk<T> left = new()
        {
            Array = array[baseA..endA],
            Remaining = lenA,
        };

        TimChunk<T> right = new()
        {
            Array = array,
            Index = baseB,
            Remaining = lenB,
        };

        // Move first element of the right chunk and deal with degenerate cases.
        if (!TimSorter<T>.NeedsMerge(left, right, ref dest))
        {
            // One of the chunks had 0-1 items in it, so no need to merge anything.
            return;
        }

        var gallop = minGallop;

        while (RunMerge(left, right, ref dest, ref gallop))
        {
            // Penalize for leaving gallop mode
            gallop = gallop > 0
                ? gallop + 2
                : 2;
        }

        minGallop = gallop >= 1
            ? gallop
            : 1;

        FinalizeMerge(left, right, dest);
    }

    private bool RunMerge(TimChunk<T> left, TimChunk<T> right, ref int dest, ref int gallop)
    {
		_logger.LogError(">>> Entering RunMerge(left={Left},right={Right},dest={Dest},gallop={Gallop})", left, right, dest, gallop);
        // Reset the number of times in row a run wins.
        left.Wins = 0;
		_logger.LogDebug("RunMerge(left={Left},right={Right},dest={Dest},gallop={Gallop}): left.Wins → {Arg4}", left, right, dest, gallop, left.Wins);
        right.Wins = 0;
		_logger.LogDebug("RunMerge(left={Left},right={Right},dest={Dest},gallop={Gallop}): right.Wins → {Arg4}", left, right, dest, gallop, right.Wins);

        // Run a stable merge sort until (if ever) one run starts winning consistently.
        if (StableMerge(left, right, ref dest, gallop))
        {
            // Stable merge sort completed with no viable gallops, time to exit.
            return false;
        }

        // One run is winning so consistently that galloping may be a huge win.
        // So try that, and continue galloping until (if ever) neither run appears to be winning consistently anymore.
        do
        {
            if (GallopMerge(left, right, ref dest))
            {
                // Galloped all the way to the end, merge is complete.
                return false;
            }

            // We had a bit of a run, so make it easier to get started again.
            gallop--;
        }
        while (left.Wins >= initMinGallop || right.Wins >= initMinGallop);

		_logger.LogWarning("<<< Exiting RunMerge(left={Left},right={Right},dest={Dest},gallop={Gallop})", left, right, dest, gallop);
        return true;
    }

    private bool StableMerge(TimChunk<T> left, TimChunk<T> right, ref int dest, int gallop)
    {
		_logger.LogError(">>> Entering StableMerge(left={Left},right={Right},dest={Dest},gallop={Gallop})", left, right, dest, gallop);
        do
        {
            if (comparer.Compare(right.Array[right.Index], left.Array[left.Index]) < 0)
            {
                right.Array[dest++] = right.Array[right.Index++];
                right.Wins++;
                left.Wins = 0;
				_logger.LogDebug("StableMerge(left={Left},right={Right},dest={Dest},gallop={Gallop}): left.Wins → {Arg4}", left, right, dest, gallop, left.Wins);
                if (--right.Remaining == 0)
                {
                    return true;
                }
            }
            else
            {
                right.Array[dest++] = left.Array[left.Index++];
                left.Wins++;
                right.Wins = 0;
				_logger.LogDebug("StableMerge(left={Left},right={Right},dest={Dest},gallop={Gallop}): right.Wins → {Arg4}", left, right, dest, gallop, right.Wins);
                if (--left.Remaining == 1)
                {
                    return true;
                }
            }
        }
        while ((left.Wins | right.Wins) < gallop);

		_logger.LogWarning("<<< Exiting StableMerge(left={Left},right={Right},dest={Dest},gallop={Gallop})", left, right, dest, gallop);
        return false;
    }

    private bool GallopMerge(TimChunk<T> left, TimChunk<T> right, ref int dest)
    {
		_logger.LogError(">>> Entering GallopMerge(left={Left},right={Right},dest={Dest})", left, right, dest);
        left.Wins = GallopingStrategy<T>.GallopRight(left.Array, right.Array[right.Index], left.Index, left.Remaining, comparer);
        if (left.Wins != 0)
        {
            Array.Copy(left.Array, left.Index, right.Array, dest, left.Wins);
            dest += left.Wins;
            left.Index += left.Wins;
            left.Remaining -= left.Wins;
            if (left.Remaining <= 1)
            {
                return true;
            }
        }

        right.Array[dest++] = right.Array[right.Index++];
        if (--right.Remaining == 0)
        {
            return true;
        }

        right.Wins = GallopingStrategy<T>.GallopLeft(right.Array, left.Array[left.Index], right.Index, right.Remaining, comparer);
        if (right.Wins != 0)
        {
            Array.Copy(right.Array, right.Index, right.Array, dest, right.Wins);
            dest += right.Wins;
            right.Index += right.Wins;
            right.Remaining -= right.Wins;
            if (right.Remaining == 0)
            {
                return true;
            }
        }

        right.Array[dest++] = left.Array[left.Index++];
        if (--left.Remaining == 1)
        {
            return true;
        }

		_logger.LogWarning("<<< Exiting GallopMerge(left={Left},right={Right},dest={Dest})", left, right, dest);
        return false;
    }
}
