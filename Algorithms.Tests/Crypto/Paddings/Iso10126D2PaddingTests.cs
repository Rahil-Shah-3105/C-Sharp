using Microsoft.Extensions.Logging;
﻿using Algorithms.Crypto.Paddings;

namespace Algorithms.Tests.Crypto.Paddings;

public class Iso10126D2PaddingTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly Iso10126D2Padding padding = new Iso10126D2Padding();

    [Test]
    public void AddPadding_WhenInputOffsetIsLessThanInputDataLength_ShouldNotThrowException()
    {
		_logger.LogDebug(">>> Entering AddPadding_WhenInputOffsetIsLessThanInputDataLength_ShouldNotThrowException()");
        var inputData = new byte[10];
        const int inputOffset = 5;

        Action act = () => padding.AddPadding(inputData, inputOffset);
		_logger.LogDebug("AddPadding_WhenInputOffsetIsLessThanInputDataLength_ShouldNotThrowException(): act → {Act}", act);

        act.Should().NotThrow<ArgumentException>();
		_logger.LogDebug("<<< Exiting AddPadding_WhenInputOffsetIsLessThanInputDataLength_ShouldNotThrowException()");
    }

    [Test]
    public void AddPadding_WhenInputOffsetIsEqualToInputDataLength_ShouldThrowException()
    {
		_logger.LogDebug(">>> Entering AddPadding_WhenInputOffsetIsEqualToInputDataLength_ShouldThrowException()");
        var inputData = new byte[10];
        const int inputOffset = 10;

        Action act = () => padding.AddPadding(inputData, inputOffset);
		_logger.LogDebug("AddPadding_WhenInputOffsetIsEqualToInputDataLength_ShouldThrowException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
			_logger.LogDebug("<<< Exiting AddPadding_WhenInputOffsetIsEqualToInputDataLength_ShouldThrowException()");
    }

    [Test]
    public void AddPadding_WhenInputOffsetIsGreaterThanInputDataLength_ShouldThrowException()
    {
		_logger.LogDebug(">>> Entering AddPadding_WhenInputOffsetIsGreaterThanInputDataLength_ShouldThrowException()");
        var inputData = new byte[10];
        const int inputOffset = 128;

        Action act = () => padding.AddPadding(inputData, inputOffset);
		_logger.LogDebug("AddPadding_WhenInputOffsetIsGreaterThanInputDataLength_ShouldThrowException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
			_logger.LogDebug("<<< Exiting AddPadding_WhenInputOffsetIsGreaterThanInputDataLength_ShouldThrowException()");
    }

    [Test]
    public void AddPadding_WhenInputArrayIsValid_ShouldReturnCorrectPaddingSize()
    {
		_logger.LogDebug(">>> Entering AddPadding_WhenInputArrayIsValid_ShouldReturnCorrectPaddingSize()");
        var inputData = new byte[10];
        const int inputOffset = 5;

        var result = padding.AddPadding(inputData, inputOffset);
		_logger.LogDebug("AddPadding_WhenInputArrayIsValid_ShouldReturnCorrectPaddingSize(): result → {Result}", result);

        result.Should().Be(5);
		_logger.LogDebug("<<< Exiting AddPadding_WhenInputArrayIsValid_ShouldReturnCorrectPaddingSize()");
    }

    [Test]
    public void RemovePadding_WhenLengthIsLessThanOne_ShouldThrowATantrum()
    {
		_logger.LogDebug(">>> Entering RemovePadding_WhenLengthIsLessThanOne_ShouldThrowATantrum()");
        var inputData = new byte[] { 0 };

        Action act = () => padding.RemovePadding(inputData);
		_logger.LogDebug("RemovePadding_WhenLengthIsLessThanOne_ShouldThrowATantrum(): act → {Act}", act);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Invalid padding length");
			_logger.LogDebug("<<< Exiting RemovePadding_WhenLengthIsLessThanOne_ShouldThrowATantrum()");
    }

    [Test]
    public void RemovePadding_WhenPaddingLengthIsGreaterThanInputDataLength_ShouldThrowAnException()
    {
		_logger.LogDebug(">>> Entering RemovePadding_WhenPaddingLengthIsGreaterThanInputDataLength_ShouldThrowAnException()");
        var inputData = new byte[] { 2 };

        Action act = () => padding.RemovePadding(inputData);
		_logger.LogDebug("RemovePadding_WhenPaddingLengthIsGreaterThanInputDataLength_ShouldThrowAnException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Invalid padding length");
			_logger.LogDebug("<<< Exiting RemovePadding_WhenPaddingLengthIsGreaterThanInputDataLength_ShouldThrowAnException()");
    }

    [Test]
    public void RemovePadding_WhenInputDataIsValid_ShouldReturnCorrectData()
    {
		_logger.LogDebug(">>> Entering RemovePadding_WhenInputDataIsValid_ShouldReturnCorrectData()");
        var inputData = new byte[] { 1, 2, 3, 1 };
        var expected = new byte[] { 1, 2, 3 };

        var result = padding.RemovePadding(inputData);
		_logger.LogDebug("RemovePadding_WhenInputDataIsValid_ShouldReturnCorrectData(): result → {Result}", result);

        result.Should().Equal(expected);
		_logger.LogDebug("<<< Exiting RemovePadding_WhenInputDataIsValid_ShouldReturnCorrectData()");
    }

    [Test]
    public void GetPaddingCount_WhenInputIsNull_ShouldThrowAnException()
    {
		_logger.LogDebug(">>> Entering GetPaddingCount_WhenInputIsNull_ShouldThrowAnException()");
        byte[]? input = null;

        Action act = () => padding.GetPaddingCount(input!);
		_logger.LogDebug("GetPaddingCount_WhenInputIsNull_ShouldThrowAnException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Input cannot be null (Parameter 'input')");
			_logger.LogDebug("<<< Exiting GetPaddingCount_WhenInputIsNull_ShouldThrowAnException()");
    }

    [Test]
    public void GetPaddingCount_WhenPaddingBlockIsCorrupted_ShouldThrowAnException()
    {
		_logger.LogDebug(">>> Entering GetPaddingCount_WhenPaddingBlockIsCorrupted_ShouldThrowAnException()");
        var input = new byte[] { 1, 2, 3, 4, 5, 7 };

        Action act = () => padding.GetPaddingCount(input);
		_logger.LogDebug("GetPaddingCount_WhenPaddingBlockIsCorrupted_ShouldThrowAnException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Padding block is corrupted");
			_logger.LogDebug("<<< Exiting GetPaddingCount_WhenPaddingBlockIsCorrupted_ShouldThrowAnException()");
    }

    [Test]
    public void GetPaddingCount_WhenInputDataIsValid_ShouldReturnCorrectPaddingCount()
    {
		_logger.LogDebug(">>> Entering GetPaddingCount_WhenInputDataIsValid_ShouldReturnCorrectPaddingCount()");
        var input = new byte[] { 1, 2, 3, 4, 1 };

        var result = padding.GetPaddingCount(input);
		_logger.LogDebug("GetPaddingCount_WhenInputDataIsValid_ShouldReturnCorrectPaddingCount(): result → {Result}", result);

        result.Should().Be(1);
		_logger.LogDebug("<<< Exiting GetPaddingCount_WhenInputDataIsValid_ShouldReturnCorrectPaddingCount()");
    }
}
