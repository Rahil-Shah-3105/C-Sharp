using Algorithms.Sequences;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sequences;

public class KummerNumbersSequenceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void First10ElementsCorrect()
    {
		_logger.LogDebug(">>> Entering First10ElementsCorrect()");
        var sequence = new KummerNumbersSequence().Sequence.Take(10);
		_logger.LogDebug("<<< Exiting First10ElementsCorrect()");
        sequence.SequenceEqual(new BigInteger[]
            { 1, 5, 29, 209, 2309, 30029, 510509, 9699689, 223092869, 6469693229 })
            .Should().BeTrue();
			_logger.LogDebug("First10ElementsCorrect(): sequence → {Sequence}", sequence);
    }
}
