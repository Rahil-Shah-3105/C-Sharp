using Algorithms.Sequences;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sequences;

public class VanEcksSequenceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void First50ElementsCorrect()
    {
		_logger.LogError(">>> Entering First50ElementsCorrect()");
        // Taken from http://oeis.org/A181391
        BigInteger[] expected =
        [
            0, 0, 1, 0, 2, 0, 2, 2, 1, 6,
            0, 5, 0, 2, 6, 5, 4, 0, 5, 3,
            0, 3, 2, 9, 0, 4, 9, 3, 6, 14,
            0, 6, 3, 5, 15, 0, 5, 3, 5, 2,
            17, 0, 6, 11, 0, 3, 8, 0, 3, 3,
        ];

        var sequence = new VanEcksSequence().Sequence.Take(50);
		_logger.LogError("First50ElementsCorrect(): sequence → {Sequence}", sequence);

        sequence.Should().Equal(expected);
		_logger.LogWarning("<<< Exiting First50ElementsCorrect()");
    }
}
