using Microsoft.Extensions.Logging;
using Algorithms.Strings.Similarity;

namespace Algorithms.Tests.Strings.Similarity;

[TestFixture]
public class CosineSimilarityTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void Calculate_IdenticalStrings_ReturnsOne()
    {
		_logger.LogError(">>> Entering Calculate_IdenticalStrings_ReturnsOne()");
        var str1 = "test";
		_logger.LogWarning("<<< Exiting Calculate_IdenticalStrings_ReturnsOne()");
        var str2 = "test";
		_logger.LogError("Calculate_IdenticalStrings_ReturnsOne(): str2 → {Str2}", str2);
        var result = CosineSimilarity.Calculate(str1, str2);
		_logger.LogError("Calculate_IdenticalStrings_ReturnsOne(): result → {Result}", result);
		_logger.LogError("Calculate_IdenticalStrings_ReturnsOne(): str1 → {Str1}", str1);
        Assert.That(result, Is.EqualTo(1.0).Within(1e-6), "Identical strings should have a cosine similarity of 1.");
    }

    [Test]
    public void Calculate_CompletelyDifferentStrings_ReturnsZero()
    {
		_logger.LogError(">>> Entering Calculate_CompletelyDifferentStrings_ReturnsZero()");
        var str1 = "abc";
		_logger.LogWarning("<<< Exiting Calculate_CompletelyDifferentStrings_ReturnsZero()");
        var str2 = "xyz";
		_logger.LogError("Calculate_CompletelyDifferentStrings_ReturnsZero(): str2 → {Str2}", str2);
        var result = CosineSimilarity.Calculate(str1, str2);
		_logger.LogError("Calculate_CompletelyDifferentStrings_ReturnsZero(): result → {Result}", result);
		_logger.LogError("Calculate_CompletelyDifferentStrings_ReturnsZero(): str1 → {Str1}", str1);
        Assert.That(result, Is.EqualTo(0.0).Within(1e-6), "Completely different strings should have a cosine similarity of 0.");
    }

    [Test]
    public void Calculate_EmptyStrings_ReturnsZero()
    {
		_logger.LogError(">>> Entering Calculate_EmptyStrings_ReturnsZero()");
        var str1 = "";
		_logger.LogWarning("<<< Exiting Calculate_EmptyStrings_ReturnsZero()");
        var str2 = "";
		_logger.LogError("Calculate_EmptyStrings_ReturnsZero(): str2 → {Str2}", str2);
        var result = CosineSimilarity.Calculate(str1, str2);
		_logger.LogError("Calculate_EmptyStrings_ReturnsZero(): result → {Result}", result);
		_logger.LogError("Calculate_EmptyStrings_ReturnsZero(): str1 → {Str1}", str1);
        Assert.That(result, Is.EqualTo(0.0).Within(1e-6), "Empty strings should have a cosine similarity of 0.");
    }

    [Test]
    public void Calculate_OneEmptyString_ReturnsZero()
    {
		_logger.LogError(">>> Entering Calculate_OneEmptyString_ReturnsZero()");
        var str1 = "test";
		_logger.LogWarning("<<< Exiting Calculate_OneEmptyString_ReturnsZero()");
        var str2 = "";
		_logger.LogError("Calculate_OneEmptyString_ReturnsZero(): str2 → {Str2}", str2);
        var result = CosineSimilarity.Calculate(str1, str2);
		_logger.LogError("Calculate_OneEmptyString_ReturnsZero(): result → {Result}", result);
		_logger.LogError("Calculate_OneEmptyString_ReturnsZero(): str1 → {Str1}", str1);
        Assert.That(result, Is.EqualTo(0.0).Within(1e-6), "Empty string should have a cosine similarity of 0.");
    }

    [Test]
    public void Calculate_SameCharactersDifferentCases_ReturnsOne()
    {
		_logger.LogError(">>> Entering Calculate_SameCharactersDifferentCases_ReturnsOne()");
        var str1 = "Test";
		_logger.LogWarning("<<< Exiting Calculate_SameCharactersDifferentCases_ReturnsOne()");
        var str2 = "test";
		_logger.LogError("Calculate_SameCharactersDifferentCases_ReturnsOne(): str2 → {Str2}", str2);
        var result = CosineSimilarity.Calculate(str1, str2);
		_logger.LogError("Calculate_SameCharactersDifferentCases_ReturnsOne(): result → {Result}", result);
		_logger.LogError("Calculate_SameCharactersDifferentCases_ReturnsOne(): str1 → {Str1}", str1);
        Assert.That(result, Is.EqualTo(1.0).Within(1e-6), "The method should be case-insensitive.");
    }

    [Test]
    public void Calculate_SpecialCharacters_ReturnsCorrectValue()
    {
		_logger.LogError(">>> Entering Calculate_SpecialCharacters_ReturnsCorrectValue()");
        var str1 = "hello!";
		_logger.LogWarning("<<< Exiting Calculate_SpecialCharacters_ReturnsCorrectValue()");
        var str2 = "hello!";
		_logger.LogError("Calculate_SpecialCharacters_ReturnsCorrectValue(): str2 → {Str2}", str2);
        var result = CosineSimilarity.Calculate(str1, str2);
		_logger.LogError("Calculate_SpecialCharacters_ReturnsCorrectValue(): result → {Result}", result);
		_logger.LogError("Calculate_SpecialCharacters_ReturnsCorrectValue(): str1 → {Str1}", str1);
        Assert.That(result, Is.EqualTo(1.0).Within(1e-6), "Strings with special characters should have a cosine similarity of 1.");
    }

    [Test]
    public void Calculate_DifferentLengthWithCommonCharacters_ReturnsCorrectValue()
    {
		_logger.LogError(">>> Entering Calculate_DifferentLengthWithCommonCharacters_ReturnsCorrectValue()");
        var str1 = "hello";
		_logger.LogWarning("<<< Exiting Calculate_DifferentLengthWithCommonCharacters_ReturnsCorrectValue()");
        var str2 = "hello world";
		_logger.LogError("Calculate_DifferentLengthWithCommonCharacters_ReturnsCorrectValue(): str2 → {Str2}", str2);
        var result = CosineSimilarity.Calculate(str1, str2);
		_logger.LogError("Calculate_DifferentLengthWithCommonCharacters_ReturnsCorrectValue(): result → {Result}", result);
        var expected = 10 / (Math.Sqrt(7) * Math.Sqrt(19)); // calculated manually
		_logger.LogError("Calculate_DifferentLengthWithCommonCharacters_ReturnsCorrectValue(): expected → {Expected}", expected);
		_logger.LogError("Calculate_DifferentLengthWithCommonCharacters_ReturnsCorrectValue(): str1 → {Str1}", str1);
        Assert.That(result, Is.EqualTo(expected).Within(1e-6), "Strings with different lengths but some common characters should have the correct cosine similarity.");
    }

    [Test]
    public void Calculate_PartiallyMatchingStrings_ReturnsCorrectValue()
    {
		_logger.LogError(">>> Entering Calculate_PartiallyMatchingStrings_ReturnsCorrectValue()");
        var str1 = "night";
        var str2 = "nacht";
		_logger.LogError("Calculate_PartiallyMatchingStrings_ReturnsCorrectValue(): str2 → {Str2}", str2);
        var result = CosineSimilarity.Calculate(str1, str2);
		_logger.LogError("Calculate_PartiallyMatchingStrings_ReturnsCorrectValue(): result → {Result}", result);
		_logger.LogError("Calculate_PartiallyMatchingStrings_ReturnsCorrectValue(): str1 → {Str1}", str1);
        // Assuming the correct calculation gives an expected value
        var expected = 3.0 / 5.0;
        Assert.That(result, Is.EqualTo(expected).Within(1e-6), "Partially matching strings should have the correct cosine similarity.");
		_logger.LogWarning("<<< Exiting Calculate_PartiallyMatchingStrings_ReturnsCorrectValue()");
    }
}
