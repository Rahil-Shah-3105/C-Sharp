using Algorithms.Sequences;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sequences;

public class PrimePiSequenceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void First10ElementsCorrect()
    {
		_logger.LogError(">>> Entering First10ElementsCorrect()");
        var sequence = new PrimePiSequence().Sequence.Take(10);
		_logger.LogWarning("<<< Exiting First10ElementsCorrect()");
        sequence.SequenceEqual(new BigInteger[] { 0, 1, 2, 2, 3, 3, 4, 4, 4, 4 })
            .Should().BeTrue();
			_logger.LogError("First10ElementsCorrect(): sequence → {Sequence}", sequence);
    }
}
