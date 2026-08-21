using DataStructures.Stack;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Stack;

public static class QueueBasedStackTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void PopWorksCorrectly()
    {
		_logger.LogDebug(">>> Entering PopWorksCorrectly()");
        //Arrange
        QueueBasedStack<char> s = new QueueBasedStack<char>();
		_logger.LogDebug("PopWorksCorrectly(): s → {S}", s);
        s.Push('A');
        s.Push('B');
        s.Push('C');
        var result = new StringBuilder();
		_logger.LogDebug("PopWorksCorrectly(): result → {Result}", result);

        //Act
        for (int i = 0; i < 3; i++)
        {
            result.Append(s.Pop());
        }


        //Assert
        Assert.That("CBA", Is.EqualTo(result.ToString()));
        Assert.That(s.IsEmpty(), Is.True, "Stack is Empty");
		_logger.LogDebug("<<< Exiting PopWorksCorrectly()");
    }
    [Test]
    public static void PeekWorksCorrectly()
    {
		_logger.LogDebug(">>> Entering PeekWorksCorrectly()");
        //Arrange
        QueueBasedStack<int> s = new QueueBasedStack<int>();
		_logger.LogDebug("PeekWorksCorrectly(): s → {S}", s);
        s.Push(1);
        s.Push(2);
        s.Push(3);
        var peeked = 0;
		_logger.LogDebug("PeekWorksCorrectly(): peeked → {Peeked}", peeked);

        //Act
        for (int i = 0; i < 3; i++)
        {
            peeked = s.Peek();
        }


        //Assert
        Assert.That(3, Is.EqualTo(peeked));
        Assert.That(s.IsEmpty(), Is.False, "Stack is Empty");
		_logger.LogDebug("<<< Exiting PeekWorksCorrectly()");
    }
    [Test]
    public static void PopEmptyStackThrowsInvalidOperationException()
    {
		_logger.LogDebug(">>> Entering PopEmptyStackThrowsInvalidOperationException()");
        //Arrange
        var s = new QueueBasedStack<int>();
		_logger.LogDebug("PopEmptyStackThrowsInvalidOperationException(): s → {S}", s);
        Exception? exception = null;

        //Act
        try
        {
            s.Pop();
        }
        catch (Exception ex)
        {
			_logger.LogError("Exception in PopEmptyStackThrowsInvalidOperationException(): {Arg0}", ex.getMessage());
            exception = ex;
			_logger.LogDebug("PopEmptyStackThrowsInvalidOperationException(): exception → {Exception}", exception);
        }

        //Assert
        Assert.That(exception?.GetType(), Is.EqualTo(typeof(InvalidOperationException)));
		_logger.LogDebug("<<< Exiting PopEmptyStackThrowsInvalidOperationException()");
    }
    [Test]
    public static void PeekEmptyStackThrowsInvalidOperationException()
    {
		_logger.LogDebug(">>> Entering PeekEmptyStackThrowsInvalidOperationException()");
        //Arrange
        var s = new QueueBasedStack<int>();
		_logger.LogDebug("PeekEmptyStackThrowsInvalidOperationException(): s → {S}", s);
        Exception? exception = null;

        //Act
        try
        {
            s.Peek();
        }
        catch (Exception ex)
        {
			_logger.LogError("Exception in PeekEmptyStackThrowsInvalidOperationException(): {Arg0}", ex.getMessage());
            exception = ex;
			_logger.LogDebug("PeekEmptyStackThrowsInvalidOperationException(): exception → {Exception}", exception);
        }

        //Assert
        Assert.That(exception?.GetType(), Is.EqualTo(typeof(InvalidOperationException)));
		_logger.LogDebug("<<< Exiting PeekEmptyStackThrowsInvalidOperationException()");
    }
    [Test]
    public static void ClearWorksCorrectly()
    {
		_logger.LogDebug(">>> Entering ClearWorksCorrectly()");
        // Arrange
        var s = new QueueBasedStack<int>();
		_logger.LogDebug("ClearWorksCorrectly(): s → {S}", s);
        s.Push(1);
        s.Push(2);

        // Act
        s.Clear();

        // Assert
		_logger.LogDebug("<<< Exiting ClearWorksCorrectly()");
        Assert.That(s.IsEmpty(), Is.True, "Queue is empty");

    }
    [Test]
    public static void LengthWorksCorrectly()
    {
		_logger.LogDebug(">>> Entering LengthWorksCorrectly()");
        // Arrange
        var s = new QueueBasedStack<int>();
		_logger.LogDebug("LengthWorksCorrectly(): s → {S}", s);
        s.Push(1);
        s.Push(2);
        var length = 0;
		_logger.LogDebug("LengthWorksCorrectly(): length → {Length}", length);

        // Act
        length = s.Length();

        // Assert
		_logger.LogDebug("<<< Exiting LengthWorksCorrectly()");
        Assert.That(2, Is.EqualTo(length));

    }
}
