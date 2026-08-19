using Algorithms.Crypto.Paddings;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Crypto.Paddings;

public class Iso7816D4PaddingTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly Iso7816D4Padding padding = new Iso7816D4Padding();

    [Test]
    public void AddPadding_WhenCalledWithValidInput_ShouldReturnCorrectPadding()
    {
		_logger.LogDebug(">>> Entering AddPadding_WhenCalledWithValidInput_ShouldReturnCorrectPadding()");
        var inputData = new byte[10];
        var inputOffset = 5;

        var result = padding.AddPadding(inputData, inputOffset);
		_logger.LogDebug("AddPadding_WhenCalledWithValidInput_ShouldReturnCorrectPadding(): result → {Result}", result);

        result.Should().Be(5);
        inputData[5].Should().Be(80);
        inputData.Skip(6).Should().AllBeEquivalentTo(0);
		_logger.LogDebug("<<< Exiting AddPadding_WhenCalledWithValidInput_ShouldReturnCorrectPadding()");
    }

    [Test]
    public void AddPadding_WhenCalledWithInvalidInput_ShouldThrowArgumentException()
    {
		_logger.LogDebug(">>> Entering AddPadding_WhenCalledWithInvalidInput_ShouldThrowArgumentException()");
        var inputData = new byte[10];
        var inputOffset = 11;

        Action act = () => padding.AddPadding(inputData, inputOffset);
		_logger.LogDebug("AddPadding_WhenCalledWithInvalidInput_ShouldThrowArgumentException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>()
            .WithMessage("not enough space in input array for padding");
			_logger.LogDebug("<<< Exiting AddPadding_WhenCalledWithInvalidInput_ShouldThrowArgumentException()");
    }

    [Test]
    public void AddPadding_WhenCalledWithZeroOffset_ShouldReturnCorrectPadding()
    {
		_logger.LogDebug(">>> Entering AddPadding_WhenCalledWithZeroOffset_ShouldReturnCorrectPadding()");
        var inputData = new byte[10];
        var inputOffset = 0;

        var result = padding.AddPadding(inputData, inputOffset);
		_logger.LogDebug("AddPadding_WhenCalledWithZeroOffset_ShouldReturnCorrectPadding(): result → {Result}", result);

        result.Should().Be(10);
        inputData[0].Should().Be(80);
        inputData.Skip(1).Should().AllBeEquivalentTo(0);
		_logger.LogDebug("<<< Exiting AddPadding_WhenCalledWithZeroOffset_ShouldReturnCorrectPadding()");
    }

    [Test]
    public void AddPadding_WhenCalledWithOffsetEqualToLength_ShouldThrowArgumentException()
    {
		_logger.LogDebug(">>> Entering AddPadding_WhenCalledWithOffsetEqualToLength_ShouldThrowArgumentException()");
        var inputData = new byte[10];
        var inputOffset = 10;

        Action act = () => padding.AddPadding(inputData, inputOffset);
		_logger.LogDebug("AddPadding_WhenCalledWithOffsetEqualToLength_ShouldThrowArgumentException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
			_logger.LogDebug("<<< Exiting AddPadding_WhenCalledWithOffsetEqualToLength_ShouldThrowArgumentException()");
    }

    [Test]
    public void AddPadding_WhenCalledWithEmptyArray_ShouldThrowArgumentException()
    {
		_logger.LogDebug(">>> Entering AddPadding_WhenCalledWithEmptyArray_ShouldThrowArgumentException()");
        var inputData = new byte[0];
        var inputOffset = 0;

        Action act = () => padding.AddPadding(inputData, inputOffset);
		_logger.LogDebug("AddPadding_WhenCalledWithEmptyArray_ShouldThrowArgumentException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
			_logger.LogDebug("<<< Exiting AddPadding_WhenCalledWithEmptyArray_ShouldThrowArgumentException()");
    }

    [Test]
    public void RemovePadding_WhenCalledWithValidInput_shouldReturnCorrectData()
    {
		_logger.LogDebug(">>> Entering RemovePadding_WhenCalledWithValidInput_shouldReturnCorrectData()");
        var inputData = new byte[] { 1, 2, 3, 4, 5, 0x80, 0, 0, 0 };

        var result = padding.RemovePadding(inputData);
		_logger.LogDebug("RemovePadding_WhenCalledWithValidInput_shouldReturnCorrectData(): result → {Result}", result);

        result.Should().Equal([1, 2, 3, 4, 5]);
		_logger.LogDebug("<<< Exiting RemovePadding_WhenCalledWithValidInput_shouldReturnCorrectData()");
    }

    [Test]
    public void RemovePadding_WhenCalledWithInvalidInput_ShouldThrowArgumentException()
    {
		_logger.LogDebug(">>> Entering RemovePadding_WhenCalledWithInvalidInput_ShouldThrowArgumentException()");
        var inputData = new byte[] { 1, 2, 3, 4, 5 };

        Action act = () => padding.RemovePadding(inputData);
		_logger.LogDebug("RemovePadding_WhenCalledWithInvalidInput_ShouldThrowArgumentException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Invalid padding");
			_logger.LogDebug("<<< Exiting RemovePadding_WhenCalledWithInvalidInput_ShouldThrowArgumentException()");
    }

    [Test]
    public void RemovePadding_WhenCalledWithArrayContainingOnlyPadding_ShouldReturnEmptyArray()
    {
		_logger.LogDebug(">>> Entering RemovePadding_WhenCalledWithArrayContainingOnlyPadding_ShouldReturnEmptyArray()");
        var inputData = new byte[] { 0x80, 0, 0, 0, 0 };

        var result = padding.RemovePadding(inputData);
		_logger.LogDebug("RemovePadding_WhenCalledWithArrayContainingOnlyPadding_ShouldReturnEmptyArray(): result → {Result}", result);

        result.Should().BeEmpty();
		_logger.LogDebug("<<< Exiting RemovePadding_WhenCalledWithArrayContainingOnlyPadding_ShouldReturnEmptyArray()");
    }

    [Test]
    public void RemovePadding_WhenCalledWithArrayNotContainingStartOfPadding_ShouldThrowArgumentException()
    {
		_logger.LogDebug(">>> Entering RemovePadding_WhenCalledWithArrayNotContainingStartOfPadding_ShouldThrowArgumentException()");
        var input = new byte[] { 1, 2, 3, 4, 5, 0, 0, 0, 0 };

        Action act = () => padding.RemovePadding(input);
		_logger.LogDebug("RemovePadding_WhenCalledWithArrayNotContainingStartOfPadding_ShouldThrowArgumentException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Invalid padding");
			_logger.LogDebug("<<< Exiting RemovePadding_WhenCalledWithArrayNotContainingStartOfPadding_ShouldThrowArgumentException()");
    }

    [Test]
    public void GetPaddingCount_WhenCalledWithValidInput_ShouldReturnCorrectCount()
    {
		_logger.LogDebug(">>> Entering GetPaddingCount_WhenCalledWithValidInput_ShouldReturnCorrectCount()");
        var inputData = new byte[] { 1, 2, 3, 4, 5, 0x80, 0, 0, 0 };

        var result = padding.GetPaddingCount(inputData);
		_logger.LogDebug("GetPaddingCount_WhenCalledWithValidInput_ShouldReturnCorrectCount(): result → {Result}", result);

        result.Should().Be(4);
		_logger.LogDebug("<<< Exiting GetPaddingCount_WhenCalledWithValidInput_ShouldReturnCorrectCount()");
    }

    [Test]
    public void GetPaddingCount_WhenCalledWithInvalidInput_ShouldThrowArgumentException()
    {
		_logger.LogDebug(">>> Entering GetPaddingCount_WhenCalledWithInvalidInput_ShouldThrowArgumentException()");
        var inputData = new byte[] { 1, 2, 3, 4, 5 };

        Action act = () => padding.GetPaddingCount(inputData);
		_logger.LogDebug("GetPaddingCount_WhenCalledWithInvalidInput_ShouldThrowArgumentException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Pad block corrupted");
			_logger.LogDebug("<<< Exiting GetPaddingCount_WhenCalledWithInvalidInput_ShouldThrowArgumentException()");
    }

    [Test]
    public void GetPaddingCount_WhenCalledWithEmptyArray_ShouldThrowArgumentException()
    {
		_logger.LogDebug(">>> Entering GetPaddingCount_WhenCalledWithEmptyArray_ShouldThrowArgumentException()");
        var inputData = Array.Empty<byte>();

		_logger.LogDebug("GetPaddingCount_WhenCalledWithEmptyArray_ShouldThrowArgumentException(): inputData → {InputData}", inputData);
        Action act = () => padding.GetPaddingCount(inputData);
		_logger.LogDebug("GetPaddingCount_WhenCalledWithEmptyArray_ShouldThrowArgumentException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Pad block corrupted");
			_logger.LogDebug("<<< Exiting GetPaddingCount_WhenCalledWithEmptyArray_ShouldThrowArgumentException()");
    }

    [Test]
    public void GetPaddingCount_WhenCalledWithArrayContainingOnlyPadding_ShouldReturnCorrectCount()
    {
		_logger.LogDebug(">>> Entering GetPaddingCount_WhenCalledWithArrayContainingOnlyPadding_ShouldReturnCorrectCount()");
        var inputData = new byte[] { 0x80, 0x00, 0x00 };

        var result = padding.GetPaddingCount(inputData);
		_logger.LogDebug("GetPaddingCount_WhenCalledWithArrayContainingOnlyPadding_ShouldReturnCorrectCount(): result → {Result}", result);

        result.Should().Be(3);
		_logger.LogDebug("<<< Exiting GetPaddingCount_WhenCalledWithArrayContainingOnlyPadding_ShouldReturnCorrectCount()");
    }

    [Test]
    public void GetPaddingCount_WhenCalledWithArrayNotContainingStartOfPadding_ShouldThrowAnException()
    {
		_logger.LogDebug(">>> Entering GetPaddingCount_WhenCalledWithArrayNotContainingStartOfPadding_ShouldThrowAnException()");
        var inputData = new byte[] { 1, 2, 3, 4, 5, 0, 0, 0, 0 };

        Action act = () => padding.GetPaddingCount(inputData);
		_logger.LogDebug("GetPaddingCount_WhenCalledWithArrayNotContainingStartOfPadding_ShouldThrowAnException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Pad block corrupted");
			_logger.LogDebug("<<< Exiting GetPaddingCount_WhenCalledWithArrayNotContainingStartOfPadding_ShouldThrowAnException()");
    }
}
