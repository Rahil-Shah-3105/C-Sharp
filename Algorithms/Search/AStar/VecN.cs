using Microsoft.Extensions.Logging;
namespace Algorithms.Search.AStar;

/// <summary>
///     Vector Struct with N Dimensions.
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="VecN" /> struct.
/// </remarks>
/// <param name="vals">Vector components as array.</param>
public struct VecN(params double[] vals) : IEquatable<VecN>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly double[] data = vals;

    /// <summary>
    ///     Gets the dimension count of this vector.
    /// </summary>
    public int N => data.Length;

    /// <summary>
    ///     Returns the Length squared.
    /// </summary>
    /// <returns>The squared length of the vector.</returns>
    public double SqrLength()
    {
		_logger.LogDebug(">>> Entering SqrLength()");
        double ret = 0;
        for (var i = 0; i < data.Length; i++)
        {
            ret += data[i] * data[i];
        }

		_logger.LogDebug("<<< Exiting SqrLength()");
        return ret;
    }

    /// <summary>
    ///     Returns the Length of the vector.
    /// </summary>
    /// <returns>Length of the Vector.</returns>
    public double Length() => Math.Sqrt(SqrLength());

    /// <summary>
    ///     Returns the Distance between this and other.
    /// </summary>
    /// <param name="other">Other vector.</param>
    /// <returns>The distance between this and other.</returns>
    public double Distance(VecN other)
    {
		_logger.LogDebug(">>> Entering Distance(other={Other})", other);
        var delta = Subtract(other);
		_logger.LogDebug("Distance(other={Other}): delta → {Delta}", other, delta);
		_logger.LogDebug("<<< Exiting Distance(other={Other})", other);
        return delta.Length();
    }

    /// <summary>
    ///     Returns the squared Distance between this and other.
    /// </summary>
    /// <param name="other">Other vector.</param>
    /// <returns>The squared distance between this and other.</returns>
    public double SqrDistance(VecN other)
    {
		_logger.LogDebug(">>> Entering SqrDistance(other={Other})", other);
        var delta = Subtract(other);
		_logger.LogDebug("SqrDistance(other={Other}): delta → {Delta}", other, delta);
		_logger.LogDebug("<<< Exiting SqrDistance(other={Other})", other);
        return delta.SqrLength();
    }

    /// <summary>
    ///     Substracts other from this vector.
    /// </summary>
    /// <param name="other">Other vector.</param>
    /// <returns>The new vector.</returns>
    public VecN Subtract(VecN other)
    {
		_logger.LogDebug(">>> Entering Subtract(other={Other})", other);
        var dd = new double[Math.Max(data.Length, other.data.Length)];
        for (var i = 0; i < dd.Length; i++)
        {
            double val = 0;
			_logger.LogDebug("Subtract(other={Other}): val → {Val}", other, val);
            if (data.Length > i)
            {
                val = data[i];
            }

            if (other.data.Length > i)
            {
                val -= other.data[i];
            }

            dd[i] = val;
        }

		_logger.LogDebug("<<< Exiting Subtract(other={Other})", other);
        return new VecN(dd);
    }

    /// <summary>
    ///     Is used to compare Vectors with each other.
    /// </summary>
    /// <param name="other">The vector to be compared.</param>
    /// <returns>A value indicating if other has the same values as this.</returns>
    public bool Equals(VecN other)
    {
		_logger.LogDebug(">>> Entering Equals(other={Other})", other);
        if (other.N != N)
        {
            return false;
        }

        for (var i = 0; i < other.data.Length; i++)
        {
            if (Math.Abs(data[i] - other.data[i]) > 0.000001)
            {
                return false;
            }
        }

		_logger.LogDebug("<<< Exiting Equals(other={Other})", other);
        return true;
    }
}
