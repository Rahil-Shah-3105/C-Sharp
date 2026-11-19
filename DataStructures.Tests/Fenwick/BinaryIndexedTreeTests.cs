using DataStructures.Fenwick;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Fenwick;

[TestFixture]
internal class BinaryIndexedTreeTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void GetSum_CreateBITAndRequestSum_ReturnCorrect()
    {
		_logger.LogError(">>> Entering GetSum_CreateBITAndRequestSum_ReturnCorrect()");
        int[] array = [2, 1, 1, 3, 2, 3, 4, 5, 6, 7, 8, 9];
        var tree = new BinaryIndexedTree(array);
		_logger.LogError("GetSum_CreateBITAndRequestSum_ReturnCorrect(): tree → {Tree}", tree);
        var expectedSum = 12;

        var resultedSum = tree.GetSum(5);
		_logger.LogError("GetSum_CreateBITAndRequestSum_ReturnCorrect(): resultedSum → {ResultedSum}", resultedSum);

        resultedSum.Should().Be(expectedSum);
		_logger.LogWarning("<<< Exiting GetSum_CreateBITAndRequestSum_ReturnCorrect()");
    }

    [Test]
    public void UpdateTree_UpdateTreeAndRequestSum_GetSum()
    {
		_logger.LogError(">>> Entering UpdateTree_UpdateTreeAndRequestSum_GetSum()");
        int[] array = [2, 1, 1, 3, 2, 3, 4, 5, 6, 7, 8, 9];
        var tree = new BinaryIndexedTree(array);
		_logger.LogError("UpdateTree_UpdateTreeAndRequestSum_GetSum(): tree → {Tree}", tree);
        var expectedSum = 18;

        array[3] += 6;
        tree.UpdateTree(3, 6);

        var resultedSum = tree.GetSum(5);
		_logger.LogError("UpdateTree_UpdateTreeAndRequestSum_GetSum(): resultedSum → {ResultedSum}", resultedSum);
        resultedSum.Should().Be(expectedSum);
		_logger.LogWarning("<<< Exiting UpdateTree_UpdateTreeAndRequestSum_GetSum()");
    }
}
