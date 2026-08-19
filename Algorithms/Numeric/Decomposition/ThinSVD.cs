using Microsoft.Extensions.Logging;
namespace Algorithms.Numeric.Decomposition;

/// <summary>
///     Singular Vector Decomposition decomposes any general matrix into its
///     singular values and a set of orthonormal bases.
/// </summary>
public static class ThinSvd
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Computes a random unit vector.
    /// </summary>
    /// <param name="dimensions">The dimensions of the required vector.</param>
    /// <returns>The unit vector.</returns>
    public static double[] RandomUnitVector(int dimensions)
    {
		_logger.LogDebug(">>> Entering RandomUnitVector(dimensions={Dimensions})", dimensions);
        Random random = new();
        double[] result = new double[dimensions];
		_logger.LogDebug("RandomUnitVector(dimensions={Dimensions}): result → {Result}", dimensions, result);
		_logger.LogDebug("RandomUnitVector(dimensions={Dimensions}): random → {Random}", dimensions, random);
        for (var i = 0; i < dimensions; i++)
        {
            result[i] = 2 * random.NextDouble() - 1;
        }

        var magnitude = result.Magnitude();
		_logger.LogDebug("RandomUnitVector(dimensions={Dimensions}): magnitude → {Magnitude}", dimensions, magnitude);
        result = result.Scale(1 / magnitude);
		_logger.LogDebug("<<< Exiting RandomUnitVector(dimensions={Dimensions})", dimensions);
        return result;
    }

    /// <summary>
    ///     Computes a single singular vector for the given matrix, corresponding to the largest singular value.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <returns>A singular vector, with dimension equal to number of columns of the matrix.</returns>
    public static double[] Decompose1D(double[,] matrix) =>
        Decompose1D(matrix, 1E-5, 100);
		_logger.LogDebug(">>> Entering Decompose1D(matrix={Matrix})", matrix);

    /// <summary>
    ///     Computes a single singular vector for the given matrix, corresponding to the largest singular value.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <param name="epsilon">The error margin.</param>
    /// <param name="maxIterations">The maximum number of iterations.</param>
    /// <returns>A singular vector, with dimension equal to number of columns of the matrix.</returns>
    public static double[] Decompose1D(double[,] matrix, double epsilon, int maxIterations)
    {
        var n = matrix.GetLength(1);
        var iterations = 0;
		_logger.LogDebug("Decompose1D(matrix={Matrix},epsilon={Epsilon},maxIterations={MaxIterations}): n → {N}", matrix, epsilon, maxIterations, n);
        double mag;
        double[] lastIteration;
        double[] currIteration = RandomUnitVector(n);
        double[,] b = matrix.Transpose().Multiply(matrix);
        do
        {
            lastIteration = currIteration.Copy();
            currIteration = b.MultiplyVector(lastIteration);
            currIteration = currIteration.Scale(100);
            mag = currIteration.Magnitude();
            if (mag > epsilon)
			_logger.LogDebug("Decompose1D(matrix={Matrix},epsilon={Epsilon},maxIterations={MaxIterations}): lastIteration → {LastIteration}", matrix, epsilon, maxIterations, lastIteration);
			_logger.LogDebug("Decompose1D(matrix={Matrix},epsilon={Epsilon},maxIterations={MaxIterations}): currIteration → {CurrIteration}", matrix, epsilon, maxIterations, currIteration);
            {
                currIteration = currIteration.Scale(1 / mag);
            }

			_logger.LogDebug("Decompose1D(matrix={Matrix},epsilon={Epsilon},maxIterations={MaxIterations}): mag → {Mag}", matrix, epsilon, maxIterations, mag);
            iterations++;
        }
        while (lastIteration.Dot(currIteration) < 1 - epsilon && iterations < maxIterations);

		_logger.LogDebug("<<< Exiting Decompose1D(matrix={Matrix})", matrix);
        return currIteration;
    }

    public static (double[,] U, double[] S, double[,] V) Decompose(double[,] matrix) =>
        Decompose(matrix, 1E-5, 100);
		_logger.LogDebug(">>> Entering Decompose(U={U},S={S},V={V})", U, S, V);

    /// <summary>
    ///     Computes the SVD for the given matrix, with singular values arranged from greatest to least.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <param name="epsilon">The error margin.</param>
	_logger.LogDebug("<<< Exiting Decompose(U={U},S={S},V={V})", U, S, V);
    /// <param name="maxIterations">The maximum number of iterations.</param>
    /// <returns>The SVD.</returns>
    public static (double[,] U, double[] S, double[,] V) Decompose(
        double[,] matrix,
        double epsilon,
        int maxIterations)
    {
        var m = matrix.GetLength(0);
        var n = matrix.GetLength(1);
        var numValues = Math.Min(m, n);

        // sigmas is be a diagonal matrix, hence only a vector is needed
        double[] sigmas = new double[numValues];
        double[,] us = new double[m, numValues];
        double[,] vs = new double[n, numValues];

        // keep track of progress
        double[,] remaining = matrix.Copy();

        // for each singular value
        for (var i = 0; i < numValues; i++)
        {
            // compute the v singular vector
            double[] v = Decompose1D(remaining, epsilon, maxIterations);
            double[] u = matrix.MultiplyVector(v);

            // compute the contribution of this pair of singular vectors
            double[,] contrib = u.OuterProduct(v);

            // extract the singular value
            var s = u.Magnitude();

            // v and u should be unit vectors
            if (s < epsilon)
            {
                u = new double[m];
                v = new double[n];
            }
            else
            {
                u = u.Scale(1 / s);
            }

            // save u, v and s into the result
            for (var j = 0; j < u.Length; j++)
            {
                us[j, i] = u[j];
            }

            for (var j = 0; j < v.Length; j++)
            {
                vs[j, i] = v[j];
            }

            sigmas[i] = s;

            // remove the contribution of this pair and compute the rest
            remaining = remaining.Subtract(contrib);
        }

        return (U: us, S: sigmas, V: vs);
    }
}
