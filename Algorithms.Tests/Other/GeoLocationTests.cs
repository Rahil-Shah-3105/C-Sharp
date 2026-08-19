using Algorithms.Other;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Other;

public static class GeoLocationTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(53.430488d, -2.96129d, 53.430488d, -2.96129d, 0d)]
    [TestCase(53.430971d, -2.959806d, 53.430242d, -2.960830d, 105d)]
    public static void CalculateDistanceFromLatLngTest(
        double lat1,
        double lng1,
        double lat2,
        double lng2,
        double expectedValue)
    {
		_logger.LogDebug(">>> Entering CalculateDistanceFromLatLngTest(lat1={Lat1},lng1={Lng1},lat2={Lat2},lng2={Lng2},expectedValue={ExpectedValue})", lat1, lng1, lat2, lng2, expectedValue);
        var result = GeoLocation.CalculateDistanceFromLatLng(lat1, lng1, lat2, lng2);
		_logger.LogDebug("CalculateDistanceFromLatLngTest(lat1={Lat1},lng1={Lng1},lat2={Lat2},lng2={Lng2},expectedValue={ExpectedValue}): result → {Result}", lat1, lng1, lat2, lng2, expectedValue, result);
        var actualValue = Convert.ToDouble(result);
		_logger.LogDebug("CalculateDistanceFromLatLngTest(lat1={Lat1},lng1={Lng1},lat2={Lat2},lng2={Lng2},expectedValue={ExpectedValue}): actualValue → {ActualValue}", lat1, lng1, lat2, lng2, expectedValue, actualValue);

        // Assert
        Assert.That(actualValue, Is.EqualTo(expectedValue).Within(1d)); // Accept if distance diff is +/-1 meters.
		_logger.LogDebug("<<< Exiting CalculateDistanceFromLatLngTest(lat1={Lat1},lng1={Lng1},lat2={Lat2},lng2={Lng2},expectedValue={ExpectedValue})", lat1, lng1, lat2, lng2, expectedValue);
    }
}
