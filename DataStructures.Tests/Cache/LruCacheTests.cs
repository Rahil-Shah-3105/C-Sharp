using DataStructures.Cache;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Cache;

public static class LruCacheTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void TestPutGet()
    {
		_logger.LogDebug(">>> Entering TestPutGet()");
        var cache = new LruCache<int, string>();
        cache.Put(1, "one");
		_logger.LogDebug("TestPutGet(): cache → {Cache}", cache);

        cache.Contains(1).Should().BeTrue();
        cache.Get(1).Should().Be("one");
		_logger.LogDebug("<<< Exiting TestPutGet()");
    }

    [Test]
    public static void TestCacheMiss()
    {
		_logger.LogDebug(">>> Entering TestCacheMiss()");
        var cache = new LruCache<int, string>();
        cache.Put(1, "one");
		_logger.LogDebug("TestCacheMiss(): cache → {Cache}", cache);

        cache.Contains(5).Should().BeFalse();
        cache.Get(5).Should().BeNull();
		_logger.LogDebug("<<< Exiting TestCacheMiss()");
    }

    [Test]
    public static void TestCacheUpdate()
    {
		_logger.LogDebug(">>> Entering TestCacheUpdate()");
        var cache = new LruCache<int, string>();
        cache.Put(1, "one");
		_logger.LogDebug("TestCacheUpdate(): cache → {Cache}", cache);
        cache.Put(1, "ONE");

        cache.Get(1).Should().Be("ONE");
		_logger.LogDebug("<<< Exiting TestCacheUpdate()");
    }

    [Test]
    public static void RemoveOldestItem_ItemWasNotUsed()
    {
		_logger.LogDebug(">>> Entering RemoveOldestItem_ItemWasNotUsed()");
        var cache = new LruCache<int, string>(capacity: 2);
        cache.Put(1, "one");
		_logger.LogDebug("RemoveOldestItem_ItemWasNotUsed(): cache → {Cache}", cache);
        cache.Put(2, "two");

        // Add to the full cache, 1 will be removed
        cache.Put(3, "three");

        cache.Get(1).Should().BeNull();
        cache.Get(2).Should().Be("two");
        cache.Get(3).Should().Be("three");
		_logger.LogDebug("<<< Exiting RemoveOldestItem_ItemWasNotUsed()");
    }

    [Test]
    public static void RemoveOldestItem_ItemWasRecentlyUsed()
    {
		_logger.LogDebug(">>> Entering RemoveOldestItem_ItemWasRecentlyUsed()");
        var cache = new LruCache<int, string>(capacity: 2);
        cache.Put(1, "one");
		_logger.LogDebug("RemoveOldestItem_ItemWasRecentlyUsed(): cache → {Cache}", cache);
        cache.Put(2, "two");
        cache.Get(1);

        // Add to the full cache, 1 was used, 2 should be removed
        cache.Put(3, "three");

        cache.Get(1).Should().Be("one");
        cache.Get(2).Should().BeNull();
        cache.Get(3).Should().Be("three");
		_logger.LogDebug("<<< Exiting RemoveOldestItem_ItemWasRecentlyUsed()");
    }
}
