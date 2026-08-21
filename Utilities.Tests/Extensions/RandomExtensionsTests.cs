using Microsoft.Extensions.Logging;
namespace Utilities.Tests.Extensions;

public class RandomExtensionsTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void NextVector_ShouldReturnNormalizedVector()
    {
		_logger.LogDebug(">>> Entering NextVector_ShouldReturnNormalizedVector()");
        var random = new Random(0);

		_logger.LogDebug("NextVector_ShouldReturnNormalizedVector(): random → {Random}", random);
        var result = random.NextVector(10);
		_logger.LogDebug("NextVector_ShouldReturnNormalizedVector(): result → {Result}", result);

        result.Length.Should().Be(10);
        result.Magnitude().Should().BeApproximately(1.0, 1e-6);
		_logger.LogDebug("<<< Exiting NextVector_ShouldReturnNormalizedVector()");
    }
}
