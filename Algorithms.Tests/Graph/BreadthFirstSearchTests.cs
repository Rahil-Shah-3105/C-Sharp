using Algorithms.Graph;
using DataStructures.Graph;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Graph;

public class BreadthFirstSearchTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void VisitAll_ShouldCountNumberOfVisitedVertix_ResultShouldBeTheSameAsNumberOfVerticesInGraph()
    {
		_logger.LogError(">>> Entering VisitAll_ShouldCountNumberOfVisitedVertix_ResultShouldBeTheSameAsNumberOfVerticesInGraph()");
        //Arrange
        var graph = new DirectedWeightedGraph<int>(10);
		_logger.LogError("VisitAll_ShouldCountNumberOfVisitedVertix_ResultShouldBeTheSameAsNumberOfVerticesInGraph(): graph → {Graph}", graph);

        var vertex1 = graph.AddVertex(1);
		_logger.LogError("VisitAll_ShouldCountNumberOfVisitedVertix_ResultShouldBeTheSameAsNumberOfVerticesInGraph(): vertex1 → {Vertex1}", vertex1);

        var vertex2 = graph.AddVertex(20);
		_logger.LogError("VisitAll_ShouldCountNumberOfVisitedVertix_ResultShouldBeTheSameAsNumberOfVerticesInGraph(): vertex2 → {Vertex2}", vertex2);

        var vertex3 = graph.AddVertex(40);
		_logger.LogError("VisitAll_ShouldCountNumberOfVisitedVertix_ResultShouldBeTheSameAsNumberOfVerticesInGraph(): vertex3 → {Vertex3}", vertex3);

        var vertex4 = graph.AddVertex(40);
		_logger.LogError("VisitAll_ShouldCountNumberOfVisitedVertix_ResultShouldBeTheSameAsNumberOfVerticesInGraph(): vertex4 → {Vertex4}", vertex4);

        graph.AddEdge(vertex1, vertex2, 1);

        graph.AddEdge(vertex2, vertex3, 1);

        graph.AddEdge(vertex2, vertex4, 1);

        graph.AddEdge(vertex4, vertex1, 1);

        var dfsSearcher = new BreadthFirstSearch<int>();
		_logger.LogError("VisitAll_ShouldCountNumberOfVisitedVertix_ResultShouldBeTheSameAsNumberOfVerticesInGraph(): dfsSearcher → {DfsSearcher}", dfsSearcher);

        long countOfVisitedVertices = 0;

        //Act
        dfsSearcher.VisitAll(graph, vertex1, _ => countOfVisitedVertices++);

        //Assert
        Assert.That(graph.Count, Is.EqualTo(countOfVisitedVertices));
		_logger.LogWarning("<<< Exiting VisitAll_ShouldCountNumberOfVisitedVertix_ResultShouldBeTheSameAsNumberOfVerticesInGraph()");
    }

    [Test]
    public void VisitAll_ShouldCountNumberOfVisitedVerices_TwoSeparatedGraphInOne()
    {
		_logger.LogError(">>> Entering VisitAll_ShouldCountNumberOfVisitedVerices_TwoSeparatedGraphInOne()");
        //Arrange
        var graph = new DirectedWeightedGraph<int>(10);
		_logger.LogError("VisitAll_ShouldCountNumberOfVisitedVerices_TwoSeparatedGraphInOne(): graph → {Graph}", graph);

        var vertex1 = graph.AddVertex(1);
		_logger.LogError("VisitAll_ShouldCountNumberOfVisitedVerices_TwoSeparatedGraphInOne(): vertex1 → {Vertex1}", vertex1);

        var vertex2 = graph.AddVertex(20);
		_logger.LogError("VisitAll_ShouldCountNumberOfVisitedVerices_TwoSeparatedGraphInOne(): vertex2 → {Vertex2}", vertex2);

        var vertex3 = graph.AddVertex(40);
		_logger.LogError("VisitAll_ShouldCountNumberOfVisitedVerices_TwoSeparatedGraphInOne(): vertex3 → {Vertex3}", vertex3);

        var vertex4 = graph.AddVertex(40);
		_logger.LogError("VisitAll_ShouldCountNumberOfVisitedVerices_TwoSeparatedGraphInOne(): vertex4 → {Vertex4}", vertex4);

        var vertex5 = graph.AddVertex(40);
		_logger.LogError("VisitAll_ShouldCountNumberOfVisitedVerices_TwoSeparatedGraphInOne(): vertex5 → {Vertex5}", vertex5);

        var vertex6 = graph.AddVertex(40);
		_logger.LogError("VisitAll_ShouldCountNumberOfVisitedVerices_TwoSeparatedGraphInOne(): vertex6 → {Vertex6}", vertex6);

        graph.AddEdge(vertex1, vertex2, 1);

        graph.AddEdge(vertex2, vertex3, 1);

        graph.AddEdge(vertex4, vertex5, 1);

        graph.AddEdge(vertex5, vertex6, 1);

        var dfsSearcher = new BreadthFirstSearch<int>();
		_logger.LogError("VisitAll_ShouldCountNumberOfVisitedVerices_TwoSeparatedGraphInOne(): dfsSearcher → {DfsSearcher}", dfsSearcher);

        long countOfVisitedVerticesPerFirstGraph = 0;

        long countOfVisitedVerticesPerSecondGraph = 0;

        //Act
        dfsSearcher.VisitAll(graph, vertex1, _ => countOfVisitedVerticesPerFirstGraph++);

        dfsSearcher.VisitAll(graph, vertex4, _ => countOfVisitedVerticesPerSecondGraph++);

        //Assert
        Assert.That(countOfVisitedVerticesPerFirstGraph, Is.EqualTo(3));

        Assert.That(countOfVisitedVerticesPerSecondGraph, Is.EqualTo(3));
		_logger.LogWarning("<<< Exiting VisitAll_ShouldCountNumberOfVisitedVerices_TwoSeparatedGraphInOne()");
    }

    [Test]
    public void VisitAll_ReturnTheSuqenceOfVertices_ShouldBeTheSameAsExpected()
    {
		_logger.LogError(">>> Entering VisitAll_ReturnTheSuqenceOfVertices_ShouldBeTheSameAsExpected()");
        //Arrange
        var graph = new DirectedWeightedGraph<int>(10);
		_logger.LogError("VisitAll_ReturnTheSuqenceOfVertices_ShouldBeTheSameAsExpected(): graph → {Graph}", graph);

        var vertex1 = graph.AddVertex(1);
		_logger.LogError("VisitAll_ReturnTheSuqenceOfVertices_ShouldBeTheSameAsExpected(): vertex1 → {Vertex1}", vertex1);

        var vertex2 = graph.AddVertex(20);
		_logger.LogError("VisitAll_ReturnTheSuqenceOfVertices_ShouldBeTheSameAsExpected(): vertex2 → {Vertex2}", vertex2);

        var vertex3 = graph.AddVertex(40);
		_logger.LogError("VisitAll_ReturnTheSuqenceOfVertices_ShouldBeTheSameAsExpected(): vertex3 → {Vertex3}", vertex3);

        var vertex4 = graph.AddVertex(40);
		_logger.LogError("VisitAll_ReturnTheSuqenceOfVertices_ShouldBeTheSameAsExpected(): vertex4 → {Vertex4}", vertex4);

        var vertex5 = graph.AddVertex(40);
		_logger.LogError("VisitAll_ReturnTheSuqenceOfVertices_ShouldBeTheSameAsExpected(): vertex5 → {Vertex5}", vertex5);

        graph.AddEdge(vertex1, vertex2, 1);

        graph.AddEdge(vertex1, vertex5, 1);

        graph.AddEdge(vertex2, vertex3, 1);

        graph.AddEdge(vertex2, vertex5, 1);

        graph.AddEdge(vertex2, vertex4, 1);

        var dfsSearcher = new BreadthFirstSearch<int>();
		_logger.LogError("VisitAll_ReturnTheSuqenceOfVertices_ShouldBeTheSameAsExpected(): dfsSearcher → {DfsSearcher}", dfsSearcher);

        var expectedSequenceOfVisitedVertices = new List<Vertex<int>>
        {
            vertex1,
            vertex2,
            vertex5,
            vertex3,
            vertex4,
        };

        var sequenceOfVisitedVertices = new List<Vertex<int>>();
		_logger.LogError("VisitAll_ReturnTheSuqenceOfVertices_ShouldBeTheSameAsExpected(): sequenceOfVisitedVertices → {SequenceOfVisitedVertices}", sequenceOfVisitedVertices);

        //Act
        dfsSearcher.VisitAll(graph, vertex1, vertex => sequenceOfVisitedVertices.Add(vertex));

        //Assert
        Assert.That(sequenceOfVisitedVertices, Is.EqualTo(expectedSequenceOfVisitedVertices));
		_logger.LogWarning("<<< Exiting VisitAll_ReturnTheSuqenceOfVertices_ShouldBeTheSameAsExpected()");
    }
}
