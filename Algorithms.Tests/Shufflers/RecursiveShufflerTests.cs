using Algorithms.Shufflers;
using Algorithms.Tests.Helpers;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Shufflers
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public static class RecursiveShufflerTests
    {
        [Test]
        public static void ArrayShuffled_NewArraySameSize(
            [Random(10, 1000, 100, Distinct = true)]
            int n)
        {
			_logger.LogDebug(">>> Entering ArrayShuffled_NewArraySameSize(Distinct={Distinct})", Distinct);
            // Arrange
            var shuffler = new RecursiveShuffler<int>();
			_logger.LogDebug("ArrayShuffled_NewArraySameSize(Distinct={Distinct}): shuffler → {Shuffler}", Distinct, shuffler);
            var (correctArray, testArray) = RandomHelper.GetArrays(n);

            // Act
            shuffler.Shuffle(testArray);

            // Assert
            testArray.Length.Should().Be(correctArray.Length);
			_logger.LogDebug("<<< Exiting ArrayShuffled_NewArraySameSize(Distinct={Distinct})", Distinct);
        }

        [Test]
        public static void ArrayShuffled_NewArraySameValues(
            [Random(10, 1000, 100, Distinct = true)]
            int n)
        {
			_logger.LogDebug(">>> Entering ArrayShuffled_NewArraySameValues(Distinct={Distinct})", Distinct);
            // Arrange
            var shuffler = new RecursiveShuffler<int>();
			_logger.LogDebug("ArrayShuffled_NewArraySameValues(Distinct={Distinct}): shuffler → {Shuffler}", Distinct, shuffler);
            var (correctArray, testArray) = RandomHelper.GetArrays(n);

            // Act
            shuffler.Shuffle(testArray);

            // Assert
            testArray.Should().BeEquivalentTo(correctArray);
			_logger.LogDebug("<<< Exiting ArrayShuffled_NewArraySameValues(Distinct={Distinct})", Distinct);
        }

        [Test]
        public static void ArrayShuffled_NewArraySameShuffle(
            [Random(0, 1000, 2, Distinct = true)] int n,
            [Random(1000, 10000, 5, Distinct = true)] int seed)
        {
			_logger.LogDebug(">>> Entering ArrayShuffled_NewArraySameShuffle(Distinct={Distinct},Distinct={Distinct1})", Distinct, Distinct);
            // Arrange
            var shuffler = new RecursiveShuffler<int>();
			_logger.LogDebug("ArrayShuffled_NewArraySameShuffle(Distinct={Distinct},Distinct={Distinct1}): shuffler → {Shuffler}", Distinct, Distinct, shuffler);
            var (correctArray, testArray) = RandomHelper.GetArrays(n);

            // Act
            shuffler.Shuffle(testArray, seed);
            shuffler.Shuffle(correctArray, seed);

            // Assert
            correctArray.Should().BeEquivalentTo(testArray, options => options.WithStrictOrdering());
			_logger.LogDebug("<<< Exiting ArrayShuffled_NewArraySameShuffle(Distinct={Distinct},Distinct={Distinct1})", Distinct, Distinct);
        }
    }
}
