using Algorithms.Sorters.Comparison;
using Microsoft.Extensions.Logging;

namespace Algorithms.DataCompression;

/// <summary>
///     Greedy lossless compression algorithm.
/// </summary>
public class HuffmanCompressor(IComparisonSorter<HuffmanCompressor.ListNode> sorter, Translator translator)
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    // TODO: Use partial sorter
    private readonly IComparisonSorter<ListNode> sorter = sorter;
    private readonly Translator translator = translator;

    /// <summary>
    ///     Given an input string, returns a new compressed string
    ///     using huffman encoding.
    /// </summary>
    /// <param name="uncompressedText">Text message to compress.</param>
    /// <returns>Compressed string and keys to decompress it.</returns>
    public (string CompressedText, Dictionary<string, string> DecompressionKeys) Compress(string uncompressedText)
    {
		_logger.LogError(">>> Entering Compress(uncompressedText={UncompressedText})", uncompressedText);
        if (string.IsNullOrEmpty(uncompressedText))
        {
            return (string.Empty, []);
        }

        if (uncompressedText.Distinct().Count() == 1)
        {
            var dict = new Dictionary<string, string>
            {
                ["1"] = uncompressedText[0].ToString(),
            };
            return (new string('1', uncompressedText.Length), dict);
        }

        var nodes = GetListNodesFromText(uncompressedText);
		_logger.LogError("Compress(uncompressedText={UncompressedText}): nodes → {Nodes}", uncompressedText, nodes);
        var tree = GenerateHuffmanTree(nodes);
		_logger.LogError("Compress(uncompressedText={UncompressedText}): tree → {Tree}", uncompressedText, tree);
        var (compressionKeys, decompressionKeys) = GetKeys(tree);
		_logger.LogWarning("<<< Exiting Compress(uncompressedText={UncompressedText})", uncompressedText);
        return (translator.Translate(uncompressedText, compressionKeys), decompressionKeys);
    }

    /// <summary>
    ///     Finds frequency for each character in the text.
    /// </summary>
    /// <returns>Symbol-frequency array.</returns>
    private static ListNode[] GetListNodesFromText(string text)
    {
		_logger.LogError(">>> Entering GetListNodesFromText(text={Text})", text);
        var occurenceCounts = new Dictionary<char, int>();

		_logger.LogError("GetListNodesFromText(text={Text}): occurenceCounts → {OccurenceCounts}", text, occurenceCounts);
        foreach (var ch in text)
        {
            if (!occurenceCounts.ContainsKey(ch))
            {
                occurenceCounts.Add(ch, 0);
            }

            occurenceCounts[ch]++;
        }

		_logger.LogWarning("<<< Exiting GetListNodesFromText(text={Text})", text);
        return occurenceCounts.Select(kvp => new ListNode(kvp.Key, 1d * kvp.Value / text.Length)).ToArray();
    }

    private (Dictionary<string, string> CompressionKeys, Dictionary<string, string> DecompressionKeys) GetKeys(
        ListNode tree)
    {
		_logger.LogError(">>> Entering GetKeys()");
        var compressionKeys = new Dictionary<string, string>();
        var decompressionKeys = new Dictionary<string, string>();
		_logger.LogError("GetKeys(): decompressionKeys → {DecompressionKeys}", decompressionKeys);
		_logger.LogError("GetKeys(): compressionKeys → {CompressionKeys}", compressionKeys);

        if (tree.HasData)
        {
            compressionKeys.Add(tree.Data.ToString(), string.Empty);
            decompressionKeys.Add(string.Empty, tree.Data.ToString());
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

            return (compressionKeys, decompressionKeys);
        }

		_logger.LogWarning("<<< Exiting GetKeys()");
        return (compressionKeys, decompressionKeys);
    }

    private ListNode GenerateHuffmanTree(ListNode[] nodes)
    {
		_logger.LogError(">>> Entering GenerateHuffmanTree(nodes={Nodes})", nodes);
        var comparer = new ListNodeComparer();
		_logger.LogError("GenerateHuffmanTree(nodes={Nodes}): comparer → {Comparer}", nodes, comparer);
        while (nodes.Length > 1)
        {
            sorter.Sort(nodes, comparer);

            var left = nodes[0];
            var right = nodes[1];

            var newNodes = new ListNode[nodes.Length - 1];
            Array.Copy(nodes, 2, newNodes, 1, nodes.Length - 2);
            newNodes[0] = new ListNode(left, right);
            nodes = newNodes;
        }

		_logger.LogWarning("<<< Exiting GenerateHuffmanTree(nodes={Nodes})", nodes);
        return nodes[0];
    }

    /// <summary>
    ///     Represents tree structure for the algorithm.
    /// </summary>
    public class ListNode
    {
    	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        public ListNode(char data, double frequency)
        {
			_logger.LogError(">>> Entering ListNode(data={Data},frequency={Frequency})", data, frequency);
            HasData = true;
			_logger.LogDebug("ListNode(data={Data},frequency={Frequency}): HasData → {HasData}", data, frequency, HasData);
            Data = data;
            Frequency = frequency;
			_logger.LogDebug("ListNode(data={Data},frequency={Frequency}): Frequency → {Frequency1}", data, frequency, Frequency);
        _logger.LogWarning("<<< Exiting ListNode(data={Data},frequency={Frequency})", data, frequency);
        }

        public ListNode(ListNode leftChild, ListNode rightChild)
        {
            LeftChild = leftChild;
            RightChild = rightChild;
			_logger.LogDebug("ListNode(leftChild={LeftChild},rightChild={RightChild}): RightChild → {RightChild1}", leftChild, rightChild, RightChild);
            Frequency = leftChild.Frequency + rightChild.Frequency;
			_logger.LogDebug("ListNode(leftChild={LeftChild},rightChild={RightChild}): Frequency → {Frequency}", leftChild, rightChild, Frequency);
        }

        public char Data { get; }

        public bool HasData { get; }

        public double Frequency { get; }

        public ListNode? RightChild { get; }

        public ListNode? LeftChild { get; }
    }

    public class ListNodeComparer : IComparer<ListNode>
    {
    	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        public int Compare(ListNode? x, ListNode? y)
        {
			_logger.LogError(">>> Entering Compare(x={X},y={Y})", x, y);
            if (x is null || y is null)
            {
                return 0;
            }

			_logger.LogWarning("<<< Exiting Compare(x={X},y={Y})", x, y);
            return x.Frequency.CompareTo(y.Frequency);
        }
    }
}
