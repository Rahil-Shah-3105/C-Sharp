using Algorithms.Crypto.Utils;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Crypto.Utils
{
    [TestFixture]
    public class ByteEncodingUtilsTests
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        [Test]
        public void BigEndianToUint64_ByteArray_ShouldConvertCorrectly()
        {
			_logger.LogDebug(">>> Entering BigEndianToUint64_ByteArray_ShouldConvertCorrectly()");
            // Arrange
            byte[] input = [0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF];
            var expected = 0x0123456789ABCDEFUL;

            // Act
            var result = ByteEncodingUtils.BigEndianToUint64(input, 0);
			_logger.LogDebug("BigEndianToUint64_ByteArray_ShouldConvertCorrectly(): result → {Result}", result);

            // Assert
            result.Should().Be(expected);
			_logger.LogDebug("<<< Exiting BigEndianToUint64_ByteArray_ShouldConvertCorrectly()");
        }

        [Test]
        public void BigEndianToUint64_ByteArray_WithOffset_ShouldConvertCorrectly()
        {
			_logger.LogDebug(">>> Entering BigEndianToUint64_ByteArray_WithOffset_ShouldConvertCorrectly()");
            // Arrange
            byte[] input = [0x00, 0x00, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF];
            var expected = 0x0123456789ABCDEFUL;

            // Act
            var result = ByteEncodingUtils.BigEndianToUint64(input, 2);
			_logger.LogDebug("BigEndianToUint64_ByteArray_WithOffset_ShouldConvertCorrectly(): result → {Result}", result);

            // Assert
            result.Should().Be(expected);
			_logger.LogDebug("<<< Exiting BigEndianToUint64_ByteArray_WithOffset_ShouldConvertCorrectly()");
        }

        [Test]
        public void BigEndianToUint64_Span_ShouldConvertCorrectly()
        {
			_logger.LogDebug(">>> Entering BigEndianToUint64_Span_ShouldConvertCorrectly()");
            // Arrange
            Span<byte> input = stackalloc byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF };
            var expected = 0x0123456789ABCDEFUL;

            // Act
            var result = ByteEncodingUtils.BigEndianToUint64(input);
			_logger.LogDebug("BigEndianToUint64_Span_ShouldConvertCorrectly(): result → {Result}", result);

            // Assert
            result.Should().Be(expected);
			_logger.LogDebug("<<< Exiting BigEndianToUint64_Span_ShouldConvertCorrectly()");
        }

        [Test]
        public void UInt64ToBigEndian_ShouldWriteCorrectly()
        {
			_logger.LogDebug(">>> Entering UInt64ToBigEndian_ShouldWriteCorrectly()");
            // Arrange
            var value = 0x0123456789ABCDEFUL;
            Span<byte> output = stackalloc byte[8];
            byte[] expected = [0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF];

            // Act
            ByteEncodingUtils.UInt64ToBigEndian(value, output);

            // Assert
            output.ToArray().Should().Equal(expected);
			_logger.LogDebug("<<< Exiting UInt64ToBigEndian_ShouldWriteCorrectly()");
        }

        [Test]
        public void BigEndianToUint64_InvalidOffset_ShouldThrowException()
        {
			_logger.LogDebug(">>> Entering BigEndianToUint64_InvalidOffset_ShouldThrowException()");
            // Arrange
            byte[] input = [0x01, 0x23];

            // Act
            Action act = () => ByteEncodingUtils.BigEndianToUint64(input, 1);
			_logger.LogDebug("BigEndianToUint64_InvalidOffset_ShouldThrowException(): act → {Act}", act);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
			_logger.LogDebug("<<< Exiting BigEndianToUint64_InvalidOffset_ShouldThrowException()");
        }
    }
}
