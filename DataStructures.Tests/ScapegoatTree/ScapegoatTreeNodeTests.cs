using DataStructures.ScapegoatTree;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.ScapegoatTree;

[TestFixture]
public class ScapegoatTreeNodeTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(2, 1)]
    [TestCase("B", "A")]
    public void RightSetter_OtherKeyPrecedesRightKey_ThrowsException<TKey>(TKey a, TKey b)
        where TKey : IComparable
    {
        var instance = new Node<TKey>(a);
        var other = new Node<TKey>(b);

        Assert.Throws<ArgumentException>(() => instance.Right = other);
    }

    [TestCase(1, 2)]
    [TestCase("A", "B")]
    public void RightSetter_OtherKeyFollowsRightKey_AddsChild<TKey>(TKey a, TKey b)
        where TKey : IComparable
    {
        var instance = new Node<TKey>(a);
        var other = new Node<TKey>(b);

        Assert.DoesNotThrow(() => instance.Right = other);
    }

    [TestCase(1, 2)]
    [TestCase("A", "B")]
    public void LeftSetter_OtherKeyFollowsLeftKey_ThrowsException<TKey>(TKey a, TKey b)
        where TKey : IComparable
    {
        var instance = new Node<TKey>(a);
        var other = new Node<TKey>(b);

        Assert.Throws<ArgumentException>(() => instance.Left = other);
    }

    [TestCase(2, 1)]
    [TestCase("B", "A")]
    public void LeftSetter_OtherKeyPrecedesLeftKey_AddsChild<TKey>(TKey a, TKey b)
        where TKey : IComparable
    {
        var instance = new Node<TKey>(a);
        var other = new Node<TKey>(b);

        Assert.DoesNotThrow(() => instance.Left = other);
    }

    [TestCase(1, 2)]
    [TestCase("A", "B")]
    public void CompareTo_InstanceKeyPrecedesOtherKey_ReturnsMinusOne<TKey>(TKey a, TKey b)
        where TKey : IComparable
    {
        var instance = new Node<TKey>(a);
        var other = new Node<TKey>(b);

        var result = instance.Key.CompareTo(other.Key);

        Assert.That(result, Is.EqualTo(-1));
    }

    [TestCase(2, 1)]
    [TestCase("B", "A")]
    public void CompareTo_InstanceKeyFollowsOtherKey_ReturnsOne<TKey>(TKey a, TKey b)
        where TKey : IComparable
    {
        var instance = new Node<TKey>(a);
        var other = new Node<TKey>(b);

        var result = instance.Key.CompareTo(other.Key);

        Assert.That(1, Is.EqualTo(result));
    }

    [TestCase(1, 1)]
    [TestCase("A", "A")]
    public void CompareTo_InstanceKeyEqualsOtherKey_ReturnsZero<TKey>(TKey a, TKey b)
        where TKey : IComparable
    {
        var instance = new Node<TKey>(a);
        var other = new Node<TKey>(b);

        var result = instance.Key.CompareTo(other.Key);

        Assert.That(0, Is.EqualTo(result));
    }

    [Test]
    public void GetSize_NodeHasNoChildren_ReturnsOne()
    {
		_logger.LogError(">>> Entering GetSize_NodeHasNoChildren_ReturnsOne()");
        var node = new Node<int>(1);

		_logger.LogError("GetSize_NodeHasNoChildren_ReturnsOne(): node → {Node}", node);
        Assert.That(1, Is.EqualTo(node.GetSize()));
		_logger.LogWarning("<<< Exiting GetSize_NodeHasNoChildren_ReturnsOne()");
    }

    [Test]
    public void GetSize_NodeHasChildren_ReturnsCorrectSize()
    {
		_logger.LogError(">>> Entering GetSize_NodeHasChildren_ReturnsCorrectSize()");
        var node = new Node<int>(1, new Node<int>(2), new Node<int>(0));

		_logger.LogError("GetSize_NodeHasChildren_ReturnsCorrectSize(): node → {Node}", node);
        Assert.That(3, Is.EqualTo(node.GetSize()));
		_logger.LogWarning("<<< Exiting GetSize_NodeHasChildren_ReturnsCorrectSize()");
    }

    [Test]
    public void GetSmallestKeyNode_NodeHasNoLeftChildren_ReturnsNode()
    {
		_logger.LogError(">>> Entering GetSmallestKeyNode_NodeHasNoLeftChildren_ReturnsNode()");
        var node = new Node<int>(1);

		_logger.LogError("GetSmallestKeyNode_NodeHasNoLeftChildren_ReturnsNode(): node → {Node}", node);
        Assert.That(node, Is.EqualTo(node.GetSmallestKeyNode()));
		_logger.LogWarning("<<< Exiting GetSmallestKeyNode_NodeHasNoLeftChildren_ReturnsNode()");
    }

    [Test]
    public void GetSmallestKeyNode_NodeHasSmallestChild_ReturnsChild()
    {
		_logger.LogError(">>> Entering GetSmallestKeyNode_NodeHasSmallestChild_ReturnsChild()");
        var node = new Node<int>(1);
        var smaller = new Node<int>(0);
		_logger.LogError("GetSmallestKeyNode_NodeHasSmallestChild_ReturnsChild(): smaller → {Smaller}", smaller);
        var smallest = new Node<int>(-1);
		_logger.LogError("GetSmallestKeyNode_NodeHasSmallestChild_ReturnsChild(): smallest → {Smallest}", smallest);
		_logger.LogError("GetSmallestKeyNode_NodeHasSmallestChild_ReturnsChild(): node → {Node}", node);
        node.Left = smaller;
		_logger.LogDebug("GetSmallestKeyNode_NodeHasSmallestChild_ReturnsChild(): node.Left → {Arg0}", node.Left);
        smaller.Left = smallest;
		_logger.LogDebug("GetSmallestKeyNode_NodeHasSmallestChild_ReturnsChild(): smaller.Left → {Arg0}", smaller.Left);

        Assert.That(smallest, Is.EqualTo(node.GetSmallestKeyNode()));
		_logger.LogWarning("<<< Exiting GetSmallestKeyNode_NodeHasSmallestChild_ReturnsChild()");
    }

    [Test]
    public void GetLargestKeyNode_NodeHasNoRightChildren_ReturnsNode()
    {
		_logger.LogError(">>> Entering GetLargestKeyNode_NodeHasNoRightChildren_ReturnsNode()");
        var node = new Node<int>(1);

		_logger.LogError("GetLargestKeyNode_NodeHasNoRightChildren_ReturnsNode(): node → {Node}", node);
        Assert.That(node, Is.EqualTo(node.GetLargestKeyNode()));
		_logger.LogWarning("<<< Exiting GetLargestKeyNode_NodeHasNoRightChildren_ReturnsNode()");
    }

    [Test]
    public void GetLargestKeyNode_NodeHasLargestChild_ReturnsChild()
    {
		_logger.LogError(">>> Entering GetLargestKeyNode_NodeHasLargestChild_ReturnsChild()");
        var node = new Node<int>(1);
        var larger = new Node<int>(2);
		_logger.LogError("GetLargestKeyNode_NodeHasLargestChild_ReturnsChild(): larger → {Larger}", larger);
        var largest = new Node<int>(3);
		_logger.LogError("GetLargestKeyNode_NodeHasLargestChild_ReturnsChild(): largest → {Largest}", largest);
		_logger.LogError("GetLargestKeyNode_NodeHasLargestChild_ReturnsChild(): node → {Node}", node);
        node.Right = larger;
		_logger.LogDebug("GetLargestKeyNode_NodeHasLargestChild_ReturnsChild(): node.Right → {Arg0}", node.Right);
        larger.Right = largest;
		_logger.LogDebug("GetLargestKeyNode_NodeHasLargestChild_ReturnsChild(): larger.Right → {Arg0}", larger.Right);

        Assert.That(largest, Is.EqualTo(node.GetLargestKeyNode()));
		_logger.LogWarning("<<< Exiting GetLargestKeyNode_NodeHasLargestChild_ReturnsChild()");
    }

    [Test]
    public void IsAlphaWeightBalanced_TreeIsUnbalanced_ReturnsFalse()
    {
		_logger.LogError(">>> Entering IsAlphaWeightBalanced_TreeIsUnbalanced_ReturnsFalse()");
        var root = new Node<int>(0);
        var a = new Node<int>(-1);
		_logger.LogError("IsAlphaWeightBalanced_TreeIsUnbalanced_ReturnsFalse(): a → {A}", a);
        var b = new Node<int>(-2);
		_logger.LogError("IsAlphaWeightBalanced_TreeIsUnbalanced_ReturnsFalse(): b → {B}", b);
        var c = new Node<int>(-3);
		_logger.LogError("IsAlphaWeightBalanced_TreeIsUnbalanced_ReturnsFalse(): c → {C}", c);
        var d = new Node<int>(1);
		_logger.LogError("IsAlphaWeightBalanced_TreeIsUnbalanced_ReturnsFalse(): d → {D}", d);
		_logger.LogError("IsAlphaWeightBalanced_TreeIsUnbalanced_ReturnsFalse(): root → {Root}", root);

        root.Left = a;
		_logger.LogDebug("IsAlphaWeightBalanced_TreeIsUnbalanced_ReturnsFalse(): root.Left → {Arg0}", root.Left);
        a.Left = b;
		_logger.LogDebug("IsAlphaWeightBalanced_TreeIsUnbalanced_ReturnsFalse(): a.Left → {Arg0}", a.Left);
        b.Left = c;
		_logger.LogDebug("IsAlphaWeightBalanced_TreeIsUnbalanced_ReturnsFalse(): b.Left → {Arg0}", b.Left);
        root.Right = d;
		_logger.LogDebug("IsAlphaWeightBalanced_TreeIsUnbalanced_ReturnsFalse(): root.Right → {Arg0}", root.Right);

        Assert.That(root.IsAlphaWeightBalanced(0.5), Is.False);
		_logger.LogWarning("<<< Exiting IsAlphaWeightBalanced_TreeIsUnbalanced_ReturnsFalse()");
    }

    [Test]
    public void IsAlphaWeightBalanced_TreeIsBalanced_ReturnsTrue()
    {
		_logger.LogError(">>> Entering IsAlphaWeightBalanced_TreeIsBalanced_ReturnsTrue()");
        var root = new Node<int>(0);
        var a = new Node<int>(-1);
		_logger.LogError("IsAlphaWeightBalanced_TreeIsBalanced_ReturnsTrue(): a → {A}", a);
        var b = new Node<int>(-2);
		_logger.LogError("IsAlphaWeightBalanced_TreeIsBalanced_ReturnsTrue(): b → {B}", b);
        var d = new Node<int>(1);
		_logger.LogError("IsAlphaWeightBalanced_TreeIsBalanced_ReturnsTrue(): d → {D}", d);
		_logger.LogError("IsAlphaWeightBalanced_TreeIsBalanced_ReturnsTrue(): root → {Root}", root);

        root.Left = a;
		_logger.LogDebug("IsAlphaWeightBalanced_TreeIsBalanced_ReturnsTrue(): root.Left → {Arg0}", root.Left);
        a.Left = b;
		_logger.LogDebug("IsAlphaWeightBalanced_TreeIsBalanced_ReturnsTrue(): a.Left → {Arg0}", a.Left);
        root.Right = d;
		_logger.LogDebug("IsAlphaWeightBalanced_TreeIsBalanced_ReturnsTrue(): root.Right → {Arg0}", root.Right);

        Assert.That(root.IsAlphaWeightBalanced(0.5), Is.True);
		_logger.LogWarning("<<< Exiting IsAlphaWeightBalanced_TreeIsBalanced_ReturnsTrue()");
    }
}
