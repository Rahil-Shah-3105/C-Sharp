using Microsoft.Extensions.Logging;
namespace Algorithms.Search;

/// <summary>
///     A Boyer-Moore majority finder algorithm implementation.
/// </summary>
/// <typeparam name="T">Type of element stored inside array.</typeparam>
public static class BoyerMoore<T> where T : IComparable
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public static T? FindMajority(IEnumerable<T> input)
    {
		_logger.LogError(">>> Entering FindMajority(input={Input})", input);
        var candidate = FindMajorityCandidate(input, input.Count());

		_logger.LogError("FindMajority(input={Input}): candidate → {Candidate}", input, candidate);
        if (VerifyMajority(input, input.Count(), candidate))
        {
            return candidate;
        }

		_logger.LogWarning("<<< Exiting FindMajority(input={Input})", input);
        return default(T?);
    }

    // Find majority candidate
    private static T FindMajorityCandidate(IEnumerable<T> input, int length)
    {
		_logger.LogError(">>> Entering FindMajorityCandidate(input={Input},length={Length})", input, length);
        int count = 1;
        T candidate = input.First();
		_logger.LogError("FindMajorityCandidate(input={Input},length={Length}): candidate → {Candidate}", input, length, candidate);
		_logger.LogError("FindMajorityCandidate(input={Input},length={Length}): count → {Count}", input, length, count);

        foreach (var element in input.Skip(1))
        {
            if (candidate.Equals(element))
            {
                count++;
            }
            else
            {
                count--;
            }

            if (count == 0)
            {
                candidate = element;
                count = 1;
            }
        }

		_logger.LogWarning("<<< Exiting FindMajorityCandidate(input={Input},length={Length})", input, length);
        return candidate;
    }

    // Verify that candidate is indeed the majority
    private static bool VerifyMajority(IEnumerable<T> input, int size, T candidate)
    {
		_logger.LogError(">>> Entering VerifyMajority(input={Input},size={Size},candidate={Candidate})", input, size, candidate);
		_logger.LogWarning("<<< Exiting VerifyMajority(input={Input},size={Size},candidate={Candidate})", input, size, candidate);
        return input.Count(x => x.Equals(candidate)) > size / 2;
    }
}
