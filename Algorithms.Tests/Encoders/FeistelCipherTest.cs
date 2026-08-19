using Algorithms.Encoders;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Encoders;

public static class FeistelCipherTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void DecodedStringIsTheSame([Random(100)] uint key)
    {
		_logger.LogDebug(">>> Entering DecodedStringIsTheSame(key={Key})", key);
        // Arrange
        var encoder = new FeistelCipher();
		_logger.LogDebug("DecodedStringIsTheSame(key={Key}): encoder → {Encoder}", key, encoder);
        var random = new Randomizer();
		_logger.LogDebug("DecodedStringIsTheSame(key={Key}): random → {Random}", key, random);

        int lenOfString = random.Next(1000);
		_logger.LogDebug("DecodedStringIsTheSame(key={Key}): lenOfString → {LenOfString}", key, lenOfString);

        string message = random.GetString(lenOfString);

        // Act
        var encoded = encoder.Encode(message, key);
		_logger.LogDebug("DecodedStringIsTheSame(key={Key}): encoded → {Encoded}", key, encoded);
        var decoded = encoder.Decode(encoded, key);
		_logger.LogDebug("DecodedStringIsTheSame(key={Key}): decoded → {Decoded}", key, decoded);

        // Assert
        Assert.That(decoded, Is.EqualTo(message));
		_logger.LogDebug("<<< Exiting DecodedStringIsTheSame(key={Key})", key);
    }

    [TestCase("00001111", (uint)0x12345678)]
    [TestCase("00001111222233334444555566667", (uint)0x12345678)]
    [TestCase("000011112222333344445555666677", (uint)0x12345678)]
    [TestCase("0000111122223333444455556666777", (uint)0x12345678)]
    // The plain text will be padded to fill the size of block (16 bytes), so the encoded message should be aligned with the rule
    // (text.Length % 16 == 0)
    public static void TestEncodedMessageSize(string testCase, uint key)
    {
		_logger.LogDebug(">>> Entering TestEncodedMessageSize(testCase={TestCase},key={Key})", testCase, key);
        // Arrange
        var encoder = new FeistelCipher();
		_logger.LogDebug("TestEncodedMessageSize(testCase={TestCase},key={Key}): encoder → {Encoder}", testCase, key, encoder);

        // Assert
        Assert.Throws<ArgumentException>(() => encoder.Decode(testCase, key));
		_logger.LogDebug("<<< Exiting TestEncodedMessageSize(testCase={TestCase},key={Key})", testCase, key);
    }
}
