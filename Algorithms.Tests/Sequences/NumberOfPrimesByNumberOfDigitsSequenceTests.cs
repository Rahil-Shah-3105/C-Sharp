using Algorithms.Sequences;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sequences;

public class NumberOfPrimesByNumberOfDigitsSequenceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void First5ElementsCorrect()
    {
		_logger.LogDebug(">>> Entering First5ElementsCorrect()");
        var sequence = new NumberOfPrimesByNumberOfDigitsSequence().Sequence.Take(5);
		_logger.LogDebug("<<< Exiting First5ElementsCorrect()");
        sequence.SequenceEqual(new BigInteger[] { 0, 4, 21, 143, 1061 })
            .Should().BeTrue();
			_logger.LogDebug("First5ElementsCorrect(): sequence → {Sequence}", sequence);
    }
}
