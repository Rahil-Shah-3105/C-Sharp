using Algorithms.Crypto.Utils;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Crypto.Utils
{
    [TestFixture]
    public class LongUtilsTests
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        [Test]
        public void RotateLeft_Long_ShouldRotateCorrectly()
        {
			_logger.LogDebug(">>> Entering RotateLeft_Long_ShouldRotateCorrectly()");
            // Arrange
            var input = 0x0123456789ABCDEF;
            var distance = 8;
            var expected = 0x23456789ABCDEF01L;  // The expected result is a signed long value.

            // Act
            var result = LongUtils.RotateLeft(input, distance);
			_logger.LogDebug("RotateLeft_Long_ShouldRotateCorrectly(): result → {Result}", result);

            // Assert
            result.Should().Be(expected);
			_logger.LogDebug("<<< Exiting RotateLeft_Long_ShouldRotateCorrectly()");
        }

        [Test]
        public void RotateLeft_Ulong_ShouldRotateCorrectly()
        {
			_logger.LogDebug(">>> Entering RotateLeft_Ulong_ShouldRotateCorrectly()");
            // Arrange
            var input = 0x0123456789ABCDEFUL;
            var distance = 8;
            var expected = 0x23456789ABCDEF01UL;  // The expected result is an unsigned ulong value.

            // Act
            var result = LongUtils.RotateLeft(input, distance);
			_logger.LogDebug("RotateLeft_Ulong_ShouldRotateCorrectly(): result → {Result}", result);

            // Assert
            result.Should().Be(expected);
			_logger.LogDebug("<<< Exiting RotateLeft_Ulong_ShouldRotateCorrectly()");
        }

        [Test]
        public void RotateRight_Long_ShouldRotateCorrectly()
        {
			_logger.LogDebug(">>> Entering RotateRight_Long_ShouldRotateCorrectly()");
            // Arrange
            var input = 0x0123456789ABCDEF;
            var distance = 8;
            var expected = unchecked((long)0xEF0123456789ABCD);  // Using unchecked to correctly represent signed long.
			_logger.LogDebug("RotateRight_Long_ShouldRotateCorrectly(): expected → {Expected}", expected);

            // Act
            var result = LongUtils.RotateRight(input, distance);
			_logger.LogDebug("RotateRight_Long_ShouldRotateCorrectly(): result → {Result}", result);

            // Assert
            result.Should().Be(expected);
			_logger.LogDebug("<<< Exiting RotateRight_Long_ShouldRotateCorrectly()");
        }

        [Test]
        public void RotateRight_Ulong_ShouldRotateCorrectly()
        {
			_logger.LogDebug(">>> Entering RotateRight_Ulong_ShouldRotateCorrectly()");
            // Arrange
            var input = 0x0123456789ABCDEFUL;
            var distance = 8;
            var expected = 0xEF0123456789ABCDUL;  // The expected result is an unsigned ulong value.

            // Act
            var result = LongUtils.RotateRight(input, distance);
			_logger.LogDebug("RotateRight_Ulong_ShouldRotateCorrectly(): result → {Result}", result);

            // Assert
            result.Should().Be(expected);
			_logger.LogDebug("<<< Exiting RotateRight_Ulong_ShouldRotateCorrectly()");
        }

        [Test]
        public void RotateLeft_Long_ShouldHandleZeroRotation()
        {
			_logger.LogDebug(">>> Entering RotateLeft_Long_ShouldHandleZeroRotation()");
            // Arrange
            var input = 0x0123456789ABCDEF;
            var distance = 0;

            // Act
            var result = LongUtils.RotateLeft(input, distance);
			_logger.LogDebug("RotateLeft_Long_ShouldHandleZeroRotation(): result → {Result}", result);

            // Assert
            result.Should().Be(input);  // No rotation, result should be the same as input.
			_logger.LogDebug("<<< Exiting RotateLeft_Long_ShouldHandleZeroRotation()");
        }

        [Test]
        public void RotateRight_Ulong_ShouldHandleFullRotation()
        {
			_logger.LogDebug(">>> Entering RotateRight_Ulong_ShouldHandleFullRotation()");
            // Arrange
            var input = 0x0123456789ABCDEFUL;
            var distance = 64;

            // Act
            var result = LongUtils.RotateRight(input, distance);
			_logger.LogDebug("RotateRight_Ulong_ShouldHandleFullRotation(): result → {Result}", result);

            // Assert
            result.Should().Be(input);  // Full 64-bit rotation should result in the same value.
			_logger.LogDebug("<<< Exiting RotateRight_Ulong_ShouldHandleFullRotation()");
        }
    }
}
