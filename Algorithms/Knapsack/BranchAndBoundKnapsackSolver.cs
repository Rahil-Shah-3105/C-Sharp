using Microsoft.Extensions.Logging;
namespace Algorithms.Knapsack;

/// <summary>
///     Branch and bound Knapsack solver.
/// </summary>
/// <typeparam name="T">Type of items in knapsack.</typeparam>
public class BranchAndBoundKnapsackSolver<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Returns the knapsack containing the items that maximize value while not exceeding weight capacity.
    ///     Construct a tree structure with total number of items + 1 levels, each node have two child nodes,
    ///     starting with a dummy item root, each following levels are associated with 1 items, construct the
    ///     tree in breadth first order to identify the optimal item set.
    /// </summary>
    /// <param name="items">All items to choose from.</param>
    /// <param name="capacity">The maximum weight capacity of the knapsack to be filled.</param>
    /// <param name="weightSelector">
    ///     A function that returns the value of the specified item
    ///     from the <paramref name="items">items</paramref> list.
    /// </param>
    /// <param name="valueSelector">
    ///     A function that returns the weight of the specified item
    ///     from the <paramref name="items">items</paramref> list.
    /// </param>
    /// <returns>
    ///     The array of items that provides the maximum value of the
    ///     knapsack without exceeding the specified weight <paramref name="capacity">capacity</paramref>.
    /// </returns>
    public T[] Solve(T[] items, int capacity, Func<T, int> weightSelector, Func<T, double> valueSelector)
    {
		_logger.LogDebug(">>> Entering Solve(items={Items},capacity={Capacity},weightSelector={WeightSelector},valueSelector={ValueSelector})", items, capacity, weightSelector, valueSelector);
        // This is required for greedy approach in upper bound calculation to work.
        items = items.OrderBy(i => valueSelector(i) / weightSelector(i)).ToArray();

        // nodesQueue --> used to construct tree in breadth first order
        Queue<BranchAndBoundNode> nodesQueue = new();
		_logger.LogDebug("Solve(items={Items},capacity={Capacity},weightSelector={WeightSelector},valueSelector={ValueSelector}): nodesQueue → {NodesQueue}", items, capacity, weightSelector, valueSelector, nodesQueue);

        // maxCumulativeValue --> maximum value while not exceeding weight capacity.
        var maxCumulativeValue = 0.0;
		_logger.LogDebug("Solve(items={Items},capacity={Capacity},weightSelector={WeightSelector},valueSelector={ValueSelector}): maxCumulativeValue → {MaxCumulativeValue}", items, capacity, weightSelector, valueSelector, maxCumulativeValue);

        // starting node, associated with a temporary created dummy item
        BranchAndBoundNode root = new(level: -1, taken: false);
		_logger.LogWarning("Solve(items={Items},capacity={Capacity},weightSelector={WeightSelector},valueSelector={ValueSelector}): root → {Root}", items, capacity, weightSelector, valueSelector, root);

        // lastNodeOfOptimalPat --> last item in the optimal item sets identified by this algorithm
        BranchAndBoundNode lastNodeOfOptimalPath = root;
		_logger.LogDebug("Solve(items={Items},capacity={Capacity},weightSelector={WeightSelector},valueSelector={ValueSelector}): lastNodeOfOptimalPath → {LastNodeOfOptimalPath}", items, capacity, weightSelector, valueSelector, lastNodeOfOptimalPath);

        nodesQueue.Enqueue(root);

        while (nodesQueue.Count != 0)
        {
            // parent --> parent node which represents the previous item, may or may not be taken into the knapsack
            BranchAndBoundNode parent = nodesQueue.Dequeue();
			_logger.LogDebug("Solve(items={Items},capacity={Capacity},weightSelector={WeightSelector},valueSelector={ValueSelector}): parent → {Parent}", items, capacity, weightSelector, valueSelector, parent);

            // IF it is the last level, branching cannot be performed
            if (parent.Level == items.Length - 1)
            {
                continue;
            }

            // create a child node where the associated item is taken into the knapsack
            var left = new BranchAndBoundNode(parent.Level + 1, true, parent);
			_logger.LogDebug("Solve(items={Items},capacity={Capacity},weightSelector={WeightSelector},valueSelector={ValueSelector}): left → {Left}", items, capacity, weightSelector, valueSelector, left);

            // create a child node where the associated item is not taken into the knapsack
            var right = new BranchAndBoundNode(parent.Level + 1, false, parent);
			_logger.LogDebug("Solve(items={Items},capacity={Capacity},weightSelector={WeightSelector},valueSelector={ValueSelector}): right → {Right}", items, capacity, weightSelector, valueSelector, right);

            // Since the associated item on current level is taken for the first node,
            // set the cumulative weight of first node to cumulative weight of parent node + weight of the associated item,
            // set the cumulative value of first node to cumulative value of parent node + value of current level's item.
            left.CumulativeWeight = parent.CumulativeWeight + weightSelector(items[left.Level]);
            left.CumulativeValue = parent.CumulativeValue + valueSelector(items[left.Level]);
            right.CumulativeWeight = parent.CumulativeWeight;
			_logger.LogDebug("Solve(items={Items},capacity={Capacity},weightSelector={WeightSelector},valueSelector={ValueSelector}): right.CumulativeWeight → {Arg4}", items, capacity, weightSelector, valueSelector, right.CumulativeWeight);
			_logger.LogDebug("Solve(items={Items},capacity={Capacity},weightSelector={WeightSelector},valueSelector={ValueSelector}): right.CumulativeValue → {Arg4}", items, capacity, weightSelector, valueSelector, right.CumulativeValue);
            right.CumulativeValue = parent.CumulativeValue;

            // IF cumulative weight is smaller than the weight capacity of the knapsack AND
            // current cumulative value is larger then the current maxCumulativeValue, update the maxCumulativeValue
            if (left.CumulativeWeight <= capacity && left.CumulativeValue > maxCumulativeValue)
            {
                maxCumulativeValue = left.CumulativeValue;
                lastNodeOfOptimalPath = left;
            }

            left.UpperBound = ComputeUpperBound(left, items, capacity, weightSelector, valueSelector);
            right.UpperBound = ComputeUpperBound(right, items, capacity, weightSelector, valueSelector);

            // IF upperBound of this node is larger than maxCumulativeValue,
            // the current path is still possible to reach or surpass the maximum value,
            // add current node to nodesQueue so that nodes below it can be further explored
            if (left.UpperBound > maxCumulativeValue && left.CumulativeWeight < capacity)
            {
                nodesQueue.Enqueue(left);
            }

            // Cumulative weight is the same as for parent node and < capacity
            if (right.UpperBound > maxCumulativeValue)
            {
                nodesQueue.Enqueue(right);
            }
        }

		_logger.LogDebug("<<< Exiting Solve(items={Items},capacity={Capacity},weightSelector={WeightSelector},valueSelector={ValueSelector})", items, capacity, weightSelector, valueSelector);
        return GetItemsFromPath(items, lastNodeOfOptimalPath);
    }

    // determine items taken based on the path
    private static T[] GetItemsFromPath(T[] items, BranchAndBoundNode lastNodeOfPath)
    {
		_logger.LogDebug(">>> Entering GetItemsFromPath(items={Items},lastNodeOfPath={LastNodeOfPath})", items, lastNodeOfPath);
        List<T> takenItems = [];

        // only bogus initial node has no parent
        for (var current = lastNodeOfPath; current.Parent is not null; current = current.Parent)
        {
            if (current.IsTaken)
            {
                takenItems.Add(items[current.Level]);
            }
        }

		_logger.LogDebug("<<< Exiting GetItemsFromPath(items={Items},lastNodeOfPath={LastNodeOfPath})", items, lastNodeOfPath);
        return takenItems.ToArray();
    }

    /// <summary>
    ///     Returns the upper bound value of a given node.
    /// </summary>
    /// <param name="aNode">The given node.</param>
    /// <param name="items">All items to choose from.</param>
    /// <param name="capacity">The maximum weight capacity of the knapsack to be filled.</param>
    /// <param name="weightSelector">
    ///     A function that returns the value of the specified item
    ///     from the <paramref name="items">items</paramref> list.
    /// </param>
    /// <param name="valueSelector">
    ///     A function that returns the weight of the specified item
    ///     from the <paramref name="items">items</paramref> list.
    /// </param>
    /// <returns>
    ///     upper bound value of the given <paramref name="aNode">node</paramref>.
    /// </returns>
    private static double ComputeUpperBound(BranchAndBoundNode aNode, T[] items, int capacity, Func<T, int> weightSelector, Func<T, double> valueSelector)
    {
		_logger.LogDebug(">>> Entering ComputeUpperBound(aNode={ANode},items={Items},capacity={Capacity},weightSelector={WeightSelector},valueSelector={ValueSelector})", aNode, items, capacity, weightSelector, valueSelector);
        var upperBound = aNode.CumulativeValue;
        var availableWeight = capacity - aNode.CumulativeWeight;
		_logger.LogDebug("ComputeUpperBound(aNode={ANode},items={Items},capacity={Capacity},weightSelector={WeightSelector},valueSelector={ValueSelector}): availableWeight → {AvailableWeight}", aNode, items, capacity, weightSelector, valueSelector, availableWeight);
        var nextLevel = aNode.Level + 1;

        while (availableWeight > 0 && nextLevel < items.Length)
        {
            if (weightSelector(items[nextLevel]) <= availableWeight)
            {
                upperBound += valueSelector(items[nextLevel]);
                availableWeight -= weightSelector(items[nextLevel]);
            }
            else
            {
                upperBound += valueSelector(items[nextLevel]) / weightSelector(items[nextLevel]) * availableWeight;
                availableWeight = 0;
            }

            nextLevel++;
        }

		_logger.LogDebug("<<< Exiting ComputeUpperBound(aNode={ANode},items={Items},capacity={Capacity},weightSelector={WeightSelector},valueSelector={ValueSelector})", aNode, items, capacity, weightSelector, valueSelector);
        return upperBound;
    }
}
