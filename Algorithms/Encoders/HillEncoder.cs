using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Encoders;

/// <summary>
///     Lester S. Hill's polygraphic substitution cipher,
///     without representing letters using mod26, using
///     corresponding "(char)value" instead.
/// </summary>
public class HillEncoder : IEncoder<double[,]>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly GaussJordanElimination linearEquationSolver;

    public HillEncoder() => linearEquationSolver = new GaussJordanElimination(); // TODO: add DI

    public string Encode(string text, double[,] key)
    {
		_logger.LogError(">>> Entering Encode(text={Text},key={Key})", text, key);
        var preparedText = FillGaps(text);
        var chunked = ChunkTextToArray(preparedText);
		_logger.LogError("Encode(text={Text},key={Key}): chunked → {Chunked}", text, key, chunked);
        var splitted = SplitToCharArray(chunked);
		_logger.LogError("Encode(text={Text},key={Key}): splitted → {Splitted}", text, key, splitted);
		_logger.LogError("Encode(text={Text},key={Key}): preparedText → {PreparedText}", text, key, preparedText);

        var ciphered = new double[chunked.Length][];

        for (var i = 0; i < chunked.Length; i++)
        {
            var vector = new double[3];
            Array.Copy(splitted, i * 3, vector, 0, 3);
            var product = MatrixCipher(vector, key);
			_logger.LogError("Encode(text={Text},key={Key}): product → {Product}", text, key, product);
            ciphered[i] = product;
        }

        var merged = MergeArrayList(ciphered);
		_logger.LogError("Encode(text={Text},key={Key}): merged → {Merged}", text, key, merged);

		_logger.LogWarning("<<< Exiting Encode(text={Text},key={Key})", text, key);
        return BuildStringFromArray(merged);
    }

    public string Decode(string text, double[,] key)
    {
		_logger.LogError(">>> Entering Decode(text={Text},key={Key})", text, key);
        var chunked = ChunkTextToArray(text);
        var split = SplitToCharArray(chunked);
		_logger.LogError("Decode(text={Text},key={Key}): split → {Split}", text, key, split);
		_logger.LogError("Decode(text={Text},key={Key}): chunked → {Chunked}", text, key, chunked);

        var deciphered = new double[chunked.Length][];

        for (var i = 0; i < chunked.Length; i++)
        {
            var vector = new double[3];
            Array.Copy(split, i * 3, vector, 0, 3);
            var product = MatrixDeCipher(vector, key);
			_logger.LogError("Decode(text={Text},key={Key}): product → {Product}", text, key, product);
            deciphered[i] = product;
        }

        var merged = MergeArrayList(deciphered);
		_logger.LogError("Decode(text={Text},key={Key}): merged → {Merged}", text, key, merged);
        var str = BuildStringFromArray(merged);
		_logger.LogError("Decode(text={Text},key={Key}): str → {Str}", text, key, str);

		_logger.LogWarning("<<< Exiting Decode(text={Text},key={Key})", text, key);
        return UnFillGaps(str);
    }

    /// <summary>
    ///     Converts elements from the array to their corresponding Unicode characters.
    /// </summary>
    /// <param name="arr">array of vectors.</param>
    /// <returns>Message.</returns>
    private static string BuildStringFromArray(double[] arr)
    {
        _logger.LogError(">>> Entering BuildStringFromArray(arr={Arr})", arr);
        _logger.LogWarning("<<< Exiting BuildStringFromArray(arr={Arr})", arr);
        return new(arr.Select(c => (char)c).ToArray());
    }

    /// <summary>
    ///     Multiplies the key for the given scalar.
    /// </summary>
    /// <param name="vector">list of splitted words as numbers.</param>
    /// <param name="key">Cipher selected key.</param>
    /// <returns>Ciphered vector.</returns>
    private static double[] MatrixCipher(double[] vector, double[,] key)
    {
		_logger.LogError(">>> Entering MatrixCipher(vector={Vector},key={Key})", vector, key);
        var multiplied = new double[vector.Length];

        for (var i = 0; i < key.GetLength(1); i++)
        {
            for (var j = 0; j < key.GetLength(0); j++)
            {
                multiplied[i] += key[i, j] * vector[j];
            }
        }

		_logger.LogWarning("<<< Exiting MatrixCipher(vector={Vector},key={Key})", vector, key);
        return multiplied;
    }

    /// <summary>
    ///     Given a list of vectors, returns a single array of elements.
    /// </summary>
    /// <param name="list">List of ciphered arrays.</param>
    /// <returns>unidimensional list.</returns>
    private static double[] MergeArrayList(double[][] list)
    {
		_logger.LogError(">>> Entering MergeArrayList(list={List})", list);
        var merged = new double[list.Length * 3];

        for (var i = 0; i < list.Length; i++)
        {
            Array.Copy(list[i], 0, merged, i * 3, list[0].Length);
        }

		_logger.LogWarning("<<< Exiting MergeArrayList(list={List})", list);
        return merged;
    }

    /// <summary>
    ///     Splits the input text message as chunks of words.
    /// </summary>
    /// <param name="chunked">chunked words list.</param>
    /// <returns>spliiter char array.</returns>
    private static char[] SplitToCharArray(string[] chunked)
    {
		_logger.LogError(">>> Entering SplitToCharArray(chunked={Chunked})", chunked);
        var splitted = new char[chunked.Length * 3];

        for (var i = 0; i < chunked.Length; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                splitted[i * 3 + j] = chunked[i].ToCharArray()[j];
            }
        }

		_logger.LogWarning("<<< Exiting SplitToCharArray(chunked={Chunked})", chunked);
        return splitted;
    }

    /// <summary>
    ///     Chunks the input text message.
    /// </summary>
    /// <param name="text">text message.</param>
    /// <returns>array of words.</returns>
    private static string[] ChunkTextToArray(string text)
    {
		_logger.LogError(">>> Entering ChunkTextToArray(text={Text})", text);
        // To split the message into chunks
        var div = text.Length / 3;
        var chunks = new string[div];

        for (var i = 0; i < div; i++)
        {
            chunks.SetValue(text.Substring(i * 3, 3), i);
        }

		_logger.LogWarning("<<< Exiting ChunkTextToArray(text={Text})", text);
        return chunks;
    }

    /// <summary>
    ///     Fills a text message with spaces at the end
    ///     to enable a simple split by 3-length-word.
    /// </summary>
    /// <param name="text">Text Message.</param>
    /// <returns>Modified text Message.</returns>
    private static string FillGaps(string text)
    {
		_logger.LogError(">>> Entering FillGaps(text={Text})", text);
        var remainder = text.Length % 3;
		_logger.LogError("FillGaps(text={Text}): remainder → {Remainder}", text, remainder);
		_logger.LogWarning("<<< Exiting FillGaps(text={Text})", text);
        return remainder == 0 ? text : text + new string(' ', 3 - remainder);
    }

    /// <summary>
    ///     Removes the extra spaces included on the cipher phase.
    /// </summary>
    /// <param name="text">Text message.</param>
    /// <returns>Deciphered Message.</returns>
    private static string UnFillGaps(string text)
    {
        _logger.LogError(">>> Entering UnFillGaps(text={Text})", text);
        _logger.LogWarning("<<< Exiting UnFillGaps(text={Text})", text);
        return text.TrimEnd();
    }

    /// <summary>
    ///     Finds the inverse of the given matrix using a linear equation solver.
    /// </summary>
    /// <param name="vector">Splitted words vector.</param>
    /// <param name="key">Key used for the cipher.</param>
    /// <returns>TODO.</returns>
    private double[] MatrixDeCipher(double[] vector, double[,] key)
    {
		_logger.LogError(">>> Entering MatrixDeCipher(vector={Vector},key={Key})", vector, key);
        // To augment the original key with the given vector.
        var augM = new double[3, 4];

        for (var i = 0; i < key.GetLength(0); i++)
        {
            for (var j = 0; j < key.GetLength(1); j++)
            {
                augM[i, j] = key[i, j];
            }
        }

        for (var k = 0; k < vector.Length; k++)
        {
            augM[k, 3] = vector[k];
        }

        _ = linearEquationSolver.Solve(augM);

		_logger.LogWarning("<<< Exiting MatrixDeCipher(vector={Vector},key={Key})", vector, key);
        return [augM[0, 3], augM[1, 3], augM[2, 3]];
    }
}
