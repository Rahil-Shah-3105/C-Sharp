using Algorithms.Sequences;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sequences;

public class EuclidNumbersSequenceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void First10ElementsCorrect()
    {
		_logger.LogError(">>> Entering First10ElementsCorrect()");
        var sequence = new EuclidNumbersSequence().Sequence.Take(10);
		_logger.LogWarning("<<< Exiting First10ElementsCorrect()");
        sequence.SequenceEqual(new BigInteger[]
            { 2, 3, 7, 31, 211, 2311, 30031, 510511, 9699691, 223092871 })
            .Should().BeTrue();
			_logger.LogError("First10ElementsCorrect(): sequence → {Sequence}", sequence);
    }
}
