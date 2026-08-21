using Algorithms.Other;
using SkiaSharp;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Other;

public static class MandelbrotTest
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void BitmapWidthIsZeroOrNegative_ThrowsArgumentOutOfRangeException()
    {
		_logger.LogDebug(">>> Entering BitmapWidthIsZeroOrNegative_ThrowsArgumentOutOfRangeException()");
        Assert.Throws<ArgumentOutOfRangeException>(() => Mandelbrot.GetBitmap(-200));
		_logger.LogDebug("<<< Exiting BitmapWidthIsZeroOrNegative_ThrowsArgumentOutOfRangeException()");
    }

    [Test]
    public static void BitmapHeightIsZeroOrNegative_ThrowsArgumentOutOfRangeException()
    {
		_logger.LogDebug(">>> Entering BitmapHeightIsZeroOrNegative_ThrowsArgumentOutOfRangeException()");
        Assert.Throws<ArgumentOutOfRangeException>(() => Mandelbrot.GetBitmap(bitmapHeight: 0));
		_logger.LogDebug("<<< Exiting BitmapHeightIsZeroOrNegative_ThrowsArgumentOutOfRangeException()");
    }

    [Test]
    public static void MaxStepIsZeroOrNegative_ThrowsArgumentOutOfRangeException()
    {
		_logger.LogDebug(">>> Entering MaxStepIsZeroOrNegative_ThrowsArgumentOutOfRangeException()");
        Assert.Throws<ArgumentOutOfRangeException>(() => Mandelbrot.GetBitmap(maxStep: -1));
		_logger.LogDebug("<<< Exiting MaxStepIsZeroOrNegative_ThrowsArgumentOutOfRangeException()");
    }

    [Test]
    public static void TestBlackAndWhite()
    {
		_logger.LogDebug(">>> Entering TestBlackAndWhite()");
        SKBitmap bitmap = Mandelbrot.GetBitmap(useDistanceColorCoding: false);
        // Pixel outside the Mandelbrot set should be white.
		_logger.LogDebug("TestBlackAndWhite(): bitmap → {Bitmap}", bitmap);
        Assert.That(new SKColor(255, 255, 255, 255), Is.EqualTo(bitmap.GetPixel(0, 0)));

        // Pixel inside the Mandelbrot set should be black.
        Assert.That(new SKColor(0, 0, 0, 255), Is.EqualTo(bitmap.GetPixel(400, 300)));
		_logger.LogDebug("<<< Exiting TestBlackAndWhite()");
    }

    [Test]
    public static void TestColorCoded()
    {
		_logger.LogDebug(">>> Entering TestColorCoded()");
        SKBitmap bitmap = Mandelbrot.GetBitmap(useDistanceColorCoding: true);
        // Pixel distant to the Mandelbrot set should be red.
		_logger.LogDebug("TestColorCoded(): bitmap → {Bitmap}", bitmap);
        Assert.That(new SKColor(255, 0, 0, 255), Is.EqualTo(bitmap.GetPixel(0, 0)));

        // Pixel inside the Mandelbrot set should be black.
        Assert.That(new SKColor(0, 0, 0, 255), Is.EqualTo(bitmap.GetPixel(400, 300)));
		_logger.LogDebug("<<< Exiting TestColorCoded()");
    }
}
