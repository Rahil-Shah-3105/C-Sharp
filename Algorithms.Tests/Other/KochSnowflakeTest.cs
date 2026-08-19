using Algorithms.Other;
using SkiaSharp;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Other;

public static class KochSnowflakeTest
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void TestIterateMethod()
    {
		_logger.LogDebug(">>> Entering TestIterateMethod()");
        List<Vector2> vectors = [new Vector2(0, 0), new Vector2(1, 0)];
        List<Vector2> result = KochSnowflake.Iterate(vectors, 1);
		_logger.LogDebug("TestIterateMethod(): result → {Result}", result);
		_logger.LogDebug("TestIterateMethod(): vectors → {Vectors}", vectors);
        result[0].Should().Be(new Vector2(0, 0));
        result[1].Should().Be(new Vector2((float)1 / 3, 0));

        /* Should().BeApproximately() is not defined for Vector2 or float
        so the x-y-components have to be tested separately and the y-component needs to be cast to double */
        result[2].X.Should().Be(0.5f);
        ((double)result[2].Y).Should().BeApproximately(Math.Sin(Math.PI / 3) / 3, 0.0001);

        result[3].Should().Be(new Vector2((float)2 / 3, 0));
        result[4].Should().Be(new Vector2(1, 0));
		_logger.LogDebug("<<< Exiting TestIterateMethod()");
    }

    [Test]
    public static void BitmapWidthIsZeroOrNegative_ThrowsArgumentOutOfRangeException()
    {
		_logger.LogDebug(">>> Entering BitmapWidthIsZeroOrNegative_ThrowsArgumentOutOfRangeException()");
        Assert.Throws<ArgumentOutOfRangeException>(() => KochSnowflake.GetKochSnowflake(-200));
		_logger.LogDebug("<<< Exiting BitmapWidthIsZeroOrNegative_ThrowsArgumentOutOfRangeException()");
    }

    [Test]
    public static void TestKochSnowflakeExample()
    {
		_logger.LogDebug(">>> Entering TestKochSnowflakeExample()");
        var bitmapWidth = 600;
        var offsetX = bitmapWidth / 10f;
        var offsetY = bitmapWidth / 3.7f;
        SKBitmap bitmap = KochSnowflake.GetKochSnowflake();
		_logger.LogDebug("TestKochSnowflakeExample(): bitmap → {Bitmap}", bitmap);
        bitmap.GetPixel(0, 0)
            .Should()
            .Be(new SKColor(255, 255, 255, 255), "because the background should be white");
        bitmap.GetPixel((int)offsetX, (int)offsetY)
            .Should()
            .Be(new SKColor(0, 0, 0, 255), "because the snowflake is drawn in black and this is the position of the first vector");
			_logger.LogDebug("<<< Exiting TestKochSnowflakeExample()");
    }
}
