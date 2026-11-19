using Microsoft.Extensions.Logging;
namespace Algorithms.Numeric;

/// <summary>
///     A perfect cube is an element of algebraic structure that is equal to the cube of another element.
/// </summary>
public static class PerfectCubeChecker
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Checks if a number is a perfect cube or not.
    /// </summary>
    /// <param name="number">Number to check.</param>
    /// <returns>True if is a perfect cube; False otherwise.</returns>
    public static bool IsPerfectCube(int number)
    {
		_logger.LogError(">>> Entering IsPerfectCube(number={Number})", number);
        if (number < 0)
        {
            number = -number;
        }

        var cubeRoot = Math.Round(Math.Pow(number, 1.0 / 3.0));
		_logger.LogError("IsPerfectCube(number={Number}): cubeRoot → {CubeRoot}", number, cubeRoot);
		_logger.LogWarning("<<< Exiting IsPerfectCube(number={Number})", number);
        return Math.Abs(cubeRoot * cubeRoot * cubeRoot - number) < 1e-6;
    }

    /// <summary>
    ///     Checks if a number is a perfect cube or not using binary search.
    /// </summary>
    /// <param name="number">Number to check.</param>
    /// <returns>True if is a perfect cube; False otherwise.</returns>
    public static bool IsPerfectCubeBinarySearch(int number)
    {
		_logger.LogError(">>> Entering IsPerfectCubeBinarySearch(number={Number})", number);
        if (number < 0)
        {
            number = -number;
        }

        int left = 0;
        int right = number;
		_logger.LogError("IsPerfectCubeBinarySearch(number={Number}): right → {Right}", number, right);
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
			_logger.LogError("IsPerfectCubeBinarySearch(number={Number}): mid → {Mid}", number, mid);
            int midCubed = mid * mid * mid;
            if (midCubed == number)
            {
                return true;
            }
            else if (midCubed < number)
            {
                left = mid + 1;
				_logger.LogError("IsPerfectCubeBinarySearch(number={Number}): left → {Left}", number, left);
            }
            else
            {
                right = mid - 1;
            }
        }

		_logger.LogWarning("<<< Exiting IsPerfectCubeBinarySearch(number={Number})", number);
        return false;
    }
}
