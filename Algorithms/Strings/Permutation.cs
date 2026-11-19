using Microsoft.Extensions.Logging;
namespace Algorithms.Strings;

public static class Permutation
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// Returns every anagram of a given word.
    /// </summary>
    /// <returns>List of anagrams.</returns>
    public static List<string> GetEveryUniquePermutation(string word)
    {
		_logger.LogError(">>> Entering GetEveryUniquePermutation(word={Word})", word);
        if (word.Length < 2)
        {
            return
            [
                word,
            ];
        }

        var result = new HashSet<string>();
		_logger.LogError("GetEveryUniquePermutation(word={Word}): result → {Result}", word, result);

        for (var i = 0; i < word.Length; i++)
        {
            var temp = GetEveryUniquePermutation(word.Remove(i, 1));

            result.UnionWith(temp.Select(subPerm => word[i] + subPerm));
        }

		_logger.LogWarning("<<< Exiting GetEveryUniquePermutation(word={Word})", word);
        return result.ToList();
    }
}
