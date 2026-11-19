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
			_logger.LogError(">>> Entering IsBalanced(expression={Expression})", expression);
            var checker = new BalancedParenthesesChecker();
			_logger.LogError("IsBalanced(expression={Expression}): checker → {Checker}", expression, checker);
			_logger.LogWarning("<<< Exiting IsBalanced(expression={Expression})", expression);
            return checker.IsBalanced(expression);
        }

        [Test]
        public void IsBalanced_EmptyString_ThrowsArgumentException()
        {
			_logger.LogError(">>> Entering IsBalanced_EmptyString_ThrowsArgumentException()");
            // Arrange
            var expression = string.Empty;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => IsBalanced(expression));
			_logger.LogError("IsBalanced_EmptyString_ThrowsArgumentException(): ex → {Ex}", ex);

            if (ex != null)
            {
                Assert.That(ex.Message, Is.EqualTo("The input expression cannot be null or empty."));
            }

        }

        [Test]
        public void IsBalanced_ValidBalancedExpression_ReturnsTrue()
        {
			_logger.LogError(">>> Entering IsBalanced_ValidBalancedExpression_ReturnsTrue()");
            // Arrange
            var expression = "{[()]}";
			_logger.LogError("IsBalanced_ValidBalancedExpression_ReturnsTrue(): expression → {Expression}", expression);

            // Act
            var result = IsBalanced(expression);
			_logger.LogError("IsBalanced_ValidBalancedExpression_ReturnsTrue(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(true));
			_logger.LogWarning("<<< Exiting IsBalanced_ValidBalancedExpression_ReturnsTrue()");
        }

        [Test]
        public void IsBalanced_ValidUnbalancedExpression_ReturnsFalse()
        {
			_logger.LogError(">>> Entering IsBalanced_ValidUnbalancedExpression_ReturnsFalse()");
            // Arrange
            var expression = "{[(])}";
			_logger.LogError("IsBalanced_ValidUnbalancedExpression_ReturnsFalse(): expression → {Expression}", expression);

            // Act
            var result = IsBalanced(expression);
			_logger.LogError("IsBalanced_ValidUnbalancedExpression_ReturnsFalse(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(false));
			_logger.LogWarning("<<< Exiting IsBalanced_ValidUnbalancedExpression_ReturnsFalse()");
        }

        [Test]
        public void IsBalanced_UnbalancedWithExtraClosingBracket_ReturnsFalse()
        {
			_logger.LogError(">>> Entering IsBalanced_UnbalancedWithExtraClosingBracket_ReturnsFalse()");
            // Arrange
            var expression = "{[()]}]";
			_logger.LogError("IsBalanced_UnbalancedWithExtraClosingBracket_ReturnsFalse(): expression → {Expression}", expression);

            // Act
            var result = IsBalanced(expression);
			_logger.LogError("IsBalanced_UnbalancedWithExtraClosingBracket_ReturnsFalse(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(false));
			_logger.LogWarning("<<< Exiting IsBalanced_UnbalancedWithExtraClosingBracket_ReturnsFalse()");
        }

        [Test]
        public void IsBalanced_ExpressionWithInvalidCharacters_ThrowsArgumentException()
        {
			_logger.LogError(">>> Entering IsBalanced_ExpressionWithInvalidCharacters_ThrowsArgumentException()");
            // Arrange
            var expression = "{[a]}";
			_logger.LogError("IsBalanced_ExpressionWithInvalidCharacters_ThrowsArgumentException(): expression → {Expression}", expression);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => IsBalanced(expression));
			_logger.LogError("IsBalanced_ExpressionWithInvalidCharacters_ThrowsArgumentException(): ex → {Ex}", ex);
            if (ex != null)
            {
                Assert.That(ex.Message, Is.EqualTo("Invalid character 'a' found in the expression."));
            }
			_logger.LogWarning("<<< Exiting IsBalanced_ExpressionWithInvalidCharacters_ThrowsArgumentException()");
        }

        [Test]
        public void IsBalanced_SingleOpeningBracket_ReturnsFalse()
        {
			_logger.LogError(">>> Entering IsBalanced_SingleOpeningBracket_ReturnsFalse()");
            // Arrange
            var expression = "(";

            // Act
            var result = IsBalanced(expression);
			_logger.LogError("IsBalanced_SingleOpeningBracket_ReturnsFalse(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(false));
			_logger.LogWarning("<<< Exiting IsBalanced_SingleOpeningBracket_ReturnsFalse()");
        }

        [Test]
        public void IsBalanced_SingleClosingBracket_ReturnsFalse()
        {
			_logger.LogError(">>> Entering IsBalanced_SingleClosingBracket_ReturnsFalse()");
            // Arrange
            var expression = ")";
			_logger.LogError("IsBalanced_SingleClosingBracket_ReturnsFalse(): expression → {Expression}", expression);

            // Act
            var result = IsBalanced(expression);
			_logger.LogError("IsBalanced_SingleClosingBracket_ReturnsFalse(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(false));
			_logger.LogWarning("<<< Exiting IsBalanced_SingleClosingBracket_ReturnsFalse()");
        }

        [Test]
        public void IsBalanced_ExpressionWithMultipleBalancedBrackets_ReturnsTrue()
        {
			_logger.LogError(">>> Entering IsBalanced_ExpressionWithMultipleBalancedBrackets_ReturnsTrue()");
            // Arrange
            var expression = "[{()}]()";
			_logger.LogError("IsBalanced_ExpressionWithMultipleBalancedBrackets_ReturnsTrue(): expression → {Expression}", expression);

            // Act
            var result = IsBalanced(expression);
			_logger.LogError("IsBalanced_ExpressionWithMultipleBalancedBrackets_ReturnsTrue(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(true));
			_logger.LogWarning("<<< Exiting IsBalanced_ExpressionWithMultipleBalancedBrackets_ReturnsTrue()");
        }
    }
}
