using Algorithms.Sequences;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sequences;

public class GolombsSequenceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void First50ElementsCorrect()
    {
		_logger.LogDebug(">>> Entering First50ElementsCorrect()");
        // Taken from https://oeis.org/A001462
        var expected = new BigInteger[] {
            1, 2, 2, 3, 3, 4, 4, 4, 5, 5,
            5, 6, 6, 6, 6, 7, 7, 7, 7, 8,
            8, 8, 8, 9, 9, 9, 9, 9, 10, 10,
            10, 10, 10, 11, 11, 11, 11, 11, 12, 12,
            12, 12, 12, 12, 13, 13, 13, 13, 13, 13};

        var sequence = new GolombsSequence().Sequence.Take(50);
		_logger.LogDebug("First50ElementsCorrect(): sequence → {Sequence}", sequence);

        sequence.SequenceEqual(expected).Should().BeTrue();
		_logger.LogDebug("<<< Exiting First50ElementsCorrect()");
    }
}
