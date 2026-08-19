using DataStructures.BinarySearchTree;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests;

public static class BinarySearchTreeTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void Constructor_UseCustomComparer_FormsCorrectTree()
    {
		_logger.LogDebug(">>> Entering Constructor_UseCustomComparer_FormsCorrectTree()");
        var cmpFunc = Comparer<string>.Create((x, y) => x.Length - y.Length);
        var tree = new BinarySearchTree<string>(cmpFunc);
		_logger.LogDebug("Constructor_UseCustomComparer_FormsCorrectTree(): tree → {Tree}", tree);
        var elems = new[] { "z", "yy", "vvv", "bbbb", "fffff", "pppppp" };
		_logger.LogDebug("Constructor_UseCustomComparer_FormsCorrectTree(): elems → {Elems}", elems);
		_logger.LogDebug("Constructor_UseCustomComparer_FormsCorrectTree(): cmpFunc → {CmpFunc}", cmpFunc);
        tree.AddRange(elems);

        Assert.That(tree.Search("vvv"), Is.Not.Null);
        Assert.That(tree.Search("vvv")!.Right, Is.Not.Null);
        Assert.That(tree.Search("vvv")!.Right!.Key, Is.EqualTo("bbbb"));
		_logger.LogDebug("<<< Exiting Constructor_UseCustomComparer_FormsCorrectTree()");
    }

    [Test]
    public static void Add_MultipleKeys_FormsCorrectBST()
    {
		_logger.LogDebug(">>> Entering Add_MultipleKeys_FormsCorrectBST()");
        var tree = new BinarySearchTree<int>();

		_logger.LogDebug("Add_MultipleKeys_FormsCorrectBST(): tree → {Tree}", tree);
        tree.Add(5);
        Assert.That(tree.Count, Is.EqualTo(1));

        tree.Add(3);
        Assert.That(tree.Count, Is.EqualTo(2));

        tree.Add(4);
        Assert.That(tree.Count, Is.EqualTo(3));

        tree.Add(2);
        Assert.That(tree.Count, Is.EqualTo(4));

        var rootNode = tree.Search(5);
		_logger.LogDebug("Add_MultipleKeys_FormsCorrectBST(): rootNode → {RootNode}", rootNode);
        Assert.That(rootNode!.Key, Is.EqualTo(5));
        Assert.That(rootNode!.Left!.Key, Is.EqualTo(3));
        Assert.That(rootNode!.Right, Is.Null);

        var threeNode = tree.Search(3);
		_logger.LogDebug("Add_MultipleKeys_FormsCorrectBST(): threeNode → {ThreeNode}", threeNode);
        Assert.That(threeNode!.Key, Is.EqualTo(3));
        Assert.That(threeNode!.Left!.Key, Is.EqualTo(2));
        Assert.That(threeNode!.Right!.Key, Is.EqualTo(4));

        var twoNode = tree.Search(2);
		_logger.LogDebug("Add_MultipleKeys_FormsCorrectBST(): twoNode → {TwoNode}", twoNode);
        Assert.That(twoNode!.Left, Is.Null);
        Assert.That(twoNode!.Right, Is.Null);

        var fourNode = tree.Search(4);
		_logger.LogDebug("Add_MultipleKeys_FormsCorrectBST(): fourNode → {FourNode}", fourNode);
        Assert.That(fourNode!.Left, Is.Null);
        Assert.That(fourNode!.Right, Is.Null);
		_logger.LogDebug("<<< Exiting Add_MultipleKeys_FormsCorrectBST()");
    }

    [Test]
    public static void Add_KeyAlreadyInTree_ThrowsCorrectException()
    {
		_logger.LogDebug(">>> Entering Add_KeyAlreadyInTree_ThrowsCorrectException()");
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2 });
		_logger.LogDebug("Add_KeyAlreadyInTree_ThrowsCorrectException(): tree → {Tree}", tree);

        _ = Assert.Throws<ArgumentException>(() => tree.Add(5));
		_logger.LogDebug("<<< Exiting Add_KeyAlreadyInTree_ThrowsCorrectException()");
    }

    [Test]
    public static void AddRange_MultipleKeys_FormsCorrectBST()
    {
		_logger.LogDebug(">>> Entering AddRange_MultipleKeys_FormsCorrectBST()");
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2 });
		_logger.LogDebug("AddRange_MultipleKeys_FormsCorrectBST(): tree → {Tree}", tree);

        var rootNode = tree.Search(5);
		_logger.LogDebug("AddRange_MultipleKeys_FormsCorrectBST(): rootNode → {RootNode}", rootNode);
        Assert.That(rootNode!.Key, Is.EqualTo(5));
        Assert.That(rootNode!.Left!.Key, Is.EqualTo(3));
        Assert.That(rootNode!.Right, Is.Null);

        var threeNode = tree.Search(3);
		_logger.LogDebug("AddRange_MultipleKeys_FormsCorrectBST(): threeNode → {ThreeNode}", threeNode);
        Assert.That(threeNode!.Key, Is.EqualTo(3));
        Assert.That(threeNode!.Left!.Key, Is.EqualTo(2));
        Assert.That(threeNode!.Right!.Key, Is.EqualTo(4));

        var twoNode = tree.Search(2);
		_logger.LogDebug("AddRange_MultipleKeys_FormsCorrectBST(): twoNode → {TwoNode}", twoNode);
        Assert.That(twoNode!.Left, Is.Null);
        Assert.That(twoNode!.Right, Is.Null);

        var fourNode = tree.Search(4);
		_logger.LogDebug("AddRange_MultipleKeys_FormsCorrectBST(): fourNode → {FourNode}", fourNode);
        Assert.That(fourNode!.Left, Is.Null);
        Assert.That(fourNode!.Right, Is.Null);
		_logger.LogDebug("<<< Exiting AddRange_MultipleKeys_FormsCorrectBST()");
    }

    [Test]
    public static void Search_MultipleKeys_FindsAllKeys()
    {
		_logger.LogDebug(">>> Entering Search_MultipleKeys_FindsAllKeys()");
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });
		_logger.LogDebug("Search_MultipleKeys_FindsAllKeys(): tree → {Tree}", tree);

        Assert.That(tree.Search(2)!.Key, Is.EqualTo(2));
        Assert.That(tree.Search(3)!.Key, Is.EqualTo(3));
        Assert.That(tree.Search(4)!.Key, Is.EqualTo(4));
        Assert.That(tree.Search(5)!.Key, Is.EqualTo(5));
        Assert.That(tree.Search(6)!.Key, Is.EqualTo(6));
        Assert.That(tree.Search(7)!.Key, Is.EqualTo(7));
        Assert.That(tree.Search(8)!.Key, Is.EqualTo(8));
		_logger.LogDebug("<<< Exiting Search_MultipleKeys_FindsAllKeys()");
    }

    [Test]
    public static void Contains_MultipleKeys_FindsAllKeys()
    {
		_logger.LogDebug(">>> Entering Contains_MultipleKeys_FindsAllKeys()");
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });
		_logger.LogDebug("Contains_MultipleKeys_FindsAllKeys(): tree → {Tree}", tree);

        Assert.That(tree.Contains(2), Is.True);
        Assert.That(tree.Contains(3), Is.True);
        Assert.That(tree.Contains(4), Is.True);
        Assert.That(tree.Contains(5), Is.True);
        Assert.That(tree.Contains(6), Is.True);
        Assert.That(tree.Contains(7), Is.True);
        Assert.That(tree.Contains(8), Is.True);
		_logger.LogDebug("<<< Exiting Contains_MultipleKeys_FindsAllKeys()");
    }

    [Test]
    public static void Remove_LeafNodes_CorrectlyRemovesNodes()
    {
		_logger.LogDebug(">>> Entering Remove_LeafNodes_CorrectlyRemovesNodes()");
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });
		_logger.LogDebug("Remove_LeafNodes_CorrectlyRemovesNodes(): tree → {Tree}", tree);

        var twoRemoveResult = tree.Remove(2);
		_logger.LogDebug("Remove_LeafNodes_CorrectlyRemovesNodes(): twoRemoveResult → {TwoRemoveResult}", twoRemoveResult);
        Assert.That(twoRemoveResult, Is.True);
        Assert.That(tree.Search(2), Is.Null);
        Assert.That(tree.Search(3)!.Left, Is.Null);
        Assert.That(tree.Search(3)!.Right, Is.Not.Null);
        Assert.That(tree.Count, Is.EqualTo(6));

        var fourRemoveResult = tree.Remove(4);
		_logger.LogDebug("Remove_LeafNodes_CorrectlyRemovesNodes(): fourRemoveResult → {FourRemoveResult}", fourRemoveResult);
        Assert.That(fourRemoveResult, Is.True);
        Assert.That(tree.Search(4), Is.Null);
        Assert.That(tree.Search(3)!.Left, Is.Null);
        Assert.That(tree.Search(3)!.Right, Is.Null);
        Assert.That(tree.Count, Is.EqualTo(5));
		_logger.LogDebug("<<< Exiting Remove_LeafNodes_CorrectlyRemovesNodes()");
    }

    [Test]
    public static void Remove_NodesWithOneChild_CorrectlyRemovesNodes()
    {
		_logger.LogDebug(">>> Entering Remove_NodesWithOneChild_CorrectlyRemovesNodes()");
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });
		_logger.LogDebug("Remove_NodesWithOneChild_CorrectlyRemovesNodes(): tree → {Tree}", tree);

        tree.Remove(4);
        var threeRemoveResult = tree.Remove(3);
		_logger.LogDebug("Remove_NodesWithOneChild_CorrectlyRemovesNodes(): threeRemoveResult → {ThreeRemoveResult}", threeRemoveResult);
        Assert.That(threeRemoveResult, Is.True);
        Assert.That(tree.Search(3), Is.Null);
        Assert.That(tree.Search(2)!.Left, Is.Null);
        Assert.That(tree.Search(2)!.Right, Is.Null);
        Assert.That(tree.Count, Is.EqualTo(5));

        tree.Remove(6);
        var sevenRemoveResult = tree.Remove(7);
		_logger.LogDebug("Remove_NodesWithOneChild_CorrectlyRemovesNodes(): sevenRemoveResult → {SevenRemoveResult}", sevenRemoveResult);
        Assert.That(sevenRemoveResult, Is.True);
        Assert.That(tree.Search(7), Is.Null);
        Assert.That(tree.Search(8)!.Left, Is.Null);
        Assert.That(tree.Search(8)!.Right, Is.Null);
        Assert.That(tree.Count, Is.EqualTo(3));
		_logger.LogDebug("<<< Exiting Remove_NodesWithOneChild_CorrectlyRemovesNodes()");
    }

    [Test]
    public static void Remove_NodesWithTwoChildren_CorrectlyRemovesNodes()
    {
		_logger.LogDebug(">>> Entering Remove_NodesWithTwoChildren_CorrectlyRemovesNodes()");
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });
		_logger.LogDebug("Remove_NodesWithTwoChildren_CorrectlyRemovesNodes(): tree → {Tree}", tree);

        var sevenRemoveResult = tree.Remove(7);
		_logger.LogDebug("Remove_NodesWithTwoChildren_CorrectlyRemovesNodes(): sevenRemoveResult → {SevenRemoveResult}", sevenRemoveResult);
        Assert.That(sevenRemoveResult, Is.True);
        Assert.That(tree.Search(7), Is.Null);
        Assert.That(tree.Search(6)!.Left, Is.Null);
        Assert.That(tree.Search(6)!.Right, Is.Not.Null);
        Assert.That(tree.Count, Is.EqualTo(6));
		_logger.LogDebug("<<< Exiting Remove_NodesWithTwoChildren_CorrectlyRemovesNodes()");
    }

    [Test]
    public static void Remove_NonExistentElement_ReturnsFalse()
    {
		_logger.LogDebug(">>> Entering Remove_NonExistentElement_ReturnsFalse()");
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });
		_logger.LogDebug("Remove_NonExistentElement_ReturnsFalse(): tree → {Tree}", tree);

        Assert.That(tree.Remove(999), Is.False);
        Assert.That(tree.Count, Is.EqualTo(7));
		_logger.LogDebug("<<< Exiting Remove_NonExistentElement_ReturnsFalse()");
    }

    [Test]
    public static void Remove_EmptyTree_ReturnsFalse()
    {
		_logger.LogDebug(">>> Entering Remove_EmptyTree_ReturnsFalse()");
        var tree = new BinarySearchTree<int>();
        Assert.That(tree.Remove(8), Is.False);
		_logger.LogDebug("Remove_EmptyTree_ReturnsFalse(): tree → {Tree}", tree);
        Assert.That(tree.Count, Is.EqualTo(0));
		_logger.LogDebug("<<< Exiting Remove_EmptyTree_ReturnsFalse()");
    }

    [Test]
    public static void Remove_RemoveRoot_CorrectlyRemovesRoot()
    {
		_logger.LogDebug(">>> Entering Remove_RemoveRoot_CorrectlyRemovesRoot()");
        var tree = new BinarySearchTree<int>();
        tree.Add(5);
		_logger.LogDebug("Remove_RemoveRoot_CorrectlyRemovesRoot(): tree → {Tree}", tree);
        tree.Remove(5);

        Assert.That(tree.Count, Is.EqualTo(0));
        Assert.That(tree.Search(5), Is.Null);

        tree.AddRange(new List<int> { 5, 4, 6 });
        tree.Remove(5);

        Assert.That(tree.Count, Is.EqualTo(2));
        Assert.That(tree.Search(5), Is.Null);
        Assert.That(tree.Search(4), Is.Not.Null);
        Assert.That(tree.Search(6), Is.Not.Null);
        Assert.That(tree.Search(4)!.Right!.Key, Is.EqualTo(6));
		_logger.LogDebug("<<< Exiting Remove_RemoveRoot_CorrectlyRemovesRoot()");
    }

    [Test]
    public static void GetMax_NonEmptyTree_ReturnsCorrectValue()
    {
		_logger.LogDebug(">>> Entering GetMax_NonEmptyTree_ReturnsCorrectValue()");
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });
		_logger.LogDebug("GetMax_NonEmptyTree_ReturnsCorrectValue(): tree → {Tree}", tree);

        Assert.That(tree.GetMax()!.Key, Is.EqualTo(8));
		_logger.LogDebug("<<< Exiting GetMax_NonEmptyTree_ReturnsCorrectValue()");
    }

    [Test]
    public static void GetMax_EmptyTree_ReturnsDefaultValue()
    {
		_logger.LogDebug(">>> Entering GetMax_EmptyTree_ReturnsDefaultValue()");
        var tree = new BinarySearchTree<int>();
        Assert.That(tree.GetMax(), Is.Null);
		_logger.LogDebug("GetMax_EmptyTree_ReturnsDefaultValue(): tree → {Tree}", tree);
		_logger.LogDebug("<<< Exiting GetMax_EmptyTree_ReturnsDefaultValue()");
    }

    [Test]
    public static void GetMin_NonEmptyTree_ReturnsCorrectValue()
    {
		_logger.LogDebug(">>> Entering GetMin_NonEmptyTree_ReturnsCorrectValue()");
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });
		_logger.LogDebug("GetMin_NonEmptyTree_ReturnsCorrectValue(): tree → {Tree}", tree);

        Assert.That(tree.GetMin()!.Key, Is.EqualTo(2));
		_logger.LogDebug("<<< Exiting GetMin_NonEmptyTree_ReturnsCorrectValue()");
    }

    [Test]
    public static void GetMin_EmptyTree_ReturnsDefaultValue()
    {
		_logger.LogDebug(">>> Entering GetMin_EmptyTree_ReturnsDefaultValue()");
        var tree = new BinarySearchTree<int>();
        Assert.That(tree.GetMin(), Is.Null);
		_logger.LogDebug("GetMin_EmptyTree_ReturnsDefaultValue(): tree → {Tree}", tree);
		_logger.LogDebug("<<< Exiting GetMin_EmptyTree_ReturnsDefaultValue()");
    }

    [Test]
    public static void GetKeysInOrder_MultipleKeys_ReturnsAllKeysInCorrectOrder()
    {
		_logger.LogDebug(">>> Entering GetKeysInOrder_MultipleKeys_ReturnsAllKeysInCorrectOrder()");
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });
		_logger.LogDebug("GetKeysInOrder_MultipleKeys_ReturnsAllKeysInCorrectOrder(): tree → {Tree}", tree);

        var keys = tree.GetKeysInOrder();
		_logger.LogDebug("GetKeysInOrder_MultipleKeys_ReturnsAllKeysInCorrectOrder(): keys → {Keys}", keys);
        var expected = new List<int> { 2, 3, 4, 5, 6, 7, 8 };
        Assert.That(keys.SequenceEqual(expected), Is.True);
		_logger.LogDebug("<<< Exiting GetKeysInOrder_MultipleKeys_ReturnsAllKeysInCorrectOrder()");
    }

    [Test]
    public static void GetKeysPreOrder_MultipleKeys_ReturnsAllKeysInCorrectOrder()
    {
		_logger.LogDebug(">>> Entering GetKeysPreOrder_MultipleKeys_ReturnsAllKeysInCorrectOrder()");
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });
		_logger.LogDebug("GetKeysPreOrder_MultipleKeys_ReturnsAllKeysInCorrectOrder(): tree → {Tree}", tree);

        var keys = tree.GetKeysPreOrder();
		_logger.LogDebug("GetKeysPreOrder_MultipleKeys_ReturnsAllKeysInCorrectOrder(): keys → {Keys}", keys);
        var expected = new List<int> { 5, 3, 2, 4, 7, 6, 8 };
        Assert.That(keys.SequenceEqual(expected), Is.True);
		_logger.LogDebug("<<< Exiting GetKeysPreOrder_MultipleKeys_ReturnsAllKeysInCorrectOrder()");
    }

    [Test]
    public static void GetKeysPostOrder_MultipleKeys_ReturnsAllKeysInCorrectOrder()
    {
		_logger.LogDebug(">>> Entering GetKeysPostOrder_MultipleKeys_ReturnsAllKeysInCorrectOrder()");
        var tree = new BinarySearchTree<int>();
        tree.AddRange(new List<int> { 5, 3, 4, 2, 7, 6, 8 });
		_logger.LogDebug("GetKeysPostOrder_MultipleKeys_ReturnsAllKeysInCorrectOrder(): tree → {Tree}", tree);

        var keys = tree.GetKeysPostOrder();
		_logger.LogDebug("GetKeysPostOrder_MultipleKeys_ReturnsAllKeysInCorrectOrder(): keys → {Keys}", keys);
        var expected = new List<int> { 2, 4, 3, 6, 8, 7, 5 };
        Assert.That(keys.SequenceEqual(expected), Is.True);
		_logger.LogDebug("<<< Exiting GetKeysPostOrder_MultipleKeys_ReturnsAllKeysInCorrectOrder()");
    }
}
