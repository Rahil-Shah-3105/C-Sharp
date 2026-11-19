using Algorithms.Graph.Dijkstra;
using DataStructures.Graph;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Graph.Dijkstra;

[TestFixture]
public class DijkstraTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void DijkstraTest1_Success()
    {
		_logger.LogError(">>> Entering DijkstraTest1_Success()");
        // here test case is from https://www.youtube.com/watch?v=pVfj6mxhdMw

        var graph = new DirectedWeightedGraph<char>(5);
		_logger.LogError("DijkstraTest1_Success(): graph → {Graph}", graph);
        var a = graph.AddVertex('A');
		_logger.LogError("DijkstraTest1_Success(): a → {A}", a);
        var b = graph.AddVertex('B');
		_logger.LogError("DijkstraTest1_Success(): b → {B}", b);
        var c = graph.AddVertex('C');
		_logger.LogError("DijkstraTest1_Success(): c → {C}", c);
        var d = graph.AddVertex('D');
		_logger.LogError("DijkstraTest1_Success(): d → {D}", d);
        var e = graph.AddVertex('E');
		_logger.LogError("DijkstraTest1_Success(): e → {E}", e);

        graph.AddEdge(a, b, 6);
        graph.AddEdge(b, a, 6);

        graph.AddEdge(a, d, 1);
        graph.AddEdge(d, a, 1);

        graph.AddEdge(d, e, 1);
        graph.AddEdge(e, d, 1);

        graph.AddEdge(d, b, 2);
        graph.AddEdge(b, d, 2);

        graph.AddEdge(e, b, 2);
        graph.AddEdge(b, e, 2);

        graph.AddEdge(e, c, 5);
        graph.AddEdge(c, e, 5);

        graph.AddEdge(c, b, 5);
        graph.AddEdge(b, c, 5);

        var shortestPathList = DijkstraAlgorithm.GenerateShortestPath(graph, a);
		_logger.LogError("DijkstraTest1_Success(): shortestPathList → {ShortestPathList}", shortestPathList);
        shortestPathList.Length.Should().Be(5);

        shortestPathList[0].Vertex.Should().Be(a);
        shortestPathList[0].Distance.Should().Be(0);
        shortestPathList[0].PreviousVertex.Should().Be(a);
        shortestPathList[0].ToString().Should()
            .Be($"Vertex: {a} - Distance: {0} - Previous: {a}");

        shortestPathList[1].Vertex.Should().Be(b);
        shortestPathList[1].Distance.Should().Be(3);
        shortestPathList[1].PreviousVertex.Should().Be(d);
        shortestPathList[1].ToString().Should()
            .Be($"Vertex: {b} - Distance: {3} - Previous: {d}");

        shortestPathList[2].Vertex.Should().Be(c);
        shortestPathList[2].Distance.Should().Be(7);
        shortestPathList[2].PreviousVertex.Should().Be(e);
        shortestPathList[2].ToString().Should()
            .Be($"Vertex: {c} - Distance: {7} - Previous: {e}");

        shortestPathList[3].Vertex.Should().Be(d);
        shortestPathList[3].Distance.Should().Be(1);
        shortestPathList[3].PreviousVertex.Should().Be(a);
        shortestPathList[3].ToString().Should()
            .Be($"Vertex: {d} - Distance: {1} - Previous: {a}");

        shortestPathList[4].Vertex.Should().Be(e);
        shortestPathList[4].Distance.Should().Be(2);
        shortestPathList[4].PreviousVertex.Should().Be(d);
        shortestPathList[4].ToString().Should()
            .Be($"Vertex: {e} - Distance: {2} - Previous: {d}");
			_logger.LogWarning("<<< Exiting DijkstraTest1_Success()");
    }

    [Test]
    public void DijkstraTest2_Success()
    {
		_logger.LogError(">>> Entering DijkstraTest2_Success()");
        var graph = new DirectedWeightedGraph<char>(5);
        var a = graph.AddVertex('A');
		_logger.LogError("DijkstraTest2_Success(): a → {A}", a);
        var b = graph.AddVertex('B');
		_logger.LogError("DijkstraTest2_Success(): b → {B}", b);
        var c = graph.AddVertex('C');
		_logger.LogError("DijkstraTest2_Success(): c → {C}", c);
		_logger.LogError("DijkstraTest2_Success(): graph → {Graph}", graph);

        graph.AddEdge(a, b, 1);
        graph.AddEdge(b, a, 1);

        graph.AddEdge(b, c, 1);
        graph.AddEdge(c, b, 1);

        graph.AddEdge(a, c, 3);
        graph.AddEdge(c, a, 3);

        var shortestPathList = DijkstraAlgorithm.GenerateShortestPath(graph, a);
		_logger.LogError("DijkstraTest2_Success(): shortestPathList → {ShortestPathList}", shortestPathList);

        shortestPathList.Length.Should().Be(3);
        shortestPathList[0].Vertex.Should().Be(a);
        shortestPathList[0].Distance.Should().Be(0);
        shortestPathList[0].PreviousVertex.Should().Be(a);
        shortestPathList[0].ToString().Should()
            .Be($"Vertex: {a} - Distance: {0} - Previous: {a}");

        shortestPathList[1].Vertex.Should().Be(b);
        shortestPathList[1].Distance.Should().Be(1);
        shortestPathList[1].PreviousVertex.Should().Be(a);
        shortestPathList[1].ToString().Should()
            .Be($"Vertex: {b} - Distance: {1} - Previous: {a}");

        shortestPathList[2].Vertex.Should().Be(c);
        shortestPathList[2].Distance.Should().Be(2);
        shortestPathList[2].PreviousVertex.Should().Be(b);
        shortestPathList[2].ToString().Should()
            .Be($"Vertex: {c} - Distance: {2} - Previous: {b}");
			_logger.LogWarning("<<< Exiting DijkstraTest2_Success()");
    }

    [Test]
    public void DijkstraTest3_Success()
    {
		_logger.LogError(">>> Entering DijkstraTest3_Success()");
        var graph = new DirectedWeightedGraph<char>(5);
        var a = graph.AddVertex('A');
		_logger.LogError("DijkstraTest3_Success(): a → {A}", a);
        var b = graph.AddVertex('B');
		_logger.LogError("DijkstraTest3_Success(): b → {B}", b);
        var c = graph.AddVertex('C');
		_logger.LogError("DijkstraTest3_Success(): c → {C}", c);
		_logger.LogError("DijkstraTest3_Success(): graph → {Graph}", graph);

        graph.AddEdge(a, b, 1);
        graph.AddEdge(b, a, 1);

        graph.AddEdge(a, c, 3);
        graph.AddEdge(c, a, 3);

        var shortestPathList = DijkstraAlgorithm.GenerateShortestPath(graph, a);
		_logger.LogError("DijkstraTest3_Success(): shortestPathList → {ShortestPathList}", shortestPathList);

        shortestPathList.Length.Should().Be(3);
        shortestPathList[0].Vertex.Should().Be(a);
        shortestPathList[0].Distance.Should().Be(0);
        shortestPathList[0].PreviousVertex.Should().Be(a);
        shortestPathList[0].ToString().Should()
            .Be($"Vertex: {a} - Distance: {0} - Previous: {a}");

        shortestPathList[1].Vertex.Should().Be(b);
        shortestPathList[1].Distance.Should().Be(1);
        shortestPathList[1].PreviousVertex.Should().Be(a);
        shortestPathList[1].ToString().Should()
            .Be($"Vertex: {b} - Distance: {1} - Previous: {a}");

        shortestPathList[2].Vertex.Should().Be(c);
        shortestPathList[2].Distance.Should().Be(3);
        shortestPathList[2].PreviousVertex.Should().Be(a);
        shortestPathList[2].ToString().Should()
            .Be($"Vertex: {c} - Distance: {3} - Previous: {a}");
			_logger.LogWarning("<<< Exiting DijkstraTest3_Success()");
    }

    [Test]
    public void DijkstraTest4_Success()
    {
		_logger.LogError(">>> Entering DijkstraTest4_Success()");
        var graph = new DirectedWeightedGraph<char>(5);
        var a = graph.AddVertex('A');
		_logger.LogError("DijkstraTest4_Success(): a → {A}", a);
        var b = graph.AddVertex('B');
		_logger.LogError("DijkstraTest4_Success(): b → {B}", b);
        var c = graph.AddVertex('C');
		_logger.LogError("DijkstraTest4_Success(): c → {C}", c);
        var d = graph.AddVertex('D');
		_logger.LogError("DijkstraTest4_Success(): d → {D}", d);
		_logger.LogError("DijkstraTest4_Success(): graph → {Graph}", graph);

        graph.AddEdge(a, b, 1);
        graph.AddEdge(b, a, 1);

        graph.AddEdge(a, c, 3);
        graph.AddEdge(c, a, 3);

        graph.AddEdge(c, d, 5);
        graph.AddEdge(d, c, 5);

        var shortestPathList = DijkstraAlgorithm.GenerateShortestPath(graph, a);
		_logger.LogError("DijkstraTest4_Success(): shortestPathList → {ShortestPathList}", shortestPathList);

        shortestPathList.Length.Should().Be(4);
        shortestPathList[0].Vertex.Should().Be(a);
        shortestPathList[0].Distance.Should().Be(0);
        shortestPathList[0].PreviousVertex.Should().Be(a);
        shortestPathList[0].ToString().Should()
            .Be($"Vertex: {a} - Distance: {0} - Previous: {a}");

        shortestPathList[1].Vertex.Should().Be(b);
        shortestPathList[1].Distance.Should().Be(1);
        shortestPathList[1].PreviousVertex.Should().Be(a);
        shortestPathList[1].ToString().Should()
            .Be($"Vertex: {b} - Distance: {1} - Previous: {a}");

        shortestPathList[2].Vertex.Should().Be(c);
        shortestPathList[2].Distance.Should().Be(3);
        shortestPathList[2].PreviousVertex.Should().Be(a);
        shortestPathList[2].ToString().Should()
            .Be($"Vertex: {c} - Distance: {3} - Previous: {a}");

        shortestPathList[3].Vertex.Should().Be(d);
        shortestPathList[3].Distance.Should().Be(8);
        shortestPathList[3].PreviousVertex.Should().Be(c);
        shortestPathList[3].ToString().Should()
            .Be($"Vertex: {d} - Distance: {8} - Previous: {c}");
			_logger.LogWarning("<<< Exiting DijkstraTest4_Success()");
    }

    [Test]
    public void DijkstraTest5_Success()
    {
		_logger.LogError(">>> Entering DijkstraTest5_Success()");
        // here test case is from https://www.youtube.com/watch?v=pVfj6mxhdMw

        var graph = new DirectedWeightedGraph<char>(7);
		_logger.LogError("DijkstraTest5_Success(): graph → {Graph}", graph);
        var a = graph.AddVertex('A');
		_logger.LogError("DijkstraTest5_Success(): a → {A}", a);
        var b = graph.AddVertex('B');
		_logger.LogError("DijkstraTest5_Success(): b → {B}", b);
        var c = graph.AddVertex('C');
		_logger.LogError("DijkstraTest5_Success(): c → {C}", c);
        var d = graph.AddVertex('D');
		_logger.LogError("DijkstraTest5_Success(): d → {D}", d);
        var e = graph.AddVertex('E');
		_logger.LogError("DijkstraTest5_Success(): e → {E}", e);
        var w = graph.AddVertex('W');
		_logger.LogError("DijkstraTest5_Success(): w → {W}", w);
        var z = graph.AddVertex('Z');
		_logger.LogError("DijkstraTest5_Success(): z → {Z}", z);

        graph.AddEdge(a, b, 6);
        graph.AddEdge(b, a, 6);

        graph.AddEdge(a, d, 1);
        graph.AddEdge(d, a, 1);

        graph.AddEdge(d, e, 1);
        graph.AddEdge(e, d, 1);

        graph.AddEdge(d, b, 2);
        graph.AddEdge(b, d, 2);

        graph.AddEdge(e, b, 2);
        graph.AddEdge(b, e, 2);

        graph.AddEdge(e, c, 5);
        graph.AddEdge(c, e, 5);

        graph.AddEdge(c, b, 5);
        graph.AddEdge(b, c, 5);

        graph.AddEdge(a, w, 50);
        graph.AddEdge(w, a, 50);

        graph.AddEdge(w, z, 1);
        graph.AddEdge(z, w, 1);

        var shortestPathList = DijkstraAlgorithm.GenerateShortestPath(graph, a);
		_logger.LogError("DijkstraTest5_Success(): shortestPathList → {ShortestPathList}", shortestPathList);
        shortestPathList.Length.Should().Be(7);

        shortestPathList[0].Vertex.Should().Be(a);
        shortestPathList[0].Distance.Should().Be(0);
        shortestPathList[0].PreviousVertex.Should().Be(a);
        shortestPathList[0].ToString().Should()
            .Be($"Vertex: {a} - Distance: {0} - Previous: {a}");

        shortestPathList[1].Vertex.Should().Be(b);
        shortestPathList[1].Distance.Should().Be(3);
        shortestPathList[1].PreviousVertex.Should().Be(d);
        shortestPathList[1].ToString().Should()
            .Be($"Vertex: {b} - Distance: {3} - Previous: {d}");

        shortestPathList[2].Vertex.Should().Be(c);
        shortestPathList[2].Distance.Should().Be(7);
        shortestPathList[2].PreviousVertex.Should().Be(e);
        shortestPathList[2].ToString().Should()
            .Be($"Vertex: {c} - Distance: {7} - Previous: {e}");

        shortestPathList[3].Vertex.Should().Be(d);
        shortestPathList[3].Distance.Should().Be(1);
        shortestPathList[3].PreviousVertex.Should().Be(a);
        shortestPathList[3].ToString().Should()
            .Be($"Vertex: {d} - Distance: {1} - Previous: {a}");

        shortestPathList[4].Vertex.Should().Be(e);
        shortestPathList[4].Distance.Should().Be(2);
        shortestPathList[4].PreviousVertex.Should().Be(d);
        shortestPathList[4].ToString().Should()
            .Be($"Vertex: {e} - Distance: {2} - Previous: {d}");

        shortestPathList[5].Vertex.Should().Be(w);
        shortestPathList[5].Distance.Should().Be(50);
        shortestPathList[5].PreviousVertex.Should().Be(a);
        shortestPathList[5].ToString().Should()
            .Be($"Vertex: {w} - Distance: {50} - Previous: {a}");

        shortestPathList[6].Vertex.Should().Be(z);
        shortestPathList[6].Distance.Should().Be(51);
        shortestPathList[6].PreviousVertex.Should().Be(w);
        shortestPathList[6].ToString().Should()
            .Be($"Vertex: {z} - Distance: {51} - Previous: {w}");
			_logger.LogWarning("<<< Exiting DijkstraTest5_Success()");
    }

    [Test]
    public void DijkstraTest6_Success()
    {
		_logger.LogError(">>> Entering DijkstraTest6_Success()");
        var graph = new DirectedWeightedGraph<char>(5);
        var a = graph.AddVertex('A');
		_logger.LogError("DijkstraTest6_Success(): a → {A}", a);
        var b = graph.AddVertex('B');
		_logger.LogError("DijkstraTest6_Success(): b → {B}", b);
        var c = graph.AddVertex('C');
		_logger.LogError("DijkstraTest6_Success(): c → {C}", c);
        var d = graph.AddVertex('D');
		_logger.LogError("DijkstraTest6_Success(): d → {D}", d);
		_logger.LogError("DijkstraTest6_Success(): graph → {Graph}", graph);

        graph.AddEdge(a, b, 1);
        graph.AddEdge(b, a, 1);

        graph.AddEdge(c, d, 5);
        graph.AddEdge(d, c, 5);

        var shortestPathList = DijkstraAlgorithm.GenerateShortestPath(graph, a);
		_logger.LogError("DijkstraTest6_Success(): shortestPathList → {ShortestPathList}", shortestPathList);

        shortestPathList.Length.Should().Be(4);
        shortestPathList[0].Vertex.Should().Be(a);
        shortestPathList[0].Distance.Should().Be(0);
        shortestPathList[0].PreviousVertex.Should().Be(a);
        shortestPathList[0].ToString().Should()
            .Be($"Vertex: {a} - Distance: {0} - Previous: {a}");

        shortestPathList[1].Vertex.Should().Be(b);
        shortestPathList[1].Distance.Should().Be(1);
        shortestPathList[1].PreviousVertex.Should().Be(a);
        shortestPathList[1].ToString().Should()
            .Be($"Vertex: {b} - Distance: {1} - Previous: {a}");

        shortestPathList[2].Vertex.Should().Be(c);
        shortestPathList[2].Distance.Should().Be(double.MaxValue);
        shortestPathList[2].PreviousVertex.Should().BeNull();
        shortestPathList[2].ToString().Should()
            .Be($"Vertex: {c} - Distance: {double.MaxValue} - Previous: {null}");

        shortestPathList[3].Vertex.Should().Be(d);
        shortestPathList[3].Distance.Should().Be(double.MaxValue);
        shortestPathList[3].PreviousVertex.Should().BeNull();
        shortestPathList[3].ToString().Should()
            .Be($"Vertex: {d} - Distance: {double.MaxValue} - Previous: {null}");
			_logger.LogWarning("<<< Exiting DijkstraTest6_Success()");
    }

    [Test]
    public void DijkstraMethodTest_ShouldThrow_GraphIsNull()
    {
		_logger.LogError(">>> Entering DijkstraMethodTest_ShouldThrow_GraphIsNull()");
        var graph = new DirectedWeightedGraph<char>(5);
        var a = graph.AddVertex('A');
		_logger.LogError("DijkstraMethodTest_ShouldThrow_GraphIsNull(): a → {A}", a);
		_logger.LogError("DijkstraMethodTest_ShouldThrow_GraphIsNull(): graph → {Graph}", graph);

        Func<DistanceModel<char>[]> action = () => DijkstraAlgorithm.GenerateShortestPath(null!, a);

        action.Should().Throw<ArgumentNullException>()
            .WithMessage($"Value cannot be null. (Parameter '{nameof(graph)}')");
			_logger.LogWarning("<<< Exiting DijkstraMethodTest_ShouldThrow_GraphIsNull()");
    }

    [Test]
    public void DijkstraMethodTest_ShouldThrow_VertexDoesntBelongToGraph()
    {
		_logger.LogError(">>> Entering DijkstraMethodTest_ShouldThrow_VertexDoesntBelongToGraph()");
        var graph = new DirectedWeightedGraph<char>(5);
        var startVertex = graph.AddVertex('A');
		_logger.LogError("DijkstraMethodTest_ShouldThrow_VertexDoesntBelongToGraph(): startVertex → {StartVertex}", startVertex);
		_logger.LogError("DijkstraMethodTest_ShouldThrow_VertexDoesntBelongToGraph(): graph → {Graph}", graph);

        Func<DistanceModel<char>[]> action = () => DijkstraAlgorithm.GenerateShortestPath(
            new DirectedWeightedGraph<char>(5), startVertex);

        action.Should().Throw<ArgumentNullException>()
            .WithMessage($"Value cannot be null. (Parameter '{nameof(graph)}')");
			_logger.LogWarning("<<< Exiting DijkstraMethodTest_ShouldThrow_VertexDoesntBelongToGraph()");
    }
}
