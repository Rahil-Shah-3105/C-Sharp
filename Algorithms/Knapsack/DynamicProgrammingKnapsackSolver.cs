using Microsoft.Extensions.Logging;
namespace Algorithms.Knapsack;

/// <summary>
///     Dynamic Programming Knapsack solver.
/// </summary>
/// <typeparam name="T">Type of items in knapsack.</typeparam>
public class DynamicProgrammingKnapsackSolver<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Returns the knapsack containing the items that
    ///     maximize value while not exceeding weight capacity.
    /// </summary>
    /// <param name="items">The list of items from which we select ones to be in the knapsack.</param>
    /// <param name="capacity">
    ///     The maximum weight capacity of the knapsack
    ///     to be filled. Only integer values of this capacity are tried. If
    ///     a greater resolution is needed, multiply the
    ///     weights/capacity by a factor of 10.
    /// </param>
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
		_logger.LogError(">>> Entering Solve(items={Items},capacity={Capacity},weightSelector={WeightSelector},valueSelector={ValueSelector})", items, capacity, weightSelector, valueSelector);
        var cache = Tabulate(items, weightSelector, valueSelector, capacity);
		_logger.LogError("Solve(items={Items},capacity={Capacity},weightSelector={WeightSelector},valueSelector={ValueSelector}): cache → {Cache}", items, capacity, weightSelector, valueSelector, cache);
		_logger.LogWarning("<<< Exiting Solve(items={Items},capacity={Capacity},weightSelector={WeightSelector},valueSelector={ValueSelector})", items, capacity, weightSelector, valueSelector);
        return GetOptimalItems(items, weightSelector, cache, capacity);
    }

    private static T[] GetOptimalItems(T[] items, Func<T, int> weightSelector, double[,] cache, int capacity)
    {
		_logger.LogError(">>> Entering GetOptimalItems(items={Items},weightSelector={WeightSelector},cache={Cache},capacity={Capacity})", items, weightSelector, cache, capacity);
        var currentCapacity = capacity;

        var result = new List<T>();
		_logger.LogError("GetOptimalItems(items={Items},weightSelector={WeightSelector},cache={Cache},capacity={Capacity}): result → {Result}", items, weightSelector, cache, capacity, result);
        for (var i = items.Length - 1; i >= 0; i--)
        {
            if (cache[i + 1, currentCapacity] > cache[i, currentCapacity])
            {
                var item = items[i];
                result.Add(item);
                currentCapacity -= weightSelector(item);
            }
        }

        result.Reverse(); // we added items back to front
		_logger.LogWarning("<<< Exiting GetOptimalItems(items={Items},weightSelector={WeightSelector},cache={Cache},capacity={Capacity})", items, weightSelector, cache, capacity);
        return result.ToArray();
    }

    private static double[,] Tabulate(
        T[] items,
        Func<T, int> weightSelector,
        Func<T, double> valueSelector,
        int maxCapacity)
    {
		_logger.LogError(">>> Entering Tabulate(items={Items},weightSelector={WeightSelector},valueSelector={ValueSelector},maxCapacity={MaxCapacity})", items, weightSelector, valueSelector, maxCapacity);
        // Store the incremental results in a bottom up manner
        var n = items.Length;
        var results = new double[n + 1, maxCapacity + 1];
		_logger.LogError("Tabulate(items={Items},weightSelector={WeightSelector},valueSelector={ValueSelector},maxCapacity={MaxCapacity}): results → {Results}", items, weightSelector, valueSelector, maxCapacity, results);
        for (var i = 0; i <= n; i++)
        {
            for (var w = 0; w <= maxCapacity; w++)
            {
                if (i == 0 || w == 0)
                {
                    // If we have no items to take, or
                    // if we have no capacity in our knapsack
                    // we cannot possibly have any value
                    results[i, w] = 0;
                }
                else if (weightSelector(items[i - 1]) <= w)
                {
                    // Decide if it is better to take or not take this item
                    var iut = items[i - 1]; // iut = Item under test
                    var vut = valueSelector(iut); // vut = Value of item under test
					_logger.LogError("Tabulate(items={Items},weightSelector={WeightSelector},valueSelector={ValueSelector},maxCapacity={MaxCapacity}): vut → {Vut}", items, weightSelector, valueSelector, maxCapacity, vut);
                    var wut = weightSelector(iut); // wut = Weight of item under test
					_logger.LogError("Tabulate(items={Items},weightSelector={WeightSelector},valueSelector={ValueSelector},maxCapacity={MaxCapacity}): wut → {Wut}", items, weightSelector, valueSelector, maxCapacity, wut);
                    var valueIfTaken = vut + results[i - 1, w - wut];
                    var valueIfNotTaken = results[i - 1, w];
                    results[i, w] = Math.Max(valueIfTaken, valueIfNotTaken);
                }
                else
                {
                    // There is not enough room to take this item
                    results[i, w] = results[i - 1, w];
                }
            }
        }

		_logger.LogWarning("<<< Exiting Tabulate(items={Items},weightSelector={WeightSelector},valueSelector={ValueSelector},maxCapacity={MaxCapacity})", items, weightSelector, valueSelector, maxCapacity);
        return results;
    }
}
