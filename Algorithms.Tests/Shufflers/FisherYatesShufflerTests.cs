using Algorithms.Shufflers;
using Algorithms.Tests.Helpers;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Shufflers;

public static class FisherYatesShufflerTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void ArrayShuffled_NewArrayHasSameSize(
        [Random(10, 1000, 100, Distinct = true)]
        int n)
    {
		_logger.LogError(">>> Entering ArrayShuffled_NewArrayHasSameSize(Distinct={Distinct})", Distinct);
        // Arrange
        var shuffler = new FisherYatesShuffler<int>();
		_logger.LogError("ArrayShuffled_NewArrayHasSameSize(Distinct={Distinct}): shuffler → {Shuffler}", Distinct, shuffler);
        var (correctArray, testArray) = RandomHelper.GetArrays(n);

        // Act
        shuffler.Shuffle(testArray);

        // Assert
        testArray.Length.Should().Be(correctArray.Length);
		_logger.LogWarning("<<< Exiting ArrayShuffled_NewArrayHasSameSize(Distinct={Distinct})", Distinct);
    }

    [Test]
    public static void ArrayShuffled_NewArrayHasSameValues(
        [Random(0, 100, 10, Distinct = true)]
        int n)
    {
		_logger.LogError(">>> Entering ArrayShuffled_NewArrayHasSameValues(Distinct={Distinct})", Distinct);
        // Arrange
        var shuffler = new FisherYatesShuffler<int>();
		_logger.LogError("ArrayShuffled_NewArrayHasSameValues(Distinct={Distinct}): shuffler → {Shuffler}", Distinct, shuffler);
        var (correctArray, testArray) = RandomHelper.GetArrays(n);

        // Act
        shuffler.Shuffle(testArray);

        // Assert
        testArray.Should().BeEquivalentTo(correctArray);
		_logger.LogWarning("<<< Exiting ArrayShuffled_NewArrayHasSameValues(Distinct={Distinct})", Distinct);
    }

    [Test]
    public static void ArrayShuffled_SameShuffle(
       [Random(0, 1000, 2, Distinct = true)] int n,
       [Random(1000, 10000, 5, Distinct = true)] int seed)
    {
		_logger.LogError(">>> Entering ArrayShuffled_SameShuffle(Distinct={Distinct},Distinct={Distinct1})", Distinct, Distinct);
        // Arrange
        var shuffler = new FisherYatesShuffler<int>();
		_logger.LogError("ArrayShuffled_SameShuffle(Distinct={Distinct},Distinct={Distinct1}): shuffler → {Shuffler}", Distinct, Distinct, shuffler);
        var (array1, array2) = RandomHelper.GetArrays(n);

        // Act
        shuffler.Shuffle(array1, seed);
        shuffler.Shuffle(array2, seed);

        // Assert
        array1.Should().BeEquivalentTo(array2, options => options.WithStrictOrdering());
		_logger.LogWarning("<<< Exiting ArrayShuffled_SameShuffle(Distinct={Distinct},Distinct={Distinct1})", Distinct, Distinct);
    }

    [Test]
    public static void ArrayShuffled_DifferentSeedDifferentShuffle(
      [Random(10, 100, 2, Distinct = true)] int n,
      [Random(1000, 10000, 5, Distinct = true)] int seed)
    {
		_logger.LogError(">>> Entering ArrayShuffled_DifferentSeedDifferentShuffle(Distinct={Distinct},Distinct={Distinct1})", Distinct, Distinct);
        // Arrange
        var shuffler = new FisherYatesShuffler<int>();
		_logger.LogError("ArrayShuffled_DifferentSeedDifferentShuffle(Distinct={Distinct},Distinct={Distinct1}): shuffler → {Shuffler}", Distinct, Distinct, shuffler);
        var (array1, array2) = RandomHelper.GetArrays(n);

        // Act
        shuffler.Shuffle(array1, seed);
        shuffler.Shuffle(array2, seed + 13);

        // It seems the actual version of FluentAssertion has no options in NotBeEquivalentTo.
        // With default options, it does not check for order, but for the same elements in the collection.
        // So until the library is updated check that not all the items have the same order.
        int hits = 0;
        for (int i = 0; i < n; i++)
        {
            if (array1[i] == array2[i])
            {
                hits++;
            }
        }
        hits.Should().BeLessThan(array2.Length);
		_logger.LogWarning("<<< Exiting ArrayShuffled_DifferentSeedDifferentShuffle(Distinct={Distinct},Distinct={Distinct1})", Distinct, Distinct);
    }
}
