using Microsoft.Extensions.Logging;
﻿using Algorithms.Encoders;

namespace Algorithms.Tests.Encoders;

// Tests ported from the Java Algorithms repository

public class BlowfishEncoderTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private const string Key = "aabb09182736ccdd";

    [Test]
    public void BlowfishEncoder_Encryption_ShouldWorkCorrectly()
    {
		_logger.LogDebug(">>> Entering BlowfishEncoder_Encryption_ShouldWorkCorrectly()");
        // Arrange
        var encoder = new BlowfishEncoder();
		_logger.LogDebug("BlowfishEncoder_Encryption_ShouldWorkCorrectly(): encoder → {Encoder}", encoder);
        encoder.GenerateKey(Key);

        const string plainText = "123456abcd132536";
        const string cipherText = "d748ec383d3405f7";

        // Act
        var result = encoder.Encrypt(plainText);
		_logger.LogDebug("BlowfishEncoder_Encryption_ShouldWorkCorrectly(): result → {Result}", result);

        // Assert
        result.Should().Be(cipherText);
		_logger.LogDebug("<<< Exiting BlowfishEncoder_Encryption_ShouldWorkCorrectly()");
    }

    [Test]
    public void BlowfishEncoder_Decryption_ShouldWorkCorrectly()
    {
		_logger.LogDebug(">>> Entering BlowfishEncoder_Decryption_ShouldWorkCorrectly()");
        // Arrange
        var encoder = new BlowfishEncoder();
		_logger.LogDebug("BlowfishEncoder_Decryption_ShouldWorkCorrectly(): encoder → {Encoder}", encoder);
        encoder.GenerateKey(Key);

        const string cipherText = "d748ec383d3405f7";
        const string plainText = "123456abcd132536";

        // Act
        var result = encoder.Decrypt(cipherText);
		_logger.LogDebug("BlowfishEncoder_Decryption_ShouldWorkCorrectly(): result → {Result}", result);

        // Assert
        result.Should().Be(plainText);
		_logger.LogDebug("<<< Exiting BlowfishEncoder_Decryption_ShouldWorkCorrectly()");
    }
}
