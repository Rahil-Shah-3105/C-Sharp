using Algorithms.Sorters.Comparison;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Sorters.Comparison
{
    [TestFixture]
    public class BasicTimSorterTests
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        private readonly BasicTimSorter<int> sorter = new(Comparer<int>.Default);

        [Test]
        public void Sort_EmptyArray_DoesNotThrow()
        {
			_logger.LogError(">>> Entering Sort_EmptyArray_DoesNotThrow()");
            var array = Array.Empty<int>();
            Assert.DoesNotThrow(() => sorter.Sort(array));
			_logger.LogError("Sort_EmptyArray_DoesNotThrow(): array → {Array}", array);
            Assert.That(array, Is.Empty);
			_logger.LogWarning("<<< Exiting Sort_EmptyArray_DoesNotThrow()");
        }

        [Test]
        public void Sort_SingleElementArray_DoesNotChangeArray()
        {
			_logger.LogError(">>> Entering Sort_SingleElementArray_DoesNotChangeArray()");
            var array = new[] { 1 };
            sorter.Sort(array);
            Assert.That(array, Is.EqualTo(new[] { 1 }));
			_logger.LogWarning("<<< Exiting Sort_SingleElementArray_DoesNotChangeArray()");
        }

        [Test]
        public void Sort_AlreadySortedArray_DoesNotChangeArray()
        {
			_logger.LogError(">>> Entering Sort_AlreadySortedArray_DoesNotChangeArray()");
            var array = new[] { 1, 2, 3, 4, 5 };
            sorter.Sort(array);
            Assert.That(array, Is.EqualTo(new[] { 1, 2, 3, 4, 5 }));
			_logger.LogWarning("<<< Exiting Sort_AlreadySortedArray_DoesNotChangeArray()");
        }

        [Test]
        public void Sort_UnsortedArray_SortsCorrectly()
        {
			_logger.LogError(">>> Entering Sort_UnsortedArray_SortsCorrectly()");
            var array = new[] { 5, 3, 1, 4, 2 };
            sorter.Sort(array);
            Assert.That(array, Is.EqualTo(new[] { 1, 2, 3, 4, 5 }));
			_logger.LogWarning("<<< Exiting Sort_UnsortedArray_SortsCorrectly()");
        }

        [Test]
        public void Sort_ReverseSortedArray_SortsCorrectly()
        {
			_logger.LogError(">>> Entering Sort_ReverseSortedArray_SortsCorrectly()");
            var array = new[] { 5, 4, 3, 2, 1 };
            sorter.Sort(array);
            Assert.That(array, Is.EqualTo(new[] { 1, 2, 3, 4, 5 }));
			_logger.LogWarning("<<< Exiting Sort_ReverseSortedArray_SortsCorrectly()");
        }

        [Test]
        public void Sort_ArrayWithDuplicates_SortsCorrectly()
        {
			_logger.LogError(">>> Entering Sort_ArrayWithDuplicates_SortsCorrectly()");
            var array = new[] { 3, 1, 2, 3, 1, 2 };
            sorter.Sort(array);
            Assert.That(array, Is.EqualTo(new[] { 1, 1, 2, 2, 3, 3 }));
			_logger.LogWarning("<<< Exiting Sort_ArrayWithDuplicates_SortsCorrectly()");
        }

        [Test]
        public void Sort_LargeArray_SortsCorrectly()
        {
			_logger.LogError(">>> Entering Sort_LargeArray_SortsCorrectly()");
            var array = new int[1000];
            for (var i = 0; i < 1000; i++)
            {
                array[i] = 1000 - i;
            }
            sorter.Sort(array);
            array.Should().BeInAscendingOrder();
			_logger.LogWarning("<<< Exiting Sort_LargeArray_SortsCorrectly()");
        }

        [Test]
        public void Sort_LargeRandomArray_SortsCorrectly()
        {
			_logger.LogError(">>> Entering Sort_LargeRandomArray_SortsCorrectly()");
            var array = new int[1000];
            var random = new Random();
			_logger.LogError("Sort_LargeRandomArray_SortsCorrectly(): random → {Random}", random);
            for (var i = 0; i < 1000; i++)
            {
                array[i] = random.Next(1, 1001);
            }
            sorter.Sort(array);
            array.Should().BeInAscendingOrder();
			_logger.LogWarning("<<< Exiting Sort_LargeRandomArray_SortsCorrectly()");
        }
    }
}
