using DataStructures.Heap;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Heap;

[TestFixture]
public static class MinMaxHeapTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private static readonly object[] CollectionsSource =
    [
        new[] { 5, 10, -2, 0, 3, 13, 5, -8, 41, -5, -7, -60, -12 },
        new[] { 'e', '4', 'x', 'D', '!', '$', '-', '_', '2', ')', 'Z', 'q' },
        new[] { "abc", "abc", "xyz", "bcd", "klm", "opq", "ijk" },
    ];

    [Test]
    public static void CustomComparerTest()
    {
		_logger.LogDebug(">>> Entering CustomComparerTest()");
        var arr = new[] { "aaaa", "c", "dd", "bbb" };
        var comparer = Comparer<string>.Create((a, b) => Comparer<int>.Default.Compare(a.Length, b.Length));
		_logger.LogDebug("CustomComparerTest(): comparer → {Comparer}", comparer);
		_logger.LogDebug("CustomComparerTest(): arr → {Arr}", arr);

        var mmh = new MinMaxHeap<string>(comparer: comparer);
		_logger.LogDebug("CustomComparerTest(): mmh → {Mmh}", mmh);
        foreach (var s in arr)
        {
            mmh.Add(s);
        }

        Assert.That(comparer, Is.EqualTo(mmh.Comparer));
        Assert.That("c", Is.EqualTo(mmh.GetMin()));
        Assert.That("aaaa", Is.EqualTo(mmh.GetMax()));
		_logger.LogDebug("<<< Exiting CustomComparerTest()");
    }

    [TestCaseSource(nameof(CollectionsSource))]
    public static void AddTest<T>(IEnumerable<T> collection)
    {
		_logger.LogDebug(">>> Entering AddTest(collection={Collection})", collection);
        var mmh = new MinMaxHeap<T>();
        foreach (var item in collection)
        {
		_logger.LogDebug("AddTest(collection={Collection}): mmh → {Mmh}", collection, mmh);
            mmh.Add(item);
        }

        var minValue = mmh.GetMin();
		_logger.LogDebug("AddTest(collection={Collection}): minValue → {MinValue}", collection, minValue);
        var maxValue = mmh.GetMax();
		_logger.LogDebug("AddTest(collection={Collection}): maxValue → {MaxValue}", collection, maxValue);

        Assert.That(collection.Min(), Is.EqualTo(minValue));
        Assert.That(collection.Max(), Is.EqualTo(maxValue));
        Assert.That(collection.Count(), Is.EqualTo(mmh.Count));
		_logger.LogDebug("<<< Exiting AddTest(collection={Collection})", collection);
    }

    [TestCaseSource(nameof(CollectionsSource))]
    public static void ExtractMaxTest<T>(IEnumerable<T> collection)
    {
		_logger.LogDebug(">>> Entering ExtractMaxTest(collection={Collection})", collection);
        var ordered = collection.OrderByDescending(x => x);
        var mmh = new MinMaxHeap<T>(collection);
		_logger.LogDebug("ExtractMaxTest(collection={Collection}): mmh → {Mmh}", collection, mmh);
        var emptyHeap = new MinMaxHeap<int>();
		_logger.LogDebug("ExtractMaxTest(collection={Collection}): emptyHeap → {EmptyHeap}", collection, emptyHeap);
		_logger.LogDebug("ExtractMaxTest(collection={Collection}): ordered → {Ordered}", collection, ordered);

        var first = mmh.ExtractMax();
		_logger.LogDebug("ExtractMaxTest(collection={Collection}): first → {First}", collection, first);
        var second = mmh.GetMax();
		_logger.LogDebug("ExtractMaxTest(collection={Collection}): second → {Second}", collection, second);

        Assert.Throws<InvalidOperationException>(() => emptyHeap.ExtractMax());
        Assert.That(ordered.ElementAt(0), Is.EqualTo(first));
        Assert.That(ordered.ElementAt(1), Is.EqualTo(second));
        Assert.That(collection.Count() - 1, Is.EqualTo(mmh.Count));
		_logger.LogDebug("<<< Exiting ExtractMaxTest(collection={Collection})", collection);
    }

    [TestCaseSource(nameof(CollectionsSource))]
    public static void ExtractMinTest<T>(IEnumerable<T> collection)
    {
		_logger.LogDebug(">>> Entering ExtractMinTest(collection={Collection})", collection);
        var ordered = collection.OrderBy(x => x);
        var mmh = new MinMaxHeap<T>(collection);
		_logger.LogDebug("ExtractMinTest(collection={Collection}): mmh → {Mmh}", collection, mmh);
        var emptyHeap = new MinMaxHeap<int>();
		_logger.LogDebug("ExtractMinTest(collection={Collection}): emptyHeap → {EmptyHeap}", collection, emptyHeap);
		_logger.LogDebug("ExtractMinTest(collection={Collection}): ordered → {Ordered}", collection, ordered);

        var first = mmh.ExtractMin();
		_logger.LogDebug("ExtractMinTest(collection={Collection}): first → {First}", collection, first);
        var second = mmh.GetMin();
		_logger.LogDebug("ExtractMinTest(collection={Collection}): second → {Second}", collection, second);

        Assert.Throws<InvalidOperationException>(() => emptyHeap.ExtractMin());
        Assert.That(ordered.ElementAt(0), Is.EqualTo(first));
        Assert.That(ordered.ElementAt(1), Is.EqualTo(second));
        Assert.That(collection.Count() - 1, Is.EqualTo(mmh.Count));
		_logger.LogDebug("<<< Exiting ExtractMinTest(collection={Collection})", collection);
    }

    [TestCaseSource(nameof(CollectionsSource))]
    public static void GetMaxTest<T>(IEnumerable<T> collection)
    {
		_logger.LogDebug(">>> Entering GetMaxTest(collection={Collection})", collection);
        var emptyHeap = new MinMaxHeap<T>();
        var mmh = new MinMaxHeap<T>(collection);
		_logger.LogDebug("GetMaxTest(collection={Collection}): mmh → {Mmh}", collection, mmh);
		_logger.LogDebug("GetMaxTest(collection={Collection}): emptyHeap → {EmptyHeap}", collection, emptyHeap);

        var maxValue = mmh.GetMax();
		_logger.LogDebug("GetMaxTest(collection={Collection}): maxValue → {MaxValue}", collection, maxValue);

        Assert.Throws<InvalidOperationException>(() => emptyHeap.GetMax());
        Assert.That(collection.Max(), Is.EqualTo(maxValue));
		_logger.LogDebug("<<< Exiting GetMaxTest(collection={Collection})", collection);
    }

    [TestCaseSource(nameof(CollectionsSource))]
    public static void GetMinTest<T>(IEnumerable<T> collection)
    {
		_logger.LogDebug(">>> Entering GetMinTest(collection={Collection})", collection);
        var emptyHeap = new MinMaxHeap<T>();
        var mmh = new MinMaxHeap<T>(collection);
		_logger.LogDebug("GetMinTest(collection={Collection}): mmh → {Mmh}", collection, mmh);
		_logger.LogDebug("GetMinTest(collection={Collection}): emptyHeap → {EmptyHeap}", collection, emptyHeap);

        var minValue = mmh.GetMin();
		_logger.LogDebug("GetMinTest(collection={Collection}): minValue → {MinValue}", collection, minValue);

        Assert.Throws<InvalidOperationException>(() => emptyHeap.GetMin());
        Assert.That(collection.Min(), Is.EqualTo(minValue));
		_logger.LogDebug("<<< Exiting GetMinTest(collection={Collection})", collection);
    }

    [Test]
    public static void HeapSortUsingGet<T>(
        [ValueSource(nameof(CollectionsSource))] IEnumerable<T> collection,
        [Values] bool ascending)
    {
		_logger.LogDebug(">>> Entering HeapSortUsingGet(collection={Collection},ascending={Ascending})", collection, ascending);
        var ordered = ascending ? collection.OrderBy(x => x) : collection.OrderByDescending(x => x);
        var mmh = new MinMaxHeap<T>(collection);
		_logger.LogDebug("HeapSortUsingGet(collection={Collection},ascending={Ascending}): mmh → {Mmh}", collection, ascending, mmh);
        var extracted = new List<T>();
		_logger.LogDebug("HeapSortUsingGet(collection={Collection},ascending={Ascending}): extracted → {Extracted}", collection, ascending, extracted);
		_logger.LogDebug("HeapSortUsingGet(collection={Collection},ascending={Ascending}): ordered → {Ordered}", collection, ascending, ordered);

        while (mmh.Count > 0)
        {
            T value;
            if (ascending)
            {
                value = mmh.GetMin();
                _ = mmh.ExtractMin();
				_logger.LogDebug("HeapSortUsingGet(collection={Collection},ascending={Ascending}): _ → {_}", collection, ascending, _);
				_logger.LogDebug("HeapSortUsingGet(collection={Collection},ascending={Ascending}): value → {Value}", collection, ascending, value);
            }
            else
            {
                value = mmh.GetMax();
                _ = mmh.ExtractMax();
            }

            extracted.Add(value);
        }

        Assert.That(ordered.SequenceEqual(extracted), Is.True);
		_logger.LogDebug("<<< Exiting HeapSortUsingGet(collection={Collection},ascending={Ascending})", collection, ascending);
    }

    [Test]
    public static void HeapSortUsingExtract<T>(
        [ValueSource(nameof(CollectionsSource))] IEnumerable<T> collection,
        [Values] bool ascending)
    {
		_logger.LogDebug(">>> Entering HeapSortUsingExtract(collection={Collection},ascending={Ascending})", collection, ascending);
        var ordered = ascending ? collection.OrderBy(x => x) : collection.OrderByDescending(x => x);
        var mmh = new MinMaxHeap<T>(collection);
		_logger.LogDebug("HeapSortUsingExtract(collection={Collection},ascending={Ascending}): mmh → {Mmh}", collection, ascending, mmh);
        var extracted = new List<T>();
		_logger.LogDebug("HeapSortUsingExtract(collection={Collection},ascending={Ascending}): extracted → {Extracted}", collection, ascending, extracted);
		_logger.LogDebug("HeapSortUsingExtract(collection={Collection},ascending={Ascending}): ordered → {Ordered}", collection, ascending, ordered);

        while (mmh.Count > 0)
        {
            var value = ascending ? mmh.ExtractMin() : mmh.ExtractMax();
			_logger.LogDebug("HeapSortUsingExtract(collection={Collection},ascending={Ascending}): value → {Value}", collection, ascending, value);
            extracted.Add(value);
        }

        Assert.That(ordered.SequenceEqual(extracted), Is.True);
		_logger.LogDebug("<<< Exiting HeapSortUsingExtract(collection={Collection},ascending={Ascending})", collection, ascending);
    }
}
