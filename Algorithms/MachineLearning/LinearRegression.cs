using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Algorithms.MachineLearning;

/// <summary>
/// Implements simple linear regression for one independent variable (univariate).
/// Linear regression is a supervised learning algorithm used to model the relationship
/// between a scalar dependent variable (Y) and an independent variable (X).
/// The model fits a line: Y = a + bX, where 'a' is the intercept and 'b' is the slope.
/// </summary>
public class LinearRegression
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    // Intercept (a) and slope (b) of the fitted line
    public double Intercept { get; private set; }

    public double Slope { get; private set; }

    public bool IsFitted { get; private set; }

    /// <summary>
    /// Fits the linear regression model to the provided data.
    /// </summary>
    /// <param name="x">List of independent variable values.</param>
    /// <param name="y">List of dependent variable values.</param>
    /// <exception cref="ArgumentException">Thrown if input lists are null, empty, or of different lengths.</exception>
    public void Fit(IList<double> x, IList<double> y)
    {
		_logger.LogDebug(">>> Entering Fit(x={X},y={Y})", x, y);
        if (x == null || y == null)
        {
			_logger.LogWarning("Throwing ArgumentException from Fit(x={X},y={Y})", x, y);
            throw new ArgumentException("Input data cannot be null.");
        }

        if (x.Count == 0 || y.Count == 0)
        {
            throw new ArgumentException("Input data cannot be empty.");
        }

        if (x.Count != y.Count)
        {
            throw new ArgumentException("Input lists must have the same length.");
        }

        // Calculate means
        double xMean = x.Average();
		_logger.LogDebug("Fit(x={X},y={Y}): xMean → {XMean}", x, y, xMean);
        double yMean = y.Average();
		_logger.LogDebug("Fit(x={X},y={Y}): yMean → {YMean}", x, y, yMean);

        // Calculate slope (b) and intercept (a)
        double numerator = 0.0;
        double denominator = 0.0;
        for (int i = 0; i < x.Count; i++)
        {
            numerator += (x[i] - xMean) * (y[i] - yMean);
            denominator += (x[i] - xMean) * (x[i] - xMean);
        }

        const double epsilon = 1e-12;
        if (Math.Abs(denominator) < epsilon)
        {
		_logger.LogDebug("<<< Exiting Fit(x={X},y={Y})", x, y);
            throw new ArgumentException("Variance of X must not be zero.");
        }

        Slope = numerator / denominator;
		_logger.LogDebug("Fit(x={X},y={Y}): Slope → {Slope}", x, y, Slope);
        Intercept = yMean - Slope * xMean;
		_logger.LogDebug("Fit(x={X},y={Y}): Intercept → {Intercept}", x, y, Intercept);
        IsFitted = true;
		_logger.LogDebug("Fit(x={X},y={Y}): IsFitted → {IsFitted}", x, y, IsFitted);
    }

    /// <summary>
    /// Predicts the output value for a given input using the fitted model.
    /// </summary>
    /// <param name="x">Input value.</param>
    /// <returns>Predicted output value.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the model is not fitted.</exception>
    public double Predict(double x)
    {
		_logger.LogDebug(">>> Entering Predict(x={X})", x);
        if (!IsFitted)
        {
			_logger.LogWarning("Throwing InvalidOperationException from Predict(x={X})", x);
		_logger.LogDebug("<<< Exiting Predict(x={X})", x);
            throw new InvalidOperationException("Model must be fitted before prediction.");
        }

		_logger.LogDebug("<<< Exiting Predict(xValues={XValues})", xValues);
        return Intercept + Slope * x;
    }

    /// <summary>
    /// Predicts output values for a list of inputs using the fitted model.
    /// </summary>
    /// <param name="xValues">List of input values.</param>
    /// <returns>List of predicted output values.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the model is not fitted.</exception>
    public IList<double> Predict(IList<double> xValues)
    {
        if (!IsFitted)
        {
            throw new InvalidOperationException("Model must be fitted before prediction.");
        }

        return xValues.Select(Predict).ToList();
    }
}
