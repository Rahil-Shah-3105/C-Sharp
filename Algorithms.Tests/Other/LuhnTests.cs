using Algorithms.Other;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Other;

/// <summary>
///     A class for testing the Luhn algorithm.
/// </summary>
public class LuhnTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase("89014103211118510720")] // ICCID
    [TestCase("071052120")] // Social Security Code
    [TestCase("449125546588769")] // IMEI
    [TestCase("4417123456789113")] // Bank card
    public void ValidateTrue(string number)
    {
		_logger.LogDebug(">>> Entering ValidateTrue(number={Number})", number);
        // Arrange
        bool validate;

        // Act
        validate = Luhn.Validate(number);

		_logger.LogDebug("ValidateTrue(number={Number}): validate → {Validate}", number, validate);
        // Assert
        Assert.That(validate, Is.True);
		_logger.LogDebug("<<< Exiting ValidateTrue(number={Number})", number);
    }

    [TestCase("89012104211118510720")] // ICCID
    [TestCase("021053120")] // Social Security Code
    [TestCase("449145545588969")] // IMEI
    [TestCase("4437113456749113")] // Bank card
    public void ValidateFalse(string number)
    {
		_logger.LogDebug(">>> Entering ValidateFalse(number={Number})", number);
        // Arrange
        bool validate;

        // Act
        validate = Luhn.Validate(number);

		_logger.LogDebug("ValidateFalse(number={Number}): validate → {Validate}", number, validate);
        // Assert
        Assert.That(validate, Is.False);
		_logger.LogDebug("<<< Exiting ValidateFalse(number={Number})", number);
    }

    [TestCase("x9012104211118510720")] // ICCID
    [TestCase("0210x3120")] // Social Security Code
    [TestCase("44914554558896x")] // IMEI
    [TestCase("4437113456x49113")] // Bank card
    public void GetLostNum(string number)
    {
		_logger.LogDebug(">>> Entering GetLostNum(number={Number})", number);
        // Arrange
        int lostNum;
        bool validate;

        // Act
        lostNum = Luhn.GetLostNum(number);
        validate = Luhn.Validate(number.Replace("x", lostNum.ToString()));

		_logger.LogDebug("GetLostNum(number={Number}): lostNum → {LostNum}", number, lostNum);
		_logger.LogDebug("GetLostNum(number={Number}): validate → {Validate}", number, validate);
        // Assert
        Assert.That(validate, Is.True);
		_logger.LogDebug("<<< Exiting GetLostNum(number={Number})", number);
    }

    [TestCase("")]
    [TestCase("xxxx")] 
    [TestCase("abcde")]
    [TestCase("1x345678901234567")]
    [TestCase("x1234567890123456")]
    [TestCase("1234567890123456x")]
    [TestCase("1111111111111111")]
    [TestCase("1a2b3c4d5e6f7g8h9i0j")]
    public void EdgeCases_GetLostNum(string number)
    {
		_logger.LogDebug(">>> Entering EdgeCases_GetLostNum(number={Number})", number);
        // Act
        int lostNum = Luhn.GetLostNum(number.Replace("x", "0"));
		_logger.LogDebug("EdgeCases_GetLostNum(number={Number}): lostNum → {LostNum}", number, lostNum);
        // Assert
        Assert.That(lostNum, Is.InRange(0, 9));
		_logger.LogDebug("<<< Exiting EdgeCases_GetLostNum(number={Number})", number);
    }

    [TestCase("1a2b3c4d5e6f7g8h9i0j")]
    public void EdgeCases_Validate(string number)
    {
		_logger.LogDebug(">>> Entering EdgeCases_Validate(number={Number})", number);
        // Act
        bool result = Luhn.Validate(number);
        // Assert
        Assert.That(result, Is.False);
		_logger.LogDebug("<<< Exiting EdgeCases_Validate(number={Number})", number);
    }
}
