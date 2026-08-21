using Microsoft.Extensions.Logging;
namespace Algorithms.Numeric;

/// <summary>
///     Perform ceiling operation on a number.
/// </summary>
public static class Ceil
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///    Returns the smallest integer greater than or equal to the number.
    /// </summary>
    /// <typeparam name="T">Type of number.</typeparam>
    /// <param name="inputNum">Number to find the ceiling of.</param>
    /// <returns>Ceiling value of the number.</returns>
    public static T CeilVal<T>(T inputNum) where T : INumber<T>
    {
		_logger.LogDebug(">>> Entering CeilVal(inputNum={InputNum})", inputNum);
        T intPart = T.CreateChecked(Convert.ToInt32(inputNum));

		_logger.LogDebug("CeilVal(inputNum={InputNum}): intPart → {IntPart}", inputNum, intPart);
		_logger.LogDebug("<<< Exiting CeilVal(inputNum={InputNum})", inputNum);
        return inputNum > intPart ? intPart + T.One : intPart;
    }
}
