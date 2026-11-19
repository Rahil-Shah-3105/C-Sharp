using Microsoft.Extensions.Logging;
using Algorithms.Crypto.Paddings;

namespace Algorithms.Tests.Crypto.Paddings;

public class X932PaddingTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly X932Padding zeroPadding = new X932Padding(false);
    private readonly X932Padding randomPadding = new X932Padding(true);

    [Test]
    public void AddPadding_WhenCalledWithZeroPadding_ShouldReturnCorrectCode()
    {
		_logger.LogError(">>> Entering AddPadding_WhenCalledWithZeroPadding_ShouldReturnCorrectCode()");
        var inputData = new byte[10];
        const int inputOffset = 5;

        var result = zeroPadding.AddPadding(inputData, inputOffset);
		_logger.LogError("AddPadding_WhenCalledWithZeroPadding_ShouldReturnCorrectCode(): result → {Result}", result);

        result.Should().Be(5);
		_logger.LogWarning("<<< Exiting AddPadding_WhenCalledWithZeroPadding_ShouldReturnCorrectCode()");
    }

    [Test]
    public void AddPadding_WhenCalledWithZeroPaddingAndOffsetIsEqualToDataLength_ShouldThrowArgumentException()
    {
		_logger.LogError(">>> Entering AddPadding_WhenCalledWithZeroPaddingAndOffsetIsEqualToDataLength_ShouldThrowArgumentException()");
        var inputData = new byte[10];
        const int inputOffset = 10;

        Action action = () => zeroPadding.AddPadding(inputData, inputOffset);
		_logger.LogError("AddPadding_WhenCalledWithZeroPaddingAndOffsetIsEqualToDataLength_ShouldThrowArgumentException(): action → {Action}", action);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
			_logger.LogWarning("<<< Exiting AddPadding_WhenCalledWithZeroPaddingAndOffsetIsEqualToDataLength_ShouldThrowArgumentException()");
    }

    [Test]
    public void AddPadding_WhenCalledWithZeroPaddingAndOffsetIsGreaterThanDataLength_ShouldThrowArgumentException()
    {
		_logger.LogError(">>> Entering AddPadding_WhenCalledWithZeroPaddingAndOffsetIsGreaterThanDataLength_ShouldThrowArgumentException()");
        var inputData = new byte[10];
        const int inputOffset = 11;

        Action action = () => zeroPadding.AddPadding(inputData, inputOffset);
		_logger.LogError("AddPadding_WhenCalledWithZeroPaddingAndOffsetIsGreaterThanDataLength_ShouldThrowArgumentException(): action → {Action}", action);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
			_logger.LogWarning("<<< Exiting AddPadding_WhenCalledWithZeroPaddingAndOffsetIsGreaterThanDataLength_ShouldThrowArgumentException()");
    }


    [Test]
    public void AddPadding_WhenCalledWithRandomPadding_ShouldReturnCorrectCode()
    {
		_logger.LogError(">>> Entering AddPadding_WhenCalledWithRandomPadding_ShouldReturnCorrectCode()");
        var inputData = new byte[10];
        const int inputOffset = 5;

        var result = randomPadding.AddPadding(inputData, inputOffset);
		_logger.LogError("AddPadding_WhenCalledWithRandomPadding_ShouldReturnCorrectCode(): result → {Result}", result);

        result.Should().Be(5);
		_logger.LogWarning("<<< Exiting AddPadding_WhenCalledWithRandomPadding_ShouldReturnCorrectCode()");
    }

    [Test]
    public void AddPadding_WhenCalledWithRandomPaddingAndOffsetIsEqualToDataLength_ShouldThrowArgumentException()
    {
		_logger.LogError(">>> Entering AddPadding_WhenCalledWithRandomPaddingAndOffsetIsEqualToDataLength_ShouldThrowArgumentException()");
        var inputData = new byte[10];
        const int inputOffset = 10;

        Action action = () => randomPadding.AddPadding(inputData, inputOffset);
		_logger.LogError("AddPadding_WhenCalledWithRandomPaddingAndOffsetIsEqualToDataLength_ShouldThrowArgumentException(): action → {Action}", action);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
			_logger.LogWarning("<<< Exiting AddPadding_WhenCalledWithRandomPaddingAndOffsetIsEqualToDataLength_ShouldThrowArgumentException()");
    }

    [Test]
    public void AddPadding_WhenCalledWithRandomPaddingAndOffsetIsGreaterThanDataLength_ShouldThrowArgumentException()
    {
		_logger.LogError(">>> Entering AddPadding_WhenCalledWithRandomPaddingAndOffsetIsGreaterThanDataLength_ShouldThrowArgumentException()");
        var inputData = new byte[10];
        const int inputOffset = 11;

        Action action = () => randomPadding.AddPadding(inputData, inputOffset);
		_logger.LogError("AddPadding_WhenCalledWithRandomPaddingAndOffsetIsGreaterThanDataLength_ShouldThrowArgumentException(): action → {Action}", action);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
			_logger.LogWarning("<<< Exiting AddPadding_WhenCalledWithRandomPaddingAndOffsetIsGreaterThanDataLength_ShouldThrowArgumentException()");
    }

    [Test]
    public void AddPadding_WhenCalledWithZeroPaddingAndOffsetIsZero_ShouldReturnLengthOfInputData()
    {
		_logger.LogError(">>> Entering AddPadding_WhenCalledWithZeroPaddingAndOffsetIsZero_ShouldReturnLengthOfInputData()");
        var inputData = new byte[10];
        const int inputOffset = 0;

        var result = zeroPadding.AddPadding(inputData, inputOffset);
		_logger.LogError("AddPadding_WhenCalledWithZeroPaddingAndOffsetIsZero_ShouldReturnLengthOfInputData(): result → {Result}", result);

        result.Should().Be(10);
		_logger.LogWarning("<<< Exiting AddPadding_WhenCalledWithZeroPaddingAndOffsetIsZero_ShouldReturnLengthOfInputData()");
    }

    [Test]
    public void AddPadding_WhenCalledWithRandomPaddingAndOffsetIsZero_ShouldReturnLengthOfInputData()
    {
		_logger.LogError(">>> Entering AddPadding_WhenCalledWithRandomPaddingAndOffsetIsZero_ShouldReturnLengthOfInputData()");
        var inputData = new byte[10];
        const int inputOffset = 0;

        var result = randomPadding.AddPadding(inputData, inputOffset);
		_logger.LogError("AddPadding_WhenCalledWithRandomPaddingAndOffsetIsZero_ShouldReturnLengthOfInputData(): result → {Result}", result);

        result.Should().Be(10);
		_logger.LogWarning("<<< Exiting AddPadding_WhenCalledWithRandomPaddingAndOffsetIsZero_ShouldReturnLengthOfInputData()");
    }

    [Test]
    public void AddPadding_WhenCalledWithRandomPadding_ShouldFillThePaddingWithRandomValues()
    {
		_logger.LogError(">>> Entering AddPadding_WhenCalledWithRandomPadding_ShouldFillThePaddingWithRandomValues()");
        var inputData = new byte[10];
        const int inputOffset = 5;

        var result = randomPadding.AddPadding(inputData, inputOffset);
		_logger.LogError("AddPadding_WhenCalledWithRandomPadding_ShouldFillThePaddingWithRandomValues(): result → {Result}", result);

        for (var i = inputOffset; i < inputData.Length - 1; i++)
        {
            inputData[i].Should().BeInRange(0, 255);
        }

        inputData[^1].Should().Be((byte)result);
		_logger.LogWarning("<<< Exiting AddPadding_WhenCalledWithRandomPadding_ShouldFillThePaddingWithRandomValues()");
    }

    [Test]
    public void RemovePadding_WhenCalledInEmptyArray_ShouldReturnAnEmptyArray()
    {
		_logger.LogError(">>> Entering RemovePadding_WhenCalledInEmptyArray_ShouldReturnAnEmptyArray()");
        var result = zeroPadding.RemovePadding(Array.Empty<byte>());

		_logger.LogError("RemovePadding_WhenCalledInEmptyArray_ShouldReturnAnEmptyArray(): result → {Result}", result);
        result.Should().AllBeEquivalentTo(Array.Empty<byte>());
		_logger.LogWarning("<<< Exiting RemovePadding_WhenCalledInEmptyArray_ShouldReturnAnEmptyArray()");
    }

    [Test]
    public void RemovePadding_WhenCalledOnArrayWithValidPadding_ShouldRemovePadding()
    {
		_logger.LogError(">>> Entering RemovePadding_WhenCalledOnArrayWithValidPadding_ShouldRemovePadding()");
        var inputData = new byte[] { 1, 2, 3, 2 };
        var expectedOutput = new byte[] { 1, 2 };

        var result = zeroPadding.RemovePadding(inputData);
		_logger.LogError("RemovePadding_WhenCalledOnArrayWithValidPadding_ShouldRemovePadding(): result → {Result}", result);

        result.Should().BeEquivalentTo(expectedOutput);
		_logger.LogWarning("<<< Exiting RemovePadding_WhenCalledOnArrayWithValidPadding_ShouldRemovePadding()");
    }

    [Test]
    public void RemovePadding_WithInvalidPadding_ThrowsArgumentException()
    {
		_logger.LogError(">>> Entering RemovePadding_WithInvalidPadding_ThrowsArgumentException()");
        var inputData = new byte[] { 1, 2, 3, 5 };

        Action act = () => zeroPadding.RemovePadding(inputData);
		_logger.LogError("RemovePadding_WithInvalidPadding_ThrowsArgumentException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Invalid padding length");
			_logger.LogWarning("<<< Exiting RemovePadding_WithInvalidPadding_ThrowsArgumentException()");
    }

    [Test]
    public void GetPaddingCount_WithValidPadding_ReturnsCorrectCount()
    {
		_logger.LogError(">>> Entering GetPaddingCount_WithValidPadding_ReturnsCorrectCount()");
        var inputData = new byte[] { 1, 2, 3, 2 };

        var result = zeroPadding.GetPaddingCount(inputData);
		_logger.LogError("GetPaddingCount_WithValidPadding_ReturnsCorrectCount(): result → {Result}", result);

        result.Should().Be(2);
		_logger.LogWarning("<<< Exiting GetPaddingCount_WithValidPadding_ReturnsCorrectCount()");
    }

    [Test]
    public void GetPaddingCount_WithInvalidPadding_ThrowsArgumentException()
    {
		_logger.LogError(">>> Entering GetPaddingCount_WithInvalidPadding_ThrowsArgumentException()");
        var inputData = new byte[] { 1, 2, 3, 5 };

        Action action = () => zeroPadding.GetPaddingCount(inputData);
		_logger.LogError("GetPaddingCount_WithInvalidPadding_ThrowsArgumentException(): action → {Action}", action);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Pad block corrupted");
			_logger.LogWarning("<<< Exiting GetPaddingCount_WithInvalidPadding_ThrowsArgumentException()");
    }
}
