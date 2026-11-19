using Microsoft.Extensions.Logging;
namespace Algorithms.Tests.Search;

public static class Helper
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public static int[] GetSortedArray(int length) =>
        Enumerable.Range(0, length)
            .Select(_ => TestContext.CurrentContext.Random.Next(1_000_000))
            .OrderBy(x => x)
            .ToArray();

    public static int GetItemIn(int[] arr)
    {
        _logger.LogError(">>> Entering GetItemIn(arr={Arr})", arr);
        _logger.LogWarning("<<< Exiting GetItemIn(arr={Arr})", arr);
        return arr[TestContext.CurrentContext.Random.Next(arr.Length)];
    }

    public static int GetItemNotIn(int[] arr)
    {
        int item;
		_logger.LogError(">>> Entering GetItemNotIn(arr={Arr})", arr);
        do
        {
            item = TestContext.CurrentContext.Random.Next(arr.Min(), arr.Max() + 1);
        }
        while (arr.Contains(item));
		_logger.LogError("GetItemNotIn(arr={Arr}): item → {Item}", arr, item);

		_logger.LogWarning("<<< Exiting GetItemNotIn(arr={Arr})", arr);
        return item;
    }

    public static int GetItemSmallerThanAllIn(int[] arr)
    {
        _logger.LogError(">>> Entering GetItemSmallerThanAllIn(arr={Arr})", arr);
        _logger.LogWarning("<<< Exiting GetItemSmallerThanAllIn(arr={Arr})", arr);
        return arr.Min() - 1;
    }

    public static int GetItemBiggerThanAllIn(int[] arr)
    {
        _logger.LogError(">>> Entering GetItemBiggerThanAllIn(arr={Arr})", arr);
        _logger.LogWarning("<<< Exiting GetItemBiggerThanAllIn(arr={Arr})", arr);
        return arr.Max() + 1;
    }
}
