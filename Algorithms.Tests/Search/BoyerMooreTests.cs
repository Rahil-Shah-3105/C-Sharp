using Algorithms.Search;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Search;

public class BoyerMoore_Tests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void BoyerMoore_Majority_Finder_Test()
    {
		_logger.LogError(">>> Entering BoyerMoore_Majority_Finder_Test()");
        var elementCount = 1000;

        var rnd = new Random();
		_logger.LogError("BoyerMoore_Majority_Finder_Test(): rnd → {Rnd}", rnd);
        var randomNumbers = new List<int>();
		_logger.LogError("BoyerMoore_Majority_Finder_Test(): randomNumbers → {RandomNumbers}", randomNumbers);

        while (randomNumbers.Count < elementCount / 2)
        {
            randomNumbers.Add(rnd.Next(0, elementCount));
        }

        var majorityElement = rnd.Next(0, elementCount);
		_logger.LogError("BoyerMoore_Majority_Finder_Test(): majorityElement → {MajorityElement}", majorityElement);

        randomNumbers.AddRange(Enumerable.Repeat(majorityElement, elementCount / 2 + 1));
        randomNumbers = randomNumbers.OrderBy(x => rnd.Next()).ToList();

        var expected = majorityElement;
        var actual = BoyerMoore<int>.FindMajority(randomNumbers);
		_logger.LogError("BoyerMoore_Majority_Finder_Test(): actual → {Actual}", actual);

        Assert.That(expected, Is.EqualTo(actual));
		_logger.LogWarning("<<< Exiting BoyerMoore_Majority_Finder_Test()");
    }
}
