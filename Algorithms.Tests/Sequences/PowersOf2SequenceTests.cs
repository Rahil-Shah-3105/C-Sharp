using Algorithms.Sequences;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sequences;

public class PowersOf2SequenceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void First10ElementsCorrect()
    {
		_logger.LogDebug(">>> Entering First10ElementsCorrect()");
        var sequence = new PowersOf2Sequence().Sequence.Take(10);
        BigInteger[] expected = [1, 2, 4, 8, 16, 32, 64, 128, 256, 512];
		_logger.LogDebug("First10ElementsCorrect(): sequence → {Sequence}", sequence);
        sequence.SequenceEqual(expected)
            .Should().BeTrue();
			_logger.LogDebug("<<< Exiting First10ElementsCorrect()");
    }
}
