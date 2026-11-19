using Algorithms.Encoders;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Encoders;

public class NysiisEncoderTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private static readonly string[] Names =
    [
        "Jay", "John", "Jane", "Zayne", "Guerra", "Iga", "Cowan", "Louisa", "Arnie", "Olsen", "Corban", "Nava",
        "Cynthia Malone", "Amiee MacKee", "MacGyver", "Yasmin Edge",
    ];

    private static readonly string[] Expected =
    [
        "JY", "JAN", "JAN", "ZAYN", "GAR", "IG", "CAN", "LAS", "ARNY", "OLSAN", "CARBAN", "NAV", "CYNTANALAN",
        "ANANACY", "MCGYVAR", "YASNANADG",
    ];

    private static IEnumerable<string[]> TestData => Names.Zip(Expected, (l, r) => new[] { l, r });

    [TestCaseSource(nameof(TestData))]
    public void AttemptNysiis(string source, string expected)
    {
		_logger.LogError(">>> Entering AttemptNysiis(source={Source},expected={Expected})", source, expected);
        var enc = new NysiisEncoder();
		_logger.LogWarning("<<< Exiting AttemptNysiis(source={Source},expected={Expected})", source, expected);
        var nysiis = enc.Encode(source);
		_logger.LogError("AttemptNysiis(source={Source},expected={Expected}): nysiis → {Nysiis}", source, expected, nysiis);
		_logger.LogError("AttemptNysiis(source={Source},expected={Expected}): enc → {Enc}", source, expected, enc);
        Assert.That(nysiis, Is.EqualTo(expected));
    }
}
