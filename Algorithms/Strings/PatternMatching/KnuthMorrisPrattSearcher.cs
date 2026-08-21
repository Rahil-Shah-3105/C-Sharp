using Microsoft.Extensions.Logging;
namespace Algorithms.Strings.PatternMatching;

public class KnuthMorrisPrattSearcher
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     An implementation of Knuth–Morris–Pratt Algorithm.
    ///     Worst case time complexity: O(n + k)
    ///     where n - text length, k - pattern length.
    /// </summary>
    /// <param name="str">The string to look in.</param>
    /// <param name="pat">The pattern to look for.</param>
    /// <returns>
    ///     The zero-based positions of all occurrences of <paramref name="pat" /> in <paramref name="str" />.
    /// </returns>
    public IEnumerable<int> FindIndexes(string str, string pat)
    {
		_logger.LogDebug(">>> Entering FindIndexes(str={Str},pat={Pat})", str, pat);
        var lps = FindLongestPrefixSuffixValues(pat);

		_logger.LogDebug("FindIndexes(str={Str},pat={Pat}): lps → {Lps}", str, pat, lps);
        for (int i = 0, j = 0; i < str.Length;)
        {
            if (pat[j] == str[i])
            {
                j++;
                i++;
            }

            if (j == pat.Length)
            {
                yield return i - j;
                j = lps[j - 1];
                continue;
            }

            if (i < str.Length && pat[j] != str[i])
            {
                if (j != 0)
                {
                    j = lps[j - 1];
                }
                else
                {
                    i += 1;
                }
            }
        }
		_logger.LogDebug("<<< Exiting FindIndexes(str={Str},pat={Pat})", str, pat);
    }

    /// <summary>
    ///     Return the longest prefix suffix values for pattern.
    /// </summary>
    /// <param name="pat">pattern to seek.</param>
    /// <returns>The longest prefix suffix values for <paramref name="pat" />.</returns>
    public int[] FindLongestPrefixSuffixValues(string pat)
    {
		_logger.LogDebug(">>> Entering FindLongestPrefixSuffixValues(pat={Pat})", pat);
        var lps = new int[pat.Length];
        for (int i = 1, len = 0; i < pat.Length;)
        {
            if (pat[i] == pat[len])
            {
                len++;
                lps[i] = len;
                i++;
                continue;
            }

            if (len != 0)
            {
                len = lps[len - 1];
				_logger.LogDebug("FindLongestPrefixSuffixValues(pat={Pat}): len → {Len}", pat, len);
            }
            else
            {
                lps[i] = 0;
                i++;
            }
        }

		_logger.LogDebug("<<< Exiting FindLongestPrefixSuffixValues(pat={Pat})", pat);
        return lps;
    }
}
