using Algorithms.Sequences;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sequences;

public class BinomialSequenceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void First4RowsCorrect()
    {
		_logger.LogError(">>> Entering First4RowsCorrect()");
        var sequence = new BinomialSequence().Sequence.Take(10);
		_logger.LogWarning("<<< Exiting First4RowsCorrect()");
        sequence.SequenceEqual(new BigInteger[] { 1, 1, 1, 1, 2, 1, 1, 3, 3, 1 })
            .Should().BeTrue();
			_logger.LogError("First4RowsCorrect(): sequence → {Sequence}", sequence);
    }
}
