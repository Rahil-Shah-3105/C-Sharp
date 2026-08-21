using Algorithms.Numeric;
using NUnit.Framework;
using System;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

[TestFixture]
public static class ReluTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    // Tolerance for floating-point comparisons
    private const double Tolerance = 1e-9;

    // --- SCALAR TESTS (Relu.Compute(double)) ---

    [TestCase(0.0, 0.0)]
    [TestCase(1.0, 1.0)]
    [TestCase(-1.0, 0.0)]
    [TestCase(5.0, 5.0)]
    [TestCase(-5.0, 0.0)]
    public static void ReluFunction_Scalar_ReturnsCorrectValue(double input, double expected)
    {
		_logger.LogDebug(">>> Entering ReluFunction_Scalar_ReturnsCorrectValue(input={Input},expected={Expected})", input, expected);
        var result = Relu.Compute(input);
		_logger.LogDebug("<<< Exiting ReluFunction_Scalar_ReturnsCorrectValue(input={Input},expected={Expected})", input, expected);
		_logger.LogDebug("ReluFunction_Scalar_ReturnsCorrectValue(input={Input},expected={Expected}): result → {Result}", input, expected, result);
        Assert.That(result, Is.EqualTo(expected).Within(Tolerance));
    }

    [Test]
    public static void ReluFunction_Scalar_HandlesLimitsAndNaN()
    {
		_logger.LogDebug(">>> Entering ReluFunction_Scalar_HandlesLimitsAndNaN()");
        // Positive infinity stays +Infinity, negative infinity becomes 0, NaN propagates
        Assert.That(RelUComputePositiveInfinity(), Is.EqualTo(double.PositiveInfinity));
        Assert.That(RelUComputeNegativeInfinity(), Is.EqualTo(0.0).Within(Tolerance));
        Assert.That(RelUComputeNaN(), Is.NaN);
		_logger.LogDebug("<<< Exiting ReluFunction_Scalar_HandlesLimitsAndNaN()");

        static double RelUComputePositiveInfinity() => Relu.Compute(double.PositiveInfinity);
        static double RelUComputeNegativeInfinity() => Relu.Compute(double.NegativeInfinity);
        static double RelUComputeNaN() => Relu.Compute(double.NaN);
    }

    [TestCase(100.0)]
    [TestCase(0.0001)]
    [TestCase(-100.0)]
    public static void ReluFunction_Scalar_ResultIsNonNegative(double input)
    {
		_logger.LogDebug(">>> Entering ReluFunction_Scalar_ResultIsNonNegative(input={Input})", input);
        var result = Relu.Compute(input);
		_logger.LogDebug("<<< Exiting ReluFunction_Scalar_ResultIsNonNegative(input={Input})", input);
		_logger.LogDebug("ReluFunction_Scalar_ResultIsNonNegative(input={Input}): result → {Result}", input, result);
        Assert.That(result, Is.GreaterThanOrEqualTo(0.0));
    }

    // --- VECTOR TESTS (Relu.Compute(double[])) ---

    [Test]
    public static void ReluFunction_Vector_ReturnsCorrectValues()
    {
		_logger.LogDebug(">>> Entering ReluFunction_Vector_ReturnsCorrectValues()");
        var input = new[] { 0.0, 1.0, -2.0 };
        var expected = new[] { 0.0, 1.0, 0.0 };

        var result = Relu.Compute(input);
		_logger.LogDebug("ReluFunction_Vector_ReturnsCorrectValues(): result → {Result}", result);

        Assert.That(result, Is.EqualTo(expected).Within(Tolerance));
		_logger.LogDebug("<<< Exiting ReluFunction_Vector_ReturnsCorrectValues()");
    }

    [Test]
    public static void ReluFunction_Vector_HandlesLimitsAndNaN()
    {
		_logger.LogDebug(">>> Entering ReluFunction_Vector_HandlesLimitsAndNaN()");
        var input = new[] { double.PositiveInfinity, 0.0, double.NaN };
        var result = Relu.Compute(input);
		_logger.LogDebug("ReluFunction_Vector_HandlesLimitsAndNaN(): result → {Result}", result);

        Assert.That(result.Length, Is.EqualTo(input.Length));
        Assert.That(result[0], Is.EqualTo(double.PositiveInfinity));
        Assert.That(result[1], Is.EqualTo(0.0).Within(Tolerance));
        Assert.That(result[2], Is.NaN);
		_logger.LogDebug("<<< Exiting ReluFunction_Vector_HandlesLimitsAndNaN()");
    }

    // --- EXCEPTION TESTS ---

    [Test]
    public static void ReluFunction_Vector_ThrowsOnNullInput()
    {
		_logger.LogDebug(">>> Entering ReluFunction_Vector_ThrowsOnNullInput()");
        double[]? input = null;
        Assert.Throws<ArgumentNullException>(() => Relu.Compute(input!));
		_logger.LogDebug("<<< Exiting ReluFunction_Vector_ThrowsOnNullInput()");
    }

    [Test]
    public static void ReluFunction_Vector_ThrowsOnEmptyInput()
    {
		_logger.LogDebug(">>> Entering ReluFunction_Vector_ThrowsOnEmptyInput()");
        var input = Array.Empty<double>();
		_logger.LogDebug("<<< Exiting ReluFunction_Vector_ThrowsOnEmptyInput()");
		_logger.LogDebug("ReluFunction_Vector_ThrowsOnEmptyInput(): input → {Input}", input);
        Assert.Throws<ArgumentException>(() => Relu.Compute(input));
    }
}
