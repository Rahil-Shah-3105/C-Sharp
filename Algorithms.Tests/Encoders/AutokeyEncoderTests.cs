using Algorithms.Encoders;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Encoders
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public static class AutokeyEncoderTests
    {
        [Test]
        public static void DecodedStringIsTheSame()
        {
			_logger.LogDebug(">>> Entering DecodedStringIsTheSame()");
            // Arrange
            var plainText = "PLAINTEXT";
			_logger.LogDebug("DecodedStringIsTheSame(): plainText → {PlainText}", plainText);
            var keyword = "KEYWORD";
			_logger.LogDebug("DecodedStringIsTheSame(): keyword → {Keyword}", keyword);
            var encoder = new AutokeyEncorder();
			_logger.LogDebug("DecodedStringIsTheSame(): encoder → {Encoder}", encoder);

            // Act
            var encoded = encoder.Encode(plainText, keyword);
			_logger.LogDebug("DecodedStringIsTheSame(): encoded → {Encoded}", encoded);
            var decoded = encoder.Decode(encoded, keyword);
			_logger.LogDebug("DecodedStringIsTheSame(): decoded → {Decoded}", decoded);

            // Assert
            Assert.That(decoded, Is.EqualTo(plainText));
			_logger.LogDebug("<<< Exiting DecodedStringIsTheSame()");
        }
    }
}
