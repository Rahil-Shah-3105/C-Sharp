using Algorithms.Knapsack;
using Microsoft.Extensions.Logging;

namespace Algorithms.DataCompression;

/// <summary>
///     Greedy lossless compression algorithm.
/// </summary>
public class ShannonFanoCompressor(
    IHeuristicKnapsackSolver<(char Symbol, double Frequency)> splitter,
    Translator translator)
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly IHeuristicKnapsackSolver<(char Symbol, double Frequency)> splitter = splitter;
    private readonly Translator translator = translator;

    /// <summary>
    ///     Given an input string, returns a new compressed string
    ///     using Shannon-Fano encoding.
    /// </summary>
    /// <param name="uncompressedText">Text message to compress.</param>
    /// <returns>Compressed string and keys to decompress it.</returns>
    public (string CompressedText, Dictionary<string, string> DecompressionKeys) Compress(string uncompressedText)
    {
		_logger.LogError(">>> Entering Compress(uncompressedText={UncompressedText})", uncompressedText);
        if (string.IsNullOrEmpty(uncompressedText))
        {
            return (string.Empty, new Dictionary<string, string>());
        }

        if (uncompressedText.Distinct().Count() == 1)
        {
            var dict = new Dictionary<string, string>
            {
                { "1", uncompressedText[0].ToString() },
            };
            return (new string('1', uncompressedText.Length), dict);
        }

        var node = GetListNodeFromText(uncompressedText);
		_logger.LogError("Compress(uncompressedText={UncompressedText}): node → {Node}", uncompressedText, node);
        var tree = GenerateShannonFanoTree(node);
		_logger.LogError("Compress(uncompressedText={UncompressedText}): tree → {Tree}", uncompressedText, tree);
        var (compressionKeys, decompressionKeys) = GetKeys(tree);
		_logger.LogWarning("<<< Exiting Compress(uncompressedText={UncompressedText})", uncompressedText);
        return (translator.Translate(uncompressedText, compressionKeys), decompressionKeys);
    }

    private (Dictionary<string, string> CompressionKeys, Dictionary<string, string> DecompressionKeys) GetKeys(
        ListNode tree)
    {
		_logger.LogError(">>> Entering GetKeys()");
        var compressionKeys = new Dictionary<string, string>();
        var decompressionKeys = new Dictionary<string, string>();
		_logger.LogError("GetKeys(): decompressionKeys → {DecompressionKeys}", decompressionKeys);
		_logger.LogError("GetKeys(): compressionKeys → {CompressionKeys}", compressionKeys);

        if (tree.Data.Length == 1)
        {
            compressionKeys.Add(tree.Data[0].Symbol.ToString(), string.Empty);
            decompressionKeys.Add(string.Empty, tree.Data[0].Symbol.ToString());
            return (compressionKeys, decompressionKeys);
        }

        if (tree.LeftChild is not null)
        {
            var (lsck, lsdk) = GetKeys(tree.LeftChild);
            compressionKeys.AddMany(lsck.Select(kvp => (kvp.Key, "0" + kvp.Value)));
            decompressionKeys.AddMany(lsdk.Select(kvp => ("0" + kvp.Key, kvp.Value)));
        }

        if (tree.RightChild is not null)
        {
            var (rsck, rsdk) = GetKeys(tree.RightChild);
            compressionKeys.AddMany(rsck.Select(kvp => (kvp.Key, "1" + kvp.Value)));
            decompressionKeys.AddMany(rsdk.Select(kvp => ("1" + kvp.Key, kvp.Value)));
        }

		_logger.LogWarning("<<< Exiting GetKeys()");
        return (compressionKeys, decompressionKeys);
    }

    private ListNode GenerateShannonFanoTree(ListNode node)
    {
		_logger.LogError(">>> Entering GenerateShannonFanoTree(node={Node})", node);
        if (node.Data.Length == 1)
        {
            return node;
        }

        var left = splitter.Solve(node.Data, 0.5 * node.Data.Sum(x => x.Frequency), x => x.Frequency, _ => 1);
		_logger.LogError("GenerateShannonFanoTree(node={Node}): left → {Left}", node, left);
        var right = node.Data.Except(left).ToArray();
		_logger.LogError("GenerateShannonFanoTree(node={Node}): right → {Right}", node, right);

        node.LeftChild = GenerateShannonFanoTree(new ListNode(left));
        node.RightChild = GenerateShannonFanoTree(new ListNode(right));

		_logger.LogWarning("<<< Exiting GenerateShannonFanoTree(node={Node})", node);
        return node;
    }

    /// <summary>
    ///     Finds frequency for each character in the text.
    /// </summary>
    /// <returns>Symbol-frequency array.</returns>
    private ListNode GetListNodeFromText(string text)
    {
		_logger.LogError(">>> Entering GetListNodeFromText(text={Text})", text);
        var occurenceCounts = new Dictionary<char, double>();

		_logger.LogError("GetListNodeFromText(text={Text}): occurenceCounts → {OccurenceCounts}", text, occurenceCounts);
        for (var i = 0; i < text.Length; i++)
        {
            var ch = text[i];
            if (!occurenceCounts.ContainsKey(ch))
            {
                occurenceCounts.Add(ch, 0);
            }

            occurenceCounts[ch]++;
        }

		_logger.LogWarning("<<< Exiting GetListNodeFromText(text={Text})", text);
        return new ListNode(occurenceCounts.Select(kvp => (kvp.Key, 1d * kvp.Value / text.Length)).ToArray());
    }

    /// <summary>
    ///     Represents tree structure for the algorithm.
    /// </summary>
    public class ListNode((char Symbol, double Frequency)[] data)
    {
        public (char Symbol, double Frequency)[] Data { get; } = data;

        public ListNode? RightChild { get; set; }

        public ListNode? LeftChild { get; set; }
    }
}
