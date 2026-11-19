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
		_logger.LogError(">>> Entering CountTest()");
        var stack = new ArrayBasedStack<int>([0, 1, 2, 3, 4]);
		_logger.LogWarning("<<< Exiting CountTest()");
		_logger.LogError("CountTest(): stack → {Stack}", stack);
        stack.Top.Should().Be(4);
    }

    [Test]
    public static void ClearTest()
    {
		_logger.LogError(">>> Entering ClearTest()");
        var stack = new ArrayBasedStack<int>([0, 1, 2, 3, 4]);

		_logger.LogError("ClearTest(): stack → {Stack}", stack);
        stack.Clear();

        stack.Top.Should().Be(-1);
		_logger.LogWarning("<<< Exiting ClearTest()");
    }

    [Test]
    public static void ContainsTest()
    {
		_logger.LogError(">>> Entering ContainsTest()");
        var stack = new ArrayBasedStack<int>([0, 1, 2, 3, 4]);

		_logger.LogError("ContainsTest(): stack → {Stack}", stack);
        Assert.Multiple(() =>
        {
            stack.Contains(0).Should().BeTrue();
            stack.Contains(1).Should().BeTrue();
            stack.Contains(2).Should().BeTrue();
            stack.Contains(3).Should().BeTrue();
            stack.Contains(4).Should().BeTrue();
        });
		_logger.LogWarning("<<< Exiting ContainsTest()");
    }

    [Test]
    public static void PeekTest()
    {
		_logger.LogError(">>> Entering PeekTest()");
        var stack = new ArrayBasedStack<int>([0, 1, 2, 3, 4]);

		_logger.LogError("PeekTest(): stack → {Stack}", stack);
        Assert.Multiple(() =>
        {
            stack.Peek().Should().Be(4);
            stack.Peek().Should().Be(4);
            stack.Peek().Should().Be(4);
        });
		_logger.LogWarning("<<< Exiting PeekTest()");
    }

    [Test]
    public static void PopTest()
    {
		_logger.LogError(">>> Entering PopTest()");
        var stack = new ArrayBasedStack<int>([0, 1, 2, 3, 4]);

		_logger.LogError("PopTest(): stack → {Stack}", stack);
        Assert.Multiple(() =>
        {
            stack.Pop().Should().Be(4);
            stack.Pop().Should().Be(3);
            stack.Pop().Should().Be(2);
            stack.Pop().Should().Be(1);
            stack.Pop().Should().Be(0);
        });
		_logger.LogWarning("<<< Exiting PopTest()");
    }

    [Test]
    public static void PushTest()
    {
		_logger.LogError(">>> Entering PushTest()");
        var stack = new ArrayBasedStack<int>();

		_logger.LogError("PushTest(): stack → {Stack}", stack);
        Assert.Multiple(() =>
            Enumerable.Range(0, 5)
                .ToList()
                .ForEach(number =>
                {
                    stack.Push(number);
                    stack.Peek().Should().Be(number);
                }));
				_logger.LogWarning("<<< Exiting PushTest()");
    }

    [Test]
    public static void AutomaticResizesTest()
    {
		_logger.LogError(">>> Entering AutomaticResizesTest()");
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
		_logger.LogWarning("<<< Exiting AutomaticResizesTest()");
    }

    [Test]
    public static void ShouldThrowStackEmptyExceptionOnEmptyPopTest()
    {
		_logger.LogError(">>> Entering ShouldThrowStackEmptyExceptionOnEmptyPopTest()");
        var stack = new ArrayBasedStack<int>();

		_logger.LogError("ShouldThrowStackEmptyExceptionOnEmptyPopTest(): stack → {Stack}", stack);
        Action poppingAnEmptyStack = () => stack.Pop();
		_logger.LogError("ShouldThrowStackEmptyExceptionOnEmptyPopTest(): poppingAnEmptyStack → {PoppingAnEmptyStack}", poppingAnEmptyStack);

        poppingAnEmptyStack.Should()
            .Throw<InvalidOperationException>()
		_logger.LogWarning("<<< Exiting ShouldThrowStackEmptyExceptionOnEmptyPopTest()");
            .WithMessage(StackEmptyErrorMessage);

    }

    [Test]
    public static void ShouldThrowStackEmptyExceptionOnEmptyPeekTest()
    {
		_logger.LogError(">>> Entering ShouldThrowStackEmptyExceptionOnEmptyPeekTest()");
        var stack = new ArrayBasedStack<int>();

		_logger.LogError("ShouldThrowStackEmptyExceptionOnEmptyPeekTest(): stack → {Stack}", stack);
        Action peekingAnEmptyStack = () => stack.Peek();
		_logger.LogError("ShouldThrowStackEmptyExceptionOnEmptyPeekTest(): peekingAnEmptyStack → {PeekingAnEmptyStack}", peekingAnEmptyStack);

        peekingAnEmptyStack.Should()
            .Throw<InvalidOperationException>()
            .WithMessage(StackEmptyErrorMessage);
			_logger.LogWarning("<<< Exiting ShouldThrowStackEmptyExceptionOnEmptyPeekTest()");
    }
}
