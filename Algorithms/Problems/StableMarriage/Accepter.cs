using Microsoft.Extensions.Logging;
namespace Algorithms.Problems.StableMarriage;

public class Accepter
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public Proposer? EngagedTo { get; set; }

    public List<Proposer> PreferenceOrder { get; set; } = [];

    public bool PrefersOverCurrent(Proposer newProposer) =>
        EngagedTo is null ||
        PreferenceOrder.IndexOf(newProposer) < PreferenceOrder.IndexOf(EngagedTo);
}
