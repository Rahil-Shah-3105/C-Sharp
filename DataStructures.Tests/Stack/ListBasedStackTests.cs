using DataStructures.Stack;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Stack;

public static class ListBasedStackTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void CountTest()
    {
		_logger.LogDebug(">>> Entering CountTest()");
        var stack = new ListBasedStack<int>([0, 1, 2, 3, 4]);
        stack.Count.Should().Be(5);
		_logger.LogDebug("CountTest(): stack → {Stack}", stack);
		_logger.LogDebug("<<< Exiting CountTest()");
    }

    [Test]
    public static void ClearTest()
    {
		_logger.LogDebug(">>> Entering ClearTest()");
        var stack = new ListBasedStack<int>([0, 1, 2, 3, 4]);
        stack.Clear();
		_logger.LogDebug("ClearTest(): stack → {Stack}", stack);
        stack.Count.Should().Be(0);
		_logger.LogDebug("<<< Exiting ClearTest()");
    }

    [Test]
    public static void ContainsTest()
    {
		_logger.LogDebug(">>> Entering ContainsTest()");
        var stack = new ListBasedStack<int>([0, 1, 2, 3, 4]);

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
        var stack = new ListBasedStack<int>([0, 1, 2, 3, 4]);

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
        var stack = new ListBasedStack<int>([0, 1, 2, 3, 4]);

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
        var stack = new ListBasedStack<int>();

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
}
