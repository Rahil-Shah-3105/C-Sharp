using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Algorithms.MachineLearning;

/// <summary>
/// K-Nearest Neighbors (KNN) classifier implementation.
/// This algorithm classifies data points based on the majority label of their k nearest neighbors.
/// </summary>
/// <typeparam name="TLabel">
/// The type of the label used for classification. This can be any type that represents the class or category of a sample.
/// </typeparam>
public class KNearestNeighbors<TLabel>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly List<(double[] Features, TLabel Label)> trainingData = new();
    private readonly int k;

    /// <summary>
    /// Initializes a new instance of the <see cref="KNearestNeighbors{TLabel}"/> classifier.
    /// </summary>
    /// <param name="k">Number of neighbors to consider for classification.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if k is less than 1.</exception>
    public KNearestNeighbors(int k)
    {
		_logger.LogDebug(">>> Entering KNearestNeighbors(k={K})", k);
        if (k < 1)
        {
			_logger.LogWarning("Throwing ArgumentOutOfRangeException from KNearestNeighbors(k={K})", k);
		_logger.LogDebug("<<< Exiting KNearestNeighbors(k={K})", k);
		_logger.LogDebug("KNearestNeighbors(k={K}): k → {K1}", k, k);
            throw new ArgumentOutOfRangeException(nameof(k), "k must be at least 1.");
        }

        this.k = k;
    }

    /// <summary>
    /// Calculates the Euclidean distance between two feature vectors.
    /// </summary>
    /// <param name="a">First feature vector.</param>
    /// <param name="b">Second feature vector.</param>
    /// <returns>Euclidean distance.</returns>
    /// <exception cref="ArgumentException">Thrown if vectors are of different lengths.</exception>
    public static double EuclideanDistance(double[] a, double[] b)
    {
		_logger.LogDebug(">>> Entering EuclideanDistance(a={A},b={B})", a, b);
        if (a.Length != b.Length)
        {
			_logger.LogWarning("Throwing ArgumentException from EuclideanDistance(a={A},b={B})", a, b);
            throw new ArgumentException("Feature vectors must be of the same length.");
        }

        double sum = 0;
        for (int i = 0; i < a.Length; i++)
        {
            double diff = a[i] - b[i];
            sum += diff * diff;
        }

		_logger.LogDebug("<<< Exiting EuclideanDistance(a={A},b={B})", a, b);
        return Math.Sqrt(sum);
    }

    /// <summary>
    /// Adds a training sample to the classifier.
    /// </summary>
    /// <param name="features">Feature vector of the sample.</param>
    /// <param name="label">Label of the sample.</param>
    public void AddSample(double[] features, TLabel label)
    {
		_logger.LogDebug(">>> Entering AddSample(features={Features},label={Label})", features, label);
        if (features == null)
        {
			_logger.LogWarning("Throwing ArgumentNullException from AddSample(features={Features},label={Label})", features, label);
            throw new ArgumentNullException(nameof(features));
        }

        trainingData.Add((features, label));
		_logger.LogDebug("<<< Exiting AddSample(features={Features},label={Label})", features, label);
    }

    /// <summary>
    /// Predicts the label for a given feature vector using the KNN algorithm.
    /// </summary>
    /// <param name="features">Feature vector to classify.</param>
    /// <returns>Predicted label.</returns>
    /// <exception cref="InvalidOperationException">Thrown if there is no training data.</exception>
    public TLabel Predict(double[] features)
    {
		_logger.LogDebug(">>> Entering Predict(features={Features})", features);
        if (trainingData.Count == 0)
        {
			_logger.LogWarning("Throwing InvalidOperationException from Predict(features={Features})", features);
            throw new InvalidOperationException("No training data available.");
        }

        if (features == null)
        {
			_logger.LogWarning("Throwing ArgumentNullException from Predict(features={Features})", features);
            throw new ArgumentNullException(nameof(features));
        }

        // Compute distances to all training samples
        var distances = trainingData
            .Select(td => (Label: td.Label, Distance: EuclideanDistance(features, td.Features)))
            .OrderBy(x => x.Distance)
            .Take(k)
            .ToList();

        // Majority vote
        var labelCounts = distances
            .GroupBy(x => x.Label)
            .Select(g => new { Label = g.Key, Count = g.Count(), MinDistance = g.Min(item => item.Distance) })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.MinDistance)
            .ToList();

		_logger.LogDebug("<<< Exiting Predict(features={Features})", features);
        return labelCounts.First().Label;
    }
}
