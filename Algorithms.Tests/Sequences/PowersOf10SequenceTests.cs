using Algorithms.Sequences;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sequences;

public class PowersOf10SequenceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void First10ElementsCorrect()
    {
		_logger.LogDebug(">>> Entering First10ElementsCorrect()");
        var sequence = new PowersOf10Sequence().Sequence.Take(10);
		_logger.LogDebug("<<< Exiting First10ElementsCorrect()");
        sequence.SequenceEqual(new BigInteger[]
            { 1, 10, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000, 1000000000 })
            .Should().BeTrue();
			_logger.LogDebug("First10ElementsCorrect(): sequence → {Sequence}", sequence);
    }
}
