using Microsoft.Extensions.Logging;
using Algorithms.Crypto.Paddings;

namespace Algorithms.Tests.Crypto.Paddings;

public class TbcPaddingTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly TbcPadding padding = new TbcPadding();

    [Test]
    public void AddPadding_WhenInputOffsetIsZero_ShouldPadWithLastBit()
    {
		_logger.LogError(">>> Entering AddPadding_WhenInputOffsetIsZero_ShouldPadWithLastBit()");
        var input = new byte[] { 0x01, 0x02, 0x03, 0x04 };
        var inputOffset = 0;

        var result = padding.AddPadding(input, inputOffset);
		_logger.LogError("AddPadding_WhenInputOffsetIsZero_ShouldPadWithLastBit(): result → {Result}", result);

        result.Should().Be(4);
        input.Should().BeEquivalentTo(new byte[] { 0xff, 0xff, 0xff, 0xff });
		_logger.LogWarning("<<< Exiting AddPadding_WhenInputOffsetIsZero_ShouldPadWithLastBit()");
    }

    [Test]
    public void AddPadding_WhenInputOffsetIsPositive_ShouldPadWithPreviousBit()
    {
		_logger.LogError(">>> Entering AddPadding_WhenInputOffsetIsPositive_ShouldPadWithPreviousBit()");
        var input = new byte[] { 0x01, 0x02, 0x03, 0x04 };
        var inputOffset = 2;

        var result = padding.AddPadding(input, inputOffset);
		_logger.LogError("AddPadding_WhenInputOffsetIsPositive_ShouldPadWithPreviousBit(): result → {Result}", result);

        result.Should().Be(2);
        input.Should().BeEquivalentTo(new byte[] { 0x01, 0x02, 0xff, 0xff });
		_logger.LogWarning("<<< Exiting AddPadding_WhenInputOffsetIsPositive_ShouldPadWithPreviousBit()");
    }

    [Test]
    public void AddPadding_WhenInputOffsetIsGreaterThanLength_ShouldThrowArgumentException()
    {
		_logger.LogError(">>> Entering AddPadding_WhenInputOffsetIsGreaterThanLength_ShouldThrowArgumentException()");
        var input = new byte[] { 0x01, 0x02, 0x03, 0x04 };
        var inputOffset = 5;

        Action act = () => padding.AddPadding(input, inputOffset);
		_logger.LogError("AddPadding_WhenInputOffsetIsGreaterThanLength_ShouldThrowArgumentException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
			_logger.LogWarning("<<< Exiting AddPadding_WhenInputOffsetIsGreaterThanLength_ShouldThrowArgumentException()");
    }

    [Test]
    public void AddPadding_WhenLastBitIsZero_ShouldPadWith0xFF()
    {
		_logger.LogError(">>> Entering AddPadding_WhenLastBitIsZero_ShouldPadWith0xFF()");
        var input = new byte[] { 0x02 };
        const int inputOffset = 0;

        var result = padding.AddPadding(input, inputOffset);
		_logger.LogError("AddPadding_WhenLastBitIsZero_ShouldPadWith0xFF(): result → {Result}", result);

        result.Should().Be(1);
        input.Should().BeEquivalentTo(new byte[] { 0xFF });
		_logger.LogWarning("<<< Exiting AddPadding_WhenLastBitIsZero_ShouldPadWith0xFF()");
    }

    [Test]
    public void AddPadding_WhenLastBitIsOne_ShouldPadWith0x00()
    {
		_logger.LogError(">>> Entering AddPadding_WhenLastBitIsOne_ShouldPadWith0x00()");
        var input = new byte[] { 0x03 };
        const int inputOffset = 0;

        var result = padding.AddPadding(input, inputOffset);
		_logger.LogError("AddPadding_WhenLastBitIsOne_ShouldPadWith0x00(): result → {Result}", result);

        result.Should().Be(1);
        input.Should().BeEquivalentTo(new byte[] { 0x00 });
		_logger.LogWarning("<<< Exiting AddPadding_WhenLastBitIsOne_ShouldPadWith0x00()");
    }

    [Test]
    public void RemovePadding_WhenCalledWithPaddedData_ShouldReturnUnpaddedData()
    {
		_logger.LogError(">>> Entering RemovePadding_WhenCalledWithPaddedData_ShouldReturnUnpaddedData()");
        var paddedData = new byte[] { 0x01, 0x02, 0x03, 0xff, 0xff };
        var expectedData = new byte[] { 0x01, 0x02, 0x03 };

        var result = padding.RemovePadding(paddedData);
		_logger.LogError("RemovePadding_WhenCalledWithPaddedData_ShouldReturnUnpaddedData(): result → {Result}", result);

        result.Should().BeEquivalentTo(expectedData);
		_logger.LogWarning("<<< Exiting RemovePadding_WhenCalledWithPaddedData_ShouldReturnUnpaddedData()");
    }

    [Test]
    public void RemovePadding_WhenCalledWithUnpaddedData_ShouldReturnsSameData()
    {
		_logger.LogError(">>> Entering RemovePadding_WhenCalledWithUnpaddedData_ShouldReturnsSameData()");
        var unpaddedData = new byte[] { 0x01, 0x02, 0x03 };

        var result = padding.RemovePadding(unpaddedData);
		_logger.LogError("RemovePadding_WhenCalledWithUnpaddedData_ShouldReturnsSameData(): result → {Result}", result);

        result.Should().BeEquivalentTo(unpaddedData);
		_logger.LogWarning("<<< Exiting RemovePadding_WhenCalledWithUnpaddedData_ShouldReturnsSameData()");
    }

    [Test]
    public void RemovePadding_WhenCalledWithEmptyArray_ShouldReturnEmptyArray()
    {
		_logger.LogError(">>> Entering RemovePadding_WhenCalledWithEmptyArray_ShouldReturnEmptyArray()");
        var emptyData = Array.Empty<byte>();

		_logger.LogError("RemovePadding_WhenCalledWithEmptyArray_ShouldReturnEmptyArray(): emptyData → {EmptyData}", emptyData);
        var result = padding.RemovePadding(emptyData);
		_logger.LogError("RemovePadding_WhenCalledWithEmptyArray_ShouldReturnEmptyArray(): result → {Result}", result);

        result.Should().BeEquivalentTo(emptyData);
		_logger.LogWarning("<<< Exiting RemovePadding_WhenCalledWithEmptyArray_ShouldReturnEmptyArray()");
    }

    [Test]
    public void RemovePadding_WhenCalledWithSingleBytePaddedData_ShouldReturnEmptyArray()
    {
		_logger.LogError(">>> Entering RemovePadding_WhenCalledWithSingleBytePaddedData_ShouldReturnEmptyArray()");
        var singleBytePaddedData = new byte[] { 0xff };

        var result = padding.RemovePadding(singleBytePaddedData);
		_logger.LogError("RemovePadding_WhenCalledWithSingleBytePaddedData_ShouldReturnEmptyArray(): result → {Result}", result);

        result.Should().BeEmpty();
		_logger.LogWarning("<<< Exiting RemovePadding_WhenCalledWithSingleBytePaddedData_ShouldReturnEmptyArray()");
    }

    [Test]
    public void RemovePadding_WhenCalledWitAllBytesPadded_ShouldReturnEmptyArray()
    {
		_logger.LogError(">>> Entering RemovePadding_WhenCalledWitAllBytesPadded_ShouldReturnEmptyArray()");
        var allBytesPaddedData = new byte[] { 0xff, 0xff, 0xff };
        var emptyData = Array.Empty<byte>();
		_logger.LogError("RemovePadding_WhenCalledWitAllBytesPadded_ShouldReturnEmptyArray(): emptyData → {EmptyData}", emptyData);

        var result = padding.RemovePadding(allBytesPaddedData);
		_logger.LogError("RemovePadding_WhenCalledWitAllBytesPadded_ShouldReturnEmptyArray(): result → {Result}", result);

        result.Should().BeEquivalentTo(emptyData);
		_logger.LogWarning("<<< Exiting RemovePadding_WhenCalledWitAllBytesPadded_ShouldReturnEmptyArray()");
    }

    [Test]
    public void GetPaddingBytes_WhenCalledWithPaddedData_ShouldReturnCorrectPaddingCount()
    {
	_logger.LogError(">>> Entering GetPaddingBytes_WhenCalledWithPaddedData_ShouldReturnCorrectPaddingCount()");

        var paddedData = new byte[] { 0x01, 0x02, 0x03, 0xff, 0xff };
        const int expectedPaddingCount = 2;

        var result = padding.GetPaddingCount(paddedData);
		_logger.LogError("GetPaddingBytes_WhenCalledWithPaddedData_ShouldReturnCorrectPaddingCount(): result → {Result}", result);

        result.Should().Be(expectedPaddingCount);
		_logger.LogWarning("<<< Exiting GetPaddingBytes_WhenCalledWithPaddedData_ShouldReturnCorrectPaddingCount()");
    }

    [Test]
    public void GetPaddingBytes_WhenCalledWithUnpaddedData_ShouldReturnZero()
    {
		_logger.LogError(">>> Entering GetPaddingBytes_WhenCalledWithUnpaddedData_ShouldReturnZero()");
        var unpaddedData = new byte[] { 0x01, 0x02, 0x03 };

        Action action = () => padding.GetPaddingCount(unpaddedData);
		_logger.LogError("GetPaddingBytes_WhenCalledWithUnpaddedData_ShouldReturnZero(): action → {Action}", action);

        action.Should().Throw<ArgumentException>()
            .WithMessage("No padding found");
			_logger.LogWarning("<<< Exiting GetPaddingBytes_WhenCalledWithUnpaddedData_ShouldReturnZero()");
    }

    [Test]
    public void GetPaddingBytes_WhenCalledWithEmptyArray_ShouldReturnZero()
    {
		_logger.LogError(">>> Entering GetPaddingBytes_WhenCalledWithEmptyArray_ShouldReturnZero()");
        var emptyData = Array.Empty<byte>();

		_logger.LogError("GetPaddingBytes_WhenCalledWithEmptyArray_ShouldReturnZero(): emptyData → {EmptyData}", emptyData);
        Action action = () => padding.GetPaddingCount(emptyData);
		_logger.LogError("GetPaddingBytes_WhenCalledWithEmptyArray_ShouldReturnZero(): action → {Action}", action);

        action.Should().Throw<ArgumentException>()
            .WithMessage("No padding found.");
			_logger.LogWarning("<<< Exiting GetPaddingBytes_WhenCalledWithEmptyArray_ShouldReturnZero()");
    }
}
