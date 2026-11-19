using Microsoft.Extensions.Logging;
namespace Algorithms.Other;

/// <summary>
///     Manually converts an integer of certain size to a string of the binary representation.
/// </summary>
public static class Int2Binary
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Returns string of the binary representation of given Int.
    /// </summary>
    /// <param name="input">Number to be converted.</param>
    /// <returns>Binary representation of input.</returns>
    public static string Int2Bin(ushort input)
    {
		_logger.LogError(">>> Entering Int2Bin(input={Input})", input);
        ushort msb = ushort.MaxValue / 2 + 1;
        var output = new StringBuilder();
		_logger.LogError("Int2Bin(input={Input}): output → {Output}", input, output);
        for (var i = 0; i < 16; i++)
        {
            if (input >= msb)
            {
                output.Append("1");
                input -= msb;
                msb /= 2;
            }
            else
            {
                output.Append("0");
                msb /= 2;
            }
        }

		_logger.LogWarning("<<< Exiting Int2Bin(input={Input})", input);
        return output.ToString();
    }

    /// <summary>
    ///     Returns string of the binary representation of given Int.
    /// </summary>
    /// <param name="input">Number to be converted.</param>
    /// <returns>Binary representation of input.</returns>
    public static string Int2Bin(uint input)
    {
        var msb = uint.MaxValue / 2 + 1;
        var output = new StringBuilder();
        for (var i = 0; i < 32; i++)
        {
            if (input >= msb)
            {
                output.Append("1");
                input -= msb;
                msb /= 2;
            }
            else
            {
                output.Append("0");
                msb /= 2;
            }
        }

        return output.ToString();
    }

    /// <summary>
    ///     Returns string of the binary representation of given Int.
    /// </summary>
    /// <param name="input">Number to be converted.</param>
    /// <returns>Binary representation of input.</returns>
    public static string Int2Bin(ulong input)
    {
        var msb = ulong.MaxValue / 2 + 1;
        var output = new StringBuilder();
        for (var i = 0; i < 64; i++)
        {
            if (input >= msb)
            {
                output.Append("1");
                input -= msb;
                msb /= 2;
            }
            else
            {
                output.Append("0");
                msb /= 2;
            }
        }

        return output.ToString();
    }
}
