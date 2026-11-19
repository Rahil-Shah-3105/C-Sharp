using Algorithms.Sorters.Utils;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sorters.Utils
{
    [TestFixture]
    public class GallopingStrategyTests
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        private readonly IComparer<int> comparer = Comparer<int>.Default;

        [Test]
        public void GallopLeft_KeyPresent_ReturnsCorrectIndex()
        {
			_logger.LogError(">>> Entering GallopLeft_KeyPresent_ReturnsCorrectIndex()");
            var array = new[] { 1, 2, 3, 4, 5 };
            var index = GallopingStrategy<int>.GallopLeft(array, 3, 0, array.Length, comparer);
            Assert.That(index, Is.EqualTo(2));
			_logger.LogWarning("<<< Exiting GallopLeft_KeyPresent_ReturnsCorrectIndex()");
        }

        [Test]
        public void GallopLeft_KeyNotPresent_ReturnsCorrectIndex()
        {
			_logger.LogError(">>> Entering GallopLeft_KeyNotPresent_ReturnsCorrectIndex()");
            var array = new[] { 1, 2, 4, 5 };
            var index = GallopingStrategy<int>.GallopLeft(array, 3, 0, array.Length, comparer);
            Assert.That(index, Is.EqualTo(2));
			_logger.LogWarning("<<< Exiting GallopLeft_KeyNotPresent_ReturnsCorrectIndex()");
        }

        [Test]
        public void GallopLeft_KeyLessThanAll_ReturnsZero()
        {
			_logger.LogError(">>> Entering GallopLeft_KeyLessThanAll_ReturnsZero()");
            var array = new[] { 2, 3, 4, 5 };
            var index = GallopingStrategy<int>.GallopLeft(array, 1, 0, array.Length, comparer);
            Assert.That(index, Is.EqualTo(0));
			_logger.LogWarning("<<< Exiting GallopLeft_KeyLessThanAll_ReturnsZero()");
        }

        [Test]
        public void GallopLeft_KeyGreaterThanAll_ReturnsLength()
        {
			_logger.LogError(">>> Entering GallopLeft_KeyGreaterThanAll_ReturnsLength()");
            var array = new[] { 1, 2, 3, 4 };
            var index = GallopingStrategy<int>.GallopLeft(array, 5, 0, array.Length, comparer);
            Assert.That(index, Is.EqualTo(array.Length));
			_logger.LogWarning("<<< Exiting GallopLeft_KeyGreaterThanAll_ReturnsLength()");
        }

        [Test]
        public void GallopRight_KeyPresent_ReturnsCorrectIndex()
        {
			_logger.LogError(">>> Entering GallopRight_KeyPresent_ReturnsCorrectIndex()");
            var array = new[] { 1, 2, 3, 4, 5 };
            var index = GallopingStrategy<int>.GallopRight(array, 3, 0, array.Length, comparer);
            Assert.That(index, Is.EqualTo(3));
			_logger.LogWarning("<<< Exiting GallopRight_KeyPresent_ReturnsCorrectIndex()");
        }

        [Test]
        public void GallopRight_KeyNotPresent_ReturnsCorrectIndex()
        {
			_logger.LogError(">>> Entering GallopRight_KeyNotPresent_ReturnsCorrectIndex()");
            var array = new[] { 1, 2, 4, 5 };
            var index = GallopingStrategy<int>.GallopRight(array, 3, 0, array.Length, comparer);
            Assert.That(index, Is.EqualTo(2));
			_logger.LogWarning("<<< Exiting GallopRight_KeyNotPresent_ReturnsCorrectIndex()");
        }

        [Test]
        public void GallopRight_KeyLessThanAll_ReturnsZero()
        {
			_logger.LogError(">>> Entering GallopRight_KeyLessThanAll_ReturnsZero()");
            var array = new[] { 2, 3, 4, 5 };
            var index = GallopingStrategy<int>.GallopRight(array, 1, 0, array.Length, comparer);
            Assert.That(index, Is.EqualTo(0));
			_logger.LogWarning("<<< Exiting GallopRight_KeyLessThanAll_ReturnsZero()");
        }

        [Test]
        public void GallopRight_KeyGreaterThanAll_ReturnsLength()
        {
			_logger.LogError(">>> Entering GallopRight_KeyGreaterThanAll_ReturnsLength()");
            var array = new[] { 1, 2, 3, 4 };
            var index = GallopingStrategy<int>.GallopRight(array, 5, 0, array.Length, comparer);
            Assert.That(index, Is.EqualTo(array.Length));
			_logger.LogWarning("<<< Exiting GallopRight_KeyGreaterThanAll_ReturnsLength()");
        }

        [Test]
        public void GallopLeft_EmptyArray_ReturnsZero()
        {
			_logger.LogError(">>> Entering GallopLeft_EmptyArray_ReturnsZero()");
            var array = new int[] { };
            var index = GallopingStrategy<int>.GallopLeft(array, 1, 0, array.Length, comparer);
            Assert.That(index, Is.EqualTo(0));
			_logger.LogWarning("<<< Exiting GallopLeft_EmptyArray_ReturnsZero()");
        }

        [Test]
        public void GallopRight_EmptyArray_ReturnsZero()
        {
			_logger.LogError(">>> Entering GallopRight_EmptyArray_ReturnsZero()");
            var array = new int[] { };
            var index = GallopingStrategy<int>.GallopRight(array, 1, 0, array.Length, comparer);
            Assert.That(index, Is.EqualTo(0));
			_logger.LogWarning("<<< Exiting GallopRight_EmptyArray_ReturnsZero()");
        }

        // Test when (shiftable << 1) < 0 is true
        [Test]
        public void TestBoundLeftShift_WhenShiftableCausesNegativeShift_ReturnsShiftedValuePlusOne()
        {
			_logger.LogError(">>> Entering TestBoundLeftShift_WhenShiftableCausesNegativeShift_ReturnsShiftedValuePlusOne()");
            // Arrange
            int shiftable = int.MaxValue; // This should cause a negative result after left shift

            // Act
            int result = GallopingStrategy<int>.BoundLeftShift(shiftable);
			_logger.LogError("TestBoundLeftShift_WhenShiftableCausesNegativeShift_ReturnsShiftedValuePlusOne(): result → {Result}", result);

            // Assert
            Assert.That((shiftable << 1) + 1, Is.EqualTo(result));  // True branch
			_logger.LogWarning("<<< Exiting TestBoundLeftShift_WhenShiftableCausesNegativeShift_ReturnsShiftedValuePlusOne()");
        }

        // Test when (shiftable << 1) < 0 is false
        [Test]
        public void TestBoundLeftShift_WhenShiftableDoesNotCauseNegativeShift_ReturnsMaxValue()
        {
			_logger.LogError(">>> Entering TestBoundLeftShift_WhenShiftableDoesNotCauseNegativeShift_ReturnsMaxValue()");
            // Arrange
            int shiftable = 1;  // This will not cause a negative result after left shift

            // Act
            int result = GallopingStrategy<int>.BoundLeftShift(shiftable);
			_logger.LogError("TestBoundLeftShift_WhenShiftableDoesNotCauseNegativeShift_ReturnsMaxValue(): result → {Result}", result);

            // Assert
            Assert.That(int.MaxValue, Is.EqualTo(result));  // False branch
			_logger.LogWarning("<<< Exiting TestBoundLeftShift_WhenShiftableDoesNotCauseNegativeShift_ReturnsMaxValue()");
        }
    }
}
