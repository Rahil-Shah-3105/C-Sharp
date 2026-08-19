using Algorithms.Other;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Other;

/// <summary>
///     A class for testing the Meeus's Julian Easter algorithm.
/// </summary>
public static class JulianEasterTest
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCaseSource(nameof(CalculateCases))]
    public static void CalculateTest(int year, DateTime expected)
    {
		_logger.LogDebug(">>> Entering CalculateTest(year={Year},expected={Expected})", year, expected);
        var result = JulianEaster.Calculate(year);

		_logger.LogDebug("CalculateTest(year={Year},expected={Expected}): result → {Result}", year, expected, result);
        Assert.That(result, Is.EqualTo(expected));
		_logger.LogDebug("<<< Exiting CalculateTest(year={Year},expected={Expected})", year, expected);
    }

    private static readonly object[] CalculateCases =
    [
        new object[] { 1800, new DateTime(1800, 04, 08, 00, 00, 00, DateTimeKind.Utc) },
        new object[] { 1950, new DateTime(1950, 03, 27, 00, 00, 00, DateTimeKind.Utc) },
        new object[] { 1991, new DateTime(1991, 03, 25, 00, 00, 00, DateTimeKind.Utc) },
        new object[] { 2000, new DateTime(2000, 04, 17, 00, 00, 00, DateTimeKind.Utc) },
        new object[] { 2199, new DateTime(2199, 04, 07, 00, 00, 00, DateTimeKind.Utc) }
    ];

}
