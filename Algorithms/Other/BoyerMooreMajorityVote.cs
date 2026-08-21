using Microsoft.Extensions.Logging;
namespace Algorithms.Other;

/// <summary>
/// Boyer-Moore Majority Vote algorithm.
/// Finds element appearing more than n/2 times in O(n) time, O(1) space.
/// </summary>
public static class BoyerMooreMajorityVote
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// Finds the majority element.
    /// </summary>
    /// <param name="nums">Input array.</param>
    /// <returns>Majority element or null.</returns>
    public static int? FindMajority(int[] nums)
    {
		_logger.LogDebug(">>> Entering FindMajority(nums={Nums})", nums);
        if (nums == null || nums.Length == 0)
        {
            return null;
        }

        var candidate = FindCandidate(nums);
		_logger.LogDebug("FindMajority(nums={Nums}): candidate → {Candidate}", nums, candidate);
		_logger.LogDebug("<<< Exiting FindMajority(nums={Nums})", nums);
        return IsMajority(nums, candidate) ? candidate : null;
    }

    private static int FindCandidate(int[] nums)
    {
		_logger.LogDebug(">>> Entering FindCandidate(nums={Nums})", nums);
        int candidate = nums[0];
        int count = 1;
		_logger.LogDebug("FindCandidate(nums={Nums}): count → {Count}", nums, count);
		_logger.LogDebug("FindCandidate(nums={Nums}): candidate → {Candidate}", nums, candidate);

        for (int i = 1; i < nums.Length; i++)
        {
            if (count == 0)
            {
                candidate = nums[i];
            }

            count += nums[i] == candidate ? 1 : -1;
        }

		_logger.LogDebug("<<< Exiting FindCandidate(nums={Nums})", nums);
        return candidate;
    }

    private static bool IsMajority(int[] nums, int candidate) =>
        nums.Count(n => n == candidate) > nums.Length / 2;
}
