using Algorithms.Sequences;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sequences;

public class PrimesSequenceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void First10ElementsCorrect()
    {
		_logger.LogDebug(">>> Entering First10ElementsCorrect()");
        var sequence = new PrimesSequence().Sequence.Take(10);
        sequence.SequenceEqual(new BigInteger[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 })
            .Should().BeTrue();
			_logger.LogDebug("First10ElementsCorrect(): sequence → {Sequence}", sequence);
			_logger.LogDebug("<<< Exiting First10ElementsCorrect()");
    }
}
