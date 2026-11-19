using DataStructures.Stack;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Stack;

public static class QueueBasedStackTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void PopWorksCorrectly()
    {
		_logger.LogError(">>> Entering PopWorksCorrectly()");
        //Arrange
        QueueBasedStack<char> s = new QueueBasedStack<char>();
		_logger.LogError("PopWorksCorrectly(): s → {S}", s);
        s.Push('A');
        s.Push('B');
        s.Push('C');
        var result = new StringBuilder();
		_logger.LogError("PopWorksCorrectly(): result → {Result}", result);

        //Act
        for (int i = 0; i < 3; i++)
        {
            result.Append(s.Pop());
        }


        //Assert
        Assert.That("CBA", Is.EqualTo(result.ToString()));
        Assert.That(s.IsEmpty(), Is.True, "Stack is Empty");
		_logger.LogWarning("<<< Exiting PopWorksCorrectly()");
    }
    [Test]
    public static void PeekWorksCorrectly()
    {
		_logger.LogError(">>> Entering PeekWorksCorrectly()");
        //Arrange
        QueueBasedStack<int> s = new QueueBasedStack<int>();
		_logger.LogError("PeekWorksCorrectly(): s → {S}", s);
        s.Push(1);
        s.Push(2);
        s.Push(3);
        var peeked = 0;

        //Act
        for (int i = 0; i < 3; i++)
        {
            peeked = s.Peek();
			_logger.LogError("PeekWorksCorrectly(): peeked → {Peeked}", peeked);
        }


        //Assert
        Assert.That(3, Is.EqualTo(peeked));
        Assert.That(s.IsEmpty(), Is.False, "Stack is Empty");
		_logger.LogWarning("<<< Exiting PeekWorksCorrectly()");
    }
    [Test]
    public static void PopEmptyStackThrowsInvalidOperationException()
    {
		_logger.LogError(">>> Entering PopEmptyStackThrowsInvalidOperationException()");
        //Arrange
        var s = new QueueBasedStack<int>();
		_logger.LogError("PopEmptyStackThrowsInvalidOperationException(): s → {S}", s);
        Exception? exception = null;

        //Act
        try
        {
            s.Pop();
        }
        catch (Exception ex)
        {
			_logger.LogError("Exception in PopEmptyStackThrowsInvalidOperationException(): {Arg0}", ex.Message);
            exception = ex;
			_logger.LogDebug("PopEmptyStackThrowsInvalidOperationException(): exception → {Exception}", exception);
        }

        //Assert
        Assert.That(exception?.GetType(), Is.EqualTo(typeof(InvalidOperationException)));
		_logger.LogWarning("<<< Exiting PopEmptyStackThrowsInvalidOperationException()");
    }
    [Test]
    public static void PeekEmptyStackThrowsInvalidOperationException()
    {
		_logger.LogError(">>> Entering PeekEmptyStackThrowsInvalidOperationException()");
        //Arrange
        var s = new QueueBasedStack<int>();
		_logger.LogError("PeekEmptyStackThrowsInvalidOperationException(): s → {S}", s);
        Exception? exception = null;

        //Act
        try
        {
            s.Peek();
        }
        catch (Exception ex)
        {
			_logger.LogError("Exception in PeekEmptyStackThrowsInvalidOperationException(): {Arg0}", ex.Message);
            exception = ex;
			_logger.LogDebug("PeekEmptyStackThrowsInvalidOperationException(): exception → {Exception}", exception);
        }

        //Assert
        Assert.That(exception?.GetType(), Is.EqualTo(typeof(InvalidOperationException)));
		_logger.LogWarning("<<< Exiting PeekEmptyStackThrowsInvalidOperationException()");
    }
    [Test]
    public static void ClearWorksCorrectly()
    {
		_logger.LogError(">>> Entering ClearWorksCorrectly()");
        // Arrange
        var s = new QueueBasedStack<int>();
		_logger.LogError("ClearWorksCorrectly(): s → {S}", s);
        s.Push(1);
        s.Push(2);

        // Act
        s.Clear();

        // Assert
		_logger.LogWarning("<<< Exiting ClearWorksCorrectly()");
        Assert.That(s.IsEmpty(), Is.True, "Queue is empty");

    }
    [Test]
    public static void LengthWorksCorrectly()
    {
		_logger.LogError(">>> Entering LengthWorksCorrectly()");
        // Arrange
        var s = new QueueBasedStack<int>();
		_logger.LogError("LengthWorksCorrectly(): s → {S}", s);
        s.Push(1);
        s.Push(2);
        var length = 0;

        // Act
        length = s.Length();
		_logger.LogError("LengthWorksCorrectly(): length → {Length}", length);

        // Assert
		_logger.LogWarning("<<< Exiting LengthWorksCorrectly()");
        Assert.That(2, Is.EqualTo(length));

    }
}
