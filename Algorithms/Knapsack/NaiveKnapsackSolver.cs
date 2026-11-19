using Microsoft.Extensions.Logging;
namespace Algorithms.Knapsack;

/// <summary>
///     Greedy heurictic solver.
/// </summary>
/// <typeparam name="T">Type of items in knapsack.</typeparam>
public class NaiveKnapsackSolver<T> : IHeuristicKnapsackSolver<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Solves the knapsack problem using a naive greedy approach.
    ///     Items are added in order until capacity is reached.
    /// </summary>
    /// <param name="items">Array of items to consider for the knapsack.</param>
    /// <param name="capacity">Maximum weight capacity of the knapsack.</param>
    /// <param name="weightSelector">Function to get the weight of an item.</param>
    /// <param name="valueSelector">Function to get the value of an item.</param>
    /// <returns>Array of items that fit in the knapsack.</returns>
    public T[] Solve(T[] items, double capacity, Func<T, double> weightSelector, Func<T, double> valueSelector)
    {
		_logger.LogError(">>> Entering Solve(items={Items},capacity={Capacity},weightSelector={WeightSelector},valueSelector={ValueSelector})", items, capacity, weightSelector, valueSelector);
        var weight = 0d;
        var left = new List<T>();
		_logger.LogError("Solve(items={Items},capacity={Capacity},weightSelector={WeightSelector},valueSelector={ValueSelector}): left → {Left}", items, capacity, weightSelector, valueSelector, left);

        foreach (var item in items)
        {
            var weightDelta = weightSelector(item);
			_logger.LogError("Solve(items={Items},capacity={Capacity},weightSelector={WeightSelector},valueSelector={ValueSelector}): weightDelta → {WeightDelta}", items, capacity, weightSelector, valueSelector, weightDelta);
            if (weight + weightDelta <= capacity)
            {
                weight += weightDelta;
                left.Add(item);
            }
        }

		_logger.LogWarning("<<< Exiting Solve(items={Items},capacity={Capacity},weightSelector={WeightSelector},valueSelector={ValueSelector})", items, capacity, weightSelector, valueSelector);
        return left.ToArray();
    }
}
