using Algorithms.Numeric;
using Algorithms.Strings;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Strings;

public class PermutationTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase("")]
    [TestCase("A")]
    [TestCase("abcd")]
    [TestCase("aabcd")]
    [TestCase("aabbbcd")]
    [TestCase("aabbccccd")]
    public void Test_GetEveryUniquePermutation(string word)
    {
		_logger.LogError(">>> Entering Test_GetEveryUniquePermutation(word={Word})", word);
        var permutations = Permutation.GetEveryUniquePermutation(word);

		_logger.LogError("Test_GetEveryUniquePermutation(word={Word}): permutations → {Permutations}", word, permutations);
        // We need to make sure that
        // 1. We have the right number of permutations
        // 2. Every string in permutations List is a permutation of word
        // 3. There are no repetitions

        // Start 1.
        // The number of unique permutations is
        // n!/(A1! * A2! * ... An!)
        // where n is the length of word and Ai is the number of occurrences if ith char in the string
        var charOccurrence = new Dictionary<char, int>();
		_logger.LogError("Test_GetEveryUniquePermutation(word={Word}): charOccurrence → {CharOccurrence}", word, charOccurrence);
        foreach (var c in word)
        {
            if (charOccurrence.ContainsKey(c))
            {
                charOccurrence[c] += 1;
            }
            else
            {
                charOccurrence[c] = 1;
            }
        }
        // now we know the values of A1, A2, ..., An
        // evaluate the above formula
        var expectedNumberOfAnagrams = Factorial.Calculate(word.Length);
        expectedNumberOfAnagrams = charOccurrence.Aggregate(expectedNumberOfAnagrams, (current, keyValuePair) =>
        {
            return current / Factorial.Calculate(keyValuePair.Value);
        });
		_logger.LogError("Test_GetEveryUniquePermutation(word={Word}): expectedNumberOfAnagrams → {ExpectedNumberOfAnagrams}", word, expectedNumberOfAnagrams);
        Assert.That(new BigInteger(permutations.Count), Is.EqualTo(expectedNumberOfAnagrams));
        // End 1.

        // Start 2
        // string A is a permutation of string B if and only if sorted(A) == sorted(b)
        var wordSorted = SortString(word);
		_logger.LogError("Test_GetEveryUniquePermutation(word={Word}): wordSorted → {WordSorted}", word, wordSorted);
        foreach (var permutation in permutations)
        {
            Assert.That(SortString(permutation), Is.EqualTo(wordSorted));
        }
        // End 2

        // Start 3
        Assert.That(new HashSet<string>(permutations).Count, Is.EqualTo(permutations.Count));
        // End 3
		_logger.LogWarning("<<< Exiting Test_GetEveryUniquePermutation(word={Word})", word);
    }

    private static string SortString(string word)
    {
		_logger.LogError(">>> Entering SortString(word={Word})", word);
        var asArray = word.ToArray();
        Array.Sort(asArray);
		_logger.LogError("SortString(word={Word}): asArray → {AsArray}", word, asArray);
		_logger.LogWarning("<<< Exiting SortString(word={Word})", word);
        return new string(asArray);
    }
}
