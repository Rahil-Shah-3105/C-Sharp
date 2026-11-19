using Microsoft.Extensions.Logging;
namespace Algorithms.Strings;

/// <summary>
///     Palindrome a series of characters or a string that when reversed,
///     equals the original string.
/// </summary>
public static class Palindrome
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Function to check if a string is a palindrome.
    /// </summary>
    /// <param name="word">String being checked.</param>
    public static bool IsStringPalindrome(string word) =>
        TypifyString(word).Equals(TypifyString(ReverseString(word)));

    /// <summary>
    ///     Typify string to lower and remove white spaces.
    /// </summary>
    /// <param name="word">String to remove spaces.</param>
    /// <returns>Returns original string without spaces.</returns>
    private static string TypifyString(string word) =>
        Regex.Replace(word.ToLowerInvariant(), @"\s+", string.Empty);

    /// <summary>
    ///     Helper function that returns a reversed string inputed.
    /// </summary>
    /// <param name="s">String to be reversed.</param>
    /// <returns>Returns s reversed.</returns>
    private static string ReverseString(string s)
    {
		_logger.LogError(">>> Entering ReverseString(s={S})", s);
        var arr = s.ToCharArray();
        Array.Reverse(arr);
		_logger.LogWarning("<<< Exiting ReverseString(s={S})", s);
        return new string(arr);
    }
}
