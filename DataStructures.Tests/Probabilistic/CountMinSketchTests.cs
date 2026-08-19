using DataStructures.Probabilistic;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Probabilistic;

public class CountMinSketchTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public class SimpleObject(string name, int number)
    {
        public string Name { get; set; } = name;
        public int Number { get; set; } = number;
    }

    [Test]
    public void TestInsertAndCount()
    {
		_logger.LogDebug(">>> Entering TestInsertAndCount()");
        var obj1 = new SimpleObject("foo", 5);
        var obj2 = new SimpleObject("bar", 6);
		_logger.LogDebug("TestInsertAndCount(): obj2 → {Obj2}", obj2);
		_logger.LogDebug("TestInsertAndCount(): obj1 → {Obj1}", obj1);

        var sketch = new CountMinSketch<SimpleObject>(200, 5);
		_logger.LogDebug("TestInsertAndCount(): sketch → {Sketch}", sketch);

        for (var i = 0; i < 5000; i++)
        {
            sketch.Insert(obj1);
            sketch.Insert(obj2);
        }

        sketch.Query(obj1).Should().BeGreaterOrEqualTo(5000);
        sketch.Query(obj2).Should().BeGreaterOrEqualTo(5000);
		_logger.LogDebug("<<< Exiting TestInsertAndCount()");
    }

    [Test]
    public void TestOptimalInitializer()
    {
		_logger.LogDebug(">>> Entering TestOptimalInitializer()");
        var obj1 = new SimpleObject("foo", 5);
        var obj2 = new SimpleObject("bar", 6);
		_logger.LogDebug("TestOptimalInitializer(): obj2 → {Obj2}", obj2);
		_logger.LogDebug("TestOptimalInitializer(): obj1 → {Obj1}", obj1);

        var sketch = new CountMinSketch<SimpleObject>(.001, .05);
		_logger.LogDebug("TestOptimalInitializer(): sketch → {Sketch}", sketch);

        for (var i = 0; i < 5000; i++)
        {
            sketch.Insert(obj1);
            sketch.Insert(obj2);
        }

        sketch.Query(obj1).Should().BeGreaterOrEqualTo(5000);
        sketch.Query(obj2).Should().BeGreaterOrEqualTo(5000);
		_logger.LogDebug("<<< Exiting TestOptimalInitializer()");
    }

    [Test]
    public void TestProbabilities()
    {
		_logger.LogDebug(">>> Entering TestProbabilities()");
        var sketch = new CountMinSketch<int>(.01, .05);
        var random = new Random();
		_logger.LogDebug("TestProbabilities(): random → {Random}", random);
        var insertedItems = new Dictionary<int, int>();
		_logger.LogDebug("TestProbabilities(): insertedItems → {InsertedItems}", insertedItems);
		_logger.LogDebug("TestProbabilities(): sketch → {Sketch}", sketch);
        for (var i = 0; i < 10000; i++)
        {
            var item = random.Next(0, 1000000);
			_logger.LogDebug("TestProbabilities(): item → {Item}", item);
            sketch.Insert(item);
            if (insertedItems.ContainsKey(item))
            {
                insertedItems[item]++;
            }
            else
            {
                insertedItems.Add(item, 1);
            }
        }

        var numMisses = 0;
        foreach (var item in insertedItems)
        {
            if (sketch.Query(item.Key) - item.Value > .01 * 100000)
            {
                numMisses++;
            }
        }

        (numMisses / (double)insertedItems.Count).Should().BeLessOrEqualTo(.05);
		_logger.LogDebug("<<< Exiting TestProbabilities()");
    }
}
