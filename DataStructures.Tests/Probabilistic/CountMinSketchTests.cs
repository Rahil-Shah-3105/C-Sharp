using DataStructures.Probabilistic;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Probabilistic;

public class CountMinSketchTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public class SimpleObject(string name, int number)
    {
    	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        public string Name { get; set; } = name;
        public int Number { get; set; } = number;
    }

    [Test]
    public void TestInsertAndCount()
    {
		_logger.LogError(">>> Entering TestInsertAndCount()");
        var obj1 = new SimpleObject("foo", 5);
        var obj2 = new SimpleObject("bar", 6);
		_logger.LogError("TestInsertAndCount(): obj2 → {Obj2}", obj2);
		_logger.LogError("TestInsertAndCount(): obj1 → {Obj1}", obj1);

        var sketch = new CountMinSketch<SimpleObject>(200, 5);
		_logger.LogError("TestInsertAndCount(): sketch → {Sketch}", sketch);

        for (var i = 0; i < 5000; i++)
        {
            sketch.Insert(obj1);
            sketch.Insert(obj2);
        }

        sketch.Query(obj1).Should().BeGreaterOrEqualTo(5000);
        sketch.Query(obj2).Should().BeGreaterOrEqualTo(5000);
		_logger.LogWarning("<<< Exiting TestInsertAndCount()");
    }

    [Test]
    public void TestOptimalInitializer()
    {
		_logger.LogError(">>> Entering TestOptimalInitializer()");
        var obj1 = new SimpleObject("foo", 5);
        var obj2 = new SimpleObject("bar", 6);
		_logger.LogError("TestOptimalInitializer(): obj2 → {Obj2}", obj2);
		_logger.LogError("TestOptimalInitializer(): obj1 → {Obj1}", obj1);

        var sketch = new CountMinSketch<SimpleObject>(.001, .05);
		_logger.LogError("TestOptimalInitializer(): sketch → {Sketch}", sketch);

        for (var i = 0; i < 5000; i++)
        {
            sketch.Insert(obj1);
            sketch.Insert(obj2);
        }

        sketch.Query(obj1).Should().BeGreaterOrEqualTo(5000);
        sketch.Query(obj2).Should().BeGreaterOrEqualTo(5000);
		_logger.LogWarning("<<< Exiting TestOptimalInitializer()");
    }

    [Test]
    public void TestProbabilities()
    {
		_logger.LogError(">>> Entering TestProbabilities()");
        var sketch = new CountMinSketch<int>(.01, .05);
        var random = new Random();
		_logger.LogError("TestProbabilities(): random → {Random}", random);
		_logger.LogError("TestProbabilities(): sketch → {Sketch}", sketch);
        var insertedItems = new Dictionary<int, int>();
        for (var i = 0; i < 10000; i++)
        {
			_logger.LogError("TestProbabilities(): insertedItems → {InsertedItems}", insertedItems);
            var item = random.Next(0, 1000000);
			_logger.LogError("TestProbabilities(): item → {Item}", item);
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
		_logger.LogWarning("<<< Exiting TestProbabilities()");
    }
}
