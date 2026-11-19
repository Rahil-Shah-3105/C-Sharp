using Algorithms.Problems.DynamicProgramming.CoinChange;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Problems.DynamicProgramming.CoinChange;

public class GetMinimalNextCoinTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void GetMinimalNextCoinTest_Success()
    {
		_logger.LogError(">>> Entering GetMinimalNextCoinTest_Success()");
        const int coin = 6;
        var coins = new[] { 1, 3, 4 };
        var exchangeDict = DynamicCoinChangeSolver.GenerateChangesDictionary(coin, coins);
		_logger.LogError("GetMinimalNextCoinTest_Success(): exchangeDict → {ExchangeDict}", exchangeDict);
        var nextCoin = DynamicCoinChangeSolver.GetMinimalNextCoin(6, exchangeDict);
		_logger.LogError("GetMinimalNextCoinTest_Success(): nextCoin → {NextCoin}", nextCoin);

        nextCoin.Should().Be(3);
		_logger.LogWarning("<<< Exiting GetMinimalNextCoinTest_Success()");
    }
}
