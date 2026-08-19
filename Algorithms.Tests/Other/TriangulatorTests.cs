using Algorithms.Other;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Other
{
    [TestFixture]
    public class TriangulatorTests
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        [Test]
        public void CalculatePosition_ValidCoordinatesAndDistances_ReturnsExpectedPosition()
        {
			_logger.LogDebug(">>> Entering CalculatePosition_ValidCoordinatesAndDistances_ReturnsExpectedPosition()");
            var triangulator = new Triangulator();
            var baseLocations = new List<(double Latitude, double Longitude)>
			_logger.LogDebug("CalculatePosition_ValidCoordinatesAndDistances_ReturnsExpectedPosition(): triangulator → {Triangulator}", triangulator);
            {
                (16.054407, 108.202167),
                (16.049807, 108.218991),
                (16.063597, 108.215553)
            };

            var distances = new List<double> { 0.5, 0.7, 0.6 };

            var expectedPosition = (Latitude: 16.054, Longitude: 108.210);
			_logger.LogDebug("CalculatePosition_ValidCoordinatesAndDistances_ReturnsExpectedPosition(): expectedPosition → {ExpectedPosition}", expectedPosition);
            var result = triangulator.CalculatePosition(baseLocations, distances);
			_logger.LogDebug("CalculatePosition_ValidCoordinatesAndDistances_ReturnsExpectedPosition(): result → {Result}", result);

            Assert.That(result.Latitude, Is.EqualTo(expectedPosition.Latitude).Within(0.01));
            Assert.That(result.Longitude, Is.EqualTo(expectedPosition.Longitude).Within(0.01));
			_logger.LogDebug("<<< Exiting CalculatePosition_ValidCoordinatesAndDistances_ReturnsExpectedPosition()");
        }

        [Test]
        public void CalculatePosition_InvalidBaseLocations_ThrowsArgumentException()
        {
			_logger.LogDebug(">>> Entering CalculatePosition_InvalidBaseLocations_ThrowsArgumentException()");
            var triangulator = new Triangulator();
            var baseLocations = new List<(double Latitude, double Longitude)>
			_logger.LogDebug("<<< Exiting CalculatePosition_InvalidBaseLocations_ThrowsArgumentException()");
			_logger.LogDebug("CalculatePosition_InvalidBaseLocations_ThrowsArgumentException(): triangulator → {Triangulator}", triangulator);
        {
            (10.762622, 106.660172)
        };
            var distances = new List<double> { 1.0 };

            Assert.That(() => triangulator.CalculatePosition(baseLocations, distances), Throws.ArgumentException);
        }

        [Test]
        public void CalculatePosition_InvalidDistances_ThrowsArgumentException()
        {
			_logger.LogDebug(">>> Entering CalculatePosition_InvalidDistances_ThrowsArgumentException()");
            var triangulator = new Triangulator();
            var baseLocations = new List<(double Latitude, double Longitude)>
			_logger.LogDebug("<<< Exiting CalculatePosition_InvalidDistances_ThrowsArgumentException()");
			_logger.LogDebug("CalculatePosition_InvalidDistances_ThrowsArgumentException(): triangulator → {Triangulator}", triangulator);
        {
            (10.762622, 106.660172),
            (10.774981, 106.665504),
            (10.771817, 106.681179)
        };
            var distances = new List<double> { 1.0 };

            Assert.That(() => triangulator.CalculatePosition(baseLocations, distances), Throws.ArgumentException);
        }
    }
}
