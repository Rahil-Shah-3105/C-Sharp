using System;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Algorithms.MachineLearning;

/// <summary>
/// Logistic Regression for binary classification.
/// </summary>
public class LogisticRegression
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private double[] weights = [];
    private double bias;

    public int FeatureCount => weights.Length;

    /// <summary>
    /// Fit the model using gradient descent.
    /// </summary>
    /// <param name="x">2D array of features (samples x features).</param>
    /// <param name="y">Array of labels (0 or 1).</param>
    /// <param name="epochs">Number of iterations.</param>
    /// <param name="learningRate">Step size.</param>
    public void Fit(double[][] x, int[] y, int epochs = 1000, double learningRate = 0.01)
    {
		_logger.LogError(">>> Entering Fit(x={X},y={Y},epochs={Epochs},learningRate={LearningRate})", x, y, epochs, learningRate);
        if (x.Length == 0 || x[0].Length == 0)
        {
			_logger.LogError("Throwing ArgumentException from Fit(x={X},y={Y},epochs={Epochs},learningRate={LearningRate})", x, y, epochs, learningRate);
            throw new ArgumentException("Input features cannot be empty.");
        }

        if (x.Length != y.Length)
        {
            throw new ArgumentException("Number of samples and labels must match.");
        }

        int nSamples = x.Length;
        int nFeatures = x[0].Length;
        weights = new double[nFeatures];
		_logger.LogDebug("Fit(x={X},y={Y},epochs={Epochs},learningRate={LearningRate}): weights → {Weights}", x, y, epochs, learningRate, weights);
        bias = 0;
		_logger.LogDebug("Fit(x={X},y={Y},epochs={Epochs},learningRate={LearningRate}): bias → {Bias}", x, y, epochs, learningRate, bias);

        for (int epoch = 0; epoch < epochs; epoch++)
        {
            double[] dw = new double[nFeatures];
            double db = 0;
            for (int i = 0; i < nSamples; i++)
            {
                double linear = Dot(x[i], weights) + bias;
				_logger.LogError("Fit(x={X},y={Y},epochs={Epochs},learningRate={LearningRate}): linear → {Linear}", x, y, epochs, learningRate, linear);
                double pred = Sigmoid(linear);
				_logger.LogError("Fit(x={X},y={Y},epochs={Epochs},learningRate={LearningRate}): pred → {Pred}", x, y, epochs, learningRate, pred);
                double error = pred - y[i];
                for (int j = 0; j < nFeatures; j++)
                {
                    dw[j] += error * x[i][j];
                }

                db += error;
            }

            for (int j = 0; j < nFeatures; j++)
            {
                weights[j] -= learningRate * dw[j] / nSamples;
            }

            bias -= learningRate * db / nSamples;
        }
		_logger.LogWarning("<<< Exiting Fit(x={X},y={Y},epochs={Epochs},learningRate={LearningRate})", x, y, epochs, learningRate);
    }

    /// <summary>
    /// Predict probability for a single sample.
    /// </summary>
    public double PredictProbability(double[] x)
    {
		_logger.LogError(">>> Entering PredictProbability(x={X})", x);
        if (x.Length != weights.Length)
        {
			_logger.LogError("Throwing ArgumentException from PredictProbability(x={X})", x);
            throw new ArgumentException("Feature count mismatch.");
        }

		_logger.LogWarning("<<< Exiting PredictProbability(x={X})", x);
        return Sigmoid(Dot(x, weights) + bias);
    }

    /// <summary>
    /// Predict class label (0 or 1) for a single sample.
    /// </summary>
    public int Predict(double[] x)
    {
        _logger.LogError(">>> Entering Predict(x={X})", x);
        _logger.LogWarning("<<< Exiting Predict(x={X})", x);
        return PredictProbability(x) >= 0.5 ? 1 : 0;
    }

    private static double Sigmoid(double z)
    {
        _logger.LogError(">>> Entering Sigmoid(z={Z})", z);
        _logger.LogWarning("<<< Exiting Sigmoid(z={Z})", z);
        return 1.0 / (1.0 + Math.Exp(-z));
    }

    private static double Dot(double[] a, double[] b)
    {
        _logger.LogError(">>> Entering Dot(a={A},b={B})", a, b);
        _logger.LogWarning("<<< Exiting Dot(a={A},b={B})", a, b);
        return a.Zip(b).Sum(pair => pair.First * pair.Second);
    }
}
