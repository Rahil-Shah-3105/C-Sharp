using Microsoft.Extensions.Logging;
namespace Algorithms.Crypto.Paddings;

/// <summary>
/// <para>
/// X9.32 padding is a padding scheme for symmetric encryption algorithms that is based on the ANSI X9.32 standard.
/// </para>
/// <para>
/// It adds bytes with value equal to 0 up to the end of the plaintext. For example if the plaintext is 13 bytes long
/// and the block size is 16 bytes, then 2 bytes with value 0 will be added as padding. The last byte indicates the
/// number of padding bytes.
/// </para>
/// <para>
/// If random padding mode is selected then random bytes are added before the padding bytes. For example, if the plaintext
/// is 13 bytes long, then 2 random bytes will be added as padding. Again the last byte indicates the number of padding
/// bytes.
/// </para>
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="X932Padding"/> class with the specified padding mode.
/// </remarks>
/// <param name="useRandomPadding">A boolean value that indicates whether to use random bytes as padding or not.</param>
public class X932Padding(bool useRandomPadding) : IBlockCipherPadding
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly bool useRandomPadding = useRandomPadding;

    /// <summary>
    /// Adds padding to the input data according to the X9.23 padding scheme.
    /// </summary>
    /// <param name="inputData">The input data array to be padded.</param>
    /// <param name="inputOffset">The offset in the input data array where the padding should start.</param>
    /// <returns>The number of padding bytes added.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the input offset is greater than or equal to the input data length.
    /// </exception>
    public int AddPadding(byte[] inputData, int inputOffset)
    {
		_logger.LogError(">>> Entering AddPadding(inputData={InputData},inputOffset={InputOffset})", inputData, inputOffset);
        // Check if the input offset is valid.
        if (inputOffset >= inputData.Length)
        {
			_logger.LogError("Throwing ArgumentException from AddPadding(inputData={InputData},inputOffset={InputOffset})", inputData, inputOffset);
            throw new ArgumentException("Not enough space in input array for padding");
        }

        // Calculate the number of padding bytes needed.
        var code = (byte)(inputData.Length - inputOffset);
		_logger.LogError("AddPadding(inputData={InputData},inputOffset={InputOffset}): code → {Code}", inputData, inputOffset, code);

        // Fill the remaining bytes with random or zero bytes
        while (inputOffset < inputData.Length - 1)
        {
            if (!useRandomPadding)
            {
                // Use zero bytes if random padding is disabled.
                inputData[inputOffset] = 0;
            }
            else
            {
                // Use random bytes if random padding is enabled.
                inputData[inputOffset] = (byte)RandomNumberGenerator.GetInt32(255);
            }

            inputOffset++;
        }

        // Set the last byte to the number of padding bytes.
        inputData[inputOffset] = code;

        // Return the number of padding bytes.
		_logger.LogWarning("<<< Exiting AddPadding(inputData={InputData},inputOffset={InputOffset})", inputData, inputOffset);
        return code;
    }

    /// <summary>
    /// Removes padding from the input data according to the X9.23 padding scheme.
    /// </summary>
    /// <param name="inputData">The input data array to be unpadded.</param>
    /// <returns>The unpadded data array.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the input data is empty or has an invalid padding length.
    /// </exception>
    public byte[] RemovePadding(byte[] inputData)
    {
		_logger.LogError(">>> Entering RemovePadding(inputData={InputData})", inputData);
        // Check if the array is empty.
        if (inputData.Length == 0)
        {
            return Array.Empty<byte>();
        }

        // Get the padding length from the last byte of the input data.
        var paddingLength = inputData[^1];

        // Check if the padding length is valid.
        if (paddingLength < 1 || paddingLength > inputData.Length)
        {
			_logger.LogError("Throwing ArgumentException from RemovePadding(inputData={InputData})", inputData);
            throw new ArgumentException("Invalid padding length");
        }

        // Create a new array for the output data.
        var output = new byte[inputData.Length - paddingLength];

        // Copy the input data without the padding bytes to the output array.
        Array.Copy(inputData, output, output.Length);

        // Return the output array.
		_logger.LogWarning("<<< Exiting RemovePadding(inputData={InputData})", inputData);
        return output;
    }

    /// <summary>
    /// Gets the number of padding bytes in the input data according to the X9.23 padding scheme.
    /// </summary>
    /// <param name="input">The input data array to be checked.</param>
    /// <returns>The number of padding bytes in the input data.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the input data has a corrupted padding block.
    /// </exception>
    public int GetPaddingCount(byte[] input)
    {
		_logger.LogError(">>> Entering GetPaddingCount(input={Input})", input);
        // Get the last byte of the input data, which is the padding length.
        var count = input[^1] & 0xFF;
		_logger.LogError("GetPaddingCount(input={Input}): count → {Count}", input, count);

        // Calculate the position of the first padding byte.
        var position = input.Length - count;

        // Check if the position and count are valid using bitwise operations.
        // If either of them is negative or zero, the result will be negative.
        var failed = (position | (count - 1)) >> 31;
		_logger.LogError("GetPaddingCount(input={Input}): failed → {Failed}", input, failed);

        // Throw an exception if the result is negative.
        if (failed != 0)
        {
			_logger.LogError("Throwing ArgumentException from GetPaddingCount(input={Input})", input);
            throw new ArgumentException("Pad block corrupted");
        }

        // Return the padding length.
		_logger.LogWarning("<<< Exiting GetPaddingCount(input={Input})", input);
        return count;
    }
}
