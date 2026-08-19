using Algorithms.Encoders;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Encoders;

public static class CaesarEncoderTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void DecodedStringIsTheSame([Random(100)] int key)
    {
		_logger.LogDebug(">>> Entering DecodedStringIsTheSame(key={Key})", key);
        // Arrange
        var encoder = new CaesarEncoder();
		_logger.LogDebug("DecodedStringIsTheSame(key={Key}): encoder → {Encoder}", key, encoder);
        var random = new Randomizer();
		_logger.LogDebug("DecodedStringIsTheSame(key={Key}): random → {Random}", key, random);
        var message = random.GetString();
		_logger.LogDebug("DecodedStringIsTheSame(key={Key}): message → {Message}", key, message);

        // Act
        var encoded = encoder.Encode(message, key);
		_logger.LogDebug("DecodedStringIsTheSame(key={Key}): encoded → {Encoded}", key, encoded);
        var decoded = encoder.Decode(encoded, key);
		_logger.LogDebug("DecodedStringIsTheSame(key={Key}): decoded → {Decoded}", key, decoded);

        // Assert
        Assert.That(decoded, Is.EqualTo(message));
		_logger.LogDebug("<<< Exiting DecodedStringIsTheSame(key={Key})", key);
    }
}
