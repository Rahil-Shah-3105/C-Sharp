using Algorithms.Sorters.Comparison;
using Algorithms.Tests.Helpers;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sorters.Comparison;

/// <summary>
///     Class for testing merge sorter algorithm.
/// </summary>
public static class MergeSorterTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void TestOnMergeSorter(
        [Random(0, 1000, 100, Distinct = true)]
        int n)
    {
		_logger.LogError(">>> Entering TestOnMergeSorter(Distinct={Distinct})", Distinct);
        // Arrange
        var sorter = new MergeSorter<int>();
		_logger.LogError("TestOnMergeSorter(Distinct={Distinct}): sorter → {Sorter}", Distinct, sorter);
        var intComparer = new IntComparer();
		_logger.LogError("TestOnMergeSorter(Distinct={Distinct}): intComparer → {IntComparer}", Distinct, intComparer);
        var (correctArray, testArray) = RandomHelper.GetArrays(n);

        // Act
        sorter.Sort(testArray, intComparer);
        Array.Sort(correctArray);

        // Assert
        Assert.That(testArray, Is.EqualTo(correctArray));
		_logger.LogWarning("<<< Exiting TestOnMergeSorter(Distinct={Distinct})", Distinct);
    }
}
