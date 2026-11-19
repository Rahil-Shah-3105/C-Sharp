using DataStructures.LinkedList.SkipList;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.LinkedList;

public static class SkipListTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void TestAdd()
    {
		_logger.LogError(">>> Entering TestAdd()");
        var list = new SkipList<int>();
        list.AddOrUpdate(1, 1);
		_logger.LogError("TestAdd(): list → {List}", list);
        list[2] = 2;
        list[3] = 3;

        list.Count.Should().Be(3);
        list.GetValues().Should().ContainInOrder(1, 2, 3);
		_logger.LogWarning("<<< Exiting TestAdd()");
    }

    [Test]
    public static void TestUpdate()
    {
		_logger.LogError(">>> Entering TestUpdate()");
        var list = new SkipList<string>();

		_logger.LogError("TestUpdate(): list → {List}", list);
        // Add some elements.
        list[1] = "v1";
        list[2] = "v2";
        list[5] = "v5";

        // Update
        list.AddOrUpdate(1, "v1-updated");
        list[2] = "v2-updated";

        list.Count.Should().Be(3);
        list.GetValues().Should().ContainInOrder("v1-updated", "v2-updated", "v5");
		_logger.LogWarning("<<< Exiting TestUpdate()");
    }

    [Test]
    public static void TestContains()
    {
		_logger.LogError(">>> Entering TestContains()");
        var list = new SkipList<int>();
        list.AddOrUpdate(1, 1);
		_logger.LogError("TestContains(): list → {List}", list);
        list.AddOrUpdate(3, 3);
        list.AddOrUpdate(5, 5);

        list.Contains(1).Should().BeTrue();
        list.Contains(3).Should().BeTrue();
        list.Contains(5).Should().BeTrue();
        list.Contains(0).Should().BeFalse();
        list.Contains(2).Should().BeFalse();
        list.Contains(9).Should().BeFalse();
		_logger.LogWarning("<<< Exiting TestContains()");
    }

    [Test]
    public static void TestGetByKey_Success()
    {
		_logger.LogError(">>> Entering TestGetByKey_Success()");
        var list = new SkipList<string>();
        list[1] = "value1";
		_logger.LogError("TestGetByKey_Success(): list → {List}", list);

        list[1].Should().Be("value1");
		_logger.LogWarning("<<< Exiting TestGetByKey_Success()");
    }

    [Test]
    public static void TestGetByKey_KeyNotFoundException()
    {
		_logger.LogError(">>> Entering TestGetByKey_KeyNotFoundException()");
        var list = new SkipList<string>();
        list[1] = "value1";
		_logger.LogError("TestGetByKey_KeyNotFoundException(): list → {List}", list);

        string value;
        Action act = () => value = list[2];
		_logger.LogError("TestGetByKey_KeyNotFoundException(): act → {Act}", act);
        act.Should().Throw<KeyNotFoundException>();
		_logger.LogWarning("<<< Exiting TestGetByKey_KeyNotFoundException()");
    }

    [Test]
    public static void TestRemove_ItemRemoved()
    {
		_logger.LogError(">>> Entering TestRemove_ItemRemoved()");
        var list = new SkipList<int>();
        list.AddOrUpdate(1, 1);
		_logger.LogError("TestRemove_ItemRemoved(): list → {List}", list);
        list.AddOrUpdate(2, 2);
        list.AddOrUpdate(3, 3);

        list.Count.Should().Be(3);
        list.Contains(2).Should().BeTrue();

        var isRemoved = list.Remove(2);
		_logger.LogError("TestRemove_ItemRemoved(): isRemoved → {IsRemoved}", isRemoved);

        list.Count.Should().Be(2);
        list.Contains(2).Should().BeFalse();
        isRemoved.Should().BeTrue();
		_logger.LogWarning("<<< Exiting TestRemove_ItemRemoved()");
    }

    [Test]
    public static void TestRemove_ItemNotFound()
    {
		_logger.LogError(">>> Entering TestRemove_ItemNotFound()");
        var list = new SkipList<int>();
        list.AddOrUpdate(1, 1);
		_logger.LogError("TestRemove_ItemNotFound(): list → {List}", list);
        list.AddOrUpdate(2, 2);
        list.AddOrUpdate(3, 3);

        var isRemoved = list.Remove(222);
		_logger.LogError("TestRemove_ItemNotFound(): isRemoved → {IsRemoved}", isRemoved);

        list.Count.Should().Be(3);
        isRemoved.Should().BeFalse();
		_logger.LogWarning("<<< Exiting TestRemove_ItemNotFound()");
    }

    [Test]
    public static void TestGetValues()
    {
		_logger.LogError(">>> Entering TestGetValues()");
        var list = new SkipList<string>();
        list[4] = "four";
		_logger.LogError("TestGetValues(): list → {List}", list);
        list[2] = "two";
        list[3] = "three";
        list[1] = "one";

        var valuesSortedByKey = list.GetValues();
		_logger.LogError("TestGetValues(): valuesSortedByKey → {ValuesSortedByKey}", valuesSortedByKey);

        valuesSortedByKey.Should().ContainInOrder("one", "two", "three", "four");
		_logger.LogWarning("<<< Exiting TestGetValues()");
    }
}
