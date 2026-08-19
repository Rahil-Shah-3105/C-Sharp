using Microsoft.Extensions.Logging;
namespace Algorithms.Other;

/// <summary>
///     The RGB color model is an additive color model in which red, green, and
///     blue light are added together in various ways to reproduce a broad array of
///     colors. The name of the model comes from the initials of the three additive
///     primary colors, red, green, and blue. Meanwhile, the HSV representation
///     models how colors appear under light. In it, colors are represented using
///     three components: hue, saturation and (brightness-)value. This class
///     provides methods for converting colors from one representation to the other.
///     (description adapted from https://en.wikipedia.org/wiki/RGB_color_model and
///     https://en.wikipedia.org/wiki/HSL_and_HSV).
/// </summary>
public static class RgbHsvConversion
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Conversion from the HSV-representation to the RGB-representation.
    /// </summary>
    /// <param name="hue">Hue of the color.</param>
    /// <param name="saturation">Saturation of the color.</param>
    /// <param name="value">Brightness-value of the color.</param>
    /// <returns>The tuple of RGB-components.</returns>
    public static (byte Red, byte Green, byte Blue) HsvToRgb(
        double hue,
        double saturation,
        double value)
    {
		_logger.LogDebug(">>> Entering HsvToRgb(Red={Red},Green={Green},Blue={Blue})", Red, Green, Blue);
        if (hue < 0 || hue > 360)
        {
			_logger.LogWarning("Throwing ArgumentOutOfRangeException from HsvToRgb(Red={Red},Green={Green},Blue={Blue})", Red, Green, Blue);
            throw new ArgumentOutOfRangeException(nameof(hue), $"{nameof(hue)} should be between 0 and 360");
        }

        if (saturation < 0 || saturation > 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(saturation),
                $"{nameof(saturation)} should be between 0 and 1");
        }

        if (value < 0 || value > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(value)} should be between 0 and 1");
        }

        var chroma = value * saturation;
        var hueSection = hue / 60;
        var secondLargestComponent = chroma * (1 - Math.Abs(hueSection % 2 - 1));
		_logger.LogDebug("HsvToRgb(Red={Red},Green={Green},Blue={Blue}): secondLargestComponent → {SecondLargestComponent}", Red, Green, Blue, secondLargestComponent);
        var matchValue = value - chroma;

		_logger.LogDebug("<<< Exiting HsvToRgb(Red={Red},Green={Green},Blue={Blue})", Red, Green, Blue);
        return GetRgbBySection(hueSection, chroma, matchValue, secondLargestComponent);
    }

    /// <summary>
    ///     Conversion from the RGB-representation to the HSV-representation.
    /// </summary>
    /// <param name="red">Red-component of the color.</param>
    /// <param name="green">Green-component of the color.</param>
    /// <param name="blue">Blue-component of the color.</param>
    /// <returns>The tuple of HSV-components.</returns>
    public static (double Hue, double Saturation, double Value) RgbToHsv(
        byte red,
        byte green,
        byte blue)
    {
		_logger.LogDebug(">>> Entering RgbToHsv(Hue={Hue},Saturation={Saturation},Value={Value})", Hue, Saturation, Value);
        var dRed = (double)red / 255;
        var dGreen = (double)green / 255;
		_logger.LogDebug("RgbToHsv(Hue={Hue},Saturation={Saturation},Value={Value}): dGreen → {DGreen}", Hue, Saturation, Value, dGreen);
        var dBlue = (double)blue / 255;
		_logger.LogDebug("RgbToHsv(Hue={Hue},Saturation={Saturation},Value={Value}): dBlue → {DBlue}", Hue, Saturation, Value, dBlue);
        var value = Math.Max(Math.Max(dRed, dGreen), dBlue);
		_logger.LogDebug("RgbToHsv(Hue={Hue},Saturation={Saturation},Value={Value}): value → {Value1}", Hue, Saturation, Value, value);
        var chroma = value - Math.Min(Math.Min(dRed, dGreen), dBlue);
		_logger.LogDebug("RgbToHsv(Hue={Hue},Saturation={Saturation},Value={Value}): chroma → {Chroma}", Hue, Saturation, Value, chroma);
        var saturation = value.Equals(0) ? 0 : chroma / value;
		_logger.LogDebug("RgbToHsv(Hue={Hue},Saturation={Saturation},Value={Value}): saturation → {Saturation1}", Hue, Saturation, Value, saturation);
		_logger.LogDebug("RgbToHsv(Hue={Hue},Saturation={Saturation},Value={Value}): dRed → {DRed}", Hue, Saturation, Value, dRed);
        double hue;

        if (chroma.Equals(0))
        {
            hue = 0;
        }
        else if (value.Equals(dRed))
        {
            hue = 60 * (0 + (dGreen - dBlue) / chroma);
        }
        else if (value.Equals(dGreen))
        {
            hue = 60 * (2 + (dBlue - dRed) / chroma);
        }
        else
        {
            hue = 60 * (4 + (dRed - dGreen) / chroma);
        }

        hue = (hue + 360) % 360;

		_logger.LogDebug("<<< Exiting RgbToHsv(Hue={Hue},Saturation={Saturation},Value={Value})", Hue, Saturation, Value);
        return (hue, saturation, value);
    }

    private static (byte Red, byte Green, byte Blue) GetRgbBySection(
        double hueSection,
        double chroma,
        double matchValue,
        double secondLargestComponent)
    {
		_logger.LogDebug(">>> Entering GetRgbBySection(Red={Red},Green={Green},Blue={Blue})", Red, Green, Blue);
        byte red;
        byte green;
        byte blue;

        if (hueSection >= 0 && hueSection <= 1)
        {
            red = ConvertToByte(chroma + matchValue);
            green = ConvertToByte(secondLargestComponent + matchValue);
            blue = ConvertToByte(matchValue);
        }
        else if (hueSection > 1 && hueSection <= 2)
        {
            red = ConvertToByte(secondLargestComponent + matchValue);
            green = ConvertToByte(chroma + matchValue);
            blue = ConvertToByte(matchValue);
        }
        else if (hueSection > 2 && hueSection <= 3)
        {
            red = ConvertToByte(matchValue);
            green = ConvertToByte(chroma + matchValue);
            blue = ConvertToByte(secondLargestComponent + matchValue);
        }
        else if (hueSection > 3 && hueSection <= 4)
        {
            red = ConvertToByte(matchValue);
            green = ConvertToByte(secondLargestComponent + matchValue);
            blue = ConvertToByte(chroma + matchValue);
        }
        else if (hueSection > 4 && hueSection <= 5)
        {
            red = ConvertToByte(secondLargestComponent + matchValue);
            green = ConvertToByte(matchValue);
            blue = ConvertToByte(chroma + matchValue);
        }
        else
        {
            red = ConvertToByte(chroma + matchValue);
            green = ConvertToByte(matchValue);
            blue = ConvertToByte(secondLargestComponent + matchValue);
        }

		_logger.LogDebug("<<< Exiting GetRgbBySection(Red={Red},Green={Green},Blue={Blue})", Red, Green, Blue);
        return (red, green, blue);
    }

    private static byte ConvertToByte(double input) => (byte)Math.Round(255 * input);
}
