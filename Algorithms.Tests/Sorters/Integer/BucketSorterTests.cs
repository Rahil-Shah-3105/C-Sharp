using Algorithms.Sorters.Integer;
using Algorithms.Tests.Helpers;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sorters.Integer;

public static class BucketSorterTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void ArraySorted(
        [Random(0, 1000, 1000, Distinct = true)]
        int n)
    {
		_logger.LogDebug(">>> Entering ArraySorted(Distinct={Distinct})", Distinct);
        // Arrange
        var sorter = new BucketSorter();
		_logger.LogDebug("ArraySorted(Distinct={Distinct}): sorter → {Sorter}", Distinct, sorter);
        var (correctArray, testArray) = RandomHelper.GetArrays(n);

        // Act
        sorter.Sort(testArray);
        Array.Sort(correctArray);

        // Assert
        Assert.That(testArray, Is.EqualTo(correctArray));
		_logger.LogDebug("<<< Exiting ArraySorted(Distinct={Distinct})", Distinct);
    }
}
