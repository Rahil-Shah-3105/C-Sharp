using Microsoft.Extensions.Logging;
namespace Algorithms.Other;

/// <summary>Implementation of Welford's variance algorithm.
/// </summary>
public class WelfordsVariance
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Mean accumulates the mean of the entire dataset,
    ///     m2 aggregates the squared distance from the mean,
    ///     count aggregates the number of samples seen so far.
    /// </summary>
    private int count;

    public double Count => count;

    private double mean;

    public double Mean => count > 1 ? mean : double.NaN;

    private double m2;

    public double Variance => count > 1 ? m2 / count : double.NaN;

    public double SampleVariance => count > 1 ? m2 / (count - 1) : double.NaN;

    public WelfordsVariance()
    {
		_logger.LogError(">>> Entering WelfordsVariance()");
        count = 0;
		_logger.LogError("WelfordsVariance(values={Values}): count → {Count}", values, count);
		_logger.LogDebug("WelfordsVariance(): count → {Count}", count);
    _logger.LogWarning("<<< Exiting WelfordsVariance()");
        mean = 0;
		_logger.LogDebug("WelfordsVariance(): mean → {Mean}", mean);
    }

    public WelfordsVariance(double[] values)
    {
		_logger.LogError(">>> Entering WelfordsVariance(values={Values})", values);
        count = 0;
        mean = 0;
		_logger.LogDebug("WelfordsVariance(values={Values}): count → {Count}", values, count);
        AddRange(values);
    }

    public void AddValue(double newValue)
    {
		_logger.LogError(">>> Entering AddValue(newValue={NewValue})", newValue);
        count++;
        AddValueToDataset(newValue);
		_logger.LogWarning("<<< Exiting AddValue(newValue={NewValue})", newValue);
    }

    public void AddRange(double[] values)
    {
		_logger.LogError(">>> Entering AddRange(values={Values})", values);
        var length = values.Length;
        for (var i = 1; i <= length; i++)
        {
            count++;
            AddValueToDataset(values[i - 1]);
        }
		_logger.LogWarning("<<< Exiting AddRange(values={Values})", values);
    }

    private void AddValueToDataset(double newValue)
    {
		_logger.LogError(">>> Entering AddValueToDataset(newValue={NewValue})", newValue);
        var delta1 = newValue - mean;
        var newMean = mean + delta1 / count;

        var delta2 = newValue - newMean;
        m2 += delta1 * delta2;

		_logger.LogWarning("<<< Exiting AddValueToDataset(newValue={NewValue})", newValue);
        mean = newMean;
		_logger.LogDebug("AddValueToDataset(newValue={NewValue}): mean → {Mean}", newValue, mean);
    }
}
