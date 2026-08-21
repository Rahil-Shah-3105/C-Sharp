using Algorithms.Encoders;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Encoders;

public static class HillEnconderTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    [Repeat(100)]
    public static void DecodedStringIsTheSame()
    {
		_logger.LogDebug(">>> Entering DecodedStringIsTheSame()");
        // Arrange
        var encoder = new HillEncoder();
		_logger.LogDebug("DecodedStringIsTheSame(): encoder → {Encoder}", encoder);
        var random = new Randomizer();
		_logger.LogDebug("DecodedStringIsTheSame(): random → {Random}", random);
        var message = random.GetString();
		_logger.LogDebug("DecodedStringIsTheSame(): message → {Message}", message);

        var key = new double[,] { { 0, 4, 5 }, { 9, 2, -1 }, { 3, 17, 7 } };
		_logger.LogWarning("DecodedStringIsTheSame(): key → {Key}", key);

        // Act
        var encodedText = encoder.Encode(message, key);
		_logger.LogDebug("DecodedStringIsTheSame(): encodedText → {EncodedText}", encodedText);
        var decodeText = encoder.Decode(encodedText, key);
		_logger.LogDebug("DecodedStringIsTheSame(): decodeText → {DecodeText}", decodeText);

        // Assert
        Assert.That(decodeText, Is.EqualTo(message));
		_logger.LogDebug("<<< Exiting DecodedStringIsTheSame()");
    }
}
