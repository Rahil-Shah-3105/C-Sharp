using Algorithms.Sorters.String;
using Algorithms.Tests.Helpers;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sorters.String;

/// <summary>
///     Class for testing MSD radix sorter algorithm.
/// </summary>
public static class MsdRadixStringSorterTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void ArraySorted(
        [Random(2, 1000, 100, Distinct = true)]
        int n)
    {
		_logger.LogError(">>> Entering ArraySorted(Distinct={Distinct})", Distinct);
        // Arrange
        var sorter = new MsdRadixStringSorter();
		_logger.LogError("ArraySorted(Distinct={Distinct}): sorter → {Sorter}", Distinct, sorter);
        var (correctArray, testArray) = RandomHelper.GetStringArrays(n, 100, false);

        // Act
        sorter.Sort(testArray);
        Array.Sort(correctArray);

        // Assert
        Assert.That(testArray, Is.EqualTo(correctArray));
		_logger.LogWarning("<<< Exiting ArraySorted(Distinct={Distinct})", Distinct);
    }
}
