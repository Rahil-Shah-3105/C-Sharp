using DataStructures.Graph;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Graph;

[TestFixture]
public class DirectedWeightedGraphTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(-1)]
    [TestCase(-2)]
    [TestCase(-3)]
    public void GraphInitializationTest_ShouldThrowOverflow(int capacity)
    {
		_logger.LogDebug(">>> Entering GraphInitializationTest_ShouldThrowOverflow(capacity={Capacity})", capacity);
        Func<DirectedWeightedGraph<char>> createGraph = () => new DirectedWeightedGraph<char>(capacity);

        createGraph.Should().Throw<InvalidOperationException>()
            .WithMessage("Graph capacity should always be a non-negative integer.");
			_logger.LogDebug("<<< Exiting GraphInitializationTest_ShouldThrowOverflow(capacity={Capacity})", capacity);
    }

    [TestCase(1)]
    [TestCase(10)]
    [TestCase(20)]
    [TestCase(30)]
    public void GraphInitializationTest_Success(int capacity)
    {
		_logger.LogDebug(">>> Entering GraphInitializationTest_Success(capacity={Capacity})", capacity);
        Func<DirectedWeightedGraph<char>> createGraph = () => new DirectedWeightedGraph<char>(capacity);

        createGraph.Should().NotThrow();
		_logger.LogDebug("<<< Exiting GraphInitializationTest_Success(capacity={Capacity})", capacity);
    }

    [Test]
    public void GraphAddVertexTest_Success()
    {
		_logger.LogDebug(">>> Entering GraphAddVertexTest_Success()");
        var graph = new DirectedWeightedGraph<char>(10);

		_logger.LogDebug("GraphAddVertexTest_Success(): graph → {Graph}", graph);
        graph.AddVertex('A');
        graph.AddVertex('B');
        graph.AddVertex('C');

        graph.Count.Should().Be(3);
		_logger.LogDebug("<<< Exiting GraphAddVertexTest_Success()");
    }

    [Test]
    public void GraphAddVertexTest_ShouldThrowOverflow()
    {
		_logger.LogDebug(">>> Entering GraphAddVertexTest_ShouldThrowOverflow()");
        var graph = new DirectedWeightedGraph<char>(10);
        for (var i = 0; i < 10; i++)
        {
            graph.AddVertex('A');
        }

        Action addOverflow = () => graph.AddVertex('A');
		_logger.LogDebug("GraphAddVertexTest_ShouldThrowOverflow(): addOverflow → {AddOverflow}", addOverflow);

        graph.Count.Should().Be(10);
        graph.Vertices.Should().OnlyContain(x => x != null && x.Data == 'A');
        addOverflow.Should().Throw<InvalidOperationException>()
            .WithMessage("Graph overflow.");
			_logger.LogDebug("<<< Exiting GraphAddVertexTest_ShouldThrowOverflow()");
    }

    [Test]
    public void GraphRemoveVertexTest_Success()
    {
		_logger.LogDebug(">>> Entering GraphRemoveVertexTest_Success()");
        var graph = new DirectedWeightedGraph<char>(10);
        var vertexA = graph.AddVertex('A');
		_logger.LogDebug("GraphRemoveVertexTest_Success(): vertexA → {VertexA}", vertexA);
        var vertexB = graph.AddVertex('B');
		_logger.LogDebug("GraphRemoveVertexTest_Success(): vertexB → {VertexB}", vertexB);
        var vertexC = graph.AddVertex('C');
		_logger.LogDebug("GraphRemoveVertexTest_Success(): vertexC → {VertexC}", vertexC);
		_logger.LogDebug("GraphRemoveVertexTest_Success(): graph → {Graph}", graph);
        graph.AddEdge(vertexB, vertexA, 5);
        graph.AddEdge(vertexC, vertexA, 5);
        var neighborsB = graph.GetNeighbors(vertexB).ToList();
		_logger.LogDebug("GraphRemoveVertexTest_Success(): neighborsB → {NeighborsB}", neighborsB);
        var neighborsC = graph.GetNeighbors(vertexC).ToList();
		_logger.LogDebug("GraphRemoveVertexTest_Success(): neighborsC → {NeighborsC}", neighborsC);

        graph.RemoveVertex(vertexA);

        neighborsB.Should().HaveCount(1);
        neighborsB[0].Should().Be(vertexA);
        neighborsC.Should().HaveCount(1);
        neighborsC[0].Should().Be(vertexA);
        graph.GetNeighbors(vertexB).Should().HaveCount(0);
        graph.GetNeighbors(vertexC).Should().HaveCount(0);
		_logger.LogDebug("<<< Exiting GraphRemoveVertexTest_Success()");
    }

    [Test]
    public void GraphRemoveAndAddVertexTest_Success()
    {
		_logger.LogDebug(">>> Entering GraphRemoveAndAddVertexTest_Success()");
        double weight_A_B = 1;
        double weight_A_C = 2;
        double weight_A_D = 3;
        double weight_B_A = 4;
        double weight_B_C = 5;
        double weight_C_A = 6;
        double weight_C_B = 7;
        double weight_C_D = 8;
        double weight_D_A = 9;
        double weight_D_C = 10;

        var graph = new DirectedWeightedGraph<char>(10);
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): graph → {Graph}", graph);
        var vertexA = graph.AddVertex('A');
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): vertexA → {VertexA}", vertexA);
        var vertexB = graph.AddVertex('B');
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): vertexB → {VertexB}", vertexB);
        var vertexC = graph.AddVertex('C');
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): vertexC → {VertexC}", vertexC);
        graph.AddEdge(vertexA, vertexB, weight_A_B);
        graph.AddEdge(vertexA, vertexC, weight_A_C);
        graph.AddEdge(vertexB, vertexA, weight_B_A);
        graph.AddEdge(vertexB, vertexC, weight_B_C);
        graph.AddEdge(vertexC, vertexA, weight_C_A);
        graph.AddEdge(vertexC, vertexB, weight_C_B);

        var vertexA_Index_BeforeUpdate = vertexA.Index;
        vertexA_Index_BeforeUpdate.Should().Be(0);
        var neighborsA_BeforeUpdate = graph.GetNeighbors(vertexA).ToList();
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): neighborsA_BeforeUpdate → {NeighborsA_BeforeUpdate}", neighborsA_BeforeUpdate);
        neighborsA_BeforeUpdate.Should().HaveCount(2);
        neighborsA_BeforeUpdate[0].Should().Be(vertexB);
        neighborsA_BeforeUpdate[1].Should().Be(vertexC);

        var vertexB_Index_BeforeUpdate = vertexB.Index;
        vertexB_Index_BeforeUpdate.Should().Be(1);
        var neighborsB_BeforeUpdate = graph.GetNeighbors(vertexB).ToList();
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): neighborsB_BeforeUpdate → {NeighborsB_BeforeUpdate}", neighborsB_BeforeUpdate);
        neighborsB_BeforeUpdate.Should().HaveCount(2);
        neighborsB_BeforeUpdate[0].Should().Be(vertexA);
        neighborsB_BeforeUpdate[1].Should().Be(vertexC);

        var vertexC_Index_BeforeUpdate = vertexC.Index;
        vertexC_Index_BeforeUpdate.Should().Be(2);
        var neighborsC_BeforeUpdate = graph.GetNeighbors(vertexC).ToList();
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): neighborsC_BeforeUpdate → {NeighborsC_BeforeUpdate}", neighborsC_BeforeUpdate);
        neighborsC_BeforeUpdate.Should().HaveCount(2);
        neighborsC_BeforeUpdate[0].Should().Be(vertexA);
        neighborsC_BeforeUpdate[1].Should().Be(vertexB);

        var weight_A_B_BeforeUpdate = graph.AdjacentDistance(vertexA, vertexB);
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): weight_A_B_BeforeUpdate → {Weight_A_B_BeforeUpdate}", weight_A_B_BeforeUpdate);
        var weight_A_C_BeforeUpdate = graph.AdjacentDistance(vertexA, vertexC);
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): weight_A_C_BeforeUpdate → {Weight_A_C_BeforeUpdate}", weight_A_C_BeforeUpdate);
        var weight_B_A_BeforeUpdate = graph.AdjacentDistance(vertexB, vertexA);
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): weight_B_A_BeforeUpdate → {Weight_B_A_BeforeUpdate}", weight_B_A_BeforeUpdate);
        var weight_B_C_BeforeUpdate = graph.AdjacentDistance(vertexB, vertexC);
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): weight_B_C_BeforeUpdate → {Weight_B_C_BeforeUpdate}", weight_B_C_BeforeUpdate);
        var weight_C_A_BeforeUpdate = graph.AdjacentDistance(vertexC, vertexA);
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): weight_C_A_BeforeUpdate → {Weight_C_A_BeforeUpdate}", weight_C_A_BeforeUpdate);
        var weight_C_B_BeforeUpdate = graph.AdjacentDistance(vertexC, vertexB);
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): weight_C_B_BeforeUpdate → {Weight_C_B_BeforeUpdate}", weight_C_B_BeforeUpdate);
        weight_A_B_BeforeUpdate.Should().Be(weight_A_B);
        weight_A_C_BeforeUpdate.Should().Be(weight_A_C);
        weight_B_A_BeforeUpdate.Should().Be(weight_B_A);
        weight_B_C_BeforeUpdate.Should().Be(weight_B_C);
        weight_C_A_BeforeUpdate.Should().Be(weight_C_A);
        weight_C_B_BeforeUpdate.Should().Be(weight_C_B);

        graph.RemoveVertex(vertexB);
        var vertexD = graph.AddVertex('D');
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): vertexD → {VertexD}", vertexD);
        graph.AddEdge(vertexA, vertexD, weight_A_D);
        graph.AddEdge(vertexC, vertexD, weight_C_D);
        graph.AddEdge(vertexD, vertexA, weight_D_A);
        graph.AddEdge(vertexD, vertexC, weight_D_C);

        var vertexA_Index_AfterUpdate = vertexA.Index;
        vertexA_Index_AfterUpdate.Should().Be(0);
        var neighborsA_AfterUpdate = graph.GetNeighbors(vertexA).ToList();
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): neighborsA_AfterUpdate → {NeighborsA_AfterUpdate}", neighborsA_AfterUpdate);
        neighborsA_AfterUpdate.Should().HaveCount(2);
        neighborsA_AfterUpdate[0].Should().Be(vertexC);
        neighborsA_AfterUpdate[1].Should().Be(vertexD);

        var vertexC_Index_AfterUpdate = vertexC.Index;
        vertexC_Index_AfterUpdate.Should().Be(1);
        var neighborsC_AfterUpdate = graph.GetNeighbors(vertexC).ToList();
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): neighborsC_AfterUpdate → {NeighborsC_AfterUpdate}", neighborsC_AfterUpdate);
        neighborsC_AfterUpdate.Should().HaveCount(2);
        neighborsC_AfterUpdate[0].Should().Be(vertexA);
        neighborsC_AfterUpdate[1].Should().Be(vertexD);

        var vertexD_Index_AfterUpdate = vertexD.Index;
        vertexD_Index_AfterUpdate.Should().Be(2);
        var neighborsD_AfterUpdate = graph.GetNeighbors(vertexD).ToList();
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): neighborsD_AfterUpdate → {NeighborsD_AfterUpdate}", neighborsD_AfterUpdate);
        neighborsD_AfterUpdate.Should().HaveCount(2);
        neighborsD_AfterUpdate[0].Should().Be(vertexA);
        neighborsD_AfterUpdate[1].Should().Be(vertexC);

        var weight_A_C_AfterUpdate = graph.AdjacentDistance(vertexA, vertexC);
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): weight_A_C_AfterUpdate → {Weight_A_C_AfterUpdate}", weight_A_C_AfterUpdate);
        var weight_A_D_AfterUpdate = graph.AdjacentDistance(vertexA, vertexD);
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): weight_A_D_AfterUpdate → {Weight_A_D_AfterUpdate}", weight_A_D_AfterUpdate);
        var weight_C_A_AfterUpdate = graph.AdjacentDistance(vertexC, vertexA);
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): weight_C_A_AfterUpdate → {Weight_C_A_AfterUpdate}", weight_C_A_AfterUpdate);
        var weight_C_D_AfterUpdate = graph.AdjacentDistance(vertexC, vertexD);
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): weight_C_D_AfterUpdate → {Weight_C_D_AfterUpdate}", weight_C_D_AfterUpdate);
        var weight_D_A_AfterUpdate = graph.AdjacentDistance(vertexD, vertexA);
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): weight_D_A_AfterUpdate → {Weight_D_A_AfterUpdate}", weight_D_A_AfterUpdate);
        var weight_D_C_AfterUpdate = graph.AdjacentDistance(vertexD, vertexC);
		_logger.LogDebug("GraphRemoveAndAddVertexTest_Success(): weight_D_C_AfterUpdate → {Weight_D_C_AfterUpdate}", weight_D_C_AfterUpdate);
        weight_A_D_AfterUpdate.Should().Be(weight_A_D);
        weight_A_C_AfterUpdate.Should().Be(weight_A_C);
        weight_D_A_AfterUpdate.Should().Be(weight_D_A);
        weight_D_C_AfterUpdate.Should().Be(weight_D_C);
        weight_C_A_AfterUpdate.Should().Be(weight_C_A);
        weight_C_D_AfterUpdate.Should().Be(weight_C_D);
		_logger.LogDebug("<<< Exiting GraphRemoveAndAddVertexTest_Success()");
    }

    [Test]
    public void GraphRemoveVertexTest_ShouldThrowVertexNotInGraph()
    {
		_logger.LogDebug(">>> Entering GraphRemoveVertexTest_ShouldThrowVertexNotInGraph()");
        var graph = new DirectedWeightedGraph<char>(10);
        var vertexA = new Vertex<char>('A', 0);
		_logger.LogDebug("GraphRemoveVertexTest_ShouldThrowVertexNotInGraph(): vertexA → {VertexA}", vertexA);
		_logger.LogDebug("GraphRemoveVertexTest_ShouldThrowVertexNotInGraph(): graph → {Graph}", graph);

        Action removeVertex = () => graph.RemoveVertex(vertexA);
		_logger.LogDebug("GraphRemoveVertexTest_ShouldThrowVertexNotInGraph(): removeVertex → {RemoveVertex}", removeVertex);

        removeVertex.Should().Throw<InvalidOperationException>()
            .WithMessage($"Vertex does not belong to graph: {vertexA}.");
			_logger.LogDebug("<<< Exiting GraphRemoveVertexTest_ShouldThrowVertexNotInGraph()");
    }

    [Test]
    public void GraphAddEdgeTest_Success()
    {
		_logger.LogDebug(">>> Entering GraphAddEdgeTest_Success()");
        var graph = new DirectedWeightedGraph<char>(10);
        var vertexA = graph.AddVertex('A');
		_logger.LogDebug("GraphAddEdgeTest_Success(): vertexA → {VertexA}", vertexA);
        var vertexB = graph.AddVertex('B');
		_logger.LogDebug("GraphAddEdgeTest_Success(): vertexB → {VertexB}", vertexB);
        var vertexC = graph.AddVertex('C');
		_logger.LogDebug("GraphAddEdgeTest_Success(): vertexC → {VertexC}", vertexC);
		_logger.LogDebug("GraphAddEdgeTest_Success(): graph → {Graph}", graph);

        graph.AddEdge(vertexA, vertexB, 5);

        graph.AreAdjacent(vertexA, vertexB).Should().BeTrue();
        graph.AreAdjacent(vertexA, vertexC).Should().BeFalse();
		_logger.LogDebug("<<< Exiting GraphAddEdgeTest_Success()");
    }

    [Test]
    public void GraphAddEdgeTest_ShouldThrowZeroWeight()
    {
		_logger.LogDebug(">>> Entering GraphAddEdgeTest_ShouldThrowZeroWeight()");
        var graph = new DirectedWeightedGraph<char>(10);
        var vertexA = graph.AddVertex('A');
		_logger.LogDebug("GraphAddEdgeTest_ShouldThrowZeroWeight(): vertexA → {VertexA}", vertexA);
        var vertexB = graph.AddVertex('B');
		_logger.LogDebug("GraphAddEdgeTest_ShouldThrowZeroWeight(): vertexB → {VertexB}", vertexB);
		_logger.LogDebug("GraphAddEdgeTest_ShouldThrowZeroWeight(): graph → {Graph}", graph);

        Action addZeroEdge = () => graph.AddEdge(vertexA, vertexB, 0);
		_logger.LogDebug("GraphAddEdgeTest_ShouldThrowZeroWeight(): addZeroEdge → {AddZeroEdge}", addZeroEdge);

        addZeroEdge.Should().Throw<InvalidOperationException>()
            .WithMessage("Edge weight cannot be zero.");
			_logger.LogDebug("<<< Exiting GraphAddEdgeTest_ShouldThrowZeroWeight()");
    }

    [Test]
    public void GraphAddEdgeTest_ShouldThrowVertexNotInGraph()
    {
		_logger.LogDebug(">>> Entering GraphAddEdgeTest_ShouldThrowVertexNotInGraph()");
        var graph = new DirectedWeightedGraph<char>(10);
        var vertexA = graph.AddVertex('A');
		_logger.LogDebug("GraphAddEdgeTest_ShouldThrowVertexNotInGraph(): vertexA → {VertexA}", vertexA);
        var vertexB = new Vertex<char>('B', 1);
		_logger.LogDebug("GraphAddEdgeTest_ShouldThrowVertexNotInGraph(): vertexB → {VertexB}", vertexB);
		_logger.LogDebug("GraphAddEdgeTest_ShouldThrowVertexNotInGraph(): graph → {Graph}", graph);

        Action addZeroEdge = () => graph.AddEdge(vertexA, vertexB, 0);
		_logger.LogDebug("GraphAddEdgeTest_ShouldThrowVertexNotInGraph(): addZeroEdge → {AddZeroEdge}", addZeroEdge);

        addZeroEdge.Should().Throw<InvalidOperationException>()
            .WithMessage($"Vertex does not belong to graph: {vertexB}.");
			_logger.LogDebug("<<< Exiting GraphAddEdgeTest_ShouldThrowVertexNotInGraph()");
    }

    [Test]
    public void GraphAddEdgeTest_ShouldThrowEdgeExists()
    {
		_logger.LogDebug(">>> Entering GraphAddEdgeTest_ShouldThrowEdgeExists()");
        var graph = new DirectedWeightedGraph<char>(10);
        var vertexA = graph.AddVertex('A');
		_logger.LogDebug("GraphAddEdgeTest_ShouldThrowEdgeExists(): vertexA → {VertexA}", vertexA);
        var vertexB = graph.AddVertex('B');
		_logger.LogDebug("GraphAddEdgeTest_ShouldThrowEdgeExists(): vertexB → {VertexB}", vertexB);
		_logger.LogDebug("GraphAddEdgeTest_ShouldThrowEdgeExists(): graph → {Graph}", graph);
        const int currentEdgeWeight = 5;
        graph.AddEdge(vertexA, vertexB, currentEdgeWeight);

        Action addZeroEdge = () => graph.AddEdge(vertexA, vertexB, 10);
		_logger.LogDebug("GraphAddEdgeTest_ShouldThrowEdgeExists(): addZeroEdge → {AddZeroEdge}", addZeroEdge);

        addZeroEdge.Should().Throw<InvalidOperationException>()
            .WithMessage($"Vertex already exists: {currentEdgeWeight}");
			_logger.LogDebug("<<< Exiting GraphAddEdgeTest_ShouldThrowEdgeExists()");
    }

    [Test]
    public void GraphRemoveEdgeTest_Success()
    {
		_logger.LogDebug(">>> Entering GraphRemoveEdgeTest_Success()");
        var graph = new DirectedWeightedGraph<char>(10);
        var vertexA = graph.AddVertex('A');
		_logger.LogDebug("GraphRemoveEdgeTest_Success(): vertexA → {VertexA}", vertexA);
        var vertexB = graph.AddVertex('B');
		_logger.LogDebug("GraphRemoveEdgeTest_Success(): vertexB → {VertexB}", vertexB);
		_logger.LogDebug("GraphRemoveEdgeTest_Success(): graph → {Graph}", graph);
        graph.AddEdge(vertexA, vertexB, 5);

        graph.RemoveEdge(vertexA, vertexB);

        graph.AreAdjacent(vertexA, vertexB).Should().BeFalse();
		_logger.LogDebug("<<< Exiting GraphRemoveEdgeTest_Success()");
    }

    [Test]
    public void GraphRemoveEdgeTest_ShouldThrowVertexNotInGraph()
    {
		_logger.LogDebug(">>> Entering GraphRemoveEdgeTest_ShouldThrowVertexNotInGraph()");
        var graph = new DirectedWeightedGraph<char>(10);
        var vertexA = graph.AddVertex('A');
		_logger.LogDebug("GraphRemoveEdgeTest_ShouldThrowVertexNotInGraph(): vertexA → {VertexA}", vertexA);
        var vertexB = new Vertex<char>('B', 1);
		_logger.LogDebug("GraphRemoveEdgeTest_ShouldThrowVertexNotInGraph(): vertexB → {VertexB}", vertexB);
		_logger.LogDebug("GraphRemoveEdgeTest_ShouldThrowVertexNotInGraph(): graph → {Graph}", graph);

        Action removeEdge = () => graph.RemoveEdge(vertexA, vertexB);
		_logger.LogDebug("GraphRemoveEdgeTest_ShouldThrowVertexNotInGraph(): removeEdge → {RemoveEdge}", removeEdge);

        removeEdge.Should().Throw<InvalidOperationException>()
            .WithMessage($"Vertex does not belong to graph: {vertexB}.");
			_logger.LogDebug("<<< Exiting GraphRemoveEdgeTest_ShouldThrowVertexNotInGraph()");
    }

    [Test]
    public void GraphGetNeighborsTest_Success()
    {
		_logger.LogDebug(">>> Entering GraphGetNeighborsTest_Success()");
        var graph = new DirectedWeightedGraph<char>(10);
        var vertexA = graph.AddVertex('A');
		_logger.LogDebug("GraphGetNeighborsTest_Success(): vertexA → {VertexA}", vertexA);
        var vertexB = graph.AddVertex('B');
		_logger.LogDebug("GraphGetNeighborsTest_Success(): vertexB → {VertexB}", vertexB);
        var vertexC = graph.AddVertex('C');
		_logger.LogDebug("GraphGetNeighborsTest_Success(): vertexC → {VertexC}", vertexC);
        var vertexD = graph.AddVertex('D');
		_logger.LogDebug("GraphGetNeighborsTest_Success(): vertexD → {VertexD}", vertexD);
		_logger.LogDebug("GraphGetNeighborsTest_Success(): graph → {Graph}", graph);
        graph.AddEdge(vertexA, vertexB, 5);
        graph.AddEdge(vertexA, vertexC, 5);
        graph.AddEdge(vertexA, vertexD, 5);

        var neighborsA = graph.GetNeighbors(vertexA).ToArray();
		_logger.LogDebug("GraphGetNeighborsTest_Success(): neighborsA → {NeighborsA}", neighborsA);

        neighborsA.Should().HaveCount(3);
        neighborsA[0].Should().Be(vertexB);
        neighborsA[1].Should().Be(vertexC);
        neighborsA[2].Should().Be(vertexD);
		_logger.LogDebug("<<< Exiting GraphGetNeighborsTest_Success()");
    }

    [Test]
    public void GraphGetNeighborsTest_ShouldThrowVertexNotInGraph()
    {
		_logger.LogDebug(">>> Entering GraphGetNeighborsTest_ShouldThrowVertexNotInGraph()");
        var graph = new DirectedWeightedGraph<char>(10);
        var vertexA = new Vertex<char>('A', 0);
		_logger.LogDebug("GraphGetNeighborsTest_ShouldThrowVertexNotInGraph(): vertexA → {VertexA}", vertexA);
		_logger.LogDebug("GraphGetNeighborsTest_ShouldThrowVertexNotInGraph(): graph → {Graph}", graph);

        Func<List<Vertex<char>?>> getNeighbors = () =>
        {
            var enumerable = graph.GetNeighbors(vertexA);
			_logger.LogDebug("GraphGetNeighborsTest_ShouldThrowVertexNotInGraph(): enumerable → {Enumerable}", enumerable);
            return enumerable.ToList();
        };

        getNeighbors.Should().Throw<InvalidOperationException>()
            .WithMessage($"Vertex does not belong to graph: {vertexA}.");
			_logger.LogDebug("<<< Exiting GraphGetNeighborsTest_ShouldThrowVertexNotInGraph()");
    }
}
