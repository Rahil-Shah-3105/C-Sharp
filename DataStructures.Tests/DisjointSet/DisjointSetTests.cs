using DataStructures.DisjointSet;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.DisjointSet;

[TestFixture]
public class DisjointSetTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void MakeSetDataInitializationTest()
    {
		_logger.LogError(">>> Entering MakeSetDataInitializationTest()");
        DisjointSet<int> ds = new();
        var one = ds.MakeSet(1);
		_logger.LogError("MakeSetDataInitializationTest(): one → {One}", one);
        var two = ds.MakeSet(2);
		_logger.LogError("MakeSetDataInitializationTest(): two → {Two}", two);
		_logger.LogError("MakeSetDataInitializationTest(): ds → {Ds}", ds);
        one.Data.Should().Be(1);
        two.Data.Should().Be(2);
		_logger.LogWarning("<<< Exiting MakeSetDataInitializationTest()");
    }
    [Test]
    public static void UnionTest()
    {
		_logger.LogError(">>> Entering UnionTest()");
        DisjointSet<int> ds = new();
        var one = ds.MakeSet(1);
		_logger.LogError("UnionTest(): one → {One}", one);
        var two = ds.MakeSet(2);
		_logger.LogError("UnionTest(): two → {Two}", two);
        var three = ds.MakeSet(3);
		_logger.LogError("UnionTest(): three → {Three}", three);
		_logger.LogError("UnionTest(): ds → {Ds}", ds);
        ds.UnionSet(one, two);
        ds.FindSet(one).Should().Be(ds.FindSet(two));
        ds.UnionSet(one, three);
        ds.FindSet(two).Should().Be(ds.FindSet(three));
        (one.Rank + two.Rank + three.Rank).Should().Be(1);
		_logger.LogWarning("<<< Exiting UnionTest()");
    }
}
