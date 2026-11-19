using Algorithms.Sequences;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sequences;

public class NumberOfBooleanFunctionsSequenceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void First5ElementsCorrect()
    {
		_logger.LogError(">>> Entering First5ElementsCorrect()");
        var sequence = new NumberOfBooleanFunctionsSequence().Sequence.Take(5);
		_logger.LogWarning("<<< Exiting First5ElementsCorrect()");
        sequence.SequenceEqual(new BigInteger[] { 2, 4, 16, 256, 65536 })
            .Should().BeTrue();
			_logger.LogError("First5ElementsCorrect(): sequence → {Sequence}", sequence);
    }
}
