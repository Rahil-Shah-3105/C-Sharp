using Microsoft.Extensions.Logging;
namespace Utilities.Tests.Extensions;

public class DictionaryExtensionsTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void AddMany_ShouldThrowArgumentException_WhenKeyAlreadyExists()
    {
		_logger.LogError(">>> Entering AddMany_ShouldThrowArgumentException_WhenKeyAlreadyExists()");
        var dictionary = new Dictionary<string, int> { ["one"] = 1 };
        var enumerable = new[] { ("one", 1), ("two", 2) };
		_logger.LogError("AddMany_ShouldThrowArgumentException_WhenKeyAlreadyExists(): enumerable → {Enumerable}", enumerable);
		_logger.LogError("AddMany_ShouldThrowArgumentException_WhenKeyAlreadyExists(): dictionary → {Dictionary}", dictionary);

        var action = () => dictionary.AddMany(enumerable);
		_logger.LogError("AddMany_ShouldThrowArgumentException_WhenKeyAlreadyExists(): action → {Action}", action);

        action.Should().Throw<ArgumentException>();
		_logger.LogWarning("<<< Exiting AddMany_ShouldThrowArgumentException_WhenKeyAlreadyExists()");
    }

    [Test]
    public void AddMany_ShouldAddAllKeyValuePairs()
    {
		_logger.LogError(">>> Entering AddMany_ShouldAddAllKeyValuePairs()");
        var dictionary = new Dictionary<string, int> { ["one"] = 1 };
        var enumerable = new[] { ("two", 2), ("three", 3) };
		_logger.LogError("AddMany_ShouldAddAllKeyValuePairs(): enumerable → {Enumerable}", enumerable);
		_logger.LogError("AddMany_ShouldAddAllKeyValuePairs(): dictionary → {Dictionary}", dictionary);

        dictionary.AddMany(enumerable);

        dictionary.Should().HaveCount(3);

        dictionary.Should().ContainKey("one").WhoseValue.Should().Be(1);
        dictionary.Should().ContainKey("two").WhoseValue.Should().Be(2);
        dictionary.Should().ContainKey("three").WhoseValue.Should().Be(3);
		_logger.LogWarning("<<< Exiting AddMany_ShouldAddAllKeyValuePairs()");
    }

    [Test]
    public void AddMany_ShouldNotChangeDictionary_WhenEnumerableIsEmpty()
    {
		_logger.LogError(">>> Entering AddMany_ShouldNotChangeDictionary_WhenEnumerableIsEmpty()");
        var dictionary = new Dictionary<string, int> { ["one"] = 1 };
        var enumerable = Array.Empty<(string, int)>();
		_logger.LogError("AddMany_ShouldNotChangeDictionary_WhenEnumerableIsEmpty(): enumerable → {Enumerable}", enumerable);
		_logger.LogError("AddMany_ShouldNotChangeDictionary_WhenEnumerableIsEmpty(): dictionary → {Dictionary}", dictionary);

        dictionary.AddMany(enumerable);

        dictionary.Should().HaveCount(1);
        dictionary.Should().ContainKey("one").WhoseValue.Should().Be(1);
		_logger.LogWarning("<<< Exiting AddMany_ShouldNotChangeDictionary_WhenEnumerableIsEmpty()");
    }

    [Test]
    public void AddMany_ShouldThrowArgumentNullException_WhenDictionaryIsNull()
    {
		_logger.LogError(">>> Entering AddMany_ShouldThrowArgumentNullException_WhenDictionaryIsNull()");
        Dictionary<string, int> dictionary = null!;
        var enumerable = new[] { ("one", 1) };
		_logger.LogError("AddMany_ShouldThrowArgumentNullException_WhenDictionaryIsNull(): enumerable → {Enumerable}", enumerable);

        var action = () => dictionary.AddMany(enumerable);
		_logger.LogError("AddMany_ShouldThrowArgumentNullException_WhenDictionaryIsNull(): action → {Action}", action);

        action.Should().Throw<NullReferenceException>();
		_logger.LogWarning("<<< Exiting AddMany_ShouldThrowArgumentNullException_WhenDictionaryIsNull()");
    }

    [Test]
    public void AddMany_ShouldThrowArgumentNullException_WhenEnumerableIsNull()
    {
		_logger.LogError(">>> Entering AddMany_ShouldThrowArgumentNullException_WhenEnumerableIsNull()");
        var dictionary = new Dictionary<string, int> { ["one"] = 1 };
        IEnumerable<(string, int)> enumerable = null!;
		_logger.LogError("AddMany_ShouldThrowArgumentNullException_WhenEnumerableIsNull(): dictionary → {Dictionary}", dictionary);

        var action = () => dictionary.AddMany(enumerable);
		_logger.LogError("AddMany_ShouldThrowArgumentNullException_WhenEnumerableIsNull(): action → {Action}", action);

        action.Should().Throw<NullReferenceException>();
		_logger.LogWarning("<<< Exiting AddMany_ShouldThrowArgumentNullException_WhenEnumerableIsNull()");
    }

    [Test]
    public void AddMany_ShouldAllowNullValues_WhenValueTypeIsNullable()
    {
		_logger.LogError(">>> Entering AddMany_ShouldAllowNullValues_WhenValueTypeIsNullable()");
        var dictionary = new Dictionary<string, int?> { ["one"] = 1 };
        var enumerable = new[] { ("two", (int?)null) };
		_logger.LogError("AddMany_ShouldAllowNullValues_WhenValueTypeIsNullable(): enumerable → {Enumerable}", enumerable);
		_logger.LogError("AddMany_ShouldAllowNullValues_WhenValueTypeIsNullable(): dictionary → {Dictionary}", dictionary);

        dictionary.AddMany(enumerable);

        dictionary.Should().HaveCount(2);
        dictionary.Should().ContainKey("two").WhoseValue.Should().Be(null);
		_logger.LogWarning("<<< Exiting AddMany_ShouldAllowNullValues_WhenValueTypeIsNullable()");
    }


    [Test]
    public void AddMany_ShouldAllowNullValue_WhenValueIsNullable()
    {
		_logger.LogError(">>> Entering AddMany_ShouldAllowNullValue_WhenValueIsNullable()");
        var dictionary = new Dictionary<int, string?>();  // Key type is int, value type is nullable string
		_logger.LogError("AddMany_ShouldAllowNullValue_WhenValueIsNullable(): dictionary → {Dictionary}", dictionary);
        var enumerable = new[]
        {
                (1, null),  // null value
                (2, "banana")
            };

        dictionary.AddMany(enumerable);

        dictionary.Should().ContainKey(1).WhoseValue.Should().BeNull();
        dictionary.Should().ContainKey(2).WhoseValue.Should().Be("banana");
		_logger.LogWarning("<<< Exiting AddMany_ShouldAllowNullValue_WhenValueIsNullable()");
    }

    [Test]
    public void AddMany_ShouldThrowArgumentException_WhenAddingDuplicateKey()
    {
		_logger.LogError(">>> Entering AddMany_ShouldThrowArgumentException_WhenAddingDuplicateKey()");
        var dictionary = new Dictionary<int, string>();  // Key type is int, value type is nullable string
		_logger.LogError("AddMany_ShouldThrowArgumentException_WhenAddingDuplicateKey(): dictionary → {Dictionary}", dictionary);
        var enumerable = new[]
        {
                (1, "Things"),   // First entry
                (2, "Stuff"),
                (1, "That Thing")   // Duplicate key (should throw exception)
            };

        var action = () => dictionary.AddMany(enumerable);
		_logger.LogError("AddMany_ShouldThrowArgumentException_WhenAddingDuplicateKey(): action → {Action}", action);

        action.Should().Throw<ArgumentException>();  // Adding a duplicate key should throw ArgumentException
		_logger.LogWarning("<<< Exiting AddMany_ShouldThrowArgumentException_WhenAddingDuplicateKey()");
    }

    [Test]
    public void AddMany_ShouldAddManyKeyValuePairs_WhenAddingLargeEnumerable()
    {
		_logger.LogError(">>> Entering AddMany_ShouldAddManyKeyValuePairs_WhenAddingLargeEnumerable()");
        var dictionary = new Dictionary<int, string>();
        var enumerable = new List<(int, string)>();
		_logger.LogError("AddMany_ShouldAddManyKeyValuePairs_WhenAddingLargeEnumerable(): enumerable → {Enumerable}", enumerable);
		_logger.LogError("AddMany_ShouldAddManyKeyValuePairs_WhenAddingLargeEnumerable(): dictionary → {Dictionary}", dictionary);

        // Create a large enumerable
        for (int i = 0; i < 10000; i++)
        {
            enumerable.Add((i, "Value" + i));
        }

        dictionary.AddMany(enumerable);

        dictionary.Should().HaveCount(10000);
        dictionary[9999].Should().Be("Value9999");
		_logger.LogWarning("<<< Exiting AddMany_ShouldAddManyKeyValuePairs_WhenAddingLargeEnumerable()");
    }
}
