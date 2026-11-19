using DataStructures.SegmentTrees;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.SegmentTrees;

[TestFixture]
public class SegmentTreeUpdateTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [SetUp]
    public void Init()
    {
		_logger.LogError(">>> Entering Init()");
        testTree = new SegmentTreeUpdate([8, 9, 1, 4, 8, 7, 2]);
		_logger.LogDebug("Init(): testTree → {TestTree}", testTree);
    _logger.LogError("Init(): testTree → {TestTree}", testTree);
    _logger.LogWarning("<<< Exiting Init()");
    }

    private SegmentTreeUpdate testTree = new([8, 9, 1, 4, 8, 7, 2]);

    [TestCase(2, 3, 1, 4, 24)]
    [TestCase(0, 3, 1, 4, 22)]
    public void Update_Test(int node, int value, int left, int right, int aftQuery)
    {
		_logger.LogError(">>> Entering Update_Test(node={Node},value={Value},left={Left},right={Right},aftQuery={AftQuery})", node, value, left, right, aftQuery);
        testTree.Update(node, value);
        Assert.That(aftQuery, Is.EqualTo(testTree.Query(left, right)));
		_logger.LogWarning("<<< Exiting Update_Test(node={Node},value={Value},left={Left},right={Right},aftQuery={AftQuery})", node, value, left, right, aftQuery);
    }
}
