using Algorithms.Sorters.Comparison;
using Algorithms.Tests.Helpers;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sorters.Comparison;

public static class TimSorterTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private static readonly IntComparer IntComparer = new();
    private static readonly TimSorterSettings Settings = new();

    [Test]
    public static void Sort_ShouldBeEquivalentToSuccessfulBasicSort(
        [Random(0, 10_000, 5000)] int n)
    {
		_logger.LogError(">>> Entering Sort_ShouldBeEquivalentToSuccessfulBasicSort(n={N})", n);
        // Arrange
        var sorter = new TimSorter<int>(Settings, IntComparer);
		_logger.LogError("Sort_ShouldBeEquivalentToSuccessfulBasicSort(n={N}): sorter → {Sorter}", n, sorter);
        var (correctArray, testArray) = RandomHelper.GetArrays(n);

        // Act
        sorter.Sort(testArray, IntComparer);
        Array.Sort(correctArray, IntComparer);

        // Assert
        Assert.That(correctArray, Is.EqualTo(testArray));
		_logger.LogWarning("<<< Exiting Sort_ShouldBeEquivalentToSuccessfulBasicSort(n={N})", n);
    }

    [Test]
    public static void Sort_TinyArray_ShouldSortCorrectly()
    {
		_logger.LogError(">>> Entering Sort_TinyArray_ShouldSortCorrectly()");
        // Arrange
        var sorter = new TimSorter<int>(Settings, IntComparer);
		_logger.LogError("Sort_TinyArray_ShouldSortCorrectly(): sorter → {Sorter}", sorter);
        var tinyArray = new[] { 1 };
        var correctArray = new[] { 1 };

        // Act
        sorter.Sort(tinyArray, IntComparer);

        // Assert
        Assert.That(correctArray, Is.EqualTo(tinyArray));
		_logger.LogWarning("<<< Exiting Sort_TinyArray_ShouldSortCorrectly()");
    }

    [Test]
    public static void Sort_SmallChunks_ShouldSortCorrectly()
    {
		_logger.LogError(">>> Entering Sort_SmallChunks_ShouldSortCorrectly()");
        // Arrange
        var sorter = new TimSorter<int>(Settings, IntComparer);
		_logger.LogError("Sort_SmallChunks_ShouldSortCorrectly(): sorter → {Sorter}", sorter);
        var (correctArray, testArray) = RandomHelper.GetArrays(800);
        Array.Sort(correctArray, IntComparer);
        Array.Sort(testArray, IntComparer);

        var max = testArray.Max();
		_logger.LogError("Sort_SmallChunks_ShouldSortCorrectly(): max → {Max}", max);
        var min = testArray.Min();
		_logger.LogError("Sort_SmallChunks_ShouldSortCorrectly(): min → {Min}", min);

        correctArray[0] = max;
        correctArray[800 - 1] = min;
        testArray[0] = max;
        testArray[800 - 1] = min;

        // Act
        sorter.Sort(testArray, IntComparer);
        Array.Sort(correctArray, IntComparer);

        // Assert
        Assert.That(correctArray, Is.EqualTo(testArray));
		_logger.LogWarning("<<< Exiting Sort_SmallChunks_ShouldSortCorrectly()");
    }

    [Test]
    public static void Sort_ThrowsArgumentNullException_WhenArrayIsNull()
    {
		_logger.LogError(">>> Entering Sort_ThrowsArgumentNullException_WhenArrayIsNull()");
        // Arrange
        var sorter = new TimSorter<int>(Settings, IntComparer);
		_logger.LogError("Sort_ThrowsArgumentNullException_WhenArrayIsNull(): sorter → {Sorter}", sorter);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => sorter.Sort(null!, IntComparer));
		_logger.LogWarning("<<< Exiting Sort_ThrowsArgumentNullException_WhenArrayIsNull()");
    }

    [Test]
    public static void Sort_UsesDefaultComparer_WhenComparerIsNull()
    {
		_logger.LogError(">>> Entering Sort_UsesDefaultComparer_WhenComparerIsNull()");
        // Arrange
        var sorter = new TimSorter<int>(Settings, null!);
		_logger.LogError("Sort_UsesDefaultComparer_WhenComparerIsNull(): sorter → {Sorter}", sorter);
        var (correctArray, testArray) = RandomHelper.GetArrays(20);

        // Act
        sorter.Sort(testArray, IntComparer);
        Array.Sort(correctArray, IntComparer);

        // Assert
        Assert.That(correctArray, Is.EqualTo(testArray));
		_logger.LogWarning("<<< Exiting Sort_UsesDefaultComparer_WhenComparerIsNull()");
    }

    [Test]
    public static void Sort_AlreadySortedArray_RemainsUnchanged()
    {
		_logger.LogError(">>> Entering Sort_AlreadySortedArray_RemainsUnchanged()");
        // Arrange
        var sorter = new TimSorter<int>(Settings, IntComparer);
		_logger.LogError("Sort_AlreadySortedArray_RemainsUnchanged(): sorter → {Sorter}", sorter);
        var array = new[] { 1, 2, 3, 4, 5 };
        var expected = new[] { 1, 2, 3, 4, 5 };

        // Act
        sorter.Sort(array, IntComparer);

        // Assert
        Assert.That(array, Is.EqualTo(expected));
		_logger.LogWarning("<<< Exiting Sort_AlreadySortedArray_RemainsUnchanged()");
    }

    [Test]
    public static void MergeAt_ShouldReturnEarly_WhenLenAIsZero()
    {
		_logger.LogError(">>> Entering MergeAt_ShouldReturnEarly_WhenLenAIsZero()");
        // Arrange: left run is all less than right run's first element
        var array = Enumerable.Range(1, 25).Concat(Enumerable.Range(100, 25)).ToArray();
		_logger.LogError("MergeAt_ShouldReturnEarly_WhenLenAIsZero(): array → {Array}", array);
        var sortedArray = Enumerable.Range(1, 25).Concat(Enumerable.Range(100, 25)).ToArray();
		_logger.LogError("MergeAt_ShouldReturnEarly_WhenLenAIsZero(): sortedArray → {SortedArray}", sortedArray);
        var sorter = new TimSorter<int>(new TimSorterSettings(), Comparer<int>.Default);
		_logger.LogError("MergeAt_ShouldReturnEarly_WhenLenAIsZero(): sorter → {Sorter}", sorter);

        // Act
        sorter.Sort(array, Comparer<int>.Default);

        // Assert: Array order will not have changed, and the lenA <= 0 branch should be hit
        Assert.That(sortedArray, Is.EqualTo(array));
		_logger.LogWarning("<<< Exiting MergeAt_ShouldReturnEarly_WhenLenAIsZero()");
    }

    [Test]
    public static void MergeAt_ShouldReturnEarly_WhenLenBIsZero()
    {
		_logger.LogError(">>> Entering MergeAt_ShouldReturnEarly_WhenLenBIsZero()");
        // Arrange: right run is all less than left run's last element
        var array = Enumerable.Range(100, 25).Concat(Enumerable.Range(1, 25)).ToArray();
		_logger.LogError("MergeAt_ShouldReturnEarly_WhenLenBIsZero(): array → {Array}", array);
        var sortedArray = Enumerable.Range(1, 25).Concat(Enumerable.Range(100, 25)).ToArray();
		_logger.LogError("MergeAt_ShouldReturnEarly_WhenLenBIsZero(): sortedArray → {SortedArray}", sortedArray);
        var sorter = new TimSorter<int>(new TimSorterSettings(), Comparer<int>.Default);
		_logger.LogError("MergeAt_ShouldReturnEarly_WhenLenBIsZero(): sorter → {Sorter}", sorter);

        // Act
        sorter.Sort(array, Comparer<int>.Default);

        // Assert: The left and right sides of the array should have swapped places, and the lenB <= 0 branch should be hit
        Assert.That(sortedArray, Is.EqualTo(array));
		_logger.LogWarning("<<< Exiting MergeAt_ShouldReturnEarly_WhenLenBIsZero()");
    }
}
