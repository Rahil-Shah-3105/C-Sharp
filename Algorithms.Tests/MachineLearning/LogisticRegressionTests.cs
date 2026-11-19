using NUnit.Framework;
using Algorithms.MachineLearning;
using System;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.MachineLearning;

[TestFixture]
public class LogisticRegressionTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void Fit_ThrowsOnEmptyInput()
    {
		_logger.LogError(">>> Entering Fit_ThrowsOnEmptyInput()");
        var model = new LogisticRegression();
		_logger.LogWarning("<<< Exiting Fit_ThrowsOnEmptyInput()");
		_logger.LogError("Fit_ThrowsOnEmptyInput(): model → {Model}", model);
        Assert.Throws<ArgumentException>(() => model.Fit(Array.Empty<double[]>(), Array.Empty<int>()));
    }

    [Test]
    public void Fit_ThrowsOnMismatchedLabels()
    {
		_logger.LogError(">>> Entering Fit_ThrowsOnMismatchedLabels()");
        var model = new LogisticRegression();
        double[][] X = { new double[] { 1, 2 } };
		_logger.LogError("Fit_ThrowsOnMismatchedLabels(): model → {Model}", model);
        int[] y = { 1, 0 };
        Assert.Throws<ArgumentException>(() => model.Fit(X, y));
		_logger.LogWarning("<<< Exiting Fit_ThrowsOnMismatchedLabels()");
    }

    [Test]
    public void FitAndPredict_WorksOnSimpleData()
    {
		_logger.LogError(">>> Entering FitAndPredict_WorksOnSimpleData()");
        // Simple AND logic
        double[][] X =
        {
            new[] { 0.0, 0.0 },
            new[] { 0.0, 1.0 },
            new[] { 1.0, 0.0 },
            new[] { 1.0, 1.0 }
        };
        int[] y = { 0, 0, 0, 1 };
        var model = new LogisticRegression();
		_logger.LogError("FitAndPredict_WorksOnSimpleData(): model → {Model}", model);
        model.Fit(X, y, epochs: 2000, learningRate: 0.1);
        Assert.That(model.Predict(new double[] { 0, 0 }), Is.EqualTo(0));
        Assert.That(model.Predict(new double[] { 0, 1 }), Is.EqualTo(0));
        Assert.That(model.Predict(new double[] { 1, 0 }), Is.EqualTo(0));
        Assert.That(model.Predict(new double[] { 1, 1 }), Is.EqualTo(1));
		_logger.LogWarning("<<< Exiting FitAndPredict_WorksOnSimpleData()");
    }

    [Test]
    public void PredictProbability_ThrowsOnFeatureMismatch()
    {
		_logger.LogError(">>> Entering PredictProbability_ThrowsOnFeatureMismatch()");
        var model = new LogisticRegression();
        double[][] X = { new double[] { 1, 2 } };
		_logger.LogError("PredictProbability_ThrowsOnFeatureMismatch(): model → {Model}", model);
        int[] y = { 1 };
        model.Fit(X, y);
        Assert.Throws<ArgumentException>(() => model.PredictProbability(new double[] { 1 }));
		_logger.LogWarning("<<< Exiting PredictProbability_ThrowsOnFeatureMismatch()");
    }

    [Test]
    public void FeatureCount_ReturnsCorrectValue()
    {
		_logger.LogError(">>> Entering FeatureCount_ReturnsCorrectValue()");
        var model = new LogisticRegression();
        double[][] X = { new double[] { 1, 2, 3 } };
		_logger.LogError("FeatureCount_ReturnsCorrectValue(): model → {Model}", model);
        int[] y = { 1 };
        model.Fit(X, y);
        Assert.That(model.FeatureCount, Is.EqualTo(3));
		_logger.LogWarning("<<< Exiting FeatureCount_ReturnsCorrectValue()");
    }
}
