using DataStructures.Stack;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Stack;

public static class ListBasedStackTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void CountTest()
    {
		_logger.LogError(">>> Entering CountTest()");
        var stack = new ListBasedStack<int>([0, 1, 2, 3, 4]);
		_logger.LogWarning("<<< Exiting CountTest()");
		_logger.LogError("CountTest(): stack → {Stack}", stack);
        stack.Count.Should().Be(5);
    }

    [Test]
    public static void ClearTest()
    {
		_logger.LogError(">>> Entering ClearTest()");
        var stack = new ListBasedStack<int>([0, 1, 2, 3, 4]);
        stack.Clear();
		_logger.LogError("ClearTest(): stack → {Stack}", stack);
        stack.Count.Should().Be(0);
		_logger.LogWarning("<<< Exiting ClearTest()");
    }

    [Test]
    public static void ContainsTest()
    {
		_logger.LogError(">>> Entering ContainsTest()");
        var stack = new ListBasedStack<int>([0, 1, 2, 3, 4]);

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
        var stack = new ListBasedStack<int>([0, 1, 2, 3, 4]);

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
        var stack = new ListBasedStack<int>([0, 1, 2, 3, 4]);

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
        var stack = new ListBasedStack<int>();

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
}
