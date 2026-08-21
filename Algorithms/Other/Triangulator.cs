using Microsoft.Extensions.Logging;
namespace Algorithms.Other;

public class Triangulator
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public (double Latitude, double Longitude) CalculatePosition(List<(double Latitude, double Longitude)> baseLocations, List<double> distances)
    {
		_logger.LogDebug(">>> Entering CalculatePosition(Latitude={Latitude},Longitude={Longitude})", Latitude, Longitude);
        if (baseLocations.Count < 3 || distances.Count < 3)
        {
			_logger.LogWarning("Throwing ArgumentException from CalculatePosition(Latitude={Latitude},Longitude={Longitude})", Latitude, Longitude);
            throw new ArgumentException("At least three points and corresponding distances are required.");
        }

        // Get the coordinates of the three base stations
        double lat1 = baseLocations[0].Latitude;
		_logger.LogDebug("CalculatePosition(Latitude={Latitude},Longitude={Longitude}): lat1 → {Lat1}", Latitude, Longitude, lat1);
        double lon1 = baseLocations[0].Longitude;
		_logger.LogDebug("CalculatePosition(Latitude={Latitude},Longitude={Longitude}): lon1 → {Lon1}", Latitude, Longitude, lon1);
        double lat2 = baseLocations[1].Latitude;
		_logger.LogDebug("CalculatePosition(Latitude={Latitude},Longitude={Longitude}): lat2 → {Lat2}", Latitude, Longitude, lat2);
        double lon2 = baseLocations[1].Longitude;
		_logger.LogDebug("CalculatePosition(Latitude={Latitude},Longitude={Longitude}): lon2 → {Lon2}", Latitude, Longitude, lon2);
        double lat3 = baseLocations[2].Latitude;
		_logger.LogDebug("CalculatePosition(Latitude={Latitude},Longitude={Longitude}): lat3 → {Lat3}", Latitude, Longitude, lat3);
        double lon3 = baseLocations[2].Longitude;
		_logger.LogDebug("CalculatePosition(Latitude={Latitude},Longitude={Longitude}): lon3 → {Lon3}", Latitude, Longitude, lon3);

        // Convert coordinates to radians
        lat1 = ToRadians(lat1);
        lon1 = ToRadians(lon1);
        lat2 = ToRadians(lat2);
        lon2 = ToRadians(lon2);
        lat3 = ToRadians(lat3);
        lon3 = ToRadians(lon3);

        // Calculate the center point
        double centerLat = (lat1 + lat2 + lat3) / 3;
		_logger.LogDebug("CalculatePosition(Latitude={Latitude},Longitude={Longitude}): centerLat → {CenterLat}", Latitude, Longitude, centerLat);
        double centerLon = (lon1 + lon2 + lon3) / 3;
		_logger.LogDebug("CalculatePosition(Latitude={Latitude},Longitude={Longitude}): centerLon → {CenterLon}", Latitude, Longitude, centerLon);

        // Convert back to degrees
        centerLat = ToDegrees(centerLat);
        centerLon = ToDegrees(centerLon);

		_logger.LogDebug("<<< Exiting CalculatePosition(Latitude={Latitude},Longitude={Longitude})", Latitude, Longitude);
        return (centerLat, centerLon);
    }

    private double ToRadians(double degrees)
    {
		_logger.LogDebug(">>> Entering ToRadians(degrees={Degrees})", degrees);
        return degrees * Math.PI / 180;
    }

    private double ToDegrees(double radians)
    {
		_logger.LogDebug(">>> Entering ToDegrees(radians={Radians})", radians);
        return radians * 180 / Math.PI;
    }
}
