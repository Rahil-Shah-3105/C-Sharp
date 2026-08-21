using Microsoft.Extensions.Logging;
namespace Algorithms.Problems.StableMarriage;

public static class GaleShapley
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Finds a stable matching between two equal sets of elements (fills EngagedTo properties).
    ///     time complexity: O(n^2), where n - array size.
    ///     Guarantees:
    ///     - Everyone is matched
    ///     - Matches are stable (there is no better accepter, for any given proposer, which would accept a new match).
    ///     Presented and proven by David Gale and Lloyd Shapley in 1962.
    /// </summary>
    public static void Match(Proposer[] proposers, Accepter[] accepters)
    {
		_logger.LogDebug(">>> Entering Match(proposers={Proposers},accepters={Accepters})", proposers, accepters);
        if (proposers.Length != accepters.Length)
        {
			_logger.LogWarning("Throwing ArgumentException from Match(proposers={Proposers},accepters={Accepters})", proposers, accepters);
            throw new ArgumentException("Collections must have equal count");
        }

        while (proposers.Any(m => !IsEngaged(m)))
        {
            DoSingleMatchingRound(proposers.Where(m => !IsEngaged(m)));
        }
		_logger.LogDebug("<<< Exiting Match(proposers={Proposers},accepters={Accepters})", proposers, accepters);
    }

    private static bool IsEngaged(Proposer proposer) => proposer.EngagedTo is not null;

    private static void DoSingleMatchingRound(IEnumerable<Proposer> proposers)
    {
		_logger.LogDebug(">>> Entering DoSingleMatchingRound(proposers={Proposers})", proposers);
        foreach (var newProposer in proposers)
        {
            var accepter = newProposer.PreferenceOrder.First!.Value;

            if (accepter.EngagedTo is null)
            {
                Engage(newProposer, accepter);
            }
            else
            {
                if (accepter.PrefersOverCurrent(newProposer))
                {
                    Free(accepter.EngagedTo);
                    Engage(newProposer, accepter);
                }
            }

            newProposer.PreferenceOrder.RemoveFirst();
        }
		_logger.LogDebug("<<< Exiting DoSingleMatchingRound(proposers={Proposers})", proposers);
    }

    private static void Free(Proposer proposer)
    {
		_logger.LogDebug(">>> Entering Free(proposer={Proposer})", proposer);
		_logger.LogDebug("Free(proposer={Proposer}): proposer.EngagedTo → {Arg1}", proposer, proposer.EngagedTo);
        proposer.EngagedTo = null;
    }

    private static void Engage(Proposer proposer, Accepter accepter)
    {
		_logger.LogDebug(">>> Entering Engage(proposer={Proposer},accepter={Accepter})", proposer, accepter);
		_logger.LogDebug("Engage(proposer={Proposer},accepter={Accepter}): accepter.EngagedTo → {Arg2}", proposer, accepter, accepter.EngagedTo);
		_logger.LogDebug("Engage(proposer={Proposer},accepter={Accepter}): proposer.EngagedTo → {Arg2}", proposer, accepter, proposer.EngagedTo);
        proposer.EngagedTo = accepter;
        accepter.EngagedTo = proposer;
    }
}
