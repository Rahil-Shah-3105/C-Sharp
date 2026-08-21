using DataStructures.BTree;
using static FluentAssertions.FluentActions;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests;

internal class BTreeTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private static readonly int[] Data = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];

    [Test]
    public void Constructor_DefaultMinimumDegree_CreatesEmptyTree()
    {
		_logger.LogDebug(">>> Entering Constructor_DefaultMinimumDegree_CreatesEmptyTree()");
        var tree = new BTree<int>();

		_logger.LogDebug("Constructor_DefaultMinimumDegree_CreatesEmptyTree(): tree → {Tree}", tree);
        tree.Count.Should().Be(0);
        tree.MinimumDegree.Should().Be(2);
		_logger.LogDebug("<<< Exiting Constructor_DefaultMinimumDegree_CreatesEmptyTree()");
    }

    [Test]
    public void Constructor_CustomMinimumDegree_SetsCorrectDegree()
    {
		_logger.LogDebug(">>> Entering Constructor_CustomMinimumDegree_SetsCorrectDegree()");
        var tree = new BTree<int>(3);

		_logger.LogDebug("Constructor_CustomMinimumDegree_SetsCorrectDegree(): tree → {Tree}", tree);
        tree.MinimumDegree.Should().Be(3);
        tree.Count.Should().Be(0);
		_logger.LogDebug("<<< Exiting Constructor_CustomMinimumDegree_SetsCorrectDegree()");
    }

    [Test]
    public void Constructor_MinimumDegreeLessThan2_ThrowsException()
    {
		_logger.LogDebug(">>> Entering Constructor_MinimumDegreeLessThan2_ThrowsException()");
        Invoking(() => new BTree<int>(1))
            .Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage("Minimum degree must be at least 2.*");
			_logger.LogDebug("<<< Exiting Constructor_MinimumDegreeLessThan2_ThrowsException()");
    }

    [Test]
    public void Constructor_CustomComparerMinimumDegreeLessThan2_ThrowsException()
    {
		_logger.LogDebug(">>> Entering Constructor_CustomComparerMinimumDegreeLessThan2_ThrowsException()");
        var comparer = Comparer<int>.Create((x, y) => y.CompareTo(x));

		_logger.LogDebug("Constructor_CustomComparerMinimumDegreeLessThan2_ThrowsException(): comparer → {Comparer}", comparer);
        Invoking(() => new BTree<int>(1, comparer))
            .Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage("Minimum degree must be at least 2.*");
			_logger.LogDebug("<<< Exiting Constructor_CustomComparerMinimumDegreeLessThan2_ThrowsException()");
    }

    [Test]
    public void Constructor_UseCustomComparer_FormsCorrectTree()
    {
		_logger.LogDebug(">>> Entering Constructor_UseCustomComparer_FormsCorrectTree()");
        var tree = new BTree<int>(2, Comparer<int>.Create((x, y) => y.CompareTo(x)));

		_logger.LogDebug("Constructor_UseCustomComparer_FormsCorrectTree(): tree → {Tree}", tree);
        tree.AddRange(new[] { 1, 2, 3, 4, 5 });

        tree.GetMin().Should().Be(5);
        tree.GetMax().Should().Be(1);

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 5, 4, 3, 2, 1 },
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting Constructor_UseCustomComparer_FormsCorrectTree()");
    }

    [Test]
    public void Add_SingleKey_IncreasesCount()
    {
		_logger.LogDebug(">>> Entering Add_SingleKey_IncreasesCount()");
        var tree = new BTree<int>();

		_logger.LogDebug("Add_SingleKey_IncreasesCount(): tree → {Tree}", tree);
        tree.Add(5);

        tree.Count.Should().Be(1);
        tree.Contains(5).Should().BeTrue();
		_logger.LogDebug("<<< Exiting Add_SingleKey_IncreasesCount()");
    }

    [Test]
    public void Add_MultipleKeys_FormsCorrectTree()
    {
		_logger.LogDebug(">>> Entering Add_MultipleKeys_FormsCorrectTree()");
        var tree = new BTree<int>(2);

		_logger.LogDebug("Add_MultipleKeys_FormsCorrectTree(): tree → {Tree}", tree);
        for (var i = 0; i < Data.Length; i++)
        {
            tree.Add(Data[i]);
            tree.Count.Should().Be(i + 1);
        }

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                Data,
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting Add_MultipleKeys_FormsCorrectTree()");
    }

    [Test]
    public void Add_DuplicateKey_ThrowsException()
    {
		_logger.LogDebug(">>> Entering Add_DuplicateKey_ThrowsException()");
        var tree = new BTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5 });
		_logger.LogDebug("Add_DuplicateKey_ThrowsException(): tree → {Tree}", tree);

        Invoking(() => tree.Add(3))
            .Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage("""Key "3" already exists in B-Tree.""");
			_logger.LogDebug("<<< Exiting Add_DuplicateKey_ThrowsException()");
    }

    [Test]
    public void Add_DuplicateKeyInLeaf_ThrowsException()
    {
		_logger.LogDebug(">>> Entering Add_DuplicateKeyInLeaf_ThrowsException()");
        var tree = new BTree<int>(3);
        tree.AddRange(new[] { 10, 20 });
		_logger.LogDebug("Add_DuplicateKeyInLeaf_ThrowsException(): tree → {Tree}", tree);

        Invoking(() => tree.Add(10))
            .Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage("""Key "10" already exists in B-Tree.""");
			_logger.LogDebug("<<< Exiting Add_DuplicateKeyInLeaf_ThrowsException()");
    }

    [Test]
    public void Add_CausesNodeSplit_TreeStillValid()
    {
		_logger.LogDebug(">>> Entering Add_CausesNodeSplit_TreeStillValid()");
        var tree = new BTree<int>(2);
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7 });
		_logger.LogDebug("Add_CausesNodeSplit_TreeStillValid(): tree → {Tree}", tree);

        tree.Count.Should().Be(7);
        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 1, 2, 3, 4, 5, 6, 7 },
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting Add_CausesNodeSplit_TreeStillValid()");
    }

    [Test]
    public void Add_LargeNumberOfKeys_TreeStillValid()
    {
		_logger.LogDebug(">>> Entering Add_LargeNumberOfKeys_TreeStillValid()");
        var tree = new BTree<int>(3);
        var keys = Enumerable.Range(1, 100).ToArray();
		_logger.LogDebug("Add_LargeNumberOfKeys_TreeStillValid(): keys → {Keys}", keys);
		_logger.LogDebug("Add_LargeNumberOfKeys_TreeStillValid(): tree → {Tree}", tree);

        tree.AddRange(keys);

        tree.Count.Should().Be(100);
        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                keys,
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting Add_LargeNumberOfKeys_TreeStillValid()");
    }

    [Test]
    public void AddRange_MultipleKeys_FormsCorrectTree()
    {
		_logger.LogDebug(">>> Entering AddRange_MultipleKeys_FormsCorrectTree()");
        var tree = new BTree<char>(2);
        tree.AddRange(new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
		_logger.LogDebug("AddRange_MultipleKeys_FormsCorrectTree(): tree → {Tree}", tree);

        tree.Count.Should().Be(7);

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' },
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting AddRange_MultipleKeys_FormsCorrectTree()");
    }

    [Test]
    public void AddRange_EmptyCollection_TreeRemainsEmpty()
    {
		_logger.LogDebug(">>> Entering AddRange_EmptyCollection_TreeRemainsEmpty()");
        var tree = new BTree<int>();
        tree.AddRange(Array.Empty<int>());
		_logger.LogDebug("AddRange_EmptyCollection_TreeRemainsEmpty(): tree → {Tree}", tree);

        tree.Count.Should().Be(0);
        tree.GetKeysInOrder().Should().BeEmpty();
		_logger.LogDebug("<<< Exiting AddRange_EmptyCollection_TreeRemainsEmpty()");
    }

    [Test]
    public void Contains_KeyExists_ReturnsTrue()
    {
		_logger.LogDebug(">>> Entering Contains_KeyExists_ReturnsTrue()");
        var tree = new BTree<int>();
        tree.AddRange(Data);
		_logger.LogDebug("Contains_KeyExists_ReturnsTrue(): tree → {Tree}", tree);

        tree.Contains(5).Should().BeTrue();
        tree.Contains(1).Should().BeTrue();
        tree.Contains(15).Should().BeTrue();
		_logger.LogDebug("<<< Exiting Contains_KeyExists_ReturnsTrue()");
    }

    [Test]
    public void Contains_KeyDoesNotExist_ReturnsFalse()
    {
		_logger.LogDebug(">>> Entering Contains_KeyDoesNotExist_ReturnsFalse()");
        var tree = new BTree<int>();
        tree.AddRange(Data);
		_logger.LogDebug("Contains_KeyDoesNotExist_ReturnsFalse(): tree → {Tree}", tree);

        tree.Contains(100).Should().BeFalse();
        tree.Contains(-5).Should().BeFalse();
        tree.Contains(0).Should().BeFalse();
		_logger.LogDebug("<<< Exiting Contains_KeyDoesNotExist_ReturnsFalse()");
    }

    [Test]
    public void Contains_EmptyTree_ReturnsFalse()
    {
		_logger.LogDebug(">>> Entering Contains_EmptyTree_ReturnsFalse()");
        var tree = new BTree<int>();

		_logger.LogDebug("Contains_EmptyTree_ReturnsFalse(): tree → {Tree}", tree);
        tree.Contains(5).Should().BeFalse();
        tree.Contains(-12).Should().BeFalse();
		_logger.LogDebug("<<< Exiting Contains_EmptyTree_ReturnsFalse()");
    }

    [Test]
    public void Remove_SingleKey_DecreasesCount()
    {
		_logger.LogDebug(">>> Entering Remove_SingleKey_DecreasesCount()");
        var tree = new BTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5 });
		_logger.LogDebug("Remove_SingleKey_DecreasesCount(): tree → {Tree}", tree);

        tree.Remove(3);

        tree.Count.Should().Be(4);
        tree.Contains(3).Should().BeFalse();
		_logger.LogDebug("<<< Exiting Remove_SingleKey_DecreasesCount()");
    }

    [Test]
    public void Remove_FromLeafNode_TreeStillValid()
    {
		_logger.LogDebug(">>> Entering Remove_FromLeafNode_TreeStillValid()");
        var tree = new BTree<int>(2);
        tree.AddRange(Data);
		_logger.LogDebug("Remove_FromLeafNode_TreeStillValid(): tree → {Tree}", tree);

        tree.Remove(1);

        tree.Count.Should().Be(14);
        tree.Contains(1).Should().BeFalse();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting Remove_FromLeafNode_TreeStillValid()");
    }

    [Test]
    public void Remove_FromNonLeafNode_TreeStillValid()
    {
		_logger.LogDebug(">>> Entering Remove_FromNonLeafNode_TreeStillValid()");
        var tree = new BTree<int>(2);
        tree.AddRange(Data);
		_logger.LogDebug("Remove_FromNonLeafNode_TreeStillValid(): tree → {Tree}", tree);

        tree.Remove(8);

        tree.Count.Should().Be(14);
        tree.Contains(8).Should().BeFalse();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 1, 2, 3, 4, 5, 6, 7, 9, 10, 11, 12, 13, 14, 15 },
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting Remove_FromNonLeafNode_TreeStillValid()");
    }

    [Test]
    public void Remove_CausesMerge_TreeStillValid()
    {
		_logger.LogDebug(">>> Entering Remove_CausesMerge_TreeStillValid()");
        var tree = new BTree<int>(2);
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7 });
		_logger.LogDebug("Remove_CausesMerge_TreeStillValid(): tree → {Tree}", tree);

        tree.Remove(4);

        tree.Count.Should().Be(6);
        tree.Contains(4).Should().BeFalse();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 1, 2, 3, 5, 6, 7 },
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting Remove_CausesMerge_TreeStillValid()");
    }

    [Test]
    public void Remove_MultipleKeys_TreeStillValid()
    {
		_logger.LogDebug(">>> Entering Remove_MultipleKeys_TreeStillValid()");
        var tree = new BTree<int>(2);
        tree.AddRange(Data);
		_logger.LogDebug("Remove_MultipleKeys_TreeStillValid(): tree → {Tree}", tree);

        tree.Remove(5);
        tree.Remove(10);
        tree.Remove(15);

        tree.Count.Should().Be(12);
        tree.Contains(5).Should().BeFalse();
        tree.Contains(10).Should().BeFalse();
        tree.Contains(15).Should().BeFalse();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 1, 2, 3, 4, 6, 7, 8, 9, 11, 12, 13, 14 },
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting Remove_MultipleKeys_TreeStillValid()");
    }

    [Test]
    public void Remove_AllKeys_TreeBecomesEmpty()
    {
		_logger.LogDebug(">>> Entering Remove_AllKeys_TreeBecomesEmpty()");
        var tree = new BTree<int>(2);
        tree.AddRange(new[] { 1, 2, 3, 4, 5 });
		_logger.LogDebug("Remove_AllKeys_TreeBecomesEmpty(): tree → {Tree}", tree);

        tree.Remove(1);
        tree.Remove(2);
        tree.Remove(3);
        tree.Remove(4);
        tree.Remove(5);

        tree.Count.Should().Be(0);
        tree.GetKeysInOrder().Should().BeEmpty();
		_logger.LogDebug("<<< Exiting Remove_AllKeys_TreeBecomesEmpty()");
    }

    [Test]
    public void Remove_EmptyTree_ThrowsException()
    {
		_logger.LogDebug(">>> Entering Remove_EmptyTree_ThrowsException()");
        var tree = new BTree<int>();

		_logger.LogDebug("Remove_EmptyTree_ThrowsException(): tree → {Tree}", tree);
        Invoking(() => tree.Remove(1))
            .Should()
            .ThrowExactly<KeyNotFoundException>()
            .WithMessage("""Key "1" is not in the B-Tree.""");
			_logger.LogDebug("<<< Exiting Remove_EmptyTree_ThrowsException()");
    }

    [Test]
    public void Remove_KeyNotInTree_ThrowsException()
    {
		_logger.LogDebug(">>> Entering Remove_KeyNotInTree_ThrowsException()");
        var tree = new BTree<int>();
        tree.AddRange(Data);
		_logger.LogDebug("Remove_KeyNotInTree_ThrowsException(): tree → {Tree}", tree);

        Invoking(() => tree.Remove(100))
            .Should()
            .ThrowExactly<KeyNotFoundException>()
            .WithMessage("""Key "100" is not in the B-Tree.""");
			_logger.LogDebug("<<< Exiting Remove_KeyNotInTree_ThrowsException()");
    }

    [Test]
    public void GetMin_NonEmptyTree_ReturnsMinimum()
    {
		_logger.LogDebug(">>> Entering GetMin_NonEmptyTree_ReturnsMinimum()");
        var tree = new BTree<int>();
        tree.AddRange(new[] { 5, 2, 8, 1, 9, 3 });
		_logger.LogDebug("GetMin_NonEmptyTree_ReturnsMinimum(): tree → {Tree}", tree);

        tree.GetMin().Should().Be(1);
		_logger.LogDebug("<<< Exiting GetMin_NonEmptyTree_ReturnsMinimum()");
    }

    [Test]
    public void GetMin_SingleElement_ReturnsElement()
    {
		_logger.LogDebug(">>> Entering GetMin_SingleElement_ReturnsElement()");
        var tree = new BTree<int>();
        tree.Add(42);
		_logger.LogDebug("GetMin_SingleElement_ReturnsElement(): tree → {Tree}", tree);

        tree.GetMin().Should().Be(42);
		_logger.LogDebug("<<< Exiting GetMin_SingleElement_ReturnsElement()");
    }

    [Test]
    public void GetMin_EmptyTree_ThrowsException()
    {
		_logger.LogDebug(">>> Entering GetMin_EmptyTree_ThrowsException()");
        var tree = new BTree<int>();

		_logger.LogDebug("GetMin_EmptyTree_ThrowsException(): tree → {Tree}", tree);
        Invoking(() => tree.GetMin())
            .Should()
            .ThrowExactly<InvalidOperationException>()
            .WithMessage("B-Tree is empty.");
			_logger.LogDebug("<<< Exiting GetMin_EmptyTree_ThrowsException()");
    }

    [Test]
    public void GetMax_NonEmptyTree_ReturnsMaximum()
    {
		_logger.LogDebug(">>> Entering GetMax_NonEmptyTree_ReturnsMaximum()");
        var tree = new BTree<int>();
        tree.AddRange(new[] { 5, 2, 8, 1, 9, 3 });
		_logger.LogDebug("GetMax_NonEmptyTree_ReturnsMaximum(): tree → {Tree}", tree);

        tree.GetMax().Should().Be(9);
		_logger.LogDebug("<<< Exiting GetMax_NonEmptyTree_ReturnsMaximum()");
    }

    [Test]
    public void GetMax_SingleElement_ReturnsElement()
    {
		_logger.LogDebug(">>> Entering GetMax_SingleElement_ReturnsElement()");
        var tree = new BTree<int>();
        tree.Add(42);
		_logger.LogDebug("GetMax_SingleElement_ReturnsElement(): tree → {Tree}", tree);

        tree.GetMax().Should().Be(42);
		_logger.LogDebug("<<< Exiting GetMax_SingleElement_ReturnsElement()");
    }

    [Test]
    public void GetMax_EmptyTree_ThrowsException()
    {
		_logger.LogDebug(">>> Entering GetMax_EmptyTree_ThrowsException()");
        var tree = new BTree<int>();

		_logger.LogDebug("GetMax_EmptyTree_ThrowsException(): tree → {Tree}", tree);
        Invoking(() => tree.GetMax())
            .Should()
            .ThrowExactly<InvalidOperationException>()
            .WithMessage("B-Tree is empty.");
			_logger.LogDebug("<<< Exiting GetMax_EmptyTree_ThrowsException()");
    }

    [Test]
    public void GetKeysInOrder_NonEmptyTree_ReturnsCorrectOrder()
    {
		_logger.LogDebug(">>> Entering GetKeysInOrder_NonEmptyTree_ReturnsCorrectOrder()");
        var tree = new BTree<int>(2);
        tree.AddRange(new[] { 5, 2, 8, 1, 9, 3, 7, 4, 6 });
		_logger.LogDebug("GetKeysInOrder_NonEmptyTree_ReturnsCorrectOrder(): tree → {Tree}", tree);

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting GetKeysInOrder_NonEmptyTree_ReturnsCorrectOrder()");
    }

    [Test]
    public void GetKeysInOrder_EmptyTree_ReturnsEmpty()
    {
		_logger.LogDebug(">>> Entering GetKeysInOrder_EmptyTree_ReturnsEmpty()");
        var tree = new BTree<int>();

		_logger.LogDebug("GetKeysInOrder_EmptyTree_ReturnsEmpty(): tree → {Tree}", tree);
        tree.GetKeysInOrder().Should().BeEmpty();
		_logger.LogDebug("<<< Exiting GetKeysInOrder_EmptyTree_ReturnsEmpty()");
    }

    [Test]
    public void GetKeysInOrder_AfterRemoval_ReturnsCorrectOrder()
    {
		_logger.LogDebug(">>> Entering GetKeysInOrder_AfterRemoval_ReturnsCorrectOrder()");
        var tree = new BTree<int>(2);
        tree.AddRange(Data);
		_logger.LogDebug("GetKeysInOrder_AfterRemoval_ReturnsCorrectOrder(): tree → {Tree}", tree);
        tree.Remove(5);
        tree.Remove(10);

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 1, 2, 3, 4, 6, 7, 8, 9, 11, 12, 13, 14, 15 },
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting GetKeysInOrder_AfterRemoval_ReturnsCorrectOrder()");
    }

    [Test]
    public void GetKeysPreOrder_NonEmptyTree_ReturnsCorrectOrder()
    {
		_logger.LogDebug(">>> Entering GetKeysPreOrder_NonEmptyTree_ReturnsCorrectOrder()");
        var tree = new BTree<int>(2);
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7 });
		_logger.LogDebug("GetKeysPreOrder_NonEmptyTree_ReturnsCorrectOrder(): tree → {Tree}", tree);

        var preOrder = tree.GetKeysPreOrder().ToArray();
		_logger.LogDebug("GetKeysPreOrder_NonEmptyTree_ReturnsCorrectOrder(): preOrder → {PreOrder}", preOrder);

        preOrder.Should().HaveCount(7);
        preOrder.Should().Contain(new[] { 1, 2, 3, 4, 5, 6, 7 });
		_logger.LogDebug("<<< Exiting GetKeysPreOrder_NonEmptyTree_ReturnsCorrectOrder()");
    }

    [Test]
    public void GetKeysPreOrder_EmptyTree_ReturnsEmpty()
    {
		_logger.LogDebug(">>> Entering GetKeysPreOrder_EmptyTree_ReturnsEmpty()");
        var tree = new BTree<int>();

		_logger.LogDebug("GetKeysPreOrder_EmptyTree_ReturnsEmpty(): tree → {Tree}", tree);
        tree.GetKeysPreOrder().Should().BeEmpty();
		_logger.LogDebug("<<< Exiting GetKeysPreOrder_EmptyTree_ReturnsEmpty()");
    }

    [Test]
    public void GetKeysPostOrder_NonEmptyTree_ReturnsCorrectOrder()
    {
		_logger.LogDebug(">>> Entering GetKeysPostOrder_NonEmptyTree_ReturnsCorrectOrder()");
        var tree = new BTree<int>(2);
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7 });
		_logger.LogDebug("GetKeysPostOrder_NonEmptyTree_ReturnsCorrectOrder(): tree → {Tree}", tree);

        var postOrder = tree.GetKeysPostOrder().ToArray();
		_logger.LogDebug("GetKeysPostOrder_NonEmptyTree_ReturnsCorrectOrder(): postOrder → {PostOrder}", postOrder);

        postOrder.Should().HaveCount(7);
        postOrder.Should().Contain(new[] { 1, 2, 3, 4, 5, 6, 7 });
		_logger.LogDebug("<<< Exiting GetKeysPostOrder_NonEmptyTree_ReturnsCorrectOrder()");
    }

    [Test]
    public void GetKeysPostOrder_EmptyTree_ReturnsEmpty()
    {
		_logger.LogDebug(">>> Entering GetKeysPostOrder_EmptyTree_ReturnsEmpty()");
        var tree = new BTree<int>();

		_logger.LogDebug("GetKeysPostOrder_EmptyTree_ReturnsEmpty(): tree → {Tree}", tree);
        tree.GetKeysPostOrder().Should().BeEmpty();
		_logger.LogDebug("<<< Exiting GetKeysPostOrder_EmptyTree_ReturnsEmpty()");
    }

    [Test]
    public void BTree_LargeDataSet_MaintainsCorrectness()
    {
		_logger.LogDebug(">>> Entering BTree_LargeDataSet_MaintainsCorrectness()");
        var tree = new BTree<int>(5);
        var keys = Enumerable.Range(1, 1000).ToArray();
		_logger.LogDebug("BTree_LargeDataSet_MaintainsCorrectness(): keys → {Keys}", keys);
		_logger.LogDebug("BTree_LargeDataSet_MaintainsCorrectness(): tree → {Tree}", tree);

        tree.AddRange(keys);

        tree.Count.Should().Be(1000);
        tree.GetMin().Should().Be(1);
        tree.GetMax().Should().Be(1000);

        var inOrder = tree.GetKeysInOrder().ToArray();
		_logger.LogDebug("BTree_LargeDataSet_MaintainsCorrectness(): inOrder → {InOrder}", inOrder);
        inOrder.Should().BeEquivalentTo(keys, config => config.WithStrictOrdering());

        for (var i = 1; i <= 1000; i++)
        {
            tree.Contains(i).Should().BeTrue();
        }
		_logger.LogDebug("<<< Exiting BTree_LargeDataSet_MaintainsCorrectness()");
    }

    [Test]
    public void BTree_RandomInsertion_MaintainsCorrectness()
    {
		_logger.LogDebug(">>> Entering BTree_RandomInsertion_MaintainsCorrectness()");
        var tree = new BTree<int>(3);
        var random = new Random(42);
		_logger.LogDebug("BTree_RandomInsertion_MaintainsCorrectness(): random → {Random}", random);
        var keys = Enumerable.Range(1, 100)
            .OrderBy(_ => random.Next())
            .ToArray();
			_logger.LogDebug("BTree_RandomInsertion_MaintainsCorrectness(): tree → {Tree}", tree);

        tree.AddRange(keys);

        tree.Count.Should().Be(100);

        var inOrder = tree.GetKeysInOrder().ToArray();
		_logger.LogDebug("BTree_RandomInsertion_MaintainsCorrectness(): inOrder → {InOrder}", inOrder);
        inOrder.Should().BeEquivalentTo(
            Enumerable.Range(1, 100),
            config => config.WithStrictOrdering());
			_logger.LogDebug("<<< Exiting BTree_RandomInsertion_MaintainsCorrectness()");
    }

    [Test]
    public void BTree_RandomDeletion_MaintainsCorrectness()
    {
		_logger.LogDebug(">>> Entering BTree_RandomDeletion_MaintainsCorrectness()");
        var tree = new BTree<int>(3);
        tree.AddRange(Enumerable.Range(1, 50));
		_logger.LogDebug("BTree_RandomDeletion_MaintainsCorrectness(): tree → {Tree}", tree);

        var random = new Random(42);
		_logger.LogDebug("BTree_RandomDeletion_MaintainsCorrectness(): random → {Random}", random);
        var keysToRemove = Enumerable.Range(1, 50)
            .OrderBy(_ => random.Next())
            .Take(25)
            .ToArray();

        foreach (var key in keysToRemove)
        {
            tree.Remove(key);
        }

        tree.Count.Should().Be(25);

        var remainingKeys = Enumerable.Range(1, 50)
            .Except(keysToRemove)
            .OrderBy(x => x)
            .ToArray();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                remainingKeys,
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting BTree_RandomDeletion_MaintainsCorrectness()");
    }

    [Test]
    public void BTree_StringKeys_WorksCorrectly()
    {
		_logger.LogDebug(">>> Entering BTree_StringKeys_WorksCorrectly()");
        var tree = new BTree<string>(2);
        var keys = new[] { "apple", "banana", "cherry", "date", "elderberry", "fig", "grape" };
		_logger.LogDebug("BTree_StringKeys_WorksCorrectly(): keys → {Keys}", keys);
		_logger.LogDebug("BTree_StringKeys_WorksCorrectly(): tree → {Tree}", tree);

        tree.AddRange(keys);

        tree.Count.Should().Be(7);
        tree.GetMin().Should().Be("apple");
        tree.GetMax().Should().Be("grape");

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                keys.OrderBy(x => x),
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting BTree_StringKeys_WorksCorrectly()");
    }

    [Test]
    public void BTree_DifferentMinimumDegrees_AllWorkCorrectly()
    {
		_logger.LogDebug(">>> Entering BTree_DifferentMinimumDegrees_AllWorkCorrectly()");
        for (var degree = 2; degree <= 10; degree++)
        {
            var tree = new BTree<int>(degree);
			_logger.LogDebug("BTree_DifferentMinimumDegrees_AllWorkCorrectly(): tree → {Tree}", tree);
            tree.AddRange(Enumerable.Range(1, 50));

            tree.Count.Should().Be(50);
            tree.GetKeysInOrder()
                .Should()
                .BeEquivalentTo(
                    Enumerable.Range(1, 50),
                    config => config.WithStrictOrdering());
        }
		_logger.LogDebug("<<< Exiting BTree_DifferentMinimumDegrees_AllWorkCorrectly()");
    }

    [Test]
    public void BTree_InsertRemoveInsert_WorksCorrectly()
    {
		_logger.LogDebug(">>> Entering BTree_InsertRemoveInsert_WorksCorrectly()");
        var tree = new BTree<int>(2);

		_logger.LogDebug("BTree_InsertRemoveInsert_WorksCorrectly(): tree → {Tree}", tree);
        tree.AddRange(new[] { 1, 2, 3, 4, 5 });
        tree.Remove(3);
        tree.Add(3);

        tree.Count.Should().Be(5);
        tree.Contains(3).Should().BeTrue();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 1, 2, 3, 4, 5 },
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting BTree_InsertRemoveInsert_WorksCorrectly()");
    }

    [Test]
    public void Remove_BorrowFromPreviousSibling_TreeStillValid()
    {
		_logger.LogDebug(">>> Entering Remove_BorrowFromPreviousSibling_TreeStillValid()");
        var tree = new BTree<int>(3);

		_logger.LogDebug("Remove_BorrowFromPreviousSibling_TreeStillValid(): tree → {Tree}", tree);
        tree.AddRange(new[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 });

        tree.Remove(100);

        tree.Count.Should().Be(9);
        tree.Contains(100).Should().BeFalse();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 10, 20, 30, 40, 50, 60, 70, 80, 90 },
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting Remove_BorrowFromPreviousSibling_TreeStillValid()");
    }

    [Test]
    public void Remove_BorrowFromNextSibling_TreeStillValid()
    {
		_logger.LogDebug(">>> Entering Remove_BorrowFromNextSibling_TreeStillValid()");
        var tree = new BTree<int>(3);

		_logger.LogDebug("Remove_BorrowFromNextSibling_TreeStillValid(): tree → {Tree}", tree);
        tree.AddRange(new[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 });

        tree.Remove(10);

        tree.Count.Should().Be(9);
        tree.Contains(10).Should().BeFalse();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 20, 30, 40, 50, 60, 70, 80, 90, 100 },
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting Remove_BorrowFromNextSibling_TreeStillValid()");
    }

    [Test]
    public void Remove_UsesPredecessor_TreeStillValid()
    {
		_logger.LogDebug(">>> Entering Remove_UsesPredecessor_TreeStillValid()");
        var tree = new BTree<int>(3);

		_logger.LogDebug("Remove_UsesPredecessor_TreeStillValid(): tree → {Tree}", tree);
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });

        var rootKeys = tree.GetKeysPreOrder().Take(3).ToArray();
		_logger.LogDebug("Remove_UsesPredecessor_TreeStillValid(): rootKeys → {RootKeys}", rootKeys);
        var keyToRemove = rootKeys[0];

        tree.Remove(keyToRemove);

        tree.Contains(keyToRemove).Should().BeFalse();
        tree.Count.Should().Be(14);

        var inOrder = tree.GetKeysInOrder().ToArray();
		_logger.LogDebug("Remove_UsesPredecessor_TreeStillValid(): inOrder → {InOrder}", inOrder);
        inOrder.Should().HaveCount(14);
        inOrder.Should().BeInAscendingOrder();
		_logger.LogDebug("<<< Exiting Remove_UsesPredecessor_TreeStillValid()");
    }

    [Test]
    public void Remove_UsesSuccessor_TreeStillValid()
    {
		_logger.LogDebug(">>> Entering Remove_UsesSuccessor_TreeStillValid()");
        var tree = new BTree<int>(2);

		_logger.LogDebug("Remove_UsesSuccessor_TreeStillValid(): tree → {Tree}", tree);
        tree.AddRange(new[] { 10, 20, 30, 40, 50, 60, 70 });

        tree.Remove(40);

        tree.Contains(40).Should().BeFalse();
        tree.Count.Should().Be(6);

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 10, 20, 30, 50, 60, 70 },
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting Remove_UsesSuccessor_TreeStillValid()");
    }

    [Test]
    public void Remove_MergeWithSibling_TreeStillValid()
    {
		_logger.LogDebug(">>> Entering Remove_MergeWithSibling_TreeStillValid()");
        var tree = new BTree<int>(2);

		_logger.LogDebug("Remove_MergeWithSibling_TreeStillValid(): tree → {Tree}", tree);
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

        tree.Remove(9);
        tree.Remove(8);
        tree.Remove(7);

        tree.Count.Should().Be(6);

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 1, 2, 3, 4, 5, 6 },
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting Remove_MergeWithSibling_TreeStillValid()");
    }

    [Test]
    public void Remove_ComplexRebalancing_TreeStillValid()
    {
		_logger.LogDebug(">>> Entering Remove_ComplexRebalancing_TreeStillValid()");
        var tree = new BTree<int>(3);

		_logger.LogDebug("Remove_ComplexRebalancing_TreeStillValid(): tree → {Tree}", tree);
        tree.AddRange(Enumerable.Range(1, 30));

        var keysToRemove = new[] { 5, 15, 25, 10, 20, 30, 1, 29 };

        foreach (var key in keysToRemove)
        {
            tree.Remove(key);
            tree.Contains(key).Should().BeFalse();
        }

        tree.Count.Should().Be(22);

        var expected = Enumerable.Range(1, 30).Except(keysToRemove).OrderBy(x => x);
		_logger.LogDebug("Remove_ComplexRebalancing_TreeStillValid(): expected → {Expected}", expected);
        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                expected,
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting Remove_ComplexRebalancing_TreeStillValid()");
    }

    [Test]
    public void Remove_FromMinimumDegree3_AllRebalancingPaths()
    {
		_logger.LogDebug(">>> Entering Remove_FromMinimumDegree3_AllRebalancingPaths()");
        var tree = new BTree<int>(3);

		_logger.LogDebug("Remove_FromMinimumDegree3_AllRebalancingPaths(): tree → {Tree}", tree);
        tree.AddRange(Enumerable.Range(1, 50));
        var keysToRemove = new[] { 25, 12, 37, 6, 44, 18, 31, 3, 47, 15 };

        foreach (var key in keysToRemove)
        {
            var countBefore = tree.Count;
            tree.Remove(key);

            tree.Count.Should().Be(countBefore - 1);
            tree.Contains(key).Should().BeFalse();

            var inOrder = tree.GetKeysInOrder().ToArray();
			_logger.LogDebug("Remove_FromMinimumDegree3_AllRebalancingPaths(): inOrder → {InOrder}", inOrder);
            inOrder.Should().BeInAscendingOrder();
        }

        tree.Count.Should().Be(40);
		_logger.LogDebug("<<< Exiting Remove_FromMinimumDegree3_AllRebalancingPaths()");
    }

    [Test]
    public void Remove_SequentialFromLargeTree_MaintainsStructure()
    {
		_logger.LogDebug(">>> Entering Remove_SequentialFromLargeTree_MaintainsStructure()");
        var tree = new BTree<int>(4);

		_logger.LogDebug("Remove_SequentialFromLargeTree_MaintainsStructure(): tree → {Tree}", tree);
        tree.AddRange(Enumerable.Range(1, 100));
        for (var i = 3; i <= 99; i += 3)
        {
            tree.Remove(i);
        }

        tree.Count.Should().Be(67);

        var inOrder = tree.GetKeysInOrder().ToArray();
		_logger.LogDebug("Remove_SequentialFromLargeTree_MaintainsStructure(): inOrder → {InOrder}", inOrder);
        inOrder.Should().HaveCount(67);
        inOrder.Should().BeInAscendingOrder();

        tree.Contains(3).Should().BeFalse();
        tree.Contains(6).Should().BeFalse();
        tree.Contains(99).Should().BeFalse();
        tree.Contains(1).Should().BeTrue();
        tree.Contains(2).Should().BeTrue();
        tree.Contains(100).Should().BeTrue();
		_logger.LogDebug("<<< Exiting Remove_SequentialFromLargeTree_MaintainsStructure()");
    }

    [Test]
    public void BTree_DegreeThree_CompleteInsertDeleteCycle()
    {
		_logger.LogDebug(">>> Entering BTree_DegreeThree_CompleteInsertDeleteCycle()");
        var tree = new BTree<int>(3);

		_logger.LogDebug("BTree_DegreeThree_CompleteInsertDeleteCycle(): tree → {Tree}", tree);
        var keys = Enumerable.Range(1, 40).ToArray();
		_logger.LogDebug("BTree_DegreeThree_CompleteInsertDeleteCycle(): keys → {Keys}", keys);
        tree.AddRange(keys);
        tree.Count.Should().Be(40);

        for (var i = 2; i <= 40; i += 2)
        {
            tree.Remove(i);
        }

        tree.Count.Should().Be(20);

        var remaining = tree.GetKeysInOrder().ToArray();
		_logger.LogDebug("BTree_DegreeThree_CompleteInsertDeleteCycle(): remaining → {Remaining}", remaining);
        remaining.Should().BeEquivalentTo(
            Enumerable.Range(1, 40).Where(x => x % 2 != 0),
            config => config.WithStrictOrdering());

        tree.Add(2);
        tree.Add(4);
        tree.Count.Should().Be(22);

        tree.Contains(2).Should().BeTrue();
        tree.Contains(4).Should().BeTrue();
		_logger.LogDebug("<<< Exiting BTree_DegreeThree_CompleteInsertDeleteCycle()");
    }

    [Test]
    public void Remove_RootWithMultipleChildren_HandledCorrectly()
    {
		_logger.LogDebug(">>> Entering Remove_RootWithMultipleChildren_HandledCorrectly()");
        var tree = new BTree<int>(2);

		_logger.LogDebug("Remove_RootWithMultipleChildren_HandledCorrectly(): tree → {Tree}", tree);
        tree.AddRange(new[] { 1, 2, 3, 4, 5 });
        tree.Remove(3);
        tree.Count.Should().Be(4);

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 1, 2, 4, 5 },
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting Remove_RootWithMultipleChildren_HandledCorrectly()");
    }

    [Test]
    public void BTree_HighDegree_StressTest()
    {
		_logger.LogDebug(">>> Entering BTree_HighDegree_StressTest()");
        var tree = new BTree<int>(10);

		_logger.LogDebug("BTree_HighDegree_StressTest(): tree → {Tree}", tree);
        tree.AddRange(Enumerable.Range(1, 200));
        tree.Count.Should().Be(200);

        for (var i = 10; i <= 200; i += 10)
        {
            tree.Remove(i);
        }

        tree.Count.Should().Be(180);

        var inOrder = tree.GetKeysInOrder().ToArray();
		_logger.LogDebug("BTree_HighDegree_StressTest(): inOrder → {InOrder}", inOrder);
        inOrder.Should().HaveCount(180);
        inOrder.Should().BeInAscendingOrder();

        tree.Contains(10).Should().BeFalse();
        tree.Contains(100).Should().BeFalse();
        tree.Contains(200).Should().BeFalse();
		_logger.LogDebug("<<< Exiting BTree_HighDegree_StressTest()");
    }
}
