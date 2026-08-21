using Microsoft.Extensions.Logging;
namespace DataStructures.Tests;

[TestFixture]
public class SortedListTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void Add_AddMultipleValues_SortingCorrectly(
        [Random(1, 1000, 100, Distinct = true)]
        int count)
    {
		_logger.LogDebug(">>> Entering Add_AddMultipleValues_SortingCorrectly(Distinct={Distinct})", Distinct);
        var values = GetValues(count);
        var list = new SortedList<int>();
		_logger.LogDebug("Add_AddMultipleValues_SortingCorrectly(Distinct={Distinct}): list → {List}", Distinct, list);
		_logger.LogDebug("Add_AddMultipleValues_SortingCorrectly(Distinct={Distinct}): values → {Values}", Distinct, values);

        foreach (var value in values)
        {
            list.Add(value);
        }

        Assert.That(list, Is.EqualTo(values.OrderBy(i => i)));
		_logger.LogDebug("<<< Exiting Add_AddMultipleValues_SortingCorrectly(Distinct={Distinct})", Distinct);
    }

    [Test]
    public void Contains_PositiveArrayAdded_NegativeNumberAsked_FalseReturned(
        [Random(1, 200, 10, Distinct = true)] int count)
    {
		_logger.LogDebug(">>> Entering Contains_PositiveArrayAdded_NegativeNumberAsked_FalseReturned(Distinct={Distinct})", Distinct);
        var values = GetValues(count);
        const int value = -1;
		_logger.LogDebug("Contains_PositiveArrayAdded_NegativeNumberAsked_FalseReturned(Distinct={Distinct}): values → {Values}", Distinct, values);

        var list = new SortedList<int>();
		_logger.LogDebug("Contains_PositiveArrayAdded_NegativeNumberAsked_FalseReturned(Distinct={Distinct}): list → {List}", Distinct, list);

        foreach (var i in values)
        {
            list.Add(i);
        }

        Assert.That(list.Contains(value), Is.False);
		_logger.LogDebug("<<< Exiting Contains_PositiveArrayAdded_NegativeNumberAsked_FalseReturned(Distinct={Distinct})", Distinct);
    }

    [Test]
    public void Contains_PositiveArrayAdded_ContainingValueAsked_TrueReturned(
        [Random(1, 200, 10, Distinct = true)] int count)
    {
		_logger.LogDebug(">>> Entering Contains_PositiveArrayAdded_ContainingValueAsked_TrueReturned(Distinct={Distinct})", Distinct);
        var values = GetValues(count);
        var value = values[TestContext.CurrentContext.Random.Next(count - 1)];
		_logger.LogDebug("Contains_PositiveArrayAdded_ContainingValueAsked_TrueReturned(Distinct={Distinct}): value → {Value}", Distinct, value);
		_logger.LogDebug("Contains_PositiveArrayAdded_ContainingValueAsked_TrueReturned(Distinct={Distinct}): values → {Values}", Distinct, values);

        var list = new SortedList<int>();
		_logger.LogDebug("Contains_PositiveArrayAdded_ContainingValueAsked_TrueReturned(Distinct={Distinct}): list → {List}", Distinct, list);

        foreach (var i in values)
        {
            list.Add(i);
        }

        Assert.That(list.Contains(value), Is.True);
		_logger.LogDebug("<<< Exiting Contains_PositiveArrayAdded_ContainingValueAsked_TrueReturned(Distinct={Distinct})", Distinct);
    }


    [Test]
    public void Remove_PositiveArrayAdded_NegativeNumberAsked_FalseReturned(
        [Random(1, 200, 10, Distinct = true)] int count)
    {
		_logger.LogDebug(">>> Entering Remove_PositiveArrayAdded_NegativeNumberAsked_FalseReturned(Distinct={Distinct})", Distinct);
        var values = GetValues(count);
        const int value = -1;
		_logger.LogDebug("Remove_PositiveArrayAdded_NegativeNumberAsked_FalseReturned(Distinct={Distinct}): values → {Values}", Distinct, values);

        var list = new SortedList<int>();
		_logger.LogDebug("Remove_PositiveArrayAdded_NegativeNumberAsked_FalseReturned(Distinct={Distinct}): list → {List}", Distinct, list);

        foreach (var i in values)
        {
            list.Add(i);
        }

        Assert.That(list.TryRemove(value), Is.False);
		_logger.LogDebug("<<< Exiting Remove_PositiveArrayAdded_NegativeNumberAsked_FalseReturned(Distinct={Distinct})", Distinct);
    }

    [Test]
    public void Remove_PositiveArrayAdded_ContainingValueAsked_TrueReturned(
        [Random(1, 200, 10, Distinct = true)] int count)
    {
		_logger.LogDebug(">>> Entering Remove_PositiveArrayAdded_ContainingValueAsked_TrueReturned(Distinct={Distinct})", Distinct);
        var values = GetValues(count);
        var value = values[TestContext.CurrentContext.Random.Next(count - 1)];
		_logger.LogDebug("Remove_PositiveArrayAdded_ContainingValueAsked_TrueReturned(Distinct={Distinct}): value → {Value}", Distinct, value);
		_logger.LogDebug("Remove_PositiveArrayAdded_ContainingValueAsked_TrueReturned(Distinct={Distinct}): values → {Values}", Distinct, values);

        var list = new SortedList<int>();
		_logger.LogDebug("Remove_PositiveArrayAdded_ContainingValueAsked_TrueReturned(Distinct={Distinct}): list → {List}", Distinct, list);

        foreach (var i in values)
        {
            list.Add(i);
        }

        var expectingValues = values
            .OrderBy(i => i)
            .ToList();

        expectingValues.Remove(value);

        Assert.That(list.TryRemove(value), Is.True);
        Assert.That(list, Is.EqualTo(expectingValues));
		_logger.LogDebug("<<< Exiting Remove_PositiveArrayAdded_ContainingValueAsked_TrueReturned(Distinct={Distinct})", Distinct);
    }

    [Test]
    public void Clear_ArrayAdded_ListCleaned_ListIsEmpty(
        [Random(1, 20, 1, Distinct = true)] int count)
    {
		_logger.LogDebug(">>> Entering Clear_ArrayAdded_ListCleaned_ListIsEmpty(Distinct={Distinct})", Distinct);
        var values = GetValues(count);

		_logger.LogDebug("Clear_ArrayAdded_ListCleaned_ListIsEmpty(Distinct={Distinct}): values → {Values}", Distinct, values);
        var list = new SortedList<int>();
		_logger.LogDebug("Clear_ArrayAdded_ListCleaned_ListIsEmpty(Distinct={Distinct}): list → {List}", Distinct, list);

        foreach (var i in values)
        {
            list.Add(i);
        }

        list.Clear();

        Assert.That(list, Is.Empty);
		_logger.LogDebug("<<< Exiting Clear_ArrayAdded_ListCleaned_ListIsEmpty(Distinct={Distinct})", Distinct);
    }

    private static List<int> GetValues(int count)
        => Enumerable
            .Range(0, count)
            .Select(_ => TestContext.CurrentContext.Random.Next(1_000_000))
            .ToList();
			_logger.LogDebug("<<< Exiting GetValues(count={Count})", count);
}
