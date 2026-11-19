using Microsoft.Extensions.Logging;
namespace Algorithms.Tests.Helpers;

internal static class RandomHelper
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public static (int[] correctArray, int[] testArray) GetArrays(int n)
    {
		_logger.LogError(">>> Entering GetArrays(n={N})", n);
        var testArr = new int[n];
        var correctArray = new int[n];

        for (var i = 0; i < n; i++)
        {
            var t = TestContext.CurrentContext.Random.Next(1_000_000);
			_logger.LogError("GetArrays(n={N}): t → {T}", n, t);
            testArr[i] = t;
            correctArray[i] = t;
        }

		_logger.LogWarning("<<< Exiting GetArrays(n={N})", n);
        return (correctArray, testArr);
    }

    public static (string[] correctArray, string[] testArray) GetStringArrays(
        int n,
        int maxLength,
        bool equalLength)
    {
		_logger.LogError(">>> Entering GetStringArrays()");
        var testArr = new string[n];
        var correctArray = new string[n];
        var length = TestContext.CurrentContext.Random.Next(2, maxLength);
		_logger.LogError("GetStringArrays(): length → {Length}", length);

        for (var i = 0; i < n; i++)
        {
            if (!equalLength)
            {
                length = TestContext.CurrentContext.Random.Next(2, maxLength);
            }

            var chars = new char[length];
            for (var j = 0; j < length; j++)
            {
                chars[j] = (char)TestContext.CurrentContext.Random.Next(97, 123);
            }

            var t = new string(chars);
			_logger.LogError("GetStringArrays(): t → {T}", t);
            testArr[i] = t;
            correctArray[i] = t;
        }

		_logger.LogWarning("<<< Exiting GetStringArrays()");
        return (correctArray, testArr);
    }
}
