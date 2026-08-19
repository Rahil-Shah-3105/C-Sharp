using Algorithms.DataCompression;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Compressors;

public class BurrowsWheelerTransformTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase("banana", "nnbaaa", 3)]
    [TestCase("SIX.MIXED.PIXIES.SIFT.SIXTY.PIXIE.DUST.BOXES", "TEXYDST.E.IXIXIXXSSMPPS.B..E.S.EUSFXDIIOIIIT", 29)]
    [TestCase("", "", 0)]
    public void Encode(string input, string expectedString, int expectedIndex)
    {
		_logger.LogDebug(">>> Entering Encode(input={Input},expectedString={ExpectedString},expectedIndex={ExpectedIndex})", input, expectedString, expectedIndex);
        var bwt = new BurrowsWheelerTransform();

		_logger.LogDebug("Encode(input={Input},expectedString={ExpectedString},expectedIndex={ExpectedIndex}): bwt → {Bwt}", input, expectedString, expectedIndex, bwt);
        var (encoded, index) = bwt.Encode(input);

        Assert.That(encoded, Is.EqualTo(expectedString));
        Assert.That(index, Is.EqualTo(expectedIndex));
		_logger.LogDebug("<<< Exiting Encode(input={Input},expectedString={ExpectedString},expectedIndex={ExpectedIndex})", input, expectedString, expectedIndex);
    }

    [TestCase("nnbaaa", 3, "banana")]
    [TestCase("TEXYDST.E.IXIXIXXSSMPPS.B..E.S.EUSFXDIIOIIIT", 29, "SIX.MIXED.PIXIES.SIFT.SIXTY.PIXIE.DUST.BOXES")]
    [TestCase("", 0, "")]
    public void Decode(string encoded, int index, string expected)
    {
		_logger.LogDebug(">>> Entering Decode(encoded={Encoded},index={Index},expected={Expected})", encoded, index, expected);
        var bwt = new BurrowsWheelerTransform();

		_logger.LogDebug("Decode(encoded={Encoded},index={Index},expected={Expected}): bwt → {Bwt}", encoded, index, expected, bwt);
        var result = bwt.Decode(encoded, index);
		_logger.LogDebug("Decode(encoded={Encoded},index={Index},expected={Expected}): result → {Result}", encoded, index, expected, result);

        Assert.That(result, Is.EqualTo(expected));
		_logger.LogDebug("<<< Exiting Decode(encoded={Encoded},index={Index},expected={Expected})", encoded, index, expected);
    }

    [Test]
    [Repeat(100)]
    public void RandomEncodeDecode()
    {
		_logger.LogDebug(">>> Entering RandomEncodeDecode()");
        var bwt = new BurrowsWheelerTransform();
        var random = new Randomizer();
		_logger.LogDebug("RandomEncodeDecode(): random → {Random}", random);
        var inputString = random.GetString();
		_logger.LogDebug("RandomEncodeDecode(): inputString → {InputString}", inputString);
		_logger.LogDebug("RandomEncodeDecode(): bwt → {Bwt}", bwt);

        var (encoded, index) = bwt.Encode(inputString);
        var result = bwt.Decode(encoded, index);
		_logger.LogDebug("RandomEncodeDecode(): result → {Result}", result);

        Assert.That(result, Is.EqualTo(inputString));
		_logger.LogDebug("<<< Exiting RandomEncodeDecode()");
    }
}
