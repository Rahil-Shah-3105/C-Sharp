using Microsoft.Extensions.Logging;
﻿namespace Algorithms;

public static class NewtonSquareRoot
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public static BigInteger Calculate(BigInteger number)
    {
		_logger.LogDebug(">>> Entering Calculate(number={Number})", number);
        if (number < 0)
        {
			_logger.LogWarning("Throwing ArgumentException from Calculate(number={Number})", number);
            throw new ArgumentException("Cannot calculate the square root of a negative number.");
        }

        if (number == 0)
        {
            return BigInteger.Zero;
        }

        var bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(number, 2)));
		_logger.LogDebug("Calculate(number={Number}): bitLength → {BitLength}", number, bitLength);
        BigInteger root = BigInteger.One << (bitLength / 2);
		_logger.LogWarning("Calculate(number={Number}): root → {Root}", number, root);

        while (!IsSquareRoot(number, root))
        {
            root += number / root;
            root /= 2;
        }

		_logger.LogDebug("<<< Exiting Calculate(number={Number})", number);
        return root;
    }

    private static bool IsSquareRoot(BigInteger number, BigInteger root)
    {
		_logger.LogDebug(">>> Entering IsSquareRoot(number={Number},root={Root})", number, root);
        var lowerBound = root * root;
		_logger.LogDebug("<<< Exiting IsSquareRoot(number={Number},root={Root})", number, root);
        return number >= lowerBound && number <= lowerBound + root + root;
    }
}
