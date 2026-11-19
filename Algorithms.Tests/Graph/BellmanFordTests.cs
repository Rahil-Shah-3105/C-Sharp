using Algorithms.Graph;
using DataStructures.Graph;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Graph;

public class BellmanFordTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void CorrectDistancesTest()
    {
		_logger.LogError(">>> Entering CorrectDistancesTest()");
        var graph = new DirectedWeightedGraph<int>(10);

		_logger.LogError("CorrectDistancesTest(): graph → {Graph}", graph);
        var vertex1 = graph.AddVertex(1);
		_logger.LogError("CorrectDistancesTest(): vertex1 → {Vertex1}", vertex1);
        var vertex2 = graph.AddVertex(2);
		_logger.LogError("CorrectDistancesTest(): vertex2 → {Vertex2}", vertex2);
        var vertex3 = graph.AddVertex(3);
		_logger.LogError("CorrectDistancesTest(): vertex3 → {Vertex3}", vertex3);
        var vertex4 = graph.AddVertex(4);
		_logger.LogError("CorrectDistancesTest(): vertex4 → {Vertex4}", vertex4);
        var vertex5 = graph.AddVertex(5);
		_logger.LogError("CorrectDistancesTest(): vertex5 → {Vertex5}", vertex5);

        graph.AddEdge(vertex1, vertex2, 3);
        graph.AddEdge(vertex1, vertex5, -4);
        graph.AddEdge(vertex1, vertex3, 8);
        graph.AddEdge(vertex2, vertex5, 7);
        graph.AddEdge(vertex2, vertex4, 1);
        graph.AddEdge(vertex3, vertex2, 4);
        graph.AddEdge(vertex4, vertex3, -5);
        graph.AddEdge(vertex4, vertex1, 2);
        graph.AddEdge(vertex5, vertex4, 6);

        var expectedDistances = new Dictionary<Vertex<int>, double>
        {
            { vertex1, 0 },
            { vertex2, 1 },
            { vertex3, -3 },
            { vertex4, 2 },
            { vertex5, -4 }
        };

        var bellmanFord = new BellmanFord<int>(graph, [], []);
		_logger.LogError("CorrectDistancesTest(): bellmanFord → {BellmanFord}", bellmanFord);

        var calculatedDistances = bellmanFord.Run(vertex1);
		_logger.LogError("CorrectDistancesTest(): calculatedDistances → {CalculatedDistances}", calculatedDistances);

        foreach (var vertex in graph.Vertices)
        {
            if (vertex != null)
            {
                calculatedDistances[vertex].Should().BeApproximately(expectedDistances[vertex], 0.001);
            }
        }
		_logger.LogWarning("<<< Exiting CorrectDistancesTest()");
    }

    [Test]
    public void NegativeWeightCycleTest()
    {
		_logger.LogError(">>> Entering NegativeWeightCycleTest()");
        var graph = new DirectedWeightedGraph<int>(3);

		_logger.LogError("NegativeWeightCycleTest(): graph → {Graph}", graph);
        var vertex1 = graph.AddVertex(1);
		_logger.LogError("NegativeWeightCycleTest(): vertex1 → {Vertex1}", vertex1);
        var vertex2 = graph.AddVertex(2);
		_logger.LogError("NegativeWeightCycleTest(): vertex2 → {Vertex2}", vertex2);
        var vertex3 = graph.AddVertex(3);
		_logger.LogError("NegativeWeightCycleTest(): vertex3 → {Vertex3}", vertex3);

        graph.AddEdge(vertex1, vertex2, -1);
        graph.AddEdge(vertex2, vertex3, -2);
        graph.AddEdge(vertex3, vertex1, -3);

        var bellmanFord = new BellmanFord<int>(graph, [], []);
		_logger.LogError("NegativeWeightCycleTest(): bellmanFord → {BellmanFord}", bellmanFord);

        Action action = () => bellmanFord.Run(vertex1);
		_logger.LogError("NegativeWeightCycleTest(): action → {Action}", action);

        action.Should().Throw<InvalidOperationException>().WithMessage("Graph contains a negative weight cycle.");
		_logger.LogWarning("<<< Exiting NegativeWeightCycleTest()");
    }
}
