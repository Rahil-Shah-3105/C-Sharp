using Algorithms.DataCompression;
using Algorithms.Sorters.Comparison;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Compressors;

public static class HuffmanCompressorTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase("This is a string", "101010110111011101110111100011111010010010010011000")]
    [TestCase("Hello", "1101110010")]
    [TestCase("dddddddddd", "1111111111")]
    [TestCase("a", "1")]
    [TestCase("", "")]
    public static void CompressingPhrase(string uncompressedText, string expectedCompressedText)
    {
		_logger.LogDebug(">>> Entering CompressingPhrase(uncompressedText={UncompressedText},expectedCompressedText={ExpectedCompressedText})", uncompressedText, expectedCompressedText);
        //Arrange
        var sorter = new BubbleSorter<HuffmanCompressor.ListNode>();
		_logger.LogDebug("CompressingPhrase(uncompressedText={UncompressedText},expectedCompressedText={ExpectedCompressedText}): sorter → {Sorter}", uncompressedText, expectedCompressedText, sorter);
        var translator = new Translator();
		_logger.LogDebug("CompressingPhrase(uncompressedText={UncompressedText},expectedCompressedText={ExpectedCompressedText}): translator → {Translator}", uncompressedText, expectedCompressedText, translator);
        var huffman = new HuffmanCompressor(sorter, translator);
		_logger.LogDebug("CompressingPhrase(uncompressedText={UncompressedText},expectedCompressedText={ExpectedCompressedText}): huffman → {Huffman}", uncompressedText, expectedCompressedText, huffman);

        //Act
        var (compressedText, decompressionKeys) = huffman.Compress(uncompressedText);
        var decompressedText = translator.Translate(compressedText, decompressionKeys);
		_logger.LogDebug("CompressingPhrase(uncompressedText={UncompressedText},expectedCompressedText={ExpectedCompressedText}): decompressedText → {DecompressedText}", uncompressedText, expectedCompressedText, decompressedText);

        //Assert
        Assert.That(compressedText, Is.EqualTo(expectedCompressedText));
        Assert.That(decompressedText, Is.EqualTo(uncompressedText));
		_logger.LogDebug("<<< Exiting CompressingPhrase(uncompressedText={UncompressedText},expectedCompressedText={ExpectedCompressedText})", uncompressedText, expectedCompressedText);
    }

    [Test]
    public static void DecompressedTextTheSameAsOriginal(
        [Random(0, 1000, 100, Distinct = true)]
        int length)
    {
		_logger.LogDebug(">>> Entering DecompressedTextTheSameAsOriginal(Distinct={Distinct})", Distinct);
        //Arrange
        var sorter = new BubbleSorter<HuffmanCompressor.ListNode>();
		_logger.LogDebug("DecompressedTextTheSameAsOriginal(Distinct={Distinct}): sorter → {Sorter}", Distinct, sorter);
        var translator = new Translator();
		_logger.LogDebug("DecompressedTextTheSameAsOriginal(Distinct={Distinct}): translator → {Translator}", Distinct, translator);
        var huffman = new HuffmanCompressor(sorter, translator);
		_logger.LogDebug("DecompressedTextTheSameAsOriginal(Distinct={Distinct}): huffman → {Huffman}", Distinct, huffman);
        var text = Randomizer.CreateRandomizer().GetString(length);
		_logger.LogDebug("DecompressedTextTheSameAsOriginal(Distinct={Distinct}): text → {Text}", Distinct, text);

        //Act
        var (compressedText, decompressionKeys) = huffman.Compress(text);
        var decompressedText = translator.Translate(compressedText, decompressionKeys);
		_logger.LogDebug("DecompressedTextTheSameAsOriginal(Distinct={Distinct}): decompressedText → {DecompressedText}", Distinct, decompressedText);

        //Assert
        Assert.That(decompressedText, Is.EqualTo(text));
		_logger.LogDebug("<<< Exiting DecompressedTextTheSameAsOriginal(Distinct={Distinct})", Distinct);
    }

    [Test]
    public static void ListNodeComparer_NullIsUnordered()
    {
		_logger.LogDebug(">>> Entering ListNodeComparer_NullIsUnordered()");
        var comparer = new HuffmanCompressor.ListNodeComparer();
        var node = new HuffmanCompressor.ListNode('a', 0.1);
		_logger.LogDebug("ListNodeComparer_NullIsUnordered(): node → {Node}", node);
		_logger.LogDebug("ListNodeComparer_NullIsUnordered(): comparer → {Comparer}", comparer);

        comparer.Compare(node, null).Should().Be(0);
        comparer.Compare(null, node).Should().Be(0);
        comparer.Compare(null, null).Should().Be(0);
		_logger.LogDebug("<<< Exiting ListNodeComparer_NullIsUnordered()");
    }
}
