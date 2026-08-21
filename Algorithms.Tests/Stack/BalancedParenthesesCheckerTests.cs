using Algorithms.Stack;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Stack
{
    [TestFixture]
    public class BalancedParenthesesCheckerTests
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        public static bool IsBalanced(string expression)
        {
			_logger.LogDebug(">>> Entering IsBalanced(expression={Expression})", expression);
            var checker = new BalancedParenthesesChecker();
			_logger.LogDebug("IsBalanced(expression={Expression}): checker → {Checker}", expression, checker);
			_logger.LogDebug("<<< Exiting IsBalanced(expression={Expression})", expression);
            return checker.IsBalanced(expression);
        }

        [Test]
        public void IsBalanced_EmptyString_ThrowsArgumentException()
        {
			_logger.LogDebug(">>> Entering IsBalanced_EmptyString_ThrowsArgumentException()");
            // Arrange
            var expression = string.Empty;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => IsBalanced(expression));
			_logger.LogDebug("IsBalanced_EmptyString_ThrowsArgumentException(): ex → {Ex}", ex);

            if (ex != null)
            {
                Assert.That(ex.Message, Is.EqualTo("The input expression cannot be null or empty."));
			_logger.LogDebug("<<< Exiting IsBalanced_EmptyString_ThrowsArgumentException()");
            }

        }

        [Test]
        public void IsBalanced_ValidBalancedExpression_ReturnsTrue()
        {
			_logger.LogDebug(">>> Entering IsBalanced_ValidBalancedExpression_ReturnsTrue()");
            // Arrange
            var expression = "{[()]}";
			_logger.LogDebug("IsBalanced_ValidBalancedExpression_ReturnsTrue(): expression → {Expression}", expression);

            // Act
            var result = IsBalanced(expression);
			_logger.LogDebug("IsBalanced_ValidBalancedExpression_ReturnsTrue(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(true));
			_logger.LogDebug("<<< Exiting IsBalanced_ValidBalancedExpression_ReturnsTrue()");
        }

        [Test]
        public void IsBalanced_ValidUnbalancedExpression_ReturnsFalse()
        {
			_logger.LogDebug(">>> Entering IsBalanced_ValidUnbalancedExpression_ReturnsFalse()");
            // Arrange
            var expression = "{[(])}";
			_logger.LogDebug("IsBalanced_ValidUnbalancedExpression_ReturnsFalse(): expression → {Expression}", expression);

            // Act
            var result = IsBalanced(expression);
			_logger.LogDebug("IsBalanced_ValidUnbalancedExpression_ReturnsFalse(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(false));
			_logger.LogDebug("<<< Exiting IsBalanced_ValidUnbalancedExpression_ReturnsFalse()");
        }

        [Test]
        public void IsBalanced_UnbalancedWithExtraClosingBracket_ReturnsFalse()
        {
			_logger.LogDebug(">>> Entering IsBalanced_UnbalancedWithExtraClosingBracket_ReturnsFalse()");
            // Arrange
            var expression = "{[()]}]";
			_logger.LogDebug("IsBalanced_UnbalancedWithExtraClosingBracket_ReturnsFalse(): expression → {Expression}", expression);

            // Act
            var result = IsBalanced(expression);
			_logger.LogDebug("IsBalanced_UnbalancedWithExtraClosingBracket_ReturnsFalse(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(false));
			_logger.LogDebug("<<< Exiting IsBalanced_UnbalancedWithExtraClosingBracket_ReturnsFalse()");
        }

        [Test]
        public void IsBalanced_ExpressionWithInvalidCharacters_ThrowsArgumentException()
        {
			_logger.LogDebug(">>> Entering IsBalanced_ExpressionWithInvalidCharacters_ThrowsArgumentException()");
            // Arrange
            var expression = "{[a]}";
			_logger.LogDebug("IsBalanced_ExpressionWithInvalidCharacters_ThrowsArgumentException(): expression → {Expression}", expression);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => IsBalanced(expression));
			_logger.LogDebug("IsBalanced_ExpressionWithInvalidCharacters_ThrowsArgumentException(): ex → {Ex}", ex);
            if (ex != null)
            {
                Assert.That(ex.Message, Is.EqualTo("Invalid character 'a' found in the expression."));
            }
			_logger.LogDebug("<<< Exiting IsBalanced_ExpressionWithInvalidCharacters_ThrowsArgumentException()");
        }

        [Test]
        public void IsBalanced_SingleOpeningBracket_ReturnsFalse()
        {
			_logger.LogDebug(">>> Entering IsBalanced_SingleOpeningBracket_ReturnsFalse()");
            // Arrange
            var expression = "(";

            // Act
            var result = IsBalanced(expression);
			_logger.LogDebug("IsBalanced_SingleOpeningBracket_ReturnsFalse(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(false));
			_logger.LogDebug("<<< Exiting IsBalanced_SingleOpeningBracket_ReturnsFalse()");
        }

        [Test]
        public void IsBalanced_SingleClosingBracket_ReturnsFalse()
        {
			_logger.LogDebug(">>> Entering IsBalanced_SingleClosingBracket_ReturnsFalse()");
            // Arrange
            var expression = ")";
			_logger.LogDebug("IsBalanced_SingleClosingBracket_ReturnsFalse(): expression → {Expression}", expression);

            // Act
            var result = IsBalanced(expression);
			_logger.LogDebug("IsBalanced_SingleClosingBracket_ReturnsFalse(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(false));
			_logger.LogDebug("<<< Exiting IsBalanced_SingleClosingBracket_ReturnsFalse()");
        }

        [Test]
        public void IsBalanced_ExpressionWithMultipleBalancedBrackets_ReturnsTrue()
        {
			_logger.LogDebug(">>> Entering IsBalanced_ExpressionWithMultipleBalancedBrackets_ReturnsTrue()");
            // Arrange
            var expression = "[{()}]()";
			_logger.LogDebug("IsBalanced_ExpressionWithMultipleBalancedBrackets_ReturnsTrue(): expression → {Expression}", expression);

            // Act
            var result = IsBalanced(expression);
			_logger.LogDebug("IsBalanced_ExpressionWithMultipleBalancedBrackets_ReturnsTrue(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(true));
			_logger.LogDebug("<<< Exiting IsBalanced_ExpressionWithMultipleBalancedBrackets_ReturnsTrue()");
        }
    }
}
