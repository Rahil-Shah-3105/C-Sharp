using DataStructures.RedBlackTree;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests;

internal class RedBlackTreeTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void Constructor_UseCustomComparer_FormsCorrect_Tree()
    {
		_logger.LogError(">>> Entering Constructor_UseCustomComparer_FormsCorrect_Tree()");
        var tree = new RedBlackTree<int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
		_logger.LogError("Constructor_UseCustomComparer_FormsCorrect_Tree(): tree → {Tree}", tree);
        tree.GetMin().Should().Be(10);
        tree.GetMax().Should().Be(1);
        tree.GetKeysInOrder().SequenceEqual(new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }).Should().BeTrue();
		_logger.LogWarning("<<< Exiting Constructor_UseCustomComparer_FormsCorrect_Tree()");
    }

    [Test]
    public void Add_Case3_FormsCorrectTree()
    {
		_logger.LogError(">>> Entering Add_Case3_FormsCorrectTree()");
        var tree = new RedBlackTree<int>();
        tree.Add(5);
		_logger.LogError("Add_Case3_FormsCorrectTree(): tree → {Tree}", tree);
        tree.Count.Should().Be(1);
		_logger.LogWarning("<<< Exiting Add_Case3_FormsCorrectTree()");
    }

    [Test]
    public void Add_Case24_FormsCorrectTree()
    {
		_logger.LogError(">>> Entering Add_Case24_FormsCorrectTree()");
        var tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 5, 4, 6, 3 });
		_logger.LogError("Add_Case24_FormsCorrectTree(): tree → {Tree}", tree);
        tree.GetKeysPreOrder().SequenceEqual(new[] { 5, 4, 3, 6 }).Should().BeTrue();
		_logger.LogWarning("<<< Exiting Add_Case24_FormsCorrectTree()");
    }

    [Test]
    public void Add_Case1_FormsCorrectTree()
    {
		_logger.LogError(">>> Entering Add_Case1_FormsCorrectTree()");
        var tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 5, 4, 6, 3, 7 });
		_logger.LogError("Add_Case1_FormsCorrectTree(): tree → {Tree}", tree);
        tree.GetKeysPreOrder().SequenceEqual(new[] { 5, 4, 3, 6, 7 }).Should().BeTrue();
		_logger.LogWarning("<<< Exiting Add_Case1_FormsCorrectTree()");
    }

    [Test]
    public void Add_Case6_FormsCorrectTree()
    {
		_logger.LogError(">>> Entering Add_Case6_FormsCorrectTree()");
        // Right rotation
        var tree = new RedBlackTree<int>();
		_logger.LogError("Add_Case6_FormsCorrectTree(): tree → {Tree}", tree);
        tree.AddRange(new[] { 5, 4, 6, 3, 2 });
        tree.GetKeysPreOrder().SequenceEqual(new[] { 5, 3, 2, 4, 6 }).Should().BeTrue();

        // Left rotation
        tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 5, 4, 6, 7, 8 });
        tree.GetKeysPreOrder().SequenceEqual(new[] { 5, 4, 7, 6, 8 }).Should().BeTrue();
		_logger.LogWarning("<<< Exiting Add_Case6_FormsCorrectTree()");
    }

    [Test]
    public void Add_Case5_FormsCorrectTree()
    {
		_logger.LogError(">>> Entering Add_Case5_FormsCorrectTree()");
        // Left-right rotation
        var tree = new RedBlackTree<int>();
		_logger.LogError("Add_Case5_FormsCorrectTree(): tree → {Tree}", tree);
        tree.AddRange(new[] { 5, 4, 6, 2, 3 });
        tree.GetKeysPreOrder().SequenceEqual(new[] { 5, 3, 2, 4, 6 }).Should().BeTrue();

        // Right-left rotation
        tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 5, 4, 6, 8, 7 });
        tree.GetKeysPreOrder().SequenceEqual(new[] { 5, 4, 7, 6, 8 }).Should().BeTrue();
		_logger.LogWarning("<<< Exiting Add_Case5_FormsCorrectTree()");
    }

    [Test]
    public void Add_MultipleKeys_FormsCorrectTree()
    {
		_logger.LogError(">>> Entering Add_MultipleKeys_FormsCorrectTree()");
        var tree = new RedBlackTree<int>();

		_logger.LogError("Add_MultipleKeys_FormsCorrectTree(): tree → {Tree}", tree);
        foreach (var value in new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })
        {
            tree.Add(value);
            tree.Count.Should().Be(value);
        }

        tree.GetKeysInOrder().SequenceEqual(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }).Should().BeTrue();
        tree.GetKeysPreOrder().SequenceEqual(new[] { 4, 2, 1, 3, 6, 5, 8, 7, 9, 10 }).Should().BeTrue();
		_logger.LogWarning("<<< Exiting Add_MultipleKeys_FormsCorrectTree()");
    }

    [Test]
    public void Add_KeyAlreadyInTree_ThrowsException()
    {
		_logger.LogError(">>> Entering Add_KeyAlreadyInTree_ThrowsException()");
        var tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5 });
		_logger.LogError("Add_KeyAlreadyInTree_ThrowsException(): tree → {Tree}", tree);
        Assert.Throws<ArgumentException>(() => tree.Add(1));
		_logger.LogWarning("<<< Exiting Add_KeyAlreadyInTree_ThrowsException()");
    }

    [Test]
    public void AddRange_MultipleKeys_FormsCorrectTree()
    {
		_logger.LogError(">>> Entering AddRange_MultipleKeys_FormsCorrectTree()");
        var tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 9, 0, 1, 6, 7, 5, 2, 8, 4, 3 });
		_logger.LogError("AddRange_MultipleKeys_FormsCorrectTree(): tree → {Tree}", tree);
        tree.Count.Should().Be(10);
        tree.GetKeysInOrder().SequenceEqual(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }).Should().BeTrue();
        tree.GetKeysPreOrder().SequenceEqual(new[] { 5, 1, 0, 3, 2, 4, 7, 6, 9, 8 }).Should().BeTrue();
		_logger.LogWarning("<<< Exiting AddRange_MultipleKeys_FormsCorrectTree()");
    }

    [Test]
    public void Remove_SimpleCases_TreeStillValid()
    {
		_logger.LogError(">>> Entering Remove_SimpleCases_TreeStillValid()");
        var tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 13, 8, 17, 1, 11, 15, 25, 6, 22, 27 });
		_logger.LogError("Remove_SimpleCases_TreeStillValid(): tree → {Tree}", tree);
        tree.Remove(6);
        tree.Count.Should().Be(9);
        tree.Contains(6).Should().BeFalse();
        tree.GetKeysInOrder().SequenceEqual(new[] { 1, 8, 11, 13, 15, 17, 22, 25, 27 }).Should().BeTrue();
        tree.GetKeysPreOrder().SequenceEqual(new[] { 13, 8, 1, 11, 17, 15, 25, 22, 27 }).Should().BeTrue();

        tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 13, 8, 17, 1, 11, 15, 25, 6, 22, 27 });
        tree.Remove(1);
        tree.Count.Should().Be(9);
        tree.Contains(1).Should().BeFalse();
        tree.GetKeysInOrder().SequenceEqual(new[] { 6, 8, 11, 13, 15, 17, 22, 25, 27 }).Should().BeTrue();
        tree.GetKeysPreOrder().SequenceEqual(new[] { 13, 8, 6, 11, 17, 15, 25, 22, 27 }).Should().BeTrue();

        tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 13, 8, 17, 1, 11, 15, 25, 6, 22, 27 });
        tree.Remove(17);
        tree.Count.Should().Be(9);
        tree.Contains(17).Should().BeFalse();
        tree.GetKeysInOrder().SequenceEqual(new[] { 1, 6, 8, 11, 13, 15, 22, 25, 27 }).Should().BeTrue();
        tree.GetKeysPreOrder().SequenceEqual(new[] { 13, 8, 1, 6, 11, 22, 15, 25, 27 }).Should().BeTrue();

        tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 13, 8, 17, 1, 11, 15, 25, 6, 22, 27 });
        tree.Remove(25);
        tree.Count.Should().Be(9);
        tree.Contains(25).Should().BeFalse();
        tree.GetKeysInOrder().SequenceEqual(new[] { 1, 6, 8, 11, 13, 15, 17, 22, 27 }).Should().BeTrue();
        tree.GetKeysPreOrder().SequenceEqual(new[] { 13, 8, 1, 6, 11, 17, 15, 27, 22 }).Should().BeTrue();

        tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 7, 3, 18, 10, 22, 8, 11, 26 });
        tree.Remove(18);
        tree.Count.Should().Be(7);
        tree.Contains(18).Should().BeFalse();
        tree.GetKeysInOrder().SequenceEqual(new[] { 3, 7, 8, 10, 11, 22, 26 }).Should().BeTrue();
        tree.GetKeysPreOrder().SequenceEqual(new[] { 7, 3, 22, 10, 8, 11, 26 }).Should().BeTrue();

        tree = new RedBlackTree<int>();
        tree.Add(1);
        tree.Add(2);
        tree.Remove(1);
        tree.Count.Should().Be(1);
        tree.GetKeysInOrder().SequenceEqual(new[] { 2 }).Should().BeTrue();
        tree.GetKeysPreOrder().SequenceEqual(new[] { 2 }).Should().BeTrue();
		_logger.LogWarning("<<< Exiting Remove_SimpleCases_TreeStillValid()");
    }

    [Test]
    public void Remove_Case1_TreeStillValid()
    {
		_logger.LogError(">>> Entering Remove_Case1_TreeStillValid()");
        var tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 5, 2, 8, 1 });
		_logger.LogError("Remove_Case1_TreeStillValid(): tree → {Tree}", tree);
        tree.Remove(1);
        tree.Remove(2);
        tree.Contains(2).Should().BeFalse();
        tree.GetKeysInOrder().SequenceEqual(new[] { 5, 8 }).Should().BeTrue();
        tree.GetKeysPreOrder().SequenceEqual(new[] { 5, 8 }).Should().BeTrue();
		_logger.LogWarning("<<< Exiting Remove_Case1_TreeStillValid()");
    }

    [Test]
    public void Remove_Case3_TreeStillValid()
    {
		_logger.LogError(">>> Entering Remove_Case3_TreeStillValid()");
        // Move to case 6
        var tree = new RedBlackTree<int>();
		_logger.LogError("Remove_Case3_TreeStillValid(): tree → {Tree}", tree);
        tree.AddRange(new[] { 7, 3, 18, 1, 10, 22, 8, 11, 26 });
        tree.Remove(1);
        tree.Remove(3);
        tree.Count.Should().Be(7);
        tree.Contains(3).Should().BeFalse();
        tree.GetKeysInOrder().SequenceEqual(new[] { 7, 8, 10, 11, 18, 22, 26 }).Should().BeTrue();
        tree.GetKeysPreOrder().SequenceEqual(new[] { 18, 10, 7, 8, 11, 22, 26 }).Should().BeTrue();

        // Move to case 5
        tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 8, 3, 2, 0, 9, 4, 7, 6, 1, 5 });
        tree.Remove(8);
        tree.Remove(6);
        tree.Remove(9);
        tree.Count.Should().Be(7);
        tree.GetKeysInOrder().SequenceEqual(new[] { 0, 1, 2, 3, 4, 5, 7 }).Should().BeTrue();
        tree.GetKeysPreOrder().SequenceEqual(new[] { 3, 1, 0, 2, 5, 4, 7 }).Should().BeTrue();

        // Move to case 4
        tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 7, 5, 8, 4, 6, 3, 2, 9, 0, 1 });
        tree.Remove(9);
        tree.Remove(6);
        tree.Remove(5);
        tree.Remove(8);
        tree.Count.Should().Be(6);
        tree.GetKeysInOrder().SequenceEqual(new[] { 0, 1, 2, 3, 4, 7 }).Should().BeTrue();
        tree.GetKeysPreOrder().SequenceEqual(new[] { 3, 1, 0, 2, 7, 4 }).Should().BeTrue();
		_logger.LogWarning("<<< Exiting Remove_Case3_TreeStillValid()");
    }

    [Test]
    public void Remove_Case4_TreeStillValid()
    {
		_logger.LogError(">>> Entering Remove_Case4_TreeStillValid()");
        var tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 5, 2, 8, 1, 4, 7, 9, 0, 3 });
		_logger.LogError("Remove_Case4_TreeStillValid(): tree → {Tree}", tree);
        tree.Remove(0);
        tree.Remove(3);
        tree.Remove(2);
        tree.Count.Should().Be(6);
        tree.Contains(2).Should().BeFalse();
        tree.GetKeysInOrder().SequenceEqual(new[] { 1, 4, 5, 7, 8, 9 }).Should().BeTrue();
        tree.GetKeysPreOrder().SequenceEqual(new[] { 5, 4, 1, 8, 7, 9 }).Should().BeTrue();
		_logger.LogWarning("<<< Exiting Remove_Case4_TreeStillValid()");
    }

    [Test]
    public void Remove_Case5_TreeStillValid()
    {
		_logger.LogError(">>> Entering Remove_Case5_TreeStillValid()");
        var tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 13, 8, 17, 1, 11, 15, 25, 6, 22, 27 });
		_logger.LogError("Remove_Case5_TreeStillValid(): tree → {Tree}", tree);
        tree.Remove(8);
        tree.Count.Should().Be(9);
        tree.Contains(8).Should().BeFalse();
        tree.GetKeysInOrder().SequenceEqual(new[] { 1, 6, 11, 13, 15, 17, 22, 25, 27 }).Should().BeTrue();
        tree.GetKeysPreOrder().SequenceEqual(new[] { 13, 6, 1, 11, 17, 15, 25, 22, 27 }).Should().BeTrue();

        tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 13, 8, 17, 1, 11, 15, 25, 0, 6, 22 });
        tree.Remove(13);
        tree.Count.Should().Be(9);
        tree.Contains(13).Should().BeFalse();
        tree.GetKeysInOrder().SequenceEqual(new[] { 0, 1, 6, 8, 11, 15, 17, 22, 25 }).Should().BeTrue();
        tree.GetKeysPreOrder().SequenceEqual(new[] { 15, 8, 1, 0, 6, 11, 22, 17, 25 }).Should().BeTrue();

        tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 7, 0, 1, 4, 8, 2, 3, 6, 5, 9 });
        tree.Remove(7);
        tree.Remove(0);
        tree.Remove(1);
        tree.Remove(4);
        tree.Remove(8);
        tree.GetKeysInOrder().SequenceEqual(new[] { 2, 3, 5, 6, 9 }).Should().BeTrue();
        tree.GetKeysPreOrder().SequenceEqual(new[] { 3, 2, 6, 5, 9 }).Should().BeTrue();
		_logger.LogWarning("<<< Exiting Remove_Case5_TreeStillValid()");
    }

    [Test]
    public void Remove_Case6_TreeStillValid()
    {
		_logger.LogError(">>> Entering Remove_Case6_TreeStillValid()");
        var tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 13, 8, 17, 1, 11, 15, 25, 6, 22, 27 });
		_logger.LogError("Remove_Case6_TreeStillValid(): tree → {Tree}", tree);
        tree.Remove(13);
        tree.Count.Should().Be(9);
        tree.Contains(13).Should().BeFalse();
        tree.GetKeysInOrder().SequenceEqual(new[] { 1, 6, 8, 11, 15, 17, 22, 25, 27 }).Should().BeTrue();
        tree.GetKeysPreOrder().SequenceEqual(new[] { 15, 8, 1, 6, 11, 25, 17, 22, 27 }).Should().BeTrue();

        tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 13, 8, 17, 1, 11, 15, 25, 0, 6, 22 });
        tree.Remove(8);
        tree.Count.Should().Be(9);
        tree.Contains(8).Should().BeFalse();
        tree.GetKeysInOrder().SequenceEqual(new[] { 0, 1, 6, 11, 13, 15, 17, 22, 25 }).Should().BeTrue();
        tree.GetKeysPreOrder().SequenceEqual(new[] { 13, 1, 0, 11, 6, 17, 15, 25, 22 }).Should().BeTrue();
		_logger.LogWarning("<<< Exiting Remove_Case6_TreeStillValid()");
    }

    [Test]
    public void Remove_EmptyTree_ThrowsException()
    {
		_logger.LogError(">>> Entering Remove_EmptyTree_ThrowsException()");
        var tree = new RedBlackTree<int>();
		_logger.LogWarning("<<< Exiting Remove_EmptyTree_ThrowsException()");
		_logger.LogError("Remove_EmptyTree_ThrowsException(): tree → {Tree}", tree);
        Assert.Throws<InvalidOperationException>(() => tree.Remove(1));
    }

    [Test]
    public void Remove_KeyNotInTree_ThrowsException()
    {
		_logger.LogError(">>> Entering Remove_KeyNotInTree_ThrowsException()");
        var tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
		_logger.LogError("Remove_KeyNotInTree_ThrowsException(): tree → {Tree}", tree);
        Assert.Throws<KeyNotFoundException>(() => tree.Remove(24));
		_logger.LogWarning("<<< Exiting Remove_KeyNotInTree_ThrowsException()");
    }

    [Test]
    public void Contains_CorrectReturn()
    {
		_logger.LogError(">>> Entering Contains_CorrectReturn()");
        var tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
		_logger.LogError("Contains_CorrectReturn(): tree → {Tree}", tree);
        tree.Contains(3).Should().BeTrue();
        tree.Contains(7).Should().BeTrue();
        tree.Contains(24).Should().BeFalse();
        tree.Contains(-1).Should().BeFalse();
		_logger.LogWarning("<<< Exiting Contains_CorrectReturn()");
    }

    [Test]
    public void Contains_EmptyTree_ReturnsFalse()
    {
		_logger.LogError(">>> Entering Contains_EmptyTree_ReturnsFalse()");
        var tree = new RedBlackTree<int>();
        tree.Contains(5).Should().BeFalse();
		_logger.LogError("Contains_EmptyTree_ReturnsFalse(): tree → {Tree}", tree);
        tree.Contains(-12).Should().BeFalse();
		_logger.LogWarning("<<< Exiting Contains_EmptyTree_ReturnsFalse()");
    }

    [Test]
    public void GetMin_CorrectReturn()
    {
		_logger.LogError(">>> Entering GetMin_CorrectReturn()");
        var tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
		_logger.LogError("GetMin_CorrectReturn(): tree → {Tree}", tree);
        tree.GetMin().Should().Be(1);
		_logger.LogWarning("<<< Exiting GetMin_CorrectReturn()");
    }

    [Test]
    public void GetMin_EmptyTree_ThrowsException()
    {
		_logger.LogError(">>> Entering GetMin_EmptyTree_ThrowsException()");
        var tree = new RedBlackTree<int>();
		_logger.LogWarning("<<< Exiting GetMin_EmptyTree_ThrowsException()");
		_logger.LogError("GetMin_EmptyTree_ThrowsException(): tree → {Tree}", tree);
        Assert.Throws<InvalidOperationException>(() => tree.GetMin());
    }

    [Test]
    public void GetMax_CorrectReturn()
    {
		_logger.LogError(">>> Entering GetMax_CorrectReturn()");
        var tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
		_logger.LogError("GetMax_CorrectReturn(): tree → {Tree}", tree);
        tree.GetMax().Should().Be(10);
		_logger.LogWarning("<<< Exiting GetMax_CorrectReturn()");
    }

    [Test]
    public void GetMax_EmptyTree_ThrowsException()
    {
		_logger.LogError(">>> Entering GetMax_EmptyTree_ThrowsException()");
        var tree = new RedBlackTree<int>();
		_logger.LogWarning("<<< Exiting GetMax_EmptyTree_ThrowsException()");
		_logger.LogError("GetMax_EmptyTree_ThrowsException(): tree → {Tree}", tree);
        Assert.Throws<InvalidOperationException>(() => tree.GetMax());
    }

    [Test]
    public void GetKeysInOrder_CorrectReturn()
    {
		_logger.LogError(">>> Entering GetKeysInOrder_CorrectReturn()");
        var tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
		_logger.LogError("GetKeysInOrder_CorrectReturn(): tree → {Tree}", tree);
        tree.GetKeysInOrder().SequenceEqual(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }).Should().BeTrue();
		_logger.LogWarning("<<< Exiting GetKeysInOrder_CorrectReturn()");
    }

    [Test]
    public void GetKeysInOrder_EmptyTree_CorrectReturn()
    {
		_logger.LogError(">>> Entering GetKeysInOrder_EmptyTree_CorrectReturn()");
        var tree = new RedBlackTree<int>();
		_logger.LogWarning("<<< Exiting GetKeysInOrder_EmptyTree_CorrectReturn()");
		_logger.LogError("GetKeysInOrder_EmptyTree_CorrectReturn(): tree → {Tree}", tree);
        tree.GetKeysInOrder().SequenceEqual(Array.Empty<int>()).Should().BeTrue();
    }

    [Test]
    public void GetKeysPreOrder_CorrectReturn()
    {
		_logger.LogError(">>> Entering GetKeysPreOrder_CorrectReturn()");
        var tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
		_logger.LogError("GetKeysPreOrder_CorrectReturn(): tree → {Tree}", tree);
        tree.GetKeysPreOrder().SequenceEqual(new[] { 4, 2, 1, 3, 6, 5, 8, 7, 9, 10 }).Should().BeTrue();
		_logger.LogWarning("<<< Exiting GetKeysPreOrder_CorrectReturn()");
    }

    [Test]
    public void GetKeysPreOrder_EmptyTree_CorrectReturn()
    {
		_logger.LogError(">>> Entering GetKeysPreOrder_EmptyTree_CorrectReturn()");
        var tree = new RedBlackTree<int>();
		_logger.LogWarning("<<< Exiting GetKeysPreOrder_EmptyTree_CorrectReturn()");
		_logger.LogError("GetKeysPreOrder_EmptyTree_CorrectReturn(): tree → {Tree}", tree);
        tree.GetKeysPreOrder().SequenceEqual(Array.Empty<int>()).Should().BeTrue();
    }

    [Test]
    public void GetKeysPostOrder_CorrectReturn()
    {
		_logger.LogError(">>> Entering GetKeysPostOrder_CorrectReturn()");
        var tree = new RedBlackTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
		_logger.LogError("GetKeysPostOrder_CorrectReturn(): tree → {Tree}", tree);
        tree.GetKeysPostOrder().SequenceEqual(new[] { 1, 3, 2, 5, 7, 10, 9, 8, 6, 4 }).Should().BeTrue();
		_logger.LogWarning("<<< Exiting GetKeysPostOrder_CorrectReturn()");
    }

    [Test]
    public void GetKeysPostOrder_EmptyTree_CorrectReturn()
    {
		_logger.LogError(">>> Entering GetKeysPostOrder_EmptyTree_CorrectReturn()");
        var tree = new RedBlackTree<int>();
		_logger.LogWarning("<<< Exiting GetKeysPostOrder_EmptyTree_CorrectReturn()");
		_logger.LogError("GetKeysPostOrder_EmptyTree_CorrectReturn(): tree → {Tree}", tree);
        tree.GetKeysPostOrder().SequenceEqual(Array.Empty<int>()).Should().BeTrue();
    }
}
