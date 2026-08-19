using Microsoft.Extensions.Logging;
﻿using Algorithms.Crypto.Paddings;

namespace Algorithms.Tests.Crypto.Paddings;

public class X932PaddingTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly X932Padding zeroPadding = new X932Padding(false);
    private readonly X932Padding randomPadding = new X932Padding(true);

    [Test]
    public void AddPadding_WhenCalledWithZeroPadding_ShouldReturnCorrectCode()
    {
		_logger.LogDebug(">>> Entering AddPadding_WhenCalledWithZeroPadding_ShouldReturnCorrectCode()");
        var inputData = new byte[10];
        const int inputOffset = 5;

        var result = zeroPadding.AddPadding(inputData, inputOffset);
		_logger.LogDebug("AddPadding_WhenCalledWithZeroPadding_ShouldReturnCorrectCode(): result → {Result}", result);

        result.Should().Be(5);
		_logger.LogDebug("<<< Exiting AddPadding_WhenCalledWithZeroPadding_ShouldReturnCorrectCode()");
    }

    [Test]
    public void AddPadding_WhenCalledWithZeroPaddingAndOffsetIsEqualToDataLength_ShouldThrowArgumentException()
    {
		_logger.LogDebug(">>> Entering AddPadding_WhenCalledWithZeroPaddingAndOffsetIsEqualToDataLength_ShouldThrowArgumentException()");
        var inputData = new byte[10];
        const int inputOffset = 10;

        Action action = () => zeroPadding.AddPadding(inputData, inputOffset);
		_logger.LogDebug("AddPadding_WhenCalledWithZeroPaddingAndOffsetIsEqualToDataLength_ShouldThrowArgumentException(): action → {Action}", action);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
			_logger.LogDebug("<<< Exiting AddPadding_WhenCalledWithZeroPaddingAndOffsetIsEqualToDataLength_ShouldThrowArgumentException()");
    }

    [Test]
    public void AddPadding_WhenCalledWithZeroPaddingAndOffsetIsGreaterThanDataLength_ShouldThrowArgumentException()
    {
		_logger.LogDebug(">>> Entering AddPadding_WhenCalledWithZeroPaddingAndOffsetIsGreaterThanDataLength_ShouldThrowArgumentException()");
        var inputData = new byte[10];
        const int inputOffset = 11;

        Action action = () => zeroPadding.AddPadding(inputData, inputOffset);
		_logger.LogDebug("AddPadding_WhenCalledWithZeroPaddingAndOffsetIsGreaterThanDataLength_ShouldThrowArgumentException(): action → {Action}", action);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
			_logger.LogDebug("<<< Exiting AddPadding_WhenCalledWithZeroPaddingAndOffsetIsGreaterThanDataLength_ShouldThrowArgumentException()");
    }


    [Test]
    public void AddPadding_WhenCalledWithRandomPadding_ShouldReturnCorrectCode()
    {
		_logger.LogDebug(">>> Entering AddPadding_WhenCalledWithRandomPadding_ShouldReturnCorrectCode()");
        var inputData = new byte[10];
        const int inputOffset = 5;

        var result = randomPadding.AddPadding(inputData, inputOffset);
		_logger.LogDebug("AddPadding_WhenCalledWithRandomPadding_ShouldReturnCorrectCode(): result → {Result}", result);

        result.Should().Be(5);
		_logger.LogDebug("<<< Exiting AddPadding_WhenCalledWithRandomPadding_ShouldReturnCorrectCode()");
    }

    [Test]
    public void AddPadding_WhenCalledWithRandomPaddingAndOffsetIsEqualToDataLength_ShouldThrowArgumentException()
    {
		_logger.LogDebug(">>> Entering AddPadding_WhenCalledWithRandomPaddingAndOffsetIsEqualToDataLength_ShouldThrowArgumentException()");
        var inputData = new byte[10];
        const int inputOffset = 10;

        Action action = () => randomPadding.AddPadding(inputData, inputOffset);
		_logger.LogDebug("AddPadding_WhenCalledWithRandomPaddingAndOffsetIsEqualToDataLength_ShouldThrowArgumentException(): action → {Action}", action);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
			_logger.LogDebug("<<< Exiting AddPadding_WhenCalledWithRandomPaddingAndOffsetIsEqualToDataLength_ShouldThrowArgumentException()");
    }

    [Test]
    public void AddPadding_WhenCalledWithRandomPaddingAndOffsetIsGreaterThanDataLength_ShouldThrowArgumentException()
    {
		_logger.LogDebug(">>> Entering AddPadding_WhenCalledWithRandomPaddingAndOffsetIsGreaterThanDataLength_ShouldThrowArgumentException()");
        var inputData = new byte[10];
        const int inputOffset = 11;

        Action action = () => randomPadding.AddPadding(inputData, inputOffset);
		_logger.LogDebug("AddPadding_WhenCalledWithRandomPaddingAndOffsetIsGreaterThanDataLength_ShouldThrowArgumentException(): action → {Action}", action);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
			_logger.LogDebug("<<< Exiting AddPadding_WhenCalledWithRandomPaddingAndOffsetIsGreaterThanDataLength_ShouldThrowArgumentException()");
    }

    [Test]
    public void AddPadding_WhenCalledWithZeroPaddingAndOffsetIsZero_ShouldReturnLengthOfInputData()
    {
		_logger.LogDebug(">>> Entering AddPadding_WhenCalledWithZeroPaddingAndOffsetIsZero_ShouldReturnLengthOfInputData()");
        var inputData = new byte[10];
        const int inputOffset = 0;

        var result = zeroPadding.AddPadding(inputData, inputOffset);
		_logger.LogDebug("AddPadding_WhenCalledWithZeroPaddingAndOffsetIsZero_ShouldReturnLengthOfInputData(): result → {Result}", result);

        result.Should().Be(10);
		_logger.LogDebug("<<< Exiting AddPadding_WhenCalledWithZeroPaddingAndOffsetIsZero_ShouldReturnLengthOfInputData()");
    }

    [Test]
    public void AddPadding_WhenCalledWithRandomPaddingAndOffsetIsZero_ShouldReturnLengthOfInputData()
    {
		_logger.LogDebug(">>> Entering AddPadding_WhenCalledWithRandomPaddingAndOffsetIsZero_ShouldReturnLengthOfInputData()");
        var inputData = new byte[10];
        const int inputOffset = 0;

        var result = randomPadding.AddPadding(inputData, inputOffset);
		_logger.LogDebug("AddPadding_WhenCalledWithRandomPaddingAndOffsetIsZero_ShouldReturnLengthOfInputData(): result → {Result}", result);

        result.Should().Be(10);
		_logger.LogDebug("<<< Exiting AddPadding_WhenCalledWithRandomPaddingAndOffsetIsZero_ShouldReturnLengthOfInputData()");
    }

    [Test]
    public void AddPadding_WhenCalledWithRandomPadding_ShouldFillThePaddingWithRandomValues()
    {
		_logger.LogDebug(">>> Entering AddPadding_WhenCalledWithRandomPadding_ShouldFillThePaddingWithRandomValues()");
        var inputData = new byte[10];
        const int inputOffset = 5;

        var result = randomPadding.AddPadding(inputData, inputOffset);
		_logger.LogDebug("AddPadding_WhenCalledWithRandomPadding_ShouldFillThePaddingWithRandomValues(): result → {Result}", result);

        for (var i = inputOffset; i < inputData.Length - 1; i++)
        {
            inputData[i].Should().BeInRange(0, 255);
        }

        inputData[^1].Should().Be((byte)result);
		_logger.LogDebug("<<< Exiting AddPadding_WhenCalledWithRandomPadding_ShouldFillThePaddingWithRandomValues()");
    }

    [Test]
    public void RemovePadding_WhenCalledInEmptyArray_ShouldReturnAnEmptyArray()
    {
		_logger.LogDebug(">>> Entering RemovePadding_WhenCalledInEmptyArray_ShouldReturnAnEmptyArray()");
        var result = zeroPadding.RemovePadding(Array.Empty<byte>());

		_logger.LogDebug("RemovePadding_WhenCalledInEmptyArray_ShouldReturnAnEmptyArray(): result → {Result}", result);
        result.Should().AllBeEquivalentTo(Array.Empty<byte>());
		_logger.LogDebug("<<< Exiting RemovePadding_WhenCalledInEmptyArray_ShouldReturnAnEmptyArray()");
    }

    [Test]
    public void RemovePadding_WhenCalledOnArrayWithValidPadding_ShouldRemovePadding()
    {
		_logger.LogDebug(">>> Entering RemovePadding_WhenCalledOnArrayWithValidPadding_ShouldRemovePadding()");
        var inputData = new byte[] { 1, 2, 3, 2 };
        var expectedOutput = new byte[] { 1, 2 };

        var result = zeroPadding.RemovePadding(inputData);
		_logger.LogDebug("RemovePadding_WhenCalledOnArrayWithValidPadding_ShouldRemovePadding(): result → {Result}", result);

        result.Should().BeEquivalentTo(expectedOutput);
		_logger.LogDebug("<<< Exiting RemovePadding_WhenCalledOnArrayWithValidPadding_ShouldRemovePadding()");
    }

    [Test]
    public void RemovePadding_WithInvalidPadding_ThrowsArgumentException()
    {
		_logger.LogDebug(">>> Entering RemovePadding_WithInvalidPadding_ThrowsArgumentException()");
        var inputData = new byte[] { 1, 2, 3, 5 };

        Action act = () => zeroPadding.RemovePadding(inputData);
		_logger.LogDebug("RemovePadding_WithInvalidPadding_ThrowsArgumentException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Invalid padding length");
			_logger.LogDebug("<<< Exiting RemovePadding_WithInvalidPadding_ThrowsArgumentException()");
    }

    [Test]
    public void GetPaddingCount_WithValidPadding_ReturnsCorrectCount()
    {
		_logger.LogDebug(">>> Entering GetPaddingCount_WithValidPadding_ReturnsCorrectCount()");
        var inputData = new byte[] { 1, 2, 3, 2 };

        var result = zeroPadding.GetPaddingCount(inputData);
		_logger.LogDebug("GetPaddingCount_WithValidPadding_ReturnsCorrectCount(): result → {Result}", result);

        result.Should().Be(2);
		_logger.LogDebug("<<< Exiting GetPaddingCount_WithValidPadding_ReturnsCorrectCount()");
    }

    [Test]
    public void GetPaddingCount_WithInvalidPadding_ThrowsArgumentException()
    {
		_logger.LogDebug(">>> Entering GetPaddingCount_WithInvalidPadding_ThrowsArgumentException()");
        var inputData = new byte[] { 1, 2, 3, 5 };

        Action action = () => zeroPadding.GetPaddingCount(inputData);
		_logger.LogDebug("GetPaddingCount_WithInvalidPadding_ThrowsArgumentException(): action → {Action}", action);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Pad block corrupted");
			_logger.LogDebug("<<< Exiting GetPaddingCount_WithInvalidPadding_ThrowsArgumentException()");
    }
}
