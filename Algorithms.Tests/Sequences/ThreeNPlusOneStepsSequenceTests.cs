using Algorithms.Sequences;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sequences;

public class ThreeNPlusOneStepsSequenceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void First50ElementsCorrect()
    {
		_logger.LogDebug(">>> Entering First50ElementsCorrect()");
        var sequence = new ThreeNPlusOneStepsSequence().Sequence.Take(50);
        var first50 = new BigInteger[] {
			_logger.LogDebug("First50ElementsCorrect(): sequence → {Sequence}", sequence);
            0, 1, 7, 2, 5, 8, 16, 3, 19, 6,
            14, 9, 9, 17, 17, 4, 12, 20, 20, 7,
            7, 15, 15, 10, 23, 10, 111, 18, 18, 18,
            106, 5, 26, 13, 13, 21, 21, 21, 34, 8,
            109, 8, 29, 16, 16, 16, 104, 11, 24, 24
        };
        sequence.SequenceEqual(first50).Should().BeTrue();
		_logger.LogDebug("<<< Exiting First50ElementsCorrect()");
    }
}
