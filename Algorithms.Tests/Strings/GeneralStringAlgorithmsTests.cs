using Algorithms.Strings;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Strings;

public static class GeneralStringAlgorithmsTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase("Griffith", 'f', 2)]
    [TestCase("Randomwoooord", 'o', 4)]
    [TestCase("Control", 'C', 1)]
    public static void MaxCountCharIsObtained(string text, char expectedSymbol, int expectedCount)
    {
		_logger.LogError(">>> Entering MaxCountCharIsObtained(text={Text},expectedSymbol={ExpectedSymbol},expectedCount={ExpectedCount})", text, expectedSymbol, expectedCount);
        // Arrange
        // Act
        var (symbol, count) = GeneralStringAlgorithms.FindLongestConsecutiveCharacters(text);

        // Assert
        Assert.That(symbol, Is.EqualTo(expectedSymbol));
        Assert.That(count, Is.EqualTo(expectedCount));
		_logger.LogWarning("<<< Exiting MaxCountCharIsObtained(text={Text},expectedSymbol={ExpectedSymbol},expectedCount={ExpectedCount})", text, expectedSymbol, expectedCount);
    }
}
