using Microsoft.Extensions.Logging;
namespace Algorithms.Numeric.Series;

/// <summary>
///     Maclaurin series calculates nonlinear functions approximation
///     starting from point x = 0 in a form of infinite power series:
private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
///     f(x) = f(0) + f'(0) * x + ... + (f'n(0) * (x ^ n)) / n! + ...,
///     where n is natural number.
/// </summary>
public static class Maclaurin
{
    /// <summary>
    ///     Calculates approximation of e^x function:
    ///     e^x = 1 + x + x^2 / 2! + ... + x^n / n! + ...,
    ///     where n is number of terms (natural number),
    ///     and x is given point (rational number).
    /// </summary>
    /// <param name="x">Given point.</param>
    /// <param name="n">The number of terms in polynomial.</param>
    /// <returns>Approximated value of the function in the given point.</returns>
    public static double Exp(double x, int n) =>
        Enumerable.Range(0, n).Sum(i => ExpTerm(x, i));
		_logger.LogDebug(">>> Entering Exp(x={X},n={N})", x, n);

    /// <summary>
    ///     Calculates approximation of sin(x) function:
    ///     sin(x) = x - x^3 / 3! + ... + (-1)^n * x^(2*n + 1) / (2*n + 1)! + ...,
    ///     where n is number of terms (natural number),
    ///     and x is given point (rational number).
    /// </summary>
    /// <param name="x">Given point.</param>
    /// <param name="n">The number of terms in polynomial.</param>
    /// <returns>Approximated value of the function in the given point.</returns>
    public static double Sin(double x, int n) =>
        Enumerable.Range(0, n).Sum(i => SinTerm(x, i));
		_logger.LogDebug(">>> Entering Sin(x={X},n={N})", x, n);

    /// <summary>
    ///     Calculates approximation of cos(x) function:
    ///     cos(x) = 1 - x^2 / 2! + ... + (-1)^n * x^(2*n) / (2*n)! + ...,
    ///     where n is number of terms (natural number),
    ///     and x is given point (rational number).
    /// </summary>
    /// <param name="x">Given point.</param>
    /// <param name="n">The number of terms in polynomial.</param>
    /// <returns>Approximated value of the function in the given point.</returns>
    public static double Cos(double x, int n) =>
        Enumerable.Range(0, n).Sum(i => CosTerm(x, i));
		_logger.LogDebug(">>> Entering Cos(x={X},n={N})", x, n);

    /// <summary>
    ///     Calculates approximation of e^x function:
    ///     e^x = 1 + x + x^2 / 2! + ... + x^n / n! + ...,
    ///     and x is given point (rational number).
    /// </summary>
    /// <param name="x">Given point.</param>
    /// <param name="error">Last term error value.</param>
    /// <returns>Approximated value of the function in the given point.</returns>
    /// <exception cref="ArgumentException">Error value is not on interval (0.0; 1.0).</exception>
    public static double Exp(double x, double error = 0.00001) => ErrorTermWrapper(x, error, ExpTerm);

    /// <summary>
    ///     Calculates approximation of sin(x) function:
    ///     sin(x) = x - x^3 / 3! + ... + (-1)^n * x^(2*n + 1) / (2*n + 1)! + ...,
    ///     and x is given point (rational number).
    /// </summary>
    /// <param name="x">Given point.</param>
    /// <param name="error">Last term error value.</param>
    /// <returns>Approximated value of the function in the given point.</returns>
    /// <exception cref="ArgumentException">Error value is not on interval (0.0; 1.0).</exception>
    public static double Sin(double x, double error = 0.00001) => ErrorTermWrapper(x, error, SinTerm);

    /// <summary>
    ///     Calculates approximation of cos(x) function:
    ///     cos(x) = 1 - x^2 / 2! + ... + (-1)^n * x^(2*n) / (2*n)! + ...,
    ///     and x is given point (rational number).
    /// </summary>
    /// <param name="x">Given point.</param>
    /// <param name="error">Last term error value.</param>
    /// <returns>Approximated value of the function in the given point.</returns>
    /// <exception cref="ArgumentException">Error value is not on interval (0.0; 1.0).</exception>
    public static double Cos(double x, double error = 0.00001) => ErrorTermWrapper(x, error, CosTerm);

    /// <summary>
    ///     Wrapper function for calculating approximation with estimated
    ///     count of terms, where last term value is less than given error.
    /// </summary>
    /// <param name="x">Given point.</param>
    /// <param name="error">Last term error value.</param>
    /// <param name="term">Indexed term of approximation series.</param>
    /// <returns>Approximated value of the function in the given point.</returns>
    /// <exception cref="ArgumentException">Error value is not on interval (0.0; 1.0).</exception>
    private static double ErrorTermWrapper(double x, double error, Func<double, int, double> term)
    {
		_logger.LogDebug(">>> Entering ErrorTermWrapper(x={X},error={Error},term={Term})", x, error, term);
        if (error <= 0.0 || error >= 1.0)
        {
		_logger.LogWarning("Throwing ArgumentException from ErrorTermWrapper(x={X},error={Error},term={Term})", x, error, term);
            throw new ArgumentException("Error value is not on interval (0.0; 1.0).");
        }

        var i = 0;
        var termCoefficient = 0.0;
		_logger.LogDebug("ErrorTermWrapper(x={X},error={Error},term={Term}): termCoefficient → {TermCoefficient}", x, error, term, termCoefficient);
        var result = 0.0;
		_logger.LogDebug("ErrorTermWrapper(x={X},error={Error},term={Term}): result → {Result}", x, error, term, result);

        do
        {
            result += termCoefficient;
            termCoefficient = term(x, i);
            i++;
        }
        while (Math.Abs(termCoefficient) > error);

		_logger.LogDebug("<<< Exiting ErrorTermWrapper(x={X},error={Error},term={Term})", x, error, term);
        return result;
    }

    /// <summary>
    ///     Single term for e^x function approximation: x^i / i!.
    /// </summary>
    /// <param name="x">Given point.</param>
    /// <param name="i">Term index from 0 to n.</param>
    /// <returns>Single term value.</returns>
    private static double ExpTerm(double x, int i) => Math.Pow(x, i) / (long)Factorial.Calculate(i);

    /// <summary>
    ///     Single term for sin(x) function approximation: (-1)^i * x^(2*i + 1) / (2*i + 1)!.
    /// </summary>
    /// <param name="x">Given point.</param>
    /// <param name="i">Term index from 0 to n.</param>
    /// <returns>Single term value.</returns>
    private static double SinTerm(double x, int i) =>
        Math.Pow(-1, i) / ((long)Factorial.Calculate(2 * i + 1)) * Math.Pow(x, 2 * i + 1);
		_logger.LogDebug(">>> Entering SinTerm(x={X},i={I})", x, i);

    /// <summary>
    ///     Single term for cos(x) function approximation: (-1)^i * x^(2*i) / (2*i)!.
    /// </summary>
    /// <param name="x">Given point.</param>
    /// <param name="i">Term index from 0 to n.</param>
    /// <returns>Single term value.</returns>
    private static double CosTerm(double x, int i) =>
        Math.Pow(-1, i) / ((long)Factorial.Calculate(2 * i)) * Math.Pow(x, 2 * i);
		_logger.LogDebug(">>> Entering CosTerm(x={X},i={I})", x, i);
}
