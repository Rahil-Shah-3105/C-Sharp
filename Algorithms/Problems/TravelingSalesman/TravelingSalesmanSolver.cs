using Microsoft.Extensions.Logging;
namespace Algorithms.Problems.TravelingSalesman;

/// <summary>
/// Provides methods to solve the Traveling Salesman Problem (TSP) using brute-force and nearest neighbor heuristics.
/// The TSP is a classic optimization problem in which a salesman must visit each city exactly once and return to the starting city, minimizing the total travel distance.
/// </summary>
public static class TravelingSalesmanSolver
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// Solves the TSP using brute-force search. This method checks all possible permutations of cities to find the shortest possible route.
    /// WARNING: This approach is only feasible for small numbers of cities due to factorial time complexity.
    /// </summary>
    /// <param name="distanceMatrix">A square matrix where element [i, j] represents the distance from city i to city j.</param>
    /// <returns>A tuple containing the minimal route (as an array of city indices) and the minimal total distance.</returns>
    public static (int[] Route, double Distance) SolveBruteForce(double[,] distanceMatrix)
    {
		_logger.LogDebug(">>> Entering SolveBruteForce(Route={Route},Distance={Distance})", Route, Distance);
        int n = distanceMatrix.GetLength(0);
        if (n != distanceMatrix.GetLength(1))
        {
			_logger.LogWarning("Throwing ArgumentException from SolveBruteForce(Route={Route},Distance={Distance})", Route, Distance);
            throw new ArgumentException("Distance matrix must be square.");
        }

        if (n < 2)
        {
            throw new ArgumentException("At least two cities are required.");
        }

        var cities = Enumerable.Range(0, n).ToArray();
		_logger.LogDebug("SolveBruteForce(Route={Route},Distance={Distance}): cities → {Cities}", Route, Distance, cities);
        double minDistance = double.MaxValue;
		_logger.LogDebug("SolveBruteForce(Route={Route},Distance={Distance}): minDistance → {MinDistance}", Route, Distance, minDistance);
        int[]? bestRoute = null;

        foreach (var perm in Permute(cities.Skip(1).ToArray()))
        {
            var route = new int[n + 1];
            route[0] = 0;
            for (int i = 0; i < perm.Length; i++)
            {
                route[i + 1] = perm[i];
            }

            // Ensure route ends at city 0
            route[n] = 0;

            double dist = 0;
            for (int i = 0; i < n; i++)
            {
                dist += distanceMatrix[route[i], route[i + 1]];
            }

            if (dist < minDistance)
            {
                minDistance = dist;
                bestRoute = (int[])route.Clone();
            }
			_logger.LogDebug("SolveBruteForce(Route={Route},Distance={Distance}): bestRoute → {BestRoute}", Route, Distance, bestRoute);
			_logger.LogDebug(">>> Entering Permute(arr={Arr})", arr);
			_logger.LogDebug("<<< Exiting Permute(arr={Arr})", arr);
        }

		_logger.LogDebug("<<< Exiting SolveBruteForce(Route={Route},Distance={Distance})", Route, Distance);
        return (bestRoute ?? Array.Empty<int>(), minDistance);
    }

    /// <summary>
    /// Solves the TSP using the nearest neighbor heuristic. This method builds a route by always visiting the nearest unvisited city next.
    /// This approach is much faster but may not find the optimal solution.
    /// </summary>
    /// <param name="distanceMatrix">A square matrix where element [i, j] represents the distance from city i to city j.</param>
    /// <param name="start">The starting city index.</param>
    /// <returns>A tuple containing the route (as an array of city indices) and the total distance.</returns>
    public static (int[] Route, double Distance) SolveNearestNeighbor(double[,] distanceMatrix, int start = 0)
    {
		_logger.LogDebug(">>> Entering SolveNearestNeighbor(Route={Route},Distance={Distance})", Route, Distance);
        int n = distanceMatrix.GetLength(0);
        if (n != distanceMatrix.GetLength(1))
        {
			_logger.LogWarning("Throwing ArgumentException from SolveNearestNeighbor(Route={Route},Distance={Distance})", Route, Distance);
            throw new ArgumentException("Distance matrix must be square.");
        }

        if (start < 0 || start >= n)
        {
			_logger.LogWarning("Throwing ArgumentOutOfRangeException from SolveNearestNeighbor(Route={Route},Distance={Distance})", Route, Distance);
            throw new ArgumentOutOfRangeException(nameof(start));
        }

        var visited = new bool[n];
        List<int> route = [start];
        visited[start] = true;
        double totalDistance = 0;
        int current = start;
		_logger.LogDebug("SolveNearestNeighbor(Route={Route},Distance={Distance}): current → {Current}", Route, Distance, current);
        for (int step = 1; step < n; step++)
        {
            double minDist = double.MaxValue;
			_logger.LogDebug("SolveNearestNeighbor(Route={Route},Distance={Distance}): minDist → {MinDist}", Route, Distance, minDist);
            int next = -1;
			_logger.LogDebug("SolveNearestNeighbor(Route={Route},Distance={Distance}): next → {Next}", Route, Distance, next);
            for (int j = 0; j < n; j++)
            {
                if (!visited[j] && distanceMatrix[current, j] < minDist)
                {
                    minDist = distanceMatrix[current, j];
                    next = j;
                }
            }

            if (next == -1)
            {
				_logger.LogWarning("Throwing InvalidOperationException from SolveNearestNeighbor(Route={Route},Distance={Distance})", Route, Distance);
                throw new InvalidOperationException("No unvisited cities remain.");
            }

            route.Add(next);
            visited[next] = true;
            totalDistance += minDist;
            current = next;
        }

        totalDistance += distanceMatrix[current, start];
        route.Add(start);
		_logger.LogDebug("<<< Exiting SolveNearestNeighbor(Route={Route},Distance={Distance})", Route, Distance);
        return (route.ToArray(), totalDistance);
    }

    /// <summary>
    /// Generates all permutations of the input array.
    /// Used for brute-force TSP solution.
    /// </summary>
    private static IEnumerable<int[]> Permute(int[] arr)
    {
		_logger.LogDebug(">>> Entering Permute(arr={Arr})", arr);
        if (arr.Length == 1)
        {
            yield return arr;
            yield break;
        }

        for (int i = 0; i < arr.Length; i++)
        {
            var rest = arr.Where((_, idx) => idx != i).ToArray();
			_logger.LogDebug("Permute(arr={Arr}): rest → {Rest}", arr, rest);
            foreach (var perm in Permute(rest))
            {
                yield return [arr[i], ..perm];
            }
        }
		_logger.LogDebug("<<< Exiting Permute(arr={Arr})", arr);
    }
}
