using Algorithms.Sequences;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sequences;

public class AllThreesSequenceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void First10ElementsCorrect()
    {
		_logger.LogDebug(">>> Entering First10ElementsCorrect()");
        var sequence = new AllThreesSequence().Sequence.Take(10);
        sequence.SequenceEqual(new BigInteger[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 })
            .Should().BeTrue();
			_logger.LogDebug("First10ElementsCorrect(): sequence → {Sequence}", sequence);
			_logger.LogDebug("<<< Exiting First10ElementsCorrect()");
    }
}
