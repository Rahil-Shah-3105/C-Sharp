using Algorithms.Numeric;
using NUnit.Framework;
using System;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

[TestFixture]
public static class TanhTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    // Tolerance for floating-point comparisons
    private const double Tolerance = 1e-9; 

    // --- SCALAR TESTS (Tanh.Compute(double)) ---

    /// <summary>
    /// Tests Tanh function for specific values, including zero and symmetric positive/negative inputs.
    /// </summary>
    [TestCase(0.0, 0.0)]
    [TestCase(1.0, 0.7615941559557649)]
    [TestCase(-1.0, -0.7615941559557649)]
    [TestCase(5.0, 0.999909204262595)]
    [TestCase(-5.0, -0.999909204262595)]
    public static void TanhFunction_Scalar_ReturnsCorrectValue(double input, double expected)
    {
		_logger.LogDebug(">>> Entering TanhFunction_Scalar_ReturnsCorrectValue(input={Input},expected={Expected})", input, expected);
        var result = Tanh.Compute(input);
		_logger.LogDebug("<<< Exiting TanhFunction_Scalar_ReturnsCorrectValue(input={Input},expected={Expected})", input, expected);
		_logger.LogDebug("TanhFunction_Scalar_ReturnsCorrectValue(input={Input},expected={Expected}): result → {Result}", input, expected, result);
        Assert.That(result, Is.EqualTo(expected).Within(Tolerance));
    }
    
    /// <summary>
    /// Ensures the Tanh output approaches 1.0 for positive infinity and -1.0 for negative infinity.
    /// </summary>
    [Test]
    public static void TanhFunction_Scalar_ApproachesLimits()
    {
		_logger.LogDebug(">>> Entering TanhFunction_Scalar_ApproachesLimits()");
        Assert.That(Tanh.Compute(double.PositiveInfinity), Is.EqualTo(1.0).Within(Tolerance));
        Assert.That(Tanh.Compute(double.NegativeInfinity), Is.EqualTo(-1.0).Within(Tolerance));
        Assert.That(Tanh.Compute(double.NaN), Is.NaN);
		_logger.LogDebug("<<< Exiting TanhFunction_Scalar_ApproachesLimits()");
    }

    /// <summary>
    /// Checks that the Tanh result is always bounded between -1.0 and 1.0.
    /// </summary>
    [TestCase(100.0)]
    [TestCase(-100.0)]
    [TestCase(0.0001)]
    public static void TanhFunction_Scalar_ResultIsBounded(double input)
    {
		_logger.LogDebug(">>> Entering TanhFunction_Scalar_ResultIsBounded(input={Input})", input);
        var result = Tanh.Compute(input);
        Assert.That(result, Is.GreaterThanOrEqualTo(-1.0));
		_logger.LogDebug("TanhFunction_Scalar_ResultIsBounded(input={Input}): result → {Result}", input, result);
        Assert.That(result, Is.LessThanOrEqualTo(1.0));
		_logger.LogDebug("<<< Exiting TanhFunction_Scalar_ResultIsBounded(input={Input})", input);
    }

    // --- VECTOR TESTS (Tanh.Compute(double[])) ---

    /// <summary>
    /// Tests the element-wise computation for a vector input.
    /// </summary>
    [Test]
    public static void TanhFunction_Vector_ReturnsCorrectValues()
    {
		_logger.LogDebug(">>> Entering TanhFunction_Vector_ReturnsCorrectValues()");
        // Input: [0.0, 1.0, -2.0]
        var input = new[] { 0.0, 1.0, -2.0 }; 
        // Expected: [Tanh(0.0), Tanh(1.0), Tanh(-2.0)]
        var expected = new[] { 0.0, 0.7615941559557649, -0.9640275800758169 };

        var result = Tanh.Compute(input);
		_logger.LogDebug("TanhFunction_Vector_ReturnsCorrectValues(): result → {Result}", result);
        
        // Assert deep equality within tolerance
        Assert.That(result, Is.EqualTo(expected).Within(Tolerance));
		_logger.LogDebug("<<< Exiting TanhFunction_Vector_ReturnsCorrectValues()");
    }
    
    /// <summary>
    /// Tests vector handling of edge cases like infinity and NaN.
    /// </summary>
    [Test]
    public static void TanhFunction_Vector_HandlesLimitsAndNaN()
    {
		_logger.LogDebug(">>> Entering TanhFunction_Vector_HandlesLimitsAndNaN()");
        var input = new[] { double.PositiveInfinity, 0.0, double.NaN }; 
        var expected = new[] { 1.0, 0.0, double.NaN };

        var result = Tanh.Compute(input);
		_logger.LogDebug("TanhFunction_Vector_HandlesLimitsAndNaN(): result → {Result}", result);

        Assert.That(result.Length, Is.EqualTo(expected.Length));
        Assert.That(result[0], Is.EqualTo(expected[0]).Within(Tolerance)); // Pos Inf -> 1.0
        Assert.That(result[2], Is.NaN); // NaN
		_logger.LogDebug("<<< Exiting TanhFunction_Vector_HandlesLimitsAndNaN()");
    }
    
    // --- EXCEPTION TESTS ---

    /// <summary>
    /// Checks if the vector computation throws ArgumentNullException for null input.
    /// </summary>
    [Test]
    public static void TanhFunction_Vector_ThrowsOnNullInput()
    {
		_logger.LogDebug(">>> Entering TanhFunction_Vector_ThrowsOnNullInput()");
        double[]? input = null; 
        Assert.Throws<ArgumentNullException>(() => Tanh.Compute(input!));
		_logger.LogDebug("<<< Exiting TanhFunction_Vector_ThrowsOnNullInput()");
    }

    /// <summary>
    /// Checks if the vector computation throws ArgumentException for an empty input array.
    /// </summary>
    [Test]
    public static void TanhFunction_Vector_ThrowsOnEmptyInput()
    {
		_logger.LogDebug(">>> Entering TanhFunction_Vector_ThrowsOnEmptyInput()");
        var input = Array.Empty<double>();
		_logger.LogDebug("<<< Exiting TanhFunction_Vector_ThrowsOnEmptyInput()");
		_logger.LogDebug("TanhFunction_Vector_ThrowsOnEmptyInput(): input → {Input}", input);
        Assert.Throws<ArgumentException>(() => Tanh.Compute(input));
    }
}