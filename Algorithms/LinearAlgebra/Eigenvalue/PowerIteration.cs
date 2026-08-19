using Microsoft.Extensions.Logging;
namespace Algorithms.LinearAlgebra.Eigenvalue;

/// <summary>
///     Power iteration method - eigenvalue numeric algorithm, based on recurrent relation:
private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
///     Li+1 = (A * Li) / || A * Li ||, where Li - eigenvector approximation.
/// </summary>
public static class PowerIteration
{
    /// <summary>
    ///     Returns approximation of the dominant eigenvalue and eigenvector of <paramref name="source" /> matrix.
    /// </summary>
    /// <list type="bullet">
    ///     <item>
    ///         <description>The algorithm will not converge if the start vector is orthogonal to the eigenvector.</description>
    ///     </item>
    ///     <item>
    ///         <description>The <paramref name="source" /> matrix must be square-shaped.</description>
    ///     </item>
    /// </list>
    /// <param name="source">Source square-shaped matrix.</param>
    /// <param name="startVector">Start vector.</param>
    /// <param name="error">Accuracy of the result.</param>
    /// <returns>Dominant eigenvalue and eigenvector pair.</returns>
    /// <exception cref="ArgumentException">The <paramref name="source" /> matrix is not square-shaped.</exception>
    /// <exception cref="ArgumentException">The length of the start vector doesn't equal the size of the source matrix.</exception>
    public static (double Eigenvalue, double[] Eigenvector) Dominant(
        double[,] source,
        double[] startVector,
        double error = 0.00001)
    {
		_logger.LogDebug(">>> Entering Dominant(Eigenvalue={Eigenvalue},Eigenvector={Eigenvector})", Eigenvalue, Eigenvector);
        if (source.GetLength(0) != source.GetLength(1))
        {
			_logger.LogWarning("Throwing ArgumentException from Dominant(Eigenvalue={Eigenvalue},Eigenvector={Eigenvector})", Eigenvalue, Eigenvector);
            throw new ArgumentException("The source matrix is not square-shaped.");
        }

        if (source.GetLength(0) != startVector.Length)
        {
            throw new ArgumentException(
                "The length of the start vector doesn't equal the size of the source matrix.");
        }

        double eigenNorm;
        double[] previousEigenVector;
        double[] currentEigenVector = startVector;

        do
        {
            previousEigenVector = currentEigenVector;
			_logger.LogDebug("Dominant(Eigenvalue={Eigenvalue},Eigenvector={Eigenvector}): previousEigenVector → {PreviousEigenVector}", Eigenvalue, Eigenvector, previousEigenVector);
            currentEigenVector = source.Multiply(
                    previousEigenVector.ToColumnVector())
                .ToRowVector();
				_logger.LogDebug("Dominant(Eigenvalue={Eigenvalue},Eigenvector={Eigenvector}): currentEigenVector → {CurrentEigenVector}", Eigenvalue, Eigenvector, currentEigenVector);

            eigenNorm = currentEigenVector.Magnitude();
            currentEigenVector = currentEigenVector.Select(x => x / eigenNorm).ToArray();
        }
        while (Math.Abs(currentEigenVector.Dot(previousEigenVector)) < 1.0 - error);

        var eigenvalue = source.Multiply(currentEigenVector.ToColumnVector()).ToRowVector().Magnitude();
		_logger.LogDebug("Dominant(Eigenvalue={Eigenvalue},Eigenvector={Eigenvector}): eigenvalue → {Eigenvalue1}", Eigenvalue, Eigenvector, eigenvalue);

		_logger.LogDebug("<<< Exiting Dominant(Eigenvalue={Eigenvalue},Eigenvector={Eigenvector})", Eigenvalue, Eigenvector);
        return (eigenvalue, Eigenvector: currentEigenVector);
    }

    /// <summary>
    ///     Returns approximation of the dominant eigenvalue and eigenvector of <paramref name="source" /> matrix.
    ///     Random normalized vector is used as the start vector to decrease chance of orthogonality to the eigenvector.
    /// </summary>
    /// <list type="bullet">
    ///     <item>
    ///         <description>The algorithm will not converge if the start vector is orthogonal to the eigenvector.</description>
    ///     </item>
    ///     <item>
    ///         <description>The <paramref name="source" /> matrix should be square-shaped.</description>
    ///     </item>
    /// </list>
    /// <param name="source">Source square-shaped matrix.</param>
    /// <param name="error">Accuracy of the result.</param>
    /// <returns>Dominant eigenvalue and eigenvector pair.</returns>
    /// <exception cref="ArgumentException">The <paramref name="source" /> matrix is not square-shaped.</exception>
    /// <exception cref="ArgumentException">The length of the start vector doesn't equal the size of the source matrix.</exception>
    public static (double Eigenvalue, double[] Eigenvector) Dominant(double[,] source, double error = 0.00001) =>
        Dominant(source, new Random().NextVector(source.GetLength(1)), error);
}
