using Microsoft.Extensions.Logging;
namespace Algorithms.Sorters.Integer;

/// <summary>
///     Class that implements bucket sort algorithm.
/// </summary>
public class BucketSorter : IIntegerSorter
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private const int NumOfDigitsInBase10 = 10;

    /// <summary>
    ///     Sorts array elements using BucketSort Algorithm.
    /// </summary>
    /// <param name="array">Array to sort.</param>
    public void Sort(int[] array)
    {
		_logger.LogError(">>> Entering Sort(array={Array})", array);
        if (array.Length <= 1)
        {
            return;
        }

        // store maximum number of digits in numbers to sort
        var totalDigits = NumberOfDigits(array);
		_logger.LogError("Sort(array={Array}): totalDigits → {TotalDigits}", array, totalDigits);

        // bucket array where numbers will be placed
        var buckets = new int[NumOfDigitsInBase10, array.Length + 1];

        // go through all digit places and sort each number
        // according to digit place value
        for (var pass = 1; pass <= totalDigits; pass++)
        {
            DistributeElements(array, buckets, pass); // distribution pass
            CollectElements(array, buckets); // gathering pass

            if (pass != totalDigits)
            {
                EmptyBucket(buckets); // set size of buckets to 0
            }
        }
		_logger.LogWarning("<<< Exiting Sort(array={Array})", array);
    }

    /// <summary>
    ///     Determines the number of digits in the largest number.
    /// </summary>
    /// <param name="array">Input array.</param>
    /// <returns>Number of digits.</returns>
    private static int NumberOfDigits(IEnumerable<int> array)
    {
        _logger.LogError(">>> Entering NumberOfDigits(array={Array})", array);
        _logger.LogWarning("<<< Exiting NumberOfDigits(array={Array})", array);
        return (int)Math.Floor(Math.Log10(array.Max()) + 1);
    }

    /// <summary>
    ///     To distribute elements into buckets based on specified digit.
    /// </summary>
    /// <param name="data">Input array.</param>
    /// <param name="buckets">Array of buckets.</param>
    /// <param name="digit">Digit.</param>
    private static void DistributeElements(IEnumerable<int> data, int[,] buckets, int digit)
    {
		_logger.LogError(">>> Entering DistributeElements(data={Data},buckets={Buckets},digit={Digit})", data, buckets, digit);
        // determine the divisor used to get specific digit
        var divisor = (int)Math.Pow(10, digit);
		_logger.LogError("DistributeElements(data={Data},buckets={Buckets},digit={Digit}): divisor → {Divisor}", data, buckets, digit, divisor);

        foreach (var element in data)
        {
            // bucketNumber example for hundreds digit:
            // ( 1234 % 1000 ) / 100 --> 2
            var bucketNumber = NumOfDigitsInBase10 * (element % divisor) / divisor;
			_logger.LogError("DistributeElements(data={Data},buckets={Buckets},digit={Digit}): bucketNumber → {BucketNumber}", data, buckets, digit, bucketNumber);

            // retrieve value in pail[ bucketNumber , 0 ] to
            // determine the location in row to store element
            var elementNumber = ++buckets[bucketNumber, 0]; // location in bucket to place element
            buckets[bucketNumber, elementNumber] = element;
        }
		_logger.LogWarning("<<< Exiting DistributeElements(data={Data},buckets={Buckets},digit={Digit})", data, buckets, digit);
    }

    /// <summary>
    ///     Return elements to original array.
    /// </summary>
    /// <param name="data">Input array.</param>
    /// <param name="buckets">Array of buckets.</param>
    private static void CollectElements(IList<int> data, int[,] buckets)
    {
		_logger.LogError(">>> Entering CollectElements(data={Data},buckets={Buckets})", data, buckets);
        var subscript = 0; // initialize location in data

        // loop over buckets
        for (var i = 0; i < NumOfDigitsInBase10; i++)
        {
            // loop over elements in each bucket
            for (var j = 1; j <= buckets[i, 0]; j++)
            {
                data[subscript++] = buckets[i, j]; // add element to array
            }
        }
		_logger.LogWarning("<<< Exiting CollectElements(data={Data},buckets={Buckets})", data, buckets);
    }

    /// <summary>
    ///     Sets size of all buckets to zero.
    /// </summary>
    /// <param name="buckets">Array of buckets.</param>
    private static void EmptyBucket(int[,] buckets)
    {
		_logger.LogError(">>> Entering EmptyBucket(buckets={Buckets})", buckets);
        for (var i = 0; i < NumOfDigitsInBase10; i++)
        {
            buckets[i, 0] = 0; // set size of bucket to 0
        }
		_logger.LogWarning("<<< Exiting EmptyBucket(buckets={Buckets})", buckets);
    }
}
