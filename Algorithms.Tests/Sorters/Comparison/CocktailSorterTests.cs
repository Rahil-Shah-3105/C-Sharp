using Algorithms.Sorters.Comparison;
using Algorithms.Tests.Helpers;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sorters.Comparison;

public static class CocktailSorterTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void SortsArray(
        [Random(0, 1000, 100, Distinct = true)]
        int n)
    {
		_logger.LogDebug(">>> Entering SortsArray(Distinct={Distinct})", Distinct);
        // Arrange
        var sorter = new CocktailSorter<int>();
		_logger.LogDebug("SortsArray(Distinct={Distinct}): sorter → {Sorter}", Distinct, sorter);
        var intComparer = new IntComparer();
		_logger.LogDebug("SortsArray(Distinct={Distinct}): intComparer → {IntComparer}", Distinct, intComparer);
        var (correctArray, testArray) = RandomHelper.GetArrays(n);

        // Act
        sorter.Sort(testArray, intComparer);
        Array.Sort(correctArray);

        // Assert
        Assert.That(testArray, Is.EqualTo(correctArray));
		_logger.LogDebug("<<< Exiting SortsArray(Distinct={Distinct})", Distinct);
    }
}
