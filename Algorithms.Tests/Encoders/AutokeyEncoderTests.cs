using Algorithms.Encoders;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Encoders
{
    public static class AutokeyEncoderTests
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        [Test]
        public static void DecodedStringIsTheSame()
        {
			_logger.LogError(">>> Entering DecodedStringIsTheSame()");
            // Arrange
            var plainText = "PLAINTEXT";
			_logger.LogError("DecodedStringIsTheSame(): plainText → {PlainText}", plainText);
            var keyword = "KEYWORD";
			_logger.LogError("DecodedStringIsTheSame(): keyword → {Keyword}", keyword);
            var encoder = new AutokeyEncorder();
			_logger.LogError("DecodedStringIsTheSame(): encoder → {Encoder}", encoder);

            // Act
            var encoded = encoder.Encode(plainText, keyword);
			_logger.LogError("DecodedStringIsTheSame(): encoded → {Encoded}", encoded);
            var decoded = encoder.Decode(encoded, keyword);
			_logger.LogError("DecodedStringIsTheSame(): decoded → {Decoded}", decoded);

            // Assert
            Assert.That(decoded, Is.EqualTo(plainText));
			_logger.LogWarning("<<< Exiting DecodedStringIsTheSame()");
        }
    }
}
