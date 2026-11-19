using DataStructures.AATree;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests;

internal class AaTreeTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void Constructor_UseCustomComparer_FormsCorrectTree()
    {
		_logger.LogError(">>> Entering Constructor_UseCustomComparer_FormsCorrectTree()");
        var tree = new AaTree<int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
		_logger.LogError("Constructor_UseCustomComparer_FormsCorrectTree(): tree → {Tree}", tree);
        tree.GetMax().Should().Be(1);
        tree.GetMin().Should().Be(10);
        tree.GetKeysInOrder().SequenceEqual(new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }).Should().BeTrue();
        Validate(tree.Root);
		_logger.LogWarning("<<< Exiting Constructor_UseCustomComparer_FormsCorrectTree()");
    }

    [Test]
    public void Add_MultipleKeys_FormsCorrectTree()
    {
		_logger.LogError(">>> Entering Add_MultipleKeys_FormsCorrectTree()");
        var tree = new AaTree<int>();

		_logger.LogError("Add_MultipleKeys_FormsCorrectTree(): tree → {Tree}", tree);
        foreach (var elem in new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })
        {
            tree.Add(elem);
            tree.Count.Should().Be(elem);
            tree.Contains(elem).Should().BeTrue();
        }

        tree.GetKeysInOrder().SequenceEqual(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }).Should().BeTrue();
        tree.GetKeysPostOrder().SequenceEqual(new[] { 1, 3, 2, 5, 7, 10, 9, 8, 6, 4 }).Should().BeTrue();
        Validate(tree.Root);
		_logger.LogWarning("<<< Exiting Add_MultipleKeys_FormsCorrectTree()");
    }

    [Test]
    public void Add_KeyAlreadyInTree_ThrowsException()
    {
		_logger.LogError(">>> Entering Add_KeyAlreadyInTree_ThrowsException()");
        var tree = new AaTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
		_logger.LogError("Add_KeyAlreadyInTree_ThrowsException(): tree → {Tree}", tree);
        Assert.Throws<ArgumentException>(() => tree.Add(1));
		_logger.LogWarning("<<< Exiting Add_KeyAlreadyInTree_ThrowsException()");
    }

    [Test]
    public void AddRange_MultipleKeys_FormsCorrectTree()
    {
		_logger.LogError(">>> Entering AddRange_MultipleKeys_FormsCorrectTree()");
        var tree = new AaTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
		_logger.LogError("AddRange_MultipleKeys_FormsCorrectTree(): tree → {Tree}", tree);
        tree.Count.Should().Be(10);
        tree.GetKeysInOrder().SequenceEqual(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }).Should().BeTrue();
        tree.GetKeysPostOrder().SequenceEqual(new[] { 1, 3, 2, 5, 7, 10, 9, 8, 6, 4 }).Should().BeTrue();
        Validate(tree.Root);
		_logger.LogWarning("<<< Exiting AddRange_MultipleKeys_FormsCorrectTree()");
    }

    [Test]
    public void Remove_MultipleKeys_TreeStillValid()
    {
		_logger.LogError(">>> Entering Remove_MultipleKeys_TreeStillValid()");
        var tree = new AaTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
		_logger.LogError("Remove_MultipleKeys_TreeStillValid(): tree → {Tree}", tree);

        Remove(4).Should().NotThrow();
        tree.Contains(4).Should().BeFalse();
        tree.Count.Should().Be(9);

        Remove(8).Should().NotThrow();
        tree.Contains(8).Should().BeFalse();
        tree.Count.Should().Be(8);

        Remove(1).Should().NotThrow();
        tree.Contains(1).Should().BeFalse();
        tree.Count.Should().Be(7);

        Validate(tree.Root);

        Action Remove(int x) => () => tree.Remove(x);
		_logger.LogWarning("<<< Exiting Remove_MultipleKeys_TreeStillValid()");
    }

    [Test]
    public void Remove_KeyNotInTree_Throws()
    {
		_logger.LogError(">>> Entering Remove_KeyNotInTree_Throws()");
        var tree = new AaTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
		_logger.LogError("Remove_KeyNotInTree_Throws(): tree → {Tree}", tree);

        Action act = () => tree.Remove(999);
		_logger.LogError("Remove_KeyNotInTree_Throws(): act → {Act}", act);
        act.Should().Throw<InvalidOperationException>();
		_logger.LogWarning("<<< Exiting Remove_KeyNotInTree_Throws()");
    }

    [Test]
    public void Remove_EmptyTree_Throws()
    {
		_logger.LogError(">>> Entering Remove_EmptyTree_Throws()");
        var tree = new AaTree<int>();

		_logger.LogError("Remove_EmptyTree_Throws(): tree → {Tree}", tree);
        Action act = () => tree.Remove(999);
		_logger.LogError("Remove_EmptyTree_Throws(): act → {Act}", act);
        act.Should().Throw<InvalidOperationException>();
		_logger.LogWarning("<<< Exiting Remove_EmptyTree_Throws()");
    }

    [Test]
    public void Contains_NonEmptyTree_ReturnsCorrectAnswer()
    {
		_logger.LogError(">>> Entering Contains_NonEmptyTree_ReturnsCorrectAnswer()");
        var tree = new AaTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
		_logger.LogError("Contains_NonEmptyTree_ReturnsCorrectAnswer(): tree → {Tree}", tree);
        tree.Contains(6).Should().BeTrue();
        tree.Contains(999).Should().BeFalse();
		_logger.LogWarning("<<< Exiting Contains_NonEmptyTree_ReturnsCorrectAnswer()");
    }

    [Test]
    public void Contains_EmptyTree_ReturnsFalse()
    {
		_logger.LogError(">>> Entering Contains_EmptyTree_ReturnsFalse()");
        var tree = new AaTree<int>();
		_logger.LogWarning("<<< Exiting Contains_EmptyTree_ReturnsFalse()");
		_logger.LogError("Contains_EmptyTree_ReturnsFalse(): tree → {Tree}", tree);
        tree.Contains(999).Should().BeFalse();
    }

    [Test]
    public void GetMax_NonEmptyTree_ReturnsCorrectAnswer()
    {
		_logger.LogError(">>> Entering GetMax_NonEmptyTree_ReturnsCorrectAnswer()");
        var tree = new AaTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
		_logger.LogError("GetMax_NonEmptyTree_ReturnsCorrectAnswer(): tree → {Tree}", tree);
        tree.GetMax().Should().Be(10);
		_logger.LogWarning("<<< Exiting GetMax_NonEmptyTree_ReturnsCorrectAnswer()");
    }

    [Test]
    public void GetMax_EmptyTree_ThrowsCorrectException()
    {
		_logger.LogError(">>> Entering GetMax_EmptyTree_ThrowsCorrectException()");
        var tree = new AaTree<int>();
		_logger.LogWarning("<<< Exiting GetMax_EmptyTree_ThrowsCorrectException()");
		_logger.LogError("GetMax_EmptyTree_ThrowsCorrectException(): tree → {Tree}", tree);
        Assert.Throws<InvalidOperationException>(() => tree.GetMax());
    }

    [Test]
    public void GetMin_NonEmptyTree_ReturnsCorrectAnswer()
    {
		_logger.LogError(">>> Entering GetMin_NonEmptyTree_ReturnsCorrectAnswer()");
        var tree = new AaTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
		_logger.LogError("GetMin_NonEmptyTree_ReturnsCorrectAnswer(): tree → {Tree}", tree);
        tree.GetMin().Should().Be(1);
		_logger.LogWarning("<<< Exiting GetMin_NonEmptyTree_ReturnsCorrectAnswer()");
    }

    [Test]
    public void GetMin_EmptyTree_ThrowsCorrectException()
    {
		_logger.LogError(">>> Entering GetMin_EmptyTree_ThrowsCorrectException()");
        var tree = new AaTree<int>();
		_logger.LogWarning("<<< Exiting GetMin_EmptyTree_ThrowsCorrectException()");
		_logger.LogError("GetMin_EmptyTree_ThrowsCorrectException(): tree → {Tree}", tree);
        Assert.Throws<InvalidOperationException>(() => tree.GetMin());
    }

    [Test]
    public void GetKeysInOrder_NonEmptyTree_ReturnsCorrectAnswer()
    {
		_logger.LogError(">>> Entering GetKeysInOrder_NonEmptyTree_ReturnsCorrectAnswer()");
        var tree = new AaTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
		_logger.LogError("GetKeysInOrder_NonEmptyTree_ReturnsCorrectAnswer(): tree → {Tree}", tree);
        tree.GetKeysInOrder().SequenceEqual(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }).Should().BeTrue();
		_logger.LogWarning("<<< Exiting GetKeysInOrder_NonEmptyTree_ReturnsCorrectAnswer()");
    }

    [Test]
    public void GetKeysInOrder_EmptyTree_ReturnsCorrectAnswer()
    {
		_logger.LogError(">>> Entering GetKeysInOrder_EmptyTree_ReturnsCorrectAnswer()");
        var tree = new AaTree<int>();
		_logger.LogWarning("<<< Exiting GetKeysInOrder_EmptyTree_ReturnsCorrectAnswer()");
		_logger.LogError("GetKeysInOrder_EmptyTree_ReturnsCorrectAnswer(): tree → {Tree}", tree);
        tree.GetKeysInOrder().ToList().Count.Should().Be(0);
    }

    [Test]
    public void GetKeysPreOrder_NonEmptyTree_ReturnsCorrectAnswer()
    {
		_logger.LogError(">>> Entering GetKeysPreOrder_NonEmptyTree_ReturnsCorrectAnswer()");
        var tree = new AaTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
		_logger.LogError("GetKeysPreOrder_NonEmptyTree_ReturnsCorrectAnswer(): tree → {Tree}", tree);
        tree.GetKeysPreOrder().SequenceEqual(new[] { 4, 2, 1, 3, 6, 5, 8, 7, 9, 10 })
            .Should().BeTrue();
			_logger.LogWarning("<<< Exiting GetKeysPreOrder_NonEmptyTree_ReturnsCorrectAnswer()");
    }

    [Test]
    public void GetKeysPreOrder_EmptyTree_ReturnsCorrectAnswer()
    {
		_logger.LogError(">>> Entering GetKeysPreOrder_EmptyTree_ReturnsCorrectAnswer()");
        var tree = new AaTree<int>();
		_logger.LogWarning("<<< Exiting GetKeysPreOrder_EmptyTree_ReturnsCorrectAnswer()");
		_logger.LogError("GetKeysPreOrder_EmptyTree_ReturnsCorrectAnswer(): tree → {Tree}", tree);
        tree.GetKeysPreOrder().ToList().Count.Should().Be(0);
    }

    [Test]
    public void GetKeysPostOrder_NonEmptyTree_ReturnsCorrectAnswer()
    {
		_logger.LogError(">>> Entering GetKeysPostOrder_NonEmptyTree_ReturnsCorrectAnswer()");
        var tree = new AaTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
		_logger.LogError("GetKeysPostOrder_NonEmptyTree_ReturnsCorrectAnswer(): tree → {Tree}", tree);
        tree.GetKeysPostOrder().SequenceEqual(new[] { 1, 3, 2, 5, 7, 10, 9, 8, 6, 4 })
            .Should().BeTrue();
			_logger.LogWarning("<<< Exiting GetKeysPostOrder_NonEmptyTree_ReturnsCorrectAnswer()");
    }

    [Test]
    public void GetKeysPostOrder_EmptyTree_ReturnsCorrectAnswer()
    {
		_logger.LogError(">>> Entering GetKeysPostOrder_EmptyTree_ReturnsCorrectAnswer()");
        var tree = new AaTree<int>();
		_logger.LogWarning("<<< Exiting GetKeysPostOrder_EmptyTree_ReturnsCorrectAnswer()");
		_logger.LogError("GetKeysPostOrder_EmptyTree_ReturnsCorrectAnswer(): tree → {Tree}", tree);
        tree.GetKeysPostOrder().ToList().Count.Should().Be(0);
    }

    /// <summary>
    ///     Checks various properties to determine if the tree is a valid AA Tree.
    ///     Throws exceptions if properties are violated.
    ///     Useful for debugging.
    /// </summary>
    /// <remarks>
    ///     The properties that are checked are:
    ///     <list type="number">
    ///         <item>The level of every leaf node is one.</item>
    ///         <item>The level of every left child is exactly one less than that of its parent.</item>
    ///         <item>The level of every right child is equal to or one less than that of its parent.</item>
    ///         <item>The level of every right grandchild is strictly less than that of its grandparent.</item>
    ///         <item>Every node of level greater than one has two children.</item>
    ///     </list>
    ///     More information: https://en.wikipedia.org/wiki/AA_tree .
    /// </remarks>
    /// <param name="node">The node to check from.</param>
    /// <returns>true if node passes all checks, false otherwise.</returns>
    private static bool Validate<T>(AaTreeNode<T>? node)
    {
		_logger.LogError(">>> Entering Validate(node={Node})", node);
        if (node is null)
        {
            return true;
        }

        // Check level == 1 if node if a leaf node.
        var leafNodeCheck = CheckLeafNode(node);
		_logger.LogError("Validate(node={Node}): leafNodeCheck → {LeafNodeCheck}", node, leafNodeCheck);

        // Check level of left child is exactly one less than parent.
        var leftCheck = CheckLeftSubtree(node);
		_logger.LogError("Validate(node={Node}): leftCheck → {LeftCheck}", node, leftCheck);

        // Check level of right child is equal or one less than parent.
        var rightCheck = CheckRightSubtree(node);
		_logger.LogError("Validate(node={Node}): rightCheck → {RightCheck}", node, rightCheck);

        // Check right grandchild level is less than node.
        var grandchildCheck = CheckRightGrandChild(node);
		_logger.LogError("Validate(node={Node}): grandchildCheck → {GrandchildCheck}", node, grandchildCheck);

        // Check if node has two children if not leaf.
        var nonLeafChildrenCheck = CheckNonLeafChildren(node);
		_logger.LogError("Validate(node={Node}): nonLeafChildrenCheck → {NonLeafChildrenCheck}", node, nonLeafChildrenCheck);

        var thisNodeResult = leafNodeCheck && leftCheck && rightCheck;
        thisNodeResult = thisNodeResult && grandchildCheck && nonLeafChildrenCheck;
		_logger.LogError("Validate(node={Node}): thisNodeResult → {ThisNodeResult}", node, thisNodeResult);

		_logger.LogWarning("<<< Exiting Validate(node={Node})", node);
        return thisNodeResult && Validate(node.Left) && Validate(node.Right);
    }

    /// <summary>
    ///     Checks if node is a leaf, and if so if its level is 1.
    /// </summary>
    /// <param name="node">The node to check.</param>
    /// <returns>true if node passes check, false otherwise.</returns>
    private static bool CheckLeafNode<T>(AaTreeNode<T> node)
    {
		_logger.LogError(">>> Entering CheckLeafNode(node={Node})", node);
        var condition = node.Left is null && node.Right is null && node.Level != 1;
		_logger.LogWarning("<<< Exiting CheckLeafNode(node={Node})", node);
        return !condition;
    }

    /// <summary>
    ///     Checks if left node's level is exactly one less than node's level.
    /// </summary>
    /// <param name="node">The node to check.</param>
    /// <returns>true if node passes check, false otherwise.</returns>
    private static bool CheckLeftSubtree<T>(AaTreeNode<T> node)
    {
		_logger.LogError(">>> Entering CheckLeftSubtree(node={Node})", node);
        var condition = node.Left is not null && node.Level - node.Left.Level != 1;
		_logger.LogWarning("<<< Exiting CheckLeftSubtree(node={Node})", node);
        return !condition;
    }

    /// <summary>
    ///     Checks if right node's level is either equal to or one less than node's level.
    /// </summary>
    /// <param name="node">The node to check.</param>
    /// <returns>true if node passes check, false otherwise.</returns>
    private static bool CheckRightSubtree<T>(AaTreeNode<T> node)
    {
		_logger.LogError(">>> Entering CheckRightSubtree(node={Node})", node);
        var condition = node.Right is not null &&
                        node.Level - node.Right.Level != 1 &&
                        node.Level != node.Right.Level;
						_logger.LogWarning("<<< Exiting CheckRightSubtree(node={Node})", node);
        return !condition;
    }

    /// <summary>
    ///     Checks if right grandchild's (right node's right node) level is less than node.
    /// </summary>
    /// <param name="node">The node to check.</param>
    /// <returns>true if node passes check, false otherwise.</returns>
    private static bool CheckRightGrandChild<T>(AaTreeNode<T> node)
    {
		_logger.LogError(">>> Entering CheckRightGrandChild(node={Node})", node);
        var condition = node.Right?.Right is not null && node.Right.Level < node.Right.Right.Level;
		_logger.LogWarning("<<< Exiting CheckRightGrandChild(node={Node})", node);
        return !condition;
    }

    /// <summary>
    ///     Checks if node is not a leaf, and if so if it has two children.
    /// </summary>
    /// <param name="node">The node to check.</param>
    /// <returns>true if node passes check, false otherwise.</returns>
    private static bool CheckNonLeafChildren<T>(AaTreeNode<T> node)
    {
		_logger.LogError(">>> Entering CheckNonLeafChildren(node={Node})", node);
        var condition = node.Level > 1 && (node.Left is null || node.Right is null);
		_logger.LogWarning("<<< Exiting CheckNonLeafChildren(node={Node})", node);
		_logger.LogError("CheckNonLeafChildren(node={Node}): condition → {Condition}", node, condition);
        return !condition;
    }
}
