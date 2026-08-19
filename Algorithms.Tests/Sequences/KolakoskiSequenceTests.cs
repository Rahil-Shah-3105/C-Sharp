using Algorithms.Sequences;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sequences;

public class KolakoskiSequenceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void First100ElementsCorrect()
    {
		_logger.LogDebug(">>> Entering First100ElementsCorrect()");
        // Taken from https://oeis.org/A000002
        BigInteger[] expected =
        [
            1, 2, 2, 1, 1, 2, 1, 2, 2, 1,
            2, 2, 1, 1, 2, 1, 1, 2, 2, 1,
            2, 1, 1, 2, 1, 2, 2, 1, 1, 2,
            1, 1, 2, 1, 2, 2, 1, 2, 2, 1,
            1, 2, 1, 2, 2, 1, 2, 1, 1, 2,
            1, 1, 2, 2, 1, 2, 2, 1, 1, 2,
            1, 2, 2, 1, 2, 2, 1, 1, 2, 1,
            1, 2, 1, 2, 2, 1, 2, 1, 1, 2,
            2, 1, 2, 2, 1, 1, 2, 1, 2, 2,
            1, 2, 2, 1, 1, 2, 1, 1, 2, 2,
        ];

        var sequence = new KolakoskiSequence().Sequence.Take(100);
		_logger.LogDebug("First100ElementsCorrect(): sequence → {Sequence}", sequence);
        var sequence2 = new KolakoskiSequence2().Sequence.Take(100);
		_logger.LogDebug("First100ElementsCorrect(): sequence2 → {Sequence2}", sequence2);

        sequence.Should().Equal(expected);
        sequence2.Should().Equal(expected);
		_logger.LogDebug("<<< Exiting First100ElementsCorrect()");
    }
}
