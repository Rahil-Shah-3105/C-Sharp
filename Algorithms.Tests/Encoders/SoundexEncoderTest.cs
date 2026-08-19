using Algorithms.Encoders;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Encoders;

public static class SoundexEncoderTest
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private static readonly string[] Names =
    [
        "Robert", "Rupert", "Rubin", "Ashcraft", "Ashcroft", "Tymczak", "Pfister", "Honeyman",
    ];

    private static readonly string[] Expected = ["R163", "R163", "R150", "A261", "A261", "T522", "P236", "H555"];

    private static IEnumerable<string[]> TestData => Names.Zip(Expected, (l, r) => new[] { l, r });

    [TestCaseSource(nameof(TestData))]
    public static void AttemptSoundex(string source, string encoded)
    {
		_logger.LogDebug(">>> Entering AttemptSoundex(source={Source},encoded={Encoded})", source, encoded);
        SoundexEncoder enc = new();
        var nysiis = enc.Encode(source);
		_logger.LogDebug("AttemptSoundex(source={Source},encoded={Encoded}): nysiis → {Nysiis}", source, encoded, nysiis);
		_logger.LogDebug("AttemptSoundex(source={Source},encoded={Encoded}): enc → {Enc}", source, encoded, enc);
        Assert.That(encoded, Is.EqualTo(nysiis));
		_logger.LogDebug("<<< Exiting AttemptSoundex(source={Source},encoded={Encoded})", source, encoded);
    }
}
