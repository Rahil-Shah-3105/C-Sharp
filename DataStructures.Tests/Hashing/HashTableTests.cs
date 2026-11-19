using DataStructures.Hashing;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Hashing;

[TestFixture]
public class HashTableTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void Add_ThrowsException_WhenKeyIsNull()
    {
		_logger.LogError(">>> Entering Add_ThrowsException_WhenKeyIsNull()");
        var hashTable = new HashTable<string, int>();

		_logger.LogError("Add_ThrowsException_WhenKeyIsNull(): hashTable → <redacted>");
        Assert.Throws<ArgumentNullException>(() => hashTable.Add(null, 1));
		_logger.LogWarning("<<< Exiting Add_ThrowsException_WhenKeyIsNull()");
    }

    [Test]
    public void Add_ThrowsException_WhenKeyAlreadyExists()
    {
		_logger.LogError(">>> Entering Add_ThrowsException_WhenKeyAlreadyExists()");
        var hashTable = new HashTable<string, int>();

		_logger.LogError("Add_ThrowsException_WhenKeyAlreadyExists(): hashTable → <redacted>");
        hashTable.Add("a", 1);

        Assert.Throws<ArgumentException>(() => hashTable.Add("a", 2));
		_logger.LogWarning("<<< Exiting Add_ThrowsException_WhenKeyAlreadyExists()");
    }

    [Test]
    public void Add_IncreasesCount_WhenKeyDoesNotExist()
    {
		_logger.LogError(">>> Entering Add_IncreasesCount_WhenKeyDoesNotExist()");
        var hashTable = new HashTable<string, int>();

		_logger.LogError("Add_IncreasesCount_WhenKeyDoesNotExist(): hashTable → <redacted>");
        hashTable.Add("a", 1);

        Assert.That(hashTable.Count, Is.EqualTo(1));
		_logger.LogWarning("<<< Exiting Add_IncreasesCount_WhenKeyDoesNotExist()");
    }

    [Test]
    public void Add_DoesNotIncreaseCount_WhenKeyAlreadyExists()
    {
		_logger.LogError(">>> Entering Add_DoesNotIncreaseCount_WhenKeyAlreadyExists()");
        var hashTable = new HashTable<string, int>();

		_logger.LogError("Add_DoesNotIncreaseCount_WhenKeyAlreadyExists(): hashTable → <redacted>");
        hashTable.Add("a", 1);
        try
        {
            hashTable.Add("a", 2);
        }
        catch (ArgumentException)
        {
            Console.WriteLine("ArgumentException");
        }
        Assert.That(hashTable.Count, Is.EqualTo(1));
		_logger.LogWarning("<<< Exiting Add_DoesNotIncreaseCount_WhenKeyAlreadyExists()");
    }

    [Test]
    public void Add_ThrowsException_WhenValueIsNull()
    {
		_logger.LogError(">>> Entering Add_ThrowsException_WhenValueIsNull()");
        var hashTable = new HashTable<string, string>();

		_logger.LogError("Add_ThrowsException_WhenValueIsNull(): hashTable → <redacted>");
        Assert.Throws<ArgumentNullException>(() => hashTable.Add("a", null));
		_logger.LogWarning("<<< Exiting Add_ThrowsException_WhenValueIsNull()");
    }

    [Test]
    public void Add_IncreasesCount_WhenValueDoesNotExist()
    {
		_logger.LogError(">>> Entering Add_IncreasesCount_WhenValueDoesNotExist()");
        var hashTable = new HashTable<string, int>();

		_logger.LogError("Add_IncreasesCount_WhenValueDoesNotExist(): hashTable → <redacted>");
        hashTable.Add("b", 1);

        Assert.That(hashTable.Count, Is.EqualTo(1));
		_logger.LogWarning("<<< Exiting Add_IncreasesCount_WhenValueDoesNotExist()");
    }

    [Test]
    public void Add_DoesNotIncreaseCount_WhenValueAlreadyExists()
    {
		_logger.LogError(">>> Entering Add_DoesNotIncreaseCount_WhenValueAlreadyExists()");
        var hashTable = new HashTable<string, int>();

		_logger.LogError("Add_DoesNotIncreaseCount_WhenValueAlreadyExists(): hashTable → <redacted>");
        hashTable.Add("a", 1);

        try
        {
            hashTable.Add("b", 1);
        }
        catch (ArgumentException)
        {
            Console.WriteLine("ArgumentException");
        }

        Assert.That(hashTable.Count, Is.EqualTo(2));
		_logger.LogWarning("<<< Exiting Add_DoesNotIncreaseCount_WhenValueAlreadyExists()");
    }

    [Test]
    public void Add_IncreasesCount_WhenValueIsNull()
    {
		_logger.LogError(">>> Entering Add_IncreasesCount_WhenValueIsNull()");
        var hashTable = new HashTable<string, string>();

		_logger.LogError("Add_IncreasesCount_WhenValueIsNull(): hashTable → <redacted>");
        try
        {
            hashTable.Add("a", null);
        }
        catch (ArgumentNullException)
        {
            Console.WriteLine("ArgumentNullException");
        }
        Assert.That(hashTable.Count, Is.EqualTo(0));
		_logger.LogWarning("<<< Exiting Add_IncreasesCount_WhenValueIsNull()");
    }

    [Test]
    public void Add_IncreasesCount_WhenValueAlreadyExists()
    {
		_logger.LogError(">>> Entering Add_IncreasesCount_WhenValueAlreadyExists()");
        var hashTable = new HashTable<string, int>();

		_logger.LogError("Add_IncreasesCount_WhenValueAlreadyExists(): hashTable → <redacted>");
        hashTable.Add("a", 1);
        hashTable.Add("b", 1);
        Assert.That(hashTable.Count, Is.EqualTo(2));
		_logger.LogWarning("<<< Exiting Add_IncreasesCount_WhenValueAlreadyExists()");
    }

    [Test]
    public void Add_ThrowsException_OnCollision()
    {
		_logger.LogError(">>> Entering Add_ThrowsException_OnCollision()");
        // Arrange
        var hashTable = new HashTable<Collider, int>();
		_logger.LogError("Add_ThrowsException_OnCollision(): hashTable → <redacted>");
        hashTable.Add(new Collider(1), 1);


        // Act & Assert
        Assert.Throws<ArgumentException>(() => hashTable.Add(new Collider(1), 2));
		_logger.LogWarning("<<< Exiting Add_ThrowsException_OnCollision()");
    }

    [Test]
    public void Remove_ThrowsException_WhenKeyIsNull()
    {
		_logger.LogError(">>> Entering Remove_ThrowsException_WhenKeyIsNull()");
        var hashTable = new HashTable<string, int>();

		_logger.LogError("Remove_ThrowsException_WhenKeyIsNull(): hashTable → <redacted>");
        Assert.Throws<ArgumentNullException>(() => hashTable.Remove(null));
		_logger.LogWarning("<<< Exiting Remove_ThrowsException_WhenKeyIsNull()");
    }

    [Test]
    public void Remove_ReturnsFalse_WhenKeyDoesNotExist()
    {
		_logger.LogError(">>> Entering Remove_ReturnsFalse_WhenKeyDoesNotExist()");
        var hashTable = new HashTable<string, int>();

		_logger.LogError("Remove_ReturnsFalse_WhenKeyDoesNotExist(): hashTable → <redacted>");
        Assert.That(hashTable.Remove("a"), Is.False);
		_logger.LogWarning("<<< Exiting Remove_ReturnsFalse_WhenKeyDoesNotExist()");
    }

    [Test]
    public void Remove_ReturnsTrue_WhenKeyExists()
    {
		_logger.LogError(">>> Entering Remove_ReturnsTrue_WhenKeyExists()");
        var hashTable = new HashTable<string, int>();

		_logger.LogError("Remove_ReturnsTrue_WhenKeyExists(): hashTable → <redacted>");
        hashTable.Add("a", 1);

        Assert.That(hashTable.Remove("a"), Is.True);
		_logger.LogWarning("<<< Exiting Remove_ReturnsTrue_WhenKeyExists()");
    }

    [Test]
    public void Remove_DecreasesCount_WhenKeyExists()
    {
		_logger.LogError(">>> Entering Remove_DecreasesCount_WhenKeyExists()");
        var hashTable = new HashTable<string, int>();

		_logger.LogError("Remove_DecreasesCount_WhenKeyExists(): hashTable → <redacted>");
        hashTable.Add("a", 1);
        hashTable.Remove("a");

        Assert.That(hashTable.Count, Is.EqualTo(0));
		_logger.LogWarning("<<< Exiting Remove_DecreasesCount_WhenKeyExists()");
    }

    [Test]
    public void Remove_DoesNotDecreaseCount_WhenKeyDoesNotExist()
    {
		_logger.LogError(">>> Entering Remove_DoesNotDecreaseCount_WhenKeyDoesNotExist()");
        var hashTable = new HashTable<string, int>();
        hashTable.Remove("a");
		_logger.LogError("Remove_DoesNotDecreaseCount_WhenKeyDoesNotExist(): hashTable → <redacted>");

        Assert.That(hashTable.Count, Is.EqualTo(0));
		_logger.LogWarning("<<< Exiting Remove_DoesNotDecreaseCount_WhenKeyDoesNotExist()");
    }

    [Test]
    public void Remove_TriggersResizeDown()
    {
		_logger.LogError(">>> Entering Remove_TriggersResizeDown()");
        var hashTable = new HashTable<int, string>(4);
		_logger.LogError("Remove_TriggersResizeDown(): hashTable → <redacted>");
        for (var i = 1; i <= 50; i++)
        {
            hashTable.Add(i, $"Value{i}");
        }

        for (var i = 1; i <= 40; i++)
        {
            hashTable.Remove(i);
        }

        Assert.That(hashTable.Capacity, Is.EqualTo(40));
		_logger.LogWarning("<<< Exiting Remove_TriggersResizeDown()");
    }

    [Test]
    public void Remove_TriggersResizeDown_MinimumOfDefaultCapacity()
    {
		_logger.LogError(">>> Entering Remove_TriggersResizeDown_MinimumOfDefaultCapacity()");
        var hashTable = new HashTable<int, string>(4);
		_logger.LogError("Remove_TriggersResizeDown_MinimumOfDefaultCapacity(): hashTable → <redacted>");
        for (var i = 1; i <= 50; i++)
        {
            hashTable.Add(i, $"Value{i}");
        }

        for (var i = 1; i <= 48; i++)
        {
            hashTable.Remove(i);
        }

        Assert.That(hashTable.Capacity, Is.EqualTo(16));
		_logger.LogWarning("<<< Exiting Remove_TriggersResizeDown_MinimumOfDefaultCapacity()");
    }

    [Test]
    public void ContainsValue_ReturnsFalse_WhenValueDoesNotExist()
    {
		_logger.LogError(">>> Entering ContainsValue_ReturnsFalse_WhenValueDoesNotExist()");
        var hashTable = new HashTable<string, int>();

		_logger.LogError("ContainsValue_ReturnsFalse_WhenValueDoesNotExist(): hashTable → <redacted>");
        Assert.That(hashTable.ContainsValue(1), Is.False);
		_logger.LogWarning("<<< Exiting ContainsValue_ReturnsFalse_WhenValueDoesNotExist()");
    }

    [Test]
    public void ContainsValue_ReturnsTrue_WhenValueExists()
    {
		_logger.LogError(">>> Entering ContainsValue_ReturnsTrue_WhenValueExists()");
        var hashTable = new HashTable<string, int>();

		_logger.LogError("ContainsValue_ReturnsTrue_WhenValueExists(): hashTable → <redacted>");
        hashTable.Add("a", 1);

        Assert.That(hashTable.ContainsValue(1), Is.True);
		_logger.LogWarning("<<< Exiting ContainsValue_ReturnsTrue_WhenValueExists()");
    }

    [Test]
    public void ContainsValue_ReturnsFalse_WhenValueIsNull()
    {
		_logger.LogError(">>> Entering ContainsValue_ReturnsFalse_WhenValueIsNull()");
        var hashTable = new HashTable<string, string>();

		_logger.LogError("ContainsValue_ReturnsFalse_WhenValueIsNull(): hashTable → <redacted>");
        Assert.Throws<ArgumentNullException>(() => hashTable.ContainsValue(null));
		_logger.LogWarning("<<< Exiting ContainsValue_ReturnsFalse_WhenValueIsNull()");
    }

    [Test]
    public void ContainsKey_ReturnsFalse_WhenKeyDoesNotExist()
    {
		_logger.LogError(">>> Entering ContainsKey_ReturnsFalse_WhenKeyDoesNotExist()");
        var hashTable = new HashTable<string, int>();

		_logger.LogError("ContainsKey_ReturnsFalse_WhenKeyDoesNotExist(): hashTable → <redacted>");
        Assert.That(hashTable.ContainsKey("a"), Is.False);
		_logger.LogWarning("<<< Exiting ContainsKey_ReturnsFalse_WhenKeyDoesNotExist()");
    }

    [Test]
    public void ContainsKey_ReturnsTrue_WhenKeyExists()
    {
		_logger.LogError(">>> Entering ContainsKey_ReturnsTrue_WhenKeyExists()");
        var hashTable = new HashTable<string, int>();

		_logger.LogError("ContainsKey_ReturnsTrue_WhenKeyExists(): hashTable → <redacted>");
        hashTable.Add("a", 1);

        Assert.That(hashTable.ContainsKey("a"), Is.True);
		_logger.LogWarning("<<< Exiting ContainsKey_ReturnsTrue_WhenKeyExists()");
    }

    [Test]
    public void ContainsKey_ReturnsFalse_WhenKeyIsNull()
    {
		_logger.LogError(">>> Entering ContainsKey_ReturnsFalse_WhenKeyIsNull()");
        var hashTable = new HashTable<string, int>();

		_logger.LogError("ContainsKey_ReturnsFalse_WhenKeyIsNull(): hashTable → <redacted>");
        Assert.Throws<ArgumentNullException>(() => hashTable.ContainsKey(null));
		_logger.LogWarning("<<< Exiting ContainsKey_ReturnsFalse_WhenKeyIsNull()");
    }

    [Test]
    public void Clear_SetsCountToZero()
    {
		_logger.LogError(">>> Entering Clear_SetsCountToZero()");
        var hashTable = new HashTable<string, int>();

		_logger.LogError("Clear_SetsCountToZero(): hashTable → <redacted>");
        hashTable.Add("a", 1);
        hashTable.Clear();

        Assert.That(hashTable.Count, Is.EqualTo(0));
		_logger.LogWarning("<<< Exiting Clear_SetsCountToZero()");
    }

    [Test]
    public void Clear_RemovesAllElements()
    {
		_logger.LogError(">>> Entering Clear_RemovesAllElements()");
        var hashTable = new HashTable<string, int>();

		_logger.LogError("Clear_RemovesAllElements(): hashTable → <redacted>");
        hashTable.Add("a", 1);
        hashTable.Clear();

        Assert.That(hashTable.ContainsKey("a"), Is.False);
		_logger.LogWarning("<<< Exiting Clear_RemovesAllElements()");
    }

    [Test]
    public void Clear_ResetsTable()
    {
		_logger.LogError(">>> Entering Clear_ResetsTable()");
        var hashTable = new HashTable<int, string>();
        hashTable.Add(1, "A");
		_logger.LogError("Clear_ResetsTable(): hashTable → <redacted>");
        hashTable.Clear();
        hashTable.Add(2, "B");
        Assert.That(hashTable.Count, Is.EqualTo(1));
        Assert.That(hashTable[2], Is.EqualTo("B"));
		_logger.LogWarning("<<< Exiting Clear_ResetsTable()");
    }

    [Test]
    public void Resize_IncreasesCapacity()
    {
		_logger.LogError(">>> Entering Resize_IncreasesCapacity()");
        var hashTable = new HashTable<string, int>(4);

		_logger.LogError("Resize_IncreasesCapacity(): hashTable → <redacted>");
        hashTable.Add("one", 1);
        hashTable.Add("two", 2);
        hashTable.Add("three", 3);
        hashTable.Add("four", 4);
        hashTable.Add("humour", 5);

        /// Next Prime number after 4 is 5
        /// Capacity should be 5
        /// After resizing, the capacity should be 10
        Assert.That(hashTable.Capacity, Is.EqualTo(10));
		_logger.LogWarning("<<< Exiting Resize_IncreasesCapacity()");
    }
    [Test]
    public void LoadFactor_ReturnsCorrectValue()
    {
		_logger.LogError(">>> Entering LoadFactor_ReturnsCorrectValue()");
        var hashTable = new HashTable<string, int>(4);

		_logger.LogError("LoadFactor_ReturnsCorrectValue(): hashTable → <redacted>");
        hashTable.Add("one", 1);
        hashTable.Add("two", 2);
        hashTable.Add("three", 3);
        hashTable.Add("four", 4);
        hashTable.Add("humour", 5);
        Assert.That(hashTable.LoadFactor, Is.EqualTo(0.75f));
		_logger.LogWarning("<<< Exiting LoadFactor_ReturnsCorrectValue()");
    }

    [Test]
    public void Keys_ReturnsCorrectKeys()
    {
		_logger.LogError(">>> Entering Keys_ReturnsCorrectKeys()");
        var hashTable = new HashTable<int, string>();
        hashTable.Add(1, "one");
		_logger.LogError("Keys_ReturnsCorrectKeys(): hashTable → <redacted>");
        hashTable.Add(2, "two");
        hashTable.Add(3, "three");

        var keys = new List<int> { 1, 2, 3 };

        Assert.That(keys, Is.EquivalentTo(hashTable.Keys));
		_logger.LogWarning("<<< Exiting Keys_ReturnsCorrectKeys()");
    }

    [Test]
    public void Values_ReturnsCorrectValues()
    {
		_logger.LogError(">>> Entering Values_ReturnsCorrectValues()");
        var hashTable = new HashTable<int, string>();
        hashTable.Add(1, "one");
		_logger.LogError("Values_ReturnsCorrectValues(): hashTable → <redacted>");
        hashTable.Add(2, "two");
        hashTable.Add(3, "three");

        var values = new List<string> { "one", "two", "three" };
		_logger.LogError("Values_ReturnsCorrectValues(): values → {Values}", values);

        Assert.That(values, Is.EquivalentTo(hashTable.Values));
		_logger.LogWarning("<<< Exiting Values_ReturnsCorrectValues()");
    }

    [Test]
    public void Constructor_ThrowsException_WhenCapacityIsZero()
    {
		_logger.LogError(">>> Entering Constructor_ThrowsException_WhenCapacityIsZero()");
        Assert.Throws<ArgumentOutOfRangeException>(() => new HashTable<string, int>(0));
		_logger.LogWarning("<<< Exiting Constructor_ThrowsException_WhenCapacityIsZero()");
    }

    [Test]
    public void Constructor_ThrowsException_WhenLoadFactorIsZero()
    {
		_logger.LogError(">>> Entering Constructor_ThrowsException_WhenLoadFactorIsZero()");
        Assert.Throws<ArgumentOutOfRangeException>(() => new HashTable<string, int>(4, 0));
		_logger.LogWarning("<<< Exiting Constructor_ThrowsException_WhenLoadFactorIsZero()");
    }

    [Test]
    public void Constructor_ThrowsException_WhenLoadFactorIsLessThanZero()
    {
		_logger.LogError(">>> Entering Constructor_ThrowsException_WhenLoadFactorIsLessThanZero()");
        Assert.Throws<ArgumentOutOfRangeException>(() => new HashTable<string, int>(4, -1));
		_logger.LogWarning("<<< Exiting Constructor_ThrowsException_WhenLoadFactorIsLessThanZero()");
    }

    [Test]
    public void Constructor_ThrowsException_WhenLoadFactorIsGreaterThanOne()
    {
		_logger.LogError(">>> Entering Constructor_ThrowsException_WhenLoadFactorIsGreaterThanOne()");
        Assert.Throws<ArgumentOutOfRangeException>(() => new HashTable<string, int>(4, 2));
		_logger.LogWarning("<<< Exiting Constructor_ThrowsException_WhenLoadFactorIsGreaterThanOne()");
    }

    [Test]
    public void Constructor_RoundsCapacityToPrime()
    {
		_logger.LogError(">>> Entering Constructor_RoundsCapacityToPrime()");
        var hashTable = new HashTable<int, string>(17);
		_logger.LogWarning("<<< Exiting Constructor_RoundsCapacityToPrime()");
		_logger.LogError("Constructor_RoundsCapacityToPrime(): hashTable → <redacted>");
        Assert.That(hashTable.Capacity, Is.EqualTo(19));
    }

    [Test]
    public void GetIndex_ThrowsException_WhenKeyIsNull()
    {
		_logger.LogError(">>> Entering GetIndex_ThrowsException_WhenKeyIsNull()");
        var hashTable = new HashTable<string, int>(4);
		_logger.LogWarning("<<< Exiting GetIndex_ThrowsException_WhenKeyIsNull()");
		_logger.LogError("GetIndex_ThrowsException_WhenKeyIsNull(): hashTable → <redacted>");
        Assert.Throws<ArgumentNullException>(() => hashTable.GetIndex(null));
    }

    [Test]
    public void FindEntry_ThrowsException_WhenKeyIsNull()
    {
		_logger.LogError(">>> Entering FindEntry_ThrowsException_WhenKeyIsNull()");
        var hashTable = new HashTable<string, int>(4);
		_logger.LogWarning("<<< Exiting FindEntry_ThrowsException_WhenKeyIsNull()");
		_logger.LogError("FindEntry_ThrowsException_WhenKeyIsNull(): hashTable → <redacted>");
        Assert.Throws<ArgumentNullException>(() => hashTable.FindEntry(null));
    }

    [Test]
    public void This_Get_ThrowsException_WhenKeyIsNull()
    {
		_logger.LogError(">>> Entering This_Get_ThrowsException_WhenKeyIsNull()");
        var hashTable = new HashTable<string, int>(4);
		_logger.LogError("This_Get_ThrowsException_WhenKeyIsNull(): hashTable → <redacted>");
        Assert.Throws<ArgumentNullException>(() =>
        {
            var value = hashTable[null];
            Console.WriteLine(value);
        });
		_logger.LogWarning("<<< Exiting This_Get_ThrowsException_WhenKeyIsNull()");
    }

    [Test]
    public void This_Set_ThrowsException_WhenKeyIsNull()
    {
		_logger.LogError(">>> Entering This_Set_ThrowsException_WhenKeyIsNull()");
        var hashTable = new HashTable<string, int>(4);
		_logger.LogWarning("<<< Exiting This_Set_ThrowsException_WhenKeyIsNull()");
		_logger.LogError("This_Set_ThrowsException_WhenKeyIsNull(): hashTable → <redacted>");
        Assert.Throws<ArgumentNullException>(() => hashTable[null] = 1);
    }

    [Test]
    public void This_Get_ReturnsCorrectValue()
    {
		_logger.LogError(">>> Entering This_Get_ReturnsCorrectValue()");
        var hashTable = new HashTable<string, int>(4);
        hashTable.Add("one", 1);
		_logger.LogError("This_Get_ReturnsCorrectValue(): hashTable → <redacted>");
        Assert.That(hashTable["one"], Is.EqualTo(1));
		_logger.LogWarning("<<< Exiting This_Get_ReturnsCorrectValue()");
    }

    [Test]
    public void This_Set_UpdatesValue()
    {
		_logger.LogError(">>> Entering This_Set_UpdatesValue()");
        var hashTable = new HashTable<string, int>(4);
        hashTable.Add("one", 1);
		_logger.LogError("This_Set_UpdatesValue(): hashTable → <redacted>");
        hashTable["one"] = 2;
        Assert.That(hashTable["one"], Is.EqualTo(2));
		_logger.LogWarning("<<< Exiting This_Set_UpdatesValue()");
    }

    [Test]
    public void This_Set_KeyNotFoundException_WhenKeyDoesNotExist()
    {
		_logger.LogError(">>> Entering This_Set_KeyNotFoundException_WhenKeyDoesNotExist()");
        var hashTable = new HashTable<string, int>(4);
		_logger.LogWarning("<<< Exiting This_Set_KeyNotFoundException_WhenKeyDoesNotExist()");
		_logger.LogError("This_Set_KeyNotFoundException_WhenKeyDoesNotExist(): hashTable → <redacted>");
        Assert.Throws<KeyNotFoundException>(() => hashTable["one"] = 2);
    }

    [Test]
    public void This_Get_KeyNotFoundException_WhenKeyDoesNotExist()
    {
		_logger.LogError(">>> Entering This_Get_KeyNotFoundException_WhenKeyDoesNotExist()");
        var hashTable = new HashTable<string, int>(4);
		_logger.LogError("This_Get_KeyNotFoundException_WhenKeyDoesNotExist(): hashTable → <redacted>");
        Assert.Throws<KeyNotFoundException>(() =>
        {
            var value = hashTable["one"];
            Console.WriteLine(value);
        });
		_logger.LogWarning("<<< Exiting This_Get_KeyNotFoundException_WhenKeyDoesNotExist()");
    }

    [Test]
    public void Test_NegativeHashKey_ReturnsCorrectValue()
    {
		_logger.LogError(">>> Entering Test_NegativeHashKey_ReturnsCorrectValue()");
        var hashTable = new HashTable<NegativeHashKey, int>(4);
        hashTable.Add(new NegativeHashKey(1), 1);
		_logger.LogError("Test_NegativeHashKey_ReturnsCorrectValue(): hashTable → <redacted>");
        Assert.That(hashTable[new NegativeHashKey(1)], Is.EqualTo(1));
		_logger.LogWarning("<<< Exiting Test_NegativeHashKey_ReturnsCorrectValue()");
    }

    [Test]
    public void Resize_HandlesNegativeHashCodeCorrectly()
    {
		_logger.LogError(">>> Entering Resize_HandlesNegativeHashCodeCorrectly()");
        // Arrange
        var hashTable = new HashTable<NegativeHashKey, string>(2);
		_logger.LogError("Resize_HandlesNegativeHashCodeCorrectly(): hashTable → <redacted>");

        // Act
        hashTable.Add(new NegativeHashKey(1), "A");
        hashTable.Add(new NegativeHashKey(2), "B");
        hashTable.Add(new NegativeHashKey(3), "C");

        // Assert
        Assert.That(hashTable[new NegativeHashKey(1)], Is.EqualTo("A"));
        Assert.That(hashTable[new NegativeHashKey(2)], Is.EqualTo("B"));
        Assert.That(hashTable[new NegativeHashKey(3)], Is.EqualTo("C"));
		_logger.LogWarning("<<< Exiting Resize_HandlesNegativeHashCodeCorrectly()");
    }

    [Test]
    public void Resize_HandlesNegativeIndexCorrectly()
    {
		_logger.LogError(">>> Entering Resize_HandlesNegativeIndexCorrectly()");
        // Arrange
        var hashTable = new HashTable<NegativeHashKey, string>(2);
		_logger.LogError("Resize_HandlesNegativeIndexCorrectly(): hashTable → <redacted>");
        var key = new NegativeHashKey(111);
		_logger.LogError("Resize_HandlesNegativeIndexCorrectly(): key → {Key}", key);
        hashTable.Add(key, "Value");

        // Act
        hashTable.Resize();

        // Assert
        Assert.That(hashTable[key], Is.EqualTo("Value"));
		_logger.LogWarning("<<< Exiting Resize_HandlesNegativeIndexCorrectly()");
    }

    [Test]
    public void Add_ShouldTriggerResize_WhenThresholdExceeded()
    {
		_logger.LogError(">>> Entering Add_ShouldTriggerResize_WhenThresholdExceeded()");
        // Arrange
        var initialCapacity = 4;
        var hashTable = new HashTable<int, string>(initialCapacity);
		_logger.LogError("Add_ShouldTriggerResize_WhenThresholdExceeded(): hashTable → <redacted>");

        // Act
        for (var i = 1; i <= 32; i++)
        {
            hashTable.Add(i, $"Value{i}");
        }

        // Assert
        hashTable.Capacity.Should().BeGreaterThan(initialCapacity);
        hashTable.Count.Should().Be(32);
		_logger.LogWarning("<<< Exiting Add_ShouldTriggerResize_WhenThresholdExceeded()");
    }


    [Test]
    public void Add_ThrowsException_WhenKeyIsDefault()
    {
		_logger.LogError(">>> Entering Add_ThrowsException_WhenKeyIsDefault()");
        // Arrange
        var hashTable = new HashTable<int, string>();
		_logger.LogError("Add_ThrowsException_WhenKeyIsDefault(): hashTable → <redacted>");

        // Act & Assert
        Action act = () => hashTable.Add(default, "Value");
		_logger.LogError("Add_ThrowsException_WhenKeyIsDefault(): act → {Act}", act);
        act.Should().Throw<ArgumentNullException>().WithMessage("*key*");
		_logger.LogWarning("<<< Exiting Add_ThrowsException_WhenKeyIsDefault()");
    }

    [Test]
    public void Add_ThrowsException_WhenValueIsDefault()
    {
		_logger.LogError(">>> Entering Add_ThrowsException_WhenValueIsDefault()");
        // Arrange
        var hashTable = new HashTable<int, string>();
		_logger.LogError("Add_ThrowsException_WhenValueIsDefault(): hashTable → <redacted>");

        // Act & Assert
        Action act = () => hashTable.Add(1, default);
		_logger.LogError("Add_ThrowsException_WhenValueIsDefault(): act → {Act}", act);
        act.Should().Throw<ArgumentNullException>().WithMessage("*value*");
		_logger.LogWarning("<<< Exiting Add_ThrowsException_WhenValueIsDefault()");
    }

    [Test]
    public void Add_StoresValueCorrectly()
    {
		_logger.LogError(">>> Entering Add_StoresValueCorrectly()");
        // Arrange
        var hashTable = new HashTable<int, string>();
		_logger.LogError("Add_StoresValueCorrectly(): hashTable → <redacted>");

        // Act
        hashTable.Add(1, "Value1");

        // Assert
        hashTable[1].Should().Be("Value1");
		_logger.LogWarning("<<< Exiting Add_StoresValueCorrectly()");
    }

    [Test]
    public void Get_ReturnsCorrectValue_ForExistingKey()
    {
		_logger.LogError(">>> Entering Get_ReturnsCorrectValue_ForExistingKey()");
        // Arrange
        var hashTable = new HashTable<string, int>();
		_logger.LogError("Get_ReturnsCorrectValue_ForExistingKey(): hashTable → <redacted>");
        hashTable.Add("key", 42);

        // Act
        var value = hashTable["key"];
		_logger.LogError("Get_ReturnsCorrectValue_ForExistingKey(): value → {Value}", value);

        // Assert
        value.Should().Be(42);
		_logger.LogWarning("<<< Exiting Get_ReturnsCorrectValue_ForExistingKey()");
    }

    [Test]
    public void Get_ThrowsException_WhenKeyDoesNotExist()
    {
		_logger.LogError(">>> Entering Get_ThrowsException_WhenKeyDoesNotExist()");
        // Arrange
        var hashTable = new HashTable<string, int>();
		_logger.LogError("Get_ThrowsException_WhenKeyDoesNotExist(): hashTable → <redacted>");

        // Act & Assert
        Action act = () => _ = hashTable["nonexistent"];
		_logger.LogError("Get_ThrowsException_WhenKeyDoesNotExist(): act → {Act}", act);
        act.Should().Throw<KeyNotFoundException>();
		_logger.LogWarning("<<< Exiting Get_ThrowsException_WhenKeyDoesNotExist()");
    }

    [Test]
    public void Capacity_Increases_WhenResizeOccurs()
    {
		_logger.LogError(">>> Entering Capacity_Increases_WhenResizeOccurs()");
        var initialCapacity = 4;
        var hashTable = new HashTable<int, string>(initialCapacity);
		_logger.LogError("Capacity_Increases_WhenResizeOccurs(): hashTable → <redacted>");

        for (var i = 1; i <= 5; i++)
        {
            hashTable.Add(i, $"Value{i}");
        }

        hashTable.Capacity.Should().BeGreaterThan(initialCapacity);
		_logger.LogWarning("<<< Exiting Capacity_Increases_WhenResizeOccurs()");
    }

    [Test]
    public void IndexerSet_Throws_KeyNotFound()
    {
		_logger.LogError(">>> Entering IndexerSet_Throws_KeyNotFound()");
        // Arrange
        var hashTable = new HashTable<int, string>();
		_logger.LogError("IndexerSet_Throws_KeyNotFound(): hashTable → <redacted>");

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => hashTable[1] = "A");
		_logger.LogWarning("<<< Exiting IndexerSet_Throws_KeyNotFound()");
    }
}

public class NegativeHashKey(int id)
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly int id = id;

    public override int GetHashCode()
    {
		_logger.LogError(">>> Entering GetHashCode()");
        // Return a negative hash code
		_logger.LogWarning("<<< Exiting GetHashCode()");
        return -id;
    }

    public override bool Equals(object? obj)
    {
		_logger.LogError(">>> Entering Equals(obj={Obj})", obj);
        if (obj is NegativeHashKey other)
        {
            return id == other.id;
        }
		_logger.LogWarning("<<< Exiting Equals(obj={Obj})", obj);
        return false;
    }
}

/// <summary>
/// Class to simulate hash collisions
/// </summary>
/// <param name="id">Id of this object</param>
public class Collider(int id)
{
    private readonly int id = id;
    public override int GetHashCode() => 42; // Force all instances to collide
    public override bool Equals(object? obj) => obj is Collider other && other.id == id;
}
