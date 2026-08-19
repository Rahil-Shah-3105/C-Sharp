using Algorithms.Strings;
using NUnit.Framework;
using System;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Strings;

/// <summary>
///     Comprehensive test suite for Manacher's Algorithm implementation.
///     Tests cover various scenarios including:
private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
///     - Odd-length palindromes
///     - Even-length palindromes
///     - Single character strings
///     - Empty strings
///     - Strings with no palindromes longer than 1 character
///     - Strings that are entirely palindromic
///     - Multiple palindromes of the same length
///     - Edge cases (null input, special characters)
///     - Palindrome detection functionality
///     - Detailed palindrome information retrieval.
/// </summary>
public static class ManachersAlgorithmTests
{
    [Test]
    public static void FindLongestPalindrome_WithOddLengthPalindrome_ReturnsCorrectPalindrome()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindrome_WithOddLengthPalindrome_ReturnsCorrectPalindrome()");
        // Arrange: Classic example with odd-length palindrome "bab" or "aba"
        string input = "babad";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Either "bab" or "aba" is valid (both have length 3)
        Assert.That(result.Length, Is.EqualTo(3));
        Assert.That(result == "bab" || result == "aba", Is.True);
		_logger.LogDebug("<<< Exiting FindLongestPalindrome_WithOddLengthPalindrome_ReturnsCorrectPalindrome()");
    }

    [Test]
    public static void FindLongestPalindrome_WithEvenLengthPalindrome_ReturnsCorrectPalindrome()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindrome_WithEvenLengthPalindrome_ReturnsCorrectPalindrome()");
        // Arrange: String with even-length palindrome "abba"
        string input = "cbbd";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should find "bb" (length 2)
        Assert.That(result, Is.EqualTo("bb"));
		_logger.LogDebug("<<< Exiting FindLongestPalindrome_WithEvenLengthPalindrome_ReturnsCorrectPalindrome()");
    }

    [Test]
    public static void FindLongestPalindrome_WithSingleCharacter_ReturnsSingleCharacter()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindrome_WithSingleCharacter_ReturnsSingleCharacter()");
        // Arrange: Edge case with single character
        string input = "a";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Single character is a palindrome of itself
        Assert.That(result, Is.EqualTo("a"));
		_logger.LogDebug("<<< Exiting FindLongestPalindrome_WithSingleCharacter_ReturnsSingleCharacter()");
    }

    [Test]
    public static void FindLongestPalindrome_WithEmptyString_ReturnsEmptyString()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindrome_WithEmptyString_ReturnsEmptyString()");
        // Arrange: Edge case with empty string
        string input = string.Empty;

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Empty string should return empty string
        Assert.That(result, Is.EqualTo(string.Empty));
		_logger.LogDebug("<<< Exiting FindLongestPalindrome_WithEmptyString_ReturnsEmptyString()");
    }

    [Test]
    public static void FindLongestPalindrome_WithNullString_ThrowsArgumentException()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindrome_WithNullString_ThrowsArgumentException()");
        // Arrange: Test defensive programming - null input validation
        string? input = null;

        // Act & Assert: Should throw ArgumentException for null input
        Assert.Throws<ArgumentException>(() => ManachersAlgorithm.FindLongestPalindrome(input!));
		_logger.LogDebug("<<< Exiting FindLongestPalindrome_WithNullString_ThrowsArgumentException()");
    }

    [Test]
    public static void FindLongestPalindrome_WithEntirePalindrome_ReturnsEntireString()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindrome_WithEntirePalindrome_ReturnsEntireString()");
        // Arrange: String that is entirely a palindrome
        string input = "racecar";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should return the entire string
        Assert.That(result, Is.EqualTo("racecar"));
		_logger.LogDebug("<<< Exiting FindLongestPalindrome_WithEntirePalindrome_ReturnsEntireString()");
    }

    [Test]
    public static void FindLongestPalindrome_WithNoPalindromes_ReturnsSingleCharacter()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindrome_WithNoPalindromes_ReturnsSingleCharacter()");
        // Arrange: String with no palindromes longer than 1 character
        string input = "abcdef";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should return a single character (any character is a palindrome)
        Assert.That(result.Length, Is.EqualTo(1));
		_logger.LogDebug("<<< Exiting FindLongestPalindrome_WithNoPalindromes_ReturnsSingleCharacter()");
    }

    [Test]
    public static void FindLongestPalindrome_WithLongPalindrome_ReturnsCorrectPalindrome()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindrome_WithLongPalindrome_ReturnsCorrectPalindrome()");
        // Arrange: String with a longer palindrome
        string input = "forgeeksskeegfor";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should find "geeksskeeg" (length 10)
        Assert.That(result, Is.EqualTo("geeksskeeg"));
		_logger.LogDebug("<<< Exiting FindLongestPalindrome_WithLongPalindrome_ReturnsCorrectPalindrome()");
    }

    [Test]
    public static void FindLongestPalindrome_WithRepeatingCharacters_ReturnsCorrectPalindrome()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindrome_WithRepeatingCharacters_ReturnsCorrectPalindrome()");
        // Arrange: String with repeating characters
        string input = "aaaa";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Entire string is a palindrome
        Assert.That(result, Is.EqualTo("aaaa"));
		_logger.LogDebug("<<< Exiting FindLongestPalindrome_WithRepeatingCharacters_ReturnsCorrectPalindrome()");
    }

    [Test]
    public static void FindLongestPalindrome_WithSpecialCharacters_ReturnsCorrectPalindrome()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindrome_WithSpecialCharacters_ReturnsCorrectPalindrome()");
        // Arrange: String with special characters and spaces
        string input = "A man, a plan, a canal: Panama";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should find a palindrome (note: this includes spaces and punctuation)
        // The longest palindrome considering all characters is "anama"
        Assert.That(result.Length, Is.GreaterThan(0));
		_logger.LogDebug("<<< Exiting FindLongestPalindrome_WithSpecialCharacters_ReturnsCorrectPalindrome()");
    }

    [Test]
    public static void FindLongestPalindrome_WithTwoCharacters_ReturnsCorrectResult()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindrome_WithTwoCharacters_ReturnsCorrectResult()");
        // Arrange: Edge case with two identical characters
        string input = "aa";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should return "aa"
        Assert.That(result, Is.EqualTo("aa"));
		_logger.LogDebug("<<< Exiting FindLongestPalindrome_WithTwoCharacters_ReturnsCorrectResult()");
    }

    [Test]
    public static void FindLongestPalindrome_WithTwoDifferentCharacters_ReturnsSingleCharacter()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindrome_WithTwoDifferentCharacters_ReturnsSingleCharacter()");
        // Arrange: Edge case with two different characters
        string input = "ab";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should return a single character
        Assert.That(result.Length, Is.EqualTo(1));
		_logger.LogDebug("<<< Exiting FindLongestPalindrome_WithTwoDifferentCharacters_ReturnsSingleCharacter()");
    }

    [Test]
    public static void FindLongestPalindromeWithDetails_ReturnsCorrectDetails()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindromeWithDetails_ReturnsCorrectDetails()");
        // Arrange: Test the detailed version that returns index and length
        string input = "babad";

        // Act
        var (palindrome, startIndex, length) = ManachersAlgorithm.FindLongestPalindromeWithDetails(input);

        // Assert: Verify all components
        Assert.That(length, Is.EqualTo(3));
        Assert.That(palindrome.Length, Is.EqualTo(length));
        Assert.That(input.Substring(startIndex, length), Is.EqualTo(palindrome));
        Assert.That(palindrome == "bab" || palindrome == "aba", Is.True);
		_logger.LogDebug("<<< Exiting FindLongestPalindromeWithDetails_ReturnsCorrectDetails()");
    }

    [Test]
    public static void FindLongestPalindromeWithDetails_WithEmptyString_ReturnsZeroLength()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindromeWithDetails_WithEmptyString_ReturnsZeroLength()");
        // Arrange
        string input = string.Empty;

        // Act
        var (palindrome, startIndex, length) = ManachersAlgorithm.FindLongestPalindromeWithDetails(input);

        // Assert
        Assert.That(palindrome, Is.EqualTo(string.Empty));
        Assert.That(startIndex, Is.EqualTo(0));
        Assert.That(length, Is.EqualTo(0));
		_logger.LogDebug("<<< Exiting FindLongestPalindromeWithDetails_WithEmptyString_ReturnsZeroLength()");
    }

    [Test]
    public static void FindLongestPalindromeWithDetails_WithSingleCharacter_ReturnsCorrectDetails()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindromeWithDetails_WithSingleCharacter_ReturnsCorrectDetails()");
        // Arrange
        string input = "x";

        // Act
        var (palindrome, startIndex, length) = ManachersAlgorithm.FindLongestPalindromeWithDetails(input);

        // Assert
        Assert.That(palindrome, Is.EqualTo("x"));
        Assert.That(startIndex, Is.EqualTo(0));
        Assert.That(length, Is.EqualTo(1));
		_logger.LogDebug("<<< Exiting FindLongestPalindromeWithDetails_WithSingleCharacter_ReturnsCorrectDetails()");
    }

    [Test]
    public static void FindLongestPalindromeWithDetails_WithNullString_ThrowsArgumentException()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindromeWithDetails_WithNullString_ThrowsArgumentException()");
        // Arrange
        string? input = null;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => ManachersAlgorithm.FindLongestPalindromeWithDetails(input!));
		_logger.LogDebug("<<< Exiting FindLongestPalindromeWithDetails_WithNullString_ThrowsArgumentException()");
    }

    [Test]
    public static void IsPalindrome_WithPalindromeString_ReturnsTrue()
    {
		_logger.LogDebug(">>> Entering IsPalindrome_WithPalindromeString_ReturnsTrue()");
        // Arrange: Test palindrome detection with a valid palindrome
        string input = "racecar";

        // Act
        bool result = ManachersAlgorithm.IsPalindrome(input);

        // Assert: Should return true
        Assert.That(result, Is.True);
		_logger.LogDebug("<<< Exiting IsPalindrome_WithPalindromeString_ReturnsTrue()");
    }

    [Test]
    public static void IsPalindrome_WithNonPalindromeString_ReturnsFalse()
    {
		_logger.LogDebug(">>> Entering IsPalindrome_WithNonPalindromeString_ReturnsFalse()");
        // Arrange: Test palindrome detection with a non-palindrome
        string input = "hello";

        // Act
        bool result = ManachersAlgorithm.IsPalindrome(input);

        // Assert: Should return false
        Assert.That(result, Is.False);
		_logger.LogDebug("<<< Exiting IsPalindrome_WithNonPalindromeString_ReturnsFalse()");
    }

    [Test]
    public static void IsPalindrome_WithSingleCharacter_ReturnsTrue()
    {
		_logger.LogDebug(">>> Entering IsPalindrome_WithSingleCharacter_ReturnsTrue()");
        // Arrange: Single character is always a palindrome
        string input = "a";

        // Act
        bool result = ManachersAlgorithm.IsPalindrome(input);

        // Assert: Should return true
        Assert.That(result, Is.True);
		_logger.LogDebug("<<< Exiting IsPalindrome_WithSingleCharacter_ReturnsTrue()");
    }

    [Test]
    public static void IsPalindrome_WithEmptyString_ReturnsTrue()
    {
		_logger.LogDebug(">>> Entering IsPalindrome_WithEmptyString_ReturnsTrue()");
        // Arrange: Empty string is considered a palindrome
        string input = string.Empty;

        // Act
        bool result = ManachersAlgorithm.IsPalindrome(input);

        // Assert: Should return true
        Assert.That(result, Is.True);
		_logger.LogDebug("<<< Exiting IsPalindrome_WithEmptyString_ReturnsTrue()");
    }

    [Test]
    public static void IsPalindrome_WithNullString_ThrowsArgumentException()
    {
		_logger.LogDebug(">>> Entering IsPalindrome_WithNullString_ThrowsArgumentException()");
        // Arrange
        string? input = null;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => ManachersAlgorithm.IsPalindrome(input!));
		_logger.LogDebug("<<< Exiting IsPalindrome_WithNullString_ThrowsArgumentException()");
    }

    [Test]
    public static void IsPalindrome_WithEvenLengthPalindrome_ReturnsTrue()
    {
		_logger.LogDebug(">>> Entering IsPalindrome_WithEvenLengthPalindrome_ReturnsTrue()");
        // Arrange: Even-length palindrome
        string input = "abba";

        // Act
        bool result = ManachersAlgorithm.IsPalindrome(input);

        // Assert: Should return true
        Assert.That(result, Is.True);
		_logger.LogDebug("<<< Exiting IsPalindrome_WithEvenLengthPalindrome_ReturnsTrue()");
    }

    [Test]
    public static void FindLongestPalindrome_WithNumericString_ReturnsCorrectPalindrome()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindrome_WithNumericString_ReturnsCorrectPalindrome()");
        // Arrange: String with numbers
        string input = "12321";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should return the entire string
        Assert.That(result, Is.EqualTo("12321"));
		_logger.LogDebug("<<< Exiting FindLongestPalindrome_WithNumericString_ReturnsCorrectPalindrome()");
    }

    [Test]
    public static void FindLongestPalindrome_WithMixedCase_ReturnsCorrectPalindrome()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindrome_WithMixedCase_ReturnsCorrectPalindrome()");
        // Arrange: Mixed case string (case-sensitive palindrome check)
        string input = "AbcbA";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should find "AbcbA" as it's case-sensitive
        Assert.That(result, Is.EqualTo("AbcbA"));
		_logger.LogDebug("<<< Exiting FindLongestPalindrome_WithMixedCase_ReturnsCorrectPalindrome()");
    }

    [Test]
    public static void FindLongestPalindrome_WithPalindromeAtStart_ReturnsCorrectPalindrome()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindrome_WithPalindromeAtStart_ReturnsCorrectPalindrome()");
        // Arrange: Palindrome at the start of the string
        string input = "abaxyz";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should find "aba"
        Assert.That(result, Is.EqualTo("aba"));
		_logger.LogDebug("<<< Exiting FindLongestPalindrome_WithPalindromeAtStart_ReturnsCorrectPalindrome()");
    }

    [Test]
    public static void FindLongestPalindrome_WithPalindromeAtEnd_ReturnsCorrectPalindrome()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindrome_WithPalindromeAtEnd_ReturnsCorrectPalindrome()");
        // Arrange: Palindrome at the end of the string
        string input = "xyzaba";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should find "aba"
        Assert.That(result, Is.EqualTo("aba"));
		_logger.LogDebug("<<< Exiting FindLongestPalindrome_WithPalindromeAtEnd_ReturnsCorrectPalindrome()");
    }

    [Test]
    public static void FindLongestPalindrome_WithMultiplePalindromesOfSameLength_ReturnsOne()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindrome_WithMultiplePalindromesOfSameLength_ReturnsOne()");
        // Arrange: Multiple palindromes of the same length
        // "aba" at index 0 and "cdc" at index 3
        string input = "abacdc";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should return one of them (the first one found)
        Assert.That(result.Length, Is.EqualTo(3));
        Assert.That(result == "aba" || result == "cdc", Is.True);
		_logger.LogDebug("<<< Exiting FindLongestPalindrome_WithMultiplePalindromesOfSameLength_ReturnsOne()");
    }

    [Test]
    public static void FindLongestPalindromeWithDetails_WithLongString_PerformsEfficiently()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindromeWithDetails_WithLongString_PerformsEfficiently()");
        // Arrange: Test with a longer string to verify O(n) performance
        // Create a string with a palindrome in the middle
        string input = new string('a', 1000) + "racecar" + new string('b', 1000);

        // Act
        var (_, _, length) = ManachersAlgorithm.FindLongestPalindromeWithDetails(input);

        // Assert: Should find the longest palindrome (either the 'a's or 'b's or "racecar")
        // The 1000 'a's form a palindrome
        Assert.That(length, Is.GreaterThanOrEqualTo(7)); // At least "racecar"
		_logger.LogDebug("<<< Exiting FindLongestPalindromeWithDetails_WithLongString_PerformsEfficiently()");
    }

    [Test]
    public static void FindLongestPalindrome_WithVeryLongPalindrome_HandlesGracefully()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindrome_WithVeryLongPalindrome_HandlesGracefully()");
        // Arrange: Test with a very long palindrome to verify O(n) performance
        string input = new string('a', 10000);

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should correctly identify the entire string as a palindrome
        Assert.That(result.Length, Is.EqualTo(10000));
        Assert.That(result, Is.EqualTo(input));
		_logger.LogDebug("<<< Exiting FindLongestPalindrome_WithVeryLongPalindrome_HandlesGracefully()");
    }

    [Test]
    public static void IsPalindrome_WithVeryLongPalindrome_HandlesGracefully()
    {
		_logger.LogDebug(">>> Entering IsPalindrome_WithVeryLongPalindrome_HandlesGracefully()");
        // Arrange: Test with a very long palindrome to verify performance
        string input = new string('x', 5000);

        // Act
        bool result = ManachersAlgorithm.IsPalindrome(input);

        // Assert: Should correctly identify as palindrome in O(n) time
        Assert.That(result, Is.True);
		_logger.LogDebug("<<< Exiting IsPalindrome_WithVeryLongPalindrome_HandlesGracefully()");
    }

    [Test]
    public static void FindLongestPalindrome_WithAlternatingCharacters_HandlesEdgeCases()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindrome_WithAlternatingCharacters_HandlesEdgeCases()");
        // Arrange: Test with alternating characters (minimal palindromes)
        string input = "abababababababababababababababab";

        // Act
        string result = ManachersAlgorithm.FindLongestPalindrome(input);

        // Assert: Should find at least a 3-character palindrome like "aba" or "bab"
        Assert.That(result.Length, Is.GreaterThanOrEqualTo(3));
		_logger.LogDebug("<<< Exiting FindLongestPalindrome_WithAlternatingCharacters_HandlesEdgeCases()");
    }

    [Test]
    public static void FindLongestPalindromeWithDetails_WithSpecialCharacters_HandlesCorrectly()
    {
		_logger.LogDebug(">>> Entering FindLongestPalindromeWithDetails_WithSpecialCharacters_HandlesCorrectly()");
        // Arrange: Test with special characters to ensure proper handling
        string input = "abc!@#@!xyz";

        // Act
        var (palindrome, startIndex, length) = ManachersAlgorithm.FindLongestPalindromeWithDetails(input);

        // Assert: Should find the palindrome with special characters
        Assert.That(palindrome, Is.EqualTo("!@#@!"));
        Assert.That(length, Is.EqualTo(5));
        Assert.That(startIndex, Is.EqualTo(3));
		_logger.LogDebug("<<< Exiting FindLongestPalindromeWithDetails_WithSpecialCharacters_HandlesCorrectly()");
    }
}
