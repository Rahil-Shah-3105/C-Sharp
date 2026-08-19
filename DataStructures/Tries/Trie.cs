using Microsoft.Extensions.Logging;
namespace DataStructures.Tries;

/// <summary>
/// A Trie is a data structure (particular case of m-ary tree) used to efficiently represent strings with common prefixes.
/// Originally posed by E. Fredkin in 1960.
///     Fredkin, Edward (Sept. 1960), "Trie Memory", Communications of the ACM 3 (9): 490-499.
/// Its name is due to retrieval because its main application is in the field of "Information Retrieval" (information retrieval).
/// </summary>
public class Trie
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// This character marks the end of a string.
    /// </summary>
    private const char Mark = '$';

    /// <summary>
    /// This property represents the root node of the trie.
    /// </summary>
    private readonly TrieNode root;

    /// <summary>
    /// Initializes a new instance of the <see cref="Trie"/> class. This instances was created without text strings, generating the root node of the trie, without children.
    /// </summary>
    public Trie()
    {
		_logger.LogDebug(">>> Entering Trie()");
        root = new TrieNode(Mark);
		_logger.LogWarning("Trie(): root → {Root}", root);
		_logger.LogDebug("<<< Exiting Trie()");
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Trie"/> class. Given a set of text strings, each of those strings inserts them into the trie using the Insert (string) method.
    /// </summary>
    /// <param name="words">The array with text strings to insert in the trie.</param>
    public Trie(IEnumerable<string> words)
        : this()
    {
        foreach (string s in words)
        {
            Insert(s);
        }
    }

    /// <summary>
    /// Insert a string s to the trie. The $ mark is added to the end of the chain and then it is added, this in order to indicate the end of the chain in the trie.
    /// </summary>
    /// <param name="s">The string to insert into the trie.</param>
    public void Insert(string s)
    {
		_logger.LogDebug(">>> Entering Insert(s={S})", s);
        s += Mark;

        int index = 0;
        TrieNode match = PrefixQuery(s, ref index);
		_logger.LogDebug("Insert(s={S}): match → {Match}", s, match);

        for (int i = index; i < s.Length; i++)
        {
            TrieNode t = new(s[i], match);
			_logger.LogDebug("Insert(s={S}): t → {T}", s, t);
            match[s[i]] = t;
            match = t;
        }
		_logger.LogDebug("<<< Exiting Insert(s={S})", s);
    }

    /// <summary>
    /// Remove a text string from the trie.
    /// </summary>
    /// <param name="s">The text string to be removed from the trie.</param>
    public void Remove(string s)
    {
		_logger.LogDebug(">>> Entering Remove(s={S})", s);
        s += Mark;
        int index = 0;
        TrieNode match = PrefixQuery(s, ref index);
		_logger.LogDebug("Remove(s={S}): match → {Match}", s, match);
        while (match.IsLeaf())
        {
            char c = match.Value;
            if (match.Parent == null)
            {
                break;
            }

            match = match.Parent;
            match.Children.Remove(c);
        }
		_logger.LogDebug("<<< Exiting Remove(s={S})", s);
    }

    /// <summary>
    /// Know if a text string is in the trie.
    /// </summary>
    /// <param name="s">The string s that you want to know if it is in the trie.</param>
    /// <returns>If the string is found, it returns true, otherwise false.</returns>
    public bool Find(string s)
    {
		_logger.LogDebug(">>> Entering Find(s={S})", s);
        int index = 0;
		_logger.LogDebug("<<< Exiting Find(s={S})", s);
        return PrefixQuery(s + Mark, ref index).IsLeaf();
    }

    /// <summary>
    /// This method analyzes which is the longest common prefix of a string s in the trie. If the string is in the trie then it is equivalent to doing Find (s).
    /// </summary>
    /// <param name="s">The string for which you want to know the longest common prefix.</param>
    /// <param name="index">The index to which the longest common prefix goes.</param>
    /// <returns>
    /// Returns the longest common prefix node found in the trie with the string s.
    /// </returns>
    private TrieNode PrefixQuery(string s, ref int index)
    {
		_logger.LogDebug(">>> Entering PrefixQuery(s={S},index={Index})", s, index);
        TrieNode current = root;
        for (int i = 0; i < s.Length && current != null; i++)
		_logger.LogDebug("PrefixQuery(s={S},index={Index}): current → {Current}", s, index, current);
        {
            if (current[s[i]] != null)
            {
                current = current[s[i]] ?? throw new NullReferenceException();
                index = i + 1;
            }
            else
            {
                break;
            }
        }

		_logger.LogDebug("<<< Exiting PrefixQuery(s={S},index={Index})", s, index);
        return current ?? throw new NullReferenceException();
    }
}
