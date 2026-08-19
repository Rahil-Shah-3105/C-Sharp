using DataStructures.ScapegoatTree;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.ScapegoatTree;

public class ScapegoatTreeTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void Constructor_NoParameters_InstanceIsValid()
    {
		_logger.LogDebug(">>> Entering Constructor_NoParameters_InstanceIsValid()");
        var tree = new ScapegoatTree<int>();

		_logger.LogDebug("Constructor_NoParameters_InstanceIsValid(): tree → {Tree}", tree);
        Assert.That(tree.Root, Is.Null);
        Assert.That(tree.Size == 0, Is.True);
        Assert.That(tree.MaxSize == 0, Is.True);
        Assert.That(tree.Alpha, Is.EqualTo(0.5));
		_logger.LogDebug("<<< Exiting Constructor_NoParameters_InstanceIsValid()");
    }

    [Test]
    public void Constructor_AlphaParameter_InstanceIsValid()
    {
		_logger.LogDebug(">>> Entering Constructor_AlphaParameter_InstanceIsValid()");
        var expected = 0.6;

        var tree = new ScapegoatTree<int>(expected);
		_logger.LogDebug("Constructor_AlphaParameter_InstanceIsValid(): tree → {Tree}", tree);

        Assert.That(tree.Root, Is.Null);
        Assert.That(tree.Size == 0, Is.True);
        Assert.That(tree.MaxSize == 0, Is.True);
        Assert.That(tree.Alpha, Is.EqualTo(expected));
		_logger.LogDebug("<<< Exiting Constructor_AlphaParameter_InstanceIsValid()");
    }

    [TestCase(1.1)]
    [TestCase(0.4)]
    public void Constructor_AlphaParameterIsInvalid_ThrowsException(double alpha)
    {
		_logger.LogDebug(">>> Entering Constructor_AlphaParameterIsInvalid_ThrowsException(alpha={Alpha})", alpha);
        Assert.Throws<ArgumentException>(() => new ScapegoatTree<int>(alpha));
        Assert.Throws<ArgumentException>(() => new ScapegoatTree<int>(1, alpha));
		_logger.LogDebug("<<< Exiting Constructor_AlphaParameterIsInvalid_ThrowsException(alpha={Alpha})", alpha);
    }

    [Test]
    public void Constructor_KeyParameter_InstanceIsValid()
    {
		_logger.LogDebug(">>> Entering Constructor_KeyParameter_InstanceIsValid()");
        var expected = 10;

        var tree = new ScapegoatTree<int>(expected);
		_logger.LogDebug("Constructor_KeyParameter_InstanceIsValid(): tree → {Tree}", tree);

        Assert.That(tree.Root, Is.Not.Null);
        Assert.That(tree.Root!.Key == expected, Is.True);
        Assert.That(tree.Size == 1, Is.True);
        Assert.That(tree.MaxSize == 1, Is.True);
        Assert.That(tree.Alpha, Is.EqualTo(0.5));
		_logger.LogDebug("<<< Exiting Constructor_KeyParameter_InstanceIsValid()");
    }

    [Test]
    public void Constructor_KeyAndAlphaParameters_InstanceIsValid()
    {
		_logger.LogDebug(">>> Entering Constructor_KeyAndAlphaParameters_InstanceIsValid()");
        var key = 10;
        var alpha = 0.8;
		_logger.LogWarning("Constructor_KeyAndAlphaParameters_InstanceIsValid(): key → {Key}", key);

        var tree = new ScapegoatTree<int>(key, alpha);
		_logger.LogDebug("Constructor_KeyAndAlphaParameters_InstanceIsValid(): tree → {Tree}", tree);

        Assert.That(tree.Root, Is.Not.Null);
        Assert.That(tree.Size == 1, Is.True);
        Assert.That(tree.MaxSize == 1, Is.True);
        Assert.That(tree.Alpha, Is.EqualTo(alpha));
		_logger.LogDebug("<<< Exiting Constructor_KeyAndAlphaParameters_InstanceIsValid()");
    }

    [Test]
    public void Constructor_NodeAndAlphaParameters_InstanceIsValid()
    {
		_logger.LogDebug(">>> Entering Constructor_NodeAndAlphaParameters_InstanceIsValid()");
        var node = new Node<int>(10, new Node<int>(11), new Node<int>(1));
        var alpha = 0.8;
		_logger.LogDebug("Constructor_NodeAndAlphaParameters_InstanceIsValid(): node → {Node}", node);

        var tree = new ScapegoatTree<int>(node, alpha);
		_logger.LogDebug("Constructor_NodeAndAlphaParameters_InstanceIsValid(): tree → {Tree}", tree);

        Assert.That(tree.Root, Is.Not.Null);
        Assert.That(tree.Size == 3, Is.True);
        Assert.That(tree.MaxSize == 3, Is.True);
        Assert.That(tree.Alpha, Is.EqualTo(alpha));
		_logger.LogDebug("<<< Exiting Constructor_NodeAndAlphaParameters_InstanceIsValid()");
    }

    [Test]
    public void IsAlphaWeightBalanced_RootIsNull_ReturnsTrue()
    {
		_logger.LogDebug(">>> Entering IsAlphaWeightBalanced_RootIsNull_ReturnsTrue()");
        var tree = new ScapegoatTree<int>();

		_logger.LogDebug("IsAlphaWeightBalanced_RootIsNull_ReturnsTrue(): tree → {Tree}", tree);
        var result = tree.IsAlphaWeightBalanced();
		_logger.LogDebug("IsAlphaWeightBalanced_RootIsNull_ReturnsTrue(): result → {Result}", result);

        Assert.That(result, Is.True);
		_logger.LogDebug("<<< Exiting IsAlphaWeightBalanced_RootIsNull_ReturnsTrue()");
    }

    [Test]
    public void Search_RootIsNull_ReturnsNull()
    {
		_logger.LogDebug(">>> Entering Search_RootIsNull_ReturnsNull()");
        var tree = new ScapegoatTree<int>();

		_logger.LogDebug("Search_RootIsNull_ReturnsNull(): tree → {Tree}", tree);
        var result = tree.Search(1);
		_logger.LogDebug("Search_RootIsNull_ReturnsNull(): result → {Result}", result);

        Assert.That(result, Is.Null);
		_logger.LogDebug("<<< Exiting Search_RootIsNull_ReturnsNull()");
    }

    [Test]
    public void Search_KeyIsPresent_ReturnsKey()
    {
		_logger.LogDebug(">>> Entering Search_KeyIsPresent_ReturnsKey()");
        var tree = new ScapegoatTree<int>(key: 1);

		_logger.LogDebug("Search_KeyIsPresent_ReturnsKey(): tree → {Tree}", tree);
        var result = tree.Search(1);
		_logger.LogDebug("Search_KeyIsPresent_ReturnsKey(): result → {Result}", result);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Key, Is.EqualTo(1));
		_logger.LogDebug("<<< Exiting Search_KeyIsPresent_ReturnsKey()");
    }

    [TestCase(-2)]
    [TestCase(3)]
    public void Search_KeyIsNotPresent_ReturnsNull(int key)
    {
		_logger.LogDebug(">>> Entering Search_KeyIsNotPresent_ReturnsNull(key={Key})", key);
        var root = new Node<int>(1, new Node<int>(2), new Node<int>(-1));

		_logger.LogWarning("Search_KeyIsNotPresent_ReturnsNull(key={Key}): root → {Root}", key, root);
        var tree = new ScapegoatTree<int>(root, 0.5);
		_logger.LogDebug("Search_KeyIsNotPresent_ReturnsNull(key={Key}): tree → {Tree}", key, tree);

        var result = tree.Search(key);
		_logger.LogDebug("Search_KeyIsNotPresent_ReturnsNull(key={Key}): result → {Result}", key, result);

        Assert.That(result, Is.Null);
		_logger.LogDebug("<<< Exiting Search_KeyIsNotPresent_ReturnsNull(key={Key})", key);
    }

    [Test]
    public void Insert_RootIsNull_InsertsRoot()
    {
		_logger.LogDebug(">>> Entering Insert_RootIsNull_InsertsRoot()");
        var tree = new ScapegoatTree<int>();

		_logger.LogDebug("Insert_RootIsNull_InsertsRoot(): tree → {Tree}", tree);
        var inserted = tree.Insert(1);
		_logger.LogDebug("Insert_RootIsNull_InsertsRoot(): inserted → {Inserted}", inserted);

        Assert.That(inserted, Is.True);
        Assert.That(tree.Root, Is.Not.Null);
        Assert.That(tree.Root!.Key, Is.EqualTo(1));
        Assert.That(tree.Size, Is.EqualTo(1));
        Assert.That(tree.MaxSize, Is.EqualTo(1));
		_logger.LogDebug("<<< Exiting Insert_RootIsNull_InsertsRoot()");
    }

    [Test]
    public void Delete_RootIsNull_ReturnsFalse()
    {
		_logger.LogDebug(">>> Entering Delete_RootIsNull_ReturnsFalse()");
        var tree = new ScapegoatTree<int>();

		_logger.LogDebug("Delete_RootIsNull_ReturnsFalse(): tree → {Tree}", tree);
        var deleted = tree.Delete(1);
		_logger.LogDebug("Delete_RootIsNull_ReturnsFalse(): deleted → {Deleted}", deleted);

        Assert.That(deleted, Is.False);
		_logger.LogDebug("<<< Exiting Delete_RootIsNull_ReturnsFalse()");
    }

    [Test]
    public void Delete_KeyIsNotPresent_ReturnsFalse()
    {
		_logger.LogDebug(">>> Entering Delete_KeyIsNotPresent_ReturnsFalse()");
        var tree = new ScapegoatTree<int>(1);

		_logger.LogDebug("Delete_KeyIsNotPresent_ReturnsFalse(): tree → {Tree}", tree);
        var deleted = tree.Delete(2);
		_logger.LogDebug("Delete_KeyIsNotPresent_ReturnsFalse(): deleted → {Deleted}", deleted);

        Assert.That(deleted, Is.False);
        Assert.That(tree.Size, Is.EqualTo(1));
		_logger.LogDebug("<<< Exiting Delete_KeyIsNotPresent_ReturnsFalse()");
    }

    [Test]
    public void Insert_KeyIsPresent_ReturnsFalse()
    {
		_logger.LogDebug(">>> Entering Insert_KeyIsPresent_ReturnsFalse()");
        var tree = new ScapegoatTree<int>(1);

		_logger.LogDebug("Insert_KeyIsPresent_ReturnsFalse(): tree → {Tree}", tree);
        var inserted = tree.Insert(1);
		_logger.LogDebug("Insert_KeyIsPresent_ReturnsFalse(): inserted → {Inserted}", inserted);

        Assert.That(inserted, Is.False);
        Assert.That(tree.Size, Is.EqualTo(1));
        Assert.That(tree.MaxSize, Is.EqualTo(1));
		_logger.LogDebug("<<< Exiting Insert_KeyIsPresent_ReturnsFalse()");
    }

    [Test]
    public void Remove_KeyIsPresent_RemovesKey()
    {
		_logger.LogDebug(">>> Entering Remove_KeyIsPresent_RemovesKey()");
        var tree = new ScapegoatTree<int>(1);

		_logger.LogDebug("Remove_KeyIsPresent_RemovesKey(): tree → {Tree}", tree);
        var inserted = tree.Insert(2);
		_logger.LogDebug("Remove_KeyIsPresent_RemovesKey(): inserted → {Inserted}", inserted);

        Assert.That(inserted, Is.True);

        var deleted = tree.Delete(2);
		_logger.LogDebug("Remove_KeyIsPresent_RemovesKey(): deleted → {Deleted}", deleted);

        Assert.That(deleted, Is.True);
        Assert.That(tree.Size, Is.EqualTo(1));
		_logger.LogDebug("<<< Exiting Remove_KeyIsPresent_RemovesKey()");
    }

    [Test]
    public void Remove_KeyIsRootWithNoChildren_RemovesKey()
    {
		_logger.LogDebug(">>> Entering Remove_KeyIsRootWithNoChildren_RemovesKey()");
        var tree = new ScapegoatTree<int>(1);

		_logger.LogDebug("Remove_KeyIsRootWithNoChildren_RemovesKey(): tree → {Tree}", tree);
        var deleted = tree.Delete(1);
		_logger.LogDebug("Remove_KeyIsRootWithNoChildren_RemovesKey(): deleted → {Deleted}", deleted);

        Assert.That(deleted, Is.True);
        Assert.That(tree.Root, Is.Null);
        Assert.That(tree.Size, Is.EqualTo(0));
		_logger.LogDebug("<<< Exiting Remove_KeyIsRootWithNoChildren_RemovesKey()");
    }

    [Test]
    public void Remove_KeyIsRootWithOneLeftChild_RemovesKey()
    {
		_logger.LogDebug(">>> Entering Remove_KeyIsRootWithOneLeftChild_RemovesKey()");
        var tree = new ScapegoatTree<int>(1);

		_logger.LogDebug("Remove_KeyIsRootWithOneLeftChild_RemovesKey(): tree → {Tree}", tree);
        var inserted = tree.Insert(-1);
		_logger.LogDebug("Remove_KeyIsRootWithOneLeftChild_RemovesKey(): inserted → {Inserted}", inserted);

        Assert.That(inserted, Is.True);

        var deleted = tree.Delete(1);
		_logger.LogDebug("Remove_KeyIsRootWithOneLeftChild_RemovesKey(): deleted → {Deleted}", deleted);

        Assert.That(deleted, Is.True);
        Assert.That(tree.Size, Is.EqualTo(1));
		_logger.LogDebug("<<< Exiting Remove_KeyIsRootWithOneLeftChild_RemovesKey()");
    }

    [Test]
    public void Remove_KeyIsRootWithOneRightChild_RemovesKey()
    {
		_logger.LogDebug(">>> Entering Remove_KeyIsRootWithOneRightChild_RemovesKey()");
        var tree = new ScapegoatTree<int>(1);

		_logger.LogDebug("Remove_KeyIsRootWithOneRightChild_RemovesKey(): tree → {Tree}", tree);
        var inserted = tree.Insert(2);
		_logger.LogDebug("Remove_KeyIsRootWithOneRightChild_RemovesKey(): inserted → {Inserted}", inserted);

        Assert.That(inserted, Is.True);

        var deleted = tree.Delete(1);
		_logger.LogDebug("Remove_KeyIsRootWithOneRightChild_RemovesKey(): deleted → {Deleted}", deleted);

        Assert.That(deleted, Is.True);
        Assert.That(tree.Size, Is.EqualTo(1));
		_logger.LogDebug("<<< Exiting Remove_KeyIsRootWithOneRightChild_RemovesKey()");
    }

    [Test]
    public void Remove_KeyIsRootWithTwoChildren_RemovesKey()
    {
		_logger.LogDebug(">>> Entering Remove_KeyIsRootWithTwoChildren_RemovesKey()");
        var tree = new ScapegoatTree<int>(1);

		_logger.LogDebug("Remove_KeyIsRootWithTwoChildren_RemovesKey(): tree → {Tree}", tree);
        var inserted = tree.Insert(-1);
		_logger.LogDebug("Remove_KeyIsRootWithTwoChildren_RemovesKey(): inserted → {Inserted}", inserted);

        Assert.That(inserted, Is.True);

        inserted = tree.Insert(2);

        Assert.That(inserted, Is.True);

        var deleted = tree.Delete(1);
		_logger.LogDebug("Remove_KeyIsRootWithTwoChildren_RemovesKey(): deleted → {Deleted}", deleted);

        Assert.That(deleted, Is.True);
        Assert.That(tree.Size, Is.EqualTo(2));
		_logger.LogDebug("<<< Exiting Remove_KeyIsRootWithTwoChildren_RemovesKey()");
    }

    [Test]
    public void Insert_KeyIsNotPresent_KeyIsInserted()
    {
		_logger.LogDebug(">>> Entering Insert_KeyIsNotPresent_KeyIsInserted()");
        var tree = new ScapegoatTree<int>(1);

		_logger.LogDebug("Insert_KeyIsNotPresent_KeyIsInserted(): tree → {Tree}", tree);
        var inserted = tree.Insert(2);
		_logger.LogDebug("Insert_KeyIsNotPresent_KeyIsInserted(): inserted → {Inserted}", inserted);

        Assert.That(inserted, Is.True);
        Assert.That(tree.Size, Is.EqualTo(2));
        Assert.That(tree.MaxSize, Is.EqualTo(2));
		_logger.LogDebug("<<< Exiting Insert_KeyIsNotPresent_KeyIsInserted()");
    }

    [TestCase(3, new[] { 2, 5, 1, 6 }, -1, 0.5)]
    public void Insert_TreeIsUnbalanced_RebuildsTree(int root, int[] keys, int candidate, double alpha)
    {
		_logger.LogDebug(">>> Entering Insert_TreeIsUnbalanced_RebuildsTree(root={Root},keys={Keys},candidate={Candidate},alpha={Alpha})", root, keys, candidate, alpha);
        var tree = new ScapegoatTree<int>(root, alpha);

		_logger.LogDebug("Insert_TreeIsUnbalanced_RebuildsTree(root={Root},keys={Keys},candidate={Candidate},alpha={Alpha}): tree → {Tree}", root, keys, candidate, alpha, tree);
        tree.TreeIsUnbalanced += FailTreeIsUnbalanced;

        foreach (var item in keys)
        {
            Assert.DoesNotThrow(() => tree.Insert(item));
        }

        tree.TreeIsUnbalanced -= FailTreeIsUnbalanced;
        tree.TreeIsUnbalanced += PassTreeIsUnbalanced;

        Assert.Throws<SuccessException>(() => tree.Insert(candidate));
		_logger.LogDebug("<<< Exiting Insert_TreeIsUnbalanced_RebuildsTree(root={Root},keys={Keys},candidate={Candidate},alpha={Alpha})", root, keys, candidate, alpha);
    }

    [TestCase(20, new[] { 10, 30, 5, 11, 29, 40, 50, 1, 12 }, new[] { 50, 40, 30, 29 }, 0.7)]
    public void Delete_TreeIsUnbalanced_BalancesTree(int root, int[] keys, int[] candidates, double alpha)
    {
		_logger.LogDebug(">>> Entering Delete_TreeIsUnbalanced_BalancesTree(root={Root},keys={Keys},candidates={Candidates},alpha={Alpha})", root, keys, candidates, alpha);
        var tree = new ScapegoatTree<int>(root, alpha);

		_logger.LogDebug("Delete_TreeIsUnbalanced_BalancesTree(root={Root},keys={Keys},candidates={Candidates},alpha={Alpha}): tree → {Tree}", root, keys, candidates, alpha, tree);
        tree.TreeIsUnbalanced += FailTreeIsUnbalanced;

        foreach (var item in keys)
        {
            Assert.DoesNotThrow(() => tree.Insert(item));
        }

        tree.TreeIsUnbalanced -= FailTreeIsUnbalanced;
        tree.TreeIsUnbalanced += PassTreeIsUnbalanced;

        Assert.Throws<SuccessException>(() =>
        {
            foreach (var item in candidates)
            {
                tree.Delete(item);
            }
        });
		_logger.LogDebug("<<< Exiting Delete_TreeIsUnbalanced_BalancesTree(root={Root},keys={Keys},candidates={Candidates},alpha={Alpha})", root, keys, candidates, alpha);
    }

    [TestCase(20, new[] { 10, 30, 5, 11, 29, 40, 50 }, 10, 1)]
    public void Delete_TreeIsUnbalanced_MaxSizeEqualsSize(int root, int[] keys, int candidate, double alpha)
    {
		_logger.LogDebug(">>> Entering Delete_TreeIsUnbalanced_MaxSizeEqualsSize(root={Root},keys={Keys},candidate={Candidate},alpha={Alpha})", root, keys, candidate, alpha);
        var tree = new ScapegoatTree<int>(root, alpha);

		_logger.LogDebug("Delete_TreeIsUnbalanced_MaxSizeEqualsSize(root={Root},keys={Keys},candidate={Candidate},alpha={Alpha}): tree → {Tree}", root, keys, candidate, alpha, tree);
        tree.TreeIsUnbalanced += FailTreeIsUnbalanced;

        foreach (var item in keys)
        {
            Assert.DoesNotThrow(() => tree.Insert(item));
        }

        tree.TreeIsUnbalanced -= FailTreeIsUnbalanced;

        tree.Delete(candidate);

        Assert.That(tree.MaxSize, Is.EqualTo(tree.Size));
		_logger.LogDebug("<<< Exiting Delete_TreeIsUnbalanced_MaxSizeEqualsSize(root={Root},keys={Keys},candidate={Candidate},alpha={Alpha})", root, keys, candidate, alpha);
    }

    [TestCase(3, new[] { 2, 5, 1, 6 }, -1, 0.5)]
    [TestCase(3, new[] { 2, 5, 1, 6 }, 7, 0.5)]
    public void Insert_TreeIsUnbalanced_BalancesTree(int root, int[] keys, int candidate, double alpha)
    {
		_logger.LogDebug(">>> Entering Insert_TreeIsUnbalanced_BalancesTree(root={Root},keys={Keys},candidate={Candidate},alpha={Alpha})", root, keys, candidate, alpha);
        var tree = new ScapegoatTree<int>(root, alpha);

		_logger.LogDebug("Insert_TreeIsUnbalanced_BalancesTree(root={Root},keys={Keys},candidate={Candidate},alpha={Alpha}): tree → {Tree}", root, keys, candidate, alpha, tree);
        tree.TreeIsUnbalanced += FailTreeIsUnbalanced;

        foreach (var item in keys)
        {
            Assert.DoesNotThrow(() => tree.Insert(item));
        }

        tree.TreeIsUnbalanced -= FailTreeIsUnbalanced;

        var inserted = tree.Insert(candidate);
		_logger.LogDebug("Insert_TreeIsUnbalanced_BalancesTree(root={Root},keys={Keys},candidate={Candidate},alpha={Alpha}): inserted → {Inserted}", root, keys, candidate, alpha, inserted);

        Assert.That(inserted, Is.True);
        Assert.That(tree.Size == 6, Is.True);
        Assert.That(tree.IsAlphaWeightBalanced(), Is.True);
		_logger.LogDebug("<<< Exiting Insert_TreeIsUnbalanced_BalancesTree(root={Root},keys={Keys},candidate={Candidate},alpha={Alpha})", root, keys, candidate, alpha);
    }

    [TestCase(3, 5, 0.5)]
    public void Insert_TreeIsUnbalanced_BalancesTree2(int root, int candidate, double alpha)
    {
		_logger.LogDebug(">>> Entering Insert_TreeIsUnbalanced_BalancesTree2(root={Root},candidate={Candidate},alpha={Alpha})", root, candidate, alpha);
        var tree = new ScapegoatTree<int>(root, alpha);

		_logger.LogDebug("Insert_TreeIsUnbalanced_BalancesTree2(root={Root},candidate={Candidate},alpha={Alpha}): tree → {Tree}", root, candidate, alpha, tree);
        var inserted = tree.Insert(candidate);
		_logger.LogDebug("Insert_TreeIsUnbalanced_BalancesTree2(root={Root},candidate={Candidate},alpha={Alpha}): inserted → {Inserted}", root, candidate, alpha, inserted);

        Assert.That(inserted, Is.True);
        Assert.That(tree.Size == 2, Is.True);
        Assert.That(tree.IsAlphaWeightBalanced(), Is.True);
		_logger.LogDebug("<<< Exiting Insert_TreeIsUnbalanced_BalancesTree2(root={Root},candidate={Candidate},alpha={Alpha})", root, candidate, alpha);
    }

    [Test]
    public void Contains_RootIsNull_ReturnsFalse()
    {
		_logger.LogDebug(">>> Entering Contains_RootIsNull_ReturnsFalse()");
        var tree = new ScapegoatTree<int>();

		_logger.LogDebug("Contains_RootIsNull_ReturnsFalse(): tree → {Tree}", tree);
        Assert.That(tree.Contains(1), Is.False);
		_logger.LogDebug("<<< Exiting Contains_RootIsNull_ReturnsFalse()");
    }

    [Test]
    public void Contains_RootHasKey_ReturnsTrue()
    {
		_logger.LogDebug(">>> Entering Contains_RootHasKey_ReturnsTrue()");
        var tree = new ScapegoatTree<int>(1);

		_logger.LogDebug("Contains_RootHasKey_ReturnsTrue(): tree → {Tree}", tree);
        Assert.That(tree.Contains(1), Is.True);
		_logger.LogDebug("<<< Exiting Contains_RootHasKey_ReturnsTrue()");
    }

    [Test]
    public void Contains_TreeHasKey_ReturnsTrue()
    {
		_logger.LogDebug(">>> Entering Contains_TreeHasKey_ReturnsTrue()");
        var tree = new ScapegoatTree<int>(1);

		_logger.LogDebug("Contains_TreeHasKey_ReturnsTrue(): tree → {Tree}", tree);
        tree.Insert(2);

        Assert.That(tree.Contains(2), Is.True);
		_logger.LogDebug("<<< Exiting Contains_TreeHasKey_ReturnsTrue()");
    }

    [Test]
    public void Contains_TreeDoesNotContainKey_ReturnsFalse()
    {
		_logger.LogDebug(">>> Entering Contains_TreeDoesNotContainKey_ReturnsFalse()");
        var tree = new ScapegoatTree<int>(1);

		_logger.LogDebug("Contains_TreeDoesNotContainKey_ReturnsFalse(): tree → {Tree}", tree);
        tree.Insert(2);

        Assert.That(tree.Contains(-1), Is.False);
		_logger.LogDebug("<<< Exiting Contains_TreeDoesNotContainKey_ReturnsFalse()");
    }

    [Test]
    public void Clear_TreeHasKeys_ClearsTree()
    {
		_logger.LogDebug(">>> Entering Clear_TreeHasKeys_ClearsTree()");
        var tree = new ScapegoatTree<int>(1);

		_logger.LogDebug("Clear_TreeHasKeys_ClearsTree(): tree → {Tree}", tree);
        tree.Clear();

        Assert.That(tree.Size == 0, Is.True);
        Assert.That(tree.MaxSize == 0, Is.True);
        Assert.That(tree.Root, Is.Null);
		_logger.LogDebug("<<< Exiting Clear_TreeHasKeys_ClearsTree()");
    }

    [Test]
    public void Tune_AlphaIsValid_ChangesAlpha()
    {
		_logger.LogDebug(">>> Entering Tune_AlphaIsValid_ChangesAlpha()");
        var expected = 0.7;

        var tree = new ScapegoatTree<int>();
		_logger.LogDebug("Tune_AlphaIsValid_ChangesAlpha(): tree → {Tree}", tree);

        tree.Tune(expected);

        Assert.That(tree.Alpha, Is.EqualTo(expected));
		_logger.LogDebug("<<< Exiting Tune_AlphaIsValid_ChangesAlpha()");
    }

    [Test]
    public void Tune_AlphaIsNotValid_ThrowsException()
    {
		_logger.LogDebug(">>> Entering Tune_AlphaIsNotValid_ThrowsException()");
        var expected = 9.9;

        var tree = new ScapegoatTree<int>();
		_logger.LogDebug("Tune_AlphaIsNotValid_ThrowsException(): tree → {Tree}", tree);

        Assert.Throws<ArgumentException>(() => tree.Tune(expected));
		_logger.LogDebug("<<< Exiting Tune_AlphaIsNotValid_ThrowsException()");
    }

    [Test]
    public void FindScapegoatInPath_PathIsEmpty_ThrowsAnException()
    {
		_logger.LogDebug(">>> Entering FindScapegoatInPath_PathIsEmpty_ThrowsAnException()");
        var tree = new ScapegoatTree<int>();
        Assert.Throws<ArgumentException>(() => tree.FindScapegoatInPath(new Stack<Node<int>>()));
		_logger.LogDebug("FindScapegoatInPath_PathIsEmpty_ThrowsAnException(): tree → {Tree}", tree);
		_logger.LogDebug("<<< Exiting FindScapegoatInPath_PathIsEmpty_ThrowsAnException()");
    }

    [Test]
    public void FindScapegoatInPath_ScapegoatIsNotPresent_ThrowsAnException()
    {
		_logger.LogDebug(">>> Entering FindScapegoatInPath_ScapegoatIsNotPresent_ThrowsAnException()");
        var tree = new ScapegoatTree<int>(1, 1);
        var path = new Stack<Node<int>>();
		_logger.LogDebug("FindScapegoatInPath_ScapegoatIsNotPresent_ThrowsAnException(): path → {Path}", path);
		_logger.LogDebug("FindScapegoatInPath_ScapegoatIsNotPresent_ThrowsAnException(): tree → {Tree}", tree);
        path.Push(tree.Root!);
        Assert.Throws<InvalidOperationException>(() => tree.FindScapegoatInPath(path));
		_logger.LogDebug("<<< Exiting FindScapegoatInPath_ScapegoatIsNotPresent_ThrowsAnException()");
    }

    private static void FailTreeIsUnbalanced(object? sender, EventArgs? e)
    {
		_logger.LogDebug(">>> Entering FailTreeIsUnbalanced(sender={Sender},e={E})", sender, e);
        Assert.Fail();
		_logger.LogDebug("<<< Exiting FailTreeIsUnbalanced(sender={Sender},e={E})", sender, e);
    }

    private static void PassTreeIsUnbalanced(object? sender, EventArgs? e)
    {
		_logger.LogDebug(">>> Entering PassTreeIsUnbalanced(sender={Sender},e={E})", sender, e);
        Assert.Pass();
		_logger.LogDebug("<<< Exiting PassTreeIsUnbalanced(sender={Sender},e={E})", sender, e);
    }
}
