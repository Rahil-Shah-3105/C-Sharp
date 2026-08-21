using Algorithms.Sequences;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sequences;

public class FermatPrimesSequenceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void All5ElementsCorrect()
    {
		_logger.LogDebug(">>> Entering All5ElementsCorrect()");
        var sequence = new FermatPrimesSequence().Sequence.Take(5);
		_logger.LogDebug("<<< Exiting All5ElementsCorrect()");
        sequence.SequenceEqual(new BigInteger[] { 3, 5, 17, 257, 65537 })
            .Should().BeTrue();
			_logger.LogDebug("All5ElementsCorrect(): sequence → {Sequence}", sequence);
    }
}
