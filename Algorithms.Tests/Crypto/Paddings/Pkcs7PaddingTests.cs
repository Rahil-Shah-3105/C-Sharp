using Microsoft.Extensions.Logging;
﻿using Algorithms.Crypto.Paddings;

namespace Algorithms.Tests.Crypto.Paddings;

public class Pkcs7PaddingTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private const int DefaultBlockSize = 16;

    [Test]
    public void Constructor_WhenBlockSizeIsLessThanOne_ShouldThrowArgumentOutOfRangeException()
    {
		_logger.LogDebug(">>> Entering Constructor_WhenBlockSizeIsLessThanOne_ShouldThrowArgumentOutOfRangeException()");
        const int blockSize = 0;

        Action act = () => new Pkcs7Padding(blockSize);
		_logger.LogDebug("Constructor_WhenBlockSizeIsLessThanOne_ShouldThrowArgumentOutOfRangeException(): act → {Act}", act);

        act.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Invalid block size: 0 (Parameter 'blockSize')");
			_logger.LogDebug("<<< Exiting Constructor_WhenBlockSizeIsLessThanOne_ShouldThrowArgumentOutOfRangeException()");
    }

    [Test]
    public void Constructor_WhenBlockSizeIsMoreThan255_ShouldThrowArgumentOutOfRangeException()
    {
		_logger.LogDebug(">>> Entering Constructor_WhenBlockSizeIsMoreThan255_ShouldThrowArgumentOutOfRangeException()");
        const int blockSize = 256;

        Action act = () => new Pkcs7Padding(blockSize);
		_logger.LogDebug("Constructor_WhenBlockSizeIsMoreThan255_ShouldThrowArgumentOutOfRangeException(): act → {Act}", act);

        act.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Invalid block size: 256 (Parameter 'blockSize')");
			_logger.LogDebug("<<< Exiting Constructor_WhenBlockSizeIsMoreThan255_ShouldThrowArgumentOutOfRangeException()");
    }

    [Test]
    public void Constructor_WhenBlockSizeIsWithinValidRange_ShouldNotThrowAFit()
    {
		_logger.LogDebug(">>> Entering Constructor_WhenBlockSizeIsWithinValidRange_ShouldNotThrowAFit()");
        const int blockSize = 128;

        Action act = () => new Pkcs7Padding(blockSize);
		_logger.LogDebug("Constructor_WhenBlockSizeIsWithinValidRange_ShouldNotThrowAFit(): act → {Act}", act);

        act.Should().NotThrow();
		_logger.LogDebug("<<< Exiting Constructor_WhenBlockSizeIsWithinValidRange_ShouldNotThrowAFit()");
    }

    [Test]
    public void AddPadding_WhenNotEnoughSpaceInInputArrayForPadding_ShouldThrowArgumentException()
    {
		_logger.LogDebug(">>> Entering AddPadding_WhenNotEnoughSpaceInInputArrayForPadding_ShouldThrowArgumentException()");
        var padding = new Pkcs7Padding(DefaultBlockSize);
        const int inputOffset = 1;
		_logger.LogDebug("AddPadding_WhenNotEnoughSpaceInInputArrayForPadding_ShouldThrowArgumentException(): padding → {Padding}", padding);

        var size16Input = new byte[16];

        Action act = () => padding.AddPadding(size16Input, inputOffset);
		_logger.LogDebug("AddPadding_WhenNotEnoughSpaceInInputArrayForPadding_ShouldThrowArgumentException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
			_logger.LogDebug("<<< Exiting AddPadding_WhenNotEnoughSpaceInInputArrayForPadding_ShouldThrowArgumentException()");
    }

    [Test]
    public void AddPadding_WhenInputArrayHasEnoughSpace_ShouldReturnCorrectPaddingSize()
    {
		_logger.LogDebug(">>> Entering AddPadding_WhenInputArrayHasEnoughSpace_ShouldReturnCorrectPaddingSize()");
        var padding = new Pkcs7Padding(DefaultBlockSize);
        const int inputOffset = DefaultBlockSize;
		_logger.LogDebug("AddPadding_WhenInputArrayHasEnoughSpace_ShouldReturnCorrectPaddingSize(): padding → {Padding}", padding);

        var size32Input = new byte[32];

        var result = padding.AddPadding(size32Input, inputOffset);
		_logger.LogDebug("AddPadding_WhenInputArrayHasEnoughSpace_ShouldReturnCorrectPaddingSize(): result → {Result}", result);

        result.Should().Be(DefaultBlockSize);
		_logger.LogDebug("<<< Exiting AddPadding_WhenInputArrayHasEnoughSpace_ShouldReturnCorrectPaddingSize()");
    }

    [Test]
    public void AddPadding_WhenAppliedToAnInputArray_ShouldAddCorrectPKCS7Padding()
    {
		_logger.LogDebug(">>> Entering AddPadding_WhenAppliedToAnInputArray_ShouldAddCorrectPKCS7Padding()");
        var padding = new Pkcs7Padding(DefaultBlockSize);
        const int inputOffset = DefaultBlockSize;
		_logger.LogDebug("AddPadding_WhenAppliedToAnInputArray_ShouldAddCorrectPKCS7Padding(): padding → {Padding}", padding);

        var size32Input = new byte[32];

        padding.AddPadding(size32Input, inputOffset);

        for (var i = 0; i < DefaultBlockSize - 1; i++)
        {
            size32Input[inputOffset + i].Should().Be(DefaultBlockSize);
        }
		_logger.LogDebug("<<< Exiting AddPadding_WhenAppliedToAnInputArray_ShouldAddCorrectPKCS7Padding()");
    }

    [Test]
    public void RemovePadding_WhenAppliedToAValidInputArray_ShouldRemovePKCS7PaddingCorrectly()
    {
		_logger.LogDebug(">>> Entering RemovePadding_WhenAppliedToAValidInputArray_ShouldRemovePKCS7PaddingCorrectly()");
        var paddingSize = 5;
        var size32Input = new byte[32];
        for (var i = 0; i < paddingSize; i++)
        {
            size32Input[size32Input.Length - 1 - i] = (byte)paddingSize;
        }

        var padding = new Pkcs7Padding(DefaultBlockSize);
		_logger.LogDebug("RemovePadding_WhenAppliedToAValidInputArray_ShouldRemovePKCS7PaddingCorrectly(): padding → {Padding}", padding);

        var output = padding.RemovePadding(size32Input);
		_logger.LogDebug("RemovePadding_WhenAppliedToAValidInputArray_ShouldRemovePKCS7PaddingCorrectly(): output → {Output}", output);

        output.Length.Should().Be(size32Input.Length - paddingSize);
		_logger.LogDebug("<<< Exiting RemovePadding_WhenAppliedToAValidInputArray_ShouldRemovePKCS7PaddingCorrectly()");
    }

    [Test]
    public void RemovePadding_WhenInputLengthNotMultipleOfBlockSize_ShouldThrowArgumentException()
    {
		_logger.LogDebug(">>> Entering RemovePadding_WhenInputLengthNotMultipleOfBlockSize_ShouldThrowArgumentException()");
        var input = new byte[DefaultBlockSize + 1]; // Length is not a multiple of blockSize
        var padding = new Pkcs7Padding(DefaultBlockSize);
		_logger.LogDebug("RemovePadding_WhenInputLengthNotMultipleOfBlockSize_ShouldThrowArgumentException(): padding → {Padding}", padding);

        Action act = () => padding.RemovePadding(input);
		_logger.LogDebug("RemovePadding_WhenInputLengthNotMultipleOfBlockSize_ShouldThrowArgumentException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Input length must be a multiple of block size");
			_logger.LogDebug("<<< Exiting RemovePadding_WhenInputLengthNotMultipleOfBlockSize_ShouldThrowArgumentException()");
    }

    [Test]
    public void RemovePadding_WhenInvalidPaddingLength_ShouldThrowArgumentException()
    {
		_logger.LogDebug(">>> Entering RemovePadding_WhenInvalidPaddingLength_ShouldThrowArgumentException()");
        var size32Input = new byte[32];

        size32Input[^1] = (byte)(DefaultBlockSize + 1); // Set invalid padding length
        var padding = new Pkcs7Padding(DefaultBlockSize);
		_logger.LogDebug("RemovePadding_WhenInvalidPaddingLength_ShouldThrowArgumentException(): padding → {Padding}", padding);

        Action act = () => padding.RemovePadding(size32Input);
		_logger.LogDebug("RemovePadding_WhenInvalidPaddingLength_ShouldThrowArgumentException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>().WithMessage("Invalid padding length");
		_logger.LogDebug("<<< Exiting RemovePadding_WhenInvalidPaddingLength_ShouldThrowArgumentException()");
    }

    [Test]
    public void RemovePadding_WhenInvalidPadding_ShouldThrowArgumentException()
    {
		_logger.LogDebug(">>> Entering RemovePadding_WhenInvalidPadding_ShouldThrowArgumentException()");
        var size32Input = new byte[32];

        size32Input[^1] = (byte)(DefaultBlockSize); // Set valid padding length
        size32Input[^2] = (byte)(DefaultBlockSize - 1); // Set invalid padding byte
        var padding = new Pkcs7Padding(DefaultBlockSize);
		_logger.LogDebug("RemovePadding_WhenInvalidPadding_ShouldThrowArgumentException(): padding → {Padding}", padding);

        Action act = () => padding.RemovePadding(size32Input);
		_logger.LogDebug("RemovePadding_WhenInvalidPadding_ShouldThrowArgumentException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Invalid padding");
			_logger.LogDebug("<<< Exiting RemovePadding_WhenInvalidPadding_ShouldThrowArgumentException()");
    }

    [Test]
    public void GetPaddingCount_WhenArrayIsNull_ShouldThrowArgumentNullException()
    {
		_logger.LogDebug(">>> Entering GetPaddingCount_WhenArrayIsNull_ShouldThrowArgumentNullException()");
        var padding = new Pkcs7Padding(DefaultBlockSize);

		_logger.LogDebug("GetPaddingCount_WhenArrayIsNull_ShouldThrowArgumentNullException(): padding → {Padding}", padding);
        Action act = () => padding.GetPaddingCount(null!);
		_logger.LogDebug("GetPaddingCount_WhenArrayIsNull_ShouldThrowArgumentNullException(): act → {Act}", act);

        act.Should().Throw<ArgumentNullException>();
		_logger.LogDebug("<<< Exiting GetPaddingCount_WhenArrayIsNull_ShouldThrowArgumentNullException()");
    }

    [Test]
    public void GetPaddingCount_WhenInputArrayIsValid_ShouldReturnCorrectPaddingCount()
    {
		_logger.LogDebug(">>> Entering GetPaddingCount_WhenInputArrayIsValid_ShouldReturnCorrectPaddingCount()");
        const int paddingSize = 5;
        var size32Input = new byte[32];

        for (var i = 0; i < paddingSize; i++)
        {
            size32Input[size32Input.Length - 1 - i] = (byte)paddingSize; // Add padding bytes at the end of the array
        }

        var padding = new Pkcs7Padding(DefaultBlockSize);
		_logger.LogDebug("GetPaddingCount_WhenInputArrayIsValid_ShouldReturnCorrectPaddingCount(): padding → {Padding}", padding);

        var output = padding.GetPaddingCount(size32Input);
		_logger.LogDebug("GetPaddingCount_WhenInputArrayIsValid_ShouldReturnCorrectPaddingCount(): output → {Output}", output);

        output.Should().Be(paddingSize);
		_logger.LogDebug("<<< Exiting GetPaddingCount_WhenInputArrayIsValid_ShouldReturnCorrectPaddingCount()");
    }

    [Test]
    public void GetPaddingCount_WhenInvalidPadding_ShouldThrowArgumentException()
    {
		_logger.LogDebug(">>> Entering GetPaddingCount_WhenInvalidPadding_ShouldThrowArgumentException()");
        var size32Input = new byte[32];

        size32Input[^1] = DefaultBlockSize;
        size32Input[^2] = DefaultBlockSize - 1;

        var padding = new Pkcs7Padding(DefaultBlockSize);
		_logger.LogDebug("GetPaddingCount_WhenInvalidPadding_ShouldThrowArgumentException(): padding → {Padding}", padding);

        Action act = () => padding.GetPaddingCount(size32Input);
		_logger.LogDebug("GetPaddingCount_WhenInvalidPadding_ShouldThrowArgumentException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Padding block is corrupted");
			_logger.LogDebug("<<< Exiting GetPaddingCount_WhenInvalidPadding_ShouldThrowArgumentException()");
    }
}
