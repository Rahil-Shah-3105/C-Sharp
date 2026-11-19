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
		_logger.LogError(">>> Entering RandomUnitVector(dimensions={Dimensions})", dimensions);
        Random random = new();
        double[] result = new double[dimensions];
		_logger.LogError("RandomUnitVector(dimensions={Dimensions}): random → {Random}", dimensions, random);
        for (var i = 0; i < dimensions; i++)
        {
            result[i] = 2 * random.NextDouble() - 1;
        }

        var magnitude = result.Magnitude();
		_logger.LogError("RandomUnitVector(dimensions={Dimensions}): magnitude → {Magnitude}", dimensions, magnitude);
        result = result.Scale(1 / magnitude);
		_logger.LogError("RandomUnitVector(dimensions={Dimensions}): result → {Result}", dimensions, result);
		_logger.LogWarning("<<< Exiting RandomUnitVector(dimensions={Dimensions})", dimensions);
        return result;
    }

    /// <summary>
    ///     Computes a single singular vector for the given matrix, corresponding to the largest singular value.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <returns>A singular vector, with dimension equal to number of columns of the matrix.</returns>
    public static double[] Decompose1D(double[,] matrix) =>
        Decompose1D(matrix, 1E-5, 100);

    /// <summary>
    ///     Computes a single singular vector for the given matrix, corresponding to the largest singular value.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <param name="epsilon">The error margin.</param>
    /// <param name="maxIterations">The maximum number of iterations.</param>
    /// <returns>A singular vector, with dimension equal to number of columns of the matrix.</returns>
    public static double[] Decompose1D(double[,] matrix, double epsilon, int maxIterations)
    {
		_logger.LogError(">>> Entering Decompose1D(matrix={Matrix})", matrix);
        var n = matrix.GetLength(1);
        var iterations = 0;
        double mag;
		_logger.LogError("Decompose1D(matrix={Matrix},epsilon={Epsilon},maxIterations={MaxIterations}): n → {N}", matrix, epsilon, maxIterations, n);
		_logger.LogError("Decompose1D(matrix={Matrix}): n → {N}", matrix, n);
        double[] lastIteration;
        double[] currIteration = RandomUnitVector(n);
        double[,] b = matrix.Transpose().Multiply(matrix);
        do
        {
            lastIteration = currIteration.Copy();
			_logger.LogError("Decompose1D(matrix={Matrix}): lastIteration → {LastIteration}", matrix, lastIteration);
			_logger.LogError("Decompose1D(matrix={Matrix},epsilon={Epsilon},maxIterations={MaxIterations}): lastIteration → {LastIteration}", matrix, epsilon, maxIterations, lastIteration);
            currIteration = b.MultiplyVector(lastIteration);
            currIteration = currIteration.Scale(100);
			_logger.LogError("Decompose1D(matrix={Matrix}): currIteration → {CurrIteration}", matrix, currIteration);
			_logger.LogError("Decompose1D(matrix={Matrix},epsilon={Epsilon},maxIterations={MaxIterations}): currIteration → {CurrIteration}", matrix, epsilon, maxIterations, currIteration);
            mag = currIteration.Magnitude();
            if (mag > epsilon)
            {
			_logger.LogDebug("Decompose1D(matrix={Matrix},epsilon={Epsilon},maxIterations={MaxIterations}): mag → {Mag}", matrix, epsilon, maxIterations, mag);
                currIteration = currIteration.Scale(1 / mag);
            }
			_logger.LogError("Decompose1D(matrix={Matrix}): mag → {Mag}", matrix, mag);
			_logger.LogError("Decompose1D(matrix={Matrix},epsilon={Epsilon},maxIterations={MaxIterations}): mag → {Mag}", matrix, epsilon, maxIterations, mag);

            iterations++;
        }
        while (lastIteration.Dot(currIteration) < 1 - epsilon && iterations < maxIterations);
		_logger.LogDebug("Decompose1D(matrix={Matrix},epsilon={Epsilon},maxIterations={MaxIterations}): lastIteration → {LastIteration}", matrix, epsilon, maxIterations, lastIteration);
		_logger.LogDebug("Decompose1D(matrix={Matrix},epsilon={Epsilon},maxIterations={MaxIterations}): currIteration → {CurrIteration}", matrix, epsilon, maxIterations, currIteration);

		_logger.LogWarning("<<< Exiting Decompose1D(matrix={Matrix},epsilon={Epsilon},maxIterations={MaxIterations})", matrix, epsilon, maxIterations);
        return currIteration;
    }

    public static (double[,] U, double[] S, double[,] V) Decompose(double[,] matrix) =>
        Decompose(matrix, 1E-5, 100);

    /// <summary>
    ///     Computes the SVD for the given matrix, with singular values arranged from greatest to least.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <param name="epsilon">The error margin.</param>
    /// <param name="maxIterations">The maximum number of iterations.</param>
    /// <returns>The SVD.</returns>
    public static (double[,] U, double[] S, double[,] V) Decompose(
        double[,] matrix,
        double epsilon,
        int maxIterations)
    {
		_logger.LogError(">>> Entering Decompose(matrix={Matrix})", matrix);
        var m = matrix.GetLength(0);
        var n = matrix.GetLength(1);
        var numValues = Math.Min(m, n);
		_logger.LogError("Decompose(): m → {M}", m);
		_logger.LogError("Decompose(matrix={Matrix}): n → {N}", matrix, n);
		_logger.LogError("Decompose(): n → {N}", n);
		_logger.LogError("Decompose(matrix={Matrix}): numValues → {NumValues}", matrix, numValues);
		_logger.LogError("Decompose(): numValues → {NumValues}", numValues);
		_logger.LogError("Decompose(matrix={Matrix}): m → {M}", matrix, m);

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
			_logger.LogError("Decompose(matrix={Matrix}): s → {S}", matrix, s);
			_logger.LogError("Decompose(): s → {S}", s);

            // v and u should be unit vectors
            if (s < epsilon)
            {
                u = new double[m];
				_logger.LogError("Decompose(matrix={Matrix}): u → {U}", matrix, u);
				_logger.LogError("Decompose(): u → {U}", u);
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
			_logger.LogError("Decompose(matrix={Matrix}): remaining → {Remaining}", matrix, remaining);
        }

		_logger.LogDebug("Decompose(): remaining → {Remaining}", remaining);
		_logger.LogWarning("<<< Exiting Decompose()");
        return (U: us, S: sigmas, V: vs);
    }
}
