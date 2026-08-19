using Algorithms.Sorters.Integer;
using Algorithms.Tests.Helpers;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sorters.Integer;

public static class CountingSorterTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void SortsNonEmptyArray(
        [Random(1, 10000, 100, Distinct = true)]
        int n)
    {
		_logger.LogDebug(">>> Entering SortsNonEmptyArray(Distinct={Distinct})", Distinct);
        // Arrange
        var sorter = new CountingSorter();
		_logger.LogDebug("SortsNonEmptyArray(Distinct={Distinct}): sorter → {Sorter}", Distinct, sorter);
        var (correctArray, testArray) = RandomHelper.GetArrays(n);

        // Act
        sorter.Sort(testArray);
        Array.Sort(correctArray);

        // Assert
        Assert.That(testArray, Is.EqualTo(correctArray));
		_logger.LogDebug("<<< Exiting SortsNonEmptyArray(Distinct={Distinct})", Distinct);
    }

    [Test]
    public static void SortsEmptyArray()
    {
		_logger.LogDebug(">>> Entering SortsEmptyArray()");
        // Arrange
        var sorter = new CountingSorter();
		_logger.LogDebug("SortsEmptyArray(): sorter → {Sorter}", sorter);
        var (correctArray, testArray) = RandomHelper.GetArrays(0);

        // Act
        sorter.Sort(testArray);
        Array.Sort(correctArray);

        // Assert
        Assert.That(testArray, Is.Empty);
		_logger.LogDebug("<<< Exiting SortsEmptyArray()");
    }
}
