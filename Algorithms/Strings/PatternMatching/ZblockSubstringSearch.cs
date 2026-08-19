using Microsoft.Extensions.Logging;
namespace Algorithms.Strings.PatternMatching;

/// <summary>Implementation Z-block substring search.
/// </summary>
public static class ZblockSubstringSearch
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     This algorithm finds all occurrences of a pattern in a text in linear time - O(m+n).
    /// </summary>
    public static int FindSubstring(string pattern, string text)
    {
		_logger.LogDebug(">>> Entering FindSubstring(pattern={Pattern},text={Text})", pattern, text);
        var concatStr = $"{pattern}${text}";
        var patternLength = pattern.Length;
		_logger.LogDebug("FindSubstring(pattern={Pattern},text={Text}): concatStr → {ConcatStr}", pattern, text, concatStr);
        var n = concatStr.Length;
        var zArray = new int[n];

        var left = 0;
		_logger.LogDebug("FindSubstring(pattern={Pattern},text={Text}): left → {Left}", pattern, text, left);
        var right = 0;
		_logger.LogDebug("FindSubstring(pattern={Pattern},text={Text}): right → {Right}", pattern, text, right);

        for(var i = 1; i < n; i++)
        {
            if(i > right)
            {
                left = i;
                right = ComputeNewRightValue(concatStr, n, left, i);

                zArray[i] = right - left;
                right--;
            }
            else
            {
                var k = i - left;
                if (zArray[k] < (right - i + 1))
                {
                    zArray[i] = zArray[k];
                }
                else
                {
                    left = i;
                    right = ComputeNewRightValue(concatStr, n, left, right);
                    zArray[i] = right - left;
                    right--;
                }
            }
        }

        var found = 0;
        foreach(var z_value in zArray)
        {
            if(z_value == patternLength)
            {
                found++;
            }
        }

		_logger.LogDebug("<<< Exiting FindSubstring(pattern={Pattern},text={Text})", pattern, text);
        return found;
    }

    private static int ComputeNewRightValue(string concatStr, int n, int left, int right)
    {
		_logger.LogDebug(">>> Entering ComputeNewRightValue(concatStr={ConcatStr},n={N},left={Left},right={Right})", concatStr, n, left, right);
        while (right < n && concatStr[right - left].Equals(concatStr[right]))
        {
            right++;
        }

		_logger.LogDebug("<<< Exiting ComputeNewRightValue(concatStr={ConcatStr},n={N},left={Left},right={Right})", concatStr, n, left, right);
        return right;
    }
}
