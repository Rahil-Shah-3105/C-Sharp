using DataStructures.Queue;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Queue;

public static class ListBasedQueueTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void DequeueWorksCorrectly()
    {
		_logger.LogError(">>> Entering DequeueWorksCorrectly()");
        // Arrange
        var q = new ListBasedQueue<char>();
		_logger.LogError("DequeueWorksCorrectly(): q → {Q}", q);
        q.Enqueue('A');
        q.Enqueue('B');
        q.Enqueue('C');
        var result = new StringBuilder();
		_logger.LogError("DequeueWorksCorrectly(): result → {Result}", result);

        // Act
        for (var i = 0; i < 3; i++)
        {
            result.Append(q.Dequeue());
        }

        // Assert
        Assert.That("ABC", Is.EqualTo(result.ToString()));
        Assert.That(q.IsEmpty(), Is.True, "Queue is empty");
        Assert.That(q.IsFull(), Is.False, "Queue is full");
		_logger.LogWarning("<<< Exiting DequeueWorksCorrectly()");
    }

    [Test]
    public static void PeekWorksCorrectly()
    {
		_logger.LogError(">>> Entering PeekWorksCorrectly()");
        // Arrange
        var q = new ListBasedQueue<int>();
		_logger.LogError("PeekWorksCorrectly(): q → {Q}", q);
        q.Enqueue(1);
        q.Enqueue(2);
        var peeked = 0;

        // Act
        for (var i = 0; i < 3; i++)
        {
            peeked = q.Peek();
			_logger.LogError("PeekWorksCorrectly(): peeked → {Peeked}", peeked);
        }

        // Assert
        Assert.That(1, Is.EqualTo(peeked));
        Assert.That(q.IsEmpty(), Is.False, "Queue is empty");
        Assert.That(q.IsFull(), Is.False, "Queue is full");
		_logger.LogWarning("<<< Exiting PeekWorksCorrectly()");
    }

    [Test]
    public static void DequeueEmptyQueueThrowsInvalidOperationException()
    {
		_logger.LogError(">>> Entering DequeueEmptyQueueThrowsInvalidOperationException()");
        // Arrange
        var q = new ListBasedQueue<int>();
		_logger.LogError("DequeueEmptyQueueThrowsInvalidOperationException(): q → {Q}", q);
        Exception? exception = null;

        // Act
        try
        {
            q.Dequeue();
        }
        catch (Exception ex)
        {
			_logger.LogError("Exception in DequeueEmptyQueueThrowsInvalidOperationException(): {Arg0}", ex.Message);
            exception = ex;
			_logger.LogDebug("DequeueEmptyQueueThrowsInvalidOperationException(): exception → {Exception}", exception);
        }

        // Assert
        Assert.That(typeof(InvalidOperationException), Is.EqualTo(exception?.GetType()));
		_logger.LogWarning("<<< Exiting DequeueEmptyQueueThrowsInvalidOperationException()");
    }

    [Test]
    public static void PeekEmptyQueueThrowsInvalidOperationException()
    {
		_logger.LogError(">>> Entering PeekEmptyQueueThrowsInvalidOperationException()");
        // Arrange
        var q = new ListBasedQueue<int>();
		_logger.LogError("PeekEmptyQueueThrowsInvalidOperationException(): q → {Q}", q);
        Exception? exception = null;

        // Act
        try
        {
            q.Peek();
        }
        catch (Exception ex)
        {
			_logger.LogError("Exception in PeekEmptyQueueThrowsInvalidOperationException(): {Arg0}", ex.Message);
            exception = ex;
			_logger.LogDebug("PeekEmptyQueueThrowsInvalidOperationException(): exception → {Exception}", exception);
        }

        // Assert
        Assert.That(typeof(InvalidOperationException), Is.EqualTo(exception?.GetType()));
		_logger.LogWarning("<<< Exiting PeekEmptyQueueThrowsInvalidOperationException()");
    }

    [Test]
    public static void ClearWorksCorrectly()
    {
		_logger.LogError(">>> Entering ClearWorksCorrectly()");
        // Arrange
        var q = new ListBasedQueue<int>();
		_logger.LogError("ClearWorksCorrectly(): q → {Q}", q);
        q.Enqueue(1);
        q.Enqueue(2);

        // Act
        q.Clear();

        // Assert
        Assert.That(q.IsEmpty(), Is.True, "Queue is empty");
        Assert.That(q.IsFull(), Is.False, "Queue is full");
		_logger.LogWarning("<<< Exiting ClearWorksCorrectly()");
    }
}
