using Microsoft.Extensions.Logging;
namespace Algorithms.Other;

/// <summary>
///     Fermat's prime tester https://en.wikipedia.org/wiki/Fermat_primality_test.
/// </summary>
public static class FermatPrimeChecker
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Checks if input number is a probable prime.
    /// </summary>
    /// <param name="numberToTest">Input number.</param>
    /// <param name="timesToCheck">Number of times to check.</param>
    /// <returns>True if is a prime; False otherwise.</returns>
    public static bool IsPrime(int numberToTest, int timesToCheck)
    {
		_logger.LogDebug(">>> Entering IsPrime(numberToTest={NumberToTest},timesToCheck={TimesToCheck})", numberToTest, timesToCheck);
        // You have to use BigInteger for two reasons:
        //   1. The pow operation between two int numbers usually overflows an int
        //   2. The pow and modular operation is very optimized
        var numberToTestBigInteger = new BigInteger(numberToTest);
		_logger.LogDebug("IsPrime(numberToTest={NumberToTest},timesToCheck={TimesToCheck}): numberToTestBigInteger → {NumberToTestBigInteger}", numberToTest, timesToCheck, numberToTestBigInteger);
        var exponentBigInteger = new BigInteger(numberToTest - 1);
		_logger.LogDebug("IsPrime(numberToTest={NumberToTest},timesToCheck={TimesToCheck}): exponentBigInteger → {ExponentBigInteger}", numberToTest, timesToCheck, exponentBigInteger);

        // Create a random number generator using the current time as seed
        var r = new Random(default(DateTime).Millisecond);
		_logger.LogDebug("IsPrime(numberToTest={NumberToTest},timesToCheck={TimesToCheck}): r → {R}", numberToTest, timesToCheck, r);

        var iterator = 1;
        var prime = true;
		_logger.LogDebug("IsPrime(numberToTest={NumberToTest},timesToCheck={TimesToCheck}): prime → {Prime}", numberToTest, timesToCheck, prime);

        while (iterator < timesToCheck && prime)
        {
            var randomNumber = r.Next(1, numberToTest);
			_logger.LogDebug("IsPrime(numberToTest={NumberToTest},timesToCheck={TimesToCheck}): randomNumber → {RandomNumber}", numberToTest, timesToCheck, randomNumber);
            var randomNumberBigInteger = new BigInteger(randomNumber);
			_logger.LogDebug("IsPrime(numberToTest={NumberToTest},timesToCheck={TimesToCheck}): randomNumberBigInteger → {RandomNumberBigInteger}", numberToTest, timesToCheck, randomNumberBigInteger);
            if (BigInteger.ModPow(randomNumberBigInteger, exponentBigInteger, numberToTestBigInteger) != 1)
            {
                prime = false;
            }

            iterator++;
        }

		_logger.LogDebug("<<< Exiting IsPrime(numberToTest={NumberToTest},timesToCheck={TimesToCheck})", numberToTest, timesToCheck);
        return prime;
    }
}
