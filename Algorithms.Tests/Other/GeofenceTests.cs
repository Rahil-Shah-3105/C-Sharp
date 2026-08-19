using Algorithms.Other;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Other
{
    [TestFixture]
    public class GeofenceTests
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        private Geofence? geofence;

        [SetUp]
        public void Setup()
        {
			_logger.LogDebug(">>> Entering Setup()");
            geofence = new Geofence(10.8231, 106.6297, 500);
			_logger.LogDebug("Setup(): geofence → {Geofence}", geofence);
			_logger.LogDebug("<<< Exiting Setup()");
        }

        [Test]
        public void IsInside_ShouldReturnTrue_WhenUserIsInsideGeofence()
        {
			_logger.LogDebug(">>> Entering IsInside_ShouldReturnTrue_WhenUserIsInsideGeofence()");
            double userLat = 10.8221;
            double userLon = 106.6289;

            bool? result = geofence?.IsInside(userLat, userLon);

            Assert.That(result, Is.True);
			_logger.LogDebug("<<< Exiting IsInside_ShouldReturnTrue_WhenUserIsInsideGeofence()");
        }

        [Test]
        public void IsInside_ShouldReturnFalse_WhenUserIsOutsideGeofence()
        {
			_logger.LogDebug(">>> Entering IsInside_ShouldReturnFalse_WhenUserIsOutsideGeofence()");
            double userLat = 10.8300;
            double userLon = 106.6400;

            bool? result = geofence?.IsInside(userLat, userLon);

            Assert.That(result, Is.False);
			_logger.LogDebug("<<< Exiting IsInside_ShouldReturnFalse_WhenUserIsOutsideGeofence()");
        }

        [Test]
        public void IsInside_ShouldReturnTrue_WhenUserIsExactlyOnGeofenceBoundary()
        {
			_logger.LogDebug(">>> Entering IsInside_ShouldReturnTrue_WhenUserIsExactlyOnGeofenceBoundary()");
            double userLat = 10.8231;
            double userLon = 106.6297;

            bool? result = geofence?.IsInside(userLat, userLon);

            Assert.That(result, Is.True);
			_logger.LogDebug("<<< Exiting IsInside_ShouldReturnTrue_WhenUserIsExactlyOnGeofenceBoundary()");
        }

        [Test]
        public void IsInside_ShouldReturnFalse_WhenUserIsFarFromGeofence()
        {
			_logger.LogDebug(">>> Entering IsInside_ShouldReturnFalse_WhenUserIsFarFromGeofence()");
            double userLat = 20.0000;
            double userLon = 100.0000;

            bool? result = geofence?.IsInside(userLat, userLon);

            Assert.That(result, Is.False);
			_logger.LogDebug("<<< Exiting IsInside_ShouldReturnFalse_WhenUserIsFarFromGeofence()");
        }
    }
}
