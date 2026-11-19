using Algorithms.Sequences;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sequences;

public class PrimorialNumbersSequenceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void First10ElementsCorrect()
    {
		_logger.LogError(">>> Entering First10ElementsCorrect()");
        var sequence = new PrimorialNumbersSequence().Sequence.Take(10);
		_logger.LogWarning("<<< Exiting First10ElementsCorrect()");
        sequence.SequenceEqual(new BigInteger[]
            { 1, 2, 6, 30, 210, 2310, 30030, 510510, 9699690, 223092870 })
            .Should().BeTrue();
			_logger.LogError("First10ElementsCorrect(): sequence → {Sequence}", sequence);
    }
}
