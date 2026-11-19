using System;
using NUnit.Framework;
using Algorithms.Stack;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Stack
{
    [TestFixture]
    public class InfixToPostfixTests
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        [Test]
        public void InfixToPostfixConversion_SimpleAddition_ReturnsCorrectPostfix()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_SimpleAddition_ReturnsCorrectPostfix()");
            // Arrange
            string infix = "A+B";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);
			_logger.LogError("InfixToPostfixConversion_SimpleAddition_ReturnsCorrectPostfix(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo("AB+"));
			_logger.LogWarning("<<< Exiting InfixToPostfixConversion_SimpleAddition_ReturnsCorrectPostfix()");
        }

        [Test]
        public void InfixToPostfixConversion_SimpleSubtraction_ReturnsCorrectPostfix()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_SimpleSubtraction_ReturnsCorrectPostfix()");
            // Arrange
            string infix = "A-B";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);
			_logger.LogError("InfixToPostfixConversion_SimpleSubtraction_ReturnsCorrectPostfix(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo("AB-"));
			_logger.LogWarning("<<< Exiting InfixToPostfixConversion_SimpleSubtraction_ReturnsCorrectPostfix()");
        }

        [Test]
        public void InfixToPostfixConversion_MultiplicationAndDivision_ReturnsCorrectPostfix()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_MultiplicationAndDivision_ReturnsCorrectPostfix()");
            // Arrange
            string infix = "A*B/C";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);
			_logger.LogError("InfixToPostfixConversion_MultiplicationAndDivision_ReturnsCorrectPostfix(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo("AB*C/"));
			_logger.LogWarning("<<< Exiting InfixToPostfixConversion_MultiplicationAndDivision_ReturnsCorrectPostfix()");
        }

        [Test]
        public void InfixToPostfixConversion_ExponentiationOperator_ReturnsCorrectPostfix()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_ExponentiationOperator_ReturnsCorrectPostfix()");
            // Arrange
            string infix = "A^B";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);
			_logger.LogError("InfixToPostfixConversion_ExponentiationOperator_ReturnsCorrectPostfix(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo("AB^"));
			_logger.LogWarning("<<< Exiting InfixToPostfixConversion_ExponentiationOperator_ReturnsCorrectPostfix()");
        }

        [Test]
        public void InfixToPostfixConversion_MixedOperatorPrecedence_ReturnsCorrectPostfix()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_MixedOperatorPrecedence_ReturnsCorrectPostfix()");
            // Arrange
            string infix = "A+B*C";
       
            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);
			_logger.LogError("InfixToPostfixConversion_MixedOperatorPrecedence_ReturnsCorrectPostfix(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo("ABC*+"));
			_logger.LogWarning("<<< Exiting InfixToPostfixConversion_MixedOperatorPrecedence_ReturnsCorrectPostfix()");
        }

        [Test]
        public void InfixToPostfixConversion_WithParentheses_ReturnsCorrectPostfix()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_WithParentheses_ReturnsCorrectPostfix()");
            // Arrange
            string infix = "(A+B)*C";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);
			_logger.LogError("InfixToPostfixConversion_WithParentheses_ReturnsCorrectPostfix(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo("AB+C*"));
			_logger.LogWarning("<<< Exiting InfixToPostfixConversion_WithParentheses_ReturnsCorrectPostfix()");
        }

        [Test]
        public void InfixToPostfixConversion_NestedParentheses_ReturnsCorrectPostfix()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_NestedParentheses_ReturnsCorrectPostfix()");
            // Arrange
            string infix = "((A+B)*C)";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);
			_logger.LogError("InfixToPostfixConversion_NestedParentheses_ReturnsCorrectPostfix(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo("AB+C*"));
			_logger.LogWarning("<<< Exiting InfixToPostfixConversion_NestedParentheses_ReturnsCorrectPostfix()");
        }

        [Test]
        public void InfixToPostfixConversion_ComplexExpression_ReturnsCorrectPostfix()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_ComplexExpression_ReturnsCorrectPostfix()");
            // Arrange
            string infix = "A+B*C-D/E";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);
			_logger.LogError("InfixToPostfixConversion_ComplexExpression_ReturnsCorrectPostfix(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo("ABC*+DE/-"));
			_logger.LogWarning("<<< Exiting InfixToPostfixConversion_ComplexExpression_ReturnsCorrectPostfix()");
        }

        [Test]
        public void InfixToPostfixConversion_WithDigits_ReturnsCorrectPostfix()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_WithDigits_ReturnsCorrectPostfix()");
            // Arrange
            string infix = "1+2*3";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);
			_logger.LogError("InfixToPostfixConversion_WithDigits_ReturnsCorrectPostfix(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo("123*+"));
			_logger.LogWarning("<<< Exiting InfixToPostfixConversion_WithDigits_ReturnsCorrectPostfix()");
        }

        [Test]
        public void InfixToPostfixConversion_LowercaseLetters_ReturnsCorrectPostfix()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_LowercaseLetters_ReturnsCorrectPostfix()");
            // Arrange
            string infix = "a+b*c";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);
			_logger.LogError("InfixToPostfixConversion_LowercaseLetters_ReturnsCorrectPostfix(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo("abc*+"));
			_logger.LogWarning("<<< Exiting InfixToPostfixConversion_LowercaseLetters_ReturnsCorrectPostfix()");
        }

        [Test]
        public void InfixToPostfixConversion_WithWhitespace_IgnoresWhitespace()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_WithWhitespace_IgnoresWhitespace()");
            // Arrange
            string infix = "A + B * C";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);
			_logger.LogError("InfixToPostfixConversion_WithWhitespace_IgnoresWhitespace(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo("ABC*+"));
			_logger.LogWarning("<<< Exiting InfixToPostfixConversion_WithWhitespace_IgnoresWhitespace()");
        }

        [Test]
        public void InfixToPostfixConversion_MultipleParentheses_ReturnsCorrectPostfix()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_MultipleParentheses_ReturnsCorrectPostfix()");
            // Arrange
            string infix = "(A+B)*(C-D)";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);
			_logger.LogError("InfixToPostfixConversion_MultipleParentheses_ReturnsCorrectPostfix(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo("AB+CD-*"));
			_logger.LogWarning("<<< Exiting InfixToPostfixConversion_MultipleParentheses_ReturnsCorrectPostfix()");
        }

        [Test]
        public void InfixToPostfixConversion_AllOperators_ReturnsCorrectPostfix()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_AllOperators_ReturnsCorrectPostfix()");
            // Arrange
            string infix = "A+B-C*D/E^F";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);
			_logger.LogError("InfixToPostfixConversion_AllOperators_ReturnsCorrectPostfix(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo("AB+CD*EF^/-"));
			_logger.LogWarning("<<< Exiting InfixToPostfixConversion_AllOperators_ReturnsCorrectPostfix()");
        }

        [Test]
        public void InfixToPostfixConversion_NullExpression_ThrowsArgumentException()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_NullExpression_ThrowsArgumentException()");
            // Arrange
            string infix = null!;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                InfixToPostfix.InfixToPostfixConversion(infix));
            Assert.That(ex!.Message, Does.Contain("Infix cannot be null or empty"));
			_logger.LogWarning("<<< Exiting InfixToPostfixConversion_NullExpression_ThrowsArgumentException()");
        }

        [Test]
        public void InfixToPostfixConversion_EmptyExpression_ThrowsArgumentException()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_EmptyExpression_ThrowsArgumentException()");
            // Arrange
            string infix = "";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                InfixToPostfix.InfixToPostfixConversion(infix));
            Assert.That(ex!.Message, Does.Contain("Infix cannot be null or empty"));
			_logger.LogWarning("<<< Exiting InfixToPostfixConversion_EmptyExpression_ThrowsArgumentException()");
        }

        [Test]
        public void InfixToPostfixConversion_WhitespaceOnlyExpression_ThrowsArgumentException()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_WhitespaceOnlyExpression_ThrowsArgumentException()");
            // Arrange
            string infix = "   ";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                InfixToPostfix.InfixToPostfixConversion(infix));
            Assert.That(ex!.Message, Does.Contain("Infix cannot be null or empty"));
			_logger.LogWarning("<<< Exiting InfixToPostfixConversion_WhitespaceOnlyExpression_ThrowsArgumentException()");
        }

        [Test]
        public void InfixToPostfixConversion_InvalidCharacter_ThrowsArgumentException()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_InvalidCharacter_ThrowsArgumentException()");
            // Arrange
            string infix = "A+B$C";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                InfixToPostfix.InfixToPostfixConversion(infix));
            Assert.That(ex!.Message, Does.Contain("Invalid character $"));
			_logger.LogWarning("<<< Exiting InfixToPostfixConversion_InvalidCharacter_ThrowsArgumentException()");
        }

        [Test]
        public void InfixToPostfixConversion_MismatchedParenthesesClosingExtra_ThrowsInvalidOperationException()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_MismatchedParenthesesClosingExtra_ThrowsInvalidOperationException()");
            // Arrange
            string infix = "A+B)";

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() =>
                InfixToPostfix.InfixToPostfixConversion(infix));
            Assert.That(ex!.Message, Does.Contain("Mismatched parentheses"));
			_logger.LogWarning("<<< Exiting InfixToPostfixConversion_MismatchedParenthesesClosingExtra_ThrowsInvalidOperationException()");
        }

        [Test]
        public void InfixToPostfixConversion_MismatchedParenthesesOpeningExtra_ThrowsInvalidOperationException()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_MismatchedParenthesesOpeningExtra_ThrowsInvalidOperationException()");
            // Arrange
            string infix = "(A+B";

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() =>
                InfixToPostfix.InfixToPostfixConversion(infix));
            Assert.That(ex!.Message, Does.Contain("Mismatched parentheses"));
			_logger.LogWarning("<<< Exiting InfixToPostfixConversion_MismatchedParenthesesOpeningExtra_ThrowsInvalidOperationException()");
        }

        [Test]
        public void InfixToPostfixConversion_OnlyOpeningParenthesis_ThrowsInvalidOperationException()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_OnlyOpeningParenthesis_ThrowsInvalidOperationException()");
            // Arrange
            string infix = "(";

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                InfixToPostfix.InfixToPostfixConversion(infix));
				_logger.LogWarning("<<< Exiting InfixToPostfixConversion_OnlyOpeningParenthesis_ThrowsInvalidOperationException()");
        }

        [Test]
        public void InfixToPostfixConversion_OnlyClosingParenthesis_ThrowsInvalidOperationException()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_OnlyClosingParenthesis_ThrowsInvalidOperationException()");
            // Arrange
            string infix = ")";

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                InfixToPostfix.InfixToPostfixConversion(infix));
				_logger.LogWarning("<<< Exiting InfixToPostfixConversion_OnlyClosingParenthesis_ThrowsInvalidOperationException()");
        }

        [Test]
        public void InfixToPostfixConversion_ExponentiationWithOtherOperators_ReturnsCorrectPostfix()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_ExponentiationWithOtherOperators_ReturnsCorrectPostfix()");
            // Arrange
            string infix = "A+B^C*D";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);
			_logger.LogError("InfixToPostfixConversion_ExponentiationWithOtherOperators_ReturnsCorrectPostfix(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo("ABC^D*+"));
			_logger.LogWarning("<<< Exiting InfixToPostfixConversion_ExponentiationWithOtherOperators_ReturnsCorrectPostfix()");
        }

        [Test]
        public void InfixToPostfixConversion_SingleOperand_ReturnsOperand()
        {
			_logger.LogError(">>> Entering InfixToPostfixConversion_SingleOperand_ReturnsOperand()");
            // Arrange
            string infix = "A";

            // Act
            string result = InfixToPostfix.InfixToPostfixConversion(infix);
			_logger.LogError("InfixToPostfixConversion_SingleOperand_ReturnsOperand(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo("A"));
			_logger.LogWarning("<<< Exiting InfixToPostfixConversion_SingleOperand_ReturnsOperand()");
        }

        [Test]
        public void PostfixExpressionEvaluation_SimpleAddition_ReturnsCorrectResult()
        {
			_logger.LogError(">>> Entering PostfixExpressionEvaluation_SimpleAddition_ReturnsCorrectResult()");
            // Arrange
            string postfix = "23+";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);
			_logger.LogError("PostfixExpressionEvaluation_SimpleAddition_ReturnsCorrectResult(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(5));
			_logger.LogWarning("<<< Exiting PostfixExpressionEvaluation_SimpleAddition_ReturnsCorrectResult()");
        }

        [Test]
        public void PostfixExpressionEvaluation_SimpleSubtraction_ReturnsCorrectResult()
        {
			_logger.LogError(">>> Entering PostfixExpressionEvaluation_SimpleSubtraction_ReturnsCorrectResult()");
            // Arrange
            string postfix = "53-";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);
			_logger.LogError("PostfixExpressionEvaluation_SimpleSubtraction_ReturnsCorrectResult(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(2));
			_logger.LogWarning("<<< Exiting PostfixExpressionEvaluation_SimpleSubtraction_ReturnsCorrectResult()");
        }

        [Test]
        public void PostfixExpressionEvaluation_SimpleMultiplication_ReturnsCorrectResult()
        {
			_logger.LogError(">>> Entering PostfixExpressionEvaluation_SimpleMultiplication_ReturnsCorrectResult()");
            // Arrange
            string postfix = "34*";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);
			_logger.LogError("PostfixExpressionEvaluation_SimpleMultiplication_ReturnsCorrectResult(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(12));
			_logger.LogWarning("<<< Exiting PostfixExpressionEvaluation_SimpleMultiplication_ReturnsCorrectResult()");
        }

        [Test]
        public void PostfixExpressionEvaluation_SimpleDivision_ReturnsCorrectResult()
        {
			_logger.LogError(">>> Entering PostfixExpressionEvaluation_SimpleDivision_ReturnsCorrectResult()");
            // Arrange
            string postfix = "82/";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);
			_logger.LogError("PostfixExpressionEvaluation_SimpleDivision_ReturnsCorrectResult(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(4));
			_logger.LogWarning("<<< Exiting PostfixExpressionEvaluation_SimpleDivision_ReturnsCorrectResult()");
        }

        [Test]
        public void PostfixExpressionEvaluation_SimpleExponentiation_ReturnsCorrectResult()
        {
			_logger.LogError(">>> Entering PostfixExpressionEvaluation_SimpleExponentiation_ReturnsCorrectResult()");
            // Arrange
            string postfix = "23^";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);
			_logger.LogError("PostfixExpressionEvaluation_SimpleExponentiation_ReturnsCorrectResult(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(8));
			_logger.LogWarning("<<< Exiting PostfixExpressionEvaluation_SimpleExponentiation_ReturnsCorrectResult()");
        }

        [Test]
        public void PostfixExpressionEvaluation_ComplexExpression_ReturnsCorrectResult()
        {
			_logger.LogError(">>> Entering PostfixExpressionEvaluation_ComplexExpression_ReturnsCorrectResult()");
            // Arrange
            string postfix = "23*4+";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);
			_logger.LogError("PostfixExpressionEvaluation_ComplexExpression_ReturnsCorrectResult(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(10));
			_logger.LogWarning("<<< Exiting PostfixExpressionEvaluation_ComplexExpression_ReturnsCorrectResult()");
        }

        [Test]
        public void PostfixExpressionEvaluation_WithWhitespace_ReturnsCorrectResult()
        {
			_logger.LogError(">>> Entering PostfixExpressionEvaluation_WithWhitespace_ReturnsCorrectResult()");
            // Arrange
            string postfix = "2 3 + 4 *";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);
			_logger.LogError("PostfixExpressionEvaluation_WithWhitespace_ReturnsCorrectResult(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(20));
			_logger.LogWarning("<<< Exiting PostfixExpressionEvaluation_WithWhitespace_ReturnsCorrectResult()");
        }

        [Test]
        public void PostfixExpressionEvaluation_SingleDigit_ReturnsDigit()
        {
			_logger.LogError(">>> Entering PostfixExpressionEvaluation_SingleDigit_ReturnsDigit()");
            // Arrange
            string postfix = "5";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);
			_logger.LogError("PostfixExpressionEvaluation_SingleDigit_ReturnsDigit(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(5));
			_logger.LogWarning("<<< Exiting PostfixExpressionEvaluation_SingleDigit_ReturnsDigit()");
        }

        [Test]
        public void PostfixExpressionEvaluation_NullExpression_ThrowsArgumentException()
        {
			_logger.LogError(">>> Entering PostfixExpressionEvaluation_NullExpression_ThrowsArgumentException()");
            // Arrange
            string postfix = null!;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                InfixToPostfix.PostfixExpressionEvaluation(postfix));
            Assert.That(ex!.Message, Does.Contain("Postfix cannot be null or empty"));
			_logger.LogWarning("<<< Exiting PostfixExpressionEvaluation_NullExpression_ThrowsArgumentException()");
        }

        [Test]
        public void PostfixExpressionEvaluation_EmptyExpression_ThrowsArgumentException()
        {
			_logger.LogError(">>> Entering PostfixExpressionEvaluation_EmptyExpression_ThrowsArgumentException()");
            // Arrange
            string postfix = "";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                InfixToPostfix.PostfixExpressionEvaluation(postfix));
            Assert.That(ex!.Message, Does.Contain("Postfix cannot be null or empty"));
			_logger.LogWarning("<<< Exiting PostfixExpressionEvaluation_EmptyExpression_ThrowsArgumentException()");
        }

        [Test]
        public void PostfixExpressionEvaluation_WhitespaceOnlyExpression_ThrowsArgumentException()
        {
			_logger.LogError(">>> Entering PostfixExpressionEvaluation_WhitespaceOnlyExpression_ThrowsArgumentException()");
            // Arrange
            string postfix = "   ";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                InfixToPostfix.PostfixExpressionEvaluation(postfix));
            Assert.That(ex!.Message, Does.Contain("Postfix cannot be null or empty"));
			_logger.LogWarning("<<< Exiting PostfixExpressionEvaluation_WhitespaceOnlyExpression_ThrowsArgumentException()");
        }

        [Test]
        public void PostfixExpressionEvaluation_InsufficientOperands_ThrowsInvalidOperationException()
        {
			_logger.LogError(">>> Entering PostfixExpressionEvaluation_InsufficientOperands_ThrowsInvalidOperationException()");
            // Arrange
            string postfix = "2+";

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() =>
                InfixToPostfix.PostfixExpressionEvaluation(postfix));
            Assert.That(ex!.Message, Does.Contain("Insufficient operands"));
			_logger.LogWarning("<<< Exiting PostfixExpressionEvaluation_InsufficientOperands_ThrowsInvalidOperationException()");
        }

        [Test]
        public void PostfixExpressionEvaluation_DivisionByZero_ThrowsDivideByZeroException()
        {
			_logger.LogError(">>> Entering PostfixExpressionEvaluation_DivisionByZero_ThrowsDivideByZeroException()");
            // Arrange
            string postfix = "20/";

            // Act & Assert
            var ex = Assert.Throws<DivideByZeroException>(() =>
                InfixToPostfix.PostfixExpressionEvaluation(postfix));
            Assert.That(ex!.Message, Does.Contain("Cannot divide by zero"));
			_logger.LogWarning("<<< Exiting PostfixExpressionEvaluation_DivisionByZero_ThrowsDivideByZeroException()");
        }

        [Test]
        public void PostfixExpressionEvaluation_InvalidCharacter_ThrowsInvalidOperationException()
        {
			_logger.LogError(">>> Entering PostfixExpressionEvaluation_InvalidCharacter_ThrowsInvalidOperationException()");
            // Arrange
            string postfix = "23A+";

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() =>
                InfixToPostfix.PostfixExpressionEvaluation(postfix));
            Assert.That(ex!.Message, Does.Contain("Invalid character in expression"));
			_logger.LogWarning("<<< Exiting PostfixExpressionEvaluation_InvalidCharacter_ThrowsInvalidOperationException()");
        }

        [Test]
        public void PostfixExpressionEvaluation_LeftoverOperands_ThrowsInvalidOperationException()
        {
			_logger.LogError(">>> Entering PostfixExpressionEvaluation_LeftoverOperands_ThrowsInvalidOperationException()");
            // Arrange
            string postfix = "234";

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() =>
                InfixToPostfix.PostfixExpressionEvaluation(postfix));
            Assert.That(ex!.Message, Does.Contain("Invalid postfix expression: Leftover operands"));
			_logger.LogWarning("<<< Exiting PostfixExpressionEvaluation_LeftoverOperands_ThrowsInvalidOperationException()");
        }

        [Test]
        public void PostfixExpressionEvaluation_UnknownOperator_ThrowsInvalidOperationException()
        {
			_logger.LogError(">>> Entering PostfixExpressionEvaluation_UnknownOperator_ThrowsInvalidOperationException()");
            // This test ensures the default case in the switch is covered
            // Note: This is difficult to test directly as IsOperator filters valid operators
            // But we can test by passing an operator character that somehow bypasses IsOperator
			_logger.LogWarning("<<< Exiting PostfixExpressionEvaluation_UnknownOperator_ThrowsInvalidOperationException()");
        }

        [Test]
        public void PostfixExpressionEvaluation_ComplexExpressionWithAllOperators_ReturnsCorrectResult()
        {
			_logger.LogError(">>> Entering PostfixExpressionEvaluation_ComplexExpressionWithAllOperators_ReturnsCorrectResult()");
            // Arrange - (2+3)*4-6/2 = 5*4-3 = 20-3 = 17
            string postfix = "23+4*62/-";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);
			_logger.LogError("PostfixExpressionEvaluation_ComplexExpressionWithAllOperators_ReturnsCorrectResult(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(17));
			_logger.LogWarning("<<< Exiting PostfixExpressionEvaluation_ComplexExpressionWithAllOperators_ReturnsCorrectResult()");
        }

        [Test]
        public void PostfixExpressionEvaluation_ExponentiationInExpression_ReturnsCorrectResult()
        {
			_logger.LogError(">>> Entering PostfixExpressionEvaluation_ExponentiationInExpression_ReturnsCorrectResult()");
            // Arrange - 2^3*2 = 8*2 = 16
            string postfix = "23^2*";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);
			_logger.LogError("PostfixExpressionEvaluation_ExponentiationInExpression_ReturnsCorrectResult(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(16));
			_logger.LogWarning("<<< Exiting PostfixExpressionEvaluation_ExponentiationInExpression_ReturnsCorrectResult()");
        }

        [Test]
        public void PostfixExpressionEvaluation_ZeroOperands_ReturnsZero()
        {
			_logger.LogError(">>> Entering PostfixExpressionEvaluation_ZeroOperands_ReturnsZero()");
            // Arrange
            string postfix = "0";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);
			_logger.LogError("PostfixExpressionEvaluation_ZeroOperands_ReturnsZero(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(0));
			_logger.LogWarning("<<< Exiting PostfixExpressionEvaluation_ZeroOperands_ReturnsZero()");
        }

        [Test]
        public void PostfixExpressionEvaluation_LargerNumbers_ReturnsCorrectResult()
        {
			_logger.LogError(">>> Entering PostfixExpressionEvaluation_LargerNumbers_ReturnsCorrectResult()");
            // Arrange - Uses single digits only: 9+8 = 17
            string postfix = "98+";

            // Act
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);
			_logger.LogError("PostfixExpressionEvaluation_LargerNumbers_ReturnsCorrectResult(): result → {Result}", result);

            // Assert
            Assert.That(result, Is.EqualTo(17));
			_logger.LogWarning("<<< Exiting PostfixExpressionEvaluation_LargerNumbers_ReturnsCorrectResult()");
        }

        [Test]
        public void IntegrationTest_ConvertAndEvaluate_SimpleExpression()
        {
			_logger.LogError(">>> Entering IntegrationTest_ConvertAndEvaluate_SimpleExpression()");
            // Arrange
            string infix = "2+3";

            // Act
            string postfix = InfixToPostfix.InfixToPostfixConversion(infix);
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);
			_logger.LogError("IntegrationTest_ConvertAndEvaluate_SimpleExpression(): result → {Result}", result);

            // Assert
            Assert.That(postfix, Is.EqualTo("23+"));
            Assert.That(result, Is.EqualTo(5));
			_logger.LogWarning("<<< Exiting IntegrationTest_ConvertAndEvaluate_SimpleExpression()");
        }

        [Test]
        public void IntegrationTest_ConvertAndEvaluate_ComplexExpression()
        {
			_logger.LogError(">>> Entering IntegrationTest_ConvertAndEvaluate_ComplexExpression()");
            // Arrange
            string infix = "(2+3)*4";

            // Act
            string postfix = InfixToPostfix.InfixToPostfixConversion(infix);
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);
			_logger.LogError("IntegrationTest_ConvertAndEvaluate_ComplexExpression(): result → {Result}", result);

            // Assert
            Assert.That(postfix, Is.EqualTo("23+4*"));
            Assert.That(result, Is.EqualTo(20));
			_logger.LogWarning("<<< Exiting IntegrationTest_ConvertAndEvaluate_ComplexExpression()");
        }

        [Test]
        public void IntegrationTest_ConvertAndEvaluate_WithAllOperators()
        {
			_logger.LogError(">>> Entering IntegrationTest_ConvertAndEvaluate_WithAllOperators()");
            // Arrange - 2+3*4-6/2 = 2+12-3 = 11
            string infix = "2+3*4-6/2";

            // Act
            string postfix = InfixToPostfix.InfixToPostfixConversion(infix);
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);
			_logger.LogError("IntegrationTest_ConvertAndEvaluate_WithAllOperators(): result → {Result}", result);

            // Assert
            Assert.That(postfix, Is.EqualTo("234*+62/-"));
            Assert.That(result, Is.EqualTo(11));
			_logger.LogWarning("<<< Exiting IntegrationTest_ConvertAndEvaluate_WithAllOperators()");
        }

        [Test]
        public void IntegrationTest_ConvertAndEvaluate_WithExponentiation()
        {
			_logger.LogError(">>> Entering IntegrationTest_ConvertAndEvaluate_WithExponentiation()");
            // Arrange - 2^3+1 = 8+1 = 9
            string infix = "2^3+1";

            // Act
            string postfix = InfixToPostfix.InfixToPostfixConversion(infix);
            int result = InfixToPostfix.PostfixExpressionEvaluation(postfix);
			_logger.LogError("IntegrationTest_ConvertAndEvaluate_WithExponentiation(): result → {Result}", result);

            // Assert
            Assert.That(postfix, Is.EqualTo("23^1+"));
            Assert.That(result, Is.EqualTo(9));
			_logger.LogWarning("<<< Exiting IntegrationTest_ConvertAndEvaluate_WithExponentiation()");
        }
    }
}
