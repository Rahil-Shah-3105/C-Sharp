using Algorithms.Strings;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Strings;

public static class PalindromeTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase("Anna")]
    [TestCase("A Santa at Nasa")]
    public static void TextIsPalindrome_TrueExpected(string text)
    {
		_logger.LogDebug(">>> Entering TextIsPalindrome_TrueExpected(text={Text})", text);
        // Arrange
        // Act
        var isPalindrome = Palindrome.IsStringPalindrome(text);
		_logger.LogDebug("TextIsPalindrome_TrueExpected(text={Text}): isPalindrome → {IsPalindrome}", text, isPalindrome);

        // Assert
        Assert.That(isPalindrome, Is.True);
		_logger.LogDebug("<<< Exiting TextIsPalindrome_TrueExpected(text={Text})", text);
    }

    [TestCase("hallo")]
    [TestCase("Once upon a time")]
    public static void TextNotPalindrome_FalseExpected(string text)
    {
		_logger.LogDebug(">>> Entering TextNotPalindrome_FalseExpected(text={Text})", text);
        // Arrange
        // Act
        var isPalindrome = Palindrome.IsStringPalindrome(text);
		_logger.LogDebug("TextNotPalindrome_FalseExpected(text={Text}): isPalindrome → {IsPalindrome}", text, isPalindrome);

        // Assert
        Assert.That(isPalindrome, Is.False);
		_logger.LogDebug("<<< Exiting TextNotPalindrome_FalseExpected(text={Text})", text);
    }
}
