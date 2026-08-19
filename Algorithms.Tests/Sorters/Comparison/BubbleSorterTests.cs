using Algorithms.Sorters.Comparison;
using Algorithms.Tests.Helpers;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sorters.Comparison;

public static class BubbleSorterTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void ArraySorted(
        [Random(0, 1000, 100, Distinct = true)]
        int n)
    {
		_logger.LogDebug(">>> Entering ArraySorted(Distinct={Distinct})", Distinct);
        // Arrange
        var sorter = new BubbleSorter<int>();
		_logger.LogDebug("ArraySorted(Distinct={Distinct}): sorter → {Sorter}", Distinct, sorter);
        var intComparer = new IntComparer();
		_logger.LogDebug("ArraySorted(Distinct={Distinct}): intComparer → {IntComparer}", Distinct, intComparer);
        var (correctArray, testArray) = RandomHelper.GetArrays(n);

        // Act
        sorter.Sort(testArray, intComparer);
        Array.Sort(correctArray, intComparer);

        // Assert
        Assert.That(correctArray, Is.EqualTo(testArray));
		_logger.LogDebug("<<< Exiting ArraySorted(Distinct={Distinct})", Distinct);
    }
}
