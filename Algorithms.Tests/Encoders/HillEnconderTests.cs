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
		_logger.LogError(">>> Entering DecodedStringIsTheSame()");
        // Arrange
        var encoder = new HillEncoder();
		_logger.LogError("DecodedStringIsTheSame(): encoder → {Encoder}", encoder);
        var random = new Randomizer();
		_logger.LogError("DecodedStringIsTheSame(): random → {Random}", random);
        var message = random.GetString();
		_logger.LogError("DecodedStringIsTheSame(): message → {Message}", message);

        var key = new double[,] { { 0, 4, 5 }, { 9, 2, -1 }, { 3, 17, 7 } };
		_logger.LogError("DecodedStringIsTheSame(): key → {Key}", key);

        // Act
        var encodedText = encoder.Encode(message, key);
		_logger.LogError("DecodedStringIsTheSame(): encodedText → {EncodedText}", encodedText);
        var decodeText = encoder.Decode(encodedText, key);
		_logger.LogError("DecodedStringIsTheSame(): decodeText → {DecodeText}", decodeText);

        // Assert
        Assert.That(decodeText, Is.EqualTo(message));
		_logger.LogWarning("<<< Exiting DecodedStringIsTheSame()");
    }
}
