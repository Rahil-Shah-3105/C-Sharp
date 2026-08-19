using Algorithms.DataCompression;
using Algorithms.Knapsack;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Compressors;

public static class ShannonFanoCompressorTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase("dddddddddd", "1111111111")]
    [TestCase("a", "1")]
    [TestCase("", "")]
    public static void CompressingPhrase(string uncompressedText, string expectedCompressedText)
    {
		_logger.LogDebug(">>> Entering CompressingPhrase(uncompressedText={UncompressedText},expectedCompressedText={ExpectedCompressedText})", uncompressedText, expectedCompressedText);
        //Arrange
        var solver = new NaiveKnapsackSolver<(char, double)>();
		_logger.LogDebug("CompressingPhrase(uncompressedText={UncompressedText},expectedCompressedText={ExpectedCompressedText}): solver → {Solver}", uncompressedText, expectedCompressedText, solver);
        var translator = new Translator();
		_logger.LogDebug("CompressingPhrase(uncompressedText={UncompressedText},expectedCompressedText={ExpectedCompressedText}): translator → {Translator}", uncompressedText, expectedCompressedText, translator);
        var shannonFanoCompressor = new ShannonFanoCompressor(solver, translator);
		_logger.LogDebug("CompressingPhrase(uncompressedText={UncompressedText},expectedCompressedText={ExpectedCompressedText}): shannonFanoCompressor → {ShannonFanoCompressor}", uncompressedText, expectedCompressedText, shannonFanoCompressor);

        //Act
        var (compressedText, decompressionKeys) = shannonFanoCompressor.Compress(uncompressedText);
        var decompressedText = translator.Translate(compressedText, decompressionKeys);
		_logger.LogDebug("CompressingPhrase(uncompressedText={UncompressedText},expectedCompressedText={ExpectedCompressedText}): decompressedText → {DecompressedText}", uncompressedText, expectedCompressedText, decompressedText);

        //Assert
        Assert.That(compressedText, Is.EqualTo(expectedCompressedText));
        Assert.That(decompressedText, Is.EqualTo(uncompressedText));
		_logger.LogDebug("<<< Exiting CompressingPhrase(uncompressedText={UncompressedText},expectedCompressedText={ExpectedCompressedText})", uncompressedText, expectedCompressedText);
    }

    [Test]
    public static void DecompressedTextTheSameAsOriginal([Random(0, 1000, 100)] int length)
    {
		_logger.LogDebug(">>> Entering DecompressedTextTheSameAsOriginal(length={Length})", length);
        //Arrange
        var solver = new NaiveKnapsackSolver<(char, double)>();
		_logger.LogDebug("DecompressedTextTheSameAsOriginal(length={Length}): solver → {Solver}", length, solver);
        var translator = new Translator();
		_logger.LogDebug("DecompressedTextTheSameAsOriginal(length={Length}): translator → {Translator}", length, translator);
        var shannonFanoCompressor = new ShannonFanoCompressor(solver, translator);
		_logger.LogDebug("DecompressedTextTheSameAsOriginal(length={Length}): shannonFanoCompressor → {ShannonFanoCompressor}", length, shannonFanoCompressor);
        var text = Randomizer.CreateRandomizer().GetString(length);
		_logger.LogDebug("DecompressedTextTheSameAsOriginal(length={Length}): text → {Text}", length, text);

        //Act
        var (compressedText, decompressionKeys) = shannonFanoCompressor.Compress(text);
        var decompressedText = translator.Translate(compressedText, decompressionKeys);
		_logger.LogDebug("DecompressedTextTheSameAsOriginal(length={Length}): decompressedText → {DecompressedText}", length, decompressedText);

        //Assert
        Assert.That(decompressedText, Is.EqualTo(text));
		_logger.LogDebug("<<< Exiting DecompressedTextTheSameAsOriginal(length={Length})", length);
    }
}
