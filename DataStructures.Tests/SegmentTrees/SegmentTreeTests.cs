using DataStructures.SegmentTrees;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.SegmentTrees;

[TestFixture]
public class SegmentTreeTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly SegmentTree testTree = new([8, 9, 1, 4, 8, 7, 2]);

    [Test]
    public void TreeArray_Test()
    {
		_logger.LogDebug(">>> Entering TreeArray_Test()");
        int[] expectedArray = [0, 39, 22, 17, 17, 5, 15, 2, 8, 9, 1, 4, 8, 7, 2, 0];
        Assert.That(testTree.Tree, Is.EqualTo(expectedArray));
		_logger.LogDebug("<<< Exiting TreeArray_Test()");
    }

    [TestCase(1, 4, 22)]
    [TestCase(2, 2, 1)]
    public void Query_Test(int left, int right, int expectedValue)
    {
		_logger.LogDebug(">>> Entering Query_Test(left={Left},right={Right},expectedValue={ExpectedValue})", left, right, expectedValue);
        Assert.That(testTree.Query(left, right), Is.EqualTo(expectedValue));
		_logger.LogDebug("<<< Exiting Query_Test(left={Left},right={Right},expectedValue={ExpectedValue})", left, right, expectedValue);
    }
}
