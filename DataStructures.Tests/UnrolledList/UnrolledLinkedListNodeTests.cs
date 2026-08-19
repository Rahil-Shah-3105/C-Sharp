using DataStructures.UnrolledList;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.UnrolledList;

public class UnrolledLinkedListNodeTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void GetAndSet_SetItemNodeAndGetIt_ReturnExpectedItem()
    {
		_logger.LogDebug(">>> Entering GetAndSet_SetItemNodeAndGetIt_ReturnExpectedItem()");
        var node = new UnrolledLinkedListNode(6);
        node.Set(0, 1);
		_logger.LogDebug("GetAndSet_SetItemNodeAndGetIt_ReturnExpectedItem(): node → {Node}", node);

        var result = node.Get(0);
		_logger.LogDebug("GetAndSet_SetItemNodeAndGetIt_ReturnExpectedItem(): result → {Result}", result);

        result.Should().Be(1);
		_logger.LogDebug("<<< Exiting GetAndSet_SetItemNodeAndGetIt_ReturnExpectedItem()");
    }

    [Test]
    public void Get_GetLowIndex_ThrowArgumentException()
    {
		_logger.LogDebug(">>> Entering Get_GetLowIndex_ThrowArgumentException()");
        var node = new UnrolledLinkedListNode(6);

		_logger.LogDebug("Get_GetLowIndex_ThrowArgumentException(): node → {Node}", node);
        Action action = () => node.Get(-1);
		_logger.LogDebug("Get_GetLowIndex_ThrowArgumentException(): action → {Action}", action);

        action.Should().Throw<ArgumentException>();
		_logger.LogDebug("<<< Exiting Get_GetLowIndex_ThrowArgumentException()");
    }

    [Test]
    public void Get_GetHighIndex_ThrowArgumentException()
    {
		_logger.LogDebug(">>> Entering Get_GetHighIndex_ThrowArgumentException()");
        var node = new UnrolledLinkedListNode(6);

		_logger.LogDebug("Get_GetHighIndex_ThrowArgumentException(): node → {Node}", node);
        Action action = () => node.Get(7);
		_logger.LogDebug("Get_GetHighIndex_ThrowArgumentException(): action → {Action}", action);

        action.Should().Throw<ArgumentException>();
		_logger.LogDebug("<<< Exiting Get_GetHighIndex_ThrowArgumentException()");
    }

    [Test]
    public void Set_SetLowIndex_ThrowArgumentException()
    {
		_logger.LogDebug(">>> Entering Set_SetLowIndex_ThrowArgumentException()");
        var node = new UnrolledLinkedListNode(6);

		_logger.LogDebug("Set_SetLowIndex_ThrowArgumentException(): node → {Node}", node);
        Action action = () => node.Set(-1, 0);
		_logger.LogDebug("Set_SetLowIndex_ThrowArgumentException(): action → {Action}", action);

        action.Should().Throw<ArgumentException>();
		_logger.LogDebug("<<< Exiting Set_SetLowIndex_ThrowArgumentException()");
    }

    [Test]
    public void Set_SetHighIndex_ThrowArgumentException()
    {
		_logger.LogDebug(">>> Entering Set_SetHighIndex_ThrowArgumentException()");
        var node = new UnrolledLinkedListNode(6);

		_logger.LogDebug("Set_SetHighIndex_ThrowArgumentException(): node → {Node}", node);
        Action action = () => node.Set(7, 0);
		_logger.LogDebug("Set_SetHighIndex_ThrowArgumentException(): action → {Action}", action);

        action.Should().Throw<ArgumentException>();
		_logger.LogDebug("<<< Exiting Set_SetHighIndex_ThrowArgumentException()");
    }
}
