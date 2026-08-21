using Microsoft.Extensions.Logging;
namespace Algorithms.Numeric;

/// <summary>
///     Perform floor operation on a number.
/// </summary>
public static class Floor
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///    Returns the largest integer less than or equal to the number.
    /// </summary>
    /// <typeparam name="T">Type of number.</typeparam>
    /// <param name="inputNum">Number to find the floor of.</param>
    /// <returns>Floor value of the number.</returns>
    public static T FloorVal<T>(T inputNum) where T : INumber<T>
    {
		_logger.LogDebug(">>> Entering FloorVal(inputNum={InputNum})", inputNum);
        T intPart = T.CreateChecked(Convert.ToInt32(inputNum));

		_logger.LogDebug("FloorVal(inputNum={InputNum}): intPart → {IntPart}", inputNum, intPart);
		_logger.LogDebug("<<< Exiting FloorVal(inputNum={InputNum})", inputNum);
        return inputNum < intPart ? intPart - T.One : intPart;
    }
}
