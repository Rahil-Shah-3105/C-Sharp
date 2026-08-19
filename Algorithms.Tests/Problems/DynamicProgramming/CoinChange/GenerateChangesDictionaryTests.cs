using Algorithms.Problems.DynamicProgramming.CoinChange;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Problems.DynamicProgramming.CoinChange;

[TestFixture]
public class GenerateChangesDictionaryTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void GenerateChangesDictionaryTest_Success()
    {
		_logger.LogDebug(">>> Entering GenerateChangesDictionaryTest_Success()");
        const int coin = 6;
        var coins = new[] { 1, 3, 4 };
        var changeDictionary = DynamicCoinChangeSolver.GenerateChangesDictionary(coin, coins);
		_logger.LogDebug("GenerateChangesDictionaryTest_Success(): changeDictionary → {ChangeDictionary}", changeDictionary);

        changeDictionary[1].SequenceEqual(new[] { 0 }).Should().BeTrue();
        changeDictionary[2].SequenceEqual(new[] { 1 }).Should().BeTrue();
        changeDictionary[3].SequenceEqual(new[] { 0, 2 }).Should().BeTrue();
        changeDictionary[4].SequenceEqual(new[] { 0, 1, 3 }).Should().BeTrue();
        changeDictionary[5].SequenceEqual(new[] { 1, 2, 4 }).Should().BeTrue();
        changeDictionary[6].SequenceEqual(new[] { 2, 3, 5 }).Should().BeTrue();
		_logger.LogDebug("<<< Exiting GenerateChangesDictionaryTest_Success()");
    }
}
