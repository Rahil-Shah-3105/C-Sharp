using SkiaSharp;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Other;

public static class Tests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private const byte Alpha = 255;
    private static readonly SKColor Black = new(0, 0, 0, Alpha);
    private static readonly SKColor Green = new(0, 255, 0, Alpha);
    private static readonly SKColor Violet = new(255, 0, 255, Alpha);
    private static readonly SKColor White = new(255, 255, 255, Alpha);
    private static readonly SKColor Orange = new(255, 128, 0, Alpha);

    [Test]
    public static void BreadthFirstSearch_ThrowsArgumentOutOfRangeException()
    {
		_logger.LogDebug(">>> Entering BreadthFirstSearch_ThrowsArgumentOutOfRangeException()");
        Action act = () => Algorithms.Other.FloodFill.BreadthFirstSearch(GenerateTestBitmap(), (10, 10), Black, White);
		_logger.LogDebug("<<< Exiting BreadthFirstSearch_ThrowsArgumentOutOfRangeException()");
		_logger.LogDebug("BreadthFirstSearch_ThrowsArgumentOutOfRangeException(): act → {Act}", act);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public static void DepthFirstSearch_ThrowsArgumentOutOfRangeException()
    {
		_logger.LogDebug(">>> Entering DepthFirstSearch_ThrowsArgumentOutOfRangeException()");
        Action act = () => Algorithms.Other.FloodFill.DepthFirstSearch(GenerateTestBitmap(), (-1, -1), Black, White);
		_logger.LogDebug("<<< Exiting DepthFirstSearch_ThrowsArgumentOutOfRangeException()");
		_logger.LogDebug("DepthFirstSearch_ThrowsArgumentOutOfRangeException(): act → {Act}", act);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public static void BreadthFirstSearch_Test1()
    {
		_logger.LogDebug(">>> Entering BreadthFirstSearch_Test1()");
        TestAlgorithm(Algorithms.Other.FloodFill.BreadthFirstSearch, (1, 1), Green, Orange, (1, 1), Orange);
		_logger.LogDebug("<<< Exiting BreadthFirstSearch_Test1()");
    }

    [Test]
    public static void BreadthFirstSearch_Test2()
    {
		_logger.LogDebug(">>> Entering BreadthFirstSearch_Test2()");
        TestAlgorithm(Algorithms.Other.FloodFill.BreadthFirstSearch, (1, 1), Green, Orange, (0, 1), Violet);
		_logger.LogDebug("<<< Exiting BreadthFirstSearch_Test2()");
    }

    [Test]
    public static void BreadthFirstSearch_Test3()
    {
		_logger.LogDebug(">>> Entering BreadthFirstSearch_Test3()");
        TestAlgorithm(Algorithms.Other.FloodFill.BreadthFirstSearch, (1, 1), Green, Orange, (6, 4), White);
		_logger.LogDebug("<<< Exiting BreadthFirstSearch_Test3()");
    }

    [Test]
    public static void DepthFirstSearch_Test1()
    {
		_logger.LogDebug(">>> Entering DepthFirstSearch_Test1()");
        TestAlgorithm(Algorithms.Other.FloodFill.DepthFirstSearch, (1, 1), Green, Orange, (1, 1), Orange);
		_logger.LogDebug("<<< Exiting DepthFirstSearch_Test1()");
    }

    [Test]
    public static void DepthFirstSearch_Test2()
    {
		_logger.LogDebug(">>> Entering DepthFirstSearch_Test2()");
        TestAlgorithm(Algorithms.Other.FloodFill.DepthFirstSearch, (1, 1), Green, Orange, (0, 1), Violet);
		_logger.LogDebug("<<< Exiting DepthFirstSearch_Test2()");
    }

    [Test]
    public static void DepthFirstSearch_Test3()
    {
		_logger.LogDebug(">>> Entering DepthFirstSearch_Test3()");
        TestAlgorithm(Algorithms.Other.FloodFill.DepthFirstSearch, (1, 1), Green, Orange, (6, 4), White);
		_logger.LogDebug("<<< Exiting DepthFirstSearch_Test3()");
    }

    private static SKBitmap GenerateTestBitmap()
    {
		_logger.LogDebug(">>> Entering GenerateTestBitmap()");
        SKColor[,] layout =
        {
            {Violet, Violet, Green, Green, Black, Green, Green},
            {Violet, Green, Green, Black, Green, Green, Green},
            {Green, Green, Green, Black, Green, Green, Green},
            {Black, Black, Green, Black, White, White, Green},
            {Violet, Violet, Black, Violet, Violet, White, White},
            {Green, Green, Green, Violet, Violet, Violet, Violet},
            {Violet, Violet, Violet, Violet, Violet, Violet, Violet},
        };

        SKBitmap bitmap = new(7, 7);
		_logger.LogDebug("GenerateTestBitmap(): bitmap → {Bitmap}", bitmap);
        for (int x = 0; x < layout.GetLength(0); x++)
        {
            for (int y = 0; y < layout.GetLength(1); y++)
            {
                bitmap.SetPixel(x, y, layout[y, x]);
            }
        }

		_logger.LogDebug("<<< Exiting GenerateTestBitmap()");
        return bitmap;
    }

    private static void TestAlgorithm(
        Action<SKBitmap, ValueTuple<int, int>, SKColor, SKColor> algorithm,
        ValueTuple<int, int> fillLocation,
        SKColor targetColor,
        SKColor replacementColor,
        ValueTuple<int, int> testLocation,
        SKColor expectedColor)
    {
		_logger.LogDebug(">>> Entering TestAlgorithm(algorithm={Algorithm},fillLocation={FillLocation},targetColor={TargetColor},replacementColor={ReplacementColor},testLocation={TestLocation},expectedColor={ExpectedColor})", algorithm, fillLocation, targetColor, replacementColor, testLocation, expectedColor);
        SKBitmap bitmap = GenerateTestBitmap();
		_logger.LogDebug("TestAlgorithm(algorithm={Algorithm},fillLocation={FillLocation},targetColor={TargetColor},replacementColor={ReplacementColor},testLocation={TestLocation},expectedColor={ExpectedColor}): bitmap → {Bitmap}", algorithm, fillLocation, targetColor, replacementColor, testLocation, expectedColor, bitmap);
        algorithm(bitmap, fillLocation, targetColor, replacementColor);
        SKColor actualColor = bitmap.GetPixel(testLocation.Item1, testLocation.Item2);
		_logger.LogDebug("TestAlgorithm(algorithm={Algorithm},fillLocation={FillLocation},targetColor={TargetColor},replacementColor={ReplacementColor},testLocation={TestLocation},expectedColor={ExpectedColor}): actualColor → {ActualColor}", algorithm, fillLocation, targetColor, replacementColor, testLocation, expectedColor, actualColor);
        actualColor.Should().Be(expectedColor);
		_logger.LogDebug("<<< Exiting TestAlgorithm(algorithm={Algorithm},fillLocation={FillLocation},targetColor={TargetColor},replacementColor={ReplacementColor},testLocation={TestLocation},expectedColor={ExpectedColor})", algorithm, fillLocation, targetColor, replacementColor, testLocation, expectedColor);
    }
}
