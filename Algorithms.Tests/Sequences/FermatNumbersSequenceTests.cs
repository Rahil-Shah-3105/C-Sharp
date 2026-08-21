using Algorithms.Sequences;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sequences;

public class FermatNumbersSequenceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void First5ElementsCorrect()
    {
		_logger.LogDebug(">>> Entering First5ElementsCorrect()");
        var sequence = new FermatNumbersSequence().Sequence.Take(5);
		_logger.LogDebug("<<< Exiting First5ElementsCorrect()");
        sequence.SequenceEqual(new BigInteger[] { 3, 5, 17, 257, 65537 })
            .Should().BeTrue();
			_logger.LogDebug("First5ElementsCorrect(): sequence → {Sequence}", sequence);
    }
}
