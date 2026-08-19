using NUnit.Framework;
using Algorithms.MachineLearning;
using System;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.MachineLearning;

[TestFixture]
public class KNearestNeighborsTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void Constructor_InvalidK_ThrowsException()
    {
		_logger.LogDebug(">>> Entering Constructor_InvalidK_ThrowsException()");
        Assert.Throws<ArgumentOutOfRangeException>(() => new KNearestNeighbors<string>(0));
		_logger.LogDebug("<<< Exiting Constructor_InvalidK_ThrowsException()");
    }

    [Test]
    public void AddSample_NullFeatures_ThrowsException()
    {
		_logger.LogDebug(">>> Entering AddSample_NullFeatures_ThrowsException()");
        var knn = new KNearestNeighbors<string>(3);
        double[]? features = null;
		_logger.LogDebug("AddSample_NullFeatures_ThrowsException(): knn → {Knn}", knn);
        Assert.Throws<ArgumentNullException>(() => knn.AddSample(features!, "A"));
		_logger.LogDebug("<<< Exiting AddSample_NullFeatures_ThrowsException()");
    }

    [Test]
    public void Predict_NoTrainingData_ThrowsException()
    {
		_logger.LogDebug(">>> Entering Predict_NoTrainingData_ThrowsException()");
        var knn = new KNearestNeighbors<string>(1);
        Assert.Throws<InvalidOperationException>(() => knn.Predict(new[] { 1.0 }));
		_logger.LogDebug("Predict_NoTrainingData_ThrowsException(): knn → {Knn}", knn);
		_logger.LogDebug("<<< Exiting Predict_NoTrainingData_ThrowsException()");
    }

    [Test]
    public void Predict_NullFeatures_ThrowsException()
    {
		_logger.LogDebug(">>> Entering Predict_NullFeatures_ThrowsException()");
        var knn = new KNearestNeighbors<string>(1);
        knn.AddSample(new[] { 1.0 }, "A");
		_logger.LogDebug("Predict_NullFeatures_ThrowsException(): knn → {Knn}", knn);
        double[]? features = null;
        Assert.Throws<ArgumentNullException>(() => knn.Predict(features!));
		_logger.LogDebug("<<< Exiting Predict_NullFeatures_ThrowsException()");
    }

    [Test]
    public void EuclideanDistance_DifferentLengths_ThrowsException()
    {
		_logger.LogDebug(">>> Entering EuclideanDistance_DifferentLengths_ThrowsException()");
        Assert.Throws<ArgumentException>(() => KNearestNeighbors<string>.EuclideanDistance(new[] { 1.0 }, new[] { 1.0, 2.0 }));
		_logger.LogDebug("<<< Exiting EuclideanDistance_DifferentLengths_ThrowsException()");
    }

    [Test]
    public void EuclideanDistance_CorrectResult()
    {
		_logger.LogDebug(">>> Entering EuclideanDistance_CorrectResult()");
        double[] a = { 1.0, 2.0 };
        double[] b = { 4.0, 6.0 };
        double expected = 5.0;
        double actual = KNearestNeighbors<string>.EuclideanDistance(a, b);
		_logger.LogDebug("EuclideanDistance_CorrectResult(): actual → {Actual}", actual);
        Assert.That(actual, Is.EqualTo(expected).Within(1e-9));
		_logger.LogDebug("<<< Exiting EuclideanDistance_CorrectResult()");
    }

    [Test]
    public void Predict_SingleNeighbor_CorrectLabel()
    {
		_logger.LogDebug(">>> Entering Predict_SingleNeighbor_CorrectLabel()");
        var knn = new KNearestNeighbors<string>(1);
        knn.AddSample(new[] { 1.0, 2.0 }, "A");
		_logger.LogDebug("Predict_SingleNeighbor_CorrectLabel(): knn → {Knn}", knn);
        knn.AddSample(new[] { 3.0, 4.0 }, "B");
        var label = knn.Predict(new[] { 1.1, 2.1 });
		_logger.LogDebug("Predict_SingleNeighbor_CorrectLabel(): label → {Label}", label);
        Assert.That(label, Is.EqualTo("A"));
		_logger.LogDebug("<<< Exiting Predict_SingleNeighbor_CorrectLabel()");
    }

    [Test]
    public void Predict_MajorityVote_CorrectLabel()
    {
		_logger.LogDebug(">>> Entering Predict_MajorityVote_CorrectLabel()");
        var knn = new KNearestNeighbors<string>(3);
        knn.AddSample(new[] { 0.0, 0.0 }, "A");
		_logger.LogDebug("Predict_MajorityVote_CorrectLabel(): knn → {Knn}", knn);
        knn.AddSample(new[] { 0.1, 0.1 }, "A");
        knn.AddSample(new[] { 1.0, 1.0 }, "B");
        var label = knn.Predict(new[] { 0.05, 0.05 });
		_logger.LogDebug("Predict_MajorityVote_CorrectLabel(): label → {Label}", label);
        Assert.That(label, Is.EqualTo("A"));
		_logger.LogDebug("<<< Exiting Predict_MajorityVote_CorrectLabel()");
    }

    [Test]
    public void Predict_TieBreaker_ReturnsConsistentLabel()
    {
		_logger.LogDebug(">>> Entering Predict_TieBreaker_ReturnsConsistentLabel()");
        var knn = new KNearestNeighbors<string>(2);
        knn.AddSample(new[] { 0.0, 0.0 }, "A");
		_logger.LogDebug("Predict_TieBreaker_ReturnsConsistentLabel(): knn → {Knn}", knn);
        knn.AddSample(new[] { 1.0, 1.0 }, "B");
        var label = knn.Predict(new[] { 0.5, 0.5 });
		_logger.LogDebug("Predict_TieBreaker_ReturnsConsistentLabel(): label → {Label}", label);
        Assert.That(label, Is.EqualTo("A"));
		_logger.LogDebug("<<< Exiting Predict_TieBreaker_ReturnsConsistentLabel()");
    }
}
