using Microsoft.Extensions.Logging;
namespace Algorithms.Encoders
{
    /// <summary>
    ///     Class for AutoKey encoding strings.
    /// </summary>
    public class AutokeyEncorder
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        /// <summary>
        ///     Autokey Cipher is a type of polyalphabetic cipher.
        ///     This works by choosing a key (a word or short phrase),
        ///     then you append the plaintext to itself to form a longer key.
        /// </summary>
        /// <param name="plainText">The string to be appended to the key.</param>
        /// <param name="keyword">The string to be appended to the plaintext.</param>
        /// <returns>The Autokey encoded string (All Uppercase).</returns>
        public string Encode(string plainText, string keyword)
        {
			_logger.LogDebug(">>> Entering Encode(plainText={PlainText},keyword={Keyword})", plainText, keyword);
            plainText = Regex.Replace(plainText.ToUpper(CultureInfo.InvariantCulture), "[^A-Z]", string.Empty);
            keyword = keyword.ToUpper(CultureInfo.InvariantCulture);
			_logger.LogDebug("Encode(plainText={PlainText},keyword={Keyword}): keyword → {Keyword1}", plainText, keyword, keyword);
			_logger.LogDebug("Encode(plainText={PlainText},keyword={Keyword}): plainText → {PlainText1}", plainText, keyword, plainText);

            keyword += plainText;

            StringBuilder cipherText = new StringBuilder();
			_logger.LogDebug("Encode(plainText={PlainText},keyword={Keyword}): cipherText → {CipherText}", plainText, keyword, cipherText);

            for (int i = 0; i < plainText.Length; i++)
            {
                char plainCharacter = plainText[i];
                char keyCharacter = keyword[i];

                int encryptedCharacter = (plainCharacter - 'A' + keyCharacter - 'A') % 26 + 'A';
				_logger.LogDebug("Encode(plainText={PlainText},keyword={Keyword}): encryptedCharacter → {EncryptedCharacter}", plainText, keyword, encryptedCharacter);
                cipherText.Append((char)encryptedCharacter);
            }

			_logger.LogDebug("<<< Exiting Encode(plainText={PlainText},keyword={Keyword})", plainText, keyword);
            return cipherText.ToString();
        }

        /// <summary>
        ///     Removed the key from the encoded string.
        /// </summary>
        /// <param name="cipherText">The encoded string.</param>
        /// <param name="keyword">The key to be removed from the encoded string.</param>
        /// <returns>The plaintext (All Uppercase).</returns>
        public string Decode(string cipherText, string keyword)
        {
			_logger.LogDebug(">>> Entering Decode(cipherText={CipherText},keyword={Keyword})", cipherText, keyword);
            cipherText = Regex.Replace(cipherText.ToUpper(CultureInfo.InvariantCulture), "[^A-Z]", string.Empty);
            keyword = keyword.ToUpper(CultureInfo.InvariantCulture);
			_logger.LogDebug("Decode(cipherText={CipherText},keyword={Keyword}): keyword → {Keyword1}", cipherText, keyword, keyword);
			_logger.LogDebug("Decode(cipherText={CipherText},keyword={Keyword}): cipherText → {CipherText1}", cipherText, keyword, cipherText);

            StringBuilder plainText = new StringBuilder();
			_logger.LogDebug("Decode(cipherText={CipherText},keyword={Keyword}): plainText → {PlainText}", cipherText, keyword, plainText);
            StringBuilder extendedKeyword = new StringBuilder(keyword);
			_logger.LogDebug("Decode(cipherText={CipherText},keyword={Keyword}): extendedKeyword → {ExtendedKeyword}", cipherText, keyword, extendedKeyword);

            for (int i = 0; i < cipherText.Length; i++)
            {
                char cipherCharacter = cipherText[i];
                char keywordCharacter = extendedKeyword[i];

                int decryptedCharacter = (cipherCharacter - 'A' - (keywordCharacter - 'A') + 26) % 26 + 'A';
				_logger.LogDebug("Decode(cipherText={CipherText},keyword={Keyword}): decryptedCharacter → {DecryptedCharacter}", cipherText, keyword, decryptedCharacter);
                plainText.Append((char)decryptedCharacter);
                extendedKeyword.Append((char)decryptedCharacter);
            }

			_logger.LogDebug("<<< Exiting Decode(cipherText={CipherText},keyword={Keyword})", cipherText, keyword);
            return plainText.ToString();
        }
    }
}
