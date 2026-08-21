using Algorithms.Stack;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Stack
{
    [TestFixture]
    public class NextGreaterElementTests
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        private static int[] FindNextGreaterElement(int[] input)
        {
			_logger.LogDebug(">>> Entering FindNextGreaterElement(input={Input})", input);
            var obj = new NextGreaterElement();
			_logger.LogDebug("FindNextGreaterElement(input={Input}): obj → {Obj}", input, obj);
			_logger.LogDebug("<<< Exiting FindNextGreaterElement(input={Input})", input);
            return obj.FindNextGreaterElement(input);
        }

        [Test]
        public void FindNextGreaterElement_InputIsEmpty_ReturnsEmptyArray()
        {
			_logger.LogDebug(">>> Entering FindNextGreaterElement_InputIsEmpty_ReturnsEmptyArray()");
            // Arrange
            int[] input = Array.Empty<int>();
            int[] expected = Array.Empty<int>();

            // Act
            var result = FindNextGreaterElement(input);
			_logger.LogDebug("FindNextGreaterElement_InputIsEmpty_ReturnsEmptyArray(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
			_logger.LogDebug("<<< Exiting FindNextGreaterElement_InputIsEmpty_ReturnsEmptyArray()");
        }

        [Test]
        public void FindNextGreaterElement_BasicScenario_ReturnsCorrectResult()
        {
			_logger.LogDebug(">>> Entering FindNextGreaterElement_BasicScenario_ReturnsCorrectResult()");
            // Arrange
            int[] input = [4, 5, 2, 25];
            int[] expected = [5, 25, 25, -1];

            // Act
            var result = FindNextGreaterElement(input);
			_logger.LogDebug("FindNextGreaterElement_BasicScenario_ReturnsCorrectResult(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
			_logger.LogDebug("<<< Exiting FindNextGreaterElement_BasicScenario_ReturnsCorrectResult()");
        }

        [Test]
        public void FindNextGreaterElement_NoNextGreaterElement_ReturnsCorrectResult()
        {
			_logger.LogDebug(">>> Entering FindNextGreaterElement_NoNextGreaterElement_ReturnsCorrectResult()");
            // Arrange
            int[] input = [13, 7, 6, 12];
            int[] expected = [-1, 12, 12, -1];

            // Act
            var result = FindNextGreaterElement(input);
			_logger.LogDebug("FindNextGreaterElement_NoNextGreaterElement_ReturnsCorrectResult(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
			_logger.LogDebug("<<< Exiting FindNextGreaterElement_NoNextGreaterElement_ReturnsCorrectResult()");
        }

        [Test]
        public void FindNextGreaterElement_AllElementsHaveNoGreaterElement_ReturnsAllNegativeOnes()
        {
			_logger.LogDebug(">>> Entering FindNextGreaterElement_AllElementsHaveNoGreaterElement_ReturnsAllNegativeOnes()");
            // Arrange
            int[] input = [5, 4, 3, 2, 1];
            int[] expected = [-1, -1, -1, -1, -1];

            // Act
            var result = FindNextGreaterElement(input);
			_logger.LogDebug("FindNextGreaterElement_AllElementsHaveNoGreaterElement_ReturnsAllNegativeOnes(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
			_logger.LogDebug("<<< Exiting FindNextGreaterElement_AllElementsHaveNoGreaterElement_ReturnsAllNegativeOnes()");
        }

        [Test]
        public void FindNextGreaterElement_InputWithDuplicates_ReturnsCorrectResult()
        {
			_logger.LogDebug(">>> Entering FindNextGreaterElement_InputWithDuplicates_ReturnsCorrectResult()");
            // Arrange
            int[] input = [4, 4, 3, 2, 4];
            int[] expected = [-1, -1, 4, 4, -1];

            // Act
            var result = FindNextGreaterElement(input);
			_logger.LogDebug("FindNextGreaterElement_InputWithDuplicates_ReturnsCorrectResult(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
			_logger.LogDebug("<<< Exiting FindNextGreaterElement_InputWithDuplicates_ReturnsCorrectResult()");
        }

        [Test]
        public void FindNextGreaterElement_SingleElementArray_ReturnsNegativeOne()
        {
			_logger.LogDebug(">>> Entering FindNextGreaterElement_SingleElementArray_ReturnsNegativeOne()");
            // Arrange
            int[] input = [10];
            int[] expected = [-1];

            // Act
            var result = FindNextGreaterElement(input);
			_logger.LogDebug("FindNextGreaterElement_SingleElementArray_ReturnsNegativeOne(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
			_logger.LogDebug("<<< Exiting FindNextGreaterElement_SingleElementArray_ReturnsNegativeOne()");
        }
    }
}
