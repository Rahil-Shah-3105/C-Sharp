
using System.Reflection;

using Algorithms.Search.AStar;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Search;

public static class AStarTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void ResetNodes_ResetsAllNodeProperties()
    {
		_logger.LogDebug(">>> Entering ResetNodes_ResetsAllNodeProperties()");
        var node = new Node(new VecN(0, 0), true, 1.0)
        {
            CurrentCost = 5,
            EstimatedCost = 10,
            Parent = new Node(new VecN(1, 1), true, 1.0),
            State = NodeState.Closed
        };
        var nodes = new List<Node> { node };

        AStar.ResetNodes(nodes);

        node.CurrentCost.Should().Be(0);
        node.EstimatedCost.Should().Be(0);
        node.Parent.Should().BeNull();
        node.State.Should().Be(NodeState.Unconsidered);
		_logger.LogDebug("<<< Exiting ResetNodes_ResetsAllNodeProperties()");
    }

    [Test]
    public static void GeneratePath_ReturnsPathFromTargetToRoot()
    {
		_logger.LogDebug(">>> Entering GeneratePath_ReturnsPathFromTargetToRoot()");
        var start = new Node(new VecN(0, 0), true, 1.0);
        var mid = new Node(new VecN(1, 0), true, 1.0) { Parent = start };
		_logger.LogDebug("GeneratePath_ReturnsPathFromTargetToRoot(): mid → {Mid}", mid);
        var end = new Node(new VecN(2, 0), true, 1.0) { Parent = mid };
		_logger.LogDebug("GeneratePath_ReturnsPathFromTargetToRoot(): end → {End}", end);

        var path = AStar.GeneratePath(end);
		_logger.LogDebug("GeneratePath_ReturnsPathFromTargetToRoot(): path → {Path}", path);

        path.Should().HaveCount(3);
        path[0].Should().BeSameAs(start);
        path[1].Should().BeSameAs(mid);
        path[2].Should().BeSameAs(end);
		_logger.LogDebug("<<< Exiting GeneratePath_ReturnsPathFromTargetToRoot()");
    }

    [Test]
    public static void Compute_ReturnsEmptyList_WhenNoPathExists()
    {
		_logger.LogDebug(">>> Entering Compute_ReturnsEmptyList_WhenNoPathExists()");
        var start = new Node(new VecN(0, 0), true, 1.0);
        var end = new Node(new VecN(1, 0), true, 1.0);
		_logger.LogDebug("Compute_ReturnsEmptyList_WhenNoPathExists(): end → {End}", end);
		_logger.LogDebug("Compute_ReturnsEmptyList_WhenNoPathExists(): start.ConnectedNodes → {Arg0}", start.ConnectedNodes);
		_logger.LogDebug("Compute_ReturnsEmptyList_WhenNoPathExists(): end.ConnectedNodes → {Arg0}", end.ConnectedNodes);
        start.ConnectedNodes = [];
        end.ConnectedNodes = [];

        var path = AStar.Compute(start, end);
		_logger.LogDebug("Compute_ReturnsEmptyList_WhenNoPathExists(): path → {Path}", path);

        path.Should().BeEmpty();
		_logger.LogDebug("<<< Exiting Compute_ReturnsEmptyList_WhenNoPathExists()");
    }

    [Test]
    public static void Compute_ReturnsPath_WhenPathExists()
    {
		_logger.LogDebug(">>> Entering Compute_ReturnsPath_WhenPathExists()");
        var start = new Node(new VecN(0, 0), true, 1.0);
        var mid = new Node(new VecN(1, 0), true, 1.0);
		_logger.LogDebug("Compute_ReturnsPath_WhenPathExists(): mid → {Mid}", mid);
        var end = new Node(new VecN(2, 0), true, 1.0);
		_logger.LogDebug("Compute_ReturnsPath_WhenPathExists(): end → {End}", end);

        start.ConnectedNodes = [mid];
		_logger.LogDebug("Compute_ReturnsPath_WhenPathExists(): start.ConnectedNodes → {Arg0}", start.ConnectedNodes);
		_logger.LogDebug("Compute_ReturnsPath_WhenPathExists(): mid.ConnectedNodes → {Arg0}", mid.ConnectedNodes);
		_logger.LogDebug("Compute_ReturnsPath_WhenPathExists(): end.ConnectedNodes → {Arg0}", end.ConnectedNodes);
        mid.ConnectedNodes = [end];
        end.ConnectedNodes = [];

        var path = AStar.Compute(start, end);
		_logger.LogDebug("Compute_ReturnsPath_WhenPathExists(): path → {Path}", path);

        path.Should().NotBeEmpty();
        path[0].Should().Be(start);
        path[^1].Should().Be(end);
		_logger.LogDebug("<<< Exiting Compute_ReturnsPath_WhenPathExists()");
    }

    [Test]
    public static void VecN_Equality_WorksAsExpected()
    {
		_logger.LogDebug(">>> Entering VecN_Equality_WorksAsExpected()");
        var a = new VecN(1, 2);
        var b = new VecN(1, 2);
		_logger.LogDebug("VecN_Equality_WorksAsExpected(): b → {B}", b);
        var c = new VecN(2, 1);
		_logger.LogDebug("VecN_Equality_WorksAsExpected(): c → {C}", c);
		_logger.LogDebug("VecN_Equality_WorksAsExpected(): a → {A}", a);

        a.Equals(b).Should().BeTrue();
        a.Equals(c).Should().BeFalse();
		_logger.LogDebug("<<< Exiting VecN_Equality_WorksAsExpected()");
    }

    [Test]
    public static void AddOrUpdateConnected_ThrowsPathfindingException_OnSelfReference()
    {
		_logger.LogDebug(">>> Entering AddOrUpdateConnected_ThrowsPathfindingException_OnSelfReference()");
        var node = new Node(new VecN(0, 0), true, 1.0);
        node.ConnectedNodes = [node];
		_logger.LogDebug("AddOrUpdateConnected_ThrowsPathfindingException_OnSelfReference(): node.ConnectedNodes → {Arg0}", node.ConnectedNodes);
		_logger.LogDebug("AddOrUpdateConnected_ThrowsPathfindingException_OnSelfReference(): node.State → {Arg0}", node.State);
		_logger.LogDebug("AddOrUpdateConnected_ThrowsPathfindingException_OnSelfReference(): node → {Node}", node);
        node.State = NodeState.Open;

        var queue = new PriorityQueue<Node>();
		_logger.LogDebug("AddOrUpdateConnected_ThrowsPathfindingException_OnSelfReference(): queue → {Queue}", queue);

        Action act = () => {
            // Directly call the private method using reflection, otherwise we can't test this case
            var method = typeof(AStar).GetMethod("AddOrUpdateConnected", BindingFlags.NonPublic | BindingFlags.Static);
			_logger.LogDebug("AddOrUpdateConnected_ThrowsPathfindingException_OnSelfReference(): method → {Method}", method);
            method!.Invoke(null, [node, node, queue]);
        };

        act.Should().Throw<TargetInvocationException>()
            .WithInnerException<PathfindingException>()
            .WithMessage("*same node twice*");
			_logger.LogDebug("<<< Exiting AddOrUpdateConnected_ThrowsPathfindingException_OnSelfReference()");
    }
}
