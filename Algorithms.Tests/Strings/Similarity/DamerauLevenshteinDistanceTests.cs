using Microsoft.Extensions.Logging;
﻿using Algorithms.Strings.Similarity;

namespace Algorithms.Tests.Strings.Similarity;

[TestFixture]
public class DamerauLevenshteinDistanceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void Calculate_IdenticalStrings_ReturnsZero()
    {
		_logger.LogDebug(">>> Entering Calculate_IdenticalStrings_ReturnsZero()");
        var str1 = "test";
		_logger.LogDebug("<<< Exiting Calculate_IdenticalStrings_ReturnsZero()");
        var str2 = "test";
		_logger.LogDebug("Calculate_IdenticalStrings_ReturnsZero(): str2 → {Str2}", str2);
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
		_logger.LogDebug("Calculate_IdenticalStrings_ReturnsZero(): result → {Result}", result);
		_logger.LogDebug("Calculate_IdenticalStrings_ReturnsZero(): str1 → {Str1}", str1);
        Assert.That(result, Is.EqualTo(0), "Identical strings should have a Damerau-Levenshtein distance of 0.");
    }

    [Test]
    public void Calculate_CompletelyDifferentStrings_ReturnsLengthOfLongestString()
    {
		_logger.LogDebug(">>> Entering Calculate_CompletelyDifferentStrings_ReturnsLengthOfLongestString()");
        var str1 = "abc";
		_logger.LogDebug("<<< Exiting Calculate_CompletelyDifferentStrings_ReturnsLengthOfLongestString()");
        var str2 = "xyz";
		_logger.LogDebug("Calculate_CompletelyDifferentStrings_ReturnsLengthOfLongestString(): str2 → {Str2}", str2);
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
		_logger.LogDebug("Calculate_CompletelyDifferentStrings_ReturnsLengthOfLongestString(): result → {Result}", result);
		_logger.LogDebug("Calculate_CompletelyDifferentStrings_ReturnsLengthOfLongestString(): str1 → {Str1}", str1);
        Assert.That(result, Is.EqualTo(3), "Completely different strings should have a Damerau-Levenshtein distance equal to the length of the longest string.");
    }

    [Test]
    public void Calculate_OneEmptyString_ReturnsLengthOfOtherString()
    {
		_logger.LogDebug(">>> Entering Calculate_OneEmptyString_ReturnsLengthOfOtherString()");
        var str1 = "test";
		_logger.LogDebug("<<< Exiting Calculate_OneEmptyString_ReturnsLengthOfOtherString()");
        var str2 = "";
		_logger.LogDebug("Calculate_OneEmptyString_ReturnsLengthOfOtherString(): str2 → {Str2}", str2);
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
		_logger.LogDebug("Calculate_OneEmptyString_ReturnsLengthOfOtherString(): result → {Result}", result);
		_logger.LogDebug("Calculate_OneEmptyString_ReturnsLengthOfOtherString(): str1 → {Str1}", str1);
        Assert.That(result, Is.EqualTo(4), "One empty string should have a Damerau-Levenshtein distance equal to the length of the other string.");
    }

    [Test]
    public void Calculate_BothEmptyStrings_ReturnsZero()
    {
		_logger.LogDebug(">>> Entering Calculate_BothEmptyStrings_ReturnsZero()");
        var str1 = "";
		_logger.LogDebug("<<< Exiting Calculate_BothEmptyStrings_ReturnsZero()");
        var str2 = "";
		_logger.LogDebug("Calculate_BothEmptyStrings_ReturnsZero(): str2 → {Str2}", str2);
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
		_logger.LogDebug("Calculate_BothEmptyStrings_ReturnsZero(): result → {Result}", result);
		_logger.LogDebug("Calculate_BothEmptyStrings_ReturnsZero(): str1 → {Str1}", str1);
        Assert.That(result, Is.EqualTo(0), "Both empty strings should have a Damerau-Levenshtein distance of 0.");
    }

    [Test]
    public void Calculate_DifferentLengths_ReturnsCorrectValue()
    {
		_logger.LogDebug(">>> Entering Calculate_DifferentLengths_ReturnsCorrectValue()");
        var str1 = "short";
		_logger.LogDebug("<<< Exiting Calculate_DifferentLengths_ReturnsCorrectValue()");
        var str2 = "longer";
		_logger.LogDebug("Calculate_DifferentLengths_ReturnsCorrectValue(): str2 → {Str2}", str2);
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
		_logger.LogDebug("Calculate_DifferentLengths_ReturnsCorrectValue(): result → {Result}", result);
		_logger.LogDebug("Calculate_DifferentLengths_ReturnsCorrectValue(): str1 → {Str1}", str1);
        Assert.That(result, Is.EqualTo(6), "Strings of different lengths should return the correct Damerau-Levenshtein distance.");
    }

    [Test]
    public void Calculate_SpecialCharacters_ReturnsCorrectValue()
    {
		_logger.LogDebug(">>> Entering Calculate_SpecialCharacters_ReturnsCorrectValue()");
        var str1 = "hello!";
		_logger.LogDebug("<<< Exiting Calculate_SpecialCharacters_ReturnsCorrectValue()");
        var str2 = "hello?";
		_logger.LogDebug("Calculate_SpecialCharacters_ReturnsCorrectValue(): str2 → {Str2}", str2);
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
		_logger.LogDebug("Calculate_SpecialCharacters_ReturnsCorrectValue(): result → {Result}", result);
		_logger.LogDebug("Calculate_SpecialCharacters_ReturnsCorrectValue(): str1 → {Str1}", str1);
        Assert.That(result, Is.EqualTo(1), "Strings with special characters should return the correct Damerau-Levenshtein distance.");
    }

    [Test]
    public void Calculate_DifferentCases_ReturnsCorrectValue()
    {
		_logger.LogDebug(">>> Entering Calculate_DifferentCases_ReturnsCorrectValue()");
        var str1 = "Hello";
		_logger.LogDebug("<<< Exiting Calculate_DifferentCases_ReturnsCorrectValue()");
        var str2 = "hello";
		_logger.LogDebug("Calculate_DifferentCases_ReturnsCorrectValue(): str2 → {Str2}", str2);
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
		_logger.LogDebug("Calculate_DifferentCases_ReturnsCorrectValue(): result → {Result}", result);
		_logger.LogDebug("Calculate_DifferentCases_ReturnsCorrectValue(): str1 → {Str1}", str1);
        Assert.That(result, Is.EqualTo(1), "Strings with different cases should return the correct Damerau-Levenshtein distance.");
    }

    [Test]
    public void Calculate_CommonPrefixes_ReturnsCorrectValue()
    {
		_logger.LogDebug(">>> Entering Calculate_CommonPrefixes_ReturnsCorrectValue()");
        var str1 = "prefix";
		_logger.LogDebug("<<< Exiting Calculate_CommonPrefixes_ReturnsCorrectValue()");
        var str2 = "pre";
		_logger.LogDebug("Calculate_CommonPrefixes_ReturnsCorrectValue(): str2 → {Str2}", str2);
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
		_logger.LogDebug("Calculate_CommonPrefixes_ReturnsCorrectValue(): result → {Result}", result);
		_logger.LogDebug("Calculate_CommonPrefixes_ReturnsCorrectValue(): str1 → {Str1}", str1);
        Assert.That(result, Is.EqualTo(3), "Strings with common prefixes should return the correct Damerau-Levenshtein distance.");
    }

    [Test]
    public void Calculate_CommonSuffixes_ReturnsCorrectValue()
    {
		_logger.LogDebug(">>> Entering Calculate_CommonSuffixes_ReturnsCorrectValue()");
        var str1 = "suffix";
		_logger.LogDebug("<<< Exiting Calculate_CommonSuffixes_ReturnsCorrectValue()");
        var str2 = "fix";
		_logger.LogDebug("Calculate_CommonSuffixes_ReturnsCorrectValue(): str2 → {Str2}", str2);
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
		_logger.LogDebug("Calculate_CommonSuffixes_ReturnsCorrectValue(): result → {Result}", result);
		_logger.LogDebug("Calculate_CommonSuffixes_ReturnsCorrectValue(): str1 → {Str1}", str1);
        Assert.That(result, Is.EqualTo(3), "Strings with common suffixes should return the correct Damerau-Levenshtein distance.");
    }

    [Test]
    public void Calculate_Transpositions_ReturnsCorrectValue()
    {
		_logger.LogDebug(">>> Entering Calculate_Transpositions_ReturnsCorrectValue()");
        var str1 = "abcd";
		_logger.LogDebug("<<< Exiting Calculate_Transpositions_ReturnsCorrectValue()");
        var str2 = "acbd";
		_logger.LogDebug("Calculate_Transpositions_ReturnsCorrectValue(): str2 → {Str2}", str2);
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
		_logger.LogDebug("Calculate_Transpositions_ReturnsCorrectValue(): result → {Result}", result);
		_logger.LogDebug("Calculate_Transpositions_ReturnsCorrectValue(): str1 → {Str1}", str1);
        Assert.That(result, Is.EqualTo(1), "Strings with transpositions should return the correct Damerau-Levenshtein distance.");
    }

    [Test]
    public void Calculate_RepeatedCharacters_ReturnsCorrectValue()
    {
		_logger.LogDebug(">>> Entering Calculate_RepeatedCharacters_ReturnsCorrectValue()");
        var str1 = "aaa";
		_logger.LogDebug("<<< Exiting Calculate_RepeatedCharacters_ReturnsCorrectValue()");
        var str2 = "aaaaa";
		_logger.LogDebug("Calculate_RepeatedCharacters_ReturnsCorrectValue(): str2 → {Str2}", str2);
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
		_logger.LogDebug("Calculate_RepeatedCharacters_ReturnsCorrectValue(): result → {Result}", result);
		_logger.LogDebug("Calculate_RepeatedCharacters_ReturnsCorrectValue(): str1 → {Str1}", str1);
        Assert.That(result, Is.EqualTo(2), "Strings with repeated characters should return the correct Damerau-Levenshtein distance.");
    }

    [Test]
    public void Calculate_UnicodeCharacters_ReturnsCorrectValue()
    {
		_logger.LogDebug(">>> Entering Calculate_UnicodeCharacters_ReturnsCorrectValue()");
        var str1 = "こんにちは";
		_logger.LogDebug("<<< Exiting Calculate_UnicodeCharacters_ReturnsCorrectValue()");
        var str2 = "こんばんは";
		_logger.LogDebug("Calculate_UnicodeCharacters_ReturnsCorrectValue(): str2 → {Str2}", str2);
        var result = DamerauLevenshteinDistance.Calculate(str1, str2);
		_logger.LogDebug("Calculate_UnicodeCharacters_ReturnsCorrectValue(): result → {Result}", result);
		_logger.LogDebug("Calculate_UnicodeCharacters_ReturnsCorrectValue(): str1 → {Str1}", str1);
        Assert.That(result, Is.EqualTo(2), "Strings with Unicode characters should return the correct Damerau-Levenshtein distance.");
    }
}
