using DataStructures.Probabilistic;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Probabilistic;

public class BloomFilterTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    static readonly string[] TestNames = ["kal;jsnfka", "alkjsdfn;lakm", "aljfopiawjf", "afowjeaofeij", "oajwsefoaiwje", "aoiwjfaoiejmf", "aoijfoawiejf"];

    private class SimpleObject(string name, int number)
    {
        public string Name { get; set; } = name;
        public int Number { get; set; } = number;
    }

    private class SimpleObjectOverridenHash(string name, int number)
    {
        private const uint FnvPrime = 16777619;
        private const uint FnvOffsetBasis = 2166136261;
        public string Name { get; set; } = name;
        public int Number { get; set; } = number;

        public override int GetHashCode()
        {
			_logger.LogDebug(">>> Entering GetHashCode()");
            var bytes = Encoding.UTF8.GetBytes(Name).Concat(BitConverter.GetBytes(Number));
            var hash = FnvOffsetBasis;
			_logger.LogDebug("GetHashCode(): hash → {Hash}", hash);
			_logger.LogDebug("GetHashCode(): bytes → {Bytes}", bytes);
            foreach (var @byte in bytes)
            {
                hash = hash * FnvPrime;
                hash ^= @byte;
            }

			_logger.LogDebug("<<< Exiting GetHashCode()");
            return (int)hash;
        }

        public override bool Equals(object? obj)
        {
			_logger.LogDebug(">>> Entering Equals(obj={Obj})", obj);
            return obj is SimpleObjectOverridenHash asSimpleObj && asSimpleObj.Name == Name && asSimpleObj.Number == Number;
        }
    }

    [Test]
    public void TestBloomFilterInsertOptimalSize()
    {
		_logger.LogDebug(">>> Entering TestBloomFilterInsertOptimalSize()");
        var filter = new BloomFilter<int>(1000);
        var set = new HashSet<int>();
		_logger.LogDebug("TestBloomFilterInsertOptimalSize(): set → {Set}", set);
        var rand = new Random(124);
		_logger.LogDebug("TestBloomFilterInsertOptimalSize(): rand → {Rand}", rand);
		_logger.LogDebug("TestBloomFilterInsertOptimalSize(): filter → {Filter}", filter);
        var falsePositives = 0;
        for (var i = 0; i < 1000; i++)
        {
            var k = rand.Next(0, 1000);
            if (!set.Contains(k) && filter.Search(k))
            {
                falsePositives++;
            }
            filter.Insert(k);
            set.Add(k);
            Assert.That(filter.Search(k), Is.True);
        }

        Assert.That(.05 > falsePositives / 1000.0, Is.True); // be a bit generous in our fault tolerance here
		_logger.LogDebug("<<< Exiting TestBloomFilterInsertOptimalSize()");
    }

    [Test]
    public void TestBloomFilterInsert()
    {
		_logger.LogDebug(">>> Entering TestBloomFilterInsert()");
        var filter = new BloomFilter<SimpleObject>(100000, 3);
        var rand = new Random();
		_logger.LogDebug("TestBloomFilterInsert(): rand → {Rand}", rand);
		_logger.LogDebug("TestBloomFilterInsert(): filter → {Filter}", filter);
        for (var i = 0; i < 1000; i++)
        {
            var simpleObject = new SimpleObject(TestNames[rand.Next(TestNames.Length)], rand.Next(15));
			_logger.LogDebug("TestBloomFilterInsert(): simpleObject → {SimpleObject}", simpleObject);
            filter.Insert(simpleObject);
            Assert.That(filter.Search(simpleObject), Is.True);
        }
		_logger.LogDebug("<<< Exiting TestBloomFilterInsert()");
    }

    [Test]
    public void TestBloomFilterSearchOverridenHash()
    {
		_logger.LogDebug(">>> Entering TestBloomFilterSearchOverridenHash()");
        var filter = new BloomFilter<SimpleObjectOverridenHash>(100000, 3);
        var simpleObjectInserted = new SimpleObjectOverridenHash("foo", 1);
		_logger.LogDebug("TestBloomFilterSearchOverridenHash(): simpleObjectInserted → {SimpleObjectInserted}", simpleObjectInserted);
        var simpleObjectInserted2 = new SimpleObjectOverridenHash("foo", 1);
		_logger.LogDebug("TestBloomFilterSearchOverridenHash(): simpleObjectInserted2 → {SimpleObjectInserted2}", simpleObjectInserted2);
        var simpleObjectNotInserted = new SimpleObjectOverridenHash("bar", 2);
		_logger.LogDebug("TestBloomFilterSearchOverridenHash(): simpleObjectNotInserted → {SimpleObjectNotInserted}", simpleObjectNotInserted);
		_logger.LogDebug("TestBloomFilterSearchOverridenHash(): filter → {Filter}", filter);
        filter.Insert(simpleObjectInserted);
        Assert.That(filter.Search(simpleObjectInserted), Is.True);
        Assert.That(filter.Search(simpleObjectInserted2), Is.True);
        Assert.That(filter.Search(simpleObjectNotInserted), Is.False);
		_logger.LogDebug("<<< Exiting TestBloomFilterSearchOverridenHash()");
    }

    [Test]
    public void TestBloomFilterSearch()
    {
		_logger.LogDebug(">>> Entering TestBloomFilterSearch()");
        var filter = new BloomFilter<SimpleObject>(10000, 3);
        var simpleObjectInserted = new SimpleObject("foo", 1);
		_logger.LogDebug("TestBloomFilterSearch(): simpleObjectInserted → {SimpleObjectInserted}", simpleObjectInserted);
        var simpleObjectNotInserted = new SimpleObject("foo", 1);
		_logger.LogDebug("TestBloomFilterSearch(): simpleObjectNotInserted → {SimpleObjectNotInserted}", simpleObjectNotInserted);
		_logger.LogDebug("TestBloomFilterSearch(): filter → {Filter}", filter);
        filter.Insert(simpleObjectInserted);
        Assert.That(filter.Search(simpleObjectNotInserted), Is.False);
		_logger.LogDebug("<<< Exiting TestBloomFilterSearch()");
        Assert.That(filter.Search(simpleObjectInserted), Is.True);

    }
}
