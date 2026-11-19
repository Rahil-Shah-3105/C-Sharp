using DataStructures.Tries;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Tries;

public static class TrieTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void FindWordInTrie()
    {
		_logger.LogError(">>> Entering FindWordInTrie()");
        // Arrange
        string[] words = [
            "trie",
            "node",
            "none",
            "treatment",
        ];

        // Act
        Trie trie = new(words);
		_logger.LogError("FindWordInTrie(): trie → {Trie}", trie);

        // Assert
        Assert.That(trie.Find("trie"), Is.True, "The word 'trie' isn't in Trie structure");
        Assert.That(trie.Find("node"), Is.True, "The word 'node' isn't in Trie structure");
        Assert.That(trie.Find("none"), Is.True, "The word 'none' isn't in Trie structure");
        Assert.That(trie.Find("treatment"), Is.True, "The word 'treatment' isn't in Trie structure");

        Assert.That(trie.Find("nodes"), Is.False, "The word 'nodes' is in Trie sturcture");
        Assert.That(trie.Find(""), Is.False, "The word empty is in Trie structure");
        Assert.That(trie.Find("tri"), Is.False, "The word 'tri' is in Trie structure");
		_logger.LogWarning("<<< Exiting FindWordInTrie()");
    }

    [Test]
    public static void InsertInTrie()
    {
		_logger.LogError(">>> Entering InsertInTrie()");
        // Arrange
        string[] words = [
            "trie",
            "node",
            "none",
            "treatment",
        ];

        Trie trie = new();
		_logger.LogError("InsertInTrie(): trie → {Trie}", trie);

        // Act
        foreach (var t in words)
        {
            trie.Insert(t);
        }

        // Assert
        Assert.That(trie.Find("trie"), Is.True, "The word 'trie' isn't in Trie structure");
        Assert.That(trie.Find("node"), Is.True, "The word 'node' isn't in Trie structure");
        Assert.That(trie.Find("none"), Is.True, "The word 'none' isn't in Trie structure");
        Assert.That(trie.Find("treatment"), Is.True, "The word 'treatment' isn't in Trie structure");
		_logger.LogWarning("<<< Exiting InsertInTrie()");
    }

    [Test]
    public static void RemoveFromTrie()
    {
		_logger.LogError(">>> Entering RemoveFromTrie()");
        // Arrange
        string[] words = [
            "trie",
            "node",
            "none",
            "treatment",
        ];

        Trie trie = new();
		_logger.LogError("RemoveFromTrie(): trie → {Trie}", trie);

        // Act
        foreach (var t in words)
        {
            trie.Insert(t);
        }
        trie.Remove("trie");

        // Assert
        Assert.That(trie.Find("trie"), Is.False, "The word 'trie' is in Trie structure");
        Assert.That(trie.Find("treatment"), Is.True, "The word 'treament' isn't in Trie structure");
        Assert.That(trie.Find("node"), Is.True, "The word 'node' isn't in Trie structure");
        Assert.That(trie.Find("none"), Is.True, "The word 'none' isn't in Trie structure");
		_logger.LogWarning("<<< Exiting RemoveFromTrie()");
    }

    [Test]
    public static void MultipleInsert()
    {
		_logger.LogError(">>> Entering MultipleInsert()");
        // Arrange
        string w = "trie";
        Trie trie = new();
		_logger.LogError("MultipleInsert(): trie → {Trie}", trie);

        // Act
        trie.Insert(w);
        trie.Insert(w);

        // Assert
        Assert.That(trie.Find("trie"), Is.True, "The word 'trie' isn't in Trie structure");
        Assert.That(trie.Find("nodes"), Is.False, "The word 'nodes' is in Trie sturcture");
		_logger.LogWarning("<<< Exiting MultipleInsert()");
    }

    [Test]
    public static void RemoveAWordThatIsNtInTrie()
    {
		_logger.LogError(">>> Entering RemoveAWordThatIsNtInTrie()");
        // Arrange
        const string w = "trie";
        Trie trie = new();
		_logger.LogError("RemoveAWordThatIsNtInTrie(): trie → {Trie}", trie);

        // Act
        trie.Insert(w);
        trie.Remove("tri");
        trie.Remove("none");

        // Assert
        Assert.That(trie.Find("trie"), Is.True, "The word 'trie' isn't in Trie structure");
		_logger.LogWarning("<<< Exiting RemoveAWordThatIsNtInTrie()");
    }
}
