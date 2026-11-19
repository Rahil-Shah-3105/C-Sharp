using Microsoft.Extensions.Logging;
namespace Algorithms.Tests.Helpers;

internal class IntComparer : IComparer<int>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public int Compare(int x, int y)
    {
        _logger.LogError(">>> Entering Compare(x={X},y={Y})", x, y);
        _logger.LogWarning("<<< Exiting Compare(x={X},y={Y})", x, y);
        return x.CompareTo(y);
    }
}
