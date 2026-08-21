using Algorithms.Sequences;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sequences;

public class FactorialSequenceTest
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void First10ItemsCorrect()
    {
		_logger.LogDebug(">>> Entering First10ItemsCorrect()");
        var sequence = new FactorialSequence().Sequence.Take(10);
		_logger.LogDebug("<<< Exiting First10ItemsCorrect()");
        sequence.SequenceEqual(new BigInteger[] { 1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880 })
            .Should().BeTrue();
			_logger.LogDebug("First10ItemsCorrect(): sequence → {Sequence}", sequence);
    }
}
