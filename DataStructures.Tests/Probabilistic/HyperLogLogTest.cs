using DataStructures.Probabilistic;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Probabilistic;

public class HyperLogLogTest
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void TestHyperLogLog()
    {
		_logger.LogError(">>> Entering TestHyperLogLog()");
        var hll = new HyperLogLog<int>();
        HashSet<int> actual = [];
		_logger.LogError("TestHyperLogLog(): hll → {Hll}", hll);

        var rand = new Random();
		_logger.LogError("TestHyperLogLog(): rand → {Rand}", rand);
        var tolerance = .05;
        for (var i = 0; i < 10000; i++)
        {
            var k = rand.Next(20000);
            hll.Add(k);
            actual.Add(k);
        }

        hll.Cardinality().Should()
            .BeGreaterOrEqualTo((int)(actual.Count * (1 - tolerance)))
            .And
            .BeLessOrEqualTo((int)(actual.Count * (1 + tolerance)));
			_logger.LogWarning("<<< Exiting TestHyperLogLog()");
    }

    [Test]
    public void TestHyperLogLogMerge()
    {
		_logger.LogError(">>> Entering TestHyperLogLogMerge()");
        var hll1 = new HyperLogLog<int>();
        var hll2 = new HyperLogLog<int>();
		_logger.LogError("TestHyperLogLogMerge(): hll2 → {Hll2}", hll2);
        var rand = new Random();
		_logger.LogError("TestHyperLogLogMerge(): rand → {Rand}", rand);
		_logger.LogError("TestHyperLogLogMerge(): hll1 → {Hll1}", hll1);
        var tolerance = .05;
        HashSet<int> actual = [];
        for (var i = 0; i < 5000; i++)
        {
            var k = rand.Next(20000);
            hll1.Add(k);
            actual.Add(k);
        }

        for (var i = 0; i < 5000; i++)
        {
            var k = rand.Next(20000);
            hll2.Add(k);
            actual.Add(k);
        }

		var __logStart = System.Diagnostics.Stopwatch.StartNew();
        var hll = HyperLogLog<int>.Merge(hll1, hll2);
		_logger.LogError("TestHyperLogLogMerge(): hll → {Hll}", hll);
        hll.Cardinality().Should()
            .BeGreaterOrEqualTo((int)(actual.Count * (1 - tolerance)))
            .And
            .BeLessOrEqualTo((int)(actual.Count * (1 + tolerance)));
			_logger.LogError("TestHyperLogLogMerge(): update query executed in {Arg0} ms", __logStart.ElapsedMilliseconds);
    _logger.LogWarning("<<< Exiting TestHyperLogLogMerge()");
    }
}
