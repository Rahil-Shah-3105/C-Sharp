using Algorithms.Problems.DynamicProgramming.CoinChange;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Problems.DynamicProgramming.CoinChange;

[TestFixture]
public class MakeCoinChangeDynamicTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void MakeCoinChangeDynamicTest_Success()
    {
		_logger.LogDebug(">>> Entering MakeCoinChangeDynamicTest_Success()");
        DynamicCoinChangeSolver
            .MakeCoinChangeDynamic(6, [1, 3, 4])
            .SequenceEqual(new[] { 3, 3 })
            .Should().BeTrue();

        DynamicCoinChangeSolver
            .MakeCoinChangeDynamic(8, [1, 3, 4])
            .SequenceEqual(new[] { 4, 4 })
            .Should().BeTrue();

        DynamicCoinChangeSolver
            .MakeCoinChangeDynamic(25, [1, 3, 4, 12, 13, 14])
            .SequenceEqual(new[] { 13, 12 })
            .Should().BeTrue();

        DynamicCoinChangeSolver
            .MakeCoinChangeDynamic(26, [1, 3, 4, 12, 13, 14])
            .SequenceEqual(new[] { 14, 12 })
            .Should().BeTrue();
			_logger.LogDebug("<<< Exiting MakeCoinChangeDynamicTest_Success()");
    }
}
