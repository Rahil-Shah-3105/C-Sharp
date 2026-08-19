using DataStructures.Queue;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Queue;

public static class StackBasedQueueTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void DequeueWorksCorrectly()
    {
		_logger.LogDebug(">>> Entering DequeueWorksCorrectly()");
        // Arrange
        var q = new StackBasedQueue<char>();
		_logger.LogDebug("DequeueWorksCorrectly(): q → {Q}", q);
        q.Enqueue('A');
        q.Enqueue('B');
        q.Enqueue('C');
        var result = new StringBuilder();
		_logger.LogDebug("DequeueWorksCorrectly(): result → {Result}", result);

        // Act
        for (var i = 0; i < 3; i++)
        {
            result.Append(q.Dequeue());
        }

        // Assert
        Assert.That("ABC", Is.EqualTo(result.ToString()));
        Assert.That(q.IsEmpty(), Is.True, "Queue is empty");
        Assert.That(q.IsFull(), Is.False, "Queue is full");
		_logger.LogDebug("<<< Exiting DequeueWorksCorrectly()");
    }

    [Test]
    public static void PeekWorksCorrectly()
    {
		_logger.LogDebug(">>> Entering PeekWorksCorrectly()");
        // Arrange
        var q = new StackBasedQueue<int>();
		_logger.LogDebug("PeekWorksCorrectly(): q → {Q}", q);
        q.Enqueue(1);
        q.Enqueue(2);
        var peeked = 0;
		_logger.LogDebug("PeekWorksCorrectly(): peeked → {Peeked}", peeked);

        // Act
        for (var i = 0; i < 3; i++)
        {
            peeked = q.Peek();
        }

        // Assert
        Assert.That(1, Is.EqualTo(peeked));
        Assert.That(q.IsEmpty(), Is.False, "Queue is empty");
        Assert.That(q.IsFull(), Is.False, "Queue is full");
		_logger.LogDebug("<<< Exiting PeekWorksCorrectly()");
    }

    [Test]
    public static void DequeueEmptyQueueThrowsInvalidOperationException()
    {
		_logger.LogDebug(">>> Entering DequeueEmptyQueueThrowsInvalidOperationException()");
        // Arrange
        var q = new StackBasedQueue<int>();
		_logger.LogDebug("DequeueEmptyQueueThrowsInvalidOperationException(): q → {Q}", q);
        Exception? exception = null;

        // Act
        try
        {
            q.Dequeue();
        }
        catch (Exception ex)
        {
			_logger.LogError("Exception in DequeueEmptyQueueThrowsInvalidOperationException(): {Arg0}", ex.getMessage());
            exception = ex;
			_logger.LogDebug("DequeueEmptyQueueThrowsInvalidOperationException(): exception → {Exception}", exception);
        }

        // Assert
        Assert.That(typeof(InvalidOperationException), Is.EqualTo(exception?.GetType()));
		_logger.LogDebug("<<< Exiting DequeueEmptyQueueThrowsInvalidOperationException()");
    }

    [Test]
    public static void PeekEmptyQueueThrowsInvalidOperationException()
    {
		_logger.LogDebug(">>> Entering PeekEmptyQueueThrowsInvalidOperationException()");
        // Arrange
        var q = new StackBasedQueue<int>();
		_logger.LogDebug("PeekEmptyQueueThrowsInvalidOperationException(): q → {Q}", q);
        Exception? exception = null;

        // Act
        try
        {
            q.Peek();
        }
        catch (Exception ex)
        {
			_logger.LogError("Exception in PeekEmptyQueueThrowsInvalidOperationException(): {Arg0}", ex.getMessage());
            exception = ex;
			_logger.LogDebug("PeekEmptyQueueThrowsInvalidOperationException(): exception → {Exception}", exception);
        }

        // Assert
        Assert.That(typeof(InvalidOperationException), Is.EqualTo(exception?.GetType()));
		_logger.LogDebug("<<< Exiting PeekEmptyQueueThrowsInvalidOperationException()");
    }

    [Test]
    public static void ClearWorksCorrectly()
    {
		_logger.LogDebug(">>> Entering ClearWorksCorrectly()");
        // Arrange
        var q = new StackBasedQueue<int>();
		_logger.LogDebug("ClearWorksCorrectly(): q → {Q}", q);
        q.Enqueue(1);
        q.Enqueue(2);

        // Act
        q.Clear();

        // Assert
        Assert.That(q.IsEmpty(), Is.True, "Queue is empty");
        Assert.That(q.IsFull(), Is.False, "Queue is full");
		_logger.LogDebug("<<< Exiting ClearWorksCorrectly()");
    }
}
