using Utilities.Exceptions;
using Microsoft.Extensions.Logging;

namespace Algorithms.Search;

/// <summary>
///     Class that implements linear search algorithm.
/// </summary>
/// <typeparam name="T">Type of array element.</typeparam>
public class LinearSearcher<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Finds first item in array that satisfies specified term
    ///     Time complexity: O(n)
    ///     Space complexity: O(1).
    /// </summary>
    /// <param name="data">Array to search in.</param>
    /// <param name="term">Term to check against.</param>
    /// <returns>First item that satisfies term.</returns>
    public T Find(T[] data, Func<T, bool> term)
    {
		_logger.LogError(">>> Entering Find(data={Data},term={Term})", data, term);
        for (var i = 0; i < data.Length; i++)
        {
            if (term(data[i]))
            {
                return data[i];
            }
        }

		_logger.LogError("Throwing ItemNotFoundException from Find(data={Data},term={Term})", data, term);
		_logger.LogWarning("<<< Exiting Find(data={Data},term={Term})", data, term);
        throw new ItemNotFoundException();
    }

    /// <summary>
    ///     Finds index of first item in array that satisfies specified term
    ///     Time complexity: O(n)
    ///     Space complexity: O(1).
    /// </summary>
    /// <param name="data">Array to search in.</param>
    /// <param name="term">Term to check against.</param>
    /// <returns>Index of first item that satisfies term or -1 if none found.</returns>
    public int FindIndex(T[] data, Func<T, bool> term)
    {
		_logger.LogError(">>> Entering FindIndex(data={Data},term={Term})", data, term);
        for (var i = 0; i < data.Length; i++)
        {
            if (term(data[i]))
            {
                return i;
            }
        }

		_logger.LogWarning("<<< Exiting FindIndex(data={Data},term={Term})", data, term);
        return -1;
    }
}
