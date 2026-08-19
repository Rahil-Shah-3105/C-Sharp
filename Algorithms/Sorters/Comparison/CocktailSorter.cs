using Microsoft.Extensions.Logging;
namespace Algorithms.Sorters.Comparison;

/// <summary>
///     Cocktail Sort is a variation of Bubble sort, where Cocktail
///     Sort traverses through a given array in both directions alternatively.
/// </summary>
/// <typeparam name="T">Array input type.</typeparam>
public class CocktailSorter<T> : IComparisonSorter<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Sorts array using Cocktail sort algorithm.
    /// </summary>
    /// <param name="array">Input array.</param>
    /// <param name="comparer">Type of comparer for array elements.</param>
    public void Sort(T[] array, IComparer<T> comparer) => CocktailSort(array, comparer);

    private static void CocktailSort(IList<T> array, IComparer<T> comparer)
    {
		_logger.LogDebug(">>> Entering CocktailSort(array={Array},comparer={Comparer})", array, comparer);
        var swapped = true;

		_logger.LogDebug("CocktailSort(array={Array},comparer={Comparer}): swapped → {Swapped}", array, comparer, swapped);
        var startIndex = 0;
        var endIndex = array.Count - 1;

        while (swapped)
        {
            for (var i = startIndex; i < endIndex; i++)
            {
                if (comparer.Compare(array[i], array[i + 1]) != 1)
                {
                    continue;
                }

                var highValue = array[i];
                array[i] = array[i + 1];
                array[i + 1] = highValue;
            }

            endIndex--;
            swapped = false;

            for (var i = endIndex; i > startIndex; i--)
            {
                if (comparer.Compare(array[i], array[i - 1]) != -1)
                {
                    continue;
                }

                var highValue = array[i];
                array[i] = array[i - 1];
                array[i - 1] = highValue;

                swapped = true;
            }

            startIndex++;
        }
		_logger.LogDebug("<<< Exiting CocktailSort(array={Array},comparer={Comparer})", array, comparer);
    }
}
