using Microsoft.Extensions.Logging;
namespace Algorithms.Encoders;

/// <summary>
///     Encodes using Feistel cipher.
///     https://en.wikipedia.org/wiki/Feistel_cipher
///     In cryptography, a Feistel cipher (also known as Luby–Rackoff block cipher)
///     is a symmetric structure used in the construction of block ciphers,
///     named after the German-born physicist and cryptographer Horst Feistel
///     who did pioneering research while working for IBM (USA)
///     A large proportion of block ciphers use the scheme, including the US DES,
///     the Soviet/Russian GOST and the more recent Blowfish and Twofish ciphers.
/// </summary>
public class FeistelCipher : IEncoder<uint>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    // number of rounds to transform data block, each round a new "round" key is generated.
    private const int Rounds = 32;

    /// <summary>
    ///     Encodes text using specified key,
    ///     where n - text length.
    /// </summary>
    /// <param name="text">Text to be encoded.</param>
    /// <param name="key">Key that will be used to encode the text.</param>
    /// <exception cref="ArgumentException">Error: key should be more than 0x00001111 for better encoding, key=0 will throw DivideByZero exception.</exception>
    /// <returns>Encoded text.</returns>
    public string Encode(string text, uint key)
    {
		_logger.LogError(">>> Entering Encode(text={Text},key={Key})", text, key);
        List<ulong> blocksListPlain = SplitTextToBlocks(text);
        StringBuilder encodedText = new();
		_logger.LogError("Encode(text={Text},key={Key}): encodedText → {EncodedText}", text, key, encodedText);
		_logger.LogError("Encode(text={Text},key={Key}): blocksListPlain → {BlocksListPlain}", text, key, blocksListPlain);

        foreach (ulong block in blocksListPlain)
        {
            uint temp = 0;

            // decompose a block to two subblocks 0x0123456789ABCDEF => 0x01234567 & 0x89ABCDEF
            uint rightSubblock = (uint)(block & 0x00000000FFFFFFFF);
			_logger.LogError("Encode(text={Text},key={Key}): rightSubblock → {RightSubblock}", text, key, rightSubblock);
            uint leftSubblock = (uint)(block >> 32);
			_logger.LogError("Encode(text={Text},key={Key}): leftSubblock → {LeftSubblock}", text, key, leftSubblock);

            uint roundKey;

            // Feistel "network" itself
            for (int round = 0; round < Rounds; round++)
            {
                roundKey = GetRoundKey(key, round);
				_logger.LogError("Encode(text={Text},key={Key}): roundKey → {RoundKey}", text, key, roundKey);
				_logger.LogDebug("Encode(text={Text},key={Key}): roundKey → {RoundKey}", text, key, roundKey);
                temp = rightSubblock ^ BlockModification(leftSubblock, roundKey);
                rightSubblock = leftSubblock;
                leftSubblock = temp;
            }

            // compile text string formating the block value to text (hex based), length of the output = 16 byte always
            ulong encodedBlock = leftSubblock;
            encodedBlock = (encodedBlock << 32) | rightSubblock;
			_logger.LogError("Encode(text={Text},key={Key}): encodedBlock → {EncodedBlock}", text, key, encodedBlock);
            encodedText.Append(string.Format("{0:X16}", encodedBlock));
        }

		_logger.LogWarning("<<< Exiting Encode(text={Text},key={Key})", text, key);
        return encodedText.ToString();
    }

    /// <summary>
    ///     Decodes text that was encoded using specified key.
    /// </summary>
    /// <param name="text">Text to be decoded.</param>
    /// <param name="key">Key that was used to encode the text.</param>
    /// <exception cref="ArgumentException">Error: key should be more than 0x00001111 for better encoding, key=0 will throw DivideByZero exception.</exception>
    /// <exception cref="ArgumentException">Error: The length of text should be divisible by 16 as it the block lenght is 16 bytes.</exception>
    /// <returns>Decoded text.</returns>
    public string Decode(string text, uint key)
    {
		_logger.LogError(">>> Entering Decode(text={Text},key={Key})", text, key);
        // The plain text will be padded to fill the size of block (16 bytes)
        if (text.Length % 16 != 0)
        {
			_logger.LogError("Throwing ArgumentException from Decode(text={Text},key={Key})", text, key);
            throw new ArgumentException($"The length of {nameof(key)} should be divisible by 16");
        }

        List<ulong> blocksListEncoded = GetBlocksFromEncodedText(text);
		_logger.LogError("Decode(text={Text},key={Key}): blocksListEncoded → {BlocksListEncoded}", text, key, blocksListEncoded);
        StringBuilder decodedTextHex = new();
		_logger.LogError("Decode(text={Text},key={Key}): decodedTextHex → {DecodedTextHex}", text, key, decodedTextHex);

        foreach (ulong block in blocksListEncoded)
        {
            uint temp = 0;

            // decompose a block to two subblocks 0x0123456789ABCDEF => 0x01234567 & 0x89ABCDEF
            uint rightSubblock = (uint)(block & 0x00000000FFFFFFFF);
			_logger.LogError("Decode(text={Text},key={Key}): rightSubblock → {RightSubblock}", text, key, rightSubblock);
            uint leftSubblock = (uint)(block >> 32);
			_logger.LogError("Decode(text={Text},key={Key}): leftSubblock → {LeftSubblock}", text, key, leftSubblock);

            // Feistel "network" - decoding, the order of rounds and operations on the blocks is reverted
            uint roundKey;
            for (int round = Rounds - 1; round >= 0; round--)
            {
                roundKey = GetRoundKey(key, round);
				_logger.LogError("Decode(text={Text},key={Key}): roundKey → {RoundKey}", text, key, roundKey);
				_logger.LogDebug("Decode(text={Text},key={Key}): roundKey → {RoundKey}", text, key, roundKey);
                temp = leftSubblock ^ BlockModification(rightSubblock, roundKey);
                leftSubblock = rightSubblock;
                rightSubblock = temp;
            }

            // compose decoded block
            ulong decodedBlock = leftSubblock;
            decodedBlock = (decodedBlock << 32) | rightSubblock;
			_logger.LogError("Decode(text={Text},key={Key}): decodedBlock → {DecodedBlock}", text, key, decodedBlock);

            for (int i = 0; i < 8; i++)
            {
                ulong a = (decodedBlock & 0xFF00000000000000) >> 56;

                // it's a trick, the code works with non zero characters, if your text has ASCII code 0x00 it will be skipped.
                if (a != 0)
                {
                    decodedTextHex.Append((char)a);
                }

                decodedBlock = decodedBlock << 8;
            }
        }

		_logger.LogWarning("<<< Exiting Decode(text={Text},key={Key})", text, key);
        return decodedTextHex.ToString();
    }

    // Using the size of block = 8 bytes this function splts the text and returns set of 8 bytes (ulong) blocks
    // the last block is extended up to 8 bytes if the tail of the text is smaller than 8 bytes
    private static List<ulong> SplitTextToBlocks(string text)
    {
		_logger.LogError(">>> Entering SplitTextToBlocks(text={Text})", text);
        List<ulong> blocksListPlain = [];
        byte[] textArray = Encoding.ASCII.GetBytes(text);
        int offset = 8;
		_logger.LogError("SplitTextToBlocks(text={Text}): offset → {Offset}", text, offset);
        for (int i = 0; i < text.Length; i += 8)
        {
            // text not always has len%16 == 0, that's why the offset should be adjusted for the last part of the text
            if (i > text.Length - 8)
            {
                offset = text.Length - i;
            }

            string block = Convert.ToHexString(textArray, i, offset);
			_logger.LogError("SplitTextToBlocks(text={Text}): block → {Block}", text, block);
            blocksListPlain.Add(Convert.ToUInt64(block, 16));
        }

		_logger.LogWarning("<<< Exiting SplitTextToBlocks(text={Text})", text);
        return blocksListPlain;
    }

    // convert the encoded text to the set of ulong values (blocks for decoding)
    private static List<ulong> GetBlocksFromEncodedText(string text)
    {
		_logger.LogError(">>> Entering GetBlocksFromEncodedText(text={Text})", text);
        List<ulong> blocksListPlain = [];
        for (int i = 0; i < text.Length; i += 16)
        {
            ulong block = Convert.ToUInt64(text.Substring(i, 16), 16);
			_logger.LogError("GetBlocksFromEncodedText(text={Text}): block → {Block}", text, block);
            blocksListPlain.Add(block);
        }

		_logger.LogWarning("<<< Exiting GetBlocksFromEncodedText(text={Text})", text);
        return blocksListPlain;
    }

    // here might be any deterministic math formula
    private static uint BlockModification(uint block, uint key)
    {
		_logger.LogError(">>> Entering BlockModification(block={Block},key={Key})", block, key);
        for (int i = 0; i < 32; i++)
        {
            // 0x55555555 for the better distribution 0 an 1 in the block
            block = ((block ^ 0x55555555) * block) % key;
            block = block ^ key;
			_logger.LogError("BlockModification(block={Block},key={Key}): block → {Block1}", block, key, block);
        }

		_logger.LogWarning("<<< Exiting BlockModification(block={Block},key={Key})", block, key);
        return block;
    }

    // There are many ways to generate a round key, any deterministic math formula does work
    private static uint GetRoundKey(uint key, int round)
    {
		_logger.LogError(">>> Entering GetRoundKey(key={Key},round={Round})", key, round);
        // "round + 2" - to avoid a situation when pow(key,1) ^ key  = key ^ key = 0
        uint a = (uint)Math.Pow((double)key, round + 2);
		_logger.LogWarning("<<< Exiting GetRoundKey(key={Key},round={Round})", key, round);
        return a ^ key;
    }
}
