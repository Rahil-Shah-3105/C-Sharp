using Algorithms.Numeric.Series;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric.Decomposition;

public class MaclaurinTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(0.01, 3, 0.01)]
    [TestCase(1, 7, 0.001)]
    [TestCase(-1.2, 7, 0.001)]
    public void Exp_TermsForm_ValidCases(double point, int terms, double expectedError)
    {
		_logger.LogError(">>> Entering Exp_TermsForm_ValidCases(point={Point},terms={Terms},expectedError={ExpectedError})", point, terms, expectedError);
        // Arrange
        var expected = Math.Exp(point);
		_logger.LogError("Exp_TermsForm_ValidCases(point={Point},terms={Terms},expectedError={ExpectedError}): expected → {Expected}", point, terms, expectedError, expected);

        // Act
        var actual = Maclaurin.Exp(point, terms);
		_logger.LogError("Exp_TermsForm_ValidCases(point={Point},terms={Terms},expectedError={ExpectedError}): actual → {Actual}", point, terms, expectedError, actual);

        // Assert
        Assert.That(Math.Abs(expected - actual) < expectedError, Is.True);
		_logger.LogWarning("<<< Exiting Exp_TermsForm_ValidCases(point={Point},terms={Terms},expectedError={ExpectedError})", point, terms, expectedError);
    }

    [Test]
    public void Exp_TermsForm_InvalidCase() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => Maclaurin.Exp(0, -1));

    [TestCase(0, 1, 0.001)]
    [TestCase(1, 7, 0.001)]
    [TestCase(1.57, 7, 0.001)]
    [TestCase(3.14, 7, 0.001)]
    public void Sin_TermsForm_ValidCases(double point, int terms, double expectedError)
    {
        // Arrange
        var expected = Math.Sin(point);
		_logger.LogError(">>> Entering Sin_TermsForm_ValidCases(point={Point},terms={Terms},expectedError={ExpectedError})", point, terms, expectedError);

        // Act
        var actual = Maclaurin.Sin(point, terms);

		_logger.LogError("Sin_TermsForm_ValidCases(point={Point},terms={Terms},expectedError={ExpectedError}): actual → {Actual}", point, terms, expectedError, actual);
        // Assert
        Assert.That(Math.Abs(expected - actual) < expectedError, Is.True);
		_logger.LogWarning("<<< Exiting Sin_TermsForm_ValidCases(point={Point},terms={Terms},expectedError={ExpectedError})", point, terms, expectedError);
    }

    [Test]
    public void Sin_TermsForm_InvalidCase() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => Maclaurin.Sin(0, -1));

    [TestCase(0, 1, 0.001)]
    [TestCase(1, 7, 0.001)]
    [TestCase(1.57, 7, 0.001)]
    [TestCase(3.14, 7, 0.001)]
    public void Cos_TermsForm_ValidCases(double point, int terms, double expectedError)
    {
        // Arrange
        var expected = Math.Cos(point);
		_logger.LogError(">>> Entering Cos_TermsForm_ValidCases(point={Point},terms={Terms},expectedError={ExpectedError})", point, terms, expectedError);

        // Act
        var actual = Maclaurin.Cos(point, terms);

		_logger.LogError("Cos_TermsForm_ValidCases(point={Point},terms={Terms},expectedError={ExpectedError}): actual → {Actual}", point, terms, expectedError, actual);
        // Assert
        Assert.That(Math.Abs(expected - actual) < expectedError, Is.True);
		_logger.LogWarning("<<< Exiting Cos_TermsForm_ValidCases(point={Point},terms={Terms},expectedError={ExpectedError})", point, terms, expectedError);
    }

    [Test]
    public void Cos_TermsForm_InvalidCase() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => Maclaurin.Cos(0, -1));

    [TestCase(0.1, 0.001)]
    [TestCase(0.1, 0.00001)]
    [TestCase(2.1, 0.001)]
    [TestCase(-1.2, 0.001)]
    public void Exp_ErrorForm_ValidCases(double point, double error)
    {
        // Arrange
        var expected = Math.Exp(point);
		_logger.LogError(">>> Entering Exp_ErrorForm_ValidCases(point={Point},error={Error})", point, error);

        // Act
        var actual = Maclaurin.Exp(point, error);

		_logger.LogError("Exp_ErrorForm_ValidCases(point={Point},error={Error}): actual → {Actual}", point, error, actual);
        // Assert
        Assert.That(Math.Abs(expected - actual) < error, Is.True);
		_logger.LogWarning("<<< Exiting Exp_ErrorForm_ValidCases(point={Point},error={Error})", point, error);
    }

    [TestCase(0.0)]
    [TestCase(1.0)]
    public void Exp_ErrorForm_InvalidCases(double error) =>
        Assert.Throws<ArgumentException>(() => Maclaurin.Exp(0.0, error));

    [TestCase(0, 0.001)]
    [TestCase(1, 0.00001)]
    [TestCase(1.57, 0.0001)]
    [TestCase(3.14, 0.0001)]
    public void Sin_ErrorForm_ValidCases(double point, double error)
    {
        // Arrange
        var expected = Math.Sin(point);
		_logger.LogError(">>> Entering Sin_ErrorForm_ValidCases(point={Point},error={Error})", point, error);

        // Act
        var actual = Maclaurin.Sin(point, error);

		_logger.LogError("Sin_ErrorForm_ValidCases(point={Point},error={Error}): actual → {Actual}", point, error, actual);
        // Assert
        Assert.That(Math.Abs(expected - actual) < error, Is.True);
		_logger.LogWarning("<<< Exiting Sin_ErrorForm_ValidCases(point={Point},error={Error})", point, error);
    }

    [TestCase(0.0)]
    [TestCase(1.0)]
    public void Sin_ErrorForm_InvalidCases(double error) =>
        Assert.Throws<ArgumentException>(() => Maclaurin.Sin(0.0, error));

    [TestCase(0, 0.001)]
    [TestCase(1, 0.00001)]
    [TestCase(1.57, 0.0001)]
    [TestCase(3.14, 0.0001)]
    public void Cos_ErrorForm_ValidCases(double point, double error)
    {
        // Arrange
        var expected = Math.Cos(point);
		_logger.LogError(">>> Entering Cos_ErrorForm_ValidCases(point={Point},error={Error})", point, error);

        // Act
        var actual = Maclaurin.Cos(point, error);

		_logger.LogError("Cos_ErrorForm_ValidCases(point={Point},error={Error}): actual → {Actual}", point, error, actual);
        // Assert
        Assert.That(Math.Abs(expected - actual) < error, Is.True);
		_logger.LogWarning("<<< Exiting Cos_ErrorForm_ValidCases(point={Point},error={Error})", point, error);
    }

    [TestCase(0.0)]
    [TestCase(1.0)]
    public void Cos_ErrorForm_InvalidCases(double error) =>
        Assert.Throws<ArgumentException>(() => Maclaurin.Cos(0.0, error));
}
