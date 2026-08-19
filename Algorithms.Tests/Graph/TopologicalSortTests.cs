using Algorithms.Graph;
using DataStructures.Graph;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Graph;

public class TopologicalSortTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Test topological sort on a simple linear DAG: A → B → C.
    ///     Expected order: [A, B, C].
    /// </summary>
    [Test]
    public void Sort_SimpleLinearGraph_ReturnsCorrectOrder()
    {
		_logger.LogDebug(">>> Entering Sort_SimpleLinearGraph_ReturnsCorrectOrder()");
        // Arrange
        var graph = new DirectedWeightedGraph<string>(3);
		_logger.LogDebug("Sort_SimpleLinearGraph_ReturnsCorrectOrder(): graph → {Graph}", graph);
        var vertexA = graph.AddVertex("A");
		_logger.LogDebug("Sort_SimpleLinearGraph_ReturnsCorrectOrder(): vertexA → {VertexA}", vertexA);
        var vertexB = graph.AddVertex("B");
		_logger.LogDebug("Sort_SimpleLinearGraph_ReturnsCorrectOrder(): vertexB → {VertexB}", vertexB);
        var vertexC = graph.AddVertex("C");
		_logger.LogDebug("Sort_SimpleLinearGraph_ReturnsCorrectOrder(): vertexC → {VertexC}", vertexC);

        graph.AddEdge(vertexA, vertexB, 1);
        graph.AddEdge(vertexB, vertexC, 1);

        var topologicalSort = new TopologicalSort<string>();
		_logger.LogDebug("Sort_SimpleLinearGraph_ReturnsCorrectOrder(): topologicalSort → {TopologicalSort}", topologicalSort);

        // Act
        var result = topologicalSort.Sort(graph);
		_logger.LogDebug("Sort_SimpleLinearGraph_ReturnsCorrectOrder(): result → {Result}", result);

        // Assert
        Assert.That(result.Count, Is.EqualTo(3));
        Assert.That(result[0], Is.EqualTo(vertexA));
        Assert.That(result[1], Is.EqualTo(vertexB));
        Assert.That(result[2], Is.EqualTo(vertexC));
		_logger.LogDebug("<<< Exiting Sort_SimpleLinearGraph_ReturnsCorrectOrder()");
    }

    /// <summary>
    ///     Test Kahn's algorithm on a simple linear DAG: A → B → C.
    ///     Expected order: [A, B, C].
    /// </summary>
    [Test]
    public void SortKahn_SimpleLinearGraph_ReturnsCorrectOrder()
    {
		_logger.LogDebug(">>> Entering SortKahn_SimpleLinearGraph_ReturnsCorrectOrder()");
        // Arrange
        var graph = new DirectedWeightedGraph<string>(3);
		_logger.LogDebug("SortKahn_SimpleLinearGraph_ReturnsCorrectOrder(): graph → {Graph}", graph);
        var vertexA = graph.AddVertex("A");
		_logger.LogDebug("SortKahn_SimpleLinearGraph_ReturnsCorrectOrder(): vertexA → {VertexA}", vertexA);
        var vertexB = graph.AddVertex("B");
		_logger.LogDebug("SortKahn_SimpleLinearGraph_ReturnsCorrectOrder(): vertexB → {VertexB}", vertexB);
        var vertexC = graph.AddVertex("C");
		_logger.LogDebug("SortKahn_SimpleLinearGraph_ReturnsCorrectOrder(): vertexC → {VertexC}", vertexC);

        graph.AddEdge(vertexA, vertexB, 1);
        graph.AddEdge(vertexB, vertexC, 1);

        var topologicalSort = new TopologicalSort<string>();
		_logger.LogDebug("SortKahn_SimpleLinearGraph_ReturnsCorrectOrder(): topologicalSort → {TopologicalSort}", topologicalSort);

        // Act
        var result = topologicalSort.SortKahn(graph);
		_logger.LogDebug("SortKahn_SimpleLinearGraph_ReturnsCorrectOrder(): result → {Result}", result);

        // Assert
        Assert.That(result.Count, Is.EqualTo(3));
        Assert.That(result[0], Is.EqualTo(vertexA));
        Assert.That(result[1], Is.EqualTo(vertexB));
        Assert.That(result[2], Is.EqualTo(vertexC));
		_logger.LogDebug("<<< Exiting SortKahn_SimpleLinearGraph_ReturnsCorrectOrder()");
    }

    /// <summary>
    ///     Test topological sort on a DAG with multiple valid orderings.
    ///     Graph: A → C
    ///            B → C
    ///     Valid orderings: [A, B, C] or [B, A, C].
    ///     We verify that C comes after both A and B.
    /// </summary>
    [Test]
    public void Sort_GraphWithMultipleValidOrderings_ReturnsValidOrder()
    {
		_logger.LogDebug(">>> Entering Sort_GraphWithMultipleValidOrderings_ReturnsValidOrder()");
        // Arrange
        var graph = new DirectedWeightedGraph<string>(3);
		_logger.LogDebug("Sort_GraphWithMultipleValidOrderings_ReturnsValidOrder(): graph → {Graph}", graph);
        var vertexA = graph.AddVertex("A");
		_logger.LogDebug("Sort_GraphWithMultipleValidOrderings_ReturnsValidOrder(): vertexA → {VertexA}", vertexA);
        var vertexB = graph.AddVertex("B");
		_logger.LogDebug("Sort_GraphWithMultipleValidOrderings_ReturnsValidOrder(): vertexB → {VertexB}", vertexB);
        var vertexC = graph.AddVertex("C");
		_logger.LogDebug("Sort_GraphWithMultipleValidOrderings_ReturnsValidOrder(): vertexC → {VertexC}", vertexC);

        graph.AddEdge(vertexA, vertexC, 1);
        graph.AddEdge(vertexB, vertexC, 1);

        var topologicalSort = new TopologicalSort<string>();
		_logger.LogDebug("Sort_GraphWithMultipleValidOrderings_ReturnsValidOrder(): topologicalSort → {TopologicalSort}", topologicalSort);

        // Act
        var result = topologicalSort.Sort(graph);
		_logger.LogDebug("Sort_GraphWithMultipleValidOrderings_ReturnsValidOrder(): result → {Result}", result);

        // Assert
        Assert.That(result.Count, Is.EqualTo(3));

        // C should come after both A and B
        var indexA = result.IndexOf(vertexA);
		_logger.LogDebug("Sort_GraphWithMultipleValidOrderings_ReturnsValidOrder(): indexA → {IndexA}", indexA);
        var indexB = result.IndexOf(vertexB);
		_logger.LogDebug("Sort_GraphWithMultipleValidOrderings_ReturnsValidOrder(): indexB → {IndexB}", indexB);
        var indexC = result.IndexOf(vertexC);
		_logger.LogDebug("Sort_GraphWithMultipleValidOrderings_ReturnsValidOrder(): indexC → {IndexC}", indexC);

        Assert.That(indexC, Is.GreaterThan(indexA));
        Assert.That(indexC, Is.GreaterThan(indexB));
		_logger.LogDebug("<<< Exiting Sort_GraphWithMultipleValidOrderings_ReturnsValidOrder()");
    }

    /// <summary>
    ///     Test Kahn's algorithm on a DAG with multiple valid orderings.
    ///     Graph: A → C
    ///            B → C
    ///     Valid orderings: [A, B, C] or [B, A, C].
    ///     We verify that C comes after both A and B.
    /// </summary>
    [Test]
    public void SortKahn_GraphWithMultipleValidOrderings_ReturnsValidOrder()
    {
		_logger.LogDebug(">>> Entering SortKahn_GraphWithMultipleValidOrderings_ReturnsValidOrder()");
        // Arrange
        var graph = new DirectedWeightedGraph<string>(3);
		_logger.LogDebug("SortKahn_GraphWithMultipleValidOrderings_ReturnsValidOrder(): graph → {Graph}", graph);
        var vertexA = graph.AddVertex("A");
		_logger.LogDebug("SortKahn_GraphWithMultipleValidOrderings_ReturnsValidOrder(): vertexA → {VertexA}", vertexA);
        var vertexB = graph.AddVertex("B");
		_logger.LogDebug("SortKahn_GraphWithMultipleValidOrderings_ReturnsValidOrder(): vertexB → {VertexB}", vertexB);
        var vertexC = graph.AddVertex("C");
		_logger.LogDebug("SortKahn_GraphWithMultipleValidOrderings_ReturnsValidOrder(): vertexC → {VertexC}", vertexC);

        graph.AddEdge(vertexA, vertexC, 1);
        graph.AddEdge(vertexB, vertexC, 1);

        var topologicalSort = new TopologicalSort<string>();
		_logger.LogDebug("SortKahn_GraphWithMultipleValidOrderings_ReturnsValidOrder(): topologicalSort → {TopologicalSort}", topologicalSort);

        // Act
        var result = topologicalSort.SortKahn(graph);
		_logger.LogDebug("SortKahn_GraphWithMultipleValidOrderings_ReturnsValidOrder(): result → {Result}", result);

        // Assert
        Assert.That(result.Count, Is.EqualTo(3));

        // C should come after both A and B
        var indexA = result.IndexOf(vertexA);
		_logger.LogDebug("SortKahn_GraphWithMultipleValidOrderings_ReturnsValidOrder(): indexA → {IndexA}", indexA);
        var indexB = result.IndexOf(vertexB);
		_logger.LogDebug("SortKahn_GraphWithMultipleValidOrderings_ReturnsValidOrder(): indexB → {IndexB}", indexB);
        var indexC = result.IndexOf(vertexC);
		_logger.LogDebug("SortKahn_GraphWithMultipleValidOrderings_ReturnsValidOrder(): indexC → {IndexC}", indexC);

        Assert.That(indexC, Is.GreaterThan(indexA));
        Assert.That(indexC, Is.GreaterThan(indexB));
		_logger.LogDebug("<<< Exiting SortKahn_GraphWithMultipleValidOrderings_ReturnsValidOrder()");
    }

    /// <summary>
    ///     Test topological sort on a more complex DAG.
    ///     Graph: A → B → D
    ///            A → C → D
    ///     Valid orderings include: [A, B, C, D], [A, C, B, D].
    ///     We verify that A comes first and D comes last.
    /// </summary>
    [Test]
    public void Sort_ComplexDAG_ReturnsValidOrder()
    {
		_logger.LogDebug(">>> Entering Sort_ComplexDAG_ReturnsValidOrder()");
        // Arrange
        var graph = new DirectedWeightedGraph<string>(4);
		_logger.LogDebug("Sort_ComplexDAG_ReturnsValidOrder(): graph → {Graph}", graph);
        var vertexA = graph.AddVertex("A");
		_logger.LogDebug("Sort_ComplexDAG_ReturnsValidOrder(): vertexA → {VertexA}", vertexA);
        var vertexB = graph.AddVertex("B");
		_logger.LogDebug("Sort_ComplexDAG_ReturnsValidOrder(): vertexB → {VertexB}", vertexB);
        var vertexC = graph.AddVertex("C");
		_logger.LogDebug("Sort_ComplexDAG_ReturnsValidOrder(): vertexC → {VertexC}", vertexC);
        var vertexD = graph.AddVertex("D");
		_logger.LogDebug("Sort_ComplexDAG_ReturnsValidOrder(): vertexD → {VertexD}", vertexD);

        graph.AddEdge(vertexA, vertexB, 1);
        graph.AddEdge(vertexA, vertexC, 1);
        graph.AddEdge(vertexB, vertexD, 1);
        graph.AddEdge(vertexC, vertexD, 1);

        var topologicalSort = new TopologicalSort<string>();
		_logger.LogDebug("Sort_ComplexDAG_ReturnsValidOrder(): topologicalSort → {TopologicalSort}", topologicalSort);

        // Act
        var result = topologicalSort.Sort(graph);
		_logger.LogDebug("Sort_ComplexDAG_ReturnsValidOrder(): result → {Result}", result);

        // Assert
        Assert.That(result.Count, Is.EqualTo(4));
        Assert.That(result[0], Is.EqualTo(vertexA)); // A must be first
        Assert.That(result[3], Is.EqualTo(vertexD)); // D must be last

        // B and C should come after A and before D
        var indexB = result.IndexOf(vertexB);
		_logger.LogDebug("Sort_ComplexDAG_ReturnsValidOrder(): indexB → {IndexB}", indexB);
        var indexC = result.IndexOf(vertexC);
		_logger.LogDebug("Sort_ComplexDAG_ReturnsValidOrder(): indexC → {IndexC}", indexC);

        Assert.That(indexB, Is.GreaterThan(0));
        Assert.That(indexB, Is.LessThan(3));
        Assert.That(indexC, Is.GreaterThan(0));
        Assert.That(indexC, Is.LessThan(3));
		_logger.LogDebug("<<< Exiting Sort_ComplexDAG_ReturnsValidOrder()");
    }

    /// <summary>
    ///     Test Kahn's algorithm on a more complex DAG.
    ///     Graph: A → B → D
    ///            A → C → D
    ///     Valid orderings include: [A, B, C, D], [A, C, B, D].
    ///     We verify that A comes first and D comes last.
    /// </summary>
    [Test]
    public void SortKahn_ComplexDAG_ReturnsValidOrder()
    {
		_logger.LogDebug(">>> Entering SortKahn_ComplexDAG_ReturnsValidOrder()");
        // Arrange
        var graph = new DirectedWeightedGraph<string>(4);
		_logger.LogDebug("SortKahn_ComplexDAG_ReturnsValidOrder(): graph → {Graph}", graph);
        var vertexA = graph.AddVertex("A");
		_logger.LogDebug("SortKahn_ComplexDAG_ReturnsValidOrder(): vertexA → {VertexA}", vertexA);
        var vertexB = graph.AddVertex("B");
		_logger.LogDebug("SortKahn_ComplexDAG_ReturnsValidOrder(): vertexB → {VertexB}", vertexB);
        var vertexC = graph.AddVertex("C");
		_logger.LogDebug("SortKahn_ComplexDAG_ReturnsValidOrder(): vertexC → {VertexC}", vertexC);
        var vertexD = graph.AddVertex("D");
		_logger.LogDebug("SortKahn_ComplexDAG_ReturnsValidOrder(): vertexD → {VertexD}", vertexD);

        graph.AddEdge(vertexA, vertexB, 1);
        graph.AddEdge(vertexA, vertexC, 1);
        graph.AddEdge(vertexB, vertexD, 1);
        graph.AddEdge(vertexC, vertexD, 1);

        var topologicalSort = new TopologicalSort<string>();
		_logger.LogDebug("SortKahn_ComplexDAG_ReturnsValidOrder(): topologicalSort → {TopologicalSort}", topologicalSort);

        // Act
        var result = topologicalSort.SortKahn(graph);
		_logger.LogDebug("SortKahn_ComplexDAG_ReturnsValidOrder(): result → {Result}", result);

        // Assert
        Assert.That(result.Count, Is.EqualTo(4));
        Assert.That(result[0], Is.EqualTo(vertexA)); // A must be first
        Assert.That(result[3], Is.EqualTo(vertexD)); // D must be last

        // B and C should come after A and before D
        var indexB = result.IndexOf(vertexB);
		_logger.LogDebug("SortKahn_ComplexDAG_ReturnsValidOrder(): indexB → {IndexB}", indexB);
        var indexC = result.IndexOf(vertexC);
		_logger.LogDebug("SortKahn_ComplexDAG_ReturnsValidOrder(): indexC → {IndexC}", indexC);

        Assert.That(indexB, Is.GreaterThan(0));
        Assert.That(indexB, Is.LessThan(3));
        Assert.That(indexC, Is.GreaterThan(0));
        Assert.That(indexC, Is.LessThan(3));
		_logger.LogDebug("<<< Exiting SortKahn_ComplexDAG_ReturnsValidOrder()");
    }

    /// <summary>
    ///     Test topological sort on a graph with a cycle.
    ///     Graph: A → B → C → A (cycle).
    ///     Should throw InvalidOperationException.
    /// </summary>
    [Test]
    public void Sort_GraphWithCycle_ThrowsInvalidOperationException()
    {
		_logger.LogDebug(">>> Entering Sort_GraphWithCycle_ThrowsInvalidOperationException()");
        // Arrange
        var graph = new DirectedWeightedGraph<string>(3);
		_logger.LogDebug("Sort_GraphWithCycle_ThrowsInvalidOperationException(): graph → {Graph}", graph);
        var vertexA = graph.AddVertex("A");
		_logger.LogDebug("Sort_GraphWithCycle_ThrowsInvalidOperationException(): vertexA → {VertexA}", vertexA);
        var vertexB = graph.AddVertex("B");
		_logger.LogDebug("Sort_GraphWithCycle_ThrowsInvalidOperationException(): vertexB → {VertexB}", vertexB);
        var vertexC = graph.AddVertex("C");
		_logger.LogDebug("Sort_GraphWithCycle_ThrowsInvalidOperationException(): vertexC → {VertexC}", vertexC);

        graph.AddEdge(vertexA, vertexB, 1);
        graph.AddEdge(vertexB, vertexC, 1);
        graph.AddEdge(vertexC, vertexA, 1); // Creates a cycle

        var topologicalSort = new TopologicalSort<string>();
		_logger.LogDebug("Sort_GraphWithCycle_ThrowsInvalidOperationException(): topologicalSort → {TopologicalSort}", topologicalSort);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => topologicalSort.Sort(graph));
		_logger.LogDebug("<<< Exiting Sort_GraphWithCycle_ThrowsInvalidOperationException()");
    }

    /// <summary>
    ///     Test Kahn's algorithm on a graph with a cycle.
    ///     Graph: A → B → C → A (cycle).
    ///     Should throw InvalidOperationException.
    /// </summary>
    [Test]
    public void SortKahn_GraphWithCycle_ThrowsInvalidOperationException()
    {
		_logger.LogDebug(">>> Entering SortKahn_GraphWithCycle_ThrowsInvalidOperationException()");
        // Arrange
        var graph = new DirectedWeightedGraph<string>(3);
		_logger.LogDebug("SortKahn_GraphWithCycle_ThrowsInvalidOperationException(): graph → {Graph}", graph);
        var vertexA = graph.AddVertex("A");
		_logger.LogDebug("SortKahn_GraphWithCycle_ThrowsInvalidOperationException(): vertexA → {VertexA}", vertexA);
        var vertexB = graph.AddVertex("B");
		_logger.LogDebug("SortKahn_GraphWithCycle_ThrowsInvalidOperationException(): vertexB → {VertexB}", vertexB);
        var vertexC = graph.AddVertex("C");
		_logger.LogDebug("SortKahn_GraphWithCycle_ThrowsInvalidOperationException(): vertexC → {VertexC}", vertexC);

        graph.AddEdge(vertexA, vertexB, 1);
        graph.AddEdge(vertexB, vertexC, 1);
        graph.AddEdge(vertexC, vertexA, 1); // Creates a cycle

        var topologicalSort = new TopologicalSort<string>();
		_logger.LogDebug("SortKahn_GraphWithCycle_ThrowsInvalidOperationException(): topologicalSort → {TopologicalSort}", topologicalSort);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => topologicalSort.SortKahn(graph));
		_logger.LogDebug("<<< Exiting SortKahn_GraphWithCycle_ThrowsInvalidOperationException()");
    }

    /// <summary>
    ///     Test topological sort on a single vertex graph.
    ///     Graph: A (no edges).
    ///     Expected order: [A].
    /// </summary>
    [Test]
    public void Sort_SingleVertexGraph_ReturnsSingleVertex()
    {
		_logger.LogDebug(">>> Entering Sort_SingleVertexGraph_ReturnsSingleVertex()");
        // Arrange
        var graph = new DirectedWeightedGraph<string>(1);
		_logger.LogDebug("Sort_SingleVertexGraph_ReturnsSingleVertex(): graph → {Graph}", graph);
        var vertexA = graph.AddVertex("A");
		_logger.LogDebug("Sort_SingleVertexGraph_ReturnsSingleVertex(): vertexA → {VertexA}", vertexA);

        var topologicalSort = new TopologicalSort<string>();
		_logger.LogDebug("Sort_SingleVertexGraph_ReturnsSingleVertex(): topologicalSort → {TopologicalSort}", topologicalSort);

        // Act
        var result = topologicalSort.Sort(graph);
		_logger.LogDebug("Sort_SingleVertexGraph_ReturnsSingleVertex(): result → {Result}", result);

        // Assert
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0], Is.EqualTo(vertexA));
		_logger.LogDebug("<<< Exiting Sort_SingleVertexGraph_ReturnsSingleVertex()");
    }

    /// <summary>
    ///     Test Kahn's algorithm on a single vertex graph.
    ///     Graph: A (no edges).
    ///     Expected order: [A].
    /// </summary>
    [Test]
    public void SortKahn_SingleVertexGraph_ReturnsSingleVertex()
    {
		_logger.LogDebug(">>> Entering SortKahn_SingleVertexGraph_ReturnsSingleVertex()");
        // Arrange
        var graph = new DirectedWeightedGraph<string>(1);
		_logger.LogDebug("SortKahn_SingleVertexGraph_ReturnsSingleVertex(): graph → {Graph}", graph);
        var vertexA = graph.AddVertex("A");
		_logger.LogDebug("SortKahn_SingleVertexGraph_ReturnsSingleVertex(): vertexA → {VertexA}", vertexA);

        var topologicalSort = new TopologicalSort<string>();
		_logger.LogDebug("SortKahn_SingleVertexGraph_ReturnsSingleVertex(): topologicalSort → {TopologicalSort}", topologicalSort);

        // Act
        var result = topologicalSort.SortKahn(graph);
		_logger.LogDebug("SortKahn_SingleVertexGraph_ReturnsSingleVertex(): result → {Result}", result);

        // Assert
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0], Is.EqualTo(vertexA));
		_logger.LogDebug("<<< Exiting SortKahn_SingleVertexGraph_ReturnsSingleVertex()");
    }

    /// <summary>
    ///     Test topological sort on a disconnected DAG.
    ///     Graph: A → B (component 1)
    ///            C → D (component 2)
    ///     Valid orderings: [A, B, C, D], [A, C, B, D], [C, A, B, D], [C, D, A, B], etc.
    ///     We verify that A comes before B and C comes before D.
    /// </summary>
    [Test]
    public void Sort_DisconnectedDAG_ReturnsValidOrder()
    {
		_logger.LogDebug(">>> Entering Sort_DisconnectedDAG_ReturnsValidOrder()");
        // Arrange
        var graph = new DirectedWeightedGraph<string>(4);
		_logger.LogDebug("Sort_DisconnectedDAG_ReturnsValidOrder(): graph → {Graph}", graph);
        var vertexA = graph.AddVertex("A");
		_logger.LogDebug("Sort_DisconnectedDAG_ReturnsValidOrder(): vertexA → {VertexA}", vertexA);
        var vertexB = graph.AddVertex("B");
		_logger.LogDebug("Sort_DisconnectedDAG_ReturnsValidOrder(): vertexB → {VertexB}", vertexB);
        var vertexC = graph.AddVertex("C");
		_logger.LogDebug("Sort_DisconnectedDAG_ReturnsValidOrder(): vertexC → {VertexC}", vertexC);
        var vertexD = graph.AddVertex("D");
		_logger.LogDebug("Sort_DisconnectedDAG_ReturnsValidOrder(): vertexD → {VertexD}", vertexD);

        graph.AddEdge(vertexA, vertexB, 1);
        graph.AddEdge(vertexC, vertexD, 1);

        var topologicalSort = new TopologicalSort<string>();
		_logger.LogDebug("Sort_DisconnectedDAG_ReturnsValidOrder(): topologicalSort → {TopologicalSort}", topologicalSort);

        // Act
        var result = topologicalSort.Sort(graph);
		_logger.LogDebug("Sort_DisconnectedDAG_ReturnsValidOrder(): result → {Result}", result);

        // Assert
        Assert.That(result.Count, Is.EqualTo(4));

        // A should come before B
        var indexA = result.IndexOf(vertexA);
		_logger.LogDebug("Sort_DisconnectedDAG_ReturnsValidOrder(): indexA → {IndexA}", indexA);
        var indexB = result.IndexOf(vertexB);
		_logger.LogDebug("Sort_DisconnectedDAG_ReturnsValidOrder(): indexB → {IndexB}", indexB);
        Assert.That(indexB, Is.GreaterThan(indexA));

        // C should come before D
        var indexC = result.IndexOf(vertexC);
		_logger.LogDebug("Sort_DisconnectedDAG_ReturnsValidOrder(): indexC → {IndexC}", indexC);
        var indexD = result.IndexOf(vertexD);
		_logger.LogDebug("Sort_DisconnectedDAG_ReturnsValidOrder(): indexD → {IndexD}", indexD);
        Assert.That(indexD, Is.GreaterThan(indexC));
		_logger.LogDebug("<<< Exiting Sort_DisconnectedDAG_ReturnsValidOrder()");
    }

    /// <summary>
    ///     Test Kahn's algorithm on a disconnected DAG.
    ///     Graph: A → B (component 1)
    ///            C → D (component 2)
    ///     Valid orderings: [A, B, C, D], [A, C, B, D], [C, A, B, D], [C, D, A, B], etc.
    ///     We verify that A comes before B and C comes before D.
    /// </summary>
    [Test]
    public void SortKahn_DisconnectedDAG_ReturnsValidOrder()
    {
		_logger.LogDebug(">>> Entering SortKahn_DisconnectedDAG_ReturnsValidOrder()");
        // Arrange
        var graph = new DirectedWeightedGraph<string>(4);
		_logger.LogDebug("SortKahn_DisconnectedDAG_ReturnsValidOrder(): graph → {Graph}", graph);
        var vertexA = graph.AddVertex("A");
		_logger.LogDebug("SortKahn_DisconnectedDAG_ReturnsValidOrder(): vertexA → {VertexA}", vertexA);
        var vertexB = graph.AddVertex("B");
		_logger.LogDebug("SortKahn_DisconnectedDAG_ReturnsValidOrder(): vertexB → {VertexB}", vertexB);
        var vertexC = graph.AddVertex("C");
		_logger.LogDebug("SortKahn_DisconnectedDAG_ReturnsValidOrder(): vertexC → {VertexC}", vertexC);
        var vertexD = graph.AddVertex("D");
		_logger.LogDebug("SortKahn_DisconnectedDAG_ReturnsValidOrder(): vertexD → {VertexD}", vertexD);

        graph.AddEdge(vertexA, vertexB, 1);
        graph.AddEdge(vertexC, vertexD, 1);

        var topologicalSort = new TopologicalSort<string>();
		_logger.LogDebug("SortKahn_DisconnectedDAG_ReturnsValidOrder(): topologicalSort → {TopologicalSort}", topologicalSort);

        // Act
        var result = topologicalSort.SortKahn(graph);
		_logger.LogDebug("SortKahn_DisconnectedDAG_ReturnsValidOrder(): result → {Result}", result);

        // Assert
        Assert.That(result.Count, Is.EqualTo(4));

        // A should come before B
        var indexA = result.IndexOf(vertexA);
		_logger.LogDebug("SortKahn_DisconnectedDAG_ReturnsValidOrder(): indexA → {IndexA}", indexA);
        var indexB = result.IndexOf(vertexB);
		_logger.LogDebug("SortKahn_DisconnectedDAG_ReturnsValidOrder(): indexB → {IndexB}", indexB);
        Assert.That(indexB, Is.GreaterThan(indexA));

        // C should come before D
        var indexC = result.IndexOf(vertexC);
		_logger.LogDebug("SortKahn_DisconnectedDAG_ReturnsValidOrder(): indexC → {IndexC}", indexC);
        var indexD = result.IndexOf(vertexD);
		_logger.LogDebug("SortKahn_DisconnectedDAG_ReturnsValidOrder(): indexD → {IndexD}", indexD);
        Assert.That(indexD, Is.GreaterThan(indexC));
		_logger.LogDebug("<<< Exiting SortKahn_DisconnectedDAG_ReturnsValidOrder()");
    }

    /// <summary>
    ///     Test topological sort on a real-world scenario: course prerequisites.
    ///     Graph represents course dependencies:
    ///     - Intro to CS (A) is a prerequisite for Data Structures (B) and Algorithms (C).
    ///     - Data Structures (B) is a prerequisite for Advanced Algorithms (D).
    ///     - Algorithms (C) is a prerequisite for Advanced Algorithms (D).
    ///     Expected: A must come first, D must come last.
    /// </summary>
    [Test]
    public void Sort_CoursePrerequisitesScenario_ReturnsValidOrder()
    {
		_logger.LogDebug(">>> Entering Sort_CoursePrerequisitesScenario_ReturnsValidOrder()");
        // Arrange
        var graph = new DirectedWeightedGraph<string>(4);
		_logger.LogDebug("Sort_CoursePrerequisitesScenario_ReturnsValidOrder(): graph → {Graph}", graph);
        var introCS = graph.AddVertex("Intro to CS");
		_logger.LogDebug("Sort_CoursePrerequisitesScenario_ReturnsValidOrder(): introCS → {IntroCS}", introCS);
        var dataStructures = graph.AddVertex("Data Structures");
		_logger.LogDebug("Sort_CoursePrerequisitesScenario_ReturnsValidOrder(): dataStructures → {DataStructures}", dataStructures);
        var algorithms = graph.AddVertex("Algorithms");
		_logger.LogDebug("Sort_CoursePrerequisitesScenario_ReturnsValidOrder(): algorithms → {Algorithms}", algorithms);
        var advancedAlgorithms = graph.AddVertex("Advanced Algorithms");
		_logger.LogDebug("Sort_CoursePrerequisitesScenario_ReturnsValidOrder(): advancedAlgorithms → {AdvancedAlgorithms}", advancedAlgorithms);

        graph.AddEdge(introCS, dataStructures, 1);
        graph.AddEdge(introCS, algorithms, 1);
        graph.AddEdge(dataStructures, advancedAlgorithms, 1);
        graph.AddEdge(algorithms, advancedAlgorithms, 1);

        var topologicalSort = new TopologicalSort<string>();
		_logger.LogDebug("Sort_CoursePrerequisitesScenario_ReturnsValidOrder(): topologicalSort → {TopologicalSort}", topologicalSort);

        // Act
        var result = topologicalSort.Sort(graph);
		_logger.LogDebug("Sort_CoursePrerequisitesScenario_ReturnsValidOrder(): result → {Result}", result);

        // Assert
        Assert.That(result.Count, Is.EqualTo(4));
        Assert.That(result[0], Is.EqualTo(introCS)); // Must take Intro to CS first
        Assert.That(result[3], Is.EqualTo(advancedAlgorithms)); // Advanced Algorithms must be last

        // Data Structures and Algorithms should be in the middle
        var indexDS = result.IndexOf(dataStructures);
		_logger.LogDebug("Sort_CoursePrerequisitesScenario_ReturnsValidOrder(): indexDS → {IndexDS}", indexDS);
        var indexAlgo = result.IndexOf(algorithms);
		_logger.LogDebug("Sort_CoursePrerequisitesScenario_ReturnsValidOrder(): indexAlgo → {IndexAlgo}", indexAlgo);

        Assert.That(indexDS, Is.GreaterThan(0));
        Assert.That(indexDS, Is.LessThan(3));
        Assert.That(indexAlgo, Is.GreaterThan(0));
        Assert.That(indexAlgo, Is.LessThan(3));
		_logger.LogDebug("<<< Exiting Sort_CoursePrerequisitesScenario_ReturnsValidOrder()");
    }

    /// <summary>
    ///     Test Kahn's algorithm on a real-world scenario: course prerequisites.
    ///     Graph represents course dependencies:
    ///     - Intro to CS (A) is a prerequisite for Data Structures (B) and Algorithms (C).
    ///     - Data Structures (B) is a prerequisite for Advanced Algorithms (D).
    ///     - Algorithms (C) is a prerequisite for Advanced Algorithms (D).
    ///     Expected: A must come first, D must come last.
    /// </summary>
    [Test]
    public void SortKahn_CoursePrerequisitesScenario_ReturnsValidOrder()
    {
		_logger.LogDebug(">>> Entering SortKahn_CoursePrerequisitesScenario_ReturnsValidOrder()");
        // Arrange
        var graph = new DirectedWeightedGraph<string>(4);
		_logger.LogDebug("SortKahn_CoursePrerequisitesScenario_ReturnsValidOrder(): graph → {Graph}", graph);
        var introCS = graph.AddVertex("Intro to CS");
		_logger.LogDebug("SortKahn_CoursePrerequisitesScenario_ReturnsValidOrder(): introCS → {IntroCS}", introCS);
        var dataStructures = graph.AddVertex("Data Structures");
		_logger.LogDebug("SortKahn_CoursePrerequisitesScenario_ReturnsValidOrder(): dataStructures → {DataStructures}", dataStructures);
        var algorithms = graph.AddVertex("Algorithms");
		_logger.LogDebug("SortKahn_CoursePrerequisitesScenario_ReturnsValidOrder(): algorithms → {Algorithms}", algorithms);
        var advancedAlgorithms = graph.AddVertex("Advanced Algorithms");
		_logger.LogDebug("SortKahn_CoursePrerequisitesScenario_ReturnsValidOrder(): advancedAlgorithms → {AdvancedAlgorithms}", advancedAlgorithms);

        graph.AddEdge(introCS, dataStructures, 1);
        graph.AddEdge(introCS, algorithms, 1);
        graph.AddEdge(dataStructures, advancedAlgorithms, 1);
        graph.AddEdge(algorithms, advancedAlgorithms, 1);

        var topologicalSort = new TopologicalSort<string>();
		_logger.LogDebug("SortKahn_CoursePrerequisitesScenario_ReturnsValidOrder(): topologicalSort → {TopologicalSort}", topologicalSort);

        // Act
        var result = topologicalSort.SortKahn(graph);
		_logger.LogDebug("SortKahn_CoursePrerequisitesScenario_ReturnsValidOrder(): result → {Result}", result);

        // Assert
        Assert.That(result.Count, Is.EqualTo(4));
        Assert.That(result[0], Is.EqualTo(introCS)); // Must take Intro to CS first
        Assert.That(result[3], Is.EqualTo(advancedAlgorithms)); // Advanced Algorithms must be last

        // Data Structures and Algorithms should be in the middle
        var indexDS = result.IndexOf(dataStructures);
		_logger.LogDebug("SortKahn_CoursePrerequisitesScenario_ReturnsValidOrder(): indexDS → {IndexDS}", indexDS);
        var indexAlgo = result.IndexOf(algorithms);
		_logger.LogDebug("SortKahn_CoursePrerequisitesScenario_ReturnsValidOrder(): indexAlgo → {IndexAlgo}", indexAlgo);

        Assert.That(indexDS, Is.GreaterThan(0));
        Assert.That(indexDS, Is.LessThan(3));
        Assert.That(indexAlgo, Is.GreaterThan(0));
        Assert.That(indexAlgo, Is.LessThan(3));
		_logger.LogDebug("<<< Exiting SortKahn_CoursePrerequisitesScenario_ReturnsValidOrder()");
    }

    /// <summary>
    ///     Test topological sort with integer vertices.
    ///     Graph: 1 → 2 → 3.
    ///     Expected order: [1, 2, 3].
    /// </summary>
    [Test]
    public void Sort_IntegerVertices_ReturnsCorrectOrder()
    {
		_logger.LogDebug(">>> Entering Sort_IntegerVertices_ReturnsCorrectOrder()");
        // Arrange
        var graph = new DirectedWeightedGraph<int>(3);
		_logger.LogDebug("Sort_IntegerVertices_ReturnsCorrectOrder(): graph → {Graph}", graph);
        var vertex1 = graph.AddVertex(1);
		_logger.LogDebug("Sort_IntegerVertices_ReturnsCorrectOrder(): vertex1 → {Vertex1}", vertex1);
        var vertex2 = graph.AddVertex(2);
		_logger.LogDebug("Sort_IntegerVertices_ReturnsCorrectOrder(): vertex2 → {Vertex2}", vertex2);
        var vertex3 = graph.AddVertex(3);
		_logger.LogDebug("Sort_IntegerVertices_ReturnsCorrectOrder(): vertex3 → {Vertex3}", vertex3);

        graph.AddEdge(vertex1, vertex2, 1);
        graph.AddEdge(vertex2, vertex3, 1);

        var topologicalSort = new TopologicalSort<int>();
		_logger.LogDebug("Sort_IntegerVertices_ReturnsCorrectOrder(): topologicalSort → {TopologicalSort}", topologicalSort);

        // Act
        var result = topologicalSort.Sort(graph);
		_logger.LogDebug("Sort_IntegerVertices_ReturnsCorrectOrder(): result → {Result}", result);

        // Assert
        Assert.That(result.Count, Is.EqualTo(3));
        Assert.That(result[0], Is.EqualTo(vertex1));
        Assert.That(result[1], Is.EqualTo(vertex2));
        Assert.That(result[2], Is.EqualTo(vertex3));
		_logger.LogDebug("<<< Exiting Sort_IntegerVertices_ReturnsCorrectOrder()");
    }

    /// <summary>
    ///     Test Kahn's algorithm with integer vertices.
    ///     Graph: 1 → 2 → 3.
    ///     Expected order: [1, 2, 3].
    /// </summary>
    [Test]
    public void SortKahn_IntegerVertices_ReturnsCorrectOrder()
    {
		_logger.LogDebug(">>> Entering SortKahn_IntegerVertices_ReturnsCorrectOrder()");
        // Arrange
        var graph = new DirectedWeightedGraph<int>(3);
		_logger.LogDebug("SortKahn_IntegerVertices_ReturnsCorrectOrder(): graph → {Graph}", graph);
        var vertex1 = graph.AddVertex(1);
		_logger.LogDebug("SortKahn_IntegerVertices_ReturnsCorrectOrder(): vertex1 → {Vertex1}", vertex1);
        var vertex2 = graph.AddVertex(2);
		_logger.LogDebug("SortKahn_IntegerVertices_ReturnsCorrectOrder(): vertex2 → {Vertex2}", vertex2);
        var vertex3 = graph.AddVertex(3);
		_logger.LogDebug("SortKahn_IntegerVertices_ReturnsCorrectOrder(): vertex3 → {Vertex3}", vertex3);

        graph.AddEdge(vertex1, vertex2, 1);
        graph.AddEdge(vertex2, vertex3, 1);

        var topologicalSort = new TopologicalSort<int>();
		_logger.LogDebug("SortKahn_IntegerVertices_ReturnsCorrectOrder(): topologicalSort → {TopologicalSort}", topologicalSort);

        // Act
        var result = topologicalSort.SortKahn(graph);
		_logger.LogDebug("SortKahn_IntegerVertices_ReturnsCorrectOrder(): result → {Result}", result);

        // Assert
        Assert.That(result.Count, Is.EqualTo(3));
        Assert.That(result[0], Is.EqualTo(vertex1));
        Assert.That(result[1], Is.EqualTo(vertex2));
        Assert.That(result[2], Is.EqualTo(vertex3));
		_logger.LogDebug("<<< Exiting SortKahn_IntegerVertices_ReturnsCorrectOrder()");
    }

    /// <summary>
    ///     Test topological sort on a graph with self-loop (cycle).
    ///     Graph: A → A (self-loop).
    ///     Should throw InvalidOperationException.
    /// </summary>
    [Test]
    public void Sort_GraphWithSelfLoop_ThrowsInvalidOperationException()
    {
		_logger.LogDebug(">>> Entering Sort_GraphWithSelfLoop_ThrowsInvalidOperationException()");
        // Arrange
        var graph = new DirectedWeightedGraph<string>(1);
		_logger.LogDebug("Sort_GraphWithSelfLoop_ThrowsInvalidOperationException(): graph → {Graph}", graph);
        var vertexA = graph.AddVertex("A");
		_logger.LogDebug("Sort_GraphWithSelfLoop_ThrowsInvalidOperationException(): vertexA → {VertexA}", vertexA);

        graph.AddEdge(vertexA, vertexA, 1); // Self-loop

        var topologicalSort = new TopologicalSort<string>();
		_logger.LogDebug("Sort_GraphWithSelfLoop_ThrowsInvalidOperationException(): topologicalSort → {TopologicalSort}", topologicalSort);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => topologicalSort.Sort(graph));
		_logger.LogDebug("<<< Exiting Sort_GraphWithSelfLoop_ThrowsInvalidOperationException()");
    }

    /// <summary>
    ///     Test Kahn's algorithm on a graph with self-loop (cycle).
    ///     Graph: A → A (self-loop).
    ///     Should throw InvalidOperationException.
    /// </summary>
    [Test]
    public void SortKahn_GraphWithSelfLoop_ThrowsInvalidOperationException()
    {
		_logger.LogDebug(">>> Entering SortKahn_GraphWithSelfLoop_ThrowsInvalidOperationException()");
        // Arrange
        var graph = new DirectedWeightedGraph<string>(1);
		_logger.LogDebug("SortKahn_GraphWithSelfLoop_ThrowsInvalidOperationException(): graph → {Graph}", graph);
        var vertexA = graph.AddVertex("A");
		_logger.LogDebug("SortKahn_GraphWithSelfLoop_ThrowsInvalidOperationException(): vertexA → {VertexA}", vertexA);

        graph.AddEdge(vertexA, vertexA, 1); // Self-loop

        var topologicalSort = new TopologicalSort<string>();
		_logger.LogDebug("SortKahn_GraphWithSelfLoop_ThrowsInvalidOperationException(): topologicalSort → {TopologicalSort}", topologicalSort);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => topologicalSort.SortKahn(graph));
		_logger.LogDebug("<<< Exiting SortKahn_GraphWithSelfLoop_ThrowsInvalidOperationException()");
    }
}
