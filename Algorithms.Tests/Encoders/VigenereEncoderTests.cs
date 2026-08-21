using Algorithms.Encoders;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Encoders;

public static class VigenereEncoderTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    [Repeat(100)]
    public static void DecodedStringIsTheSame()
    {
		_logger.LogDebug(">>> Entering DecodedStringIsTheSame()");
        // Arrange
        var random = new Randomizer();
		_logger.LogDebug("DecodedStringIsTheSame(): random → {Random}", random);
        var encoder = new VigenereEncoder();
		_logger.LogDebug("DecodedStringIsTheSame(): encoder → {Encoder}", encoder);
        var message = random.GetString();
		_logger.LogDebug("DecodedStringIsTheSame(): message → {Message}", message);
        var key = random.GetString(random.Next(1, 1000));
		_logger.LogWarning("DecodedStringIsTheSame(): key → {Key}", key);

        // Act
        var encoded = encoder.Encode(message, key);
		_logger.LogDebug("DecodedStringIsTheSame(): encoded → {Encoded}", encoded);
        var decoded = encoder.Decode(encoded, key);
		_logger.LogDebug("DecodedStringIsTheSame(): decoded → {Decoded}", decoded);

        // Assert
        Assert.That(decoded, Is.EqualTo(message));
		_logger.LogDebug("<<< Exiting DecodedStringIsTheSame()");
    }

    [Test]
    public static void Encode_KeyIsTooShort_KeyIsAppended()
    {
		_logger.LogDebug(">>> Entering Encode_KeyIsTooShort_KeyIsAppended()");
        // Arrange
        var encoder = new VigenereEncoder();
		_logger.LogDebug("Encode_KeyIsTooShort_KeyIsAppended(): encoder → {Encoder}", encoder);
        var message = new string('a', 2);
		_logger.LogDebug("Encode_KeyIsTooShort_KeyIsAppended(): message → {Message}", message);
        var key = new string('a', 1);
		_logger.LogWarning("Encode_KeyIsTooShort_KeyIsAppended(): key → {Key}", key);

        // Act
        var encoded = encoder.Encode(message, key);
		_logger.LogDebug("Encode_KeyIsTooShort_KeyIsAppended(): encoded → {Encoded}", encoded);
        var decoded = encoder.Decode(encoded, key);
		_logger.LogDebug("Encode_KeyIsTooShort_KeyIsAppended(): decoded → {Decoded}", decoded);

        // Assert
        Assert.That(decoded, Is.EqualTo(message));
		_logger.LogDebug("<<< Exiting Encode_KeyIsTooShort_KeyIsAppended()");
    }

    [Test]
    public static void EmptyKeyThrowsException()
    {
		_logger.LogDebug(">>> Entering EmptyKeyThrowsException()");
        var random = new Randomizer();
		_logger.LogDebug("<<< Exiting EmptyKeyThrowsException()");
        var encoder = new VigenereEncoder();
		_logger.LogDebug("EmptyKeyThrowsException(): encoder → {Encoder}", encoder);
        var message = random.GetString();
		_logger.LogDebug("EmptyKeyThrowsException(): message → {Message}", message);
        var key = string.Empty;
		_logger.LogWarning("EmptyKeyThrowsException(): key → {Key}", key);
		_logger.LogDebug("EmptyKeyThrowsException(): random → {Random}", random);

        _ = Assert.Throws<ArgumentOutOfRangeException>(() => encoder.Encode(message, key));
        _ = Assert.Throws<ArgumentOutOfRangeException>(() => encoder.Decode(message, key));
    }
}
