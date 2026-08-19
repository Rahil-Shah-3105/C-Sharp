using Algorithms.Sequences;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sequences;

public class SquaresSequenceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void First10ElementsCorrect()
    {
		_logger.LogDebug(">>> Entering First10ElementsCorrect()");
        var sequence = new SquaresSequence().Sequence.Take(10);
        BigInteger[] expected = [0, 1, 4, 9, 16, 25, 36, 49, 64, 81];
		_logger.LogDebug("First10ElementsCorrect(): sequence → {Sequence}", sequence);
        sequence.SequenceEqual(expected)
            .Should().BeTrue();
			_logger.LogDebug("<<< Exiting First10ElementsCorrect()");
    }
}
