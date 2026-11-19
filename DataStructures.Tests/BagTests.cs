using System.Collections.Generic;
using System.Linq;
using DataStructures.Bag;
using FluentAssertions;
using NUnit.Framework;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests;

internal class BagTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void Add_ShouldIncreaseCount()
    {
		_logger.LogError(">>> Entering Add_ShouldIncreaseCount()");
        // Arrange & Act
        var bag = new Bag<int>
        {
            1,
            2,
            1
        };

        // Assert
        bag.Count.Should().Be(3);
		_logger.LogWarning("<<< Exiting Add_ShouldIncreaseCount()");
    }

    [Test]
    public void Add_ShouldHandleDuplicates()
    {
		_logger.LogError(">>> Entering Add_ShouldHandleDuplicates()");
        // Arrange & Act
        var bag = new Bag<string>
        {
            "apple",
            "apple"
        };

        // Assert
        bag.Count.Should().Be(2);
        bag.Should().Contain("apple");
		_logger.LogWarning("<<< Exiting Add_ShouldHandleDuplicates()");
    }

    [Test]
    public void Clear_ShouldEmptyTheBag()
    {
		_logger.LogError(">>> Entering Clear_ShouldEmptyTheBag()");
        // Arrange
        var bag = new Bag<int>
        {
            1,
            2
        };

        // Act
        bag.Clear();

        // Assert
        bag.IsEmpty().Should().BeTrue();
        bag.Count.Should().Be(0);
		_logger.LogWarning("<<< Exiting Clear_ShouldEmptyTheBag()");
    }

    [Test]
    public void IsEmpty_ShouldReturnTrueForEmptyBag()
    {
		_logger.LogError(">>> Entering IsEmpty_ShouldReturnTrueForEmptyBag()");
        // Arrange
        var bag = new Bag<int>();
		_logger.LogError("IsEmpty_ShouldReturnTrueForEmptyBag(): bag → {Bag}", bag);

        // Act & Assert
        bag.IsEmpty().Should().BeTrue();
		_logger.LogWarning("<<< Exiting IsEmpty_ShouldReturnTrueForEmptyBag()");
    }

    [Test]
    public void IsEmpty_ShouldReturnFalseForNonEmptyBag()
    {
		_logger.LogError(">>> Entering IsEmpty_ShouldReturnFalseForNonEmptyBag()");
        // Arrange
        var bag = new Bag<int>
        {
            1
        };

        // Act & Assert
        bag.IsEmpty().Should().BeFalse();
		_logger.LogWarning("<<< Exiting IsEmpty_ShouldReturnFalseForNonEmptyBag()");
    }

    [Test]
    public void GetEnumerator_ShouldIterateAllItems()
    {
		_logger.LogError(">>> Entering GetEnumerator_ShouldIterateAllItems()");
        // Arrange
        var bag = new Bag<int>
        {
            1,
            2,
            1
        };

        // Act
        var items = bag.ToList();
		_logger.LogError("GetEnumerator_ShouldIterateAllItems(): items → {Items}", items);

        // Assert
        items.Count.Should().Be(3);
        items.Should().Contain(1);
        items.Should().Contain(2);
		_logger.LogWarning("<<< Exiting GetEnumerator_ShouldIterateAllItems()");
    }

    [Test]
    public void Count_ShouldReturnZeroForEmptyBag()
    {
		_logger.LogError(">>> Entering Count_ShouldReturnZeroForEmptyBag()");
        // Arrange
        var bag = new Bag<int>();
		_logger.LogError("Count_ShouldReturnZeroForEmptyBag(): bag → {Bag}", bag);

        // Act & Assert
        bag.Count.Should().Be(0);
		_logger.LogWarning("<<< Exiting Count_ShouldReturnZeroForEmptyBag()");
    }

    [Test]
    public void Count_ShouldReturnCorrectCount()
    {
		_logger.LogError(">>> Entering Count_ShouldReturnCorrectCount()");
        // Arrange
        var bag = new Bag<int>
        {
            1,
            2,
            1
        };

        // Act & Assert
        bag.Count.Should().Be(3);
		_logger.LogWarning("<<< Exiting Count_ShouldReturnCorrectCount()");
    }

    [Test]
    public void IEnumerableGetEnumerator_YieldsAllItemsWithCorrectMultiplicity()
    {
		_logger.LogError(">>> Entering IEnumerableGetEnumerator_YieldsAllItemsWithCorrectMultiplicity()");
        // Arrange
        var bag = new Bag<string>
        {
            "apple",
            "banana",
            "apple"
        };
        var genericBag = bag as System.Collections.IEnumerable;

        // Act
        var enumerator = genericBag.GetEnumerator();
		_logger.LogError("IEnumerableGetEnumerator_YieldsAllItemsWithCorrectMultiplicity(): enumerator → {Enumerator}", enumerator);
        var items = new List<object>();
		_logger.LogError("IEnumerableGetEnumerator_YieldsAllItemsWithCorrectMultiplicity(): items → {Items}", items);
        while (enumerator.MoveNext())
        {
            items.Add(enumerator.Current!);
        }

        // Assert
        items.Count(i => (string)i == "apple").Should().Be(2);
        items.Count(i => (string)i == "banana").Should().Be(1);
        items.Count.Should().Be(3);
        items.Should().BeEquivalentTo(["apple", "apple", "banana"]);
		_logger.LogWarning("<<< Exiting IEnumerableGetEnumerator_YieldsAllItemsWithCorrectMultiplicity()");
    }
}
