using DataStructures.Stack;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Stack;

public static class ArrayBasedStackTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private const string StackEmptyErrorMessage = "Stack is empty";

    [Test]
    public static void CountTest()
    {
		_logger.LogDebug(">>> Entering CountTest()");
        var stack = new ArrayBasedStack<int>([0, 1, 2, 3, 4]);
		_logger.LogDebug("<<< Exiting CountTest()");
		_logger.LogDebug("CountTest(): stack → {Stack}", stack);
        stack.Top.Should().Be(4);
    }

    [Test]
    public static void ClearTest()
    {
		_logger.LogDebug(">>> Entering ClearTest()");
        var stack = new ArrayBasedStack<int>([0, 1, 2, 3, 4]);

		_logger.LogDebug("ClearTest(): stack → {Stack}", stack);
        stack.Clear();

        stack.Top.Should().Be(-1);
		_logger.LogDebug("<<< Exiting ClearTest()");
    }

    [Test]
    public static void ContainsTest()
    {
		_logger.LogDebug(">>> Entering ContainsTest()");
        var stack = new ArrayBasedStack<int>([0, 1, 2, 3, 4]);

		_logger.LogDebug("ContainsTest(): stack → {Stack}", stack);
        Assert.Multiple(() =>
        {
            stack.Contains(0).Should().BeTrue();
            stack.Contains(1).Should().BeTrue();
            stack.Contains(2).Should().BeTrue();
            stack.Contains(3).Should().BeTrue();
            stack.Contains(4).Should().BeTrue();
        });
		_logger.LogDebug("<<< Exiting ContainsTest()");
    }

    [Test]
    public static void PeekTest()
    {
		_logger.LogDebug(">>> Entering PeekTest()");
        var stack = new ArrayBasedStack<int>([0, 1, 2, 3, 4]);

		_logger.LogDebug("PeekTest(): stack → {Stack}", stack);
        Assert.Multiple(() =>
        {
            stack.Peek().Should().Be(4);
            stack.Peek().Should().Be(4);
            stack.Peek().Should().Be(4);
        });
		_logger.LogDebug("<<< Exiting PeekTest()");
    }

    [Test]
    public static void PopTest()
    {
		_logger.LogDebug(">>> Entering PopTest()");
        var stack = new ArrayBasedStack<int>([0, 1, 2, 3, 4]);

		_logger.LogDebug("PopTest(): stack → {Stack}", stack);
        Assert.Multiple(() =>
        {
            stack.Pop().Should().Be(4);
            stack.Pop().Should().Be(3);
            stack.Pop().Should().Be(2);
            stack.Pop().Should().Be(1);
            stack.Pop().Should().Be(0);
        });
		_logger.LogDebug("<<< Exiting PopTest()");
    }

    [Test]
    public static void PushTest()
    {
		_logger.LogDebug(">>> Entering PushTest()");
        var stack = new ArrayBasedStack<int>();

		_logger.LogDebug("PushTest(): stack → {Stack}", stack);
        Assert.Multiple(() =>
            Enumerable.Range(0, 5)
                .ToList()
                .ForEach(number =>
                {
                    stack.Push(number);
                    stack.Peek().Should().Be(number);
                }));
				_logger.LogDebug("<<< Exiting PushTest()");
    }

    [Test]
    public static void AutomaticResizesTest()
    {
		_logger.LogDebug(">>> Entering AutomaticResizesTest()");
        const int initialCapacity = 2;
        var stack = new ArrayBasedStack<int>
        {
            Capacity = initialCapacity,
        };

        stack.Push(0);
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        stack.Push(4);

        stack.Capacity.Should().BeGreaterThan(initialCapacity);
		_logger.LogDebug("<<< Exiting AutomaticResizesTest()");
    }

    [Test]
    public static void ShouldThrowStackEmptyExceptionOnEmptyPopTest()
    {
		_logger.LogDebug(">>> Entering ShouldThrowStackEmptyExceptionOnEmptyPopTest()");
        var stack = new ArrayBasedStack<int>();

		_logger.LogDebug("ShouldThrowStackEmptyExceptionOnEmptyPopTest(): stack → {Stack}", stack);
        Action poppingAnEmptyStack = () => stack.Pop();
		_logger.LogDebug("ShouldThrowStackEmptyExceptionOnEmptyPopTest(): poppingAnEmptyStack → {PoppingAnEmptyStack}", poppingAnEmptyStack);

        poppingAnEmptyStack.Should()
            .Throw<InvalidOperationException>()
		_logger.LogDebug("<<< Exiting ShouldThrowStackEmptyExceptionOnEmptyPopTest()");
            .WithMessage(StackEmptyErrorMessage);

    }

    [Test]
    public static void ShouldThrowStackEmptyExceptionOnEmptyPeekTest()
    {
		_logger.LogDebug(">>> Entering ShouldThrowStackEmptyExceptionOnEmptyPeekTest()");
        var stack = new ArrayBasedStack<int>();

		_logger.LogDebug("ShouldThrowStackEmptyExceptionOnEmptyPeekTest(): stack → {Stack}", stack);
        Action peekingAnEmptyStack = () => stack.Peek();
		_logger.LogDebug("ShouldThrowStackEmptyExceptionOnEmptyPeekTest(): peekingAnEmptyStack → {PeekingAnEmptyStack}", peekingAnEmptyStack);

        peekingAnEmptyStack.Should()
            .Throw<InvalidOperationException>()
            .WithMessage(StackEmptyErrorMessage);
			_logger.LogDebug("<<< Exiting ShouldThrowStackEmptyExceptionOnEmptyPeekTest()");
    }
}
