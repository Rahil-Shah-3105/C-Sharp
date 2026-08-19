using Microsoft.Extensions.Logging;
namespace Algorithms.Encoders;

/// <summary>
///     Encodes using caesar cypher.
/// </summary>
public class CaesarEncoder : IEncoder<int>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Encodes text using specified key,
    ///     time complexity: O(n),
    ///     space complexity: O(n),
    ///     where n - text length.
    /// </summary>
    /// <param name="text">Text to be encoded.</param>
    /// <param name="key">Key that will be used to encode the text.</param>
    /// <returns>Encoded text.</returns>
    public string Encode(string text, int key) => Cipher(text, key);

    /// <summary>
    ///     Decodes text that was encoded using specified key,
    ///     time complexity: O(n),
    ///     space complexity: O(n),
    ///     where n - text length.
    /// </summary>
    /// <param name="text">Text to be decoded.</param>
    /// <param name="key">Key that was used to encode the text.</param>
    /// <returns>Decoded text.</returns>
    public string Decode(string text, int key) => Cipher(text, -key);

    private static string Cipher(string text, int key)
    {
		_logger.LogDebug(">>> Entering Cipher(text={Text},key={Key})", text, key);
        var newText = new StringBuilder(text.Length);
        for (var i = 0; i < text.Length; i++)
		_logger.LogDebug("Cipher(text={Text},key={Key}): newText → {NewText}", text, key, newText);
        {
            if (!char.IsLetter(text[i]))
            {
                _ = newText.Append(text[i]);
				_logger.LogDebug("Cipher(text={Text},key={Key}): _ → {_}", text, key, _);
                continue;
            }

            var letterA = char.IsUpper(text[i]) ? 'A' : 'a';
			_logger.LogDebug("Cipher(text={Text},key={Key}): letterA → {LetterA}", text, key, letterA);
            var letterZ = char.IsUpper(text[i]) ? 'Z' : 'z';
			_logger.LogDebug("Cipher(text={Text},key={Key}): letterZ → {LetterZ}", text, key, letterZ);

            var c = text[i] + key;
            c -= c > letterZ ? 26 * (1 + (c - letterZ - 1) / 26) : 0;
            c += c < letterA ? 26 * (1 + (letterA - c - 1) / 26) : 0;

            _ = newText.Append((char)c);
        }

		_logger.LogDebug("<<< Exiting Cipher(text={Text},key={Key})", text, key);
        return newText.ToString();
    }
}
