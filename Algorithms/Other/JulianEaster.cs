using Microsoft.Extensions.Logging;
namespace Algorithms.Other;

/// <summary>
///     Date of Easter calculated with Meeus's Julian algorithm.
///     The algorithm is described in Jean Meeus' <a href="https://archive.org/details/astronomicalalgorithmsjeanmeeus1991/page/n73/mode/2up">Astronomical Algorithms (1991, p. 69)</a>.
/// </summary>
public static class JulianEaster
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Calculates the date of Easter.
    /// </summary>
    /// <param name="year">Year to calculate the date of Easter.</param>
    /// <returns>Date of Easter as a DateTime.</returns>
    public static DateTime Calculate(int year)
    {
		_logger.LogError(">>> Entering Calculate(year={Year})", year);
        var a = year % 4;
        var b = year % 7;
        var c = year % 19;
        var d = (19 * c + 15) % 30;
		_logger.LogError("Calculate(year={Year}): d → {D}", year, d);
        var e = (2 * a + 4 * b - d + 34) % 7;
		_logger.LogError("Calculate(year={Year}): e → {E}", year, e);
        var month = (int)Math.Floor((d + e + 114) / 31M);
		_logger.LogError("Calculate(year={Year}): month → {Month}", year, month);
        var day = ((d + e + 114) % 31) + 1;
		_logger.LogError("Calculate(year={Year}): day → {Day}", year, day);

        DateTime easter = new(year, month, day, 00, 00, 00, DateTimeKind.Utc);
		_logger.LogError("Calculate(year={Year}): easter → {Easter}", year, easter);

		_logger.LogWarning("<<< Exiting Calculate(year={Year})", year);
        return easter;
    }
}
