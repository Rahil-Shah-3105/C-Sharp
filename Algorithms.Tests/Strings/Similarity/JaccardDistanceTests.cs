using Microsoft.Extensions.Logging;
﻿using Algorithms.Strings.Similarity;

namespace Algorithms.Tests.Strings.Similarity;

public class JaccardDistanceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly JaccardDistance jaccard = new JaccardDistance();
    private readonly double precision = 0.0001;

    [TestCase("left", null)]
    [TestCase(null, "right")]
    [TestCase(null, null)]
    public void Calculate_WhenStringsAreNull_ThrowsArgumentNullException(string left, string right)
    {
		_logger.LogDebug(">>> Entering Calculate_WhenStringsAreNull_ThrowsArgumentNullException(left={Left},right={Right})", left, right);
        Action action = () => jaccard.Calculate(left, right);
        action.Should().Throw<ArgumentNullException>();
		_logger.LogDebug("Calculate_WhenStringsAreNull_ThrowsArgumentNullException(left={Left},right={Right}): action → {Action}", left, right, action);
		_logger.LogDebug("<<< Exiting Calculate_WhenStringsAreNull_ThrowsArgumentNullException(left={Left},right={Right})", left, right);
    }


    [TestCase("", "", 0.0d)]
    [TestCase("left", "", 1.0d)]
    [TestCase("", "right", 1.0d)]
    [TestCase("frog", "fog", 0.25d)]
    [TestCase("fly", "ant", 1.0d)]
    [TestCase("elephant", "hippo", 0.777777d)]
    [TestCase("ABC Corporation", "ABC Corp", 0.36363d)]
    public void Calculate_WhenProvidedWithStrings_CalculatesCorrectDistance(string left, string right, double expected)
    {
		_logger.LogDebug(">>> Entering Calculate_WhenProvidedWithStrings_CalculatesCorrectDistance(left={Left},right={Right},expected={Expected})", left, right, expected);
        var distance = jaccard.Calculate(left, right);

		_logger.LogDebug("Calculate_WhenProvidedWithStrings_CalculatesCorrectDistance(left={Left},right={Right},expected={Expected}): distance → {Distance}", left, right, expected, distance);
        distance.Should().BeApproximately(expected, precision);
		_logger.LogDebug("<<< Exiting Calculate_WhenProvidedWithStrings_CalculatesCorrectDistance(left={Left},right={Right},expected={Expected})", left, right, expected);
    }
}
