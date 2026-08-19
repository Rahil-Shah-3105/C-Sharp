using Algorithms.Sorters.External;
using Algorithms.Sorters.External.Storages;
using Algorithms.Tests.Helpers;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sorters.External;

public static class ExternalMergeSorterTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void ArraySorted(
        [Random(0, 1000, 100, Distinct = true)]
        int n)
    {
		_logger.LogDebug(">>> Entering ArraySorted(Distinct={Distinct})", Distinct);
        // Arrange
        var sorter = new ExternalMergeSorter<int>();
		_logger.LogDebug("ArraySorted(Distinct={Distinct}): sorter → {Sorter}", Distinct, sorter);
        var intComparer = new IntComparer();
		_logger.LogDebug("ArraySorted(Distinct={Distinct}): intComparer → {IntComparer}", Distinct, intComparer);
        var (correctArray, testArray) = RandomHelper.GetArrays(n);
        var main = new IntInMemoryStorage(testArray);
		_logger.LogDebug("ArraySorted(Distinct={Distinct}): main → {Main}", Distinct, main);
        var temp = new IntInMemoryStorage(new int[testArray.Length]);

        // Act
        sorter.Sort(main, temp, intComparer);
        Array.Sort(correctArray, intComparer);

        // Assert
        Assert.That(correctArray, Is.EqualTo(testArray));
		_logger.LogDebug("<<< Exiting ArraySorted(Distinct={Distinct})", Distinct);
    }

    [Test]
    public static void ArraySorted_OnDisk(
        [Random(0, 1000, 100, Distinct = true)]
        int n)
    {
		_logger.LogDebug(">>> Entering ArraySorted_OnDisk(Distinct={Distinct})", Distinct);
        // Arrange
        var sorter = new ExternalMergeSorter<int>();
		_logger.LogDebug("ArraySorted_OnDisk(Distinct={Distinct}): sorter → {Sorter}", Distinct, sorter);
        var intComparer = new IntComparer();
		_logger.LogDebug("ArraySorted_OnDisk(Distinct={Distinct}): intComparer → {IntComparer}", Distinct, intComparer);
        var (correctArray, testArray) = RandomHelper.GetArrays(n);
        var randomizer = Randomizer.CreateRandomizer();
		_logger.LogDebug("ArraySorted_OnDisk(Distinct={Distinct}): randomizer → {Randomizer}", Distinct, randomizer);
        var main = new IntFileStorage($"sorted_{randomizer.GetString(100)}", n);
		_logger.LogDebug("ArraySorted_OnDisk(Distinct={Distinct}): main → {Main}", Distinct, main);
        var temp = new IntFileStorage($"temp_{randomizer.GetString(100)}", n);

        var writer = main.GetWriter();
		_logger.LogDebug("ArraySorted_OnDisk(Distinct={Distinct}): writer → {Writer}", Distinct, writer);
        for (var i = 0; i < n; i++)
        {
            writer.Write(correctArray[i]);
        }

        writer.Dispose();

        // Act
        sorter.Sort(main, temp, intComparer);
        Array.Sort(correctArray, intComparer);

        // Assert
        var reader = main.GetReader();
		_logger.LogDebug("ArraySorted_OnDisk(Distinct={Distinct}): reader → {Reader}", Distinct, reader);
        for (var i = 0; i < n; i++)
        {
            testArray[i] = reader.Read();
        }

        Assert.That(correctArray, Is.EqualTo(testArray));
		_logger.LogDebug("<<< Exiting ArraySorted_OnDisk(Distinct={Distinct})", Distinct);
    }
}
