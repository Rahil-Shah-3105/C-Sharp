using Microsoft.Extensions.Logging;
namespace Algorithms.Search.AStar;

/// <summary>
///     Contains the code for A* Pathfinding.
/// </summary>
public static class AStar
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Resets the Nodes in the list.
    /// </summary>
    /// <param name="nodes">Resets the nodes to be used again.</param>
    public static void ResetNodes(List<Node> nodes)
    {
		_logger.LogDebug(">>> Entering ResetNodes(nodes={Nodes})", nodes);
        foreach (var node in nodes)
        {
            node.CurrentCost = 0;
			_logger.LogDebug("ResetNodes(nodes={Nodes}): node.CurrentCost → {Arg1}", nodes, node.CurrentCost);
			_logger.LogDebug("ResetNodes(nodes={Nodes}): node.EstimatedCost → {Arg1}", nodes, node.EstimatedCost);
			_logger.LogDebug("ResetNodes(nodes={Nodes}): node.Parent → {Arg1}", nodes, node.Parent);
			_logger.LogDebug("ResetNodes(nodes={Nodes}): node.State → {Arg1}", nodes, node.State);
            node.EstimatedCost = 0;
            node.Parent = null;
            node.State = NodeState.Unconsidered;
        }
		_logger.LogDebug("<<< Exiting ResetNodes(nodes={Nodes})", nodes);
    }

    /// <summary>
    ///     Generates the Path from an (solved) node graph, before it gets reset.
    /// </summary>
    /// <param name="target">The node where we want to go.</param>
    /// <returns>The Path to the target node.</returns>
    public static List<Node> GeneratePath(Node target)
    {
		_logger.LogDebug(">>> Entering GeneratePath(target={Target})", target);
        var ret = new List<Node>();
        var current = target;
		_logger.LogDebug("GeneratePath(target={Target}): current → {Current}", target, current);
		_logger.LogDebug("GeneratePath(target={Target}): ret → {Ret}", target, ret);
        while (!(current is null))
        {
            ret.Add(current);
            current = current.Parent;
        }

        ret.Reverse();
		_logger.LogDebug("<<< Exiting GeneratePath(target={Target})", target);
        return ret;
    }

    /// <summary>
    ///     Computes the path from => to.
    /// </summary>
    /// <param name="from">Start node.</param>
    /// <param name="to">end node.</param>
    /// <returns>Path from start to end.</returns>
    public static List<Node> Compute(Node from, Node to)
    {
		_logger.LogDebug(">>> Entering Compute(from={From},to={To})", from, to);
        var done = new List<Node>();

		_logger.LogDebug("Compute(from={From},to={To}): done → {Done}", from, to, done);
        // A priority queue that will sort our nodes based on the total cost estimate
        var open = new PriorityQueue<Node>();
		_logger.LogDebug("Compute(from={From},to={To}): open → {Open}", from, to, open);
        foreach (var node in from.ConnectedNodes)
        {
            // Add connecting nodes if traversable
            if (node.Traversable)
            {
                // Calculate the Costs
                node.CurrentCost = from.CurrentCost + from.DistanceTo(node) * node.TraversalCostMultiplier;
                node.EstimatedCost = from.CurrentCost + node.DistanceTo(to);

                // Enqueue
                open.Enqueue(node);
            }
        }

        while (true)
        {
            // End Condition( Path not found )
            if (open.Count == 0)
            {
                ResetNodes(done);
                ResetNodes(open.GetData());
                return [];
            }

            // Selecting next Element from queue
            var current = open.Dequeue();
			_logger.LogDebug("Compute(from={From},to={To}): current → {Current}", from, to, current);

            // Add it to the done list
            done.Add(current);

            current.State = NodeState.Closed;
			_logger.LogDebug("Compute(from={From},to={To}): current.State → {Arg2}", from, to, current.State);

            // EndCondition( Path was found )
            if (current == to)
            {
                var ret = GeneratePath(to); // Create the Path
				_logger.LogDebug("Compute(from={From},to={To}): ret → {Ret}", from, to, ret);

                // Reset all Nodes that were used.
                ResetNodes(done);
                ResetNodes(open.GetData());
                return ret;
            }

            AddOrUpdateConnected(current, to, open);
        }
		_logger.LogDebug("<<< Exiting Compute(from={From},to={To})", from, to);
    }

    private static void AddOrUpdateConnected(Node current, Node to, PriorityQueue<Node> queue)
    {
		_logger.LogDebug(">>> Entering AddOrUpdateConnected(current={Current},to={To},queue={Queue})", current, to, queue);
        foreach (var connected in current.ConnectedNodes)
        {
            if (!connected.Traversable ||
                connected.State == NodeState.Closed)
            {
                continue; // Do ignore already checked and not traversable nodes.
            }

            // Adds a previously not "seen" node into the Queue
            if (connected.State == NodeState.Unconsidered)
            {
                connected.Parent = current;
				_logger.LogDebug("AddOrUpdateConnected(current={Current},to={To},queue={Queue}): connected.Parent → {Arg3}", current, to, queue, connected.Parent);
                connected.CurrentCost =
                    current.CurrentCost + current.DistanceTo(connected) * connected.TraversalCostMultiplier;
                connected.EstimatedCost = connected.CurrentCost + connected.DistanceTo(to);
                connected.State = NodeState.Open;
				_logger.LogDebug("AddOrUpdateConnected(current={Current},to={To},queue={Queue}): connected.State → {Arg3}", current, to, queue, connected.State);
                queue.Enqueue(connected);
            }
            else if (current != connected)
            {
                // Updating the cost of the node if the current way is cheaper than the previous
                var newCCost = current.CurrentCost + current.DistanceTo(connected);
				_logger.LogDebug("AddOrUpdateConnected(current={Current},to={To},queue={Queue}): newCCost → {NewCCost}", current, to, queue, newCCost);
                var newTCost = newCCost + current.EstimatedCost;
                if (newTCost < connected.TotalCost)
                {
                    connected.Parent = current;
                    connected.CurrentCost = newCCost;
					_logger.LogDebug("AddOrUpdateConnected(current={Current},to={To},queue={Queue}): connected.CurrentCost → {Arg3}", current, to, queue, connected.CurrentCost);
                }
            }
            else
            {
                // Codacy made me do it.
				_logger.LogWarning("Throwing PathfindingException from AddOrUpdateConnected(current={Current},to={To},queue={Queue})", current, to, queue);
                throw new PathfindingException(
                    "Detected the same node twice. Confusion how this could ever happen");
            }
        }
		_logger.LogDebug("<<< Exiting AddOrUpdateConnected(current={Current},to={To},queue={Queue})", current, to, queue);
    }
}
