using Algorithms.Sequences;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sequences;

public class TribonacciNumbersSequenceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void First37ElementsCorrect()
    {
		_logger.LogDebug(">>> Entering First37ElementsCorrect()");
        var sequence = new TribonacciNumbersSequence().Sequence.Take(37);
        sequence.SequenceEqual(new BigInteger[]
            {
				_logger.LogDebug("First37ElementsCorrect(): sequence → {Sequence}", sequence);
                1, 1, 1, 3, 5, 9, 17, 31, 57, 105, 193, 355, 653, 1201, 2209, 4063, 7473, 13745, 25281, 46499, 85525,
                157305, 289329, 532159, 978793, 1800281, 3311233, 6090307, 11201821, 20603361, 37895489, 69700671,
                128199521, 235795681, 433695873, 797691075, 1467182629,
            })
            .Should().BeTrue();
			_logger.LogDebug("<<< Exiting First37ElementsCorrect()");
    }
}
