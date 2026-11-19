using Algorithms.Sequences;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sequences;

public class TetranacciNumbersSequenceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void First35ElementsCorrect()
    {
		_logger.LogError(">>> Entering First35ElementsCorrect()");
        var sequence = new TetranacciNumbersSequence().Sequence.Take(35);
        BigInteger[] expected =
        [
            1, 1, 1, 1, 4, 7, 13, 25, 49, 94, 181, 349, 673, 1297, 2500, 4819, 9289, 17905, 34513, 66526, 128233,
            247177, 476449, 918385, 1770244, 3412255, 6577333, 12678217, 24438049, 47105854, 90799453, 175021573,
            337364929, 650291809, 1253477764,
        ];
		_logger.LogError("First35ElementsCorrect(): sequence → {Sequence}", sequence);
        sequence.SequenceEqual(expected)
            .Should().BeTrue();
			_logger.LogWarning("<<< Exiting First35ElementsCorrect()");
    }
}
