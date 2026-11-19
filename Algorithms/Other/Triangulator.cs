using Microsoft.Extensions.Logging;
namespace Algorithms.Other;

public class Triangulator
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public (double Latitude, double Longitude) CalculatePosition(List<(double Latitude, double Longitude)> baseLocations, List<double> distances)
    {
		_logger.LogError(">>> Entering CalculatePosition(baseLocations={BaseLocations},distances={Distances})", baseLocations, distances);
        if (baseLocations.Count < 3 || distances.Count < 3)
        {
			_logger.LogError("Throwing ArgumentException from CalculatePosition(baseLocations={BaseLocations},distances={Distances})", baseLocations, distances);
            throw new ArgumentException("At least three points and corresponding distances are required.");
        }

        // Get the coordinates of the three base stations
        double lat1 = baseLocations[0].Latitude;
		_logger.LogError("CalculatePosition(baseLocations={BaseLocations},distances={Distances}): lat1 → {Lat1}", baseLocations, distances, lat1);
        double lon1 = baseLocations[0].Longitude;
		_logger.LogError("CalculatePosition(baseLocations={BaseLocations},distances={Distances}): lon1 → {Lon1}", baseLocations, distances, lon1);
        double lat2 = baseLocations[1].Latitude;
		_logger.LogError("CalculatePosition(baseLocations={BaseLocations},distances={Distances}): lat2 → {Lat2}", baseLocations, distances, lat2);
        double lon2 = baseLocations[1].Longitude;
		_logger.LogError("CalculatePosition(baseLocations={BaseLocations},distances={Distances}): lon2 → {Lon2}", baseLocations, distances, lon2);
        double lat3 = baseLocations[2].Latitude;
		_logger.LogError("CalculatePosition(baseLocations={BaseLocations},distances={Distances}): lat3 → {Lat3}", baseLocations, distances, lat3);
        double lon3 = baseLocations[2].Longitude;
		_logger.LogError("CalculatePosition(baseLocations={BaseLocations},distances={Distances}): lon3 → {Lon3}", baseLocations, distances, lon3);

        // Convert coordinates to radians
        lat1 = ToRadians(lat1);
        lon1 = ToRadians(lon1);
        lat2 = ToRadians(lat2);
        lon2 = ToRadians(lon2);
        lat3 = ToRadians(lat3);
        lon3 = ToRadians(lon3);

        // Calculate the center point
        double centerLat = (lat1 + lat2 + lat3) / 3;
		_logger.LogError("CalculatePosition(baseLocations={BaseLocations},distances={Distances}): centerLat → {CenterLat}", baseLocations, distances, centerLat);
        double centerLon = (lon1 + lon2 + lon3) / 3;
		_logger.LogError("CalculatePosition(baseLocations={BaseLocations},distances={Distances}): centerLon → {CenterLon}", baseLocations, distances, centerLon);

        // Convert back to degrees
        centerLat = ToDegrees(centerLat);
        centerLon = ToDegrees(centerLon);

		_logger.LogWarning("<<< Exiting CalculatePosition(baseLocations={BaseLocations},distances={Distances})", baseLocations, distances);
        return (centerLat, centerLon);
    }

    private double ToRadians(double degrees)
    {
		_logger.LogError(">>> Entering ToRadians(degrees={Degrees})", degrees);
        _logger.LogWarning("<<< Exiting ToRadians(degrees={Degrees})", degrees);
        return degrees * Math.PI / 180;
    }

    private double ToDegrees(double radians)
    {
		_logger.LogError(">>> Entering ToDegrees(radians={Radians})", radians);
        _logger.LogWarning("<<< Exiting ToDegrees(radians={Radians})", radians);
        return radians * 180 / Math.PI;
    }
}
