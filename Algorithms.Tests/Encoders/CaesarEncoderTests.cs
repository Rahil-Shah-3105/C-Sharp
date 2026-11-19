using Algorithms.Encoders;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Encoders;

public static class CaesarEncoderTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void DecodedStringIsTheSame([Random(100)] int key)
    {
		_logger.LogError(">>> Entering DecodedStringIsTheSame(key={Key})", key);
        // Arrange
        var encoder = new CaesarEncoder();
		_logger.LogError("DecodedStringIsTheSame(key={Key}): encoder → {Encoder}", key, encoder);
        var random = new Randomizer();
		_logger.LogError("DecodedStringIsTheSame(key={Key}): random → {Random}", key, random);
        var message = random.GetString();
		_logger.LogError("DecodedStringIsTheSame(key={Key}): message → {Message}", key, message);

        // Act
        var encoded = encoder.Encode(message, key);
		_logger.LogError("DecodedStringIsTheSame(key={Key}): encoded → {Encoded}", key, encoded);
        var decoded = encoder.Decode(encoded, key);
		_logger.LogError("DecodedStringIsTheSame(key={Key}): decoded → {Decoded}", key, decoded);

        // Assert
        Assert.That(decoded, Is.EqualTo(message));
		_logger.LogWarning("<<< Exiting DecodedStringIsTheSame(key={Key})", key);
    }
}
