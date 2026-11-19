using Algorithms.Graph;
using DataStructures.BinarySearchTree;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Graph;

public static class BreadthFirstTreeTraversalTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void CorrectLevelOrderTraversal()
    {
		_logger.LogError(">>> Entering CorrectLevelOrderTraversal()");
        // Arrange
        int[] correctPath = [7, 4, 13, 2, 5, 11, 15, 14, 16];
        int[] insertionOrder = [7, 13, 11, 15, 14, 4, 5, 16, 2];
        BinarySearchTree<int> testTree = new BinarySearchTree<int>();
		_logger.LogError("CorrectLevelOrderTraversal(): testTree → {TestTree}", testTree);
        foreach (int data in insertionOrder)
        {
            testTree.Add(data);
        }

        // Act
        int[] levelOrder = BreadthFirstTreeTraversal<int>.LevelOrderTraversal(testTree);

        // Assert
        Assert.That(correctPath, Is.EqualTo(levelOrder));
		_logger.LogWarning("<<< Exiting CorrectLevelOrderTraversal()");
    }

    [Test]
    public static void EmptyArrayForNullRoot()
    {
		_logger.LogError(">>> Entering EmptyArrayForNullRoot()");
        // Arrange
        BinarySearchTree<int> testTree = new BinarySearchTree<int>();
		_logger.LogError("EmptyArrayForNullRoot(): testTree → {TestTree}", testTree);

        // Act
        int[] levelOrder = BreadthFirstTreeTraversal<int>.LevelOrderTraversal(testTree);

        // Assert
        Assert.That(levelOrder, Is.Empty);
		_logger.LogWarning("<<< Exiting EmptyArrayForNullRoot()");
    }

    [TestCase(new[] { 7, 9, 5 })]
    [TestCase(new[] { 7, 13, 11, 15, 14, 4, 5, 16, 2 })]
    public static void IncorrectLevelOrderTraversal(int[] insertion)
    {
		_logger.LogError(">>> Entering IncorrectLevelOrderTraversal(insertion={Insertion})", insertion);
        // Arrange
        BinarySearchTree<int> testTree = new BinarySearchTree<int>();
		_logger.LogError("IncorrectLevelOrderTraversal(insertion={Insertion}): testTree → {TestTree}", insertion, testTree);
        foreach (int data in insertion)
        {
            testTree.Add(data);
        }

        // Act
        int[] levelOrder = BreadthFirstTreeTraversal<int>.LevelOrderTraversal(testTree);

        // Assert
        Assert.That(insertion, Is.Not.EqualTo(levelOrder));
		_logger.LogWarning("<<< Exiting IncorrectLevelOrderTraversal(insertion={Insertion})", insertion);
    }

    [Test]
    public static void DeepestNodeInTree()
    {
		_logger.LogError(">>> Entering DeepestNodeInTree()");
        // Arrange
        BinarySearchTree<int> testTree = new BinarySearchTree<int>();
		_logger.LogError("DeepestNodeInTree(): testTree → {TestTree}", testTree);
        int[] insertion = [7, 13, 11, 15, 4, 5, 12, 2, 9];
        foreach (int data in insertion)
        {
            testTree.Add(data);
        }

        // Act
        int deepest = BreadthFirstTreeTraversal<int>.DeepestNode(testTree);
		_logger.LogError("DeepestNodeInTree(): deepest → {Deepest}", deepest);

        // Assert
        Assert.That(deepest, Is.EqualTo(12));
		_logger.LogWarning("<<< Exiting DeepestNodeInTree()");
    }

    [Test]
    public static void DeepestNodeOfEmptyTree()
    {
		_logger.LogError(">>> Entering DeepestNodeOfEmptyTree()");
        // Arrange
        BinarySearchTree<int?> testTree = new BinarySearchTree<int?>();
		_logger.LogError("DeepestNodeOfEmptyTree(): testTree → {TestTree}", testTree);

        // Act
        int? deepest = BreadthFirstTreeTraversal<int?>.DeepestNode(testTree);

        // Assert
        Assert.That(deepest, Is.Null);
		_logger.LogWarning("<<< Exiting DeepestNodeOfEmptyTree()");
    }
}

