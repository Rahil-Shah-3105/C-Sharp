using Microsoft.Extensions.Logging;
namespace Algorithms.DataCompression;

/// <summary>
///     Provides method for text conversion by key mapping.
/// </summary>
public class Translator
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Converts the input text according to the translation keys.
    /// </summary>
    /// <param name="text">Input text.</param>
    /// <param name="translationKeys">Translation keys used for text matching.</param>
    /// <returns>Converted text according to the translation keys.</returns>
    public string Translate(string text, Dictionary<string, string> translationKeys)
    {
		_logger.LogDebug(">>> Entering Translate(text={Text},translationKeys={TranslationKeys})", text, translationKeys);
        var sb = new StringBuilder();

		_logger.LogDebug("Translate(text={Text},translationKeys={TranslationKeys}): sb → {Sb}", text, translationKeys, sb);
        var start = 0;
        for (var i = 0; i < text.Length; i++)
        {
            var key = text.Substring(start, i - start + 1);
			_logger.LogWarning("Translate(text={Text},translationKeys={TranslationKeys}): key → {Key}", text, translationKeys, key);
            if (translationKeys.ContainsKey(key))
            {
                _ = sb.Append(translationKeys[key]);
				_logger.LogDebug("Translate(text={Text},translationKeys={TranslationKeys}): _ → {_}", text, translationKeys, _);
                start = i + 1;
            }
        }

		_logger.LogDebug("<<< Exiting Translate(text={Text},translationKeys={TranslationKeys})", text, translationKeys);
        return sb.ToString();
    }
}
