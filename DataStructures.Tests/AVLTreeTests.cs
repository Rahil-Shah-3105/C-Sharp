using DataStructures.AVLTree;
using static FluentAssertions.FluentActions;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests;

internal class AvlTreeTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private static readonly int[] Data = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    private static readonly int[] PreOrder = [4, 2, 1, 3, 8, 6, 5, 7, 9, 10];
    private static readonly int[] PostOrder = [1, 3, 2, 5, 7, 6, 10, 9, 8, 4];

    [Test]
    public void Constructor_UseCustomComparer_FormsCorrectTree()
    {
		_logger.LogDebug(">>> Entering Constructor_UseCustomComparer_FormsCorrectTree()");
        var tree = new AvlTree<int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));

		_logger.LogDebug("Constructor_UseCustomComparer_FormsCorrectTree(): tree → {Tree}", tree);
        tree.AddRange(Data);

        tree.GetMin().Should().Be(10);

        tree.GetMax().Should().Be(1);

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                Data.Reverse(),
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting Constructor_UseCustomComparer_FormsCorrectTree()");
    }

    [Test]
    public void Add_MultipleKeys_FormsCorrectTree()
    {
		_logger.LogDebug(">>> Entering Add_MultipleKeys_FormsCorrectTree()");
        var tree = new AvlTree<int>();

		_logger.LogDebug("Add_MultipleKeys_FormsCorrectTree(): tree → {Tree}", tree);
        for (var i = 0; i < Data.Length; ++i)
        {
            tree.Add(Data[i]);
            tree.Count.Should().Be(i + 1);
        }

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                Data,
                config => config.WithStrictOrdering());

        tree.GetKeysPreOrder()
            .Should()
            .BeEquivalentTo(
                PreOrder,
                config => config.WithStrictOrdering());

        tree.GetKeysPostOrder()
            .Should()
            .BeEquivalentTo(
                PostOrder,
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting Add_MultipleKeys_FormsCorrectTree()");
    }

    [Test]
    public void Add_KeyAlreadyInTree_ThrowsException()
    {
		_logger.LogDebug(">>> Entering Add_KeyAlreadyInTree_ThrowsException()");
        var tree = new AvlTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5 });
		_logger.LogDebug("Add_KeyAlreadyInTree_ThrowsException(): tree → {Tree}", tree);

        Invoking(() => tree.Add(1)).Should().ThrowExactly<ArgumentException>();
		_logger.LogDebug("<<< Exiting Add_KeyAlreadyInTree_ThrowsException()");
    }

    [Test]
    public void AddRange_MultipleKeys_FormsCorrectTree()
    {
		_logger.LogDebug(">>> Entering AddRange_MultipleKeys_FormsCorrectTree()");
        var tree = new AvlTree<char>();
        tree.AddRange(new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
		_logger.LogDebug("AddRange_MultipleKeys_FormsCorrectTree(): tree → {Tree}", tree);

        tree.Count.Should().Be(7);

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' },
                config => config.WithStrictOrdering());

        tree.GetKeysPreOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 'd', 'b', 'a', 'c', 'f', 'e', 'g' },
                config => config.WithStrictOrdering());

        tree.GetKeysPostOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 'a', 'c', 'b', 'e', 'g', 'f', 'd' },
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting AddRange_MultipleKeys_FormsCorrectTree()");
    }

    [Test]
    public void Remove_MultipleKeys_TreeStillValid()
    {
		_logger.LogDebug(">>> Entering Remove_MultipleKeys_TreeStillValid()");
        var tree = new AvlTree<int>();
        tree.AddRange(Data);
		_logger.LogDebug("Remove_MultipleKeys_TreeStillValid(): tree → {Tree}", tree);

        tree.Remove(7);

        tree.Count.Should().Be(9);
        tree.Contains(7).Should().BeFalse();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 1, 2, 3, 4, 5, 6, 8, 9, 10 },
                config => config.WithStrictOrdering());

        tree.GetKeysPreOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 4, 2, 1, 3, 8, 6, 5, 9, 10 },
                config => config.WithStrictOrdering());

        tree.GetKeysPostOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 1, 3, 2, 5, 6, 10, 9, 8, 4 },
                config => config.WithStrictOrdering());

        tree.Remove(2);

        tree.Count.Should().Be(8);
        tree.Contains(2).Should().BeFalse();

        tree.Remove(1);

        tree.Count.Should().Be(7);
        tree.Contains(1).Should().BeFalse();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 3, 4, 5, 6, 8, 9, 10 },
                config => config.WithStrictOrdering());

        tree.GetKeysPreOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 8, 4, 3, 6, 5, 9, 10 },
                config => config.WithStrictOrdering());

        tree.GetKeysPostOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 3, 5, 6, 4, 10, 9, 8 },
                config => config.WithStrictOrdering());

        tree.Remove(9);

        tree.Count.Should().Be(6);
        tree.Contains(9).Should().BeFalse();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 3, 4, 5, 6, 8, 10 },
                config => config.WithStrictOrdering());

        tree.GetKeysPreOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 6, 4, 3, 5, 8, 10 },
                config => config.WithStrictOrdering());

        tree.GetKeysPostOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 3, 5, 4, 10, 8, 6 },
                config => config.WithStrictOrdering());

        tree.Remove(3);
        tree.Remove(4);
        tree.Remove(5);
        tree.Remove(6);
        tree.Remove(8);
        tree.Remove(10);

        tree.Count.Should().Be(0);
        tree.GetKeysInOrder().Should().BeEmpty();
		_logger.LogDebug("<<< Exiting Remove_MultipleKeys_TreeStillValid()");
    }

    [Test]
    public void Remove_MultipleKeys_TreeStillValid_Variant2()
    {
		_logger.LogDebug(">>> Entering Remove_MultipleKeys_TreeStillValid_Variant2()");
        var tree = new AvlTree<int>();
        tree.AddRange(Data);
		_logger.LogDebug("Remove_MultipleKeys_TreeStillValid_Variant2(): tree → {Tree}", tree);

        tree.Remove(10);

        tree.Count.Should().Be(9);
        tree.Contains(10).Should().BeFalse();

        tree.Remove(5);

        tree.Count.Should().Be(8);
        tree.Contains(5).Should().BeFalse();

        tree.Remove(7);

        tree.Count.Should().Be(7);
        tree.Contains(7).Should().BeFalse();

        tree.Remove(9);

        tree.Count.Should().Be(6);
        tree.Contains(9).Should().BeFalse();

        tree.Remove(1);

        tree.Count.Should().Be(5);
        tree.Contains(1).Should().BeFalse();

        tree.Remove(3);

        tree.Count.Should().Be(4);
        tree.Contains(3).Should().BeFalse();

        tree.Remove(2);

        tree.Count.Should().Be(3);
        tree.Contains(2).Should().BeFalse();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 4, 6, 8 },
                config => config.WithStrictOrdering());

        tree.GetKeysPreOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 6, 4, 8 },
                config => config.WithStrictOrdering());

        tree.GetKeysPostOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 4, 8, 6 },
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting Remove_MultipleKeys_TreeStillValid_Variant2()");
    }

    [Test]
    public void Remove_EmptyTree_ThrowsException()
    {
		_logger.LogDebug(">>> Entering Remove_EmptyTree_ThrowsException()");
        var tree = new AvlTree<int>();

		_logger.LogDebug("Remove_EmptyTree_ThrowsException(): tree → {Tree}", tree);
        Invoking(() => tree.Remove(1)).Should().ThrowExactly<KeyNotFoundException>();
		_logger.LogDebug("<<< Exiting Remove_EmptyTree_ThrowsException()");
    }

    [Test]
    public void Remove_KeyNotInTree_ThrowsException()
    {
		_logger.LogDebug(">>> Entering Remove_KeyNotInTree_ThrowsException()");
        var tree = new AvlTree<int>();
        tree.AddRange(Data);
		_logger.LogDebug("Remove_KeyNotInTree_ThrowsException(): tree → {Tree}", tree);

        Invoking(() => tree.Remove(24)).Should().ThrowExactly<KeyNotFoundException>();
		_logger.LogDebug("<<< Exiting Remove_KeyNotInTree_ThrowsException()");
    }

    [Test]
    public void Contains_CorrectReturn()
    {
		_logger.LogDebug(">>> Entering Contains_CorrectReturn()");
        var tree = new AvlTree<int>();

		_logger.LogDebug("Contains_CorrectReturn(): tree → {Tree}", tree);
        tree.AddRange(Data);

        tree.Contains(3).Should().BeTrue();
        tree.Contains(7).Should().BeTrue();
        tree.Contains(24).Should().BeFalse();
        tree.Contains(-1).Should().BeFalse();
		_logger.LogDebug("<<< Exiting Contains_CorrectReturn()");
    }

    [Test]
    public void Contains_EmptyTree_ReturnsFalse()
    {
		_logger.LogDebug(">>> Entering Contains_EmptyTree_ReturnsFalse()");
        var tree = new AvlTree<int>();

		_logger.LogDebug("Contains_EmptyTree_ReturnsFalse(): tree → {Tree}", tree);
        tree.Contains(5).Should().BeFalse();
        tree.Contains(-12).Should().BeFalse();
		_logger.LogDebug("<<< Exiting Contains_EmptyTree_ReturnsFalse()");
    }

    [Test]
    public void GetMin_CorrectReturn()
    {
		_logger.LogDebug(">>> Entering GetMin_CorrectReturn()");
        var tree = new AvlTree<int>();
        tree.AddRange(Data);
		_logger.LogDebug("GetMin_CorrectReturn(): tree → {Tree}", tree);

        tree.GetMin().Should().Be(1);
		_logger.LogDebug("<<< Exiting GetMin_CorrectReturn()");
    }

    [Test]
    public void GetMin_EmptyTree_ThrowsException()
    {
		_logger.LogDebug(">>> Entering GetMin_EmptyTree_ThrowsException()");
        var tree = new AvlTree<int>();

		_logger.LogDebug("GetMin_EmptyTree_ThrowsException(): tree → {Tree}", tree);
        Invoking(() => tree.GetMin()).Should().ThrowExactly<InvalidOperationException>();
		_logger.LogDebug("<<< Exiting GetMin_EmptyTree_ThrowsException()");
    }

    [Test]
    public void GetMax_CorrectReturn()
    {
		_logger.LogDebug(">>> Entering GetMax_CorrectReturn()");
        var tree = new AvlTree<int>();
        tree.AddRange(Data);
		_logger.LogDebug("GetMax_CorrectReturn(): tree → {Tree}", tree);

        tree.GetMax().Should().Be(10);
		_logger.LogDebug("<<< Exiting GetMax_CorrectReturn()");
    }

    [Test]
    public void GetMax_EmptyTree_ThrowsException()
    {
		_logger.LogDebug(">>> Entering GetMax_EmptyTree_ThrowsException()");
        var tree = new AvlTree<int>();

		_logger.LogDebug("GetMax_EmptyTree_ThrowsException(): tree → {Tree}", tree);
        Invoking(() => tree.GetMax()).Should().ThrowExactly<InvalidOperationException>();
		_logger.LogDebug("<<< Exiting GetMax_EmptyTree_ThrowsException()");
    }

    [Test]
    public void GetKeysInOrder_CorrectReturn()
    {
		_logger.LogDebug(">>> Entering GetKeysInOrder_CorrectReturn()");
        var tree = new AvlTree<int>();
        tree.AddRange(Data);
		_logger.LogDebug("GetKeysInOrder_CorrectReturn(): tree → {Tree}", tree);

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                Data,
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting GetKeysInOrder_CorrectReturn()");
    }

    [Test]
    public void GetKeysInOrder_EmptyTree_CorrectReturn()
    {
		_logger.LogDebug(">>> Entering GetKeysInOrder_EmptyTree_CorrectReturn()");
        var tree = new AvlTree<int>();

		_logger.LogDebug("GetKeysInOrder_EmptyTree_CorrectReturn(): tree → {Tree}", tree);
        tree.GetKeysInOrder().Should().BeEmpty();
		_logger.LogDebug("<<< Exiting GetKeysInOrder_EmptyTree_CorrectReturn()");
    }

    [Test]
    public void GetKeysPreOrder_CorrectReturn()
    {
		_logger.LogDebug(">>> Entering GetKeysPreOrder_CorrectReturn()");
        var tree = new AvlTree<int>();

		_logger.LogDebug("GetKeysPreOrder_CorrectReturn(): tree → {Tree}", tree);
        tree.AddRange(Data);

        tree.GetKeysPreOrder()
            .Should()
            .BeEquivalentTo(
                PreOrder,
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting GetKeysPreOrder_CorrectReturn()");
    }

    [Test]
    public void GetKeysPreOrder_EmptyTree_CorrectReturn()
    {
		_logger.LogDebug(">>> Entering GetKeysPreOrder_EmptyTree_CorrectReturn()");
        var tree = new AvlTree<int>();

		_logger.LogDebug("GetKeysPreOrder_EmptyTree_CorrectReturn(): tree → {Tree}", tree);
        tree.GetKeysPreOrder().Should().BeEmpty();
		_logger.LogDebug("<<< Exiting GetKeysPreOrder_EmptyTree_CorrectReturn()");
    }

    [Test]
    public void GetKeysPostOrder_CorrectReturn()
    {
		_logger.LogDebug(">>> Entering GetKeysPostOrder_CorrectReturn()");
        var tree = new AvlTree<int>();
        tree.AddRange(Data);
		_logger.LogDebug("GetKeysPostOrder_CorrectReturn(): tree → {Tree}", tree);

        tree.GetKeysPostOrder()
            .Should()
            .BeEquivalentTo(
                PostOrder,
                config => config.WithStrictOrdering());
				_logger.LogDebug("<<< Exiting GetKeysPostOrder_CorrectReturn()");
    }

    [Test]
    public void GetKeysPostOrder_EmptyTree_CorrectReturn()
    {
		_logger.LogDebug(">>> Entering GetKeysPostOrder_EmptyTree_CorrectReturn()");
        var tree = new AvlTree<int>();

		_logger.LogDebug("GetKeysPostOrder_EmptyTree_CorrectReturn(): tree → {Tree}", tree);
        tree.GetKeysPostOrder().Should().BeEmpty();
		_logger.LogDebug("<<< Exiting GetKeysPostOrder_EmptyTree_CorrectReturn()");
    }
}
