using DataStructures.SegmentTrees;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.SegmentTrees;

[TestFixture]
public class SegmentTreeApplyTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly SegmentTreeApply testTree = new([8, 9, 1, 4, 8, 7, 2]);

    [Test]
    public void Apply_Query_Update_Query_Test()
    {
		_logger.LogError(">>> Entering Apply_Query_Update_Query_Test()");
        Assert.That(testTree.Query(1, 4), Is.EqualTo(22));
        testTree.Apply(0, 3, 2);
        Assert.That(testTree.Operand, Is.EqualTo(new[] { 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }));
        Assert.That(testTree.Query(1, 4), Is.EqualTo(36));
		_logger.LogWarning("<<< Exiting Apply_Query_Update_Query_Test()");
    }
}
