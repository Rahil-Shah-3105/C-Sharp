using Algorithms.DataCompression;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Compressors;

public static class TranslatorTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void TranslateCorrectly()
    {
		_logger.LogDebug(">>> Entering TranslateCorrectly()");
        // Arrange
        var translator = new Translator();
		_logger.LogDebug("TranslateCorrectly(): translator → {Translator}", translator);
        var dict = new Dictionary<string, string>
        {
            { "Hey", "Good day" },
            { " ", " " },
            { "man", "sir" },
            { "!", "." },
        };

        // Act
        var translatedText = translator.Translate("Hey man!", dict);
		_logger.LogDebug("TranslateCorrectly(): translatedText → {TranslatedText}", translatedText);

        // Assert
        Assert.That(translatedText, Is.EqualTo("Good day sir."));
		_logger.LogDebug("<<< Exiting TranslateCorrectly()");
    }
}
