using Algorithms.Stack;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Stack
{
    public class ReverseStackTests
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        public static void Reverse<T>(Stack<T> stack)
        {
			_logger.LogDebug(">>> Entering Reverse(stack={Stack})", stack);
            var obj = new ReverseStack();
			_logger.LogDebug("<<< Exiting Reverse(stack={Stack})", stack);
            obj.Reverse(stack);
        }

        [Test]
        public void Reverse_EmptyStack_DoesNotChangeStack()
        {
			_logger.LogDebug(">>> Entering Reverse_EmptyStack_DoesNotChangeStack()");
            // Arrange
            Stack<int> stack = new Stack<int>();
			_logger.LogDebug("Reverse_EmptyStack_DoesNotChangeStack(): stack → {Stack}", stack);

            // Act
            Reverse(stack);

            // Assert
            Assert.That(stack.Count, Is.EqualTo(0));
			_logger.LogDebug("<<< Exiting Reverse_EmptyStack_DoesNotChangeStack()");
        }

        [Test]
        public void Reverse_SingleElementStack_DoesNotChangeStack()
        {
			_logger.LogDebug(">>> Entering Reverse_SingleElementStack_DoesNotChangeStack()");
            // Arrange
            Stack<int> stack = new Stack<int>();
			_logger.LogDebug("Reverse_SingleElementStack_DoesNotChangeStack(): stack → {Stack}", stack);
            stack.Push(1);

            // Act
            Reverse(stack);

            // Assert
            Assert.That(stack.Count, Is.EqualTo(1));
            Assert.That(stack.Peek(), Is.EqualTo(1));
			_logger.LogDebug("<<< Exiting Reverse_SingleElementStack_DoesNotChangeStack()");
        }

        [Test]
        public void Reverse_MultipleElementStack_ReturnsCorrectOrder()
        {
			_logger.LogDebug(">>> Entering Reverse_MultipleElementStack_ReturnsCorrectOrder()");
            // Arrange
            Stack<int> stack = new Stack<int>();
			_logger.LogDebug("Reverse_MultipleElementStack_ReturnsCorrectOrder(): stack → {Stack}", stack);
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            // The stack is now [3, 2, 1] (top to bottom)

            // Act
            Reverse(stack);

            // Assert
            Assert.That(stack.Count, Is.EqualTo(3));
            Assert.That(stack.Pop(), Is.EqualTo(1)); // Should return 1
            Assert.That(stack.Pop(), Is.EqualTo(2)); // Should return 2
            Assert.That(stack.Pop(), Is.EqualTo(3)); // Should return 3
			_logger.LogDebug("<<< Exiting Reverse_MultipleElementStack_ReturnsCorrectOrder()");
        }

        [Test]
        public void Reverse_StackWithDuplicates_ReturnsCorrectOrder()
        {
			_logger.LogDebug(">>> Entering Reverse_StackWithDuplicates_ReturnsCorrectOrder()");
            // Arrange
            Stack<int> stack = new Stack<int>();
			_logger.LogDebug("Reverse_StackWithDuplicates_ReturnsCorrectOrder(): stack → {Stack}", stack);
            stack.Push(1);
            stack.Push(2);
            stack.Push(1);
            // The stack is now [1, 2, 1] (top to bottom)

            // Act
            Reverse(stack);

            // Assert
            Assert.That(stack.Count, Is.EqualTo(3));
            Assert.That(stack.Pop(), Is.EqualTo(1)); // Should return 1
            Assert.That(stack.Pop(), Is.EqualTo(2)); // Should return 2
            Assert.That(stack.Pop(), Is.EqualTo(1)); // Should return 1
			_logger.LogDebug("<<< Exiting Reverse_StackWithDuplicates_ReturnsCorrectOrder()");
        }
    }
}
