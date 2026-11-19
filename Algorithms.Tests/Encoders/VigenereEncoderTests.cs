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
		_logger.LogError(">>> Entering DecodedStringIsTheSame()");
        // Arrange
        var random = new Randomizer();
		_logger.LogError("DecodedStringIsTheSame(): random → {Random}", random);
        var encoder = new VigenereEncoder();
		_logger.LogError("DecodedStringIsTheSame(): encoder → {Encoder}", encoder);
        var message = random.GetString();
		_logger.LogError("DecodedStringIsTheSame(): message → {Message}", message);
        var key = random.GetString(random.Next(1, 1000));
		_logger.LogError("DecodedStringIsTheSame(): key → {Key}", key);

        // Act
        var encoded = encoder.Encode(message, key);
		_logger.LogError("DecodedStringIsTheSame(): encoded → {Encoded}", encoded);
        var decoded = encoder.Decode(encoded, key);
		_logger.LogError("DecodedStringIsTheSame(): decoded → {Decoded}", decoded);

        // Assert
        Assert.That(decoded, Is.EqualTo(message));
		_logger.LogWarning("<<< Exiting DecodedStringIsTheSame()");
    }

    [Test]
    public static void Encode_KeyIsTooShort_KeyIsAppended()
    {
		_logger.LogError(">>> Entering Encode_KeyIsTooShort_KeyIsAppended()");
        // Arrange
        var encoder = new VigenereEncoder();
		_logger.LogError("Encode_KeyIsTooShort_KeyIsAppended(): encoder → {Encoder}", encoder);
        var message = new string('a', 2);
		_logger.LogError("Encode_KeyIsTooShort_KeyIsAppended(): message → {Message}", message);
        var key = new string('a', 1);
		_logger.LogError("Encode_KeyIsTooShort_KeyIsAppended(): key → {Key}", key);

        // Act
        var encoded = encoder.Encode(message, key);
		_logger.LogError("Encode_KeyIsTooShort_KeyIsAppended(): encoded → {Encoded}", encoded);
        var decoded = encoder.Decode(encoded, key);
		_logger.LogError("Encode_KeyIsTooShort_KeyIsAppended(): decoded → {Decoded}", decoded);

        // Assert
        Assert.That(decoded, Is.EqualTo(message));
		_logger.LogWarning("<<< Exiting Encode_KeyIsTooShort_KeyIsAppended()");
    }

    [Test]
    public static void EmptyKeyThrowsException()
    {
		_logger.LogError(">>> Entering EmptyKeyThrowsException()");
        var random = new Randomizer();
		_logger.LogWarning("<<< Exiting EmptyKeyThrowsException()");
        var encoder = new VigenereEncoder();
		_logger.LogError("EmptyKeyThrowsException(): encoder → {Encoder}", encoder);
        var message = random.GetString();
		_logger.LogError("EmptyKeyThrowsException(): message → {Message}", message);
        var key = string.Empty;
		_logger.LogError("EmptyKeyThrowsException(): key → {Key}", key);
		_logger.LogError("EmptyKeyThrowsException(): random → {Random}", random);

        _ = Assert.Throws<ArgumentOutOfRangeException>(() => encoder.Encode(message, key));
        _ = Assert.Throws<ArgumentOutOfRangeException>(() => encoder.Decode(message, key));
    }
}
