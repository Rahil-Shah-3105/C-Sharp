using DataStructures.Cache;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Cache;

public static class LfuCacheTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void TestPutGet()
    {
		_logger.LogDebug(">>> Entering TestPutGet()");
        var cache = new LfuCache<int, string>();
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
        var cache = new LfuCache<int, string>();
        cache.Put(1, "one");
		_logger.LogDebug("TestCacheMiss(): cache → {Cache}", cache);

        cache.Contains(5).Should().BeFalse();
        cache.Get(5).Should().BeNull();
		_logger.LogDebug("<<< Exiting TestCacheMiss()");
    }

    [Test]
    public static void Evict_ItemWasNotUsed()
    {
		_logger.LogDebug(">>> Entering Evict_ItemWasNotUsed()");
        var cache = new LfuCache<int, string>(capacity: 1);
        cache.Put(1, "one");
		_logger.LogDebug("Evict_ItemWasNotUsed(): cache → {Cache}", cache);

        // Add to the full cache, 1 will be removed
        cache.Put(2, "two");

        cache.Get(1).Should().BeNull();
        cache.Get(2).Should().Be("two");
		_logger.LogDebug("<<< Exiting Evict_ItemWasNotUsed()");
    }

    [Test]
    public static void Evict_OneItemWasUsed()
    {
		_logger.LogDebug(">>> Entering Evict_OneItemWasUsed()");
        var cache = new LfuCache<int, string>(capacity: 2);
        cache.Put(1, "one");
		_logger.LogDebug("Evict_OneItemWasUsed(): cache → {Cache}", cache);
        cache.Put(2, "two");

        cache.Put(1, "ONE");

        // Add to the full cache, 2 will be removed
        cache.Put(3, "three");

        cache.Get(1).Should().Be("ONE");
        cache.Get(2).Should().BeNull();
        cache.Get(3).Should().Be("three");
		_logger.LogDebug("<<< Exiting Evict_OneItemWasUsed()");
    }

    [Test]
    public static void Evict_LruOrder()
    {
		_logger.LogDebug(">>> Entering Evict_LruOrder()");
        var cache = new LfuCache<int, string>(capacity: 2);
        cache.Put(1, "one");
		_logger.LogDebug("Evict_LruOrder(): cache → {Cache}", cache);
        cache.Put(2, "two");

        cache.Put(1, "ONE");
        cache.Put(2, "TWO");

        // Add to the full cache, 1 will be removed
        cache.Put(3, "three");

        cache.Get(1).Should().BeNull();
        cache.Get(2).Should().Be("TWO");
        cache.Get(3).Should().Be("three");
		_logger.LogDebug("<<< Exiting Evict_LruOrder()");
    }
}
