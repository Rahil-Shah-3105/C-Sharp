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

    public static int GetItemIn(int[] arr) => arr[TestContext.CurrentContext.Random.Next(arr.Length)];

    public static int GetItemNotIn(int[] arr)
    {
		_logger.LogDebug(">>> Entering GetItemNotIn(arr={Arr})", arr);
        int item;
        do
        {
            item = TestContext.CurrentContext.Random.Next(arr.Min(), arr.Max() + 1);
        }
        while (arr.Contains(item));

		_logger.LogDebug("<<< Exiting GetItemNotIn(arr={Arr})", arr);
        return item;
    }

    public static int GetItemSmallerThanAllIn(int[] arr) => arr.Min() - 1;

    public static int GetItemBiggerThanAllIn(int[] arr) => arr.Max() + 1;
}
