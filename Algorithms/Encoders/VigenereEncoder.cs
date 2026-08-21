using Microsoft.Extensions.Logging;
namespace Algorithms.Encoders;

/// <summary>
///     Encodes using vigenere cypher.
/// </summary>
public class VigenereEncoder : IEncoder<string>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly CaesarEncoder caesarEncoder = new();

    /// <summary>
    ///     Encodes text using specified key,
    ///     time complexity: O(n),
    ///     space complexity: O(n),
    ///     where n - text length.
    /// </summary>
    /// <param name="text">Text to be encoded.</param>
    /// <param name="key">Key that will be used to encode the text.</param>
    /// <returns>Encoded text.</returns>
    public string Encode(string text, string key) => Cipher(text, key, caesarEncoder.Encode);

    /// <summary>
    ///     Decodes text that was encoded using specified key,
    ///     time complexity: O(n),
    ///     space complexity: O(n),
    ///     where n - text length.
    /// </summary>
    /// <param name="text">Text to be decoded.</param>
    /// <param name="key">Key that was used to encode the text.</param>
    /// <returns>Decoded text.</returns>
    public string Decode(string text, string key) => Cipher(text, key, caesarEncoder.Decode);

    private string Cipher(string text, string key, Func<string, int, string> symbolCipher)
    {
		_logger.LogDebug(">>> Entering Cipher(text={Text},key={Key},symbolCipher={SymbolCipher})", text, key, symbolCipher);
        key = AppendKey(key, text.Length);
        var encodedTextBuilder = new StringBuilder(text.Length);
		_logger.LogDebug("Cipher(text={Text},key={Key},symbolCipher={SymbolCipher}): encodedTextBuilder → {EncodedTextBuilder}", text, key, symbolCipher, encodedTextBuilder);
		_logger.LogWarning("Cipher(text={Text},key={Key},symbolCipher={SymbolCipher}): key → {Key1}", text, key, symbolCipher, key);
        for (var i = 0; i < text.Length; i++)
        {
            if (!char.IsLetter(text[i]))
            {
                _ = encodedTextBuilder.Append(text[i]);
				_logger.LogDebug("Cipher(text={Text},key={Key},symbolCipher={SymbolCipher}): _ → {_}", text, key, symbolCipher, _);
                continue;
            }

            var letterZ = char.IsUpper(key[i]) ? 'Z' : 'z';
			_logger.LogDebug("Cipher(text={Text},key={Key},symbolCipher={SymbolCipher}): letterZ → {LetterZ}", text, key, symbolCipher, letterZ);
            var encodedSymbol = symbolCipher(text[i].ToString(), letterZ - key[i]);
			_logger.LogDebug("Cipher(text={Text},key={Key},symbolCipher={SymbolCipher}): encodedSymbol → {EncodedSymbol}", text, key, symbolCipher, encodedSymbol);
            _ = encodedTextBuilder.Append(encodedSymbol);
        }

		_logger.LogDebug("<<< Exiting Cipher(text={Text},key={Key},symbolCipher={SymbolCipher})", text, key, symbolCipher);
        return encodedTextBuilder.ToString();
    }

    private string AppendKey(string key, int length)
    {
		_logger.LogDebug(">>> Entering AppendKey(key={Key},length={Length})", key, length);
        if (string.IsNullOrEmpty(key))
        {
			_logger.LogWarning("Throwing ArgumentOutOfRangeException from AppendKey(key={Key},length={Length})", key, length);
            throw new ArgumentOutOfRangeException($"{nameof(key)} must be non-empty string");
        }

        var keyBuilder = new StringBuilder(key, length);
		_logger.LogDebug("AppendKey(key={Key},length={Length}): keyBuilder → {KeyBuilder}", key, length, keyBuilder);
        while (keyBuilder.Length < length)
        {
            _ = keyBuilder.Append(key);
			_logger.LogDebug("AppendKey(key={Key},length={Length}): _ → {_}", key, length, _);
        }

		_logger.LogDebug("<<< Exiting AppendKey(key={Key},length={Length})", key, length);
        return keyBuilder.ToString();
    }
}
