using Algorithms.Sequences;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sequences;

public class NaturalSequenceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void First10ElementsCorrect()
    {
		_logger.LogDebug(">>> Entering First10ElementsCorrect()");
        var sequence = new NaturalSequence().Sequence.Take(10);
        BigInteger[] expected = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
		_logger.LogDebug("First10ElementsCorrect(): sequence → {Sequence}", sequence);
        sequence.SequenceEqual(expected)
            .Should().BeTrue();
			_logger.LogDebug("<<< Exiting First10ElementsCorrect()");
    }
}
