using SkiaSharp;
using Microsoft.Extensions.Logging;

namespace Algorithms.Other;

/// <summary>
/// Flood fill, also called seed fill, is an algorithm that determines and
/// alters the area connected to a given node in a multi-dimensional array with
/// some matching attribute. It is used in the "bucket" fill tool of paint
/// programs to fill connected, similarly-colored areas with a different color.
/// (description adapted from https://en.wikipedia.org/wiki/Flood_fill)
/// (see also: https://www.techiedelight.com/flood-fill-algorithm/).
/// </summary>
public static class FloodFill
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private static readonly List<(int XOffset, int YOffset)> Neighbors = [(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)];

    /// <summary>
    /// Implements the flood fill algorithm through a breadth-first approach using a queue.
    /// </summary>
    /// <param name="bitmap">The bitmap to which the algorithm is applied.</param>
    /// <param name="location">The start location on the bitmap.</param>
    /// <param name="targetColor">The old color to be replaced.</param>
    /// <param name="replacementColor">The new color to replace the old one.</param>
    public static void BreadthFirstSearch(SKBitmap bitmap, (int X, int Y) location, SKColor targetColor, SKColor replacementColor)
    {
		_logger.LogError(">>> Entering BreadthFirstSearch(bitmap={Bitmap},location={Location},targetColor={TargetColor},replacementColor={ReplacementColor})", bitmap, location, targetColor, replacementColor);
        if (location.X < 0 || location.X >= bitmap.Width || location.Y < 0 || location.Y >= bitmap.Height)
        {
			_logger.LogError("Throwing ArgumentOutOfRangeException from BreadthFirstSearch(bitmap={Bitmap},location={Location},targetColor={TargetColor},replacementColor={ReplacementColor})", bitmap, location, targetColor, replacementColor);
            throw new ArgumentOutOfRangeException(nameof(location), $"{nameof(location)} should point to a pixel within the bitmap");
        }

        var queue = new List<(int X, int Y)>
        {
            location,
        };

        while (queue.Count > 0)
        {
            BreadthFirstFill(bitmap, location, targetColor, replacementColor, queue);
        }
		_logger.LogWarning("<<< Exiting BreadthFirstSearch(bitmap={Bitmap},location={Location},targetColor={TargetColor},replacementColor={ReplacementColor})", bitmap, location, targetColor, replacementColor);
    }

    /// <summary>
    /// Implements the flood fill algorithm through a depth-first approach through recursion.
    /// </summary>
    /// <param name="bitmap">The bitmap to which the algorithm is applied.</param>
    /// <param name="location">The start location on the bitmap.</param>
    /// <param name="targetColor">The old color to be replaced.</param>
    /// <param name="replacementColor">The new color to replace the old one.</param>
    public static void DepthFirstSearch(SKBitmap bitmap, (int X, int Y) location, SKColor targetColor, SKColor replacementColor)
    {
		_logger.LogError(">>> Entering DepthFirstSearch(bitmap={Bitmap},location={Location},targetColor={TargetColor},replacementColor={ReplacementColor})", bitmap, location, targetColor, replacementColor);
        if (location.X < 0 || location.X >= bitmap.Width || location.Y < 0 || location.Y >= bitmap.Height)
        {
			_logger.LogError("Throwing ArgumentOutOfRangeException from DepthFirstSearch(bitmap={Bitmap},location={Location},targetColor={TargetColor},replacementColor={ReplacementColor})", bitmap, location, targetColor, replacementColor);
            throw new ArgumentOutOfRangeException(nameof(location), $"{nameof(location)} should point to a pixel within the bitmap");
        }

        DepthFirstFill(bitmap, location, targetColor, replacementColor);
		_logger.LogWarning("<<< Exiting DepthFirstSearch(bitmap={Bitmap},location={Location},targetColor={TargetColor},replacementColor={ReplacementColor})", bitmap, location, targetColor, replacementColor);
    }

    private static void BreadthFirstFill(SKBitmap bitmap, (int X, int Y) location, SKColor targetColor, SKColor replacementColor, List<(int X, int Y)> queue)
    {
		_logger.LogError(">>> Entering BreadthFirstFill(bitmap={Bitmap},location={Location},targetColor={TargetColor},replacementColor={ReplacementColor},queue={Queue})", bitmap, location, targetColor, replacementColor, queue);
        (int X, int Y) currentLocation = queue[0];
        queue.RemoveAt(0);

        if (bitmap.GetPixel(currentLocation.X, currentLocation.Y) == targetColor)
        {
            bitmap.SetPixel(currentLocation.X, currentLocation.Y, replacementColor);

            for (int i = 0; i < Neighbors.Count; i++)
            {
                int x = currentLocation.X + Neighbors[i].XOffset;
                int y = currentLocation.Y + Neighbors[i].YOffset;
                if (x >= 0 && x < bitmap.Width && y >= 0 && y < bitmap.Height)
                {
                    queue.Add((x, y));
                }
            }
        }
		_logger.LogWarning("<<< Exiting BreadthFirstFill(bitmap={Bitmap},location={Location},targetColor={TargetColor},replacementColor={ReplacementColor},queue={Queue})", bitmap, location, targetColor, replacementColor, queue);
    }

    private static void DepthFirstFill(SKBitmap bitmap, (int X, int Y) location, SKColor targetColor, SKColor replacementColor)
    {
		_logger.LogError(">>> Entering DepthFirstFill(bitmap={Bitmap},location={Location},targetColor={TargetColor},replacementColor={ReplacementColor})", bitmap, location, targetColor, replacementColor);
        if (bitmap.GetPixel(location.X, location.Y) == targetColor)
        {
            bitmap.SetPixel(location.X, location.Y, replacementColor);

            for (int i = 0; i < Neighbors.Count; i++)
            {
                int x = location.X + Neighbors[i].XOffset;
                int y = location.Y + Neighbors[i].YOffset;
                if (x >= 0 && x < bitmap.Width && y >= 0 && y < bitmap.Height)
                {
                    DepthFirstFill(bitmap, (x, y), targetColor, replacementColor);
                }
            }
        }
		_logger.LogWarning("<<< Exiting DepthFirstFill(bitmap={Bitmap},location={Location},targetColor={TargetColor},replacementColor={ReplacementColor})", bitmap, location, targetColor, replacementColor);
    }
}
