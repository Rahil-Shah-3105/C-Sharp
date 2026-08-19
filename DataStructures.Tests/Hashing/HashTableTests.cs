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
		_logger.LogDebug(">>> Entering Add_ThrowsException_WhenKeyIsNull()");
        var hashTable = new HashTable<string, int>();

		_logger.LogDebug("Add_ThrowsException_WhenKeyIsNull(): hashTable → {HashTable}", hashTable);
        Assert.Throws<ArgumentNullException>(() => hashTable.Add(null, 1));
		_logger.LogDebug("<<< Exiting Add_ThrowsException_WhenKeyIsNull()");
    }

    [Test]
    public void Add_ThrowsException_WhenKeyAlreadyExists()
    {
		_logger.LogDebug(">>> Entering Add_ThrowsException_WhenKeyAlreadyExists()");
        var hashTable = new HashTable<string, int>();

		_logger.LogDebug("Add_ThrowsException_WhenKeyAlreadyExists(): hashTable → {HashTable}", hashTable);
        hashTable.Add("a", 1);

        Assert.Throws<ArgumentException>(() => hashTable.Add("a", 2));
		_logger.LogDebug("<<< Exiting Add_ThrowsException_WhenKeyAlreadyExists()");
    }

    [Test]
    public void Add_IncreasesCount_WhenKeyDoesNotExist()
    {
		_logger.LogDebug(">>> Entering Add_IncreasesCount_WhenKeyDoesNotExist()");
        var hashTable = new HashTable<string, int>();

		_logger.LogDebug("Add_IncreasesCount_WhenKeyDoesNotExist(): hashTable → {HashTable}", hashTable);
        hashTable.Add("a", 1);

        Assert.That(hashTable.Count, Is.EqualTo(1));
		_logger.LogDebug("<<< Exiting Add_IncreasesCount_WhenKeyDoesNotExist()");
    }

    [Test]
    public void Add_DoesNotIncreaseCount_WhenKeyAlreadyExists()
    {
		_logger.LogDebug(">>> Entering Add_DoesNotIncreaseCount_WhenKeyAlreadyExists()");
        var hashTable = new HashTable<string, int>();

		_logger.LogDebug("Add_DoesNotIncreaseCount_WhenKeyAlreadyExists(): hashTable → {HashTable}", hashTable);
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
		_logger.LogDebug("<<< Exiting Add_DoesNotIncreaseCount_WhenKeyAlreadyExists()");
    }

    [Test]
    public void Add_ThrowsException_WhenValueIsNull()
    {
		_logger.LogDebug(">>> Entering Add_ThrowsException_WhenValueIsNull()");
        var hashTable = new HashTable<string, string>();

		_logger.LogDebug("Add_ThrowsException_WhenValueIsNull(): hashTable → {HashTable}", hashTable);
        Assert.Throws<ArgumentNullException>(() => hashTable.Add("a", null));
		_logger.LogDebug("<<< Exiting Add_ThrowsException_WhenValueIsNull()");
    }

    [Test]
    public void Add_IncreasesCount_WhenValueDoesNotExist()
    {
		_logger.LogDebug(">>> Entering Add_IncreasesCount_WhenValueDoesNotExist()");
        var hashTable = new HashTable<string, int>();

		_logger.LogDebug("Add_IncreasesCount_WhenValueDoesNotExist(): hashTable → {HashTable}", hashTable);
        hashTable.Add("b", 1);

        Assert.That(hashTable.Count, Is.EqualTo(1));
		_logger.LogDebug("<<< Exiting Add_IncreasesCount_WhenValueDoesNotExist()");
    }

    [Test]
    public void Add_DoesNotIncreaseCount_WhenValueAlreadyExists()
    {
		_logger.LogDebug(">>> Entering Add_DoesNotIncreaseCount_WhenValueAlreadyExists()");
        var hashTable = new HashTable<string, int>();

		_logger.LogDebug("Add_DoesNotIncreaseCount_WhenValueAlreadyExists(): hashTable → {HashTable}", hashTable);
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
		_logger.LogDebug("<<< Exiting Add_DoesNotIncreaseCount_WhenValueAlreadyExists()");
    }

    [Test]
    public void Add_IncreasesCount_WhenValueIsNull()
    {
		_logger.LogDebug(">>> Entering Add_IncreasesCount_WhenValueIsNull()");
        var hashTable = new HashTable<string, string>();

		_logger.LogDebug("Add_IncreasesCount_WhenValueIsNull(): hashTable → {HashTable}", hashTable);
        try
        {
            hashTable.Add("a", null);
        }
        catch (ArgumentNullException)
        {
            Console.WriteLine("ArgumentNullException");
        }
        Assert.That(hashTable.Count, Is.EqualTo(0));
		_logger.LogDebug("<<< Exiting Add_IncreasesCount_WhenValueIsNull()");
    }

    [Test]
    public void Add_IncreasesCount_WhenValueAlreadyExists()
    {
		_logger.LogDebug(">>> Entering Add_IncreasesCount_WhenValueAlreadyExists()");
        var hashTable = new HashTable<string, int>();

		_logger.LogDebug("Add_IncreasesCount_WhenValueAlreadyExists(): hashTable → {HashTable}", hashTable);
        hashTable.Add("a", 1);
        hashTable.Add("b", 1);
        Assert.That(hashTable.Count, Is.EqualTo(2));
		_logger.LogDebug("<<< Exiting Add_IncreasesCount_WhenValueAlreadyExists()");
    }

    [Test]
    public void Add_ThrowsException_OnCollision()
    {
		_logger.LogDebug(">>> Entering Add_ThrowsException_OnCollision()");
        // Arrange
        var hashTable = new HashTable<Collider, int>();
		_logger.LogDebug("Add_ThrowsException_OnCollision(): hashTable → {HashTable}", hashTable);
        hashTable.Add(new Collider(1), 1);


        // Act & Assert
        Assert.Throws<ArgumentException>(() => hashTable.Add(new Collider(1), 2));
		_logger.LogDebug("<<< Exiting Add_ThrowsException_OnCollision()");
    }

    [Test]
    public void Remove_ThrowsException_WhenKeyIsNull()
    {
		_logger.LogDebug(">>> Entering Remove_ThrowsException_WhenKeyIsNull()");
        var hashTable = new HashTable<string, int>();

		_logger.LogDebug("Remove_ThrowsException_WhenKeyIsNull(): hashTable → {HashTable}", hashTable);
        Assert.Throws<ArgumentNullException>(() => hashTable.Remove(null));
		_logger.LogDebug("<<< Exiting Remove_ThrowsException_WhenKeyIsNull()");
    }

    [Test]
    public void Remove_ReturnsFalse_WhenKeyDoesNotExist()
    {
		_logger.LogDebug(">>> Entering Remove_ReturnsFalse_WhenKeyDoesNotExist()");
        var hashTable = new HashTable<string, int>();

		_logger.LogDebug("Remove_ReturnsFalse_WhenKeyDoesNotExist(): hashTable → {HashTable}", hashTable);
        Assert.That(hashTable.Remove("a"), Is.False);
		_logger.LogDebug("<<< Exiting Remove_ReturnsFalse_WhenKeyDoesNotExist()");
    }

    [Test]
    public void Remove_ReturnsTrue_WhenKeyExists()
    {
		_logger.LogDebug(">>> Entering Remove_ReturnsTrue_WhenKeyExists()");
        var hashTable = new HashTable<string, int>();

		_logger.LogDebug("Remove_ReturnsTrue_WhenKeyExists(): hashTable → {HashTable}", hashTable);
        hashTable.Add("a", 1);

        Assert.That(hashTable.Remove("a"), Is.True);
		_logger.LogDebug("<<< Exiting Remove_ReturnsTrue_WhenKeyExists()");
    }

    [Test]
    public void Remove_DecreasesCount_WhenKeyExists()
    {
		_logger.LogDebug(">>> Entering Remove_DecreasesCount_WhenKeyExists()");
        var hashTable = new HashTable<string, int>();

		_logger.LogDebug("Remove_DecreasesCount_WhenKeyExists(): hashTable → {HashTable}", hashTable);
        hashTable.Add("a", 1);
        hashTable.Remove("a");

        Assert.That(hashTable.Count, Is.EqualTo(0));
		_logger.LogDebug("<<< Exiting Remove_DecreasesCount_WhenKeyExists()");
    }

    [Test]
    public void Remove_DoesNotDecreaseCount_WhenKeyDoesNotExist()
    {
		_logger.LogDebug(">>> Entering Remove_DoesNotDecreaseCount_WhenKeyDoesNotExist()");
        var hashTable = new HashTable<string, int>();
        hashTable.Remove("a");
		_logger.LogDebug("Remove_DoesNotDecreaseCount_WhenKeyDoesNotExist(): hashTable → {HashTable}", hashTable);

        Assert.That(hashTable.Count, Is.EqualTo(0));
		_logger.LogDebug("<<< Exiting Remove_DoesNotDecreaseCount_WhenKeyDoesNotExist()");
    }

    [Test]
    public void Remove_TriggersResizeDown()
    {
		_logger.LogDebug(">>> Entering Remove_TriggersResizeDown()");
        var hashTable = new HashTable<int, string>(4);
        for (var i = 1; i <= 50; i++)
        {
            hashTable.Add(i, $"Value{i}");
        }

        for (var i = 1; i <= 40; i++)
        {
            hashTable.Remove(i);
        }

        Assert.That(hashTable.Capacity, Is.EqualTo(40));
		_logger.LogDebug("<<< Exiting Remove_TriggersResizeDown()");
    }

    [Test]
    public void Remove_TriggersResizeDown_MinimumOfDefaultCapacity()
    {
		_logger.LogDebug(">>> Entering Remove_TriggersResizeDown_MinimumOfDefaultCapacity()");
        var hashTable = new HashTable<int, string>(4);
        for (var i = 1; i <= 50; i++)
        {
            hashTable.Add(i, $"Value{i}");
        }

        for (var i = 1; i <= 48; i++)
        {
            hashTable.Remove(i);
        }

        Assert.That(hashTable.Capacity, Is.EqualTo(16));
		_logger.LogDebug("<<< Exiting Remove_TriggersResizeDown_MinimumOfDefaultCapacity()");
    }

    [Test]
    public void ContainsValue_ReturnsFalse_WhenValueDoesNotExist()
    {
		_logger.LogDebug(">>> Entering ContainsValue_ReturnsFalse_WhenValueDoesNotExist()");
        var hashTable = new HashTable<string, int>();

		_logger.LogDebug("ContainsValue_ReturnsFalse_WhenValueDoesNotExist(): hashTable → {HashTable}", hashTable);
        Assert.That(hashTable.ContainsValue(1), Is.False);
		_logger.LogDebug("<<< Exiting ContainsValue_ReturnsFalse_WhenValueDoesNotExist()");
    }

    [Test]
    public void ContainsValue_ReturnsTrue_WhenValueExists()
    {
		_logger.LogDebug(">>> Entering ContainsValue_ReturnsTrue_WhenValueExists()");
        var hashTable = new HashTable<string, int>();

		_logger.LogDebug("ContainsValue_ReturnsTrue_WhenValueExists(): hashTable → {HashTable}", hashTable);
        hashTable.Add("a", 1);

        Assert.That(hashTable.ContainsValue(1), Is.True);
		_logger.LogDebug("<<< Exiting ContainsValue_ReturnsTrue_WhenValueExists()");
    }

    [Test]
    public void ContainsValue_ReturnsFalse_WhenValueIsNull()
    {
		_logger.LogDebug(">>> Entering ContainsValue_ReturnsFalse_WhenValueIsNull()");
        var hashTable = new HashTable<string, string>();

		_logger.LogDebug("ContainsValue_ReturnsFalse_WhenValueIsNull(): hashTable → {HashTable}", hashTable);
        Assert.Throws<ArgumentNullException>(() => hashTable.ContainsValue(null));
		_logger.LogDebug("<<< Exiting ContainsValue_ReturnsFalse_WhenValueIsNull()");
    }

    [Test]
    public void ContainsKey_ReturnsFalse_WhenKeyDoesNotExist()
    {
		_logger.LogDebug(">>> Entering ContainsKey_ReturnsFalse_WhenKeyDoesNotExist()");
        var hashTable = new HashTable<string, int>();

		_logger.LogDebug("ContainsKey_ReturnsFalse_WhenKeyDoesNotExist(): hashTable → {HashTable}", hashTable);
        Assert.That(hashTable.ContainsKey("a"), Is.False);
		_logger.LogDebug("<<< Exiting ContainsKey_ReturnsFalse_WhenKeyDoesNotExist()");
    }

    [Test]
    public void ContainsKey_ReturnsTrue_WhenKeyExists()
    {
		_logger.LogDebug(">>> Entering ContainsKey_ReturnsTrue_WhenKeyExists()");
        var hashTable = new HashTable<string, int>();

		_logger.LogDebug("ContainsKey_ReturnsTrue_WhenKeyExists(): hashTable → {HashTable}", hashTable);
        hashTable.Add("a", 1);

        Assert.That(hashTable.ContainsKey("a"), Is.True);
		_logger.LogDebug("<<< Exiting ContainsKey_ReturnsTrue_WhenKeyExists()");
    }

    [Test]
    public void ContainsKey_ReturnsFalse_WhenKeyIsNull()
    {
		_logger.LogDebug(">>> Entering ContainsKey_ReturnsFalse_WhenKeyIsNull()");
        var hashTable = new HashTable<string, int>();

		_logger.LogDebug("ContainsKey_ReturnsFalse_WhenKeyIsNull(): hashTable → {HashTable}", hashTable);
        Assert.Throws<ArgumentNullException>(() => hashTable.ContainsKey(null));
		_logger.LogDebug("<<< Exiting ContainsKey_ReturnsFalse_WhenKeyIsNull()");
    }

    [Test]
    public void Clear_SetsCountToZero()
    {
		_logger.LogDebug(">>> Entering Clear_SetsCountToZero()");
        var hashTable = new HashTable<string, int>();

		_logger.LogDebug("Clear_SetsCountToZero(): hashTable → {HashTable}", hashTable);
        hashTable.Add("a", 1);
        hashTable.Clear();

        Assert.That(hashTable.Count, Is.EqualTo(0));
		_logger.LogDebug("<<< Exiting Clear_SetsCountToZero()");
    }

    [Test]
    public void Clear_RemovesAllElements()
    {
		_logger.LogDebug(">>> Entering Clear_RemovesAllElements()");
        var hashTable = new HashTable<string, int>();

		_logger.LogDebug("Clear_RemovesAllElements(): hashTable → {HashTable}", hashTable);
        hashTable.Add("a", 1);
        hashTable.Clear();

        Assert.That(hashTable.ContainsKey("a"), Is.False);
		_logger.LogDebug("<<< Exiting Clear_RemovesAllElements()");
    }

    [Test]
    public void Clear_ResetsTable()
    {
		_logger.LogDebug(">>> Entering Clear_ResetsTable()");
        var hashTable = new HashTable<int, string>();
        hashTable.Add(1, "A");
		_logger.LogDebug("Clear_ResetsTable(): hashTable → {HashTable}", hashTable);
        hashTable.Clear();
        hashTable.Add(2, "B");
        Assert.That(hashTable.Count, Is.EqualTo(1));
        Assert.That(hashTable[2], Is.EqualTo("B"));
		_logger.LogDebug("<<< Exiting Clear_ResetsTable()");
    }

    [Test]
    public void Resize_IncreasesCapacity()
    {
		_logger.LogDebug(">>> Entering Resize_IncreasesCapacity()");
        var hashTable = new HashTable<string, int>(4);

		_logger.LogDebug("Resize_IncreasesCapacity(): hashTable → {HashTable}", hashTable);
        hashTable.Add("one", 1);
        hashTable.Add("two", 2);
        hashTable.Add("three", 3);
        hashTable.Add("four", 4);
        hashTable.Add("humour", 5);

        /// Next Prime number after 4 is 5
        /// Capacity should be 5
        /// After resizing, the capacity should be 10
        Assert.That(hashTable.Capacity, Is.EqualTo(10));
		_logger.LogDebug("<<< Exiting Resize_IncreasesCapacity()");
    }
    [Test]
    public void LoadFactor_ReturnsCorrectValue()
    {
		_logger.LogDebug(">>> Entering LoadFactor_ReturnsCorrectValue()");
        var hashTable = new HashTable<string, int>(4);

		_logger.LogDebug("LoadFactor_ReturnsCorrectValue(): hashTable → {HashTable}", hashTable);
        hashTable.Add("one", 1);
        hashTable.Add("two", 2);
        hashTable.Add("three", 3);
        hashTable.Add("four", 4);
        hashTable.Add("humour", 5);
        Assert.That(hashTable.LoadFactor, Is.EqualTo(0.75f));
		_logger.LogDebug("<<< Exiting LoadFactor_ReturnsCorrectValue()");
    }

    [Test]
    public void Keys_ReturnsCorrectKeys()
    {
		_logger.LogDebug(">>> Entering Keys_ReturnsCorrectKeys()");
        var hashTable = new HashTable<int, string>();
        hashTable.Add(1, "one");
		_logger.LogDebug("Keys_ReturnsCorrectKeys(): hashTable → {HashTable}", hashTable);
        hashTable.Add(2, "two");
        hashTable.Add(3, "three");

        var keys = new List<int> { 1, 2, 3 };

        Assert.That(keys, Is.EquivalentTo(hashTable.Keys));
		_logger.LogDebug("<<< Exiting Keys_ReturnsCorrectKeys()");
    }

    [Test]
    public void Values_ReturnsCorrectValues()
    {
		_logger.LogDebug(">>> Entering Values_ReturnsCorrectValues()");
        var hashTable = new HashTable<int, string>();
        hashTable.Add(1, "one");
		_logger.LogDebug("Values_ReturnsCorrectValues(): hashTable → {HashTable}", hashTable);
        hashTable.Add(2, "two");
        hashTable.Add(3, "three");

        var values = new List<string> { "one", "two", "three" };
		_logger.LogDebug("Values_ReturnsCorrectValues(): values → {Values}", values);

        Assert.That(values, Is.EquivalentTo(hashTable.Values));
		_logger.LogDebug("<<< Exiting Values_ReturnsCorrectValues()");
    }

    [Test]
    public void Constructor_ThrowsException_WhenCapacityIsZero()
    {
		_logger.LogDebug(">>> Entering Constructor_ThrowsException_WhenCapacityIsZero()");
        Assert.Throws<ArgumentOutOfRangeException>(() => new HashTable<string, int>(0));
		_logger.LogDebug("<<< Exiting Constructor_ThrowsException_WhenCapacityIsZero()");
    }

    [Test]
    public void Constructor_ThrowsException_WhenLoadFactorIsZero()
    {
		_logger.LogDebug(">>> Entering Constructor_ThrowsException_WhenLoadFactorIsZero()");
        Assert.Throws<ArgumentOutOfRangeException>(() => new HashTable<string, int>(4, 0));
		_logger.LogDebug("<<< Exiting Constructor_ThrowsException_WhenLoadFactorIsZero()");
    }

    [Test]
    public void Constructor_ThrowsException_WhenLoadFactorIsLessThanZero()
    {
		_logger.LogDebug(">>> Entering Constructor_ThrowsException_WhenLoadFactorIsLessThanZero()");
        Assert.Throws<ArgumentOutOfRangeException>(() => new HashTable<string, int>(4, -1));
		_logger.LogDebug("<<< Exiting Constructor_ThrowsException_WhenLoadFactorIsLessThanZero()");
    }

    [Test]
    public void Constructor_ThrowsException_WhenLoadFactorIsGreaterThanOne()
    {
		_logger.LogDebug(">>> Entering Constructor_ThrowsException_WhenLoadFactorIsGreaterThanOne()");
        Assert.Throws<ArgumentOutOfRangeException>(() => new HashTable<string, int>(4, 2));
		_logger.LogDebug("<<< Exiting Constructor_ThrowsException_WhenLoadFactorIsGreaterThanOne()");
    }

    [Test]
    public void Constructor_RoundsCapacityToPrime()
    {
		_logger.LogDebug(">>> Entering Constructor_RoundsCapacityToPrime()");
        var hashTable = new HashTable<int, string>(17);
        Assert.That(hashTable.Capacity, Is.EqualTo(19));
		_logger.LogDebug("Constructor_RoundsCapacityToPrime(): hashTable → {HashTable}", hashTable);
		_logger.LogDebug("<<< Exiting Constructor_RoundsCapacityToPrime()");
    }

    [Test]
    public void GetIndex_ThrowsException_WhenKeyIsNull()
    {
		_logger.LogDebug(">>> Entering GetIndex_ThrowsException_WhenKeyIsNull()");
        var hashTable = new HashTable<string, int>(4);
        Assert.Throws<ArgumentNullException>(() => hashTable.GetIndex(null));
		_logger.LogDebug("GetIndex_ThrowsException_WhenKeyIsNull(): hashTable → {HashTable}", hashTable);
		_logger.LogDebug("<<< Exiting GetIndex_ThrowsException_WhenKeyIsNull()");
    }

    [Test]
    public void FindEntry_ThrowsException_WhenKeyIsNull()
    {
		_logger.LogDebug(">>> Entering FindEntry_ThrowsException_WhenKeyIsNull()");
        var hashTable = new HashTable<string, int>(4);
        Assert.Throws<ArgumentNullException>(() => hashTable.FindEntry(null));
		_logger.LogDebug("FindEntry_ThrowsException_WhenKeyIsNull(): hashTable → {HashTable}", hashTable);
		_logger.LogDebug("<<< Exiting FindEntry_ThrowsException_WhenKeyIsNull()");
    }

    [Test]
    public void This_Get_ThrowsException_WhenKeyIsNull()
    {
		_logger.LogDebug(">>> Entering This_Get_ThrowsException_WhenKeyIsNull()");
        var hashTable = new HashTable<string, int>(4);
        Assert.Throws<ArgumentNullException>(() =>
        {
			_logger.LogDebug("This_Get_ThrowsException_WhenKeyIsNull(): hashTable → {HashTable}", hashTable);
            var value = hashTable[null];
            Console.WriteLine(value);
        });
		_logger.LogDebug("<<< Exiting This_Get_ThrowsException_WhenKeyIsNull()");
    }

    [Test]
    public void This_Set_ThrowsException_WhenKeyIsNull()
    {
		_logger.LogDebug(">>> Entering This_Set_ThrowsException_WhenKeyIsNull()");
        var hashTable = new HashTable<string, int>(4);
        Assert.Throws<ArgumentNullException>(() => hashTable[null] = 1);
		_logger.LogDebug("This_Set_ThrowsException_WhenKeyIsNull(): hashTable → {HashTable}", hashTable);
		_logger.LogDebug("<<< Exiting This_Set_ThrowsException_WhenKeyIsNull()");
    }

    [Test]
    public void This_Get_ReturnsCorrectValue()
    {
		_logger.LogDebug(">>> Entering This_Get_ReturnsCorrectValue()");
        var hashTable = new HashTable<string, int>(4);
        hashTable.Add("one", 1);
		_logger.LogDebug("This_Get_ReturnsCorrectValue(): hashTable → {HashTable}", hashTable);
        Assert.That(hashTable["one"], Is.EqualTo(1));
		_logger.LogDebug("<<< Exiting This_Get_ReturnsCorrectValue()");
    }

    [Test]
    public void This_Set_UpdatesValue()
    {
		_logger.LogDebug(">>> Entering This_Set_UpdatesValue()");
        var hashTable = new HashTable<string, int>(4);
        hashTable.Add("one", 1);
		_logger.LogDebug("This_Set_UpdatesValue(): hashTable → {HashTable}", hashTable);
        hashTable["one"] = 2;
        Assert.That(hashTable["one"], Is.EqualTo(2));
		_logger.LogDebug("<<< Exiting This_Set_UpdatesValue()");
    }

    [Test]
    public void This_Set_KeyNotFoundException_WhenKeyDoesNotExist()
    {
		_logger.LogDebug(">>> Entering This_Set_KeyNotFoundException_WhenKeyDoesNotExist()");
        var hashTable = new HashTable<string, int>(4);
        Assert.Throws<KeyNotFoundException>(() => hashTable["one"] = 2);
		_logger.LogDebug("This_Set_KeyNotFoundException_WhenKeyDoesNotExist(): hashTable → {HashTable}", hashTable);
		_logger.LogDebug("<<< Exiting This_Set_KeyNotFoundException_WhenKeyDoesNotExist()");
    }

    [Test]
    public void This_Get_KeyNotFoundException_WhenKeyDoesNotExist()
    {
		_logger.LogDebug(">>> Entering This_Get_KeyNotFoundException_WhenKeyDoesNotExist()");
        var hashTable = new HashTable<string, int>(4);
        Assert.Throws<KeyNotFoundException>(() =>
        {
			_logger.LogDebug("This_Get_KeyNotFoundException_WhenKeyDoesNotExist(): hashTable → {HashTable}", hashTable);
            var value = hashTable["one"];
			_logger.LogDebug("This_Get_KeyNotFoundException_WhenKeyDoesNotExist(): value → {Value}", value);
            Console.WriteLine(value);
        });
		_logger.LogDebug("<<< Exiting This_Get_KeyNotFoundException_WhenKeyDoesNotExist()");
    }

    [Test]
    public void Test_NegativeHashKey_ReturnsCorrectValue()
    {
		_logger.LogDebug(">>> Entering Test_NegativeHashKey_ReturnsCorrectValue()");
        var hashTable = new HashTable<NegativeHashKey, int>(4);
        hashTable.Add(new NegativeHashKey(1), 1);
		_logger.LogDebug("Test_NegativeHashKey_ReturnsCorrectValue(): hashTable → {HashTable}", hashTable);
        Assert.That(hashTable[new NegativeHashKey(1)], Is.EqualTo(1));
		_logger.LogDebug("<<< Exiting Test_NegativeHashKey_ReturnsCorrectValue()");
    }

    [Test]
    public void Resize_HandlesNegativeHashCodeCorrectly()
    {
		_logger.LogDebug(">>> Entering Resize_HandlesNegativeHashCodeCorrectly()");
        // Arrange
        var hashTable = new HashTable<NegativeHashKey, string>(2);
		_logger.LogDebug("Resize_HandlesNegativeHashCodeCorrectly(): hashTable → {HashTable}", hashTable);

        // Act
        hashTable.Add(new NegativeHashKey(1), "A");
        hashTable.Add(new NegativeHashKey(2), "B");
        hashTable.Add(new NegativeHashKey(3), "C");

        // Assert
        Assert.That(hashTable[new NegativeHashKey(1)], Is.EqualTo("A"));
        Assert.That(hashTable[new NegativeHashKey(2)], Is.EqualTo("B"));
        Assert.That(hashTable[new NegativeHashKey(3)], Is.EqualTo("C"));
		_logger.LogDebug("<<< Exiting Resize_HandlesNegativeHashCodeCorrectly()");
    }

    [Test]
    public void Resize_HandlesNegativeIndexCorrectly()
    {
		_logger.LogDebug(">>> Entering Resize_HandlesNegativeIndexCorrectly()");
        // Arrange
        var hashTable = new HashTable<NegativeHashKey, string>(2);
		_logger.LogDebug("Resize_HandlesNegativeIndexCorrectly(): hashTable → {HashTable}", hashTable);
        var key = new NegativeHashKey(111);
		_logger.LogWarning("Resize_HandlesNegativeIndexCorrectly(): key → {Key}", key);
        hashTable.Add(key, "Value");

        // Act
        hashTable.Resize();

        // Assert
        Assert.That(hashTable[key], Is.EqualTo("Value"));
		_logger.LogDebug("<<< Exiting Resize_HandlesNegativeIndexCorrectly()");
    }

    [Test]
    public void Add_ShouldTriggerResize_WhenThresholdExceeded()
    {
		_logger.LogDebug(">>> Entering Add_ShouldTriggerResize_WhenThresholdExceeded()");
        // Arrange
        var initialCapacity = 4;
        var hashTable = new HashTable<int, string>(initialCapacity);
		_logger.LogDebug("Add_ShouldTriggerResize_WhenThresholdExceeded(): hashTable → {HashTable}", hashTable);

        // Act
        for (var i = 1; i <= 32; i++)
        {
            hashTable.Add(i, $"Value{i}");
        }

        // Assert
        hashTable.Capacity.Should().BeGreaterThan(initialCapacity);
        hashTable.Count.Should().Be(32);
		_logger.LogDebug("<<< Exiting Add_ShouldTriggerResize_WhenThresholdExceeded()");
    }


    [Test]
    public void Add_ThrowsException_WhenKeyIsDefault()
    {
		_logger.LogDebug(">>> Entering Add_ThrowsException_WhenKeyIsDefault()");
        // Arrange
        var hashTable = new HashTable<int, string>();
		_logger.LogDebug("Add_ThrowsException_WhenKeyIsDefault(): hashTable → {HashTable}", hashTable);

        // Act & Assert
        Action act = () => hashTable.Add(default, "Value");
		_logger.LogDebug("Add_ThrowsException_WhenKeyIsDefault(): act → {Act}", act);
        act.Should().Throw<ArgumentNullException>().WithMessage("*key*");
		_logger.LogDebug("<<< Exiting Add_ThrowsException_WhenKeyIsDefault()");
    }

    [Test]
    public void Add_ThrowsException_WhenValueIsDefault()
    {
		_logger.LogDebug(">>> Entering Add_ThrowsException_WhenValueIsDefault()");
        // Arrange
        var hashTable = new HashTable<int, string>();
		_logger.LogDebug("Add_ThrowsException_WhenValueIsDefault(): hashTable → {HashTable}", hashTable);

        // Act & Assert
        Action act = () => hashTable.Add(1, default);
		_logger.LogDebug("Add_ThrowsException_WhenValueIsDefault(): act → {Act}", act);
        act.Should().Throw<ArgumentNullException>().WithMessage("*value*");
		_logger.LogDebug("<<< Exiting Add_ThrowsException_WhenValueIsDefault()");
    }

    [Test]
    public void Add_StoresValueCorrectly()
    {
		_logger.LogDebug(">>> Entering Add_StoresValueCorrectly()");
        // Arrange
        var hashTable = new HashTable<int, string>();
		_logger.LogDebug("Add_StoresValueCorrectly(): hashTable → {HashTable}", hashTable);

        // Act
        hashTable.Add(1, "Value1");

        // Assert
        hashTable[1].Should().Be("Value1");
		_logger.LogDebug("<<< Exiting Add_StoresValueCorrectly()");
    }

    [Test]
    public void Get_ReturnsCorrectValue_ForExistingKey()
    {
		_logger.LogDebug(">>> Entering Get_ReturnsCorrectValue_ForExistingKey()");
        // Arrange
        var hashTable = new HashTable<string, int>();
		_logger.LogDebug("Get_ReturnsCorrectValue_ForExistingKey(): hashTable → {HashTable}", hashTable);
        hashTable.Add("key", 42);

        // Act
        var value = hashTable["key"];
		_logger.LogDebug("Get_ReturnsCorrectValue_ForExistingKey(): value → {Value}", value);

        // Assert
        value.Should().Be(42);
		_logger.LogDebug("<<< Exiting Get_ReturnsCorrectValue_ForExistingKey()");
    }

    [Test]
    public void Get_ThrowsException_WhenKeyDoesNotExist()
    {
		_logger.LogDebug(">>> Entering Get_ThrowsException_WhenKeyDoesNotExist()");
        // Arrange
        var hashTable = new HashTable<string, int>();
		_logger.LogDebug("Get_ThrowsException_WhenKeyDoesNotExist(): hashTable → {HashTable}", hashTable);

        // Act & Assert
        Action act = () => _ = hashTable["nonexistent"];
		_logger.LogDebug("Get_ThrowsException_WhenKeyDoesNotExist(): act → {Act}", act);
        act.Should().Throw<KeyNotFoundException>();
		_logger.LogDebug("<<< Exiting Get_ThrowsException_WhenKeyDoesNotExist()");
    }

    [Test]
    public void Capacity_Increases_WhenResizeOccurs()
    {
		_logger.LogDebug(">>> Entering Capacity_Increases_WhenResizeOccurs()");
        var initialCapacity = 4;
        var hashTable = new HashTable<int, string>(initialCapacity);
		_logger.LogDebug("Capacity_Increases_WhenResizeOccurs(): hashTable → {HashTable}", hashTable);

        for (var i = 1; i <= 5; i++)
        {
            hashTable.Add(i, $"Value{i}");
        }

        hashTable.Capacity.Should().BeGreaterThan(initialCapacity);
		_logger.LogDebug("<<< Exiting Capacity_Increases_WhenResizeOccurs()");
    }

    [Test]
    public void IndexerSet_Throws_KeyNotFound()
    {
		_logger.LogDebug(">>> Entering IndexerSet_Throws_KeyNotFound()");
        // Arrange
        var hashTable = new HashTable<int, string>();
		_logger.LogDebug("IndexerSet_Throws_KeyNotFound(): hashTable → {HashTable}", hashTable);

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => hashTable[1] = "A");
		_logger.LogDebug("<<< Exiting IndexerSet_Throws_KeyNotFound()");
    }
}

public class NegativeHashKey(int id)
{
    private readonly int id = id;

    public override int GetHashCode()
    {
		_logger.LogDebug(">>> Entering GetHashCode()");
        // Return a negative hash code
		_logger.LogDebug("<<< Exiting GetHashCode()");
        return -id;
    }

    public override bool Equals(object? obj)
    {
		_logger.LogDebug(">>> Entering Equals(obj={Obj})", obj);
        if (obj is NegativeHashKey other)
        {
			_logger.LogDebug("Equals(obj={Obj}): id → {Id}", obj, id);
		_logger.LogDebug("<<< Exiting Equals(obj={Obj})", obj);
            return id == other.id;
        }
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
